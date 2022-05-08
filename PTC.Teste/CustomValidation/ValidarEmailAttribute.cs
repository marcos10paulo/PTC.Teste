using PTC.Teste.Common.Interface;
using PTC.Teste.Common.Services;
using System.ComponentModel.DataAnnotations;

namespace PTC.Teste.CustomValidation
{
    public class ValidarEmailAttribute : ValidationAttribute
    {
        private readonly IFunctions functions = new Functions();

        public ValidarEmailAttribute()
        {

        }
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            bool valido = functions.ValidarEmail(value.ToString());
            return valido;
        }
    }   
}
