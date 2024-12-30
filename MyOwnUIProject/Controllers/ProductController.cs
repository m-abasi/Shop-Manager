using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesContract;

namespace MyOwnUIProject.Controllers
{
    public class ProductController : Controller
    {
        [Route("/products")]
        public async Task<IActionResult> Index(
            [FromServices] IProductService productService,
            [FromServices] ISearchTypeService searchTypeService,
            string searchType, string searchText, string sortField = nameof(ProductDto.Name), OrderEnum orderBy = OrderEnum.ASC)
        {
            var result =await productService.GetProductsByFilter(searchType, searchText, sortField, orderBy);

            ViewBag.searchTypeDto = searchTypeService.GetSearchTypeDto();


            ViewBag.searchType = searchType;
            ViewBag.searchText = searchText;

            ViewBag.sortField = sortField;
            ViewBag.orderBy = orderBy.ToString();


            return View( result);
        }
        [HttpGet]
        [Route("/products/create")]
        public IActionResult CreateProduct([FromServices] IBranchService branchService)
        {
            ViewBag.Branches = branchService.GetBranches();

            return View();
        }

        [HttpPost]
        [Route("/products/create")]
        public async Task<IActionResult> CreateProduct([FromServices] IProductService productService, [FromServices] IBranchService branchService, ProductDto model)
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


            await productService.AddProduct(model);
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Route("/products/delete/{productId:int}")]
        public async Task<IActionResult> Delete([FromServices] IProductService productService,int productId)
        {
            var product = await productService.GetProductById(productId);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpPost]
        [Route("/products/delete/{productId:int}")]
        public async Task<IActionResult> Delete([FromServices] IProductService productService, ProductDto model)
        {
            var product = await productService.GetProductById(model.Id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var result = await productService.Delete(model);
            if (result.Succeeded==true)
            {
                return RedirectToAction("Index");

            }

            ViewBag.Errors = new List<string>() { result.ErrorMessage };
            return View(product);
        }

        [HttpGet]
        [Route("/products/Edit/{productId:int}")]
        public async Task<IActionResult> Edit([FromServices] IBranchService branchService, [FromServices] IProductService productService, int productId)
        {
            var product = await productService.GetProductById(productId);
            if (product==null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Branches = branchService.GetBranches();
            return View(product);
        }

        [HttpPost]
        [Route("/products/Edit/{productId:int}")]
        public async Task<IActionResult> Edit([FromServices] IBranchService branchService, [FromServices] IProductService productService, ProductDto model)
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
            var product =await productService.GetProductById(model.Id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var response =await productService.UpdateProduct(model);
            if (response.Succeeded) 
            {
                return RedirectToAction("Index");

            }
            return View();

        }
    }
}
