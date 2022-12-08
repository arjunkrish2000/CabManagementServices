using Microsoft.AspNetCore.Mvc;

namespace CabManagementSystem.Areas.CabDriver.Controllers
{
    [Area("CabDriver")]
    public class DriverController : Controller
    {

        private readonly ApplicationDbContext _db;

        public DriverController(ApplicationDbContext db)
        {
            _db = db;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DriverDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DriverDetails(DriverDetailsViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new DriverDetails()
            {
                LicenceNo = model.LicenceNo,
                RegistrationNo = model.RegistrationNo,
                CabName = model.CabName,
                //CarModel = model.CarModel,
                DriverId = id
            };
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            return Redirect("/");
        }
    }
}