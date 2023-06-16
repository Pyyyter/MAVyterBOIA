using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using static GMap.NET.MapProviders.StrucRoads.SnappedPoint;

namespace SimpleExample
{
    public partial class FormMapa : Form
    {
        
        bool UseTimer = false;
        public PointLatLng boatLocation = new PointLatLng(-22.8570241, -43.0955684);
        Bitmap boatIcon = new Bitmap("Resources/boaticon1.bmp");

        public FormMapa()
        {
            InitializeComponent();
                 
        }

        private void mapControl_Load(object sender, EventArgs e)
        {
            mapControl.MapProvider = BingMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            mapControl.Position = boatLocation; // Initially centralizes map on top of boat marker position.
            mapControl.MinZoom = 13;
            mapControl.MaxZoom = 17;
            mapControl.Zoom = 15;
            SetupGPSMarker();
            if (UseTimer)
            {
                timerMap.Enabled = true;
                timerMap.Interval = 1000;
            }
           
        }

        private void SetupGPSMarker()
        {
            boatIcon.MakeTransparent();
            GMapMarker boatMarker = new GMarkerGoogle(boatLocation, boatIcon);
            GMapOverlay boatOverlay = new GMapOverlay("boats");
            boatOverlay.Markers.Add(boatMarker); // Multiple markers can be added to the same overlay and they are kept by reference.
            mapControl.Overlays.Add(boatOverlay);
        }

        private void UpdateGPSIcon(bool ShouldRotate = false)
        {
            if (!ShouldRotate)
            {
                mapControl.Overlays[0].Markers[0].Position = boatLocation;
                return;
            }

            Bitmap rotatedBitmap = new Bitmap(boatIcon.Width, boatIcon.Height); 
            using (Graphics graphics = Graphics.FromImage(rotatedBitmap))
            {
                Random randomAngleGenerator = new Random();
                // Set the rotation point as the center of the image
                graphics.TranslateTransform(boatIcon.Width / 2f, boatIcon.Height / 2f);
                graphics.RotateTransform(randomAngleGenerator.Next(-50, 50));
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(boatIcon, -boatIcon.Width / 2f, -boatIcon.Height / 2f); 
            }
            boatIcon = rotatedBitmap;
            mapControl.Overlays[0].Markers[0] = new GMarkerGoogle(boatLocation, boatIcon);
        }

        public void UpdateLocation(double lat, double lng)
        {
            boatLocation.Lat = lat;
            boatLocation.Lng = lng;
            UpdateGPSIcon(false);
        }

        private void timerMap_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            double lat_offset = Convert.ToDouble(random.Next(-3, 3)) / 10000.0;
            double lng_offset = Convert.ToDouble(random.Next(-3, 3)) / 10000.0;
            boatLocation.Lat += lat_offset;
            boatLocation.Lng += lng_offset;
            UpdateGPSIcon(false);
        }
    }
}
