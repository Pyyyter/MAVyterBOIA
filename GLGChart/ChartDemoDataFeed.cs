using System;
using System.Collections.Generic;
using GenLogic;

/////////////////////////////////////////////////////////////////////// 
// Sample implementation of DataFeedInterface, provides simulated chart
// data. In a real application, data will be coming from LiveDataFeed,
// a custom implementation of DataFeedInterface.
/////////////////////////////////////////////////////////////////////// 
public class ChartDemoDataFeed : ChartDataFeedInterface
{
   GlgComboChart glg_chart;
   static bool IsInitialized = true;

   long[] counters = null;
   int chart_index = 0;
   
   /* If SUPPLY_TIME_STAMP=true, BATCH_DATA_PROCESSING may be set to true
      to push data samples in packets.
   */
   bool BATCH_DATA_PROCESSING = true;
   const int PACKET_SIZE = 10;
   int packet_size;

   /////////////////////////////////////////////////////////////////////// 
   // Constructor.
   /////////////////////////////////////////////////////////////////////// 
   public ChartDemoDataFeed( GlgComboChart chart ) 
   {
      glg_chart = chart;

      // Initialize data feed. Generate an error on failure and quit.
      if( !Initialize() )
        GlgComboChart.AppError( "DataFeed initialization failed.", true );

      packet_size = 1;
      if( BATCH_DATA_PROCESSING )
      {
         if( glg_chart.SUPPLY_TIME_STAMP )
           packet_size = PACKET_SIZE;
         else
           GlgComboChart.AppInfo( "Resetting BATCH_DATA_PROCESSING due to SUPPLY_TIME_STAMP=false" );
      }
   }
   
   /////////////////////////////////////////////////////////////////////// 
   // Initiaze DataFeed. Application will provide custom code for 
   // data feed initialization.
   /////////////////////////////////////////////////////////////////////// 
   public bool Initialize()
   {
      if( IsInitialized )
        return true;    // Data feed has been already initialized.

      ChartDemoDataFeed.IsInitialized = true;
      return true;
   }

   ///////////////////////////////////////////////////////////////////////
   // Perform necessary cleanup. This method is invoked when the chart
   // confiuguration changes.
   ///////////////////////////////////////////////////////////////////////
   public void ClearDataFeed()
   {
      counters = null;
      chart_index = 0;
   }
   
   ///////////////////////////////////////////////////////////////////////
   // Obtain data for a given data source variable (tag_source).
   // Use simulated data for demo.
   ///////////////////////////////////////////////////////////////////////
   public List<PlotDataPoint> GetPlotData( string tag_source )
   {
      if( GlgComboChart.IsUndefined( tag_source ) )
        return null;

      return GetDemoPlotData( tag_source, -1, /*use tag source*/ true );
   }
   
   ///////////////////////////////////////////////////////////////////////
   // Obtain data for a given data source variable index (tag_index).
   // Use simulated data for demo.
   ///////////////////////////////////////////////////////////////////////
   public List<PlotDataPoint> GetPlotData( int tag_index )
   {
       if( tag_index < 0 )
         return null;

       return
         GetDemoPlotData( null, tag_index, /*false to use tag index*/ false );
   }

   ///////////////////////////////////////////////////////////////////////
   // It is invoked on a timer to obtain simulated real-time demo data.
   // If use_tag_source=true, generate data for the specified tag_source;
   // otherwise, generate data for the specified tag_index.
   ///////////////////////////////////////////////////////////////////////
   public List<PlotDataPoint>
     GetDemoPlotData( string tag_source, int tag_index, bool use_tag_source )
   {
      List<PlotDataPoint> data_array = new List<PlotDataPoint>();
      for( int i=0; i<packet_size; ++i )
      {
         /* Generate demo data for each data sample. */
         PlotDataPoint data_point = new PlotDataPoint();
         data_point.value = GetDemoValue( tag_source, tag_index, use_tag_source,
                                          /*realtime mode*/ false );
         data_point.value_valid = true;
      
         if( glg_chart.SUPPLY_TIME_STAMP )
           // Supply time stamp explicitly.
           data_point.time_stamp = GetTimeStamp( i );
         else
           // Chart will automatically supply time stamp using current time.
           data_point.time_stamp = 0.0;

         data_array.Add( data_point );
      }
      
      ++chart_index;
      if( chart_index >= glg_chart.NumCharts )
        chart_index = 0;

      return data_array;
   }
   
   ///////////////////////////////////////////////////////////////////////
   // Obtain historical chart data for a provided time interval for a given
   // tag_source (datasource variable name).
   /////////////////////////////////////////////////////////////////////// 
   public List<PlotDataPoint>
     GetHistPlotData( string tag_source, double start_time, double end_time )
   {
      if( GlgComboChart.IsUndefined( tag_source ) )
        return null;

      return
        GetDemoHistPlotData( tag_source, /*tag_index*/ -1,
                             start_time, end_time, /*use tag_source*/ true );
   }

   ///////////////////////////////////////////////////////////////////////
   // Obtain historical chart data for a provided time interval for a given
   // tag_index (datasource variable index).
   /////////////////////////////////////////////////////////////////////// 
   public List<PlotDataPoint>
     GetHistPlotData( int tag_index, double start_time, double end_time )
   {
      if( tag_index < 0 )
        return null;

      return GetDemoHistPlotData( /*tag_source*/ null, tag_index,
                                  start_time, end_time,
                                  /*false to use tag_index*/ false );
   }
   
   ///////////////////////////////////////////////////////////////////////
   // It is invoked once to obtain simulated historical demo data for
   // prefilling a chart.
   // If use_tag_source=true, generate data for the specified tag_source;
   // otherwise, generate data for the specified tag_index.
   /////////////////////////////////////////////////////////////////////// 
   public List<PlotDataPoint>
     GetDemoHistPlotData( string tag_source, int tag_index,
                          double start_time, double end_time,
                          bool use_tag_source )
   {
      /* Demo: generate pre-fill demo data with the reduced frequency.
         In an application, data will be queried from a real data source, 
         returning an array of data points.
      */
      double dt = glg_chart.GetSampleInterval();

      List<PlotDataPoint> data_array = new List<PlotDataPoint>();
      for( double time_stamp = start_time;
           time_stamp < end_time; time_stamp += dt )
      {
         /* Generate demo data. */
         PlotDataPoint data_point = new PlotDataPoint();
         data_point.value = GetDemoValue( tag_source, tag_index, use_tag_source,
                                          /*historical*/ true );
         data_point.time_stamp = time_stamp;
         data_point.value_valid = true;
         data_array.Add( data_point );
      }

      ++chart_index;

      // All charts are prefilled - reset chart_index.
      if( chart_index >= glg_chart.NumCharts )
        chart_index = 0;

      return data_array;
   }
   
   /////////////////////////////////////////////////////////////////////// 
   // Generates plot demo data value based on the specified data variable
   // name (tag_source) if use_tag_source=true, or based on tag_index
   // if use_tag_source=false.
   ///////////////////////////////////////////////////////////////////////
   double GetDemoValue( string tag_source, int tag_index,
                        bool use_tag_source, bool historical_mode )
   {
      double 
        period = 100.0,
        low = 0.0,
        high = 100.0;

      if( counters == null )
      {
        counters = new long[ glg_chart.NumCharts ];
        chart_index = 0;
      }

      if( use_tag_source && !GlgComboChart.IsUndefined( tag_source) )
      {
         if( tag_source.Contains( "Volt" ) )
         {
            low = 0.0;
            high = 500.0;
            period = 300.0;
         }
         else if( tag_source.Contains( "Amp" ) )
         {
            low = 0.0;
            high = 50.0;
            period = 500.0;
         }
         else if( tag_source.Contains( "Pressure" ) )
         {
            low = 0.0;
            high = 100.0;
            period = 200.0;
         }
         else if( tag_source.Contains( "Temperature" ) )
         {
            low = 0.0;
            high = 100.0;
            period = 300.0;
         }
         else if( tag_source.Contains( "RPM" ) )
         {
            low = 0.0;
            high = 3000.0;
            period = 500.0;
         }
         else if( tag_source.Contains( "Fuel" ) )
         {
            low = 0.0;
            high = 100.0;
            period = 200.0;
         }
         else
         {
            low = 0.0;
            high = 100.0;
            period = 100.0;
         }
      }
      else if( tag_index >=0 )
      {
         low = 0.0;
         high = 100.0;
         period = 50.0 * ( 1.0 + tag_index * 2.0 );
      }
      else
        return 0.0;
      
      double half_amplitude = ( high - low ) / 2.0;
      double center = low + half_amplitude;
      double alpha = 2.0 * Math.PI * counters[ chart_index ] / period;
      double value = center + 
        half_amplitude * Math.Sin( alpha ) * Math.Sin( alpha / 30.0 );

      if( historical_mode )
        counters[ chart_index ] += packet_size;
      else
        ++counters[ chart_index ];

      return value;
   }

   ///////////////////////////////////////////////////////////////////////
   double GetTimeStamp( int sample_num )
   {
      if( packet_size == 1 )
        return GlgComboChart.current_time;

      /* Demo only: Generate a demo time stamp in batch data processing mode. 
         A real application will provide a real time stamp for each data sample.
      */      
      double last_time_stamp = GlgComboChart.last_time;
      double curr_time = GlgComboChart.current_time;

      double interval = ( curr_time - last_time_stamp ) / packet_size;
      double time_stamp = last_time_stamp + interval * ( sample_num + 1 );

      return time_stamp;
   }
}
