using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Enums;

namespace ToDoApp.BLL.DTOs.ToDoTask
{
    public class UpdateToDoTaskDto : CreateToDoTaskDto
    {
        public int Id { get; set; }

        public Status Status { get; set; }
   
    }
}
