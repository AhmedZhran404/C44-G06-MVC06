using GymManagmentDAL.Data.GymDBContext;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.DataSeed
{
    public static class GymDataSeeding
    {
        public static bool SeedDate(GymDbContext context)
        {
			

			try
			{
				if(!context.Categories.Any())
				{

					var Categories = LoadDataFromJsonFile<Category>("Category.json");
					context.Categories.AddRange(Categories);

				}
				if(!context.Plans.Any())
				{
                    var Plans = LoadDataFromJsonFile<Plan>("Plans.json");
                    context.Plans.AddRange(Plans);

                }

                return context.SaveChanges() > 0;
			}
			catch (Exception)
			{
				return false;
			}


        }


		private static List<T> LoadDataFromJsonFile<T>(string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DataSeeding" , fileName);

			if(!File.Exists(filePath))
				throw new FileNotFoundException();

			var jsonData = File.ReadAllText(filePath);

			var options = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true,
			};

			options.Converters.Add(new JsonStringEnumConverter());

			return JsonSerializer.Deserialize<List<T>>(jsonData, options);

		}
    }
}
