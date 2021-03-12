using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000027 RID: 39
	public class NewSubscriptionBase<TSubscription> : NewTenantADTaskBase<TSubscription> where TSubscription : IConfigurable, new()
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007C33 File Offset: 0x00005E33
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00007C4A File Offset: 0x00005E4A
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string Name
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00007C5D File Offset: 0x00005E5D
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00007C74 File Offset: 0x00005E74
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007C87 File Offset: 0x00005E87
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00007C9E File Offset: 0x00005E9E
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)base.Fields["DisplayName"];
			}
			set
			{
				base.Fields["DisplayName"] = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007CB1 File Offset: 0x00005EB1
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00007CD6 File Offset: 0x00005ED6
		[Parameter(Mandatory = true)]
		public SmtpAddress EmailAddress
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007CEE File Offset: 0x00005EEE
		protected virtual AggregationType AggregationType
		{
			get
			{
				return AggregationType.Aggregation;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007CF4 File Offset: 0x00005EF4
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 97, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\NewSubscriptionBase.cs");
			if (this.Mailbox == null)
			{
				ADObjectId adObjectId;
				if (!base.TryGetExecutingUserId(out adObjectId))
				{
					throw new ExecutingUserPropertyNotFoundException("executingUserid");
				}
				this.Mailbox = new MailboxIdParameter(adObjectId);
			}
			ADUser adUser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(this.Mailbox.ToString())));
			IRecipientSession session = AggregationTaskUtils.VerifyIsWithinWriteScopes(tenantOrRootOrgRecipientSession, adUser, new Task.TaskErrorLoggingDelegate(this.WriteDebugInfoAndError));
			AggregationSubscriptionDataProvider result = null;
			try
			{
				result = SubscriptionConfigDataProviderFactory.Instance.CreateSubscriptionDataProvider(this.AggregationType, AggregationTaskType.New, session, adUser);
			}
			catch (MailboxFailureException exception)
			{
				this.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, this.Mailbox);
			}
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007DE8 File Offset: 0x00005FE8
		protected override IConfigurable PrepareDataObject()
		{
			PimSubscriptionProxy pimSubscriptionProxy = (PimSubscriptionProxy)base.PrepareDataObject();
			this.EnsureUserDisplayName();
			pimSubscriptionProxy.Name = this.Name;
			pimSubscriptionProxy.DisplayName = this.DisplayName;
			pimSubscriptionProxy.EmailAddress = this.EmailAddress;
			pimSubscriptionProxy.AggregationType = this.AggregationType;
			pimSubscriptionProxy.SendAsCheckNeeded = true;
			return pimSubscriptionProxy;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007E40 File Offset: 0x00006040
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalBeginProcessing();
				if (this.password != null)
				{
					if (this.password.Length <= 0)
					{
						this.WriteDebugInfoAndError(new SubscriptionPasswordEmptyException(), ErrorCategory.InvalidArgument, null);
					}
					ValidationError validationError = AggregationSubscriptionConstraints.PasswordRangeConstraint.Validate(this.password.Length, new SubscriptionProxyPropertyDefinition("Password", typeof(int)), null);
					if (validationError != null)
					{
						this.WriteDebugInfoAndError(new LocalizedException(Strings.SubscriptionPasswordTooLong(this.password.Length, this.Name)), ErrorCategory.InvalidArgument, null);
					}
				}
			}
			finally
			{
				this.WriteDebugInfo();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007EF0 File Offset: 0x000060F0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.InternalProcessRecord();
			}
			finally
			{
				this.WriteDebugInfo();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007F3C File Offset: 0x0000613C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			AggregationTaskUtils.ValidateEmailAddress(base.DataSession, this.DataObject as PimSubscriptionProxy, new Task.TaskErrorLoggingDelegate(this.WriteDebugInfoAndError));
			TaskLogger.LogExit();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007F78 File Offset: 0x00006178
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is FailedCreateAggregationSubscriptionException || exception is FailedDeleteAggregationSubscriptionException || exception is RedundantPimSubscriptionException || exception is RedundantAccountSubscriptionException || exception is SubscriptionInconsistentException || exception is SubscriptionNameAlreadyExistsException || exception is SubscriptionNumberExceedLimitException;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007FC9 File Offset: 0x000061C9
		protected void WriteDebugInfoAndError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteDebugInfo();
			base.WriteError(exception, category, target);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007FDA File Offset: 0x000061DA
		protected void WriteDebugInfo()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(CommonLoggingHelper.SyncLogSession.GetBlackBoxText());
			}
			CommonLoggingHelper.SyncLogSession.ClearBlackBox();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008000 File Offset: 0x00006200
		private void EnsureUserDisplayName()
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				string text = this.EmailAddress.ToString();
				if (text.Length >= 256)
				{
					this.DisplayName = text.Substring(0, 255);
					return;
				}
				this.DisplayName = text;
			}
		}

		// Token: 0x0400007B RID: 123
		protected SecureString password;
	}
}
