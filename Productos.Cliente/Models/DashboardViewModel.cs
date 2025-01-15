namespace Productos.Cliente.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<RolViewModel> Roles { get; set; }
        public IEnumerable<ProductoViewModel> Productos { get; set; }
    }
}