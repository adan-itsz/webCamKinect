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
using Microsoft.Kinect;

namespace camaraWeb
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor kinect;
        public MainWindow()
        {
            InitializeComponent();
            kinect = KinectSensor.KinectSensors[0];//asignamos al objeto kinect el valor del arreglo kinectSensor que indica que es el dispositivo fisico numero 1 conectado
            kinect.Start();// iniciar kinect
            kinect.ColorStream.Enable();//iniciamos señal de envio de datos camara rgb(se manejan com stream)
            kinect.ColorFrameReady += kinect_ColorFrameReady;

        }

        void kinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame frameImagen = e.OpenColorImageFrame()) //openColorImageFrame comienza a recibir flujo de datos de la camara y con la estructura using se va eliminando para recibir mas flujo de datos
            {
                if (frameImagen == null) // si no recibe ningun flujo de datos muestra un valor nulo
                {
                    return; 
                }
                byte[] datosColor = new byte[frameImagen.PixelDataLength];//el metodo pixelDataLength indica el tamaño del buffer de datos, eso lo convertimos en un array de tipo byte
                frameImagen.CopyPixelDataTo(datosColor);// hacemos peticion a frameImagen para copear datos del frame al buffer del arreglo, teniendo asi el tamaño de la imagen en el arreglo datos color
                mostrarVideo.Source = BitmapSource.Create(frameImagen.Width, frameImagen.Height,
                    96, 96, PixelFormats.Bgr32, null, datosColor,
                    frameImagen.Width * frameImagen.BytesPerPixel);//ancho imagen, alto , dpi (pts por pulgada) horizontales,dpi verticales, formato de pixeles ,valor nulo, arreglo de bytes (contenido de imagen),strike ancho por largo 

            }

        }
            

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
