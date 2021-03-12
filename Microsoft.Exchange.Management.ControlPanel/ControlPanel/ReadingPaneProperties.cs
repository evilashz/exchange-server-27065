using System;
using System.Collections.Generic;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A1 RID: 161
	[ClientScriptResource("ReadingPaneProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Messaging.js")]
	public sealed class ReadingPaneProperties : Properties, IScriptControl
	{
		// Token: 0x06001C06 RID: 7174 RVA: 0x00057CE8 File Offset: 0x00055EE8
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptDescriptor = this.GetScriptDescriptor();
			scriptDescriptor.Type = "ReadingPaneProperties";
			return new ScriptControlDescriptor[]
			{
				scriptDescriptor
			};
		}
	}
}
