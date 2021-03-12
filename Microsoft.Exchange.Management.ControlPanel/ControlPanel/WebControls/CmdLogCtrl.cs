using System;
using System.Web;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200004C RID: 76
	[ClientScriptResource("CmdLogCtrl", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class CmdLogCtrl : ScriptComponent
	{
		// Token: 0x060019C0 RID: 6592 RVA: 0x00052910 File Offset: 0x00050B10
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			CmdExecuteInfo[] cmdletLogs = HttpContext.Current.GetCmdletLogs();
			if (cmdletLogs != null)
			{
				descriptor.AddScriptProperty("Infos", cmdletLogs.ToJsonString(null));
			}
		}
	}
}
