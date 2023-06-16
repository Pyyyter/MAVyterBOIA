using System;
using System.Collections.Generic;
using GenLogic;
              
public class ChartInfo
{
   public GlgObject chart_vp;        // Chart viewport (ChartViewport<N>)
   public GlgObject chart;           // Chart object inside the chart viewport.
   public PlotInfo plot_info;        // Plot data.
   public double low;                // YAxis Low range.
   public double high;               // YAxis High range.
   public string yaxis_label;        // YAxis label.

   /* Vertical line that follows the mouse position, is stored only for the
      first chart.
   */
   public GlgObject cross_hair_line;       

   /* Label Annotation object used to display custom tooltip showing value 
      and time of the plot's data sample at the mouse position.
   */
   public GlgObject label_annotation;
   
   /* MarkerAnnotation object used to annotate the data sample selected in
      the plot.
   */
   public GlgObject marker_annotation;
   
   ////////////////////////////////////////////////////////////////////////
   // Constructor.
   ////////////////////////////////////////////////////////////////////////
   public ChartInfo() 
   {
   }
   
   ////////////////////////////////////////////////////////////////////////
   // Should be invoked after hierarchy setup.
   ////////////////////////////////////////////////////////////////////////
   public void InitializeChart( ChartConfigRecord config_record,
                                GlgObject chart_vp, int i )
   {
      if( config_record == null )
        return;

      // Store the chart viewport.
      this.chart_vp = chart_vp;
      if( chart_vp == null )
      {
         GlgComboChart.AppError( "Invalid chart viewport.", false );
         return;
      }
       
      // Stoire the chart object inside the chart's viewport.
      chart = chart_vp.GetResourceObject( "Chart" );
      if( chart == null )
        GlgComboChart.AppError( "Chart object not found.", false );

      // Store chart range to be able to restore it after zoom.
      low = config_record.low;
      high = config_record.high;
      SetChartRange();
      
      /* Set ChartZoomMode for the viewport, in case it hasn't been set
         in the drawing.
      */
      chart_vp.SetZoomMode( null, chart, null, GlgZoomMode.CHART_ZOOM_MODE );

      // Set Y axis label.
      chart.SetSResource( "YAxis/AxisLabelString", config_record.yaxis_label );

      // Set chart title.
      chart_vp.SetSResource( "Title/String", config_record.chart_title );

      // Initialize the chart's plot.
      InitPlot( config_record.tag_source, config_record.tag_index,
                config_record.plot_color );

      // Set chart's initial visibility.
      chart_vp.SetDResource( "Visibility",
                             config_record.init_visibility ? 1.0 : 0.0 );
      
      /* Initializes label and marker annotations and stores them as
         label_annotation and marker_annotation properties of chart_info.
      */
      InitAnnotations();

      /* Initialize vertical line that moves with the mouse and stores
         its object ID as the cross_hair_line property of chart_info.
         This can be done only for the first chart, since attributes of 
         all vertical lines in all charts are constrained.
      */
      if( i == 0 )
        InitCrossHairLine();
   }
   
   ////////////////////////////////////////////////////////////////////////
   // Set chart's low and high range to the stored values.
   // Assumes the chart has one YAxis and CommonRange=YES.
   ////////////////////////////////////////////////////////////////////////
   public void SetChartRange()
   {
      chart.SetDResource( "YAxis/Low", low );
      chart.SetDResource( "YAxis/High", high );
   }
   
   ////////////////////////////////////////////////////////////////////////
   // Should be invoked after hierarchy setup.
   ////////////////////////////////////////////////////////////////////////
   public void InitPlot( string tag_source, int tag_index, GlgPoint color )
   {
      /* This example assumes that each chart has one plot and one Y axis.
         The code may be extended to support multiple plots and multiple 
         Y axes as needed.
      */
      GlgObject plot_array = chart.GetResourceObject( "Plots" );  
      GlgObject plot = (GlgObject) plot_array.GetElement( 0 );
      this.plot_info = new PlotInfo( plot, tag_source, tag_index, color );
   }
   
   ////////////////////////////////////////////////////////////////////////
   public void InitCrossHairLine()
   {
      GlgObject time_lines = chart.GetResourceObject( "TimeLines" );
      if( time_lines == null )
        return;
       
      /* Retrieve CrossHairLine, used to draw a vertical crosshair line 
         that follows the mouse cursor.
      */
      this.cross_hair_line = time_lines.GetResourceObject( "CrossHairLine" );
      if( cross_hair_line == null )
      {
         GlgComboChart.AppError( "CrossHairLine object not found.", false );
         return;
      }

      // Disable time_line by default. It will be enabled on mouse move.
      cross_hair_line.SetDResource( "Enabled", 0.0 );
   }
   
   ////////////////////////////////////////////////////////////////////////
   public void InitAnnotations()
   {
      /* Retrieve annotation objects, if any. 
         LabelAnnotation moves with the time line and displays time and value 
         of the closest plot data sample. MarkerAnnotation highlights the found 
         data sample, if any.
      */
      GlgObject annotations = chart.GetResourceObject( "Annotations" );
      if( annotations == null )
        return;
      
      this.label_annotation = annotations.GetResourceObject( "LabelAnnotation" );
      if( label_annotation != null )
        label_annotation.SetDResource( "Enabled", 0.0 ); // Disable by default.
      else
        GlgComboChart.AppError( "LabelAnnotation object not found.", false );
      
      this.marker_annotation = annotations.GetResourceObject( "MarkerAnnotation" );
      if( marker_annotation != null )
        marker_annotation.SetDResource( "Enabled", 0.0 ); // Disable by default.
      else
        GlgComboChart.AppError( "MarkerAnnotation object not found.", false );
   }
   
   ////////////////////////////////////////////////////////////////////////
   // Show/hide invididual chart.
   ////////////////////////////////////////////////////////////////////////
   public void ShowChart( bool show )
   {
      chart_vp.SetDResource( "Visibility", show ? 1.0 : 0.0 );
   }
}

