using AulaVirtual.LocalStorage;
using AulaVirtual.Modelo;
using AulaVirtual.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AulaVirtual.Vistas.Alumnos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        private Alumno alumno;

        public Alumno Alumno {
            get {
                return alumno;
            }
            set {
                OnPropertyChanging("Alumno");
                alumno = value;
                OnPropertyChanged("Alumno");
            }
        }
        public Formulario()
        {
            InitializeComponent();
            BindingContext = this;
        }

#if APP_WEB_SERVICE
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (Alumno == null) {
                return;
            }
            try {
                if (!Alumno.TieneID) {
                    ResourceAlumnos.Insertar(Alumno).Wait();
                } else {
                    ResourceAlumnos.Actualizar(Alumno).Wait();
                }
                await Navigation?.PopAsync();
            } catch (Exception ex) {
                await DisplayAlert("ERROR", ex.Message, "Cerrar");
            }
        }

		private async void BtnEliminar_Clicked(object sender, EventArgs e)
		{
            if (Alumno == null) {
                return;
			}
            try {
                ResourceAlumnos.Eliminar(Alumno).Wait();
                await Navigation?.PopAsync();
			} catch (Exception ex) {
                await DisplayAlert("ERROR", ex.Message, "Cerrar");
			}
		}
#elif APP_LOCAL_DATABASE
		private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (Alumno == null) return;

            try {
                if (!Alumno.TieneID) {
                    TableAlumnos.Instance.Insertar(Alumno).Wait();
                } else {
                    TableAlumnos.Instance.Actualizar(Alumno).Wait();
                }
                await Navigation?.PopAsync();
			} catch (Exception ex) {
                await DisplayAlert("ERROR", ex.Message, "Cerrar");
			}
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
		{
            if (Alumno == null) return;

            try {
                TableAlumnos.Instance.Eliminar(Alumno).Wait();
                await Navigation?.PopAsync();
			} catch (Exception ex) {
                await DisplayAlert("ERROR", ex.Message, "Cerrar");
			}
		}
#else
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (Alumno == null) return;

            if (!DataRamStorage.Alumnos.Contains(Alumno)) {
                DataRamStorage.Alumnos.Add(Alumno);
            }
            await Navigation?.PopAsync();
        }
        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            if (Alumno == null) return;

            DataRamStorage.Alumnos.Remove(Alumno);
            await Navigation?.PopAsync();
        }
#endif
	}
}