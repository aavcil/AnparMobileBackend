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
     

        public async Task CreateOrder(int customerId, int discount)
        {
            var htmlTemplate = new HtmlTemplates();
            this.htmlTemplate = htmlTemplate.Template;
            this.productTemplate = htmlTemplate.ProductTemplate;
            var util = new DataUtils();
            util.ReadCustomers();
            util.ReadCart();
            util.ReadProducts();
            customers = util.GetCustomers();
            carts = util.GetCarts();
            products = util.GetProducts();
            var customer = customers.FirstOrDefault(a => a.Id == customerId);
            await WriteCustomer(customer);
            await WriteProducts(discount);
        }

        private async Task WriteProducts(int discount)
        {
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
                var netPrice = product.ListPrice - (product.ListPrice * (discount / 100));
                totalCarton += cart.Quantity;
                totalM3 += cart.Quantity * product.CartonVolume;
                var totalPrice = cart.Quantity * product.CartonAmount * netPrice;
                total += totalPrice;
                var pTemplate = productTemplate;
                pTemplate
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
            }

            KDV = total * 0.18;
            generalTotal = KDV + total;

            htmlTemplate = htmlTemplate.Replace("{Urunler}", temporary);

            await WriteTotal(totalCarton, totalM3, totalDesi, total, KDV, generalTotal);

        }

        public async Task WriteTotal(int totalCarton, double totalM3, double totalDesi, double totalPrice, double KDV, double generalTotal)
        {
            htmlTemplate = htmlTemplate
                .Replace("{KoliToplam}", totalCarton.ToString())
                .Replace("{MToplam}", totalM3.ToString())
                .Replace("{ToplamDesi}", totalDesi.ToString())
                .Replace("{Toplam}", totalPrice.ToString())
                .Replace("{KDV}", KDV.ToString())
                .Replace("{GenelToplam}", generalTotal.ToString());
            
        }

        public async Task WriteCustomer(Customer customer)
        {
            htmlTemplate=htmlTemplate
                .Replace("{Firma}", customer.CompanyName)
                .Replace("{FaturaAdresi}", customer.Address)
                .Replace("{VDairesi}", customer.TaxNumber)
                .Replace("{Iletisim}", customer.Phone)
                .Replace("{SevkSekli}", "Fabrika Teslim");
        }

        public string GetTemplate()
        {
            return htmlTemplate;
        }
    }
}
