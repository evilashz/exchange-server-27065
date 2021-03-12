using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200002A RID: 42
	public abstract class RemoveDistributionGroupBase : RemoveRecipientObjectTask<DistributionGroupIdParameter, ADGroup>
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A709 File Offset: 0x00008909
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDistributionGroup(this.Identity.ToString());
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A71B File Offset: 0x0000891B
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000A741 File Offset: 0x00008941
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassSecurityGroupManagerCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassSecurityGroupManagerCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassSecurityGroupManagerCheck"] = value;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A75C File Offset: 0x0000895C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.DataObject.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || base.DataObject.RecipientTypeDetails == RecipientTypeDetails.RemoteGroupMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.NotAValidDistributionGroup), ExchangeErrorCategory.Client, this.Identity.ToString());
			}
			DistributionGroupMemberTaskBase<DistributionGroupIdParameter>.GetExecutingUserAndCheckGroupOwnership(this, (IDirectorySession)base.DataSession, base.TenantGlobalCatalogSession, base.DataObject, this.BypassSecurityGroupManagerCheck);
		}
	}
}
