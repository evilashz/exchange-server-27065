using System;
using System.Collections.Generic;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000071 RID: 113
	[ClientScriptResource("CalendarDiagnosticLogProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Calendar.js")]
	public sealed class CalendarDiagnosticLogProperties : Properties, IScriptControl
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x00056421 File Offset: 0x00054621
		public CalendarDiagnosticLogProperties()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0005642F File Offset: 0x0005462F
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x00056437 File Offset: 0x00054637
		public WebServiceMethod SendLogWebServiceMethod { get; private set; }

		// Token: 0x06001B0B RID: 6923 RVA: 0x00056440 File Offset: 0x00054640
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.SendLogWebServiceMethod = new WebServiceMethod();
			this.SendLogWebServiceMethod.ID = "SendCalendarDiagnosticLog";
			this.SendLogWebServiceMethod.ServiceUrl = base.ServiceUrl;
			this.SendLogWebServiceMethod.Method = "SendLog";
			this.SendLogWebServiceMethod.ParameterNames = WebServiceParameterNames.Custom;
			base.ContentContainer.Controls.Add(this.SendLogWebServiceMethod);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000564B4 File Offset: 0x000546B4
		protected override void OnPreRender(EventArgs e)
		{
			InvokeWebService invokeWebService = new InvokeWebService();
			invokeWebService.ID = "webServiceBehavior";
			invokeWebService.TargetControlID = "btnTroubleShoot";
			invokeWebService.EnableProgressPopup = true;
			invokeWebService.CloseAfterSuccess = true;
			invokeWebService.WebServiceMethods.Add(this.SendLogWebServiceMethod);
			invokeWebService.ProgressDescription = OwaOptionStrings.Processing;
			base.ContentContainer.Controls.Add(invokeWebService);
			base.OnPreRender(e);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00056524 File Offset: 0x00054724
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptDescriptor = this.GetScriptDescriptor();
			scriptDescriptor.Type = "CalendarDiagnosticLogProperties";
			if (this.SendLogWebServiceMethod != null)
			{
				scriptDescriptor.AddComponentProperty("SendLogWebServiceMethod", this.SendLogWebServiceMethod.ClientID);
			}
			return new ScriptControlDescriptor[]
			{
				scriptDescriptor
			};
		}
	}
}
