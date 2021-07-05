using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Shared
{
    /// <summary>
    /// Contiene los errores de catalogo de ProductoCelularColectivo
    /// </summary>
    public class ProductManagementError
    {
        public ProductManagementError()
        {
            TipoError = TipoError.Unknown;
            Id = (int)SetUpErrorCatalog.not_identified;
            Description = "Error no identificado";
        }
        public ProductManagementError(SetUpErrorCatalog error, string description, TipoError tipoError)
        {
            Id = (int)error;
            Description = description;
            TipoError = tipoError;
        }

        public ProductManagementError(SetUpErrorCatalog error, TipoError tipoError)
        {
            Id = (int)error;
            TipoError = tipoError;
            SetDescripcion(TipoError);
        }

        public ProductManagementError(GetEnviromentBusinessErrorCatalog error, string description, TipoError tipoError)
        {
            Id = (int)error;
            Description = description;
            TipoError = tipoError;
        }

        public ProductManagementError(GetEnviromentBusinessErrorCatalog error, TipoError tipoError)
        {
            Id = (int)error;
            SetDescripcion(tipoError);
            TipoError = tipoError;
        }

        public ProductManagementError(BillBusinessErrorCatalog error, string description, TipoError tipoError)
        {
            Id = (int)error;
            Description = description;
            TipoError = tipoError;
        }

        public ProductManagementError(BillBusinessErrorCatalog error, TipoError tipoError)
        {
            Id = (int)error;
            SetDescripcion(tipoError);
            TipoError = tipoError;
        }

        public ProductManagementError(GetEnviromentBusinessByIdErrorCatalog error, string description, TipoError tipoError)
        {
            Id = (int)error;
            Description = description;
            TipoError = tipoError;
        }

        public ProductManagementError(GetEnviromentBusinessByIdErrorCatalog error, TipoError tipoError)
        {
            Id = (int)error;
            SetDescripcion(tipoError);
            TipoError = tipoError;
        }

        public int Id { get; protected internal set; }
        public string Description { get; protected internal set; }

        [JsonIgnore]
        public TipoError TipoError { get; protected internal set; }

        private void SetDescripcion(TipoError tipoError)
        {
            switch (tipoError)
            {
                case TipoError.Required:
                    Description = ErrorMessages.RequiredErrorMessage;
                    break;

                case TipoError.RequiredConditional:
                    Description = ErrorMessages.RequiredConditionalMessage;
                    break;

                case TipoError.Range:
                case TipoError.LowerOrEqualThan:
                case TipoError.Exact:
                case TipoError.LowerThan:
                case TipoError.HigherOrEqualThan:
                case TipoError.HigherThan:
                    Description = ErrorMessages.LongitudeErrorMessage;
                    break;

                case TipoError.StaticValue:
                    Description = ErrorMessages.StaticValueErrorMessage;
                    break;
                case TipoError.RulesAndFormats:
                    Description = ErrorMessages.FormatErrorMessage;
                    break;

                case TipoError.Age:
                    Description = ErrorMessages.UserIsUnderThan18Message;
                    break;
                default:
                    Description = "Not found";
                    break;
            }
        }
    }
}
