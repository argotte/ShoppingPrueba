using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingPrueba.Data.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(50, ErrorMessage = "Campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public String Name { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }

        [Display(Name = "Ciudades")]
        [MaxLength(50, ErrorMessage = "Campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
