using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000247 RID: 583
	[Cmdlet("Set", "ExchangeUpgradeBucket", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetExchangeUpgradeBucket : SetTopologySystemConfigurationObjectTask<ExchangeUpgradeBucketIdParameter, ExchangeUpgradeBucket>
	{
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0005B86B File Offset: 0x00059A6B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetExchangeUpgradeBucket(this.DataObject.Name);
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0005B880 File Offset: 0x00059A80
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.IsChanged(ExchangeUpgradeBucketSchema.MaxMailboxes) && !this.DataObject.MaxMailboxes.IsUnlimited && this.DataObject.Organizations.Count > 0)
			{
				int mailboxCount = UpgradeBucketTaskHelper.GetMailboxCount(this.DataObject);
				if (this.DataObject.MaxMailboxes.Value < mailboxCount)
				{
					base.WriteError(new RecipientTaskException(Strings.ExchangeUpgradeBucketInvalidCapacityValue), ExchangeErrorCategory.Client, this.DataObject);
				}
			}
			TaskLogger.LogExit();
		}
	}
}
