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
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200002A RID: 42
	[Cmdlet("New", "HotmailSubscription", SupportsShouldProcess = true, DefaultParameterSetName = "AggregationParameterSet")]
	public sealed class NewHotmailSubscription : NewSubscriptionBase<HotmailSubscriptionProxy>
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000088C5 File Offset: 0x00006AC5
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000088CD File Offset: 0x00006ACD
		[Parameter(Mandatory = true, ParameterSetName = "AggregationParameterSet")]
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000088D6 File Offset: 0x00006AD6
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000088FB File Offset: 0x00006AFB
		[Parameter(Mandatory = true, ParameterSetName = "AggregationParameterSet")]
		public new SmtpAddress EmailAddress
		{
			get
			{
				return (SmtpAddress)(base.Fields["EmailAddress"] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields["EmailAddress"] = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00008913 File Offset: 0x00006B13
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000892A File Offset: 0x00006B2A
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "AggregationParameterSet")]
		public new string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000893D File Offset: 0x00006B3D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.CreateHotmailSubscriptionConfirmation(this.DataObject);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000894C File Offset: 0x00006B4C
		protected override IConfigurable PrepareDataObject()
		{
			HotmailSubscriptionProxy hotmailSubscriptionProxy = base.PrepareDataObject() as HotmailSubscriptionProxy;
			if (this.accountSettings != null)
			{
				DeltaSyncAggregationSubscription deltaSyncAggregationSubscription = hotmailSubscriptionProxy.Subscription as DeltaSyncAggregationSubscription;
				DeltaSyncAutoProvision.UpdateSubscriptionSettings(this.accountSettings, ref deltaSyncAggregationSubscription);
			}
			return hotmailSubscriptionProxy;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008988 File Offset: 0x00006B88
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			DeltaSyncUserAccount deltaSyncUserAccount = DeltaSyncUserAccount.CreateDeltaSyncUserForTrustedPartnerAuthWithPassword(this.EmailAddress.ToString(), SyncUtilities.SecureStringToString(this.Password));
			LocalizedException exception;
			if (!DeltaSyncAutoProvision.ValidateUserHotmailAccount(deltaSyncUserAccount, CommonLoggingHelper.SyncLogSession, out this.accountSettings, out exception))
			{
				base.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, null);
			}
			this.DataObject.SetLiveAccountPuid(deltaSyncUserAccount.AuthToken.PUID);
			base.WriteDebugInfo();
			TaskLogger.LogExit();
		}

		// Token: 0x04000086 RID: 134
		private DeltaSyncSettings accountSettings;
	}
}
