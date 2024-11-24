using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private string usuario = "admin";
        private string password = "ipo1";
        private BitmapImage imagCheck = new BitmapImage(new Uri("/Assets/check.png", UriKind.Relative));
        private BitmapImage imagCross = new BitmapImage(new Uri("/Assets/cross.png", UriKind.Relative));
        MenuPrincipal principal;

        public MainWindow()
        {
            InitializeComponent();

        }


        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            // se hará la comprobación al pulsar el "Enter"
            if (e.Key == Key.Return)
            {
                if (ComprobarEntrada(txtUsuario.Text, usuario, txtUsuario, imgCheckUsuario))
                {
                    // habilitar entrada de contraseña y pasarle el foco
                    passContrasena.IsEnabled = true;
                    passContrasena.Focus();
                    // deshabilitar entrada de login
                    txtUsuario.IsEnabled = false;
                }
            }
        }

        private void passContrasena_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private Boolean ComprobarEntrada(string valorIntroducido, string valorValido,Control componenteEntrada, Image imagenFeedBack)
        {
            Boolean valido = false;
            if (valorIntroducido.Equals(valorValido))
            {
                // borde y background en verde
                componenteEntrada.BorderBrush = Brushes.Green;
                componenteEntrada.Background = Brushes.LightGreen;
                // imagen al lado de la entrada de usuario --> check
                imagenFeedBack.Source = imagCheck;
                valido = true;
            }
            else
            {
                // marcamos borde en rojo
                componenteEntrada.BorderBrush = Brushes.Red;
                componenteEntrada.Background = Brushes.LightCoral;
                // imagen al lado de la entrada de usuario --> cross
                imagenFeedBack.Source = imagCross;
                valido = false;
            }
            return valido;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Comprueba las entradas solo al hacer clic en el botón
                bool usuarioValido = ComprobarEntrada(txtUsuario.Text, usuario, txtUsuario, imgCheckUsuario);
                bool contrasenaValida = ComprobarEntrada(passContrasena.Password, password, passContrasena, imgCheckContrasena);

                if (usuarioValido && contrasenaValida)
                {
                    principal = new MenuPrincipal();
                    principal.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nombre de usuario o contraseña incorrectos", "Error de inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la ventana principal: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
