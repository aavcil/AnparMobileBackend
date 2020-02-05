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
    public class DataUtils
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        private static readonly string ApplicationName = "AnparData";

        private static readonly string SpreadsheetId = "13ekQ86KKSIDriTWkCg5wlWaJdA_mMAtoNv07d4FER1M";
        private static readonly string sheet = "products";
        private static SheetsService _service;
        public static List<Products> ProdList = new List<Products>();
        public static List<Categories> CategoryList = new List<Categories>();
        public static List<Customer> CustomerList = new List<Customer>();
        public static List<Cart> CartList = new List<Cart>();
        public static List<Orders> OrderList = new List<Orders>();
        public static List<Person> PersonList = new List<Person>();
        public static List<PersonOrder> PersonOrderList = new List<PersonOrder>();

        public DataUtils()
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

        public void ReadProducts()
        {
            ProdList = new List<Products>();
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
                ProdList.Add(product);
            }
        }

        public void ReadCategories()
        {
            CategoryList = new List<Categories>();
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
                CategoryList.Add(category);
            }
        }

        public void ReadCustomers()
        {
            CustomerList = new List<Customer>();
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
                CustomerList.Add(customer);
            }
        }

        public void ReadCart()
        {
            CartList = new List<Cart>();
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
                CartList.Add(cart);
            }
        }
        public void ReadPersonOrder()
        {
            PersonOrderList = new List<PersonOrder>();
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
                PersonOrderList.Add(personOrder);
            }
        }
        public void ReadPerson()
        {
            PersonList = new List<Person>();
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
                PersonList.Add(person);
            }
        }
        public void ReadOrders()
        {
            OrderList = new List<Orders>();
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
                OrderList.Add(order);
            }
        }

        public List<Products> GetProducts()
        {
            return ProdList;
        }

        public List<Categories> GetCategories()
        {
            return CategoryList;
        }
        public List<Person> GetPersons()
        {
            return PersonList;
        }
        public List<PersonOrder> GetPersonOrders()
        {
            return PersonOrderList;
        }

        public List<Customer> GetCustomers()
        {
            return CustomerList;
        }

        public List<Cart> GetCarts()
        {
            return CartList;
        }
        public List<Orders> GetOrders()
        {
            return OrderList;
        }

        public void WriteCarts(Cart cart)
        {
            var range = $"cart!A:C";
            var valueRange = new ValueRange();
            ReadCart();
            var id = CartList.Any() ? CartList.Last().Id + 1 : 1;
            cart.Id = id;

            var oblist = new List<object>() { cart.Id, cart.ProductId, cart.Quantity };
            valueRange.Values = new List<IList<object>> { oblist };

            var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
        }
        public void WriteOrders(Orders order)
        {
            var range = $"orders!A:E";
            var valueRange = new ValueRange();
            ReadOrders();
            var id = OrderList.Last().Id + 1;
            order.Id = id;

            var oblist = new List<object>() { order.Id, order.ProductId, order.Quantity, order.CustomerId, order.OrderNumber };
            valueRange.Values = new List<IList<object>> { oblist };

            var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            appendRequest.Execute();
        }
        public void UpdateEntry()
        {
            ReadCart();

            var range = $"cart!A:C";
            var valueRange = new ValueRange();

            var oblist = new List<object>() { 0 };
            valueRange.Values = new List<IList<object>> { oblist };
            ClearValuesRequest requestBody = new ClearValuesRequest();

            SpreadsheetsResource.ValuesResource.ClearRequest request = _service.Spreadsheets.Values.Clear(requestBody, SpreadsheetId, range);
            // To execute asynchronously in an async method, replace `request.Execute()` as shown:
            ClearValuesResponse response = request.Execute();
            // Data.ClearValuesResponse response = await request.ExecuteAsync();
            // TODO: Change code below to process the `response` object:
            Console.WriteLine(JsonConvert.SerializeObject(response));

        }
    }
}
