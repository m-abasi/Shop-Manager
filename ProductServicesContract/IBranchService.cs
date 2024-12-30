using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServicesContract
{
    public interface IBranchService
    {
        public List<BranchDto> GetBranches();
        public BranchDto GetBranchById(int branchId);
        public ResponseDto UpdateBranch(BranchDto model);
        public ResponseDto AddBranch(BranchDto model);
        public ResponseDto DeleteBranch(BranchDto model);
        public List<ProductDto> GetProducts(int branchId);
    }
}
