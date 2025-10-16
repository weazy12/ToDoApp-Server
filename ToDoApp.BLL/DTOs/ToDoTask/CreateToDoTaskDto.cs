using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Enums;

namespace ToDoApp.BLL.DTOs.ToDoTask
{
    public class CreateToDoTaskDto
    {
        public string Title { get; set; } = null!;  

        public string? Description { get; set; }


        public DateTime DueDate { get; set; }

    }
}
