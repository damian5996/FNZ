using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FNZ.Share.BindingModels;
using FNZ.Share.Models;

namespace FNZ.BL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMappings();
        }

        public void CreateMappings()
        {
            CreateMap<Post, AddPostBindingModel>();
            CreateMap<AddPostBindingModel, Post>()
                .ForMember(dest => dest.AddedAt, opt => opt.Ignore());
        }
    }
}
