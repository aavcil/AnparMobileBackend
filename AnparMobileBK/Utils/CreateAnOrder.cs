using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnparMobileBK.Helpers;
using AnparMobileBK.Models;

namespace AnparMobileBK.Utils
{
    public class CreateAnOrder
    {
        private string htmlTemplate;
        private string productTemplate;
        private List<Customer> customers = new List<Customer>();
        private List<Cart> carts = new List<Cart>();
        private List<Products> products = new List<Products>();
        private List<Person> persons = new List<Person>();
        private List<PersonOrder> personOrders = new List<PersonOrder>();


        public async Task CreateOrder(int customerId, string discount, int personId)
        {
            var htmlTemplate = new HtmlTemplates();
            this.htmlTemplate = htmlTemplate.Template;
            this.productTemplate = htmlTemplate.ProductTemplate;
            customers = DataUtils.GetCustomers();
            carts = DataUtils.GetCarts();
            products = DataUtils.GetProducts();
            persons = DataUtils.GetPersons();
            personOrders = DataUtils.GetPersonOrders();
            var customer = customers.FirstOrDefault(a => a.Id == customerId);
            var person = persons.FirstOrDefault(a => a.Id == personId);
            await WriteCustomer(customer);
            await WriteProducts(discount);
            await WritePerson(person);
        }

        private async Task WriteProducts(string discount)
        {
            string[] discounts = discount.Split("-");
            var total = 0.0;
            string temporary = "";
            var KDV = 0.0;
            var generalTotal = 0.0;
            int i = 1;
            var totalCarton = 0;
            var totalM3 = 0.0;
            var totalDesi = 0.0;
            foreach (var cart in carts)
            {
                var product = products.FirstOrDefault(q => q.Id == cart.ProductId);

                var w1 = product.CartonMeasure.Split(" x ")[0];
                var w2 = product.CartonMeasure.Split(" x ")[1];
                var w3 = product.CartonMeasure.Split(" x ")[2];
                totalDesi += int.Parse(w1) * int.Parse(w2) * int.Parse(w3) / 3000;
                var netPrice = product.ListPrice;
                foreach (var s in discounts)
                {
                    double sayi = double.Parse(s);
                    var dis = (sayi / 100);
                    netPrice = netPrice - (netPrice * dis);
                    Console.WriteLine(s);
                }
                totalCarton += cart.Quantity;
                totalM3 += cart.Quantity * product.CartonVolume;
                var totalPrice = cart.Quantity * product.CartonAmount * netPrice;
                total += totalPrice;
                var pTemplate = productTemplate;
                pTemplate = pTemplate
                     .Replace("{No}", i.ToString())
                     .Replace("{Kodu}", product.ModelCode)
                     .Replace("{Tanımı}", product.Description)
                     .Replace("{Olcusu}", product.ProductMeasure)
                     .Replace("{Miktar}", cart.Quantity.ToString())
                     .Replace("{IcMiktar}", product.CartonAmount.ToString())
                     .Replace("{ListeFiyati}", product.ListPrice.ToString())
                     .Replace("{IskontoOranı}", discount.ToString() + "%")
                     .Replace("{BirimFiyat}", String.Format("{0:0.00}", netPrice) + " TL")
                     .Replace("{ToplamTutar}", String.Format("{0:0.00}", totalPrice) + " TL");
                temporary += pTemplate;
                i += 1;
            }

            KDV = total * 0.18;
            generalTotal = KDV + total;

            htmlTemplate = htmlTemplate.Replace("{Urunler}", temporary);

            await WriteTotal(totalCarton, totalM3, totalDesi, total, KDV, generalTotal);

        }

        public async Task WriteTotal(int totalCarton, double totalM3, double totalDesi, double totalPrice, double KDV, double generalTotal)
        {
            htmlTemplate = htmlTemplate
                .Replace("{KoliToplam}", totalCarton.ToString() + " Adet")
                .Replace("{MToplam}", totalM3.ToString() + " m3")
                .Replace("{ToplamDesi}", totalDesi.ToString() + " Desi")
                .Replace("{Toplam}", $"{totalPrice:#.00}")
                .Replace("{KDV}", $"{KDV:#.00}")
                .Replace("{GenelToplam}", $"{generalTotal:#.00}");

        }

        public async Task WriteCustomer(Customer customer)
        {
            htmlTemplate = htmlTemplate
                .Replace("{Firma}", customer.CompanyName)
                .Replace("{FaturaAdresi}", customer.Address)
                .Replace("{VDairesi}", customer.TaxNumber)
                .Replace("{Iletisim}", customer.Phone)
                .Replace("{SevkSekli}", "Fabrika Teslim");
        }

        public async Task WritePerson(Person person)
        {
            var personOrderByPerson = personOrders.Where(a => a.PersonId == person.Id && a.OrderDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
            if (personOrderByPerson.Count < 1)
            {
                var date = DateTime.Now;
                var year = date.Year.ToString().Substring(2);
                var month = date.Month.ToString().Length == 1 ? "0" + date.Month.ToString() : date.Month.ToString();
                var day = date.Day.ToString().Length == 1 ? "0" + date.Day.ToString() : date.Day.ToString();
                var orderNo = year + month + day + "-" + person.Name.Substring(0, 1) + "01";
                htmlTemplate = htmlTemplate
                    .Replace("{Tarih}", DateTime.Now.ToString("dd/MM/yyyy"))
                    .Replace("{Plasiyer}", person.Name)
                    .Replace("{SıraNo}", "1")
                    .Replace("{SiparisNo}", orderNo)
                    .Replace("{Vade}", "");

            }
            else
            {
                var personOrder = personOrderByPerson.OrderBy(a => a.OrderDate).Last();
                var date = DateTime.Now;
                var year = date.Year.ToString().Substring(2);
                var month = date.Month.ToString().Length == 1 ? "0" + date.Month.ToString() : date.Month.ToString();
                var day = date.Day.ToString().Length == 1 ? "0" + date.Day.ToString() : date.Day.ToString();
                if (DateTime.Compare(personOrder.OrderDate, DateTime.Now) > 0)
                {

                    var orderNo = year + month + day + "-" + person.Name.Substring(0, 1) + "01";
                    htmlTemplate = htmlTemplate
                        .Replace("{Tarih}", DateTime.Now.ToString("dd/MM/yyyy"))
                        .Replace("{Plasiyer}", person.Name)
                        .Replace("{SıraNo}", "1")
                        .Replace("{SiparisNo}", orderNo)
                        .Replace("{Vade}", "");
                }
                else
                {
                    //200125 - E01
                    var order = personOrder.OrderNo.Substring(personOrder.OrderNo.Length - 2);
                    int o = int.Parse(order);

                    var temp = o+1;
                    var a = "0";
                    if (temp < 10)
                        a += temp.ToString();
                    else
                    {
                        a = temp.ToString();
                    }
                    var orderNo = year + month + day + "-" + person.Name.Substring(0, 1) + a;
                    htmlTemplate = htmlTemplate
                        .Replace("{Tarih}", DateTime.Now.ToString("dd/MM/yyyy"))
                        .Replace("{Plasiyer}", person.Name)
                        .Replace("{SıraNo}", "1")
                        .Replace("{SiparisNo}", orderNo)
                        .Replace("{Vade}", "");
                }
            }

        }
        public string GetTemplate()
        {
            return htmlTemplate;
        }
    }
}
