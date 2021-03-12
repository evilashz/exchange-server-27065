using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200048A RID: 1162
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.SMSProperties.js")]
	public class SmsWizardProperties : Properties
	{
		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000B0218 File Offset: 0x000AE418
		// (set) Token: 0x06003A16 RID: 14870 RVA: 0x000B0220 File Offset: 0x000AE420
		public WebServiceMethod SendVerificationCodeWebServiceMethod { get; private set; }

		// Token: 0x06003A17 RID: 14871 RVA: 0x000B022C File Offset: 0x000AE42C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.SendVerificationCodeWebServiceMethod = new WebServiceMethod();
			this.SendVerificationCodeWebServiceMethod.ID = "SendVerificationCode";
			this.SendVerificationCodeWebServiceMethod.Method = "SendVerificationCode";
			this.SendVerificationCodeWebServiceMethod.ServiceUrl = base.ServiceUrl;
			this.SendVerificationCodeWebServiceMethod.ParameterNames = WebServiceParameterNames.SetObject;
			this.Controls.Add(this.SendVerificationCodeWebServiceMethod);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000B0298 File Offset: 0x000AE498
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "SmsWizardProperties";
			scriptDescriptor.AddProperty("ServiceProviders", SmsServiceProviders.Instance);
			if (this.IsNotificationWizard)
			{
				scriptDescriptor.AddProperty("IsNotificationWizard", true);
			}
			scriptDescriptor.AddComponentProperty("SendVerificationCodeWebServiceMethod", this.SendVerificationCodeWebServiceMethod.ClientID);
			return scriptDescriptor;
		}

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000B02F7 File Offset: 0x000AE4F7
		// (set) Token: 0x06003A1A RID: 14874 RVA: 0x000B02FF File Offset: 0x000AE4FF
		public bool IsNotificationWizard { get; set; }
	}
}
