using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200020F RID: 527
	[ClientScriptResource("CrossPremise", "Microsoft.Exchange.Management.ControlPanel.Client.Navigation.js")]
	public class CrossPremise : ScriptComponent
	{
		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00079713 File Offset: 0x00077913
		// (set) Token: 0x060026E4 RID: 9956 RVA: 0x0007971B File Offset: 0x0007791B
		public string Feature { get; set; }

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x00079724 File Offset: 0x00077924
		// (set) Token: 0x060026E6 RID: 9958 RVA: 0x0007972C File Offset: 0x0007792C
		public string LogoutHelperPage { get; set; }

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x00079735 File Offset: 0x00077935
		// (set) Token: 0x060026E8 RID: 9960 RVA: 0x0007973D File Offset: 0x0007793D
		public bool OnPremiseFrameVisible { get; set; }

		// Token: 0x060026E9 RID: 9961 RVA: 0x00079748 File Offset: 0x00077948
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("Feature", this.Feature);
			descriptor.AddProperty("OnPremiseFrameVisible", this.OnPremiseFrameVisible);
			descriptor.AddProperty("LogoutHelperPage", this.LogoutHelperPage);
		}
	}
}
