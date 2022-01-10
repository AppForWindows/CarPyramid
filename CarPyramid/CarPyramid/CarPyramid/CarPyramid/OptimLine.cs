using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace CarPyramid.LineOpt
{
	public class OptimLine
	{
		public Item[] Items
		{ get; private set; }
		public int Lenght
		{ get; private set; }
		private OptimSource[] Sources;
		public OptimLine(IEnumerable<Item> Items, int Lenght, List<OptimSource> Sources)
		{
			this.Items = Items.Where(i => 0 < i.Size && i.Size <= Lenght).OrderBy(i => i.Guid).ToArray();
			this.Lenght = Lenght;
			this.Sources = Sources.OrderBy(s => s.Size).ToArray();
        }
		public OptimResult Optimize()
		{
			Population Population = new Population(8, 0, Sources);
			#region Person 1
			List<OptimSeries> OneToOne = new List<OptimSeries>();
			for (int i = 0; i < Items.Length; i++)
				try
				{
					OptimSource OS = Sources.FirstOrDefault(s => s.Size >= Items[i].Size && !OneToOne.Any(o => o.SourceGuid == s.Guid));
					if (OS != null)
						OneToOne.Add(new OptimSeries(new List<Item> { Items[i] }, OS.Size, OS.Guid));
					else
						OneToOne.Add(new OptimSeries(new List<Item> { Items[i] }, this.Lenght));
				}
				catch
				{
					OneToOne.Add(new OptimSeries(new List<Item> { Items[i] }, this.Lenght));
				}
			Population.Add(new OptimResult(OneToOne));
			#endregion
			#region Person 2
			List<OptimSeries> FillOst = new List<OptimSeries>();
			List<Item> ItemList = Items.OrderByDescending(i => i.Size).ToList();
			foreach (OptimSource os in Sources.OrderByDescending(s => s.Size))
			{
				List<Item> tList = new List<Item>();
				foreach (Item it in ItemList.Where(i => i.Size <= os.Size).OrderByDescending(i => i.Size).ToArray())
					if (tList.Sum(i => i.Size) + it.Size <= os.Size)
					{
						tList.Add(it);
						ItemList.Remove(it);
					}
					else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > os.Size))
						break;
				FillOst.Add(new OptimSeries(tList, os.Size, os.Guid));
			}
			while (ItemList.Count > 0)
			{
				List<Item> tList = new List<Item>();
				foreach (Item it in ItemList.OrderByDescending(i => i.Size).ToArray())
					if (tList.Sum(i => i.Size) + it.Size <= this.Lenght)
					{
						tList.Add(it);
						ItemList.Remove(it);
					}
					else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > this.Lenght))
						break;
				FillOst.Add(new OptimSeries(tList, this.Lenght));
			}
			Population.Add(new OptimResult(FillOst));
			#endregion
			int ReplayBest = 0;
			while (Population.Generation <= Population.Persons * Population.Persons && ReplayBest <= 4)
			{
				OptimResult PrevResult = Population.BestResult;
				Population NexPopulation = Population.Next(this.Lenght);
				OptimResult NextResult = NexPopulation.BestResult;
				//if (PrevResult.SourcesSizeWork > NextResult.SourcesSizeWork || PrevResult.Length < NextResult.Length || PrevResult.Length == NextResult.Length && PrevResult.BestRest > NextResult.BestRest)
				//	break;
				//else
				if (PrevResult.Count == NextResult.Count && PrevResult.BestRest == NextResult.BestRest && PrevResult.SourcesSizeWork == NextResult.SourcesSizeWork)
					ReplayBest += 1;
				else if (PrevResult.Count >= NextResult.Count && PrevResult.BestRest <= NextResult.BestRest && PrevResult.SourcesSizeWork <= NextResult.SourcesSizeWork)
					ReplayBest = 0;
				Population = NexPopulation;

				GC.Collect(); GC.Collect();
			}
			Generation = Population.Generation;
            return Population.BestResult;
		}
		public int Generation = 0;
		class Population : List<OptimResult>
		{
			public int Persons
			{ get; private set; }
			public int Generation
			{ get; private set; }
			private OptimSource[] Sources;
			public Population(int Persons, int Generation, OptimSource[] Sources)
			{
				this.Persons = Persons;
				this.Generation = Generation;
				this.Sources = Sources;
            }
			public Population Next(int SizeSeries)
			{
				Population Next = new Population(this.Persons, this.Generation + 1, this.Sources);
				Next.AddRange(this);

				foreach (OptimResult R1 in this.ToArray())
					foreach (OptimResult R2 in this.ToArray())
						foreach (OptimResult Ch in Child(R1, R2, Next.Sources))
							if (!Next.HasLike(Ch))
								Next.Add(Ch);

				Next.Sort(new OptimResultSorter());
				if (Next.Count >= Persons)
				{
					//Next[new Random().Next(1, Persons)].Mutate(Sources);
					for (int m = Persons/2; m < Next.Count; m++)
						Next[m].Mutate(SizeSeries, Sources);
				}
				Next.Sort(new OptimResultSorter());
				while (Next.Count > Persons)
					Next.Remove(Next.Last());

				return Next;
			}

			public OptimResult BestResult
			{
				get
				{
					this.Sort(new OptimResultSorter());
					return this[0];
				}
			}
			public static OptimResult[] Child(OptimResult R1, OptimResult R2, OptimSource[] Sources)
			{
				int SeriesLenght = Math.Max(R1.OptimSeries.Max(s => s.SizeSeries), R2.OptimSeries.Max(s => s.SizeSeries));
				#region BestSeries
				List<OptimSeries> AllSeries = new List<OptimSeries>();
				AllSeries.AddRange(R1.OptimSeries);
				AllSeries.AddRange(R2.OptimSeries);
				List<OptimSeries> BestSeries = new List<OptimSeries>();
				while (AllSeries.Any(s => s.SourceGuid != Guid.Empty))
				{
					BestSeries.Add(AllSeries.Where(s => s.SourceGuid != Guid.Empty).OrderBy(s => s.SizeRest).First());
					foreach (OptimSeries del in AllSeries.Where(a => BestSeries.Any(b => b.SourceGuid == a.SourceGuid)).ToArray())
						AllSeries.Remove(del);
					foreach (OptimSeries del in AllSeries.Where(a => a.OptimItem.Any(ia => BestSeries.Any(b => b.OptimItem.Any(ib => ib.Guid == ia.Guid)))).ToArray())
						AllSeries.Remove(del);
				}
				while (AllSeries.Count > 0)
				{
					BestSeries.Add(AllSeries.OrderBy(s => s.SizeRest).First());
					foreach (OptimSeries del in AllSeries.Where(a => a.OptimItem.Any(ia => BestSeries.Any(b => b.OptimItem.Any(ib => ib.Guid == ia.Guid)))).ToArray())
						AllSeries.Remove(del);
				}
				if (BestSeries.Any(b => b.SourceGuid != Guid.Empty))
					BestSeries.RemoveAt(new Random().Next(BestSeries.Count(b => b.SourceGuid != Guid.Empty)));

				int rnd = new Random().Next(BestSeries.Count);
				BestSeries.RemoveRange(rnd, BestSeries.Count - rnd);

				List<Item> ItemList = new List<Item>();
				ItemList.AddRange(R1.OptimItem.Where(i => !BestSeries.Any(b => b.OptimItem.Any(ib => ib.Guid == i.Guid)) && !ItemList.Any(il => il.Guid == i.Guid)));
				ItemList.AddRange(R2.OptimItem.Where(i => !BestSeries.Any(b => b.OptimItem.Any(ib => ib.Guid == i.Guid)) && !ItemList.Any(il => il.Guid == i.Guid)));
				foreach (OptimSource os in Sources.Where(s => !BestSeries.Any(b => b.SourceGuid == s.Guid)).OrderByDescending(s => s.Size))
				{
					List<Item> tList = new List<Item>();
					foreach (Item it in ItemList.Where(i => i.Size <= os.Size).OrderByDescending(i => i.Size).ToArray())
						if (tList.Sum(i => i.Size) + it.Size <= os.Size)
						{
							tList.Add(it);
							ItemList.Remove(it);
						}
						else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > os.Size))
							break;
					BestSeries.Add(new OptimSeries(tList, os.Size, os.Guid));
				}
				while (ItemList.Count > 0)
				{
					List<Item> tList = new List<Item>();
					foreach (Item it in ItemList.OrderByDescending(i => i.Size).ToArray())
						if (tList.Sum(i => i.Size) + it.Size <= SeriesLenght)
						{
							tList.Add(it);
							ItemList.Remove(it);
						}
						else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > SeriesLenght))
							break;
					BestSeries.Add(new OptimSeries(tList, SeriesLenght));
				}
				#endregion
				#region Two Merged
				List<OptimSeries> Series1 = new List<OptimSeries>();
				List<OptimSeries> Series2 = new List<OptimSeries>();
				OptimSeries[] R1s = R1.OptimSeries.Where(s => s.SourceGuid != Guid.Empty).ToArray();
				OptimSeries[] R2s = R2.OptimSeries.Where(s => s.SourceGuid != Guid.Empty).ToArray();
				if (R1s.Length > 0 && R2s.Length > 0)
				{
					rnd = new Random().Next(Math.Min(R1s.Length, R2s.Length));
					for (int i = 0; i <= rnd; i++)
					{
						Series1.Add(R1s[i]);
						Series2.Add(R2s[i]);
					}
                }
				else if (R1s.Length > 0)
				{
					Series1.AddRange(R1s);
					Series2.AddRange(R1s);
				}
				else if (R2s.Length > 0)
				{
					Series1.AddRange(R2s);
					Series2.AddRange(R2s);
				}

				//List<Item> ItemList = new List<Item>();
				//ItemList.AddRange(R1.OptimItem.Where(i => !BestSeries.Any(b => b.OptimItem.Any(ib => ib.Guid == i.Guid)) && !ItemList.Any(il => il.Guid == i.Guid)));
				//ItemList.AddRange(R2.OptimItem.Where(i => !BestSeries.Any(b => b.OptimItem.Any(ib => ib.Guid == i.Guid)) && !ItemList.Any(il => il.Guid == i.Guid)));
				//foreach (OptimSource os in Sources.Where(s => !BestSeries.Any(b => b.SourceGuid == s.Guid)).OrderByDescending(s => s.Size))
				//{
				//	List<Item> tList = new List<Item>();
				//	foreach (Item it in ItemList.Where(i => i.Size <= os.Size).OrderByDescending(i => i.Size).ToArray())
				//		if (tList.Sum(i => i.Size) + it.Size <= os.Size)
				//		{
				//			tList.Add(it);
				//			ItemList.Remove(it);
				//		}
				//		else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > os.Size))
				//			break;
				//	BestSeries.Add(new OptimSeries(tList, os.Size, os.Guid));
				//}
				//while (ItemList.Count > 0)
				//{
				//	List<Item> tList = new List<Item>();
				//	foreach (Item it in ItemList.OrderByDescending(i => i.Size).ToArray())
				//		if (tList.Sum(i => i.Size) + it.Size <= SeriesLenght)
				//		{
				//			tList.Add(it);
				//			ItemList.Remove(it);
				//		}
				//		else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > SeriesLenght))
				//			break;
				//	BestSeries.Add(new OptimSeries(tList, SeriesLenght));
				//}
				#endregion
				return new OptimResult[] { new OptimResult(BestSeries) };
			}

			public bool HasLike(OptimResult R)
			{
				return this.Any(r => r.Count == r.Count && r.BestRest == R.BestRest && r.SourcesSizeWork == R.SourcesSizeWork);
            }
			public override string ToString()
			{
				return "G:" + this.Generation.ToString() + " " + this.BestResult.ToString();
			}
		}
		class OptimResultSorter : IComparer<OptimResult>
		{
			public int Compare(OptimResult x, OptimResult y)
			{
				if (x.Count == y.Count)
				{
					if (x.SourcesSizeWork == y.SourcesSizeWork)
						return Comparer<int>.Default.Compare(y.BestRest, x.BestRest);
					else
						return Comparer<int>.Default.Compare(y.SourcesSizeWork, x.SourcesSizeWork);
				}
				else
					return Comparer<int>.Default.Compare(x.Count, y.Count);
			}
		}
	}
	public class OptimSource
	{
		public Guid Guid
		{ get; private set; }
		public int Size
		{ get; set; }
		public OptimSource(int Size)
		{
			this.Guid = Guid.NewGuid();
			this.Size = Size;
		}
		public OptimSource(int Size, Guid Guid)
		{
			this.Guid = Guid;
			this.Size = Size;
		}
	}
	public class OptimSeries
	{
		[DisplayName("Название")]
		public string ToShow
		{
			get
			{
				string str = "";
				foreach (Item i in this.OptimItem)
					str += i.Size.ToString() + " | ";
				str += "[" + SizeRest.ToString() + "]";
				return str;
			}
		}
		public override string ToString()
		{
			return ToShow;
		}
		public Item[] OptimItem
		{ get; private set; }
		[DisplayName("Длина")]
		public int SizeSeries
		{ get; private set; }
		[Browsable(false)]
		public Guid SourceGuid
		{ get; private set; }
		[Browsable(false)]
		public Guid RowGuid
		{ get; private set; }
		public OptimSeries(IEnumerable<Item> OptimItems, int SizeSeries)
		{
			this.OptimItem = OptimItems.OrderByDescending(i => i.TaskSort).ThenByDescending(i => i.Size).ToArray();
			this.SizeSeries = SizeSeries;
			this.SourceGuid = Guid.Empty;
			this.RowGuid = Guid.NewGuid();
        }
		public OptimSeries(List<Item> OptimItems, int SizeSeries, Guid SourceGuid) : this(OptimItems, SizeSeries)
		{
			this.SourceGuid = SourceGuid;
        }
		[DisplayName("ПИРАМИДА")]
		public int PyramidNum
		{ get; set; }
		[DisplayName("РЯД")]
		public int RowNum
		{ get; set; }
		public void AddItems(Item[] Items)
		{
			List<Item> OptimItems = OptimItem.ToList();
			OptimItems.AddRange(Items);
            this.OptimItem = OptimItems.OrderByDescending(i => i.Size).ToArray();
		}
		[DisplayName("Использовано")]
		public int SizeWork
		{ get { return OptimItem.Sum(i => i.Size); } }
		[DisplayName("Осталось")]
		public int SizeRest
		{ get { return SizeSeries - SizeWork; } }
		[DisplayName("Разгрузка Min")]
		public int TaskSort
		{ get { return OptimItem.Min(i => i.TaskSort); } }
    }
	public class OptimResult
	{
		public OptimSeries[] OptimSeries
		{ get; private set; }
		public OptimResult(IEnumerable<OptimSeries> OptimSerieses)
		{
			this.OptimSeries = OptimSerieses.OrderBy(s => (s.SourceGuid != Guid.Empty) ? (-1 * s.SizeWork) : s.SizeRest).ToArray();
		}
		public Item[] OptimItem
		{
			get
			{
				List<Item> ItemList = new List<Item>();
				foreach (OptimSeries s in OptimSeries)
					ItemList.AddRange(s.OptimItem);
				return ItemList.ToArray();
			}
		}
		public void Mutate(int SizeSeries, OptimSource[] Sources)
		{
			List<OptimSeries> NewResult = this.OptimSeries.ToList();
			List<Item> ItemList = new List<Item>();
            if (NewResult.Any(b => b.SourceGuid != Guid.Empty))
			{
				OptimSeries S0 = NewResult[new Random().Next(NewResult.Count(b => b.SourceGuid != Guid.Empty))];
				NewResult.Remove(S0);
				ItemList.AddRange(S0.OptimItem);
            }
			int c = NewResult.Count(b => b.SourceGuid != Guid.Empty);
            Random RND = new Random();
			if (c < NewResult.Count)
			{
				OptimSeries S1 = NewResult[RND.Next(c, NewResult.Count)];
				NewResult.Remove(S1);
				ItemList.AddRange(S1.OptimItem);
			}
			if (c < NewResult.Count)
			{
				OptimSeries S2 = NewResult[RND.Next(c, NewResult.Count)];
				NewResult.Remove(S2);
				ItemList.AddRange(S2.OptimItem);
			}
			foreach (OptimSource os in Sources.Where(s => !NewResult.Any(b => b.SourceGuid == s.Guid)).OrderByDescending(s => s.Size))
			{
				List<Item> tList = new List<Item>();
				foreach (Item it in ItemList.Where(i => i.Size <= os.Size).OrderByDescending(i => i.Size).ToArray())
					if (tList.Sum(i => i.Size) + it.Size <= os.Size)
					{
						tList.Add(it);
						ItemList.Remove(it);
					}
					else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > os.Size))
						break;
				NewResult.Add(new OptimSeries(tList, os.Size, os.Guid));
			}
			while (ItemList.Count > 0)
			{
				List<Item> tList = new List<Item>();
				foreach (Item it in ItemList.OrderByDescending(i => i.Size).ToArray())
					if (tList.Sum(i => i.Size) + it.Size <= SizeSeries)
					{
						tList.Add(it);
						ItemList.Remove(it);
					}
					else if (ItemList.All(il => tList.Sum(i => i.Size) + il.Size > SizeSeries))
						break;
				NewResult.Add(new OptimSeries(tList, SizeSeries));
			}
			this.OptimSeries = NewResult.OrderBy(s => (s.SourceGuid != Guid.Empty) ? (-1 * s.SizeWork) : s.SizeRest).ToArray();
		}
		public int BestRest
		{
			get
			{
				IEnumerable<OptimSeries> Series = OptimSeries.Where(s => s.SourceGuid == Guid.Empty);
				if (Series.Count() > 0)
					return Series.Max(s => s.SizeRest);
				else
					return 0;
			}
		}
		public int Count
		{
			get
			{
				return OptimSeries.Where(s => s.SourceGuid == Guid.Empty).Count();
			}
		}
		public int SourcesSizeWork
		{
			get
			{
				IEnumerable<OptimSeries> Series = OptimSeries.Where(s => s.SourceGuid != Guid.Empty);
				if (Series.Count() > 0)
					return Series.Sum(s => s.SizeWork);
				else
					return 0;
			}
		}
		public override string ToString()
		{
			return "S:" + this.OptimSeries.Where(s => s.SourceGuid != Guid.Empty).Count().ToString() + "(" + this.SourcesSizeWork + ") L:" + this.Count.ToString() + " O:" + BestRest.ToString();
        }
	}
}
namespace CarPyramid
{
	public class Optimizator
	{
		public static LineOpt.OptimResult GetRows(IEnumerable<Item> Items, int Lenght, List<LineOpt.OptimSource> Sources)
		{
			return new LineOpt.OptimLine(Items, Lenght, Sources).Optimize();
        }
	}
}
