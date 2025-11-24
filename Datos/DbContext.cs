using Microsoft.EntityFrameworkCore;
using Pedidos.Abstraccion;

namespace Pedidos.Datos
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntidad
    {

        DbSet<T> _Items;
        DbDataAcces _ctx;

        public DbContext(DbDataAcces ctx)
        {
            _ctx = ctx;
            _Items = _ctx.Set<T>();
        }


        public void Delete(int id)
        {
            try
            {
                var entity = _Items.FirstOrDefault(i => i.Id == id);

                if (entity != null)
                {
                    _Items.Remove(entity);
                }
                _ctx.SaveChanges();

                if (entity == null)
                {
                    throw new KeyNotFoundException($"No se encontro, la entidad con ID:{id}");
                }


            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al eliminar la entidad en la base de datos", dbEx);
            }

            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error al eliminar la entidad", ex);
            }
        }

        public IList<T> GetAll()
        {
            try
            {
                return _Items.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error al obtener la lista de entidades", ex);
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _Items.FirstOrDefault(i => i.Id == id);

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error al buscar la entidad", ex);

            }
        }

        public T Save(T entity)
        {
            try
            {
                if (entity.Id.Equals(0))
                {
                    _Items.Add(entity);

                }
                else
                {
                    var entityDbContext = GetById(entity.Id);

                    if (entityDbContext == null)
                    {
                        throw new KeyNotFoundException($"No se encontro, la entidad con ID:{entity.Id}");
                    }

                    _ctx.Entry(entityDbContext).State = EntityState.Detached;//La “desconecta” del contexto con EntityState.Detached,
                    _Items.Update(entity);
                }
                _ctx.SaveChanges();
                return entity;

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Error al guardar en la base de datos", dbEx);
            }

            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error al guardar la entidad", ex);
            }    
        }
    }
}
