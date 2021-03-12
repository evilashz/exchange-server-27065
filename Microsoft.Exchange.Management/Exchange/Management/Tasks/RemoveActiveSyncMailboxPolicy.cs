using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000433 RID: 1075
	[Cmdlet("remove", "ActiveSyncMailboxPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveActiveSyncMailboxPolicy : RemoveMailboxPolicyBase<ActiveSyncMailboxPolicy>
	{
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x00097DE2 File Offset: 0x00095FE2
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x00097DEA File Offset: 0x00095FEA
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return this.force;
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x00097DF3 File Offset: 0x00095FF3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.DataObject.IsDefault)
				{
					return Strings.ConfirmationMessageRemoveDefaultActiveSyncMailboxPolicy(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageRemoveActiveSyncMailboxPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00097E23 File Offset: 0x00096023
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			return this.Force || base.ShouldContinue(Strings.WarningRemovePolicyWithAssociatedUsers(base.DataObject.Name));
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00097E4A File Offset: 0x0009604A
		protected override void InternalProcessRecord()
		{
			this.WriteWarning(Strings.WarningCmdletIsDeprecated("Remove-ActiveSyncMailboxPolicy", "Remove-MobileMailboxPolicy"));
			base.InternalProcessRecord();
		}

		// Token: 0x04001D79 RID: 7545
		private SwitchParameter force;
	}
}
