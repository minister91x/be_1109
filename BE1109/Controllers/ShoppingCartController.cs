using DataAccess.Computer.DBContext;
using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BE1109.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {

        private MyShopDbContext _dbContext;
        private IOrderRepository _orderRepository;

        public ShoppingCartController(MyShopDbContext dbContext, IOrderRepository orderRepository)
        {
            _dbContext = dbContext;
            _orderRepository = orderRepository;
        }

        [HttpPost("OrderInsert")]
        public async Task<ActionResult> OrderInsert(CreateOrderRequestData requestData)
        {
            var returnData = new ReturnData();
            try
            {
                // Bước 00 : Kiểm tra dữ liệu đầu vào trước khi đem đi tính toán


                if (requestData == null
                    || requestData.customer == null
                    || requestData.orderItems == null)
                {
                    return BadRequest();
                }

                // Bước 0 : Kiểm tra xem với số điện thoại từ client truyền lên xem đã có trong db hay chưa 
                var customerId = 0;

                var customer = _dbContext.customer.Where(s => s.CustomerPhoneNumber == requestData.customer.CustomerPhoneNumber)
                     .FirstOrDefault();

                // Nếu mà chưa có thông tin
                if (customer == null || customer.CustomerID <= 0)
                {
                    // Đi tạo khách hàng ( tạo ra CustomerID)

                    customerId = await _orderRepository.Customer_Insert(requestData.customer);
                }
                else
                {
                    //Nếu đã có thông tin 
                    // lấy luôn thông tin CustomerId trong Database Ra 

                    customerId = customer.CustomerID;
                }


                var TotalAmount = 0;

                foreach (var item in requestData.orderItems)
                {
                    var product = _dbContext.product.Where(s => s.ProductID == item.ProductID).FirstOrDefault();
                    if (product != null && product.ProductID > 0)
                    {
                        TotalAmount += product.DonGia * item.Quantity;
                    }
                }


                // Bước 1: Tạo Order

                var orderReq = new Order
                {
                    CustomerID = customerId,
                    TotalAmount = TotalAmount,
                    CreatedDate = DateTime.Now,
                };

                var orderId = await _orderRepository.OrderInsert(orderReq);

                if (orderId > 0)
                {
                    // Bước 2: Tạo Order Detail
                    foreach (var item in requestData.orderItems)
                    {
                        var orderDetail = await _orderRepository.OrderDetailInsert(new OrderDetail
                        {
                            OrderID = orderId,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            CreateAt = DateTime.Now
                        });
                    }
                }

                returnData.Code = 1;
                returnData.Desciption = "Đặt hàng thành công!";

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(returnData);
        }
    }
}
