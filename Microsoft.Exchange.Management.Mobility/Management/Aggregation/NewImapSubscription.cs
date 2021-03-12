using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200002B RID: 43
	[Cmdlet("New", "ImapSubscription", SupportsShouldProcess = true)]
	public sealed class NewImapSubscription : NewSubscriptionBase<IMAPSubscriptionProxy>
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008A0C File Offset: 0x00006C0C
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00008A19 File Offset: 0x00006C19
		[Parameter(Mandatory = true)]
		public Fqdn IncomingServer
		{
			get
			{
				return this.DataObject.IncomingServer;
			}
			set
			{
				this.DataObject.IncomingServer = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00008A27 File Offset: 0x00006C27
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00008A34 File Offset: 0x00006C34
		[Parameter(Mandatory = false)]
		public int IncomingPort
		{
			get
			{
				return this.DataObject.IncomingPort;
			}
			set
			{
				this.DataObject.IncomingPort = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008A42 File Offset: 0x00006C42
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00008A4F File Offset: 0x00006C4F
		[Parameter(Mandatory = true)]
		public string IncomingUserName
		{
			get
			{
				return this.DataObject.IncomingUserName;
			}
			set
			{
				this.DataObject.IncomingUserName = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00008A5D File Offset: 0x00006C5D
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00008A65 File Offset: 0x00006C65
		[Parameter(Mandatory = true)]
		public SecureString IncomingPassword
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008A6E File Offset: 0x00006C6E
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00008A94 File Offset: 0x00006C94
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008AAC File Offset: 0x00006CAC
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00008AB9 File Offset: 0x00006CB9
		[Parameter(Mandatory = false)]
		public IMAPAuthenticationMechanism IncomingAuth
		{
			get
			{
				return this.DataObject.IncomingAuthentication;
			}
			set
			{
				this.DataObject.IncomingAuthentication = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00008AC7 File Offset: 0x00006CC7
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00008AD4 File Offset: 0x00006CD4
		[Parameter(Mandatory = false)]
		public IMAPSecurityMechanism IncomingSecurity
		{
			get
			{
				return this.DataObject.IncomingSecurity;
			}
			set
			{
				this.DataObject.IncomingSecurity = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00008AE2 File Offset: 0x00006CE2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.CreateIMAPSubscriptionConfirmation(this.DataObject);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public static LocalizedException ValidateSubscription(IMAPSubscriptionProxy subscription, SecureString password, bool skipAccountVerification)
		{
			ICollection<ValidationError> collection = IMAPSubscriptionValidator.Validate(subscription);
			if (collection != null && collection.Count > 0)
			{
				return new LocalizedException(Strings.IMAPAccountVerificationFailedException);
			}
			if (!skipAccountVerification)
			{
				Exception ex = IMAPAutoProvision.VerifyAccount(subscription.IncomingServer.ToString(), subscription.IncomingPort, subscription.IncomingUserName, password, subscription.IncomingAuthentication, subscription.IncomingSecurity, subscription.AggregationType, CommonLoggingHelper.SyncLogSession);
				if (ex != null)
				{
					SyncPermanentException ex2 = ex as SyncPermanentException;
					if (ex2 != null && ex2.DetailedAggregationStatus == DetailedAggregationStatus.CommunicationError && ex2.InnerException is IMAPGmailNotSupportedException)
					{
						return (LocalizedException)ex2.InnerException;
					}
					return new LocalizedException(Strings.IMAPAccountVerificationFailedException);
				}
			}
			return null;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008B90 File Offset: 0x00006D90
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			IMAPSubscriptionProxy dataObject = this.DataObject;
			if (dataObject.IncomingAuthentication == IMAPAuthenticationMechanism.Basic)
			{
				AggregationTaskUtils.ValidateUnicodeInfoOnUserNameAndPassword(dataObject.IncomingUserName, this.password, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			}
			AggregationTaskUtils.ValidateIncomingServerLength(dataObject.IncomingServer, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			Exception ex = NewImapSubscription.ValidateSubscription(dataObject, this.password, this.Force);
			if (ex != null)
			{
				base.WriteDebugInfoAndError(ex, (ErrorCategory)1003, this.DataObject);
			}
			this.DataObject.SetPassword(this.password);
			base.WriteDebugInfo();
			TaskLogger.LogExit();
		}
	}
}
