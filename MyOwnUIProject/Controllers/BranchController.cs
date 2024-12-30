using DTOs;
using Microsoft.AspNetCore.Mvc;
using ServicesContract;

namespace MyOwnUIProject.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService BranchService)
        {
            _branchService = BranchService;
        }

        [Route("/Branches")]
        public IActionResult Index()
        {
            var branches=_branchService.GetBranches();
            return View(branches);
        }

        [HttpGet]
        [Route("/Branches/Edit/{branchId:int}")]
        public IActionResult EditBranch(int branchId)
        {
            var branch = _branchService.GetBranchById(branchId);
            if (branch==null)
            {
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        [HttpPost]
        [Route("/Branches/Edit/{branchId:int}")]
        public IActionResult EditBranch(BranchDto model)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();

                foreach (var item in ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                ViewBag.Errors = errors;
                return View();
            }

            var branch = _branchService.GetBranchById(model.Id);
            if (branch==null)
            {
                return RedirectToAction("Index");
            }
            _branchService.UpdateBranch(model);
            return RedirectToAction("Index");
        }

        [Route("/Branches/Add")]
        public IActionResult AddBranch()
        {
            return View();
        }

        [Route("/Branches/Delete")]
        public IActionResult DeleteBranch()
        {
            return View();
        }

        [Route("/Branches/GetProducts/{branchId}")]
        public IActionResult GetProducts(int branchId)
        {
            var products = _branchService.GetProducts(branchId);
            var branch= _branchService.GetBranchById(branchId);
            ViewBag.Branch = branch.Name;

            return View(products);
        }
    }
}
