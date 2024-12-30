using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using ServicesContract;

namespace Services
{
    public class SearchTypeService : ISearchTypeService
    {
        private List<SearchTypeDto> _searchTypes;

        public SearchTypeService()
        {
            _searchTypes = new List<SearchTypeDto>();
            _searchTypes.Add(new SearchTypeDto
            {
                Text = "جست و جو بر اساس نام",
                Value = "Name"
            });
            _searchTypes.Add(new SearchTypeDto
            {
                Text = "جست و جو بر اساس توضیحات",
                Value = "Description"
            });
            _searchTypes.Add(new SearchTypeDto
            {
                Text = "جست و جو بر اساس شعبه",
                Value = "BranchName"
            });

        }

        public List<SearchTypeDto> GetSearchTypeDto()
        {
            return _searchTypes;
        }
    }
}
