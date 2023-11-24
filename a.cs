public class Authorization
{
    public bool Check(string Login, string Password)
    {
        List<User> users = Json.Users();
        //List<User> users = TestJson.Users();

        return users.Exists(user => user.Login == Login & user.Password == Password);
    }

    public void Registration(User user)
    {
        Json json = new Json();
        //TestJson json = new TestJson();
        
        json.AddUser(user);
    }
}

public class Json
{
    public static List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("user.json"));

    public static List<User> Users() => users;

    public void AddUser(User user)
    {
        users.Add(user);

        string jsonString = JsonConvert.SerializeObject(users, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
        File.WriteAllText("user.json", jsonString);
    }

    public static List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText("order.json"));

    public static List<Order> Orders() => orders;

    public void AddOrder(Order order)
    {
        orders.Add(order);

        string jsonString = JsonConvert.SerializeObject(orders, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
        File.WriteAllText("order.json", jsonString);
    }

    public Json(){}
}

public class Main
{
    public void AddOrder(Order order)
    {
        Json json = new Json();
        //TestJson json = new TestJson();
        json.AddOrder(order);
    }

    public List<Order> GetOrders()
    {
        return Json.Orders();
        //return TestJson.Orders();
    }

    public Order GetOrderById(string id)
    {
        List<Order> orders = Json.Orders();
        //List<Order> orders = TestJson.Orders();

        try
        {
            return orders.First(o => o.Id == id);
        } catch (Exception ex)
        {
            return null;
        }
    }

    public void Pass(Order order)
    {
        if (order.Finish < DateTime.Now)
            order.Price += 15000 * (DateTime.Now.Day - order.Finish.Day) - (DateTime.Now.Month - order.Finish.Month) * 30;
        
        order.Finish = DateTime.Now;
        order.Status = "Сдан";
    }
}

public class TestJson
{
    public static List<User> users = new List<User> { new User("test", "test"), new User("Test", "Test") };
    public static List<User> Users() => users;

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public static List<Order> orders = new List<Order> { new Order("3244", "Test", "Гараж", "проектирование",2345, DateTime.Parse("01.01.2023"), DateTime.Parse("01.12.2023")) };

    public static List<Order> Orders() => orders;

    public void AddOrder(Order order)
    {
        orders.Add(order);
    }

    public TestJson(){}
}

public class Order
{
    public string Id;
    public string Name;
    public string Description;
    public string Status;
    public double Price;
    public DateTime Created;
    public DateTime Finish;

    public Order(string id, string name, string description, string status, double price, DateTime created, DateTime finish)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
        Price = price;
        Created = created;
        Finish = finish;
    }
}

public class User
{
    public string Login;
    public string Password;

    public User(string login, string password)
    {
        this.Login = login;
        this.Password = password;
    }
}

public partial class Settings : Window
{
    private static ToggleButton _current;

    public Settings() {
        InitializeComponent();
        _current = BTThemeLight;
    }

    private void BTNC(ToggleButton This) {
        if (_current == This) { _current.IsChecked = true; return;}
        _current.IsChecked = false;
        _current = This;
    }

    private void BTThemeLight_OnClick(object sender, RoutedEventArgs e) {
        BTNC(BTThemeLight);
        Uri uri = new Uri(@"Dictionary/DictionaryLight.xaml", UriKind.Relative);
        ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
        Application.Current.Resources.Clear();
        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
    }

    private void BTThemeDark_OnClick(object sender, RoutedEventArgs e) {
        BTNC(BTThemeDark);
        Uri uri = new Uri(@"Dictionary/DictionaryDark.xaml", UriKind.Relative);
        ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
        Application.Current.Resources.Clear();
        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
    }

    private void BTThemeContrast_OnClick(object sender, RoutedEventArgs e) {
        BTNC(BTThemeContrast);
        Uri uri = new Uri(@"Dictionary/DictionaryContrast.xaml", UriKind.Relative);
        ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
        Application.Current.Resources.Clear();
        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
    }
}