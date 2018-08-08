using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FNZ.Share.BindingModels;
using FNZ.Share.Models;
using FNZ.Share.ModelsDto;

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
            CreateMap<Post, PostBindingModel>();
            CreateMap<PostBindingModel, Post>()
                .ForMember(dest => dest.AddedAt, opt => opt.UseValue(true));
                //.ForMember(dest => dest.AddedAt, opt => opt.Ignore());
            //CreateMap<Post, Post>()
            //    .ForMember(dest => dest.AddedAt, opt => opt.Ignore())
            //    .AfterMap((src, dest) => dest.AddedAt = src.AddedAt);
        }
    }
}
