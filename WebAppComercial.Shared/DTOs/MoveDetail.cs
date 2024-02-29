
namespace WebAppComercial.Shared.DTOs
{
    public class MoveDetail
    {
        public int ProductId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public double Quantity { get; set; }
        public double PercentageIVA { get; set; }
        public double PercentageDiscount { get; set; }

        public decimal valorBruto { get { return Cost * (decimal)Quantity / (1 + (decimal)PercentageIVA); } }
        public decimal valorIVA { get { return Cost * (decimal)Quantity - valorBruto; } }
        public decimal valorDescuento { get { return valorBruto * (decimal)PercentageDiscount; } }
        public decimal valorNeto { get { return Cost * (decimal)Quantity - valorDescuento; } }
    }
}
