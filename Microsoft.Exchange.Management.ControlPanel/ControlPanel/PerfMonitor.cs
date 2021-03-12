using System;
using System.Web.Configuration;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000280 RID: 640
	[ClientScriptResource("PerfMonitor", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class PerfMonitor : ScriptComponent
	{
		// Token: 0x060029E5 RID: 10725 RVA: 0x00083AD4 File Offset: 0x00081CD4
		protected override void OnLoad(EventArgs e)
		{
			bool flag = StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["ShowPerformanceConsole"]);
			this.Visible = (StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["LogPerformanceData"]) || flag);
			base.OnLoad(e);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00083B33 File Offset: 0x00081D33
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			PerfRecord.Current.EndRequest();
			descriptor.AddScriptProperty("PerfRecord", PerfRecord.Current.ToString());
		}
	}
}
