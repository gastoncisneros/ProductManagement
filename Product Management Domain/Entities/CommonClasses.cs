using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Product_Management_Domain.Entities
{
    #region GetEnviromentBusines

    public class Component_Local
    {
        public int ComponentCode { get; set; }
        public IEnumerable<ComponentCriteria_Local> ComponentCriteria { get; set; }
        public string ComponentDescription { get; set; }
        public int ComponentId { get; set; }
        public string ComponentIdDescription { get; set; }
    }

    public class ComponentCriteria_Local
    {
        public string CriteriaDescription { get; set; }
        public int CriteriaId { get; set; }
    }

    public class Product_Local
    {
        public int BranchCode { get; set; }
        public string BranchDescription { get; set; }
        public IEnumerable<Currency_Local> Currencies { get; set; }
        public IEnumerable<DiscountsTaxesSurcharges_Local> DiscountsTaxesSurcharges { get; set; }
        public IEnumerable<Frequency_Local> Frequencies { get; set; }
        public IEnumerable<Module_Local> Modules { get; set; }
        public int ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public IEnumerable<RequisiteExclusion_Local> RequisiteExclusion { get; set; }

    }

    public class Currency_Local
    {
        public int CurrencyCode { get; set; }
        public string CurrencyDescription { get; set; }
    }
    public class DiscountsTaxesSurcharges_Local
    {
        public string AllowModification { get; set; }
        public string ByDefault { get; set; }
        public string CalculationType { get; set; }
        public int CurrencyCode { get; set; }
        public string CurrencyDescription { get; set; }
        public int DecreaseModPercent { get; set; }
        public string Description { get; set; }
        public int DisexprCode { get; set; }
        public string DisexprDescription { get; set; }
        public int ExtraPremiumPercentage { get; set; }
        public int FixedAmount { get; set; }
        public int IncreaseModPercent { get; set; }
        public string IsRequire { get; set; }
        public string TypeOfItem { get; set; }
    }

    public class Frequency_Local
    {
        public int PaymentFrequencyCode { get; set; }
        public string PaymentFrequencyDescription { get; set; }
    }

    public class Module_Local
    {
        public IEnumerable<CollectionOfEntities_Local> CollectionOfEntities { get; set; }
        public IEnumerable<Coverage_Local> Coverages { get; set; }
        public int ModuleCode { get; set; }
        public string ModuleDefaulti { get; set; }
        public string ModuleDescription { get; set; }
        public string ModuleRequire { get; set; }
    }

    public class CollectionOfEntities_Local
    {
        public int AgreementCode { get; set; }
        public string AgreementCodeDescription { get; set; }
        public int CommercialNumber { get; set; }
        public int CurrencyCode { get; set; }
        public string CurrencyCodeDescription { get; set; }
        public string TypeBankInformation { get; set; }
        public int TypeOfCollection { get; set; }
        public string TypeOfCollectionDescription { get; set; }
    }
    public class AttributeOfRiskTypes_Local
    {
        public int CodeDefinitionType { get; set; }
        public string CodeDefinitionTypeDescription { get; set; }
        public int DataTypeCode { get; set; }
        public string DataTypeCodeDescription { get; set; }
        public string FieldIndicatorEnabledOnSale { get; set; }
        public string RequiredFieldIndicator { get; set; }
        public IEnumerable<ValuesAttribute_Local> ValuesAttributes { get; set; }
    }
    public class ValuesAttribute_Local
    {
        public int CodeOfValue { get; set; }
        public string DescriptionOfValue { get; set; }
    }

    public class Coverage_Local
    {
        public IEnumerable<AttributeOfRiskTypes_Local> AttributeOfRiskTypes { get; set; }
        public int CapMaxim { get; set; }
        public int CapMin { get; set; }
        public int CodeGenericCoverage { get; set; }
        public int CodeRiskType { get; set; }
        public string CodeRiskTypetDescription { get; set; }
        public int CoverageCode { get; set; }
        public int CoverageType { get; set; }
        public string CoverageTypeDescription { get; set; }
        public string Defaulted { get; set; }
        public string Description { get; set; }
        public IEnumerable<Mask_Local> Masks { get; set; }
        public int MaximumAmountOfCapitalForSpecificRisks { get; set; }
        public int MaximumNumberOfRisks { get; set; }
        public int MinimumAmountOfCapitalForSpecificRisks { get; set; }
        public string Required { get; set; }
        public int RoleRelatedCode { get; set; }
        public string RoleRelatedCodeDescription { get; set; }
        public string TypeOfChangesAllowedOnInsuredAmount { get; set; }
    }

    public class Mask_Local
    {
        public int CalculationBase { get; set; }
        public int CalculationValue { get; set; }
        public int CommercialSet { get; set; }
        public string CommercialSetDescription { get; set; }
        public int CurrencyCode { get; set; }
        public string CurrencyCodeDescription { get; set; }
        public int DefaultSumAmount { get; set; }
        public int MinimumSquareMeter { get; set; }
        public int NumberGrouping { get; set; }
        public string NumberGroupingDescription { get; set; }
        public int RoleRelatedCode { get; set; }
        public string RoleRelatedCodeDescription { get; set; }
        public int SumAmountFrom { get; set; }
        public int SumAmountTo { get; set; }
        public int TypeOfCalculation { get; set; }
        public string TypeOfCalculationDescription { get; set; }
    }

    public class RequisiteExclusion_Local
    {
        public int ComponentCode1 { get; set; }
        public string ComponentCode1Description { get; set; }
        public int ComponentCode2 { get; set; }
        public string ComponentCode2Description { get; set; }
        public int DefinitionType { get; set; }
        public string DefinitionTypeDescription { get; set; }
        public int ItemCode1 { get; set; }
        public string ItemCode1Description { get; set; }
        public int ItemCode2 { get; set; }
        public string ItemCode2Description { get; set; }
        public int Role1 { get; set; }
        public string Role1Description { get; set; }
        public int Role2 { get; set; }
        public string Role2Description { get; set; }
        public int TypeRequisiteExclusion { get; set; }
        public string TypeRequisiteExclusionDescription { get; set; }
    }
    #endregion

    #region BillBusines

    public class Billing_Local
    {
        public string BillingDetails => "";
        public DateTime BillingEndDate => DateTime.MinValue;
        public DateTime BillingStartDate => DateTime.MinValue;
        public int BillingTotalCommission => 0;
        public int BillingTotalPremium => 0;
        public string[] Errors => new string[0];
    }

    public class Address_Local
    {
        public int? CityCode { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public int? Floor { get; set; }
        public int? Number { get; set; }
        [Required]
        public int Province { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public int? TypeOfAddress { get; set; }
        public string ZipCode { get; set; }
        public string sRecType { get; set; }
        public int MunicipalityCode { get; set; }
        public int RecordOwner { get; set; }
    }

    public class SpecificRiskByCoverage_Local
    {
        public double nAmountInsured { get; set; }
        public string Remove { get; set; }
        //RiskDetails
        public double RiskItem { get; set; }
    }

    public class CoverageSelected_Local
    {
        public decimal AnnualPremium => 0;
        public string CoverageChangeIndicator { get; set; }

        [Required]
        public int CoverageCode { get; set; }
        public string CoverageDescription { get; set; }

        [Required]
        public decimal InsuredAmount { get; set; }

        [Required]
        public decimal MaximumInsuredAmount { get; set; }
        public decimal MinimumInsuredAmount { get; set; }
        public int ModuleCode { get; set; }
        public string ModuleDescription { get; set; }

        [Required]
        public string Selected { get; set; }
        public IEnumerable<SpecificRiskByCoverage_Local> SpecificObjects { get; set; }
    }

    public class HomeOwner_Local
    {
        public int CriCode { get; set; }
        public string CriCodeDescription { get; set; }
        public int DwellingType { get; set; }
        public string DwellingTypeDescription { get; set; }
        public int ExterConstr { get; set; }
        public string ExterConstrDescription { get; set; }
        public int FoundType { get; set; }
        public string FoundTypeDescription { get; set; }
        public int HomeSuper { get; set; }
        public int LandSuper { get; set; }
        public int OwnerShip { get; set; }
        public string OwnerShipDescription { get; set; }
        public int RoofType { get; set; }
        public string RoofTypeDescription { get; set; }
        public int RoofYear { get; set; }
        public int SquareValue { get; set; }
        public int Stories { get; set; }
        public int YearBuilt { get; set; }
    }

    public class ParticularData_Local
    {
        public HomeOwner_Local HomeOwner { get; set; }
        public int TrasactionId => 1;

        [Required]
        public int TypeOfParticularData { get; set; }
    }

    public class Risk_Local
    {
        public IEnumerable<Address_Local> Address { get; set; }
        public int AgreementCode { get; set; }
        public int CommercialNumber { get; set; }
        public int CommercialSet { get; set; }
        public IEnumerable<CoverageSelected_Local> Coverages { get; set; }
        public IEnumerable<StructCriteriaByUser_Local> Criteria { get; set; }
        public int CurrencyCode { get; set; }
        public IEnumerable<DiscountsTaxesSurchargesByPolicy_Local> DiscountsTaxesSurcharges { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string[] Errors { get; set; }
        public int IdStruct { get; set; }
        public int LineOfBusines { get; set; }
        public string LineOfBusinessDescription { get; set; }
        public ParticularData_Local ParticularData { get; set; }
        public int PaymentFrequency { get; set; }
        public string PaymentFrequencyDescription { get; set; }
        public int ProductCode { get; set; }
        public string ProductCodeDescription { get; set; }
    }

    public class StructCriteriaByUser_Local
    {
        public int ComponentCode { get; set; }
        public string CriteriaByUser { get; set; }
        public string CriteriaDescription { get; set; }
        public int CriteriaId { get; set; }
    }

    public class DiscountsTaxesSurchargesByPolicy_Local
    {
        public string Agree { get; set; }

        public int Amount { get; set; }

        public int CurrencyCode { get; set; }

        public string CurrencyDescription { get; set; }

        public int DisexprCode { get; set; }

        public long Notes { get; set; }

        public double Percent { get; set; }

        public int Reason { get; set; }

        public string ReasonDescription { get; set; }
    }

    public class Person_Local
    {
        public Address_Local Address { get; set; }
        public DateTime BirthDate { get; set; }
        public int CivilStatus { get; set; }
    }

    #endregion
}
