using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000584 RID: 1412
	[ClientScriptResource("DatabasesWebServiceListSource", "Microsoft.Exchange.Management.ControlPanel.Client.OrgSettings.js")]
	public class DatabasesWebServiceListSource : WebServiceListSource
	{
		// Token: 0x17002565 RID: 9573
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x000C7B65 File Offset: 0x000C5D65
		// (set) Token: 0x06004180 RID: 16768 RVA: 0x000C7B6D File Offset: 0x000C5D6D
		[DefaultValue(null)]
		public string DeferLoadWorkflowName { get; set; }

		// Token: 0x06004181 RID: 16769 RVA: 0x000C7B78 File Offset: 0x000C5D78
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
			ScriptControlDescriptor scriptControlDescriptor = (ScriptControlDescriptor)scriptDescriptors.First<ScriptDescriptor>();
			scriptControlDescriptor.AddProperty("DeferLoadWorkflowName", this.DeferLoadWorkflowName);
			return scriptDescriptors;
		}
	}
}
