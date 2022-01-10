using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using CarPyramid.LineOpt;

namespace CarPyramid.CarOpt
{
	public class OptimCar
	{
		public const int NoWorkRest = 300;
		public Item[] Items
		{ get; private set; }
		public Car Car
		{ get; private set; }
		public OptimCar(List<Item> Items, Car Car)
		{
			this.Items = Items.Where(i => 0 < i.Size && i.Size <= Car.RowLenght).OrderByDescending(i => i.TaskSort).ToArray();
			this.Car = Car;
		}
		public Car NewCar(List<OptimSeries> SeriesList)
		{
			if (Car is CustomCar)
				return new CustomCar(Car.CarType, Car.RowLenght, Car.RowCount, Car.PyramidCount, SeriesList);
			else
				return (Car)Car.GetType().GetConstructor(new Type[] { typeof(List<OptimSeries>) }).Invoke(new object[] { SeriesList });
		}
		public Car Optimize()
		{
			if (Car.CarType == CarType.SideLoad)
				return OptimizeSideLoad();
			else if (Car.CarType == CarType.BackLoad)
				return OptimizeBackLoad();
			else
				return null;
		}
		public Car Optimize(Car Car)
		{
			this.Car = Car;
			return Optimize();
		}
		Car OptimizeSideLoad()
		{
			if (Car == null)
				return null;
			Population Population = new Population(8, 0);
			#region Person 1
			List<OptimSeries> OneToOne = new List<OptimSeries>();
			foreach (IGrouping<int, Item> TaskSort in Items.GroupBy(i => i.TaskSort).OrderByDescending(ts => ts.Key))
				OneToOne.AddRange(Optimizator.GetRows(TaskSort, Car.RowLenght, new List<OptimSource>()).OptimSeries.ToList());
			Population.Add(NewCar(OneToOne));
			#endregion
			#region Person 2
			List<OptimSeries> FillOst = new List<OptimSeries>();
			List<OptimSource> Sources = new List<OptimSource>();
			foreach (IGrouping<int, Item> TaskSort in Items.GroupBy(i => i.TaskSort).OrderByDescending(ts => ts.Key))
			{
				List<OptimSeries> Rows = Optimizator.GetRows(TaskSort, Car.RowLenght, Sources).OptimSeries.ToList();
				foreach (OptimSeries so in Rows.Where(r => r.SourceGuid != Guid.Empty && r.OptimItem.Length > 0).ToArray())
				{
					OptimSeries FO = FillOst.FirstOrDefault(fo => fo.RowGuid == so.SourceGuid);
					if (FO != null)
					{
						FO.AddItems(so.OptimItem);
						Rows.Remove(so);
					}
				}
				FillOst.AddRange(Rows.Where(r => r.OptimItem.Length > 0));
				Sources.Clear();
				OptimSource LastS = null;
				foreach (OptimSeries s in FillOst.OrderBy(fo => fo.TaskSort).ThenByDescending(fo => fo.SizeRest))
				{
					LastS = new OptimSource(Math.Min((LastS == null) ? int.MaxValue : LastS.Size, s.SizeRest), s.RowGuid);
					if (LastS.Size <= NoWorkRest)
						break;
					else
						Sources.Add(LastS);
				}
			}
			Population.Add(NewCar(FillOst));
			#endregion
			#region Person 3
			List<OptimSeries> FillCircle = new List<OptimSeries>();
			IGrouping<int, Item>[] TaskSorts = Items.GroupBy(i => i.TaskSort).OrderByDescending(ts => ts.Key).ToArray();
			for (int p = 0; p < Car.PyramidCount; p++)
			{
				List<OptimSeries> FillPyr = new List<OptimSeries>();
				List<OptimSource> pyrSources = new List<OptimSource>();
				for (int t = p; t < TaskSorts.Count(); t += Car.PyramidCount)
				{
					IGrouping<int, Item> TaskSort = TaskSorts[t];
					List<OptimSeries> Rows = Optimizator.GetRows(TaskSort, Car.RowLenght, pyrSources).OptimSeries.ToList();
					foreach (OptimSeries so in Rows.Where(r => r.SourceGuid != Guid.Empty).ToArray())
					{
						OptimSeries FO = FillPyr.FirstOrDefault(fo => fo.RowGuid == so.SourceGuid);
						if (FO != null)
						{
							FO.AddItems(so.OptimItem);
							Rows.Remove(so);
						}
					}
					FillPyr.AddRange(Rows.Where(r => r.OptimItem.Length > 0));
					pyrSources.Clear();
					OptimSource LastS = null;
					foreach (OptimSeries s in FillPyr.OrderBy(fo => fo.TaskSort).ThenByDescending(fo => fo.SizeRest))
					{
						LastS = new OptimSource(Math.Min((LastS == null) ? int.MaxValue : LastS.Size, s.SizeRest), s.RowGuid);
						if (LastS.Size <= NoWorkRest)
							break;
						else
							pyrSources.Add(LastS);
					}
				}
				FillCircle.AddRange(FillPyr);
			}
			Population.Add(NewCar(FillCircle));
			#endregion
			int ReplayBest = 0;
			while (Population.Generation <= Population.Persons * Population.Persons && ReplayBest <= 4)
			{
				Car PrevCar = Population.BestCar;
				Population NexPopulation = Population.Next(this);
				Car NextCar = NexPopulation.BestCar;
				//if (PrevResult.SourcesSizeWork > NextResult.SourcesSizeWork || PrevResult.Length < NextResult.Length || PrevResult.Length == NextResult.Length && PrevResult.BestRest > NextResult.BestRest)
				//	break;
				//else
				if (PrevCar.PyramidCountFact == NextCar.PyramidCountFact && PrevCar.TotalRowCountFact == NextCar.TotalRowCountFact && PrevCar.TotalSizeRest == NextCar.TotalSizeRest)
					ReplayBest += 1;
				else if (PrevCar.PyramidCountFact >= NextCar.PyramidCountFact && PrevCar.TotalRowCountFact >= NextCar.TotalRowCountFact && PrevCar.TotalSizeRest <= NextCar.TotalSizeRest)
					ReplayBest = 0;
				Population = NexPopulation;

				GC.Collect();
				GC.Collect();
			}
			Generation = Population.Generation;
			return Population.BestCar;
		}
		public int Generation = 0;
		class Population : List<Car>
		{
			public int Persons
			{ get; private set; }
			public int Generation
			{ get; private set; }
			public Population(int Persons, int Generation)
			{
				this.Persons = Persons;
				this.Generation = Generation;
			}
			public Population Next(OptimCar CarOpt)
			{
				Population Next = new Population(this.Persons, this.Generation + 1);
				Next.AddRange(this);

				foreach (Car C1 in this.ToArray())
					foreach (Car C2 in this.ToArray())
						foreach (Car Ch in Child(C1, C2, CarOpt))
							if (!Next.HasLike(Ch))
								Next.Add(Ch);
				Next.Sort(new CarSorter());
				if (Next.Count >= Persons)
				{
					//Next[new Random().Next(1, Persons)].Mutate(Sources);
					for (int m = Persons / 2; m < Next.Count; m++)
						Next[m].Mutate();
				}
				Next.Sort(new CarSorter());
				while (Next.Count > Persons)
					Next.Remove(Next.Last());

				return Next;
			}
			public Car BestCar
			{
				get
				{
					this.Sort(new CarSorter());
					return this[0];
				}
			}
			public static Car[] Child(Car C1, Car C2, OptimCar CarOpt)
			{
				if (C1.GetType() != C2.GetType())
					return null;
				List<IGrouping<int, OptimSeries>> PossiblePyramids = new List<IGrouping<int, OptimSeries>>();
				PossiblePyramids.AddRange(C1.OptimSeries.GroupBy(s => s.PyramidNum).Where(p => p.Max(s => s.OptimItem.Max(i => i.TaskSort)) == p.Min(s => s.OptimItem.Min(i => i.TaskSort)) || !C1.OptimSeries.Any(os => os.PyramidNum < p.Key && os.TaskSort == p.Max(s => s.OptimItem.Max(i => i.TaskSort)))).ToList());
				PossiblePyramids.AddRange(C2.OptimSeries.GroupBy(s => s.PyramidNum).Where(p => p.Max(s => s.OptimItem.Max(i => i.TaskSort)) == p.Min(s => s.OptimItem.Min(i => i.TaskSort)) || !C2.OptimSeries.Any(os => os.PyramidNum < p.Key && os.TaskSort == p.Max(s => s.OptimItem.Max(i => i.TaskSort)))).ToList());
				List<IGrouping<int, OptimSeries>> BestPyramid = new List<IGrouping<int, OptimSeries>>();
				while (PossiblePyramids.Count > 0)
				{
					BestPyramid.Add(PossiblePyramids.OrderBy(s => GetSizeRest(s, C1)).First());
					foreach (IGrouping<int, OptimSeries> del in PossiblePyramids.Where(a => a.Any(sa => sa.OptimItem.Any(ia => BestPyramid.Any(b => b.Any(sb => sb.OptimItem.Any(ib => ib.Guid == ia.Guid)))))).ToArray())
						PossiblePyramids.Remove(del);
				}

				int rnd = new Random().Next(BestPyramid.Count);
				BestPyramid.RemoveRange(rnd, BestPyramid.Count - rnd);

				List<Item> ItemList = new List<Item>();
				ItemList.AddRange(C1.OptimItem.Where(i => !BestPyramid.Any(b => b.Any(sb => sb.OptimItem.Any(ib => ib.Guid == i.Guid))) && !ItemList.Any(il => il.Guid == i.Guid)));
				ItemList.AddRange(C2.OptimItem.Where(i => !BestPyramid.Any(b => b.Any(sb => sb.OptimItem.Any(ib => ib.Guid == i.Guid))) && !ItemList.Any(il => il.Guid == i.Guid)));

				List<OptimSeries> NewCar = new List<OptimSeries>();
				foreach (IGrouping<int, OptimSeries> bp in BestPyramid.OrderBy(p => p.Min(s => s.OptimItem.Min(i => i.TaskSort))))
				{
					foreach (OptimSeries os in bp)
						NewCar.Add(new OptimSeries(os.OptimItem, os.SizeSeries));

					List<OptimSource> bpSources = new List<OptimSource>();
					OptimSource LastS = null;
					foreach (OptimSeries s in bp.OrderBy(bps => bps.TaskSort).ThenByDescending(bps => bps.SizeRest))
					{
						LastS = new OptimSource(Math.Min((LastS == null) ? int.MaxValue : LastS.Size, s.SizeRest), s.RowGuid);
						if (LastS.Size <= NoWorkRest)
							break;
						else
							bpSources.Add(LastS);
					}
					int minTS = bp.Min(s => s.OptimItem.Min(i => i.TaskSort));
					foreach (IGrouping<int, Item> TaskSort in ItemList.Where(i => i.TaskSort <= minTS).GroupBy(i => i.TaskSort).OrderByDescending(ts => ts.Key))
					{
						List<OptimSeries> Rows = Optimizator.GetRows(TaskSort, C1.RowLenght, bpSources).OptimSeries.ToList();
						foreach (OptimSeries so in Rows.Where(r => r.SourceGuid != Guid.Empty).ToArray())
						{
							OptimSeries FO = NewCar.FirstOrDefault(fo => fo.RowGuid == so.SourceGuid);
							if (FO != null)
							{
								FO.AddItems(so.OptimItem);
								Rows.Remove(so);
							}
						}
						NewCar.AddRange(Rows.Where(r => r.OptimItem.Length > 0));
						bpSources.Clear();
						LastS = null;
						foreach (OptimSeries s in NewCar.OrderBy(fo => fo.TaskSort).ThenByDescending(fo => fo.SizeRest))
						{
							LastS = new OptimSource(Math.Min((LastS == null) ? int.MaxValue : LastS.Size, s.SizeRest), s.RowGuid);
							if (LastS.Size <= NoWorkRest)
								break;
							else
								bpSources.Add(LastS);
						}
					}
				}
				ItemList.RemoveAll(i => NewCar.Any(s => s.OptimItem.Any(si => si.Guid == i.Guid)));
				List<OptimSource> Sources = new List<OptimSource>();
				foreach (IGrouping<int, Item> TaskSort in ItemList.GroupBy(i => i.TaskSort).OrderByDescending(ts => ts.Key))
				{
					List<OptimSeries> Rows = Optimizator.GetRows(TaskSort, C1.RowLenght, Sources).OptimSeries.ToList();
					foreach (OptimSeries so in Rows.Where(r => r.SourceGuid != Guid.Empty).ToArray())
					{
						OptimSeries FO = NewCar.FirstOrDefault(fo => fo.RowGuid == so.SourceGuid);
						if (FO != null)
						{
							FO.AddItems(so.OptimItem);
							Rows.Remove(so);
						}
					}
					NewCar.AddRange(Rows.Where(r => r.OptimItem.Length > 0));
					Sources.Clear();
					OptimSource LastS = null;
					foreach (OptimSeries s in NewCar.OrderBy(fo => fo.TaskSort).ThenByDescending(fo => fo.SizeRest))
					{
						LastS = new OptimSource(Math.Min((LastS == null) ? int.MaxValue : LastS.Size, s.SizeRest), s.RowGuid);
						if (LastS.Size <= NoWorkRest)
							break;
						else
							Sources.Add(LastS);
					}
				}
				if (CarOpt != null && CarOpt.Car != null)
					return new Car[] { CarOpt.NewCar(NewCar) };
				else
					return new Car[] { (Car)C1.GetType().GetConstructor(new Type[] { typeof(List<OptimSeries>) }).Invoke(new object[] { NewCar }) };
			}

			public bool HasLike(Car C)
			{
				if (C == null)
					return true;
				return this.Any(c => c.PyramidCountFact == C.PyramidCountFact && c.TotalRowCountFact == C.TotalRowCountFact && c.TotalSizeRest == C.TotalSizeRest);
			}
			public override string ToString()
			{
				return "G:" + this.Generation.ToString() + " " + this.BestCar.ToString();
			}
		}
		class CarSorter : IComparer<Car>
		{
			public int Compare(Car x, Car y)
			{
				if (x.PyramidCountFact == y.PyramidCountFact)
				{
					if (x.TotalRowCountFact == y.TotalRowCountFact)
						return Comparer<int>.Default.Compare(y.TotalSizeRest, x.TotalSizeRest);
					else
						return Comparer<int>.Default.Compare(x.TotalRowCountFact, y.TotalRowCountFact);
				}
				else
					return Comparer<int>.Default.Compare(x.PyramidCountFact, y.PyramidCountFact);
			}
		}
		static int GetSizeRest(IGrouping<int, OptimSeries> Pyramid, Car Car)
		{
			int Result = Math.Max(Car.PyramidCount - Pyramid.Count(), 0) * Car.RowLenght;
			int LastRest = int.MaxValue;
			foreach (OptimSeries s in Pyramid.OrderByDescending(os => os.RowNum))
			{
				LastRest = Math.Min(LastRest, s.SizeRest);
				if (LastRest <= OptimCar.NoWorkRest)
					continue;
				else
					Result += LastRest;
			}
			return Result;
		}

		Car OptimizeBackLoad()
		{
			if (Car == null)
				return null;
			Car ResultCar = NewCar(Optimizator.GetRows(this.Items, Car.RowLenght, new List<OptimSource>()).OptimSeries.ToList());
			OptimSeries[] Series = ResultCar.OptimSeries.OrderByDescending(s => s.OptimItem.Max(i => i.SizeAdd)).ToArray();
			int Pyr = 1;
			for (int r = 0; r < Series.Count(); r++)
			{
				Series[r].RowNum = GetRowNum(r - (Pyr - 1) * Car.RowCount, Car.RowCount);
				if (Series[r].RowNum <= 0 || Car.RowCount < Series[r].RowNum)
				{
					Pyr += 1;
					Series[r].RowNum = GetRowNum(r - (Pyr - 1) * Car.RowCount, Car.RowCount);
				}
				Series[r].PyramidNum = Pyr;
				int Cell = 1;
				foreach (Item I in Series[r].OptimItem.OrderByDescending(i => i.TaskSort))
					I.CellNum = Cell++;
			}

			GC.Collect();
			GC.Collect();
			Generation = 0;
			return ResultCar;
        }
		byte GetRowNum(int r, int RowCount)
		{
			//return (byte)((0 < Math.Pow(-1, Math.Ceiling(r/2.0)) ? 1 : RowCount)+ Math.Pow(-1, Math.Ceiling(r / 2.0))* (r - (1 - Math.Pow(-1, r)) / 2) / 2); // От краев, 1-9-8-2-3-7-6-4-5
			//return (byte)((0 < Math.Pow(-1, r) ? 1 : RowCount) + Math.Pow(-1, r) * (r - (1 - Math.Pow(-1, r)) / 2) / 2); // От краев, 1-9-2-8-3-7-4-6-5
			return (byte)(Math.Ceiling(RowCount / 2.0) - Math.Pow(-1, r) * (r + (1 - Math.Pow(-1, r)) / 2) / 2); // 5-6-4-7-3-8-2-9-1																											
		}
	}
	public enum CarType
	{
		[Description("БоковаяЗагрузка")]
		SideLoad = 1,
		[Description("ТыловаяЗагрузка")]
		BackLoad = 2
	}
    public abstract class Car
	{
		public virtual string NameCar
		{ get { return this.GetType().Name; } }
		public abstract CarType CarType
		{ get; }
		public abstract int RowLenght
		{ get; }
		public abstract int RowCount
		{ get; }
		public abstract int PyramidCount
		{ get; }
		public OptimSeries[] OptimSeries
		{ get; private set; }
		public Car()
		{ this.OptimSeries = new OptimSeries[] { }; }
		public Car(List<OptimSeries> OptimSeries) : this()
		{
			this.OptimSeries = OptimSeries.ToArray();
			if (PyramidCount > 0 && RowCount > 0 && RowLenght > 0)
				this.SetPyrRowCell();
        }
		public void SetPyrRowCell()
		{
			if (PyramidCount <= 0 || RowCount <= 0 || RowLenght <= 0)
				return;
			int Pyr = 1;
			int Row = RowCount;
			int TaskSort = int.MaxValue;
			foreach (OptimSeries OS in this.OptimSeries)
			{
				if (Row <= 0 || TaskSort < OS.TaskSort)
				{
					Pyr += 1;
					Row = RowCount;
				}
				OS.PyramidNum = Pyr;
				OS.RowNum = Row;
				int Cell = 1;
				foreach (Item I in OS.OptimItem.OrderByDescending(i => i.TaskSort))
					I.CellNum = Cell++;
				Row -= 1;
				TaskSort = OS.TaskSort;
			}
		}
		public int TotalRowCount
		{ get { return PyramidCount * RowCount; } }
		public int TotalRowCountFact
		{ get { return OptimSeries.Length; } }
		public int PyramidCountFact
		{ get { return OptimSeries.GroupBy(s => s.PyramidNum).Count(); } }
		public int TotalSizeRest
		{
			get
			{
				int Result = Math.Max(TotalRowCount - TotalRowCountFact, 0) * RowLenght;
				int LastPyr = 0;
				int LastRest = int.MaxValue;
				foreach (OptimSeries s in OptimSeries.OrderBy(os => os.PyramidNum).ThenByDescending(os => os.RowNum))
				{
					LastRest = Math.Min((LastPyr != s.PyramidNum) ? int.MaxValue : LastRest, s.SizeRest);
					if (LastRest <= OptimCar.NoWorkRest)
						continue;
					else
						Result += LastRest;
				}
				return Result;
			}
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
		public void Mutate()
		{

		}
		public override string ToString()
		{
			return NameCar + ": P:" + PyramidCountFact.ToString() + " R:" + TotalRowCountFact.ToString();
		}
	}

	#region Car Classes
	public class CustomCar : Car
	{
        public override CarType CarType
		{ get { return CT; } }
		private CarType CT;
		public override int RowLenght
		{ get { return RL; } }
		private int RL;
        public override int RowCount
		{ get { return RC; } }
		private int RC;
        public override int PyramidCount
		{ get { return PC; } }
		private int PC;
		public CustomCar(CarType CT, int RL, int RC, int PC, List<OptimSeries> Items) : base(Items)
		{
			this.CT = CT;
			this.RL = RL;
			this.RC = RC;
			this.PC = PC;
			this.SetPyrRowCell();
		}
	}
	public class GazelleShort : Car
	{
		public override CarType CarType
		{ get { return CarType.BackLoad; } }
		public override int RowLenght
		{ get { return 2990; } }
		public override int RowCount
		{ get { return 11; } }
		public override int PyramidCount
		{ get { return 1; } }
		public GazelleShort() : base()
		{ }
        public GazelleShort(List<OptimSeries> Items) : base(Items)
		{ }
	}
	public class GazelleLong : Car
	{
		public override CarType CarType
		{ get { return CarType.SideLoad; } }
		public override int RowLenght
		{ get { return 4100; } }
		public override int RowCount
		{ get { return 5; } }
		public override int PyramidCount
		{ get { return 2; } }
		public GazelleLong() : base()
		{ }
		public GazelleLong(List<OptimSeries> Items) : base(Items)
		{ }
	}
	public class Isuzu : Car
	{
		public override CarType CarType
		{ get { return CarType.SideLoad; } }
		public override int RowLenght
		{ get { return 3060; } }
		public override int RowCount
		{ get { return 8; } }
		public override int PyramidCount
		{ get { return 4; } }
		public Isuzu() : base()
		{ }
		public Isuzu(List<OptimSeries> Items) : base(Items)
		{ }
	}
	public class LongVehicle : Car
	{
		public override CarType CarType
		{ get { return CarType.SideLoad; } }
		public override int RowLenght
		{ get { return 3060; } }
		public override int RowCount
		{ get { return 8; } }
		public override int PyramidCount
		{ get { return 8; } }
		public LongVehicle() : base()
		{ }
		public LongVehicle(List<OptimSeries> Items) : base(Items)
		{ }
	}
	#endregion
}
