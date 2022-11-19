using System;
using Microsoft.AspNetCore.Http;
//using PhotoSauce.MagicScaler;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;



namespace Blog21.Data.FileManager
{
    public class FileManager : IFileManager
    {

        private string _imagePath;

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];

        }
           public FileStream ImageStream(string image)
           {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
            //return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);

        }
       /* try {
                _fileManager.ImageStream(fileName);
         } catch (FileNotFoundException excep) {
              Console.WriteLine("error, you messed up");
         }*/

public async Task<string> SaveImage(IFormFile image)
        {
           try
            {//start of try

                var save_path = Path.Combine(_imagePath);

                 if(!Directory.Exists(save_path))
                  {
                      Directory.CreateDirectory(save_path);
                   
                   }

            //Internet Explorer Error C:/User/Foo/image.jpg
            //var fileName = image.FileName;
            var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyy-HH-mm-ss")}{mime}";

              using (var fileStream = new FileStream(Path.Combine(save_path,fileName), FileMode.Create))
               {
                await image.CopyToAsync(fileStream);
               }
                return fileName;


            }//end of try
            catch(Exception e)
            { 

                Console.WriteLine(e.Message);
                return "Error";

            }//end of catch

        }




    }
}
