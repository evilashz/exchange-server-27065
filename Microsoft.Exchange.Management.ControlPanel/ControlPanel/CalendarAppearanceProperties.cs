using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200006D RID: 109
	[ClientScriptResource("CalendarAppearanceProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Calendar.js")]
	public sealed class CalendarAppearanceProperties : Properties
	{
		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000562E0 File Offset: 0x000544E0
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x000562E8 File Offset: 0x000544E8
		public WebServiceMethod UpdateWebServiceMethod { get; private set; }

		// Token: 0x06001AFD RID: 6909 RVA: 0x000562F4 File Offset: 0x000544F4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.UpdateWebServiceMethod = new WebServiceMethod();
			this.UpdateWebServiceMethod.ID = "UpdateTimeZone";
			this.UpdateWebServiceMethod.ServiceUrl = base.ServiceUrl;
			this.UpdateWebServiceMethod.Method = "UpdateObject";
			this.UpdateWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
			this.Controls.Add(this.UpdateWebServiceMethod);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00056360 File Offset: 0x00054560
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "CalendarAppearanceProperties";
			if (this.UpdateWebServiceMethod != null)
			{
				scriptDescriptor.AddComponentProperty("UpdateWebServiceMethod", this.UpdateWebServiceMethod.ClientID);
			}
			return scriptDescriptor;
		}
	}
}
