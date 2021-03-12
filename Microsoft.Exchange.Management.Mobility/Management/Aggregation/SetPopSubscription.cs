using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200003B RID: 59
	[Cmdlet("Set", "PopSubscription", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetPopSubscription : SetSubscriptionSendAsVerifiedBase<PopSubscriptionProxy>
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000B4D5 File Offset: 0x000096D5
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000B4EC File Offset: 0x000096EC
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public AuthenticationMechanism IncomingAuth
		{
			get
			{
				return (AuthenticationMechanism)base.Fields["IncomingAuth"];
			}
			set
			{
				base.Fields["IncomingAuth"] = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B504 File Offset: 0x00009704
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000B51B File Offset: 0x0000971B
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public SecurityMechanism IncomingSecurity
		{
			get
			{
				return (SecurityMechanism)base.Fields["IncomingSecurity"];
			}
			set
			{
				base.Fields["IncomingSecurity"] = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B533 File Offset: 0x00009733
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000B54A File Offset: 0x0000974A
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public bool LeaveOnServer
		{
			get
			{
				return (bool)base.Fields["LeaveOnServer"];
			}
			set
			{
				base.Fields["LeaveOnServer"] = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B562 File Offset: 0x00009762
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.SetPopSubscriptionConfirmation(this.Identity);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B56F File Offset: 0x0000976F
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				return AggregationSubscriptionType.Pop;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000B572 File Offset: 0x00009772
		private PopSubscriptionProxy DynamicParameters
		{
			get
			{
				return (PopSubscriptionProxy)this.GetDynamicParameters();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B580 File Offset: 0x00009780
		protected override void ValidateWithDataObject(IConfigurable dataObject)
		{
			base.ValidateWithDataObject(dataObject);
			PopSubscriptionProxy popSubscriptionProxy = (PopSubscriptionProxy)dataObject;
			AuthenticationMechanism authenticationMechanism = base.Fields.IsModified("IncomingAuth") ? this.IncomingAuth : popSubscriptionProxy.IncomingAuthentication;
			SecureString password = this.password ?? popSubscriptionProxy.Subscription.LogonPasswordSecured;
			string text = base.Fields.IsModified("IncomingUserName") ? base.IncomingUserName : popSubscriptionProxy.IncomingUserName;
			AggregationTaskUtils.ValidateUserName(text, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			if (authenticationMechanism == AuthenticationMechanism.Basic)
			{
				AggregationTaskUtils.ValidateUnicodeInfoOnUserNameAndPassword(text, password, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			}
			string text2 = base.Fields.IsModified("IncomingServer") ? base.IncomingServer : popSubscriptionProxy.IncomingServer;
			AggregationTaskUtils.ValidateIncomingServerLength(text2, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			if (!base.ShouldSkipAccountValidation())
			{
				bool leaveOnServer = base.Fields.IsModified("LeaveOnServer") ? this.LeaveOnServer : popSubscriptionProxy.LeaveOnServer;
				int port = base.Fields.IsModified("IncomingPort") ? base.IncomingPort : popSubscriptionProxy.IncomingPort;
				SecurityMechanism security = base.Fields.IsModified("IncomingSecurity") ? this.IncomingSecurity : popSubscriptionProxy.IncomingSecurity;
				LocalizedException exception;
				if (!Pop3AutoProvision.ValidatePopSettings(leaveOnServer, popSubscriptionProxy.AggregationType == AggregationType.Mirrored, text2, port, text, password, authenticationMechanism, security, popSubscriptionProxy.Subscription.UserLegacyDN, CommonLoggingHelper.SyncLogSession, out exception))
				{
					base.WriteDebugInfoAndError(exception, (ErrorCategory)1003, dataObject);
				}
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B704 File Offset: 0x00009904
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			base.StampChangesOn(dataObject);
			base.NeedsSendAsCheck = false;
			PopSubscriptionProxy popSubscriptionProxy = dataObject as PopSubscriptionProxy;
			if (base.Fields.IsModified("EmailAddress"))
			{
				base.NeedsSendAsCheck = true;
				popSubscriptionProxy.EmailAddress = base.EmailAddress;
			}
			if (base.Fields.IsModified("IncomingUserName"))
			{
				base.NeedsSendAsCheck = true;
				popSubscriptionProxy.IncomingUserName = base.IncomingUserName;
			}
			if (base.Fields.IsModified("IncomingServer"))
			{
				base.NeedsSendAsCheck = true;
				popSubscriptionProxy.IncomingServer = base.IncomingServer;
			}
			if (base.Fields.IsModified("IncomingPort"))
			{
				popSubscriptionProxy.IncomingPort = base.IncomingPort;
			}
			if (base.Fields.IsModified("IncomingAuth"))
			{
				popSubscriptionProxy.IncomingAuthentication = this.IncomingAuth;
			}
			if (base.Fields.IsModified("IncomingSecurity"))
			{
				popSubscriptionProxy.IncomingSecurity = this.IncomingSecurity;
			}
			if (base.Fields.IsModified("LeaveOnServer"))
			{
				popSubscriptionProxy.LeaveOnServer = this.LeaveOnServer;
			}
			if (this.password != null)
			{
				popSubscriptionProxy.SetPassword(this.password);
			}
			TaskLogger.LogExit();
		}
	}
}
