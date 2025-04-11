﻿using Fiap.Api.Donation4.Data;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Donation4.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DataContext _dataContext;

        public CategoriaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<CategoriaModel>> FindAllAsync()
        {
            return await _dataContext.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<CategoriaModel> FindByIdAsync(int id)
        {
            var categoria = await _dataContext.Categorias.AsNoTracking().FirstOrDefaultAsync(u => u.CategoriaId == id);

            return categoria;   
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = new CategoriaModel();
            categoria.CategoriaId = id;

             _dataContext.Categorias.Remove(categoria);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<int>InsertAsync(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Add(categoriaModel);
            await _dataContext.SaveChangesAsync();

            return categoriaModel.CategoriaId;
        }

        public async Task UpdateAsync(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Update(categoriaModel);
            await _dataContext.SaveChangesAsync();
        }
    }
}
