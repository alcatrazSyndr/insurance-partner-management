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

        public IActionResult Index()
        {
            var partners = _repository.GetAllPartners();

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

            var id = _repository.CreatePartner(partner);
            return RedirectToAction("Index", new { newPartnerId = id });
        }

        public IActionResult Details(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePolicy(Policy policy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.CreatePolicy(policy);
            return Ok();
        }
    }
}
