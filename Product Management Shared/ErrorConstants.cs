using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Shared
{
    public static class ErrorMessages
    {
        public const string RequiredErrorMessage = "El campo '{0}' es obligatorio";
        public const string RequiredConditionalMessage = RequiredErrorMessage + " {1}";
        public const string LongitudeErrorMessage = "El campo '{0}' debe tener longitud {1}";
        public const string StaticValueErrorMessage = "El campo '{0}' tiene un valor incorrecto. Debe ser '{1}'";
        public const string FormatErrorMessage = "El campo '{0}' tiene un formato incorrecto. {1}";
        public const string UserIsUnderThan18Message = "Ningun usuario puede ser menor de 18 años";
    }

    public enum TipoError
    {
        Required,
        RequiredConditional,
        LowerOrEqualThan,
        LowerThan,
        Exact,
        Range,
        HigherThan,
        HigherOrEqualThan,
        Funcional,
        StaticValue,
        RulesAndFormats,
        ESB,
        Unknown,
        Age
    }

    public enum SetUpErrorCatalog
    {
        not_identified = 0,
        UnderwrittingCaseId,
        TransactionId,
        CoverageChangeIndicator,
        Selected,
        TypeOfParticularData,
        HomeOwner,
        Theft,
        RF_BirthDate,
        EndDate,
        PhotosRequired,
        PhotosLength,
        Required
    }

    public enum GetEnviromentBusinessErrorCatalog
    {
        effectiveDate = 0,
        firstComponentLevel,
        secondComponentLevel,
        thirdComponentLevel
    }

    public enum GetEnviromentBusinessByIdErrorCatalog
    {
        commercialStructureId = 0
    }

    public enum BillBusinessErrorCatalog
    {
        UnderwrittingCaseId = 0,
        TransactionId,
        CoverageChangeIndicator,
        Selected,
        TypeOfParticularData,
        HomeOwner,
        Theft,
        EndingDate
    }

    public enum GetUnderWritingErrorCatalog
    {
    }
}
