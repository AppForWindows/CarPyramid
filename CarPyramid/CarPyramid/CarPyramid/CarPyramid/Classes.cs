using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using CarPyramid.CarOpt;
using CarPyramid.LineOpt;

namespace Classes
{
	public class XML
	{
		public const string TASK_EXTENSION = ".cptsk";
		public const string RESULT_EXTENSION = ".cpres";
		public static string GetString(XmlElement el, string Attribute, string Default)
		{
			if (el.HasAttribute(Attribute))
				return el.Attributes[Attribute].Value;
			else
				return Default;
		}
		public static int GetInt(XmlElement el, string Attribute, int Default)
		{
			if (el.HasAttribute(Attribute) && int.TryParse(el.Attributes[Attribute].Value, out Default))
				return Default;
			else
				return Default;
		}
		public static Guid GetGuid(XmlElement el, string Attribute, Guid Default)
		{
			if (el.HasAttribute(Attribute) && Guid.TryParse(el.Attributes[Attribute].Value, out Default))
				return Default;
			else
				return Default;
		}
		public static List<Task> LoadTasksFile(string FilePath)
		{
			try
			{
				if (!System.IO.File.Exists(FilePath))
					return new List<Task>();
				XmlDocument File = new XmlDocument();
				File.Load(FilePath);

				return LoadTasksXML(File, FilePath);
			}
			catch (Exception l)
			{
				throw (new Exception("Загрузка Файла '" + FilePath + "'", l));
			}
		}
		public static List<Task> LoadTasksOuterXML(string OuterXML, string FilePath)
		{
			try
			{
				XmlDocument File = new XmlDocument();
				File.LoadXml(OuterXML);

				return LoadTasksXML(File, FilePath);
			}
			catch (Exception l)
			{
				throw (new Exception("Загрузка Файла '" + FilePath + "'", l));
			}
		}
		public static List<Task> LoadTasksXML(XmlDocument File, string FilePath)
		{
			List<Task> TaskList = new List<Task>();
			try
			{
				string CompanyName = XML.GetString(File.DocumentElement, "CompanyName", CarPyramid.Program.CompanyName);
				string ProductKey = XML.GetString(File.DocumentElement, "ProductKey", CarPyramid.Program.ProductKey);
				foreach (XmlElement t in File.GetElementsByTagName("Task"))
				{
					Task task = new Task(t, FilePath, CompanyName, ProductKey);
					if (task.Orders.Length > 0)
						TaskList.Add(task);
				}
				return TaskList;
			}
			catch (Exception l)
			{
				throw (new Exception("Загрузка Файла XML '" + FilePath + "'", l));
			}
		}
		public static bool SaveTasks(IEnumerable<Task> TaskList, string FilePath)
		{
			XmlDocument Doc = GetResultXML(TaskList, FilePath);
			FilePath = GetResultFile(FilePath);
            if (System.IO.File.Exists(FilePath))
				System.IO.File.Delete(FilePath);
			Doc.Save(FilePath);
			return true;
		}
		public static XmlDocument GetResultXML(IEnumerable<Task> TaskList, string FilePath)
		{
			XmlDocument Doc = new XmlDocument();
			XmlElement Root = Doc.CreateElement("SavedDataByCarPyramidApplication");
			Doc.AppendChild(Root);
			Root.SetAttribute("AppCompanyName", CarPyramid.Program.CompanyName);
			Root.SetAttribute("Created", DateTime.Now.ToString());
			Root.SetAttribute("FilePath", FilePath);
			Root.SetAttribute("MachineName", Environment.MachineName);
			foreach (Task T in TaskList)
				T.SaveToDoc(Doc, Root);
			return Doc;
		}
		public static string GetResultFile(string FilePath)
		{
			return System.IO.Path.ChangeExtension(FilePath, RESULT_EXTENSION);
		}
		public static string HardwareId
		{
			get
			{
				string HDD = "";
				foreach (System.Management.ManagementObject OS in new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get())
					foreach (System.Management.ManagementObject hdd in new System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive WHERE Index=" + System.Text.RegularExpressions.Regex.Matches(OS["Name"].ToString().Substring(OS["Name"].ToString().IndexOf("Harddisk")), @"[0-9]{1,}")[0].Value).Get())
						foreach (System.Management.ManagementObject pm in hdd.GetRelated("Win32_PhysicalMedia"))
							HDD = pm["SerialNumber"].ToString();
				System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
				using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
				{
					byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(HDD));
					for (int i = 0; i < data.Length; i++)
						sBuilder.Append(data[i].ToString("x2"));
				}
				return System.Text.RegularExpressions.Regex.Matches(sBuilder.ToString(), @".{1,8}").Cast<System.Text.RegularExpressions.Match>().Select(m => m.Value).Aggregate((prev, next) => prev + "-" + next).ToUpper();
			}
		}
		public static string GetProductKey(string CompanyName)
		{
			CompanyName = CompanyName.Trim();
			string HDD = HardwareId.Replace("-", "");
			string key = string.Empty;
			for (int s = 0; s < Math.Max(CompanyName.Length, HDD.Length); s++)
			{
				if (s < HDD.Length)
					key += HDD[s];
				if (s < CompanyName.Length)
					key += CompanyName[s];
			}
			System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
			using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
			{
				byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(key));
				for (int i = 0; i < data.Length; i++)
					sBuilder.Append(data[i].ToString("x2"));
			}
			return System.Text.RegularExpressions.Regex.Matches(sBuilder.ToString(), @".{1,8}").Cast<System.Text.RegularExpressions.Match>().Select(m => m.Value).Aggregate((prev, next) => prev + "-" + next).ToUpper();
		}
	}
	public class Item
	{
		[Browsable(false)]
		public Guid Guid
		{ get; private set; }
		[Browsable(false)]
		public Order Order
		{ get; private set; }
		[DisplayName("ШИРИНА")]
		public int Size
		{ get; private set; }
		[DisplayName("Доп. Размер")]
		public int SizeAdd
		{ get; private set; }
		[DisplayName("#")]
		public int IdItem
		{ get; private set; }
		[DisplayName("Экземпляр")]
		public int Instance
		{ get; private set; }
		[DisplayName("Название")]
		public string NameItem
		{ get; private set; }
		[DisplayName("User Data")]
		public string UserDataItem
		{ get; private set; }
		public Item(Order Order, int Size, int SizeAdd, int IdItem, int Instance, string NameItem, string UserDataItem)
		{
			this.Guid = Guid.NewGuid();
			this.Order = Order;
			this.Size = Size;
			this.SizeAdd = SizeAdd;
			this.IdItem = IdItem;
			this.Instance = Instance;
			this.NameItem = NameItem;
			this.UserDataItem = UserDataItem;
			this.CellNum = -1;
        }
		[DisplayName("В Ряде")]
		public int CellNum
		{ get; set; }
		[DisplayName("Заказ")]
		public string NameOrder
		{ get { return Order.NameOrder; } }
		[DisplayName("Разгрузка Заказа")]
		public int TaskSort
		{ get { return Order.TaskSort; } }
    }
	public struct Order
	{
		public List<Item> Items
		{ get; private set; }
		[DisplayName("Разгрузка")]
		public int TaskSort
		{ get; private set; }
		[DisplayName("#")]
		public int IdOrder
		{ get; private set; }
		[DisplayName("Заказ")]
		public string NameOrder
		{ get; private set; }
		[DisplayName("User Data")]
		public string UserDataOrder
		{ get; private set; }
		public Order(XmlElement Order)
		{
			this.TaskSort = XML.GetInt(Order, "TaskSort", 0);
			this.IdOrder = XML.GetInt(Order, "IdOrder", 0);
			this.NameOrder = XML.GetString(Order, "NameOrder", "");
			this.UserDataOrder = XML.GetString(Order, "UserDataOrder", "");

			this.Items = new List<Item>();
			foreach (XmlElement el in Order.GetElementsByTagName("Item"))
			{
				int Size = XML.GetInt(el, "Size", 0);
				int SizeAdd = XML.GetInt(el, "SizeAdd", 0);
				int IdItem = XML.GetInt(el, "IdItem", 0);
				string NameItem = XML.GetString(el, "NameItem", "");
				string UserDataItem = XML.GetString(el, "UserDataItem", "");
				int Qu = XML.GetInt(el, "Qu", 0);

				if (Size > 0 && Qu > 0)
					for (int Instance = 1; Instance <= Qu; Instance++)
						Items.Add(new Item(this, Size, SizeAdd, IdItem, Instance, NameItem, UserDataItem));
			}
		}
	}
	public struct Task
	{
		[DisplayName("Название")]
		public string NameTask
		{ get; private set; }
		public Order[] Orders
		{ get; private set; }
		[DisplayName("User Guid")]
		public Guid Guid
		{ get; private set; }
		[DisplayName("Файл")]
		public string FilePath
		{ get; private set; }
		[DisplayName("User Data")]
		public string UserDataTask
		{ get; private set; }
		[DisplayName("Создано")]
		public DateTime Created
		{ get; private set; }
		[DisplayName("Компания")]
		public string CompanyName
		{ get; private set; }
		[Browsable(false)]
		public string ProductKey
		{ get; private set; }
		[DisplayName("Машина")]
		public Car Car
		{ get; private set; }
		public Task(XmlElement Task, string FilePath, string CompanyName, string ProductKey)
		{
			this.FilePath = FilePath;
			this.Guid = XML.GetGuid(Task, "Guid", Guid.NewGuid());
			this.NameTask = XML.GetString(Task, "NameTask", "");
			this.UserDataTask = XML.GetString(Task, "UserDataTask", "");
			this.CompanyName = CompanyName;
			this.ProductKey = ProductKey;

			List<Order> OrderList = new List<Order>();
			foreach (XmlElement o in Task.GetElementsByTagName("Order"))
			{
				Order order = new Order(o);
				if (order.Items.Count > 0)
					OrderList.Add(order);
			}
			this.Orders = (XML.GetProductKey(this.CompanyName) == this.ProductKey) ? OrderList.OrderByDescending(o => o.TaskSort).ToArray() : new Order[] { };
			
			this.Car = null;
			foreach (XmlElement c in Task.GetElementsByTagName("Car"))
			{
				string NameCar = XML.GetString(c, "NameCar", "CustomCar");
				Type TypeCar = System.Reflection.Assembly.GetAssembly(typeof(Car)).GetTypes().FirstOrDefault(type => type.Name == NameCar && type.IsSubclassOf(typeof(Car)));
				if (TypeCar == null)
					TypeCar = typeof(CustomCar);
				if (TypeCar == typeof(CustomCar)) {
					CarType CT = CarType.SideLoad;
					try
					{ CT = (CarType)XML.GetInt(c, "CarType", 1); }
					catch { }
					int RL = XML.GetInt(c, "RowLenght", 0);
					int RC = XML.GetInt(c, "RowCount", 0);
					int PC = XML.GetInt(c, "PyramidCount", 0);
					if (0 < RL && 0 < RC && 0 < PC)
						this.Car = new CustomCar(CT, RL, RC, PC, new List<OptimSeries>());
				} else
					this.Car = (Car)TypeCar.GetConstructor(new Type[] { }).Invoke(new object[] { });
			}

			this.Created = DateTime.Now;
			this.Status = string.Empty;
		}
		public List<Item> Items
		{
			get
			{
				List<Item> ItemList = new List<Item>();
				foreach (Order o in this.Orders)
					ItemList.AddRange(o.Items);
				return ItemList;
            }
		}
        public Car Optimize()
		{
            this.Status = "BeginOptim...";
            List<Item> ItemList = this.Items;
			Car OptCar = (this.Car != null) ? this.Car : new GazelleShort();
			if (this.Items.Any(i => i.Size > OptCar.RowLenght))
				OptCar = new GazelleLong();
			this.Status = "Trying " + OptCar.GetType().Name + "...";
			OptCar = new OptimCar(ItemList, OptCar).Optimize();
			while (OptCar.PyramidCount < OptCar.PyramidCountFact || OptCar.OptimItem.Length != ItemList.Count)
			{
				if (OptCar is GazelleShort)
					OptCar = new GazelleLong();
				else if (OptCar is GazelleLong)
					OptCar = new Isuzu();
				else if (OptCar is Isuzu)
					OptCar = new LongVehicle();
				else
					break;
				this.Status = "Trying " + OptCar.GetType().Name + "...";
				OptCar = new OptimCar(ItemList, OptCar).Optimize();
			}
			this.Car = OptCar;
			this.Status = "Success " + OptCar.GetType().Name;
			return this.Car;
		}
		public void SaveToDoc(XmlDocument Doc, XmlElement Root)
		{
			this.Status = "Saving...";
			XmlElement TaskEl = Doc.CreateElement("Task");
			Root.AppendChild(TaskEl);
			TaskEl.SetAttribute("Guid", this.Guid.ToString());
			//TaskEl.SetAttribute("FilePath", this.FilePath);
			TaskEl.SetAttribute("NameTask", this.NameTask);
			TaskEl.SetAttribute("UserDataTask", this.UserDataTask);
			//TaskEl.SetAttribute("CompanyName", this.CompanyName);
			//TaskEl.SetAttribute("ProductKey", this.ProductKey);
			TaskEl.SetAttribute("Status", this.Status);
			if (this.Car != null && (XML.GetProductKey(this.CompanyName) == this.ProductKey))
			{
				XmlElement CarEl = Doc.CreateElement("Car");
				TaskEl.AppendChild(CarEl);
				CarEl.SetAttribute("NameCar", this.Car.NameCar);
				CarEl.SetAttribute("CarType", ((int)this.Car.CarType).ToString());
				CarEl.SetAttribute("RowLenght", this.Car.RowLenght.ToString());
				CarEl.SetAttribute("RowCount", this.Car.RowCount.ToString());
				CarEl.SetAttribute("PyramidCount", this.Car.PyramidCount.ToString());
				CarEl.SetAttribute("PyramidCountFact", this.Car.PyramidCountFact.ToString());
				foreach (OptimSeries OS in this.Car.OptimSeries.OrderBy(s => s.PyramidNum).ThenByDescending(s => s.RowNum))
					foreach (Item I in OS.OptimItem.OrderBy(i => i.CellNum))
					{
						XmlElement ItemEl = Doc.CreateElement("Item");
						CarEl.AppendChild(ItemEl);
						ItemEl.SetAttribute("PyramidNum", OS.PyramidNum.ToString());
						ItemEl.SetAttribute("RowNum", OS.RowNum.ToString());
						ItemEl.SetAttribute("CellNum", I.CellNum.ToString());
						ItemEl.SetAttribute("Size", I.Size.ToString());
						ItemEl.SetAttribute("SizeAdd", I.SizeAdd.ToString());
						ItemEl.SetAttribute("IdItem", I.IdItem.ToString());
						ItemEl.SetAttribute("Instance", I.Instance.ToString());
						ItemEl.SetAttribute("NameItem", I.NameItem);
						ItemEl.SetAttribute("UserDataItem", I.UserDataItem);
						ItemEl.SetAttribute("IdOrder", I.Order.IdOrder.ToString());
						ItemEl.SetAttribute("TaskSort", I.TaskSort.ToString());
						ItemEl.SetAttribute("NameOrder", I.NameOrder);
						ItemEl.SetAttribute("UserDataOrder", I.Order.UserDataOrder);
					}
			}
			this.Status = "Saved";
		}
		[DisplayName("Статус")]
		public string Status
		{ get; set; }
    }
}
