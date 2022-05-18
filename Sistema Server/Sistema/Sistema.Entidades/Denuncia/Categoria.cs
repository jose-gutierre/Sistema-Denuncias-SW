using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Denuncia
{
   public class Categoria
    {
        public int iddenuncia { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string apellido { get; set; }
        public string cargo { get; set; }
        public string entidad { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string lugar { get; set; }
        public string descripcion { get; set; }

    }
}
