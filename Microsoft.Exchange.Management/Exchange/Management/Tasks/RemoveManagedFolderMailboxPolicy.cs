using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200043C RID: 1084
	[Cmdlet("remove", "ManagedFolderMailboxPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveManagedFolderMailboxPolicy : RemoveMailboxPolicyBase<ManagedFolderMailboxPolicy>
	{
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x00098529 File Offset: 0x00096729
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveManagedFolderMailboxPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x0009853B File Offset: 0x0009673B
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x00098543 File Offset: 0x00096743
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x06002624 RID: 9764 RVA: 0x0009854C File Offset: 0x0009674C
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			return this.Force || base.ShouldContinue(Strings.WarningRemovePolicyWithAssociatedUsers(base.DataObject.Name));
		}
	}
}
