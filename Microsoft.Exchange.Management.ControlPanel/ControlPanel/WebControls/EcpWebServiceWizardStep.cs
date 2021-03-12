using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005D0 RID: 1488
	public class EcpWebServiceWizardStep : EcpWizardStep
	{
		// Token: 0x06004338 RID: 17208 RVA: 0x000CBBB4 File Offset: 0x000C9DB4
		public EcpWebServiceWizardStep()
		{
			base.ViewModel = "WebServiceWizardStepViewModel";
			this.ShowErrors = true;
			this.NextOnError = false;
		}

		// Token: 0x17002617 RID: 9751
		// (get) Token: 0x06004339 RID: 17209 RVA: 0x000CBBD5 File Offset: 0x000C9DD5
		// (set) Token: 0x0600433A RID: 17210 RVA: 0x000CBBDD File Offset: 0x000C9DDD
		public string WebServiceMethodName { get; set; }

		// Token: 0x17002618 RID: 9752
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x000CBBE6 File Offset: 0x000C9DE6
		// (set) Token: 0x0600433C RID: 17212 RVA: 0x000CBBEE File Offset: 0x000C9DEE
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000CBBF7 File Offset: 0x000C9DF7
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x000CBBFF File Offset: 0x000C9DFF
		public WebServiceParameterNames WebServiceParameterName { get; set; }

		// Token: 0x1700261A RID: 9754
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x000CBC08 File Offset: 0x000C9E08
		// (set) Token: 0x06004340 RID: 17216 RVA: 0x000CBC10 File Offset: 0x000C9E10
		[DefaultValue(true)]
		public bool ShowErrors { get; set; }

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x000CBC19 File Offset: 0x000C9E19
		// (set) Token: 0x06004342 RID: 17218 RVA: 0x000CBC21 File Offset: 0x000C9E21
		[DefaultValue(false)]
		public bool NextOnError { get; set; }

		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x000CBC2A File Offset: 0x000C9E2A
		// (set) Token: 0x06004344 RID: 17220 RVA: 0x000CBC32 File Offset: 0x000C9E32
		[DefaultValue(false)]
		public bool NextOnCancel { get; set; }

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x000CBC3B File Offset: 0x000C9E3B
		// (set) Token: 0x06004346 RID: 17222 RVA: 0x000CBC43 File Offset: 0x000C9E43
		public string ParameterNamesList { get; set; }

		// Token: 0x06004347 RID: 17223 RVA: 0x000CBC4C File Offset: 0x000C9E4C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.Attributes.Add("vm-WebServiceMethodName", this.WebServiceMethodName);
			base.Attributes.Add("vm-WebServiceParameterName", this.WebServiceParameterName.ToJsonString(null));
			base.Attributes.Add("vm-ShowErrors", this.ShowErrors.ToJsonString(null));
			base.Attributes.Add("vm-NextOnError", this.NextOnError.ToJsonString(null));
			base.Attributes.Add("vm-NextOnCancel", this.NextOnCancel.ToJsonString(null));
			base.Attributes.Add("vm-ParameterNamesList", this.ParameterNamesList);
			if (this.ServiceUrl != null)
			{
				base.Attributes.Add("vm-ServiceUrl", EcpUrl.ProcessUrl(this.ServiceUrl.ServiceUrl));
			}
		}
	}
}
