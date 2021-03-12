using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200007B RID: 123
	[ClientScriptResource("CalendarWorkflowProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Calendar.js")]
	public sealed class CalendarWorkflowProperties : Properties
	{
		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00056B40 File Offset: 0x00054D40
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x00056B48 File Offset: 0x00054D48
		public WebServiceMethod UpdateWebServiceMethod { get; private set; }

		// Token: 0x06001B43 RID: 6979 RVA: 0x00056B54 File Offset: 0x00054D54
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.UpdateWebServiceMethod = new WebServiceMethod();
			this.UpdateWebServiceMethod.ID = "EnableAutomateProcessing";
			this.UpdateWebServiceMethod.ServiceUrl = base.ServiceUrl;
			this.UpdateWebServiceMethod.Method = "UpdateObject";
			this.UpdateWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
			this.Controls.Add(this.UpdateWebServiceMethod);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00056BC0 File Offset: 0x00054DC0
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "CalendarWorkflowProperties";
			if (this.UpdateWebServiceMethod != null)
			{
				scriptDescriptor.AddComponentProperty("UpdateWebServiceMethod", this.UpdateWebServiceMethod.ClientID);
			}
			return scriptDescriptor;
		}
	}
}
