using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Net.Protocols.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000037 RID: 55
	[Cmdlet("Set", "HotmailSubscription", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHotmailSubscription : SetSubscriptionBase<HotmailSubscriptionProxy>
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.SetHotmailSubscriptionConfirmation(this.Identity);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000ACBF File Offset: 0x00008EBF
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				return AggregationSubscriptionType.DeltaSyncMail;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000ACC2 File Offset: 0x00008EC2
		private HotmailSubscriptionProxy DynamicParameters
		{
			get
			{
				return (HotmailSubscriptionProxy)this.GetDynamicParameters();
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		protected override void ValidateWithDataObject(IConfigurable dataObject)
		{
			base.ValidateWithDataObject(dataObject);
			if (this.Password != null)
			{
				DeltaSyncUserAccount account = DeltaSyncUserAccount.CreateDeltaSyncUserForTrustedPartnerAuthWithPassword(((HotmailSubscriptionProxy)dataObject).EmailAddress.ToString(), SyncUtilities.SecureStringToString(this.Password));
				LocalizedException exception;
				if (!DeltaSyncAutoProvision.ValidateUserHotmailAccount(account, CommonLoggingHelper.SyncLogSession, out this.accountSettings, out exception))
				{
					base.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, null);
				}
			}
			base.WriteDebugInfo();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000AD3C File Offset: 0x00008F3C
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			base.StampChangesOn(dataObject);
			HotmailSubscriptionProxy hotmailSubscriptionProxy = dataObject as HotmailSubscriptionProxy;
			DeltaSyncAggregationSubscription deltaSyncAggregationSubscription = hotmailSubscriptionProxy.Subscription as DeltaSyncAggregationSubscription;
			if (this.accountSettings != null)
			{
				DeltaSyncAutoProvision.UpdateSubscriptionSettings(this.accountSettings, ref deltaSyncAggregationSubscription);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400009F RID: 159
		private DeltaSyncSettings accountSettings;
	}
}
