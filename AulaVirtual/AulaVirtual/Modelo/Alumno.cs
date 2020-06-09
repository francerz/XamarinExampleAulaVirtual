using Newtonsoft.Json;
using SQLite;

namespace AulaVirtual.Modelo
{
    [Table("alumnos")]
    public class Alumno : ModelBase
    {
        private int id;
        private string numControl;
        private string nombre;
        private string apellido1;
        private string apellido2;

        [PrimaryKey, AutoIncrement, JsonProperty("id_alumno")]
        public int ID {
            get {
                return id;
            }
            set {
                OnPropertyChanging("ID");
                id = value;
                OnPropertyChanged("ID");
            }
        }

        [Ignore, JsonIgnore]
        public bool TieneID {
            get {
                return ID != 0;
            }
        }


        [Unique, JsonProperty("num_control")]
        public string NumControl {
            get {
                return numControl;
            }
            set {
                OnPropertyChanging("NumControl");
                numControl = value;
                OnPropertyChanged("NumControl");
            }
        }

        [JsonProperty("nombre")]
        public string Nombre {
            get {
                return nombre;
            }
            set {
                OnPropertyChanging("Nombre");
                nombre = value;
                OnPropertyChanged("Nombre");
                OnPropertyChanged("NombreCompleto");
            }
        }

        [JsonProperty("apellido1")]
        public string Apellido1 {
            get {
                return apellido1;
            }
            set {
                OnPropertyChanging("Apellido1");
                apellido1 = value;
                OnPropertyChanged("Apellido1");
                OnPropertyChanged("NombreCompleto");
            }
        }

        [JsonProperty("apellido2")]
        public string Apellido2 {
            get {
                return apellido2;
            }
            set {
                OnPropertyChanging("Apellido2");
                apellido2 = value;
                OnPropertyChanged("Apellido2");
                OnPropertyChanged("NombreCompleto");
            }
        }

        [Ignore, JsonIgnore]
        public string NombreCompleto {
            get {
                return string.Format(
                    "{0}{1}{2}",
					Nombre ?? "",
                    Apellido1 == null ? "" : ' ' + Apellido1,
                    Apellido2 == null ? "" : ' ' + Apellido2
                );
			}
		}
    }
}
