using System;
using System.Collections.Generic;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000261 RID: 609
	[ClientScriptResource("AutomaticRepliesProperties", "Microsoft.Exchange.Management.ControlPanel.Client.AutomaticReplies.js")]
	public sealed class AutomaticRepliesProperties : Properties, IScriptControl
	{
		// Token: 0x0600290B RID: 10507 RVA: 0x00081124 File Offset: 0x0007F324
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptDescriptor = this.GetScriptDescriptor();
			scriptDescriptor.Type = "AutomaticRepliesProperties";
			this.EnsureChildControls();
			this.AddComponentPropertyIfVisible(scriptDescriptor, "StartTimePicker", "dtpStartTime");
			this.AddComponentPropertyIfVisible(scriptDescriptor, "EndTimePicker", "dtpEndTime");
			this.AddComponentPropertyIfVisible(scriptDescriptor, "InternalMessageEditor", "rteInternalMessage");
			this.AddComponentPropertyIfVisible(scriptDescriptor, "ExternalMessageEditor", "rteExternalMessage");
			return new ScriptControlDescriptor[]
			{
				scriptDescriptor
			};
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0008119C File Offset: 0x0007F39C
		private void AddComponentPropertyIfVisible(ScriptControlDescriptor descriptor, string name, string controlName)
		{
			Control control = base.ContentContainer.FindControl(controlName);
			if (control != null && control.Visible)
			{
				descriptor.AddComponentProperty(name, control.ClientID);
			}
		}
	}
}
