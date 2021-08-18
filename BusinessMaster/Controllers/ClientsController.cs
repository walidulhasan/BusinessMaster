using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessMaster.Models;
using BusinessMaster.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Grpc.Core;

namespace BusinessMaster.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ProductDbContext _context;
        //private readonly IHostingEnvironment _he;
        private readonly IWebHostEnvironment _he;


        public ClientsController(ProductDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.clients.Include(c => c.ServicesName);
            return View(await productDbContext.ToListAsync());
        }
        public async Task<IActionResult> ClientView()
        {
            var productDbContext = _context.clients.Include(c => c.ServicesName);
            return View(await productDbContext.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.clients
                .Include(c => c.ServicesName)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["ServicesNameId"] = new SelectList(_context.servicesNames, "ServicesNameId", "ServiceName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientName,ClientAge,ClientBudget,WorkDeliveryDate,PicturPath,ServicesNameId")] BusinessMasterViewModel clientVM)
        {
            string msg = "";
            if (ModelState.IsValid)
            {

                Client c = new Client();
                c.ClientName = clientVM.ClientName;
                c.ClientAge = clientVM.ClientAge;
                c.ClientBudget = clientVM.ClientBudget;
                c.WorkDeliveryDate = clientVM.WorkDeliveryDate;

                //Image
                string webroot = _he.WebRootPath;
                string folder = "product_image";
                string imageFileName = Guid.NewGuid() + "_" + Path.GetFileName(clientVM.PicturPath.FileName);
                string fileToWrite = Path.Combine(webroot, folder, imageFileName);

                c.Picture = imageFileName;
                c.ServicesNameId = clientVM.ServicesNameId;


                using (var stream = new FileStream(fileToWrite, FileMode.Create))
                {
                    await clientVM.PicturPath.CopyToAsync(stream);
                }
                _context.Add(c);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                msg = "Product data is incomplete. Please try again.";
            }
            ViewData["ServicesNameId"] = new SelectList(_context.servicesNames, "ServicesNameId", "ServiceName", clientVM.ServicesNameId);
            return View(clientVM);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var client = await _context.clients.FindAsync(Id);
            if (client == null)
            {
                return NotFound();
            }
            BusinessMasterViewModel evm = new BusinessMasterViewModel
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientAge = client.ClientAge,
                ClientBudget = client.ClientBudget,
                WorkDeliveryDate = client.WorkDeliveryDate,
                Picture = client.Picture,
                ServicesNameId = client.ServicesNameId,


            };
            ViewData["ServicesNameId"] = new SelectList(_context.servicesNames, "ServicesNameId", "ServiceName", evm.ServicesNameId);
            return View(evm);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientName,ClientAge,ClientBudget,WorkDeliveryDate,PicturPath,ServicesNameId")] BusinessMasterViewModel clientVM)
        {
            var client2 = await _context.clients.FindAsync(id);
            if (id != clientVM.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (clientVM.PicturPath!=null)
                    {
                        //Client client = _context.clients.Find(id);
                        ////For Image Delete Form Folder
                        //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\product_image", client.Picture);

                        //if (System.IO.File.Exists(path))
                        //{
                        //    System.IO.File.Delete(path);
                        //}
                        //Image
                        
                        var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\product_image", client2.Picture);
                        System.IO.File.Delete(path2);

                        string webroot = _he.WebRootPath;
                        string folder = "product_image";
                        string imageFileName = Guid.NewGuid() + "_" + Path.GetFileName(clientVM.PicturPath.FileName);
                        string fileToWrite = Path.Combine(webroot, folder, imageFileName);

                        client2.ClientId = clientVM.ClientId;
                        client2.ClientName = clientVM.ClientName;
                        client2.ClientAge = clientVM.ClientAge;
                        client2.ClientBudget = clientVM.ClientBudget;
                        client2.WorkDeliveryDate = clientVM.WorkDeliveryDate;
                        client2.Picture = imageFileName;
                        client2.ServicesNameId = clientVM.ServicesNameId;
                        

                        using (var stream = new FileStream(fileToWrite, FileMode.Create))
                        {
                            await clientVM.PicturPath.CopyToAsync(stream);
                        }
                        _context.Update(client2);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        
                        client2.ClientId = clientVM.ClientId;
                        client2.ClientName = clientVM.ClientName;
                        client2.ClientAge = clientVM.ClientAge;
                        client2.ClientBudget = clientVM.ClientBudget;
                        client2.WorkDeliveryDate = clientVM.WorkDeliveryDate;
                        client2.ServicesNameId = clientVM.ServicesNameId;
                        client2.Picture = client2.Picture;
                        _context.Update(client2);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(clientVM.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["ServicesNameId"] = new SelectList(_context.servicesNames, "ServicesNameId", "ServiceName", clientVM.ServicesNameId);
            return View(clientVM);
        }

        //GET: Clients/Delete/5
        public IActionResult Delete(int? id)
        {
            Client client = _context.clients.Find(id);
            if (client.Picture!=null)
            {
                //For Image Delete Form Folder
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\product_image", client.Picture);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                //Delete Data
                var del = (from Client in _context.clients where Client.ClientId == id select Client).FirstOrDefault();
                _context.clients.Remove(del);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                //Delete Data
                var del = (from Client in _context.clients where Client.ClientId == id select Client).FirstOrDefault();
                _context.clients.Remove(del);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
           
        }
        //[HttpPost]
        //[Route("Clients/Delete/{id:int}")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{

        //    var client = _context.clients.SingleOrDefault(s => s.ClientId == id);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\product_image", client.Picture);

        //    if (System.IO.File.Exists(path))
        //    {
        //        System.IO.File.Delete(path);
        //    }

        //    _context.clients.Remove(client);

        //    _context.SaveChanges();

        //    return Ok();

        //}

        // POST: Clients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var client = await _context.clients.FindAsync(id);
        //    _context.clients.Remove(client);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ClientExists(int id)
        {
            return _context.clients.Any(e => e.ClientId == id);
        }
    }
}
