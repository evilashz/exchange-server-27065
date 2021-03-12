using System;
using System.Security;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000371 RID: 881
	internal sealed class SetAggregatedAccount : AggregatedAccountCommandBase<AggregatedAccountType>
	{
		// Token: 0x060018B0 RID: 6320 RVA: 0x00087EDC File Offset: 0x000860DC
		public SetAggregatedAccount(CallContext callContext, SetAggregatedAccountRequest request) : base(callContext, request, ExTraceGlobals.SetAggregatedAccountTracer, typeof(SetAggregatedAccountMetadata))
		{
			SetAggregatedAccount <>4__this = this;
			this.connectionSettingsManager = Hookable<Func<ConnectionSettingsManager>>.Create(true, () => ConnectionSettingsManager.GetInstanceForModernOutlook(new LoggingAdapter(callContext, <>4__this.tracer)));
			this.EmailAddress = SmtpAddress.Parse(request.EmailAddress);
			this.UserName = request.UserName;
			this.Authentication = request.Authentication;
			this.Password = request.Password.AsSecureString();
			this.IncomingServer = request.IncomingServer;
			this.IncomingPort = request.IncomingPort;
			if (!string.IsNullOrEmpty(request.IncrementalSyncInterval))
			{
				TimeSpan.TryParse(request.IncrementalSyncInterval, out this.IncrementalSyncInterval);
			}
			this.Security = request.Security;
			this.currentConnectionSettings = null;
			this.setSyncRequest = Hookable<Action>.Create(true, new Action(this.SetSyncRequest));
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00088000 File Offset: 0x00086200
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new SetAggregatedAccountResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00088088 File Offset: 0x00086288
		internal override ServiceResult<AggregatedAccountType> InternalExecute()
		{
			SyncRequestStatistics syncRequestStatistics = null;
			base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.GetSyncRequestStatisticsCmdletTime, delegate
			{
				syncRequestStatistics = this.GetSyncRequestStatistics();
			});
			base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.GetUpdatedConnectionSettingsTime, delegate
			{
				this.currentConnectionSettings = this.GetUpdatedConnectionSettings(syncRequestStatistics);
			});
			base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.TestLogonWithCurrentSettingsTime, delegate
			{
				this.TestLogonWithCurrentSettings();
			});
			base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.SetSyncRequestCmdletTime, delegate
			{
				this.setSyncRequest.Value();
			});
			this.TraceSuccess("Successfully updated aggregated account.");
			return new ServiceResult<AggregatedAccountType>(new AggregatedAccountType(syncRequestStatistics.TargetExchangeGuid, syncRequestStatistics.RequestGuid, (string)this.EmailAddress, this.UserName, ConnectionSettingsConverter.BuildPublicRepresentation(this.currentConnectionSettings)));
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00088152 File Offset: 0x00086352
		internal override void SetLogMetadataEnumProperties()
		{
			this.verifyEnvironmentTimeEnum = SetAggregatedAccountMetadata.VerifyEnvironmentTime;
			this.verifyUserIdentityTypeTimeEnum = SetAggregatedAccountMetadata.VerifyUserIdentityTypeTime;
			this.totalTimeEnum = SetAggregatedAccountMetadata.TotalTime;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00088178 File Offset: 0x00086378
		protected override string TypeName
		{
			get
			{
				return SetAggregatedAccount.SetAggregatedAccountName;
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00088180 File Offset: 0x00086380
		private void SetSyncRequest()
		{
			using (PSLocalTask<SetSyncRequest, object> pslocalTask = CmdletTaskFactory.Instance.CreateSetSyncRequestTask(base.CallContext.AccessingPrincipal))
			{
				pslocalTask.CaptureAdditionalIO = true;
				ConnectionSettingsConverter.UpdateSetSyncRequestCmdlet(this.currentConnectionSettings, pslocalTask.Task);
				pslocalTask.Task.Identity = SyncRequestIdParameter.Create(base.CallContext.AccessingADUser, (string)this.EmailAddress);
				if (this.Password != null)
				{
					pslocalTask.Task.Password = this.Password;
				}
				if (this.IncrementalSyncInterval != TimeSpan.Zero)
				{
					pslocalTask.Task.IncrementalSyncInterval = this.IncrementalSyncInterval;
				}
				pslocalTask.Task.Execute();
				if (pslocalTask.Error != null)
				{
					this.TraceError(string.Format("Could not update aggregated account in unified mailbox with NetId {0}. Exception: {1}", this.netId, pslocalTask.Error.Exception));
					throw new CannotSetAggregatedAccountException((CoreResources.IDs)4089853131U, pslocalTask.Error.Exception);
				}
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00088288 File Offset: 0x00086488
		private SyncRequestStatistics GetSyncRequestStatistics()
		{
			Exception ex = null;
			SyncRequestStatistics syncRequestStatistics = base.GetSyncRequestStatisticsForAggregatedAccountGetter.Value(this.EmailAddress, ex);
			if (syncRequestStatistics == null)
			{
				throw new CannotSetAggregatedAccountException((CoreResources.IDs)4089853131U, ex);
			}
			if (syncRequestStatistics.EmailAddress != this.EmailAddress)
			{
				throw new CannotSetAggregatedAccountException(CoreResources.IDs.ErrorNoSyncRequestsMatchedSpecifiedEmailAddress);
			}
			if (!syncRequestStatistics.Flags.HasFlag(RequestFlags.TargetIsAggregatedMailbox))
			{
				throw new CannotSetAggregatedAccountException(CoreResources.IDs.ErrorFoundSyncRequestForNonAggregatedAccount);
			}
			return syncRequestStatistics;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00088314 File Offset: 0x00086514
		private ConnectionSettings GetUpdatedConnectionSettings(SyncRequestStatistics syncRequestStatistics)
		{
			ConnectionSettings originalSettings = ConnectionSettingsConverter.BuildConnectionSettings(syncRequestStatistics);
			if (string.IsNullOrEmpty(this.UserName))
			{
				this.UserName = syncRequestStatistics.RemoteCredentialUsername;
			}
			return ConnectionSettingsConverter.BuildUpdateConnectionSettings((!string.IsNullOrEmpty(this.IncomingServer)) ? Fqdn.Parse(this.IncomingServer) : null, (!string.IsNullOrEmpty(this.IncomingPort)) ? new int?(int.Parse(this.IncomingPort)) : null, this.Security, this.Authentication, originalSettings);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00088460 File Offset: 0x00086660
		private void TestLogonWithCurrentSettings()
		{
			bool testWasSuccessful = false;
			base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.TestUserCanLogonTime, delegate
			{
				testWasSuccessful = this.checkUserCanLogon.Value(this.currentConnectionSettings, this.EmailAddress, ref this.UserName, this.Password);
			});
			if (testWasSuccessful)
			{
				this.TraceSuccess(string.Format("Calling TestUserCanLogon(EmailAddress={0}) for [{1}] succeeded.", this.EmailAddress, this.currentConnectionSettings));
				base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.CacheValidatedSettings, delegate
				{
					this.connectionSettingsManager.Value().SetConnectionSettings(this.EmailAddress, this.currentConnectionSettings);
				});
				return;
			}
			if (OperationStatusCodeClassification.IsLogonFailedDueToInvalidCredentials(this.currentConnectionSettings.IncomingConnectionSettings.LogonResult))
			{
				this.TraceError(string.Format("Calling TestUserCanLogon(EmailAddress={0}) for [{1}] failed due to invalid credentials.", this.EmailAddress, this.currentConnectionSettings));
				base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.CacheNotValidatedSettings, delegate
				{
					this.connectionSettingsManager.Value().SetConnectionSettings(this.EmailAddress, this.currentConnectionSettings);
				});
				throw new CannotSetAggregatedAccountException(ResponseCodeType.ErrorInvalidAggregatedAccountCredentials, (CoreResources.IDs)3667869681U);
			}
			if (OperationStatusCodeClassification.IsLogonFailedDueToServerHavingTransientProblems(this.currentConnectionSettings.IncomingConnectionSettings.LogonResult))
			{
				this.TraceError(string.Format("Calling TestUserCanLogon(EmailAddress={0}) for [{1}] failed due to remote server having transient problems.", this.EmailAddress, this.currentConnectionSettings));
				base.ExecuteWithProtocolLogging(SetAggregatedAccountMetadata.CacheNotValidatedSettings, delegate
				{
					this.connectionSettingsManager.Value().SetConnectionSettings(this.EmailAddress, this.currentConnectionSettings);
				});
				throw new CannotSetAggregatedAccountException(ResponseCodeType.ErrorServerTemporaryUnavailable, (CoreResources.IDs)3120707856U);
			}
			throw new CannotSetAggregatedAccountException(ResponseCodeType.ErrorNoConnectionSettingsAvailableForAggregatedAccount, CoreResources.ErrorNoConnectionSettingsAvailableForAggregatedAccount((string)this.EmailAddress));
		}

		// Token: 0x0400108A RID: 4234
		private static readonly string SetAggregatedAccountName = "SetAggregatedAccount";

		// Token: 0x0400108B RID: 4235
		private readonly string Authentication;

		// Token: 0x0400108C RID: 4236
		private readonly SmtpAddress EmailAddress;

		// Token: 0x0400108D RID: 4237
		private string UserName;

		// Token: 0x0400108E RID: 4238
		private readonly SecureString Password;

		// Token: 0x0400108F RID: 4239
		private readonly string IncomingServer;

		// Token: 0x04001090 RID: 4240
		private readonly string IncomingPort;

		// Token: 0x04001091 RID: 4241
		private readonly TimeSpan IncrementalSyncInterval;

		// Token: 0x04001092 RID: 4242
		private readonly string Security;

		// Token: 0x04001093 RID: 4243
		private ConnectionSettings currentConnectionSettings;

		// Token: 0x04001094 RID: 4244
		internal readonly Hookable<Func<ConnectionSettingsManager>> connectionSettingsManager;

		// Token: 0x04001095 RID: 4245
		internal readonly Hookable<AddAggregatedAccount.CheckUserCanLogonWithSettings> checkUserCanLogon = Hookable<AddAggregatedAccount.CheckUserCanLogonWithSettings>.Create(true, delegate(ConnectionSettings connectionSettings, SmtpAddress email, ref string userName, SecureString password)
		{
			return connectionSettings.TestUserCanLogon(email, ref userName, password);
		});

		// Token: 0x04001096 RID: 4246
		internal readonly Hookable<Action> setSyncRequest;
	}
}
