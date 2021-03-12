using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200047F RID: 1151
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Wizard.js")]
	public class SmsSlabProperties : Properties
	{
		// Token: 0x060039C0 RID: 14784 RVA: 0x000AF587 File Offset: 0x000AD787
		public SmsSlabProperties(string disableMethodName, string editSettingPage)
		{
			this.disableMethodName = disableMethodName;
			this.editSettingPage = editSettingPage;
		}

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000AF59D File Offset: 0x000AD79D
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000AF5A5 File Offset: 0x000AD7A5
		public WebServiceMethod DisableWebServiceMethod { get; private set; }

		// Token: 0x060039C3 RID: 14787 RVA: 0x000AF5B0 File Offset: 0x000AD7B0
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.disableMethodName != null)
			{
				this.DisableWebServiceMethod = new WebServiceMethod();
				this.DisableWebServiceMethod.ID = "Disable";
				this.DisableWebServiceMethod.Method = this.disableMethodName;
				this.DisableWebServiceMethod.ServiceUrl = base.ServiceUrl;
				this.DisableWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
				this.Controls.Add(this.DisableWebServiceMethod);
			}
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000AF628 File Offset: 0x000AD828
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "SmsSlabProperties";
			if (this.DisableWebServiceMethod != null)
			{
				scriptDescriptor.AddComponentProperty("DisableWebServiceMethod", this.DisableWebServiceMethod.ClientID);
			}
			scriptDescriptor.AddProperty("EditSettingPage", this.editSettingPage);
			return scriptDescriptor;
		}

		// Token: 0x040026C0 RID: 9920
		private string disableMethodName;

		// Token: 0x040026C1 RID: 9921
		private string editSettingPage;
	}
}
