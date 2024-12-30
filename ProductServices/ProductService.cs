using DTOs;
using ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IBranchService _branchService;
        private ShopDbContext _dbContext;

        public ProductService(IBranchService branchService, ShopDbContext dbContext)
        {
            _branchService = branchService;

            _dbContext = dbContext;


        }

        public async Task<List<ProductDto>> GetAll()
        {
            var result = await _dbContext.Products.Include(x => x.Branch).ToListAsync();
            return result.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                BranchName = x.Branch.Name,
                BranchId = x.BranchId,
                HasDiscount = x.HasDiscount,
                DiscountType = x.DiscountType,
                Discount = x.Discount,
                Price = x.Price,
            }).ToList();
        }

        public async Task<ResponseDto> Delete(ProductDto model)
        {
            var response = new ResponseDto() { Succeeded = true };
            if (model == null)
            {
                response.Succeeded = false;
                response.ErrorMessage = "مقدر مورد نظر یافت نشد";
                return response;
            }
            var product = await _dbContext.Products.FirstAsync(x => x.Id == model.Id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return response;
        }
        public async Task<ResponseDto> UpdateProduct(ProductDto model)
        {
            var response = new ResponseDto() { Succeeded = true };
            try
            {
                var product = await _dbContext.Products.FirstAsync(x => x.Id == model.Id);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.HasDiscount = model.HasDiscount;
                product.DiscountType = model.DiscountType;
                product.Discount = model.Discount;
                product.Price = model.Price;
                product.BranchId = model.BranchId;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                response.Succeeded = false;
                response.ErrorMessage = "عملیات با خطا مواجه شد";
            }


            return response;

        }
        public async Task<ProductDto>? GetProductById(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                BranchId = product.BranchId,
                Description = product.Description,
                Price = product.Price,
                HasDiscount = product.HasDiscount,
                DiscountType = product.DiscountType,
                Discount = product.Discount

            };
        }

        public async Task<ResponseDto> AddProduct(ProductDto model)
        {
            var response = new ResponseDto { Succeeded = true };
            try
            {
                 await _dbContext.Products.AddAsync(new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    BranchId = model.BranchId,
                    Description = model.Description,
                    Price = model.Price,
                    HasDiscount = model.HasDiscount,
                    DiscountType = model.DiscountType,
                    Discount = model.Discount
                });
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                response.Succeeded = false;
                response.ErrorMessage = "خطا در ثبت محصول جدید";
            }

            return response;
        }

        public async Task<List<ProductDto>> GetProductsByFilter(string searchType, string searchText, string sortField, OrderEnum orderBy)
        {


            var result = await GetAll();



            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(searchType))
            {
                result = searchType switch
                {
                    "Name" => result.Where(x => x.Name.Contains(searchText)).ToList(),
                    "Description" => result.Where(x => x.Description.Contains(searchText)).ToList(),
                    "BranchName" => result.Where(x => x.BranchName.Contains(searchText)).ToList(),
                    _ => result
                };
            }

            if (sortField == nameof(ProductDto.Name) && orderBy == OrderEnum.ASC)
            {
                result = result.OrderBy(x => x.Name).ToList();
            }
            else if (sortField == nameof(ProductDto.Name) && orderBy == OrderEnum.DESC)
            {
                result = result.OrderByDescending(x => x.Name).ToList();
            }

            else if (sortField == nameof(ProductDto.Price) && orderBy == OrderEnum.ASC)
            {
                result = result.OrderBy(x => x.Price).ToList();
            }
            else if (sortField == nameof(ProductDto.Price) && orderBy == OrderEnum.DESC)
            {
                result = result.OrderByDescending(x => x.Price).ToList();
            }

            else if (sortField == nameof(ProductDto.Discount) && orderBy == OrderEnum.ASC)
            {
                result = result.OrderBy(x => x.Discount).ToList();
            }
            else if (sortField == nameof(ProductDto.Discount) && orderBy == OrderEnum.DESC)
            {
                result = result.OrderByDescending(x => x.Discount).ToList();
            }



            return result;



        }
    }
}
