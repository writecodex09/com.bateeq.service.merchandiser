﻿using Com.Bateeq.Service.Merchandiser.Lib.ViewModels;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Merchandiser.Lib.Services
{
    public class SearchByCounterService
    {
        private CostCalculationRetailService RetailService;
        private IQueryable Query;

        public SearchByCounterService(CostCalculationRetailService retailService)
        {
            this.RetailService = retailService;
        }
        public async Task<Object> ReadModelByCounter(string countername)
        {
            Query = RetailService
                   .DbContext
                   .CostCalculationRetails
                   .Where(retail => retail.CounterName.Contains(countername) && retail._IsDeleted == false)
                   .Select(retail => new ArticleCounterViewModel
                   {
                       name = retail.CounterName
                   })
                   .GroupBy(x => x.name)
                   .Select(x => x.First());
            
            var result = await Query.ToDynamicListAsync();
            return await Task.FromResult(result);
        }
     
    }
}
