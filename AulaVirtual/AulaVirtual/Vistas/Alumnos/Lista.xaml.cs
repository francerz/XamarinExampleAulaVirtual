using AulaVirtual.LocalStorage;
using AulaVirtual.Modelo;
using AulaVirtual.WebService;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AulaVirtual.Vistas.Alumnos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {
        private ObservableCollection<Alumno> alumnos;
        public ObservableCollection<Alumno> Alumnos {
            get {
                return alumnos;
            }
            set {
                OnPropertyChanging("Alumnos");
                alumnos = value;
                OnPropertyChanged("Alumnos");
            }
        }

        public Lista()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadContent();
        }

#if APP_WEB_SERVICE
        private async void LoadContent()
        {
            try {
                Alumnos = new ObservableCollection<Alumno>(await ResourceAlumnos.Consultar());
            } catch (Exception ex) {
                await DisplayAlert("ERROR", ex.Message, "Cerrar");
			}
        }
#elif APP_LOCAL_DATABASE
        protected async void LoadContent()
		{
            try {
                Alumnos = new ObservableCollection<Alumno>(await TableAlumnos.Instance.Consultar());
            } catch (Exception ex) {
                await DisplayAlert("ERROR", ex.Message, "Cerrar");
			}
		}
#else
        private void LoadContent()
        {
            Alumnos = new ObservableCollection<Alumno>(DataRamStorage.Alumnos);
		}
#endif
        private void ToolAgregar_Clicked(object sender, EventArgs e)
        {
            Navigation?.PushAsync(new Formulario {
                Title = "Agregar alumno",
                Alumno = new Alumno()
            });
        }
		private void ToolActualizar_Clicked(object sender, EventArgs e)
		{
            LoadContent();
		}

        private void ListAlumnos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var alumno = e.Item as Alumno;
            Navigation?.PushAsync(new Formulario {
                Title = "Modificar alumno",
                Alumno = alumno
            });
        }
	}
}