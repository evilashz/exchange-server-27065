using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200002D RID: 45
	[Cmdlet("New", "PopSubscription", SupportsShouldProcess = true)]
	public sealed class NewPopSubscription : NewSubscriptionBase<PopSubscriptionProxy>
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008F06 File Offset: 0x00007106
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00008F13 File Offset: 0x00007113
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008F21 File Offset: 0x00007121
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00008F2E File Offset: 0x0000712E
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008F3C File Offset: 0x0000713C
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00008F49 File Offset: 0x00007149
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008F57 File Offset: 0x00007157
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00008F5F File Offset: 0x0000715F
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008F68 File Offset: 0x00007168
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00008F75 File Offset: 0x00007175
		[Parameter(Mandatory = false)]
		public AuthenticationMechanism IncomingAuth
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008F83 File Offset: 0x00007183
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00008F90 File Offset: 0x00007190
		[Parameter(Mandatory = false)]
		public SecurityMechanism IncomingSecurity
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008F9E File Offset: 0x0000719E
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00008FAB File Offset: 0x000071AB
		[Parameter(Mandatory = false)]
		public bool LeaveOnServer
		{
			get
			{
				return this.DataObject.LeaveOnServer;
			}
			set
			{
				this.DataObject.LeaveOnServer = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008FB9 File Offset: 0x000071B9
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00008FDF File Offset: 0x000071DF
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008FF7 File Offset: 0x000071F7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.CreatePopSubscriptionConfirmation(this.DataObject);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009004 File Offset: 0x00007204
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			PopSubscriptionProxy dataObject = this.DataObject;
			if (dataObject.IncomingAuthentication == AuthenticationMechanism.Basic)
			{
				AggregationTaskUtils.ValidateUnicodeInfoOnUserNameAndPassword(dataObject.IncomingUserName, this.password, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			}
			AggregationTaskUtils.ValidateIncomingServerLength(dataObject.IncomingServer, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			base.WriteDebugInfo();
			TaskLogger.LogExit();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009070 File Offset: 0x00007270
		protected override IConfigurable PrepareDataObject()
		{
			PopSubscriptionProxy popSubscriptionProxy = (PopSubscriptionProxy)base.PrepareDataObject();
			if (this.Force == false)
			{
				AggregationSubscriptionDataProvider aggregationSubscriptionDataProvider = (AggregationSubscriptionDataProvider)base.DataSession;
				if (base.Mailbox == null)
				{
					ADObjectId adobjectId;
					if (!base.TryGetExecutingUserId(out adobjectId))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
				}
				else
				{
					ADObjectId internalADObjectId = base.Mailbox.InternalADObjectId;
				}
				LocalizedException exception;
				if (!Pop3AutoProvision.ValidatePopSettings(popSubscriptionProxy.LeaveOnServer, popSubscriptionProxy.AggregationType == AggregationType.Mirrored, popSubscriptionProxy.IncomingServer, popSubscriptionProxy.IncomingPort, popSubscriptionProxy.IncomingUserName, this.password, popSubscriptionProxy.IncomingAuthentication, popSubscriptionProxy.IncomingSecurity, aggregationSubscriptionDataProvider.UserLegacyDN, CommonLoggingHelper.SyncLogSession, out exception))
				{
					base.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, this.DataObject);
				}
			}
			popSubscriptionProxy.SetPassword(this.password);
			base.WriteDebugInfo();
			return popSubscriptionProxy;
		}
	}
}
