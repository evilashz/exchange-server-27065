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
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000034 RID: 52
	public abstract class SetSubscriptionBase<TSubscription> : SetTenantADTaskBase<AggregationSubscriptionIdParameter, TSubscription, TSubscription> where TSubscription : IConfigurable, new()
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000A128 File Offset: 0x00008328
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000A130 File Offset: 0x00008330
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "DisableSubscriptionAsPoison", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "SubscriptionModification", ValueFromPipeline = true)]
		public override AggregationSubscriptionIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A139 File Offset: 0x00008339
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000A150 File Offset: 0x00008350
		[Parameter(Mandatory = false, ParameterSetName = "DisableSubscriptionAsPoison", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true)]
		public virtual MailboxIdParameter Mailbox
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A163 File Offset: 0x00008363
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000A17A File Offset: 0x0000837A
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A18D File Offset: 0x0000838D
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000A1B3 File Offset: 0x000083B3
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public SwitchParameter EnablePoisonSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnablePoisonSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EnablePoisonSubscription"] = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A1CB File Offset: 0x000083CB
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000A1EC File Offset: 0x000083EC
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public bool Enabled
		{
			get
			{
				return (bool)(base.Fields["Enabled"] ?? true);
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A204 File Offset: 0x00008404
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000A22A File Offset: 0x0000842A
		[Parameter(Mandatory = false, ParameterSetName = "DisableSubscriptionAsPoison")]
		public SwitchParameter DisableAsPoison
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisableAsPoison"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DisableAsPoison"] = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001F5 RID: 501
		protected abstract AggregationSubscriptionType IdentityType { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A242 File Offset: 0x00008442
		protected virtual AggregationType AggregationType
		{
			get
			{
				return AggregationType.Aggregation;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A248 File Offset: 0x00008448
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			base.InternalStateReset();
			this.Identity.SubscriptionType = new AggregationSubscriptionType?(this.IdentityType);
			TaskLogger.LogExit();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A28C File Offset: 0x0000848C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 149, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\SetSubscriptionBase.cs");
			if (this.Mailbox == null)
			{
				if (this.Identity != null && this.Identity.MailboxIdParameter != null)
				{
					this.Mailbox = this.Identity.MailboxIdParameter;
				}
				else
				{
					ADObjectId adObjectId;
					if (!base.TryGetExecutingUserId(out adObjectId))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					this.Mailbox = new MailboxIdParameter(adObjectId);
				}
			}
			ADUser adUser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(this.Mailbox.ToString())));
			IRecipientSession session = AggregationTaskUtils.VerifyIsWithinWriteScopes(tenantOrRootOrgRecipientSession, adUser, new Task.TaskErrorLoggingDelegate(this.WriteDebugInfoAndError));
			AggregationSubscriptionDataProvider result = null;
			try
			{
				result = SubscriptionConfigDataProviderFactory.Instance.CreateSubscriptionDataProvider(this.AggregationType, AggregationTaskType.Set, session, adUser);
			}
			catch (MailboxFailureException exception)
			{
				this.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, this.Mailbox);
			}
			return result;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A3A8 File Offset: 0x000085A8
		protected virtual void ValidateWithDataObject(IConfigurable dataObject)
		{
			PimSubscriptionProxy pimSubscriptionProxy = (PimSubscriptionProxy)dataObject;
			if (this.EnablePoisonSubscription && pimSubscriptionProxy.Status != AggregationStatus.Poisonous)
			{
				this.WriteDebugInfoAndError(new LocalizedException(Strings.SubscriptionCannotBeEnabled), ErrorCategory.InvalidArgument, null);
			}
			if ((pimSubscriptionProxy.Status == AggregationStatus.Poisonous && !this.EnablePoisonSubscription) || pimSubscriptionProxy.Status == AggregationStatus.InvalidVersion)
			{
				this.WriteDebugInfoAndError(new LocalizedException(Strings.SubscriptionCannotBeChanged), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A418 File Offset: 0x00008618
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable configurable = this.ResolveDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			this.ValidateWithDataObject(configurable);
			this.StampChangesOn(configurable);
			if (base.HasErrors)
			{
				return null;
			}
			PimSubscriptionProxy pimSubscriptionProxy = (PimSubscriptionProxy)configurable;
			pimSubscriptionProxy.ObjectState = ObjectState.Changed;
			SendAsManager sendAsManager = new SendAsManager();
			sendAsManager.ResetVerificationEmailData(pimSubscriptionProxy.Subscription);
			pimSubscriptionProxy.SendAsCheckNeeded = this.SendAsCheckNeeded();
			TaskLogger.LogExit();
			return pimSubscriptionProxy;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A488 File Offset: 0x00008688
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			PimSubscriptionProxy pimSubscriptionProxy = (PimSubscriptionProxy)dataObject;
			if (base.Fields.IsModified("DisplayName"))
			{
				pimSubscriptionProxy.DisplayName = this.DisplayName;
			}
			SubscriptionStateTransitionHelper subscriptionStateTransitionHelper = new SubscriptionStateTransitionHelper(pimSubscriptionProxy.Subscription);
			if (this.DisableAsPoison)
			{
				subscriptionStateTransitionHelper.DisableAsPoisonous();
			}
			else if (this.EnablePoisonSubscription)
			{
				if (this.Enabled)
				{
					subscriptionStateTransitionHelper.EnableFromPoison();
				}
				else
				{
					subscriptionStateTransitionHelper.Disable();
				}
			}
			else if (this.Enabled)
			{
				subscriptionStateTransitionHelper.Enable();
			}
			else
			{
				subscriptionStateTransitionHelper.Disable();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A520 File Offset: 0x00008720
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is SubscriptionInconsistentException;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A538 File Offset: 0x00008738
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.password != null)
			{
				if (this.password.Length <= 0)
				{
					this.WriteDebugInfoAndError(new SubscriptionPasswordEmptyException(), ErrorCategory.InvalidArgument, null);
				}
				ValidationError validationError = AggregationSubscriptionConstraints.PasswordRangeConstraint.Validate(this.password.Length, new SubscriptionProxyPropertyDefinition("password", typeof(int)), null);
				if (validationError != null)
				{
					this.WriteDebugInfoAndError(new LocalizedException(Strings.SubscriptionPasswordTooLong(this.password.Length, this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A5D4 File Offset: 0x000087D4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			Exception ex = null;
			try
			{
				base.InternalProcessRecord();
			}
			catch (LocalizedException ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (ex != null)
				{
					this.WriteDebugInfoAndError(ex, ErrorCategory.InvalidArgument, this.Mailbox);
				}
				else
				{
					this.WriteDebugInfo();
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A648 File Offset: 0x00008848
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			AggregationTaskUtils.ValidateEmailAddress(base.DataSession, this.DataObject as PimSubscriptionProxy, new Task.TaskErrorLoggingDelegate(this.WriteDebugInfoAndError));
			TaskLogger.LogExit();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A681 File Offset: 0x00008881
		protected virtual bool SendAsCheckNeeded()
		{
			return true;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A684 File Offset: 0x00008884
		protected void WriteDebugInfoAndError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteDebugInfo();
			base.WriteError(exception, category, target);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A695 File Offset: 0x00008895
		protected void WriteDebugInfo()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(CommonLoggingHelper.SyncLogSession.GetBlackBoxText());
			}
			CommonLoggingHelper.SyncLogSession.ClearBlackBox();
		}

		// Token: 0x04000095 RID: 149
		protected SecureString password;
	}
}
