using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000354 RID: 852
	internal sealed class RemoveAggregatedAccount : AggregatedAccountCommandBase<ServiceResultNone>
	{
		// Token: 0x060017FC RID: 6140 RVA: 0x000810F8 File Offset: 0x0007F2F8
		public RemoveAggregatedAccount(CallContext callContext, RemoveAggregatedAccountRequest request) : base(callContext, request, ExTraceGlobals.RemoveAggregatedAccountTracer, typeof(RemoveAggregatedAccountMetadata))
		{
			this.EmailAddress = SmtpAddress.Parse(request.EmailAddress);
			this.getAggregatedAccountMailboxGuidFromSyncRequestStatistics = Hookable<Func<Guid>>.Create(true, new Func<Guid>(this.GetAggregatedAccountMailboxGuidFromSyncRequestStatistics));
			this.removeAggregatedAccountToADUser = Hookable<Action>.Create(true, new Action(this.RemoveAggregatedAccountFromADUser));
			this.removeSyncRequest = Hookable<Action>.Create(true, new Action(this.RemoveSyncRequest));
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00081178 File Offset: 0x0007F378
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new RemoveAggregatedAccountResponse(base.Result.Code, base.Result.Error);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000811E0 File Offset: 0x0007F3E0
		internal override ServiceResult<ServiceResultNone> InternalExecute()
		{
			base.ExecuteWithProtocolLogging(RemoveAggregatedAccountMetadata.GetAggregatedMailboxGuidFromSyncRequestStatisticsTime, delegate
			{
				this.MailboxGuid = this.getAggregatedAccountMailboxGuidFromSyncRequestStatistics.Value();
			});
			base.ExecuteWithProtocolLogging(RemoveAggregatedAccountMetadata.RemoveSyncRequestCmdletTime, delegate
			{
				this.removeSyncRequest.Value();
			});
			base.ExecuteWithProtocolLogging(RemoveAggregatedAccountMetadata.RemoveAggregatedMailboxGuidFromADUserTime, delegate
			{
				this.removeAggregatedAccountToADUser.Value();
			});
			this.TraceSuccess("Successfully removed aggregated account.");
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0008124A File Offset: 0x0007F44A
		internal override void SetLogMetadataEnumProperties()
		{
			this.verifyEnvironmentTimeEnum = RemoveAggregatedAccountMetadata.VerifyEnvironmentTime;
			this.verifyUserIdentityTypeTimeEnum = RemoveAggregatedAccountMetadata.VerifyUserIdentityTypeTime;
			this.totalTimeEnum = RemoveAggregatedAccountMetadata.TotalTime;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00081270 File Offset: 0x0007F470
		protected override string TypeName
		{
			get
			{
				return RemoveAggregatedAccount.RemoveAggregatedAccountName;
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00081278 File Offset: 0x0007F478
		private Guid GetAggregatedAccountMailboxGuidFromSyncRequestStatistics()
		{
			Exception ex = null;
			SyncRequestStatistics syncRequestStatistics = base.GetSyncRequestStatisticsForAggregatedAccountGetter.Value(this.EmailAddress, ex);
			if (syncRequestStatistics == null)
			{
				throw new CannotRemoveAggregatedAccountException((CoreResources.IDs)2834376775U, ex);
			}
			if (syncRequestStatistics.EmailAddress != this.EmailAddress)
			{
				throw new CannotRemoveAggregatedAccountException(CoreResources.IDs.ErrorNoSyncRequestsMatchedSpecifiedEmailAddress);
			}
			if (!syncRequestStatistics.Flags.HasFlag(RequestFlags.TargetIsAggregatedMailbox))
			{
				throw new CannotRemoveAggregatedAccountException(CoreResources.IDs.ErrorFoundSyncRequestForNonAggregatedAccount);
			}
			if (syncRequestStatistics.TargetExchangeGuid == Guid.Empty)
			{
				throw new CannotRemoveAggregatedAccountException((CoreResources.IDs)3504612180U);
			}
			return syncRequestStatistics.TargetExchangeGuid;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0008132C File Offset: 0x0007F52C
		private void RemoveAggregatedAccountFromADUser()
		{
			using (PSLocalTask<SetMailbox, object> pslocalTask = CmdletTaskFactory.Instance.CreateSetMailboxTask(base.CallContext.AccessingPrincipal, "RemoveAggregatedMailbox"))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.Identity = new MailboxIdParameter(base.CallContext.AccessingADUser.ObjectId);
				pslocalTask.Task.AggregatedMailboxGuid = this.MailboxGuid;
				pslocalTask.Task.RemoveAggregatedAccount = new SwitchParameter(true);
				pslocalTask.Task.ForestWideDomainControllerAffinityByExecutingUser = new SwitchParameter(true);
				pslocalTask.Task.Execute();
				if (pslocalTask.Error != null)
				{
					this.TraceError(string.Format("Could not remove aggregated mailbox {0} from unified mailbox with NetId {1}. Exception: {2}.", this.MailboxGuid, this.netId, pslocalTask.Error.Exception));
					base.CallContext.ProtocolLog.Set(AddAggregatedAccountMetadata.SetMailboxCmdletError, pslocalTask.Error.Exception);
					throw new CannotRemoveAggregatedAccountException(ResponseCodeType.ErrorCannotRemoveAggregatedAccountMailbox, pslocalTask.Error.Exception);
				}
				this.TraceSuccess(string.Format("Successfully removed aggregated mailbox guid {0} from unified mailbox with NetId {1}", this.MailboxGuid, this.netId));
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000814DC File Offset: 0x0007F6DC
		private void RemoveSyncRequest()
		{
			using (PSLocalTask<RemoveSyncRequest, object> removeSyncRequest = CmdletTaskFactory.Instance.CreateRemoveSyncRequestTask(base.CallContext.AccessingPrincipal))
			{
				removeSyncRequest.CaptureAdditionalIO = true;
				removeSyncRequest.Task.Identity = SyncRequestIdParameter.Create(base.CallContext.AccessingADUser, (string)this.EmailAddress);
				Exception removeSyncRequestException = null;
				FaultInjection.FaultInjectionPoint((FaultInjection.LIDs)2340826429U, delegate
				{
					removeSyncRequest.Task.Execute();
					if (removeSyncRequest.Error != null)
					{
						removeSyncRequestException = removeSyncRequest.Error.Exception;
					}
				}, delegate
				{
					removeSyncRequestException = new Exception("This is an exception from fault injection action");
				});
				if (removeSyncRequestException != null)
				{
					this.TraceError(string.Format("Could not remove aggregated account from unified mailbox with NetId {0}. Exception: {1}.", this.netId, removeSyncRequestException));
					throw new CannotRemoveAggregatedAccountException((CoreResources.IDs)2834376775U, removeSyncRequestException);
				}
				this.TraceSuccess(string.Format("Successfully removed aggregated account from unified mailbox with NetId {0}", this.netId));
			}
		}

		// Token: 0x04001016 RID: 4118
		private static readonly string RemoveAggregatedAccountName = "RemoveAggregatedAccount";

		// Token: 0x04001017 RID: 4119
		private readonly SmtpAddress EmailAddress;

		// Token: 0x04001018 RID: 4120
		private Guid MailboxGuid;

		// Token: 0x04001019 RID: 4121
		internal readonly Hookable<Func<Guid>> getAggregatedAccountMailboxGuidFromSyncRequestStatistics;

		// Token: 0x0400101A RID: 4122
		internal readonly Hookable<Action> removeAggregatedAccountToADUser;

		// Token: 0x0400101B RID: 4123
		internal readonly Hookable<Action> removeSyncRequest;
	}
}
