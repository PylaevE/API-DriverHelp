using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace VehicleParser
{
    internal static class Program
    {
        private const string JsonFileUrl = "https://raw.githubusercontent.com/blanzh/carsBase/master/cars.json";

        private static void Main()
        {
            var jsonString = GetContent(JsonFileUrl);
            var carBaseObject = JsonSerializer.Deserialize<VehicleBase>(jsonString)?.list;
            var db = new u1225541_mainContext();
            using (db)
            {
                if (carBaseObject != null)
                {
                    var brands = carBaseObject.Keys;
                    var brandsFromDb = db.VehicleBrands.Select(vb => vb.Name);
                    foreach (var brand in brands)
                    {
                        var models = carBaseObject[brand];
                        if (!brandsFromDb.Contains(brand))
                        {
                            db.Add(new VehicleBrand
                            {
                                Name = brand
                            });
                            db.SaveChanges();
                        }

                        var brandId = db.VehicleBrands.First(vb => vb.Name == brand).Id;
                        var modelsFromDb = db.VehicleModels
                            .Where(vm => vm.BrandId == brandId)
                            .Select(b => b.Name);
                        foreach (var model in models)
                        {
                            if (!modelsFromDb.Contains(model))
                            {
                                db.VehicleModels.Add(new VehicleModel
                                {
                                    Name = model,
                                    BrandId = brandId
                                });   
                            }
                        }
                        
                    }
                    db.SaveChanges();
                }
            }
        }

        private static string GetContent(string url)
        {
            var request = WebRequest.Create(url);
            request.Proxy = null;
            request.Method = "GET";
            request.Timeout = 360000;
            request.ContentType = "application/x-www-form-urlencoded";

            using var response = request.GetResponse();
            var requestStream = response.GetResponseStream();

            return requestStream == null ? null : new StreamReader(requestStream).ReadToEnd();
        }
    }
}
