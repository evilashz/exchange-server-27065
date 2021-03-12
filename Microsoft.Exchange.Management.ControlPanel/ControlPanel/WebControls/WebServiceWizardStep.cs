using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000686 RID: 1670
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("WebServiceWizardStep", "Microsoft.Exchange.Management.ControlPanel.Client.Wizard.js")]
	public class WebServiceWizardStep : WizardStep
	{
		// Token: 0x0600482D RID: 18477 RVA: 0x000DB80F File Offset: 0x000D9A0F
		public WebServiceWizardStep()
		{
			base.ClientClassName = "WebServiceWizardStep";
			this.ShowErrors = true;
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x170027AA RID: 10154
		// (get) Token: 0x0600482E RID: 18478 RVA: 0x000DB82F File Offset: 0x000D9A2F
		// (set) Token: 0x0600482F RID: 18479 RVA: 0x000DB837 File Offset: 0x000D9A37
		public string WebServiceMethodName { get; set; }

		// Token: 0x170027AB RID: 10155
		// (get) Token: 0x06004830 RID: 18480 RVA: 0x000DB840 File Offset: 0x000D9A40
		// (set) Token: 0x06004831 RID: 18481 RVA: 0x000DB848 File Offset: 0x000D9A48
		public string ParameterNames { get; set; }

		// Token: 0x170027AC RID: 10156
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x000DB851 File Offset: 0x000D9A51
		// (set) Token: 0x06004833 RID: 18483 RVA: 0x000DB859 File Offset: 0x000D9A59
		public string ProgressDescription { get; set; }

		// Token: 0x170027AD RID: 10157
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x000DB862 File Offset: 0x000D9A62
		// (set) Token: 0x06004835 RID: 18485 RVA: 0x000DB86A File Offset: 0x000D9A6A
		[DefaultValue(false)]
		public bool NextOnCancel { get; set; }

		// Token: 0x170027AE RID: 10158
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x000DB873 File Offset: 0x000D9A73
		// (set) Token: 0x06004837 RID: 18487 RVA: 0x000DB87B File Offset: 0x000D9A7B
		[DefaultValue(false)]
		public bool NextOnError { get; set; }

		// Token: 0x170027AF RID: 10159
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x000DB884 File Offset: 0x000D9A84
		// (set) Token: 0x06004839 RID: 18489 RVA: 0x000DB88C File Offset: 0x000D9A8C
		[DefaultValue(true)]
		public bool ShowErrors { get; set; }

		// Token: 0x0600483A RID: 18490 RVA: 0x000DB898 File Offset: 0x000D9A98
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.propertyPage = base.FindPropertiesParent();
			this.webServiceMethod = new WebServiceMethod();
			this.webServiceMethod.ServiceUrl = this.propertyPage.ServiceUrl;
			this.webServiceMethod.ID = this.ID + "WebServiceMethod";
			this.webServiceMethod.Method = this.WebServiceMethodName;
			if (string.IsNullOrEmpty(this.ParameterNames))
			{
				this.webServiceMethod.ParameterNames = WebServiceParameterNames.SetObject;
			}
			else
			{
				this.webServiceMethod.ParameterNames = (WebServiceParameterNames)Enum.Parse(typeof(WebServiceParameterNames), this.ParameterNames);
			}
			this.Controls.Add(this.webServiceMethod);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x000DB958 File Offset: 0x000D9B58
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.AddComponentProperty("PropertyPage", this.propertyPage.ClientID);
			scriptDescriptor.AddComponentProperty("WebServiceMethod", this.webServiceMethod.ClientID);
			scriptDescriptor.AddProperty("ProgressDescription", this.ProgressDescription ?? Strings.PleaseWait);
			scriptDescriptor.AddProperty("NextOnCancel", this.NextOnCancel);
			scriptDescriptor.AddProperty("NextOnError", this.NextOnError);
			scriptDescriptor.AddProperty("ShowErrors", this.ShowErrors);
			return scriptDescriptor;
		}

		// Token: 0x04003073 RID: 12403
		private Properties propertyPage;

		// Token: 0x04003074 RID: 12404
		private WebServiceMethod webServiceMethod;
	}
}
