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

namespace LABORATORIO
{
    /// <summary>
    /// Lógica de interacción para EditorGrafico.xaml
    /// </summary>
    public partial class EditorGrafico : Window
{
        private SolidColorBrush lineColor = new SolidColorBrush(Colors.Black); // Color por defecto
        public EditorGrafico()
    {
        InitializeComponent();
    }

    Point currentPoint = new Point();

    private void drawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
            currentPoint = e.GetPosition(this.drawingCanvas);
    }

        private void drawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line
                {
                    Stroke = lineColor, // Usar el color definido aquí
                    X1 = currentPoint.X,
                    Y1 = currentPoint.Y,
                    X2 = e.GetPosition(this.drawingCanvas).X,
                    Y2 = e.GetPosition(this.drawingCanvas).Y
                };

                currentPoint = e.GetPosition(this.drawingCanvas);
                this.drawingCanvas.Children.Add(line);
            }
        }

        public void SetImageSource(BitmapImage image)
    {
        this.imageBrush.ImageSource = image;
    }

        // lógica para guardar la imagen editada 
        public BitmapImage SaveCanvasToImage()
        {
            // Tamaño del canvas
            int width = (int)drawingCanvas.ActualWidth;
            int height = (int)drawingCanvas.ActualHeight;

            // Crear un RenderTargetBitmap
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Pbgra32);
            drawingCanvas.Measure(new Size(width, height));
            drawingCanvas.Arrange(new Rect(new Size(width, height)));
            renderBitmap.Render(drawingCanvas);

            // Convertir a BitmapImage
            BitmapImage bitmapImage = new BitmapImage();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage editedImage = SaveCanvasToImage();

            // Aquí puedes enviar la imagen editada de vuelta al menú principal
            // Por ejemplo, usando un evento o una acción
            OnImageEdited?.Invoke(editedImage);

            this.Close();
        }

        public Action<BitmapImage> OnImageEdited;

        private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    string selectedColorName = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
    Color selectedColor = (Color)ColorConverter.ConvertFromString(selectedColorName);
    lineColor = new SolidColorBrush(selectedColor);
}
    }
}
