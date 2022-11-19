using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog21.Data.FileManager
{
    public interface IFileManager
    {

        FileStream ImageStream(string image);
        Task<string> SaveImage(IFormFile image);



    }
}
