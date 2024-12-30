using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using ServicesContract;

namespace Services
{
    public class BranchService : IBranchService
    {
        //private List<BranchDto> _branchDto;
        private ShopDbContext _dbContext;

        public BranchService(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BranchDto> GetBranches()
        {
            return _dbContext.Branches.Select(x => new BranchDto()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                BranchCode = x.BranchCode,
                Instagram = x.Instagram
            }).ToList();
        }

        public BranchDto GetBranchById(int branchId)
        {
            var entityObject = _dbContext.Branches.Include(x => x.Products).First(b => b.Id == branchId);
            var products = entityObject.Products.ToList();
            return new BranchDto
            {
                Id = entityObject.Id,
                Name = entityObject.Name,
                Address = entityObject.Address,
                BranchCode = entityObject. BranchCode,
                Instagram = entityObject. Instagram
                
            };
        }

        public ResponseDto DeleteBranch(BranchDto model)
        {
            var response = new ResponseDto() { Succeeded = true };
            if (model == null)
            {
                response.Succeeded = false;
                response.ErrorMessage = "موجود نیست";
                return response;
            }

            var branch = _dbContext.Branches.First(x => x.Id == model.Id);
            _dbContext.Branches.Remove(branch);
            _dbContext.SaveChanges();
            return response;
        }

        public ResponseDto UpdateBranch(BranchDto model)
        {
            var response = new ResponseDto() { Succeeded = true };
            try
            {
                var branch = _dbContext.Branches.First(b => b.Id == model.Id);
                branch.Name= model.Name;
                branch.Address=model.Address;
                branch.BranchCode = model.BranchCode;
                branch.Instagram = model.Instagram;
                _dbContext.SaveChanges();
            }
            catch(Exception)
            {
                response.Succeeded = false;
                response.ErrorMessage = "موجود نیست";
            }
            return response;

        }

        public List<ProductDto> GetProducts(int branchId)
        {
            var branch = _dbContext.Branches.Include(x => x.Products).First(x => branchId == x.Id);
            var products = branch.Products.ToList();
            var productDtos = products.Select(x => new ProductDto()
            {
                
                Name = x.Name,
                Description = x.Description,
                BranchName = x.Branch.Name,
                BranchId = x.BranchId,
                HasDiscount = x.HasDiscount,
                DiscountType = x.DiscountType,
                Discount = x.Discount,
                Price = x.Price

            }).ToList();
            return productDtos;
        }

        public ResponseDto AddBranch(BranchDto model)
        {
            var response = new ResponseDto() { Succeeded = true };
            try
            {
                var branch = new Branch()
                {
                    Name = model.Name,
                    Address = model.Address,
                    BranchCode = model.BranchCode,
                    Instagram = model.Instagram

                };
                _dbContext.Branches.Add(branch);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                response.Succeeded = false;
                response.ErrorMessage = "موجود نیست";
            }

            return response;
        }
    }
}
