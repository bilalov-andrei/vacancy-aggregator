using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VacancyAggregator.Data;
using VacancyAggregator.Domain.DTO;
using VacancyAggregator.Domain.Interfaces;
using VacancyAggregator.Domain.Models;
using VacancyAggregator.Domain.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacancyAggregator.WebUI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VacancyFiltersController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacancyFiltersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var VacancyFilters = await _unitOfWork.VacancyFilter.GetAllFiltersAsync(false);

            var VacancyFiltersDto = _mapper.Map<List<VacancyFilterDto>>(VacancyFilters);

            return Ok(VacancyFiltersDto);
        }

    }
}
