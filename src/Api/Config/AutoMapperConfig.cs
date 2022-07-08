﻿using Api.V1.ViewModels;
using Api.V2.ViewModels;
using AutoMapper;
using Business.Models;

namespace Api.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
            CreateMap<StockTransaction, StockTransactionViewModel>().ReverseMap();
        }
    }
}
