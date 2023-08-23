﻿using BulkyBookWeb.Repository.Models;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IProductImageRepository ProductImage { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }   
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        OperationResult Save();
    }
}
