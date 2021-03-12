using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200050F RID: 1295
	[DataContract]
	public class MailboxPlan : DropDownItemData
	{
		// Token: 0x06003E13 RID: 15891 RVA: 0x000BA758 File Offset: 0x000B8958
		public MailboxPlan(MailboxPlan plan)
		{
			base.Text = plan.DisplayName;
			base.Value = plan.Name;
			base.Selected = plan.IsDefault;
			this.RoleAssignmentPolicy = plan.RoleAssignmentPolicy;
			this.MailboxPlanIndex = plan.MailboxPlanIndex;
		}

		// Token: 0x1700245A RID: 9306
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000BA7A7 File Offset: 0x000B89A7
		// (set) Token: 0x06003E15 RID: 15893 RVA: 0x000BA7AF File Offset: 0x000B89AF
		internal ADObjectId RoleAssignmentPolicy { get; private set; }

		// Token: 0x1700245B RID: 9307
		// (get) Token: 0x06003E16 RID: 15894 RVA: 0x000BA7B8 File Offset: 0x000B89B8
		// (set) Token: 0x06003E17 RID: 15895 RVA: 0x000BA7C0 File Offset: 0x000B89C0
		internal string MailboxPlanIndex { get; private set; }
	}
}
