using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ToDoApp.BLL.DTOs.ToDoTask;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BLL.Mapping.Tasks
{
    public class ToDoTaskProfile : Profile
    {
        public ToDoTaskProfile()
        {
            CreateMap<CreateToDoTaskDto, ToDoTask>();
            CreateMap<UpdateToDoTaskDto, ToDoTask>();
            CreateMap<ToDoTask, ToDoTaskDto>();
        }
    }
}
