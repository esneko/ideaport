using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ideaport.Models
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskViewModel, Task>();
        }
    }
}
