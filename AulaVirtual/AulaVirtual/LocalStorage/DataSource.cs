using SQLite;
using System;
using System.IO;

namespace AulaVirtual.LocalStorage
{
	public static class DataSource
	{
		public static string DatabaseFile { get; } = "datos.db3";

		public static string DatabasePath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFile);

		public static SQLiteAsyncConnection Connection {
			get {
				return new SQLiteAsyncConnection(DatabasePath);
			}
		}
	}
}
