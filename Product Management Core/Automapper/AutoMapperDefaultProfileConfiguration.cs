using AutoMapper;
using GaliciaSegurosReference;
using Product_Management_Core.DTO;
using Product_Management_Core.DTO.GetEnviromentBusiness;
using Product_Management_Core.DTO.OnDemand;
using Product_Management_Core.DTO.SetupUnderWrittingAndPolicyDoc;
using Product_Management_Domain.Entities;
using System;
using System.Data;
using System.Linq;

namespace Product_Management_Core.Automapper
{
    public class AutoMapperDefaultProfileConfiguration : Profile
    {
        public AutoMapperDefaultProfileConfiguration() : this("Default")
        {
        }
        protected AutoMapperDefaultProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<CommercialStructures, Response> ()
                .ForMember(dest => dest.Components, opt => opt.MapFrom(a => a.Components))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(a => a.Message))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(a => a.Products))
                .ForMember(dest => dest.StructureCode, opt => opt.MapFrom(a => a.StructureCode))
                .ForMember(dest => dest.StructureDescription, opt => opt.MapFrom(a => a.StructureDescription));

            CreateMap<GetPolicyDocRequest, OnDemandRequest>()
            .ForMember(dest => dest.nBranch, opt => opt.MapFrom(a => a.Branch.ToString()))
            .ForMember(dest => dest.nProduct, opt => opt.MapFrom(a => a.Product.ToString()))
            .ForMember(dest => dest.nPolicy, opt => opt.MapFrom(a => a.PolicyId.ToString()))
            .ForMember(dest => dest.nCertif, opt => opt.MapFrom(a => a.CertificateID.ToString()))
            .ForMember(dest => dest.nroDoc, opt => opt.MapFrom(a => a.DocNumber))
            .ForMember(dest => dest.tipoDoc, opt => opt.MapFrom(a => a.TipoDoc))
            .ForMember(dest => dest.nombre, opt => opt.MapFrom(a => a.Nombre))
            .ForMember(dest => dest.apellido, opt => opt.MapFrom(a => a.Apellido))
            .ForMember(dest => dest.email, opt => opt.MapFrom(a => a.Email))
            .ForMember(dest => dest.origen, opt => opt.MapFrom(a => a.Origen))
            .ForMember(dest => dest.sexo, opt => opt.MapFrom(a => a.Sexo));
        }
    }
}
