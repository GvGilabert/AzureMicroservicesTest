using AzureMicroservicesTest.Api.Search.Interfaces;
using AzureMicroservicesTest.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureMicroservicesTest.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService _searchService)
        {
            searchService = _searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm searchTerm)
        {
            var result = await searchService.SearchAsync(searchTerm.OrderId);
            if(result.IsSuccess) 
            {
                return Ok(result.SearchResult);
            }
            return NotFound();
        }
    }
}
