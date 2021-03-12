using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005FB RID: 1531
	[TargetControlType(typeof(Control))]
	[ClientScriptResource("InvokeWebService", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class InvokeWebService : ExtenderControlBase
	{
		// Token: 0x060044A5 RID: 17573 RVA: 0x000CF69E File Offset: 0x000CD89E
		public InvokeWebService()
		{
			this.Trigger = "click";
			this.WebServiceMethods = new List<WebServiceMethod>();
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000CF6BC File Offset: 0x000CD8BC
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.Trigger != "click")
			{
				descriptor.AddProperty("Trigger", this.Trigger);
			}
			if (this.EnableConfirmation)
			{
				descriptor.AddProperty("EnableConfirmation", true);
			}
			if (this.IsSaveMethod)
			{
				descriptor.AddProperty("IsSaveMethod", true);
			}
			if (this.EnableProgressPopup)
			{
				descriptor.AddProperty("EnableProgressPopup", true);
			}
			descriptor.AddProperty("ProgressDescription", this.ProgressDescription, true);
			descriptor.AddProperty("AssociateElementID", this.AssociateElementID, true);
			this.CheckWebServiceErrorHandlers(this.WebServiceMethods);
			descriptor.AddProperty("WebServiceMethodIDs", this.WebServiceMethodIDs, true);
			if (this.CloseAfterSuccess)
			{
				descriptor.AddProperty("CloseAfterSuccess", true);
			}
		}

		// Token: 0x17002686 RID: 9862
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x000CF799 File Offset: 0x000CD999
		// (set) Token: 0x060044A8 RID: 17576 RVA: 0x000CF7A1 File Offset: 0x000CD9A1
		public string Trigger { get; set; }

		// Token: 0x17002687 RID: 9863
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x000CF7AA File Offset: 0x000CD9AA
		// (set) Token: 0x060044AA RID: 17578 RVA: 0x000CF7B2 File Offset: 0x000CD9B2
		public bool EnableConfirmation { get; set; }

		// Token: 0x17002688 RID: 9864
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000CF7BB File Offset: 0x000CD9BB
		// (set) Token: 0x060044AC RID: 17580 RVA: 0x000CF7C3 File Offset: 0x000CD9C3
		public bool IsSaveMethod { get; set; }

		// Token: 0x17002689 RID: 9865
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x000CF7CC File Offset: 0x000CD9CC
		// (set) Token: 0x060044AE RID: 17582 RVA: 0x000CF7D4 File Offset: 0x000CD9D4
		public bool EnableProgressPopup { get; set; }

		// Token: 0x1700268A RID: 9866
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x000CF7DD File Offset: 0x000CD9DD
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x000CF7E5 File Offset: 0x000CD9E5
		public string ProgressDescription { get; set; }

		// Token: 0x1700268B RID: 9867
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x000CF7EE File Offset: 0x000CD9EE
		// (set) Token: 0x060044B2 RID: 17586 RVA: 0x000CF7F6 File Offset: 0x000CD9F6
		public string AssociateElementID { get; set; }

		// Token: 0x1700268C RID: 9868
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x000CF807 File Offset: 0x000CDA07
		public string WebServiceMethodIDs
		{
			get
			{
				return string.Join(",", (from saveMethod in this.WebServiceMethods
				select saveMethod.ClientID).ToArray<string>());
			}
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000CF840 File Offset: 0x000CDA40
		private void CheckWebServiceErrorHandlers(List<WebServiceMethod> webServices)
		{
			int num = 0;
			foreach (WebServiceMethod webServiceMethod in webServices)
			{
				if (webServiceMethod.ExceptionHandlers.Count > 0 && ++num > 1)
				{
					throw new NotSupportedException("Current we only allow one webservice to have handler.");
				}
			}
		}

		// Token: 0x1700268D RID: 9869
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x000CF8AC File Offset: 0x000CDAAC
		// (set) Token: 0x060044B6 RID: 17590 RVA: 0x000CF8B4 File Offset: 0x000CDAB4
		public List<WebServiceMethod> WebServiceMethods { get; private set; }

		// Token: 0x1700268E RID: 9870
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x000CF8BD File Offset: 0x000CDABD
		// (set) Token: 0x060044B8 RID: 17592 RVA: 0x000CF8C5 File Offset: 0x000CDAC5
		public bool CloseAfterSuccess { get; set; }
	}
}
