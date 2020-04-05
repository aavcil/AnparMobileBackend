using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBK.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;

namespace AnparMobileBK.Utils
{
    public static class DataUtils
    {
        private static readonly string[] Scopes = {SheetsService.Scope.Spreadsheets};

        private static readonly string ApplicationName = "AnparData";

        private static readonly string SpreadsheetId = "13ekQ86KKSIDriTWkCg5wlWaJdA_mMAtoNv07d4FER1M";
        private static readonly string sheet = "products";
        private static SheetsService _service;
        private static List<Products> _prodList = new List<Products>();
        private static List<Categories> _categoryList = new List<Categories>();
        private static List<Customer> _customerList = new List<Customer>();
        private static List<Cart> _cartList = new List<Cart>();
        private static List<Orders> _orderList = new List<Orders>();
        private static List<Person> _personList = new List<Person>();
        private static List<PersonOrder> _personOrderList = new List<PersonOrder>();
        private static List<Project> _projectList = new List<Project>();
        private static List<Photo> _photoList = new List<Photo>();
        private static List<Certificate> _certificates = new List<Certificate>();
        private static List<Catalog> _catalogs = new List<Catalog>();
        private static Corporate _corporate = new Corporate();

        static DataUtils()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("./client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }

        public static void ReadProducts()
        {
            _prodList = new List<Products>();
            var range = $"{sheet}!A:O";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var product = new Products()
                {
                    Id = int.Parse(row[0].ToString()),
                    CategoryId = int.Parse(row[1].ToString()),
                    ModelCode = row[2].ToString(),
                    Description = row[3].ToString(),
                    ProductMeasure = row[4].ToString(),
                    Series = row[5].ToString(),
                    Size = int.Parse(row[6].ToString()),
                    CartonSize = int.Parse(row[7].ToString()),
                    CartonAmount = int.Parse(row[8].ToString()),
                    RawMaterials = row[9].ToString(),
                    CartonMeasure = row[10].ToString(),
                    CartonWeight = double.Parse(row[11].ToString()),
                    CartonVolume = double.Parse(row[12].ToString()),
                    ListPrice = double.Parse(row[13].ToString()),
                    Price = double.Parse(row[14].ToString())
                };
                _prodList.Add(product);
            }
        }

        public static void ReadCategories()
        {
            _categoryList = new List<Categories>();
            var range = $"category!A:D";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var category = new Categories()
                {
                    Id = int.Parse(row[0].ToString()),
                    CategoryCode = int.Parse(row[1].ToString()),
                    Name = row[2].ToString(),
                    Description = row[3].ToString(),
                };
                _categoryList.Add(category);
            }
        }

        public static void ReadProjects()
        {
            _projectList = new List<Project>();
            var range = $"project!A:G";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var project = new Project()
                {
                    Id = int.Parse(row[0].ToString()),
                    Title = (row[1].ToString()),
                    Description = row[2].ToString(),
                    Location = row[3].ToString(),
                    FinishDate = DateTime.Parse(row[4].ToString()),
                    Measure = row[5].ToString(),
                    ProjectNevi = row[6].ToString()
                };
                _projectList.Add(project);
            }
        }

        public static void ReadPhotos()
        {
            _photoList = new List<Photo>();
            var range = $"photos!A:C";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var photo = new Photo()
                {
                    Id = int.Parse(row[0].ToString()),
                    Url = (row[1].ToString()),
                    ProjectId = int.Parse(row[2].ToString()),
                };
                _photoList.Add(photo);
            }
        }

        public static void ReadCatalogs()
        {
            _catalogs = new List<Catalog>();
            var range = $"catalogs!A:D";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var catalog = new Catalog()
                {
                    Id = int.Parse(row[0].ToString()),
                    Title = (row[1].ToString()),
                    Url = (row[2].ToString()),
                    ImageUrl = (row[3].ToString()),
                };
                _catalogs.Add(catalog);
            }
        }

        public static void ReadCertificates()
        {
            _certificates = new List<Certificate>();
            var range = $"certificate!A:D";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var certificate = new Certificate()
                {
                    Id = int.Parse(row[0].ToString()),
                    Title = (row[1].ToString()),
                    Description = (row[2].ToString()),
                    Url = (row[3].ToString()),
                };
                _certificates.Add(certificate);
            }
        }

        public static void ReadCorporate()
        {
            var range = $"corporate!A:B";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                _corporate = new Corporate()
                {
                    Id = int.Parse(row[0].ToString()),
                    Description = (row[1].ToString()),
                };
            }
        }

        public static void ReadCustomers()
        {
            _customerList = new List<Customer>();
            var range = $"customer!A:H";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var customer = new Customer()
                {
                    Id = int.Parse(row[0].ToString()),
                    Email = row[1].ToString(),
                    Address = row[2].ToString(),
                    Phone = row[3].ToString(),
                    CompanyName = row[4].ToString(),
                    CustomerName = row[5].ToString(),
                    TaxNumber = row[6].ToString(),
                    DeliveryType = row[7].ToString()
                };
                _customerList.Add(customer);
            }
        }

        public static void ReadCart()
        {
            _cartList = new List<Cart>();
            var range = $"cart!A:E";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var cart = new Cart()
                {
                    Id = int.Parse(row[0].ToString()),
                    ProductId = int.Parse(row[1].ToString()),
                    Quantity = int.Parse(row[2].ToString()),
                };
                _cartList.Add(cart);
            }
        }

        public static void ReadPersonOrder()
        {
            _personOrderList = new List<PersonOrder>();
            var range = $"personOrder!A:D";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var personOrder = new PersonOrder()
                {
                    Id = int.Parse(row[0].ToString()),
                    PersonId = int.Parse(row[1].ToString()),
                    OrderDate = DateTime.Parse(row[2].ToString()),
                    OrderNo = row[3].ToString()
                };
                _personOrderList.Add(personOrder);
            }
        }

        public static void ReadPerson()
        {
            _personList = new List<Person>();
            var range = $"person!A:B";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var person = new Person()
                {
                    Id = int.Parse(row[0].ToString()),
                    Name = row[1].ToString()
                };
                _personList.Add(person);
            }
        }

        public static void ReadOrders()
        {
            _orderList = new List<Orders>();
            var range = $"orders!A:F";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null || values.Count <= 0) return;
            foreach (var row in values)
            {
                var order = new Orders()
                {
                    Id = int.Parse(row[0].ToString()),
                    ProductId = int.Parse(row[1].ToString()),
                    Quantity = int.Parse(row[2].ToString()),
                    CustomerId = int.Parse(row[3].ToString()),
                    OrderNumber = int.Parse(row[4].ToString()),
                    Discount = int.Parse(row[5].ToString()),
                };
                _orderList.Add(order);
            }
        }

        public static List<Products> GetProducts()
        {
            return _prodList;
        }

        public static List<Categories> GetCategories()
        {
            return _categoryList;
        }

        public static List<Person> GetPersons()
        {
            return _personList;
        }

        public static List<PersonOrder> GetPersonOrders()
        {
            return _personOrderList;
        }

        public static List<Customer> GetCustomers()
        {
            return _customerList;
        }

        public static List<Cart> GetCarts()
        {
            return _cartList;
        }

        public static List<Orders> GetOrders()
        {
            return _orderList;
        }

        public static List<Project> GetProjects()
        {
            return _projectList;
        }

        public static List<Photo> GetPhotos()
        {
            return _photoList;
        }

        public static List<Catalog> GetCatalogs()
        {
            return _catalogs;
        }

        public static List<Certificate> GetCertificates()
        {
            return _certificates;
        }

        public static Corporate GetCorporate()
        {
            return _corporate;
        }


        public static void WriteCarts(Cart cart)
        {
            var range = $"cart!A:C";
            var valueRange = new ValueRange();
            ReadCart();
            var id = _cartList.Any() ? _cartList.Last().Id + 1 : 1;
            cart.Id = id;

            var oblist = new List<object>() {cart.Id, cart.ProductId, cart.Quantity};
            valueRange.Values = new List<IList<object>> {oblist};

            var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption =
                SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
        }

        public static void WriteOrders(Orders order)
        {
            var range = $"orders!A:F";
            var valueRange = new ValueRange();
            ReadOrders();
            var id = _orderList.Last().Id + 1;
            order.Id = id;

            var oblist = new List<object>()
                {order.Id, order.ProductId, order.Quantity, order.CustomerId, order.OrderNumber, order.Discount};
            valueRange.Values = new List<IList<object>> {oblist};

            var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption =
                SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
        }

        public static void UpdateEntry()
        {
            ReadCart();

            var range = $"cart!A:C";
            var valueRange = new ValueRange();

            var oblist = new List<object>() {0};
            valueRange.Values = new List<IList<object>> {oblist};
            ClearValuesRequest requestBody = new ClearValuesRequest();

            SpreadsheetsResource.ValuesResource.ClearRequest request =
                _service.Spreadsheets.Values.Clear(requestBody, SpreadsheetId, range);
            // To execute asynchronously in an async method, replace `request.Execute()` as shown:
            ClearValuesResponse response = request.Execute();
            // Data.ClearValuesResponse response = await request.ExecuteAsync();
            // TODO: Change code below to process the `response` object:
            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
    }
}