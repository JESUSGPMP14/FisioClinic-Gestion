using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;

namespace LABORATORIO
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        List<listaPacientes> listPacientes;
        List<listaCitas> listCitas;
        List<listaDolencias> listDolencias;
        List<listaPersonalSanitario> listPersonalSanitario;
        List<listaPersonalLimpieza> listPersonalLimpieza;
        private string nombre = "Javier";
        private string apellidos = "Flores Mayorga";
        private string dni = "06236258J";
        private string fecha = string.Format(DateTime.Now.ToString("dd/MM/yy"));
        public delegate void ObjectCreatedEventHandler(listaPacientes r);
        public event ObjectCreatedEventHandler ObjectCreated;
        public BitmapImage img;
        private BitmapImage imagenOriginal = new BitmapImage(new Uri("pack://application:,,,/Assets/usuario.png"));
        CrearPaciente crearPacienteNuevo;

        public MenuPrincipal()
        {
            InitializeComponent();
            txtNombre.Text = nombre;
            txtApellidos.Text = apellidos;
            txtDNI.Text = dni;
            txtHora.Text = fecha;

            // Crear el listado de Pacientes
            listPacientes = new List<listaPacientes>();
            listPersonalSanitario = new List<listaPersonalSanitario>();
            listPersonalLimpieza = new List<listaPersonalLimpieza>();
            // Se cargarán los datos de prueba de un fichero XML
            listPacientes = CargarContenidoXML();
            listPersonalSanitario = CargarContenidoXML2();
            listPersonalLimpieza = CargarContenidoXML3();
            // Indicar que el origen de datos del ListBox 
            lstListaPacientes.ItemsSource = listPacientes;
            lstListaPersonal.ItemsSource = listPersonalSanitario;
            lstListaPersonalLimpieza.ItemsSource = listPersonalLimpieza;
            listCitas = new List<listaCitas>();
            listCitas = CargarContenido();
            lstHistorialPacientes.ItemsSource = listCitas;
            listDolencias = new List<listaDolencias>();
            listDolencias = CargarConten();
            lstHistorialMedico.ItemsSource = listDolencias;

            crearPacienteNuevo = new CrearPaciente();
            crearPacienteNuevo.ObjectCreated += VentanaObjectCreated;
        }
        private List<listaPacientes> CargarContenidoXML()
        {
            List<listaPacientes> listado = new List<listaPacientes>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos_Pacientes/pacientes.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevoPaciente = new listaPacientes("", "", "", "", "", "", "", "", null);
                nuevoPaciente.Nombre = node.Attributes["Nombre"].Value;
                nuevoPaciente.Apellidos = node.Attributes["Apellidos"].Value;
                nuevoPaciente.Direccion = node.Attributes["Direccion"].Value;
                nuevoPaciente.Telefono = node.Attributes["Telefono"].Value;
                nuevoPaciente.Edad = node.Attributes["Edad"].Value;
                nuevoPaciente.Sexo = node.Attributes["Sexo"].Value;
                nuevoPaciente.Correo = node.Attributes["Correo"].Value;
                nuevoPaciente.DNI = node.Attributes["DNI"].Value;
                nuevoPaciente.Foto = new Uri(node.Attributes["Foto"].Value, UriKind.Relative);
                listado.Add(nuevoPaciente);
            }
            return listado;
        }

        private List<listaPersonalSanitario> CargarContenidoXML2()
        {
            List<listaPersonalSanitario> listado = new List<listaPersonalSanitario>();
            try
            {
                XmlDocument doc = new XmlDocument();
                var fichero = Application.GetResourceStream(new Uri("Datos_Personal/sanitario.xml", UriKind.Relative));
                doc.Load(fichero.Stream);

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    string nombre = node.Attributes["Nombre"]?.Value ?? "";
                    string apellidos = node.Attributes["Apellidos"]?.Value ?? "";
                    string telefono = node.Attributes["Telefono"]?.Value ?? "";
                    string edad = node.Attributes["Edad"]?.Value ?? "";
                    string sexo = node.Attributes["Sexo"]?.Value ?? "";
                    string correo = node.Attributes["Correo"]?.Value ?? "";
                    string dni = node.Attributes["DNI"]?.Value ?? "";

                    var nuevoPersonalSanitario = new listaPersonalSanitario(nombre, apellidos, telefono, edad, sexo, correo, dni);

                    foreach (XmlNode pacienteNode in node.SelectNodes("PacientesAtendidos/Paciente"))
                    {
                        string nombrePaciente = pacienteNode.Attributes["Nombre2"]?.Value ?? "";
                        string dniPaciente = pacienteNode.Attributes["DNI2"]?.Value ?? "";

                        nuevoPersonalSanitario.PacientesAtendidos.Add(new PacientesAtendidos
                        {
                            Nombre2 = nombrePaciente,
                            DNI2 = dniPaciente
                        });
                    }

                    foreach (XmlNode horarioNode in node.SelectNodes("Horarios/Horario"))
                    {
                        string fechaHorario = horarioNode.Attributes["Fecha"]?.Value ?? "";
                        string horaHorario = horarioNode.Attributes["Hora"]?.Value ?? "";

                        nuevoPersonalSanitario.Horarios.Add(new Horarios
                        {
                            Fecha = fechaHorario,
                            Hora = horaHorario
                        });
                    }

                    listado.Add(nuevoPersonalSanitario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo XML: " + ex.Message);
            }

            return listado;
        }



        private List<listaPersonalLimpieza> CargarContenidoXML3()
        {
            List<listaPersonalLimpieza> listado = new List<listaPersonalLimpieza>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos_Personal/limpieza.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevoPersonalLimpieza = new listaPersonalLimpieza("", "", "", "", "", "", "");
                nuevoPersonalLimpieza.Nombre = node.Attributes["Nombre"].Value;
                nuevoPersonalLimpieza.Apellidos = node.Attributes["Apellidos"].Value;
                nuevoPersonalLimpieza.Telefono = node.Attributes["Telefono"].Value;
                nuevoPersonalLimpieza.Edad = node.Attributes["Edad"].Value;
                nuevoPersonalLimpieza.Sexo = node.Attributes["Sexo"].Value;
                nuevoPersonalLimpieza.Correo = node.Attributes["Correo"].Value;
                nuevoPersonalLimpieza.DNI = node.Attributes["DNI"].Value;
                listado.Add(nuevoPersonalLimpieza);
            }
            return listado;
        }

        private void VentanaObjectCreated(listaPacientes r)
        {
            listPacientes.Add(r);
            lstListaPacientes.Items.Refresh();
        }


        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow inicio;
            inicio = new MainWindow();
            MessageBox.Show("Gracias por usar nuestra aplicación.", "Despedida");
            inicio.Show();
            this.Close();
        }

        private void volver_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


    
        private void GuardarContenidoXML(List<listaPacientes> lista)
        {
            // Obtener la ruta completa del archivo temporal
            string tempFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos_Pacientes", "temp_pacientes.xml");

            // Crear el directorio si no existe
            string directoryPath = System.IO.Path.GetDirectoryName(tempFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Crear un nuevo documento XML
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Pacientes");

            foreach (var paciente in lista)
            {
                XmlElement pacienteElement = doc.CreateElement("Paciente");
                pacienteElement.SetAttribute("Nombre", paciente.Nombre);
                pacienteElement.SetAttribute("Apellidos", paciente.Apellidos);
                pacienteElement.SetAttribute("Direccion", paciente.Direccion);
                pacienteElement.SetAttribute("Telefono", paciente.Telefono);
                pacienteElement.SetAttribute("Edad", paciente.Edad);
                pacienteElement.SetAttribute("Sexo", paciente.Sexo);
                pacienteElement.SetAttribute("Correo", paciente.Correo);
                pacienteElement.SetAttribute("DNI", paciente.DNI);
                pacienteElement.SetAttribute("Foto", paciente.Foto.ToString());

                root.AppendChild(pacienteElement);
            }

            doc.AppendChild(root);

            // Guardar el nuevo documento en el archivo temporal
            doc.Save(tempFilePath);

            // Reemplazar el archivo original con el archivo temporal
            File.Copy(tempFilePath, System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos_Pacientes", "pacientes.xml"), true);
            File.Delete(tempFilePath);  // Eliminar el archivo temporal
        }

        private List<listaCitas> CargarContenido()
        {
            List<listaCitas> listado2 = new List<listaCitas>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos_Pacientes/citas.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevaCita = new listaCitas("", "", "", "", "", "");
                nuevaCita.Nombre = node.Attributes["Nombre"].Value;
                nuevaCita.Fecha = node.Attributes["Fecha"].Value;
                nuevaCita.Hora = node.Attributes["Hora"].Value;
                nuevaCita.PerEncargado = node.Attributes["PerEncargado"].Value;
                nuevaCita.Area = node.Attributes["Area"].Value;
                nuevaCita.Observaciones = node.Attributes["Observaciones"].Value;

                listado2.Add(nuevaCita);
            }
            return listado2;
        }


        private List<listaDolencias> CargarConten()
        {
            List<listaDolencias> listado3 = new List<listaDolencias>();
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos_Pacientes/dolencias.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevaDolencia = new listaDolencias("", "", "", null);
                nuevaDolencia.Nombre = node.Attributes["Nombre"].Value;
                nuevaDolencia.Fecha = node.Attributes["Fecha"].Value;
                nuevaDolencia.Tratamiento = node.Attributes["Tratamiento"].Value;
                nuevaDolencia.RayosX = new Uri(node.Attributes["RayosX"].Value, UriKind.Relative);
                listado3.Add(nuevaDolencia);
            }
            return listado3;
        }

        private void btnCrearPaciente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                crearPacienteNuevo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar la ventana CrearPaciente: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnModificarPaciente_Click(object sender, RoutedEventArgs e)
        {
            if (btnModificarPaciente.Content.ToString() == "Modificar Paciente")
            {
                // Habilitar la edición
                SetReadOnlyState(false);

                // Cambiar el texto del botón a "Guardar Modificación"
                btnModificarPaciente.Content = "Guardar Modificación";
            }
            else if (btnModificarPaciente.Content.ToString() == "Guardar Modificación")
            {
                // Guardar los cambios realizados
                GuardarCambiosPaciente();

                // Cambiar el texto del botón a "Modificar Paciente" y deshabilitar edición
                btnModificarPaciente.Content = "Modificar Paciente";
                SetReadOnlyState(true);
            }
        }

        private void GuardarCambiosPaciente()
        {
            // Obtener los valores actualizados de los campos de texto
            string nombreActualizado = txtNombre2.Text;
            string apellidosActualizado = txtApellidos2.Text;
            string direccionActualizado = txtDireccion.Text;
            string telefonoActualizado = txtTelefono.Text;
            string edadActualizado = txtEdad.Text;
            string sexoActualizado = txtSexo.Text;
            string correoActualizado = txtCorreo.Text;
            string dniActualizado = txtDni.Text;


            // Buscar el paciente en la lista usando el DNI
            var paciente = listPacientes.FirstOrDefault(p => p.DNI == txtDni.Text);
            if (paciente != null)
            {
                // Actualizar los datos del paciente
                paciente.Nombre = nombreActualizado;
                paciente.Apellidos = apellidosActualizado;
                paciente.Direccion = direccionActualizado;
                paciente.Telefono = telefonoActualizado;
                paciente.Edad = edadActualizado;
                paciente.Sexo = sexoActualizado;
                paciente.Correo = correoActualizado;
                paciente.DNI = dniActualizado;

                // Guardar los cambios en el archivo XML
                GuardarContenidoXML(listPacientes);
            }
            else
            {
                MessageBox.Show($"Error paciente no encontrado: ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetReadOnlyState(bool isReadOnly)
        {
            txtNombre2.IsReadOnly = isReadOnly;
            txtApellidos2.IsReadOnly = isReadOnly;
            txtDireccion.IsReadOnly = isReadOnly;
            txtTelefono.IsReadOnly = isReadOnly;
            txtEdad.IsReadOnly = isReadOnly;
            txtSexo.IsReadOnly = isReadOnly;
            txtCorreo.IsReadOnly = isReadOnly;
            txtDni.IsReadOnly = isReadOnly;

            var background = isReadOnly ? Brushes.White : Brushes.Beige;
            txtNombre2.Background = background;
            txtApellidos2.Background = background;
            txtDireccion.Background = background;
            txtTelefono.Background = background;
            txtEdad.Background = background;
            txtSexo.Background = background;
            txtCorreo.Background = background;
            txtDni.Background = background;
            
        }

        private void btnBorrarPaciente_Click(object sender, RoutedEventArgs e)
        {
            // Comprobar si hay un paciente seleccionado
            var pacienteSeleccionado = lstListaPacientes.SelectedItem as listaPacientes;
            if (pacienteSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un paciente para borrar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Eliminar el paciente seleccionado de la lista
            listPacientes.Remove(pacienteSeleccionado);

            // Actualizar el ListBox
            lstListaPacientes.Items.Refresh();

            // Guardar los cambios en el archivo XML
            GuardarContenidoXML(listPacientes);

            MessageBox.Show("Paciente eliminado con éxito.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(" - Navega entre las distintas pestañas superiores para ver los pacientes o el personal del edificio.\n " +
                   "- Dentro de los pacientes, selecciona uno para ver sus datos, modificar, borrar o acceder a su historial o citas.\n " +
                   "- Pulsando en el botón de crear paciente, se abrira una pestaña en la cual podrás crear un paciente u volver al menú.\n" +
                   "- Dentro del personal sanitario o de limpieza podrás acceder a la información de cada persona y algunas especificaciones.\n",
                   "Ayuda", MessageBoxButton.OK, MessageBoxImage.Information);
        
        }

        private void FotoRayos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapSource bitmapSource = FotoRayos.Source as BitmapSource;

            // Crear un MemoryStream para contener la imagen
            MemoryStream memoryStream = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);

            // Crear un nuevo BitmapImage
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            memoryStream.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            // Utilizar este BitmapImage en el editor gráfico
            EditorGrafico editor = new EditorGrafico();
            editor.SetImageSource(bitmapImage);
            editor.OnImageEdited += EditedImageReceived;
            editor.Show();
        }

        private void EditedImageReceived(BitmapImage editedImage)
        {
            // Actualiza la imagen en tu interfaz de usuario
            FotoRayos.Source = editedImage;
        }
    }
}
