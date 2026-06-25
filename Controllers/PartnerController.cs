using InsurancePartnerManagement.Helpers;
using InsurancePartnerManagement.Models;
using InsurancePartnerManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePartnerManagement.Controllers
{
    public class PartnerController : Controller
    {
        private readonly PartnerRepository _repository;

        public PartnerController(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection")!;
            _repository = new PartnerRepository(connectionString);
        }

        public IActionResult Index(int? newPartnerId)
        {
            var partners = _repository.GetAllPartners();

            ViewBag.NewPartnerId = newPartnerId;

            return View(partners);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Partner partner)
        {
            if (!ModelState.IsValid)
            {
                return View(partner);
            }

            if (!string.IsNullOrEmpty(partner.CroatianPIN) && !OibValidator.IsValid(partner.CroatianPIN))
            {
                ModelState.AddModelError("CroatianPIN", "OIB nije ispravan");
                return View(partner);
            }

            try
            {
                var id = _repository.CreatePartner(partner);
                return RedirectToAction("Index", new { newPartnerId = id });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed: Partners.ExternalCode"))
                {
                    ModelState.AddModelError("ExternalCode", "Partner sa ovim External Code-om već postoji");
                }
                else
                {
                    ModelState.AddModelError("", "Greška pri spremanju: " + ex.Message);
                }
                return View(partner);
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var partner = _repository.GetPartnerById(id);
                if (partner == null)
                {
                    return NotFound();
                }

                var policies = _repository.GetPoliciesByPartnerId(id);
                var partnerPolicies = new PartnerPolicies
                {
                    Partner = partner,
                    Policies = policies.ToList()
                };

                return Json(partnerPolicies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.CreatePolicy(policy);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult GetPolicyStats(int partnerId)
        {
            try
            {
                var stats = _repository.GetPolicyStats(partnerId);
                return Json(new { count = stats.Count, totalAmount = stats.TotalAmount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
