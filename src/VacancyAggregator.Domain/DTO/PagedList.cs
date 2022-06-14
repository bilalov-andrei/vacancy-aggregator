using System;
using System.Collections.Generic;
using System.Linq;

namespace VacancyAggregator.Domain.DTO
{
    public class PagedList<T>: List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            MetaData = new MetaData()
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(Count / (double)pageSize) + 1
            };

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
