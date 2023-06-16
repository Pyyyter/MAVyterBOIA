using System;
using System.Collections.Generic;
using GenLogic;

/////////////////////////////////////////////////////////////////////// 
// Application should provide custom implemnetation of LiveDataFeed.
/////////////////////////////////////////////////////////////////////// 
public class ChartLiveDataFeed : ChartDataFeedInterface
{
   GlgComboChart glg_chart;
   static bool IsInitialized = false;
   Random dataFeedRandomizer = new Random();

   /////////////////////////////////////////////////////////////////////// 
   // Constructor.
   /////////////////////////////////////////////////////////////////////// 
   
    public ChartLiveDataFeed( GlgComboChart chart ) 
   {
      glg_chart = chart;

      // Initialize data feed. Generate an error on failure and quit.
      if( !Initialize() )
        GlgComboChart.AppError( "DataFeed initialization failed.", true );    
   }
   
   /////////////////////////////////////////////////////////////////////// 
   // Initiaze DataFeed. 
   /////////////////////////////////////////////////////////////////////// 
   public bool Initialize()
   {
      if( IsInitialized )
        return true;    // Data feed has been already initialized.

      /* Place custom code here to initialize data feed.
         Return false on failure. 
      */
      
      ChartLiveDataFeed.IsInitialized = true;

      return true;
   }

   ///////////////////////////////////////////////////////////////////////
   // Perform necessary cleanup. This method is invoked when the chart
   // confiuguration changes.
   ///////////////////////////////////////////////////////////////////////
   public void ClearDataFeed()
   {
      /* Place custom code here to perform necessary cleanup. */
   }
   
   ///////////////////////////////////////////////////////////////////////
   // Query plot data based on the tag index (datasource variable index).
   // See GetPlotData( string tag_source ) for querying based on the
   // tag source (variable name).
   // The tag index may represent datasource variable index in the external
   // data array. For example, it may represent a sensor number.
   ///////////////////////////////////////////////////////////////////////
   public List<PlotDataPoint> GetPlotData( int tag_index )
   {
      if( tag_index < 0 )
        return null;

      List<PlotDataPoint> data_array = new List<PlotDataPoint>();
      // Define num_samples as needed.
      int num_samples = 10;  
      for( int i=0; i<num_samples; ++i )
      {
         PlotDataPoint data_point = new PlotDataPoint();

         // Place custom code to obtain data sample value.
         data_point.value = dataFeedRandomizer.NextDouble() * dataFeedRandomizer.Next(-10, 10);
         data_point.value_valid = true;
      
         if( glg_chart.SUPPLY_TIME_STAMP )
           // Place custom code here to supply time stamp.
           data_point.time_stamp = GlgComboChart.GetCurrTime();
         else
           // Chart will automatically supply time stamp using current time.
           data_point.time_stamp = 0.0;

         data_array.Add( data_point );
      }
      
      return data_array;
   }

   ///////////////////////////////////////////////////////////////////////
   // Query plot data based on the tag source, which represents variable
   // name or a process database tag.
   ///////////////////////////////////////////////////////////////////////
   public List<PlotDataPoint> GetPlotData( string tag_source )
   {
      if( GlgComboChart.IsUndefined( tag_source ) )
        return null;

      List<PlotDataPoint> data_array = new List<PlotDataPoint>();
      // Define num_samples as needed.
      int num_samples = 10;
      Random rand = new Random();
      for( int i=0; i<num_samples; ++i )
      {
         // MY EDITED CODE HERE
         PlotDataPoint data_point = new PlotDataPoint();
         data_point.value = rand.NextDouble() * rand.Next(-10, 10);
         data_point.value_valid = true;
         if (glg_chart.SUPPLY_TIME_STAMP)
             data_point.time_stamp = GlgComboChart.GetCurrTime();
         else
             // Chart will automatically supply time stamp using current time.
             data_point.time_stamp = 0.0;

         data_array.Add( data_point );
      }
      
      return data_array;
   }

   ///////////////////////////////////////////////////////////////////////
   // Query historical chart data for the specified time interval based
   // in the tag index (datasource variable index).
   // See GetHistPlotData( string tag_source, ... ) for querying based on
   // the tag source (variable name).
   /////////////////////////////////////////////////////////////////////// 
   public List<PlotDataPoint>
     GetHistPlotData( int tag_index, double start_time, double end_time )
   {
      if( tag_index < 0 )
        return null;
            
      List<PlotDataPoint> data_array = new List<PlotDataPoint>();

      /*  Provide custom application code to generate data samples for the
          requested time interval.
      */

      /*
      for(.. )
      {
         PlotDataPoint data_point = new PlotDataPoint();
      
         data_point.value = 
         data_point.time_stamp = 
         data_point.value_valid = true;
      
         data_array.add(data_point);
      }
      */
      
      return data_array;
   }   

   ///////////////////////////////////////////////////////////////////////
   // Query historical chart data for the specified time interval for a
   // given tag source (datasource variable name).
   /////////////////////////////////////////////////////////////////////// 
   public List<PlotDataPoint>
     GetHistPlotData( string tag_source, double start_time, double end_time )
   {
      if( GlgComboChart.IsUndefined( tag_source ) )
        return null;

      List<PlotDataPoint> data_array = new List<PlotDataPoint>();

      /*  Provide custom application code to generate data samples for the
          requested time interval.
      */

      /*
      for(.. )
      {
         PlotDataPoint data_point = new PlotDataPoint();
      
         data_point.value = 
         data_point.time_stamp = 
         data_point.value_valid = true;
      
         data_array.add(data_point);
      }
      */

      return data_array;
   }
}
   
