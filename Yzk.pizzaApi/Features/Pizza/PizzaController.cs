using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using Yzk.pizzaApi.Db;
using Yzk.pizzaApi.Queries;
using Yzk.share;

namespace Yzk.pizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DapperService _dapperservice;

        public PizzaController()
        {
            _appDbContext = new AppDbContext();
            _dapperservice = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _appDbContext.pizzas.ToListAsync();  
            return Ok(lst);
        }

        [HttpGet("Extra")]
        public async Task<IActionResult> GetExtraAsync()
        {
            var lst = await _appDbContext.pizzaExtras.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Order/{invoiceNo}")]
        public IActionResult GetOrder(string invoiceNo)
        {
            var item = _dapperservice.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>
                (
                    PizzaQuery.PizzaOrderQuery,
                    new { PizzaOrderInvoiceNo = invoiceNo }
                );

            var lst = _dapperservice.Query<PizzaOrderInvoiceDetailModel>
                (
                    PizzaQuery.PizzaOrderDetailQuery,
                    new { PizzaOrderInvoiceNo = invoiceNo }
                );

            var model = new PizzaOrderInvoiceResponse
            {
                Order = item,
                OrderDetail = lst
            };

            return Ok(model);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var itemPizza = await _appDbContext.pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = itemPizza.Price;

            if (orderRequest.Extras.Length > 0)
            {
                // select * from Tbl_PizzaExtras where PizzaExtraId in (1,2,3,4)
                //foreach (var item in orderRequest.Extras)
                //{
                //}

                var lstExtra = await _appDbContext.pizzaExtras.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };
            List<PizzaOrderDetailModel> pizzaExtraModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceNo = invoiceNo,
            }).ToList();

            await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
            await _appDbContext.SaveChangesAsync();

            OrderResponse response = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for your order! Enjoy your pizza!",
                TotalAmount = total,
            };

            return Ok(response);
        }
    }
}
