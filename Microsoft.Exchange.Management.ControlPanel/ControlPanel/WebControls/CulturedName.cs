using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200057E RID: 1406
	public class CulturedName : WebControl, INamingContainer
	{
		// Token: 0x17002556 RID: 9558
		// (get) Token: 0x0600415B RID: 16731 RVA: 0x000C7279 File Offset: 0x000C5479
		// (set) Token: 0x0600415C RID: 16732 RVA: 0x000C7281 File Offset: 0x000C5481
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate FirstNameTemplate { get; set; }

		// Token: 0x17002557 RID: 9559
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x000C728A File Offset: 0x000C548A
		// (set) Token: 0x0600415E RID: 16734 RVA: 0x000C7292 File Offset: 0x000C5492
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate InitialTemplate { get; set; }

		// Token: 0x17002558 RID: 9560
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x000C729B File Offset: 0x000C549B
		// (set) Token: 0x06004160 RID: 16736 RVA: 0x000C72A3 File Offset: 0x000C54A3
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate LastNameTemplate { get; set; }

		// Token: 0x06004161 RID: 16737 RVA: 0x000C72AC File Offset: 0x000C54AC
		public CulturedName()
		{
			this.CssClass = "divEncapsulation";
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000C72C0 File Offset: 0x000C54C0
		protected override void CreateChildControls()
		{
			CulturedHelper.CreateChildControls(this, "FirstName,Initials,LastName", 0, new Dictionary<string, ITemplate>
			{
				{
					"FirstName",
					this.FirstNameTemplate
				},
				{
					"Initials",
					this.InitialTemplate
				},
				{
					"LastName",
					this.LastNameTemplate
				}
			});
		}

		// Token: 0x04002B44 RID: 11076
		public const string DefaultPattern = "FirstName,Initials,LastName";
	}
}
