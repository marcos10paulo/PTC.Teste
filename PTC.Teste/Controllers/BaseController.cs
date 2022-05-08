using PTC.Teste.Common.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PTC.Teste.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IRequest _request;
        protected readonly IFunctions _functions;
        protected readonly int _usuarioId;
        

        public BaseController(IRequest request,
            IFunctions functions, IHttpContextAccessor contextAccessor)
        {
            _request = request;
            _functions = functions;

            if (contextAccessor.HttpContext.User != null)
            {
                if (contextAccessor.HttpContext.User.FindFirst("Id") != null)
                {
                    _usuarioId = int.Parse(contextAccessor.HttpContext.User.FindFirst("Id").Value);
                }
            }
        }

        protected string GerarCondicaoFiltro<T>(string filtro)
        {
            string condicao = "";
            foreach (var prop in typeof(T).GetProperties())
            {
                bool? autoGeneratorFilter = null;
                object[] obj = prop.GetCustomAttributes(true);

                if (obj.Length > 0)
                {
                    if (prop.GetCustomAttributes(true)[0] is System.ComponentModel.DataAnnotations.DisplayAttribute)
                    {
                        autoGeneratorFilter = ((System.ComponentModel.DataAnnotations.DisplayAttribute)prop.GetCustomAttributes(true)[0]).GetAutoGenerateFilter();
                    }
                }

                if (autoGeneratorFilter == null || autoGeneratorFilter.Value)
                {
                    if (prop.PropertyType != typeof(bool))
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            if (string.IsNullOrWhiteSpace(condicao))
                                condicao += _functions.ConfigureStringCondition(filtro, prop.Name);
                            else
                                condicao += " or " + _functions.ConfigureStringCondition(filtro, prop.Name);
                        }
                        else if ((prop.PropertyType == typeof(double)) || (prop.PropertyType == typeof(double?)))
                        {
                            if (_functions.IsDouble(filtro))
                            {
                                if (string.IsNullOrWhiteSpace(condicao))
                                    condicao += string.Format("({0} == {1})", prop.Name, _functions.ComaToPoint(filtro));
                                else
                                    condicao += " or " + string.Format("({0} == {1})", prop.Name, _functions.ComaToPoint(filtro));
                            }
                        }
                        else if (prop.PropertyType == typeof(DateTime))
                        {
                            if (_functions.IsDateTime(filtro))
                            {
                                if (string.IsNullOrWhiteSpace(condicao))
                                    condicao += _functions.ConfigureDateCondition(filtro, prop.Name);
                                else
                                    condicao += " or " + _functions.ConfigureDateCondition(filtro, prop.Name);
                            }
                        }
                        else if (prop.PropertyType == typeof(DateTime?))
                        {
                            if (_functions.IsDateTime(filtro))
                            {
                                if (string.IsNullOrWhiteSpace(condicao))
                                    condicao += _functions.ConfigureDateCondition(filtro, prop.Name + ".Value");
                                else
                                    condicao += " or " + _functions.ConfigureDateCondition(filtro, prop.Name + ".Value");
                            }
                        }
                        else if (prop.PropertyType == typeof(TimeSpan))
                        {
                            if (_functions.IsTimeSpan(filtro))
                            {
                                if (string.IsNullOrWhiteSpace(condicao))
                                    condicao += _functions.ConfigureTimeCondition(filtro, prop.Name);
                                else
                                    condicao += " or " + _functions.ConfigureDateCondition(filtro, prop.Name);
                            }
                        }
                        else if (prop.PropertyType == typeof(TimeSpan?))
                        {
                            if (_functions.IsTimeSpan(filtro))
                            {
                                if (string.IsNullOrWhiteSpace(condicao))
                                    condicao += _functions.ConfigureTimeCondition(filtro, prop.Name + ".Value");
                                else
                                    condicao += " or " + _functions.ConfigureDateCondition(filtro, prop.Name + ".Value");
                            }
                        }
                        else if ((prop.PropertyType == typeof(int)) || (prop.PropertyType == typeof(int?)))
                        {
                            if (_functions.IsNumberInt32(filtro))
                                //{
                                if (string.IsNullOrWhiteSpace(condicao))
                                    condicao += string.Format("({0} == {1})", prop.Name, _functions.ComaToPoint(filtro));
                                else
                                    condicao += " or " + string.Format("({0} == {1})", prop.Name, _functions.ComaToPoint(filtro));
                        }
                        else if (prop.PropertyType.IsEnum)
                        {
                            try
                            {
                                var value = Enum.Parse(prop.PropertyType, filtro);
                                if (value != null)
                                {
                                    if (string.IsNullOrWhiteSpace(condicao))
                                        condicao += string.Format("({0} == {1})", prop.Name, _functions.ComaToPoint(((int)value).ToString()));
                                    else
                                    {
                                        condicao += " or " + string.Format("({0} == {1})", prop.Name, _functions.ComaToPoint(((int)value).ToString()));
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }

            if (condicao != "")
                condicao = "(" + condicao + ")";

            return condicao;
        }

        //protected T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        //{
        //    var attrType = typeof(T);
        //    var property = instance.GetType().GetProperty(propertyName);
        //    return (T)property.GetCustomAttributes(attrType, false).First();
        //}

        protected string MostarLista(List<int> selecionadas)
        {
            string lista = "";
            if (selecionadas != null)
            {
                if (selecionadas.Count > 0)
                {
                    bool first = true;
                    foreach (var item in selecionadas)
                    {
                        if (!first)
                        {
                            lista += "|";
                        }

                        lista += item;

                        first = false;
                    }
                }
            }

            return lista;
        }
    }
}
