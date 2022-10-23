using System;
using Blog21.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog21.Models
{
    public class Post
    {

        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; } = "";

        public string Body { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.Now;

    }
}
