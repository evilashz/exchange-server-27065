using System;
using System.Management.Automation;
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
	// Token: 0x0200002F RID: 47
	public abstract class RemoveSubscriptionBase<TIdentity> : RemoveTenantADTaskBase<AggregationSubscriptionIdParameter, TIdentity> where TIdentity : IConfigurable, new()
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009B27 File Offset: 0x00007D27
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveSubscriptionConfirmation(this.Identity);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C9 RID: 457
		protected abstract AggregationType AggregationType { get; }

		// Token: 0x060001CA RID: 458 RVA: 0x00009B34 File Offset: 0x00007D34
		protected virtual MailboxIdParameter GetMailboxIdParameter()
		{
			MailboxIdParameter result;
			if (this.Identity != null && this.Identity.MailboxIdParameter != null)
			{
				result = this.Identity.MailboxIdParameter;
			}
			else
			{
				ADObjectId adObjectId;
				if (!base.TryGetExecutingUserId(out adObjectId))
				{
					throw new ExecutingUserPropertyNotFoundException("executingUserid");
				}
				result = new MailboxIdParameter(adObjectId);
			}
			return result;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009B84 File Offset: 0x00007D84
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 82, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\RemoveSubscriptionBase.cs");
			MailboxIdParameter mailboxIdParameter = this.GetMailboxIdParameter();
			ADUser adUser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(mailboxIdParameter.ToString())));
			IRecipientSession session = AggregationTaskUtils.VerifyIsWithinWriteScopes(tenantOrRootOrgRecipientSession, adUser, new Task.TaskErrorLoggingDelegate(this.WriteDebugInfoAndError));
			AggregationSubscriptionDataProvider result = null;
			try
			{
				AggregationType aggregationType = this.AggregationType;
				if (this.Identity != null && this.Identity.AggregationType != null)
				{
					aggregationType = this.Identity.AggregationType.Value;
				}
				result = SubscriptionConfigDataProviderFactory.Instance.CreateSubscriptionDataProvider(aggregationType, AggregationTaskType.Remove, session, adUser);
			}
			catch (MailboxFailureException exception)
			{
				this.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, mailboxIdParameter);
			}
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009C78 File Offset: 0x00007E78
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				base.DataObject
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

		// Token: 0x060001CD RID: 461 RVA: 0x00009CC4 File Offset: 0x00007EC4
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009CCD File Offset: 0x00007ECD
		private void WriteDebugInfoAndError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteDebugInfo();
			base.WriteError(exception, category, target);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009CDE File Offset: 0x00007EDE
		private void WriteDebugInfo()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(CommonLoggingHelper.SyncLogSession.GetBlackBoxText());
			}
			CommonLoggingHelper.SyncLogSession.ClearBlackBox();
		}
	}
}
