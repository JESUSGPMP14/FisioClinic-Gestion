using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LABORATORIO
{
    /// <summary>
    /// Lógica de interacción para CrearPaciente.xaml
    /// </summary>
    public partial class CrearPaciente : Window
    {
        public delegate void ObjectCreatedEventHandler(listaPacientes r);
        public event ObjectCreatedEventHandler ObjectCreated;
        public BitmapImage img;
        private BitmapImage imagenOriginal = new BitmapImage(new Uri("pack://application:,,,/Assets/usuario.png"));

        public CrearPaciente()
        {
            InitializeComponent();
        }

        private void volver_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LimpiarCampos();
            RestaurarImagenOriginal();

        }

        private void CrearPaciente2_Click(object sender, RoutedEventArgs e)
        {
            if (CamposCompletos() && ImagenCargada())
            {
                Uri uri = img.UriSource;
                var nuevoPaciente = new listaPacientes(txtNombre1.Text, txtApellidos1.Text, txtDireccion.Text, txtTelefono.Text, txtEdad.Text, txtDni.Text, txtSexo.Text, txtCorreo.Text, uri);
                ObjectCreated?.Invoke(nuevoPaciente);
                MessageBox.Show("Paciente creado con éxito");
                LimpiarCampos();
                RestaurarImagenOriginal();

            }
            else
            {
                MessageBox.Show("Debes rellenar todos los campos y cargar una imagen.");
            }

            /*if ((txtNombre1.Text == "") || (txtApellidos1.Text == "") || (txtDireccion.Text == "") || (txtTelefono.Text == "") || (txtEdad.Text == "") || (txtDni.Text == "") || (txtSexo.Text == "") || (txtCorreo.Text == ""))
            {
                MessageBox.Show("Debes rellenar todos los campos");
            }
            else
            {
                Uri uri = img.UriSource;
                var nuevoPaciente = new listaPacientes(txtNombre1.Text, txtApellidos1.Text, txtDireccion.Text, txtTelefono.Text, txtEdad.Text, txtDni.Text, txtSexo.Text, txtCorreo.Text, uri);
                ObjectCreated.Invoke(nuevoPaciente);
                MessageBox.Show("Paciente creado con éxito");
                this.Hide();
                txtNombre1.Text = "";
                txtApellidos1.Text = "";
                txtDireccion.Text = "";
                txtTelefono.Text = "";
                txtEdad.Text = "";
                txtDni.Text = "";
                txtSexo.Text = "";
                txtCorreo.Text = "";
            }*/
        }
        private bool CamposCompletos()
        {
            return !string.IsNullOrEmpty(txtNombre1.Text)
                && !string.IsNullOrEmpty(txtApellidos1.Text)
                && !string.IsNullOrEmpty(txtDireccion.Text)
                && !string.IsNullOrEmpty(txtTelefono.Text)
                && !string.IsNullOrEmpty(txtEdad.Text)
                && !string.IsNullOrEmpty(txtDni.Text)
                && !string.IsNullOrEmpty(txtSexo.Text)
                && !string.IsNullOrEmpty(txtCorreo.Text);
        }
        private bool ImagenCargada()
        {
            return img != null && img.UriSource != null;
        }
        private void LimpiarCampos()
        {
            txtNombre1.Text = "";
            txtApellidos1.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtEdad.Text = "";
            txtDni.Text = "";
            txtSexo.Text = "";
            txtCorreo.Text = "";
        }
        private void RestaurarImagenOriginal()
        {
            if (imagenOriginal != null)
            {
                img = imagenOriginal;
                FotoNuevoPaciente.Source = imagenOriginal;
            }
        }

        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            var Img = new Microsoft.Win32.OpenFileDialog();
            Img.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";
            if (Img.ShowDialog() == true)
            {
                try
                {
                    var bitmap = new BitmapImage(new Uri(Img.FileName, UriKind.Absolute));
                    FotoNuevoPaciente.Source = bitmap;
                    img = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }
            }
        }
    }
}
