using System;
using System.Collections.Generic;
using System.Text;

namespace Product_Management_Shared
{
    public class ErrorContainer
    {
        public ErrorContainer()
        {
            DateCreated = DateTime.Now;
            LoadSetUpBusinessErrors();
            LoadGetEnviromentBusinessErrors();
            LoadBillBusinessErrors();
            LoadGetUnderWrittingErrors();
            LoadGetEnviromentBusinessByIdErrors();
        }

        #region Fields
        private List<ProductManagementError> _setUpBusinessErrors { get; set; }
        private List<ProductManagementError> _getEnviromentBusinessErrors { get; set; }
        private List<ProductManagementError> _billBusinessErrors { get; set; }
        private List<ProductManagementError> _getUnderWrittingErrors { get; set; }
        private List<ProductManagementError> _getEnviromentBusinessByIdErrors { get; set; }
        #endregion

        #region Properties
        public DateTime DateCreated { get; private set; }
        public IReadOnlyList<ProductManagementError> SetUpBusinessErrorList { get => _setUpBusinessErrors.AsReadOnly(); }
        public IReadOnlyList<ProductManagementError> GetEnviromentBusinessErrorList { get => _getEnviromentBusinessErrors.AsReadOnly(); }
        public IReadOnlyList<ProductManagementError> BillBusinessErrorList { get => _billBusinessErrors.AsReadOnly(); }
        public IReadOnlyList<ProductManagementError> GetUnderWrittingErrorList { get => _getUnderWrittingErrors.AsReadOnly(); }
        public IReadOnlyList<ProductManagementError> GetEnviromentBusinessByIdErrorList { get => _getEnviromentBusinessByIdErrors.AsReadOnly(); }
        #endregion

        #region Private helpers 

        #region LoadSetUpBusinessErrorList
        private void LoadSetUpBusinessErrors()
        {
            _setUpBusinessErrors = new List<ProductManagementError>();
            LoadSetUpBusinessStaticValuesToList();
            LoadSetUpBusinessRequiredToList();
            LoadSetupBusinessFormatErrorList();
        }

        private void LoadSetUpBusinessStaticValuesToList()
        {
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.UnderwrittingCaseId, TipoError.StaticValue));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.TransactionId, TipoError.StaticValue));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.TypeOfParticularData, TipoError.StaticValue));
        }

        private void LoadSetUpBusinessRequiredToList()
        {
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.CoverageChangeIndicator, TipoError.Required));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.Selected, TipoError.Required));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.HomeOwner, TipoError.Required));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.Theft, TipoError.Required));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.PhotosRequired, TipoError.Required));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.Required, TipoError.Required));
        }

        private void LoadSetupBusinessFormatErrorList()
        {
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.RF_BirthDate, TipoError.Age));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.EndDate, TipoError.StaticValue));
            _setUpBusinessErrors.Add(new ProductManagementError(SetUpErrorCatalog.PhotosLength, TipoError.LowerOrEqualThan));
        }

        #endregion

        #region LoadGetEnviromentBusinessErrorList
        private void LoadGetEnviromentBusinessErrors()
        {
            _getEnviromentBusinessErrors = new List<ProductManagementError>()
            {
                new ProductManagementError(GetEnviromentBusinessErrorCatalog.firstComponentLevel, TipoError.StaticValue),
                new ProductManagementError(GetEnviromentBusinessErrorCatalog.secondComponentLevel, TipoError.StaticValue),
                new ProductManagementError(GetEnviromentBusinessErrorCatalog.thirdComponentLevel, TipoError.StaticValue),
                new ProductManagementError(GetEnviromentBusinessErrorCatalog.effectiveDate, TipoError.RulesAndFormats)
                //new ProductoCelularColectivoError(GetBusinessErrorCatalog.RF_ticketId, "La venta correspondiente al ticketId informado no existe, o no pudo procesarse", TipoError.RulesAndFormats)
            };
        }
        #endregion

        #region LoadGetEnviromentBusinessByIdErrorList
        private void LoadGetEnviromentBusinessByIdErrors()
        {
            _getEnviromentBusinessByIdErrors = new List<ProductManagementError>()
            {
                new ProductManagementError(GetEnviromentBusinessByIdErrorCatalog.commercialStructureId, TipoError.StaticValue)
            };
        }

        #endregion

        #region LoadBillBusinessErrors
        private void LoadBillBusinessErrors()
        {
            _billBusinessErrors = new List<ProductManagementError>();
            LoadBillBusinessStaticValuesToList();
            LoadRequiredToList();
        }

        private void LoadBillBusinessStaticValuesToList()
        {
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.UnderwrittingCaseId, TipoError.StaticValue));
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.TransactionId, TipoError.StaticValue));
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.TypeOfParticularData, TipoError.StaticValue));
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.EndingDate, TipoError.StaticValue));
        }

        private void LoadRequiredToList()
        {
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.CoverageChangeIndicator, TipoError.Required));
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.Selected, TipoError.Required));
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.HomeOwner, TipoError.Required));
            _billBusinessErrors.Add(new ProductManagementError(BillBusinessErrorCatalog.Theft, TipoError.Required));
        }
        #endregion

        #region LoadGetUnderWrittingErrors
        private void LoadGetUnderWrittingErrors()
        {
            _getUnderWrittingErrors = new List<ProductManagementError>();
        }
        #endregion

        #endregion
    }
}
