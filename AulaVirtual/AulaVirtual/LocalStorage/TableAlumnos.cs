using AulaVirtual.Modelo;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AulaVirtual.LocalStorage
{
	public class TableAlumnos
	{
		private static TableAlumnos instance;
		public static TableAlumnos Instance {
			get {
				return instance ?? (instance = new TableAlumnos());
			}
		}

		private SQLiteAsyncConnection connection;
		private TableAlumnos()
		{
			connection = DataSource.Connection;
			connection.CreateTableAsync<Alumno>().Wait();
		}


		public Task<List<Alumno>> Consultar()
		{
			return connection.Table<Alumno>().ToListAsync();
		}
		public Alumno ConsultarPorId()
		{
			throw new NotImplementedException();
		}
		public Task<int> Insertar(Alumno alumno)
		{
			return connection.InsertAsync(alumno);
		}
		public Task<int> Actualizar(Alumno alumno)
		{
			return connection.UpdateAsync(alumno);
		}
		public Task<int> Eliminar(Alumno alumno)
		{
			return connection.DeleteAsync(alumno);
		}
	}
}
