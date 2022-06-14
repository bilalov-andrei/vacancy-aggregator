using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VacancyAggregator.Core;
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
    public class VacanciesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacanciesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vacancy = await _unitOfWork.Vacancy.GetByIdAsync(id, false);

            if (vacancy == null)
                return NotFound();

            return Ok(vacancy);
        }

        [HttpGet]
        [Route("byVacancyFilter/{VacancyFilterId}")]
        public async Task<IActionResult> GetByVacancyFilter([FromRoute] int VacancyFilterId, [FromQuery] VacancyParameters parameters)
        {
            if (VacancyFilterId == 0)
                return BadRequest();

            var vacancies = await _unitOfWork.Vacancy.GetAllByVacancyFilterIdAsync(VacancyFilterId, parameters, false);

            var vacanciesDto = _mapper.Map<IEnumerable<VacancyDto>>(vacancies);

            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(vacancies.MetaData));

            return Ok(vacanciesDto);
        }
    }
}
