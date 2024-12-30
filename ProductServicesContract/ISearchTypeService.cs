using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract
{
    public interface ISearchTypeService
    {
        public List<SearchTypeDto> GetSearchTypeDto();

    }
}
