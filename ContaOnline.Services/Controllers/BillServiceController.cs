using Microsoft.AspNetCore.Mvc;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBill.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillServiceController : ControllerBase
    {
        private readonly IBillRepository _billRepository;

        public BillServiceController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [HttpGet]
        public IEnumerable<BillListItem> Get()
        {
            var list = _billRepository.GetByUser("5d8eb740-c8b0-4bb3-bd76-1acfb5cf64c7");

            return list;
        }
    }
}
