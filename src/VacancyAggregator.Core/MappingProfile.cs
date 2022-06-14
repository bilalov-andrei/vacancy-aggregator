using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using System;
using VacancyAggregator.Domain.DTO;
using VacancyAggregator.Domain.Models;
using VacancySourceApi = VacancyAggregator.VacancySources.Api;

namespace VacancyAggregator.Core
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<DataSource, DataSourceDto>();

            CreateMap<Vacancy, VacancyDto>()
               .ForMember(x => x.Url, opt => opt.MapFrom(x => x.ExternalUrl))
               .ForMember(x => x.Experience, opt => opt.MapFrom(source => Enum.GetName(typeof(ExperienceType), source.Experience)))
               .ForMember(x => x.Status, opt => opt.MapFrom(source => Enum.GetName(typeof(VacancyStatus), source.Status)));

            CreateMap<Salary, SalaryDto>()
               .ForMember(x => x.Currency, opt => opt.MapFrom(source => Enum.GetName(typeof(Currency), source.Currency)));

            CreateMap<VacancyFilter, VacancyFilterDto>();

            CreateMap<VacancySourceApi.Currency, Currency>()
            .ConvertUsingEnumMapping(opt => opt.MapByName())
            .ReverseMap();

            CreateMap<VacancySourceApi.ExperienceType, ExperienceType>()
            .ConvertUsingEnumMapping(opt => opt.MapByName())
            .ReverseMap();

            CreateMap<VacancySourceApi.Schedule, Schedule>()
            .ConvertUsingEnumMapping(opt => opt.MapByName())
            .ReverseMap();

            CreateMap<VacancySourceApi.VacancyStatus, VacancyStatus>()
            .ConvertUsingEnumMapping(opt => opt.MapByName())
            .ReverseMap();

            CreateMap<VacancySourceApi.Salary, Salary>().ReverseMap();

            CreateMap<VacancySourceApi.VacancyFilter, VacancyAggregator.Domain.Models.VacancyFilter>();
            CreateMap<VacancyFilter, VacancySourceApi.VacancyFilter>();

            CreateMap<VacancySourceApi.Vacancy, Vacancy>()
               .ForMember(destination => destination.Salary, opt => opt.NullSubstitute(new VacancySourceApi.Salary()))
               .ForMember(d => d.Description, opt => opt.NullSubstitute(""))
               .ForMember(d => d.Description, opt => opt.MapFrom(x => x.Description.Length > 3000 ? x.Description.Substring(0, 3000) : x.Description));

        }
    }
}
