using PTC.Teste.Common.Interface;
using PTC.Teste.Common.Services;
using System.ComponentModel.DataAnnotations;

namespace PTC.Teste.CustomValidation
{
    public class ValidarCPFAttribute : ValidationAttribute
    {
        private readonly IFunctions functions = new Functions();

        public ValidarCPFAttribute() 
        {          

        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;            

            bool valido = functions.ValidaCPF(value.ToString());
            return valido;
        }
    }
}
