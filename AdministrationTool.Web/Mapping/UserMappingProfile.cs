using AdministrationTool.Data.Models;
using AdministrationTool.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministrationTool.Web.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(u => u.ManagerPrincipalName, opt => opt.MapFrom(m => m.Manager.PrincipalName))
                .ForMember(u => u.ManagerFullName, opt => opt.MapFrom(m => $"{m.Manager.FirstName} {m.Manager.LastName}"))
                .ReverseMap();
            CreateMap<User, UserModel>()
                .ForMember(u => u.ManagerPrincipalName, opt => opt.MapFrom(m => m.Manager.PrincipalName))
                .ForMember(u => u.ManagerFullName, opt => opt.MapFrom(m => $"{m.Manager.FirstName} {m.Manager.LastName}"))
                .ReverseMap();
        }
    }
}