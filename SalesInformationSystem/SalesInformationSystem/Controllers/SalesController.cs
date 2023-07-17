using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesInformationSystem.Models;
using SalesInformationSystem.ViewModel;

namespace SalesInformationSystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly Context db;
        public SalesController(Context db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Single(long? id)
        {
            var sm = db.SaleMasters.Find(id);
            var sale = new VmSale();
            if (sm != null)
            {
                sale.CreateDate = sm.CreateDate;
                sale.CustomerName = sm.CustomerName;
                sale.CustomerAddress = sm.CustomerAddress;
                sale.Gender = sm.Gender;
                sale.SaleId = sm.SaleId;
                var listSD = new List<VmSale.VmSaleDetail>();
                var lsd = db.SaleDetails.Where(x => x.SaleId == id).ToList();
                foreach (var list in lsd)
                {
                    var sd = new VmSale.VmSaleDetail();
                    sd.ProductName = list.ProductName;
                    sd.Price = list.Price;
                    sd.SaleId = sm.SaleId;
                    listSD.Add(sd);
                }
                sale.SaleDetails = listSD;
            }
            ViewData["List"] = db.SaleMasters.ToList();
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Single(VmSale model)
        {
            var sm = db.SaleMasters.Find(model.SaleId);
            if (sm == null)
            {
                sm = new SaleMaster();
                sm.CreateDate = model.CreateDate;
                sm.CustomerName = model.CustomerName;
                sm.CustomerAddress = model.CustomerAddress;
                sm.Gender = model.Gender;
                db.SaleMasters.Add(sm);
                db.SaveChanges();
            }
            else
            {

                sm.CreateDate = model.CreateDate;
                sm.CustomerName = model.CustomerName;
                sm.CustomerAddress = model.CustomerAddress;
                sm.Gender = model.Gender;
                db.Entry(sm).State = EntityState.Modified;
                var sdr = db.SaleDetails.Where(x => x.SaleId == model.SaleId).ToList();
                db.SaleDetails.RemoveRange(sdr);
                db.SaveChanges();
            }
            var listSD = new List<SaleDetail>();
            for (int i = 0; i < model.ProductName.Length; i++)
            {
                if (!string.IsNullOrEmpty(model.ProductName[i]))
                {
                    var sd = new SaleDetail();
                    sd.ProductName = model.ProductName[i];
                    sd.Price = (decimal)model.Price[i];
                    sd.SaleId = sm.SaleId;
                    listSD.Add(sd);
                }
            }
            db.SaleDetails.AddRange(listSD);
            db.SaveChanges();
            return RedirectToAction("Single");
        }

        [HttpGet]
        public IActionResult SingleDelete(long id)
        {
            var sm = db.SaleMasters.Find(id);
            var sd = db.SaleDetails.Where(x => x.SaleId == id).ToList();
            if (sm != null)
            {
                db.SaleMasters.Remove(sm);
                db.SaleDetails.RemoveRange(sd);
                db.SaveChanges();
            }
            return RedirectToAction("Single");
        }
    }
}
