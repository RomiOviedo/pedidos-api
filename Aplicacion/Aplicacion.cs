using Pedidos.Abstraccion;
using Pedidos.Repositorio;

namespace Pedidos.Aplicacion
{
    public interface IApplication<T> : IDbOperation<T>
    {

    }

    public class Aplicacion<T> : IApplication<T>
    {
        private IRepository<T> _repositorio;



        public Aplicacion(IRepository<T> repositorio)
        {
            _repositorio = repositorio;
        }


        public void Delete(int id)
        {
            try
            {
                _repositorio.Delete(id);

            }
            catch (Exception ex)
            {

                throw new Exception($"Error al borrar la entidad con id {id}", ex);

            }
        }

        public IList<T> GetAll()
        {
            try
            {
                return _repositorio.GetAll();

            }
            catch (Exception ex)
            {

                throw new Exception($"Error al obtener en la lista de entidades", ex);
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _repositorio.GetById(id);

            }
            catch (Exception ex)
            {

                throw new Exception($"Error al obtener la entidad con id{id}", ex);
            }
        }

        public T Save(T entity)
        {
            try
            {
                return _repositorio.Save(entity);

            }
            catch (Exception ex)
            {

                throw new Exception($"Error al guardar en la entidad{entity}", ex);
            }
        }
    }
}
