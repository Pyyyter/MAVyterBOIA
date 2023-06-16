using System;
using GenLogic;

public class ChartConfigRecord
{
   public string chart_title;  // Title string to be displayed in the chart.
   public string yaxis_label;  // Label for the Y axis.
   public string tag_source;   // Data source variable for a chart's plot.
   public int tag_index;       // Optional: index in the data variables array.
   public double low;          // Low range for the Y Axis.
   public double high;         // High range for the Y Axis. 
   public GlgPoint plot_color; /* Plot EdgeColor. If not defined, the plot
                                   color defined in the .g file will be used. */
   public bool init_visibility; /* The chart's initial selection state. */

   public ChartConfigRecord( string chart_title, string yaxis_label,
                             string tag_source, int tag_index,
                             double low, double high, GlgPoint plot_color,
                             bool init_visibility )
   {
      this.chart_title = chart_title;
      this.yaxis_label = yaxis_label;
      this.tag_source = tag_source;
      this.tag_index = tag_index;
      this.low = low;
      this.high = high;
      this.plot_color = plot_color;
      this.init_visibility = init_visibility;
   }
}
