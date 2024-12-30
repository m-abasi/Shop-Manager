using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsByFilter(string searchType, string searchText, string sortField, OrderEnum orderBy);
        Task<ResponseDto> AddProduct(ProductDto model);
        Task<ProductDto>? GetProductById(int id);
        Task<ResponseDto> UpdateProduct(ProductDto model);
        Task<ResponseDto> Delete(ProductDto model);
        Task<List<ProductDto>> GetAll();
    }
}
