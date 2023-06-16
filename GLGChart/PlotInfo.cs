using System;
using GenLogic;
              
public class PlotInfo
{
    public string tag_source;       // Datasource variable name.
    public int tag_index;           /* Datasource variable index in the
                                       external data array. */
    public GlgObject plot;          // ObjectID of Plot#0.
    public GlgObject value_ep;      // Plot's ValueEntryPoint.
    public GlgObject time_ep;       // Plot's TimeEntryPoint.
    public GlgObject valid_ep;      // Plot's ValidEntryPoint.

    // Default constructor.
    public PlotInfo()
    {
        tag_index = -1;
    }

    // Constructor.
    public PlotInfo( GlgObject plot, string tag_source, int tag_index,
                     GlgPoint color )
    {
       this.plot = plot;
       if( plot == null )
         GlgComboChart.AppError( "Invalid plot object.", false );
       
       value_ep = plot.GetResourceObject( "ValueEntryPoint" );
       time_ep = plot.GetResourceObject( "TimeEntryPoint" );
       valid_ep = plot.GetResourceObject( "ValidEntryPoint" );
       
       // Assign plot's color.
       if( color != null )
         plot.SetGResource( "EdgeColor", color );
       
       this.tag_source = tag_source;
       
       // Assign TagSource for the plot's ValueEntryPoint, if any.
       AssignPlotTag( tag_source );
       
       this.tag_index = tag_index;
    }
   
   // Assign TagSource for the plot's ValueEntryPoint, if any.
   public void AssignPlotTag( string tag_source )
   {
      if( GlgComboChart.IsUndefined( tag_source ) )
        return;
      
      if( value_ep.HasResourceObject( "TagObject" ) )
        value_ep.SetSResource( "TagSource", tag_source );   
   }
}
