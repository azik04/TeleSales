using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleSales.DataProvider.Context;

namespace TeleSales.Core.Helpers;

//public class ResetExcludedCallsHelpers :BackgroundService
//{
//    private readonly IServiceScopeFactory _scopeFactory;

//    public ResetExcludedCallsHelpers(IServiceScopeFactory scopeFactory)
//    {
//        _scopeFactory = scopeFactory;
//    }

//    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    //{
//    //    while (!stoppingToken.IsCancellationRequested)
//    //    {
//    //        using var scope = _scopeFactory.CreateScope();
//    //        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    //        var callsToReset = await dbContext.Calls
//    //            .Where(c => c.IsExcluded && c.LastStatusUpdate <= DateTime.Now.AddDays(-7))
//    //            .ToListAsync(stoppingToken);

//    //        if (callsToReset.Any())
//    //        {
//    //            foreach (var call in callsToReset)
//    //            {
//    //                call.IsExcluded = false;
//    //            }

//    //            await dbContext.SaveChangesAsync(stoppingToken);
//    //        }

//    //        await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
//    //    }
//    //}
//}
