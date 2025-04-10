using Fiap.Api.Donation4.Data;
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

        public IList<CategoriaModel> FindAll()
        {
            return _dataContext.Categorias.AsNoTracking().ToList();
        }

        public CategoriaModel FindById(int id)
        {
            var categoria = _dataContext.Categorias.AsNoTracking().FirstOrDefault(u => u.CategoriaId == id);

            return categoria;
        }

        public void Delete(int id)
        {
            var categoria = new CategoriaModel();
            categoria.CategoriaId = id;

            _dataContext.Categorias.Remove(categoria);
            _dataContext.SaveChanges();
        }

        public int Insert(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Add(categoriaModel);
            _dataContext.SaveChanges();

            return categoriaModel.CategoriaId;
        }

        public void Update(CategoriaModel categoriaModel)
        {
            _dataContext.Categorias.Update(categoriaModel);
            _dataContext.SaveChanges();
        }
    }
}
