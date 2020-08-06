using Microsoft.Extensions.DependencyInjection;
using DataAccess.SqlEf;

namespace DataAccess
{
  public static class Configuration
  {
    public static void AddSqlEfDal(this IServiceCollection services)
    {
      services.AddScoped<ICategoryDal, CategoryDal>();
      services.AddScoped<IOrderDal, OrderDal>();
      services.AddScoped<IOrderLineItemDal, OrderLineItemDal>();
      services.AddScoped<IOrderLineItemPersonDal, OrderLineItemPersonDal>();
      services.AddScoped<IPersonDal, PersonDal>();
      services.AddScoped<ISkillDal, SkillDal>();
    }
  }
}
