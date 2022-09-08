using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingPrueba.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name="País")]
       [MaxLength(50,ErrorMessage = "Campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage  = "El campo {0} es obligatorio")]
        public String Name { get; set; }

        public ICollection<State> States { get; set; }

        [Display(Name ="Estados")]
        public int StatesNumber => States == null ? 0: States.Count;

      //  public int MyProperty { get; set; }
    }
}
