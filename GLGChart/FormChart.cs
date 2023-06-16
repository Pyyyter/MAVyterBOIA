using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using GenLogic;

namespace SimpleExample
{
   public partial class FormChart : Form
   {
      /* To use live data, set  RANDOM_DATA=false, or use
         -live-data command line option.
      */
      bool RANDOM_DATA = true;
      
      // Use default drawing name unless specified as a command line argument.
      String DefaultDrawingName = "stripchart_combo.g";

      // These configuration tables are used for demo.
   
      /* An array of configuration records containing configuration data 
         for each chart. In the demo, ConfigDataTable1 and ConfigDataTable2 
         are hardcoded. The application would populate these arrays 
         dynamically as needed.
         ChartConfigRecord fields: 
         chart_title, yaxis_label, tag_source, tag_index, 
         y_low, y_high, plot_color, init_visibility.
      */
      public static ChartConfigRecord[] ConfigTable1 = new ChartConfigRecord[] {
         new ChartConfigRecord( "Pressure", "PSI", "Pressure", -1,
                                0, 100, new GlgPoint( 0.0, 0.4, 0.8 ), true ),
         new ChartConfigRecord( "Temperature", "C", "Temperature", -1,
                           0, 100, new GlgPoint( 0.4, 0.8, 0.0 ), true ),
         new ChartConfigRecord( "Voltage", "V", "Voltage", -1,
                           0, 500, new GlgPoint( 0.5, 0.0, 0.65 ), false ),
         new ChartConfigRecord( "Current", "Amp", "Amps", -1,
                           0, 50, new GlgPoint( 0.65, 0.0, 0.35 ), true )
      };
      
      public static ChartConfigRecord[] ConfigTable2 = new ChartConfigRecord[] {
         new ChartConfigRecord( "RPM", "RPM", "RPM", -1,
                           0, 3000, new GlgPoint( 0.0, 0.4, 0.8 ), true ),  
         new ChartConfigRecord( "Fuel", "%", "Fuel", -1,
                           0, 100, new GlgPoint( 0.4, 0.8, 0.0 ), true )
      };
      
      const String CONFIG1_TITLE = "Configuration1";
      const String CONFIG2_TITLE = "Configuration2";
      const String CONFIG1_FILE = null;
      const String CONFIG2_FILE = null;
      
      // Default configuration info.
      ConfigInfo DefaultConfigInfo =
        new ConfigInfo( CONFIG1_TITLE, CONFIG1_FILE, ConfigTable1 );

      // Stores currently loaded configuration information.
      ConfigInfo LoadedConfigInfo = null;
      
      // GLG drawing filename. May be specified as a command line argument.
      String DrawingName = null;
      
      // GlgComboChart is subclassed from GlgControl, a GLG .NET User Control.
      public GlgComboChart glg_chart;

      public FormChart()
      {
         InitializeComponent();
         
         // Create GlgComboChart, passing a parent container.
         glg_chart = new GlgComboChart( this );
         
         // Add GLG control to the form.
         Controls.Add( glg_chart ); 
         
         // Position the GLG control.
         ResizeGLGControl();
         
         /* Process command line arguments.
            Supported command line options:
            -random-data      (use simulated demo data)
            -live-data        (use live application date from LiveDataFeed)
            drawing_name      (specifies GLG drawing to be loaded and animated)
         */
         ProcessArgs();
         
         /* Assign a GLG drawing to be displayed in the GlgComboChart control. 
            If a drawing file has not been supplied on the command line, use 
            default drawing filename defined by DefaultDrawingName.
         */  
         if( DrawingName == null )
           DrawingName = DefaultDrawingName;
      
         String drawing_file = Directory.GetFiles(Application.StartupPath, DrawingName, SearchOption.AllDirectories).FirstOrDefault();
        
         // Load a specified drawing.
         glg_chart.LoadDrawing( drawing_file );

         // Load specified configuration and configure the charts.
         SetConfiguration( DefaultConfigInfo );
                             
         // Start periodic dynamic updates.
         glg_chart.StartUpdates();
      }

      ////////////////////////////////////////////////////////////////////////
      // Load specified chart configiuration data and configure the charts
      // in the drawing based on the loaded configuration.
      ////////////////////////////////////////////////////////////////////////
      public void SetConfiguration( ConfigInfo config_info )
      {
         /* If the requested configuration matches currently loaded 
            configuration, do nothing to avoid loading the same configuration.
         */
         if( config_info == null || SameConfiguration( config_info ) )
           return;
         
         List<ChartConfigRecord> configuration =
           LoadConfiguration( config_info );

         if( configuration == null || configuration.Count == 0 )
         {
            GlgComboChart.AppError( "Invalid Configuration. Chart configuration failed.", false );
            DisplayStatus( "Loading configuration failed: " + config_info.config_title );
            return;
         }

         // Store currently loaded configuration information.
         LoadedConfigInfo = config_info;
      
         // Configure the charts using loaded configuration data.
         glg_chart.SetChartConfiguration( configuration );

         DisplayStatus( "Current Configuration: " + config_info.config_title );
      }

      ////////////////////////////////////////////////////////////////////////
      // Returns true if the requested configuration is the same as curently
      // loaded configuration, if any.
      ////////////////////////////////////////////////////////////////////////
      public bool SameConfiguration( ConfigInfo config_info )
      {
         if( config_info == null )
           return false;
         
         /* The demo uses the configuration title as a matching criteria. 
            The application can extend the matching criteria logic as needed.
         */
         String title = config_info.config_title;
         if( LoadedConfigInfo != null &&
             title.Equals( LoadedConfigInfo.config_title ) )
           return true;
         
         return false;
      }
      
      ////////////////////////////////////////////////////////////////////////
      // Create Configuration array and populate it with application
      // configuration data. In demo mode, hard-coded CongData array is used.
      // The application should provide custom implementation of this method
      // to populate Configuration array with application data.
      ////////////////////////////////////////////////////////////////////////
      public List<ChartConfigRecord> LoadConfiguration( ConfigInfo config_info )
      {
         if( config_info == null )
           return null;
          
          List<ChartConfigRecord> configuration = new List<ChartConfigRecord>();

          if( RANDOM_DATA )
          {
             // Use a predefined configuration table for demo.
             ChartConfigRecord[] config_table = config_info.config_table;
             if( config_table == null || config_table.Length == 0 )
               return null;
                        
             for( int i = 0; i < config_table.Length; ++i )
               configuration.Add( config_table[ i ] );
          }
          else
          {
             /* Place custom code here to populate configuration with a list
                of configuration records, using either a file or
                predefined table.
             */
          }

          return configuration;
      }
      
      ////////////////////////////////////////////////////////////////////////
      // Process command line arguments.
      //   Supported command line options:
      //   -random-data         (use simulated demo data)
      //   -live-data           (use live application data from LiveDataFeed)
      //   drawing_filename     (GLG drawing to be displayed)
      ///////////////////////////////////////////////////////////////////////
      public void ProcessArgs()
      {
         string[] args = Environment.GetCommandLineArgs();
         
          for( int skip = 1; skip < args.Length; ++skip )
          {
             if( args[ skip ].Equals( "-random-data" ) )
             {
                // Use simulated demo data for animation.
                glg_chart.RandomData = true;
                GlgComboChart.AppLog( "Using simulated data for animation." );
             }
             else if( args[ skip ].Equals( "-live-data" ) )
             {
                // Use live application data for animation.
                glg_chart.RandomData = false;
                GlgComboChart.AppLog( "Using live application data for animation." );
             }
             else if( args[skip].StartsWith( "-" ) )
               continue;
             else
             {
                // Use the drawing file from the command line, if any.
                DrawingName = args[ skip ];
             }
          }         
      }

      ////////////////////////////////////////////////////////////////////////
      // Set width and height of the GLG control.
      ////////////////////////////////////////////////////////////////////////
      private void ResizeGLGControl()
      {
         if( glg_chart == null )
           return;

         int y_offset = 65;
         
         // Resize GLG control according to the form's client size.
         glg_chart.Location = new Point(0, y_offset);
         glg_chart.Width = ClientSize.Width;
         glg_chart.Height = ClientSize.Height - y_offset;
         
      }

      ////////////////////////////////////////////////////////////////////////
      private void DisplayStatus( String status )
      {
         // Display status string in the StatusLabel.
         StatusLabel.Text = status;
      }
      
      ////////////////////////////////////////////////////////////////////////
      private void GlgRTChartForm_FormClosed( object sender, 
                                              FormClosedEventArgs e )
      {
         if( glg_chart == null )
           return;

         glg_chart.StopUpdates(); 
      }

      ////////////////////////////////////////////////////////////////////////
      private void GlgRTChartForm_Resize( object sender, EventArgs e )
      {
         ResizeGLGControl();
      }

      ////////////////////////////////////////////////////////////////////////
      private void Configuration1Btn_Click( object sender, EventArgs e )
      {
          if( glg_chart == null )
              return;

          SetConfiguration( new ConfigInfo( CONFIG1_TITLE, CONFIG1_FILE,
                                            ConfigTable1 ) );
      }

      ////////////////////////////////////////////////////////////////////////
      private void Configuration2Btn_Click( object sender, EventArgs e )
      {
          if( glg_chart == null )
            return;

          SetConfiguration( new ConfigInfo( CONFIG2_TITLE, CONFIG2_FILE,
                                            ConfigTable2 ) );
      }

      
    }

    ////////////////////////////////////////////////////////////////////////
    public class ConfigInfo
   {
      public String config_title;
      public String config_file;
      public ChartConfigRecord[] config_table;
      
      public ConfigInfo()
      {
         config_table = null;
      }
      
      public ConfigInfo( String title, String filename,
                         ChartConfigRecord[] config_table )
      {
         this.config_title = title;
         this.config_file = filename;
         this.config_table = config_table;
      }
   }
}
