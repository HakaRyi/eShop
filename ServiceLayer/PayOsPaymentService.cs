using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Errors;
using Net.payOS.Types;
using ServiceLayer.Settings;

namespace ServiceLayer.Services
{
    public class PayOsPaymentService
    {
        private readonly PayOS _payOs;

        public PayOsPaymentService(IOptions<PayOsSettings> settings)
        {
            var config = settings.Value;
            _payOs = new PayOS(config.ClientId, config.ApiKey, config.ChecksumKey);
        }

        public async Task<string> CreatePaymentUrlAsync(int orderId, decimal amount, string returnUrl)
        {
            var orderCodeLong = GenerateOrderCode();

            var itemList = new List<ItemData>
    {
        new ItemData("Order Payment", 1, (int)amount)
    };

            var paymentData = new PaymentData(
                orderCode: orderCodeLong,
                amount: (int)amount,
                description: $"Thanh toán đơn hàng #{orderId}",
                items: itemList,
                cancelUrl: returnUrl,
                returnUrl: $"{returnUrl}?orderId={orderId}&amount={(int)amount}"
            );

            try
            {
                var result = await _payOs.createPaymentLink(paymentData);
                return result.checkoutUrl;
            }
            catch (PayOSError ex)
            {
                throw new ApplicationException("Lỗi khi tạo link thanh toán PayOS: " + ex.Message);
            }
        }
        private long GenerateOrderCode()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(); // 13 chữ số
            var random = new Random().Next(100, 999); // 3 chữ số ngẫu nhiên
            return long.Parse($"{timestamp}{random}"); // 16 chữ số
        }

    }
}
