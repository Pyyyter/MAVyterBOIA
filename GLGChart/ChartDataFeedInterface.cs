using System;
using System.Collections.Generic;
using GenLogic;

public interface ChartDataFeedInterface
{
   bool Initialize();
   void ClearDataFeed();
   List<PlotDataPoint> GetPlotData( string tag_source );
   List<PlotDataPoint> GetPlotData( int tag_index );

   // Obtain an array of historical data for a given tag source.
   List<PlotDataPoint> GetHistPlotData( string tag_source,
                                                double start_time, 
                                                double end_time );

   // Obtain an array of historical data for a given tag index.
   List<PlotDataPoint> GetHistPlotData( int tag_index,
                                                double start_time,
                                                double end_time );
}
