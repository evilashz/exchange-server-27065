using System;
using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200028F RID: 655
	internal abstract class AggregatedAccountCommandBase<SingleItemType> : SingleStepServiceCommand<BaseAggregatedAccountRequest, SingleItemType>
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00053EA8 File Offset: 0x000520A8
		internal Hookable<Func<SmtpAddress, Exception, SyncRequestStatistics>> GetSyncRequestStatisticsForAggregatedAccountGetter
		{
			get
			{
				if (this.getSyncRequestStatisticsForAggregatedAccount == null)
				{
					this.getSyncRequestStatisticsForAggregatedAccount = Hookable<Func<SmtpAddress, Exception, SyncRequestStatistics>>.Create(true, (SmtpAddress smtpAddress, Exception exception) => this.GetSyncRequestStatisticsForAggregatedAccount(smtpAddress, out exception));
				}
				return this.getSyncRequestStatisticsForAggregatedAccount;
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00053F50 File Offset: 0x00052150
		protected AggregatedAccountCommandBase(CallContext callContext, BaseAggregatedAccountRequest request, Microsoft.Exchange.Diagnostics.Trace tracer, Type logMetadataEnumType) : base(callContext2, request)
		{
			this.tracer = tracer;
			ServiceCommandBase.ThrowIfNull(this.tracer, "tracer", "AggregatedAccountCommandBase::ctor");
			this.SetLogMetadata(logMetadataEnumType);
			this.netId = AggregatedAccountCommandBase<SingleItemType>.getNetIdFromCallContext.Value(base.CallContext);
			ServiceCommandBase.ThrowIfNull(this.netId, "NetId", "AggregatedAccountCommandBase::ctor");
			this.getOrganizationId = Hookable<Func<OrganizationId>>.Create(true, new Func<OrganizationId>(this.GetOrganizationId));
			this.organizationId = new Lazy<OrganizationId>(this.getOrganizationId.Value);
			this.organizationId_OrganizationalUnit_Name = Hookable<Func<string>>.Create(true, () => this.organizationId.Value.OrganizationalUnit.Name);
			this.callContextAccessingADUser = Hookable<Func<ADUser>>.Create(true, () => base.CallContext.AccessingADUser);
			this.getTenantResellerId = Hookable<Func<string>>.Create(true, new Func<string>(this.GetTenantResellerId));
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0005415C File Offset: 0x0005235C
		internal override ServiceResult<SingleItemType> Execute()
		{
			ServiceResult<SingleItemType> result = null;
			this.ExecuteWithProtocolLogging(this.totalTimeEnum, delegate
			{
				this.ExecuteWithProtocolLogging(this.verifyEnvironmentTimeEnum, new Action(this.VerifyEnvironment));
				this.ExecuteWithProtocolLogging(this.verifyUserIdentityTypeTimeEnum, new Action(this.VerifyUserIdentityType));
				result = this.InternalExecute();
			});
			return result;
		}

		// Token: 0x0600115A RID: 4442
		internal abstract ServiceResult<SingleItemType> InternalExecute();

		// Token: 0x0600115B RID: 4443
		internal abstract void SetLogMetadataEnumProperties();

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600115C RID: 4444
		protected abstract string TypeName { get; }

		// Token: 0x0600115D RID: 4445 RVA: 0x0005419B File Offset: 0x0005239B
		protected virtual OrganizationId GetOrganizationId()
		{
			return base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000541B4 File Offset: 0x000523B4
		protected virtual void VerifyUserIdentityType()
		{
			if (base.CallContext.IsMSAUser || base.CallContext.EffectiveCaller == null || this.callContextAccessingADUser.Value() == null)
			{
				this.TraceError(string.Format("Can't find unified mailbox with NetId {0}.", this.netId));
				throw new CannotFindUnifiedMailboxException(CoreResources.IDs.ErrorCannotFindUnifiedMailbox);
			}
			this.VerifyOpenTenant();
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0005421C File Offset: 0x0005241C
		protected virtual void TraceError(string message)
		{
			this.InternalTraceError(message, (base.CallContext.AccessingPrincipal != null) ? base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString() : null, (base.CallContext.AccessingPrincipal != null) ? base.CallContext.EffectiveCallerSid : null);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0005427E File Offset: 0x0005247E
		private void SetLogMetadata(Type logMetadataEnumType)
		{
			OwsLogRegistry.Register(this.TypeName, logMetadataEnumType, new Type[0]);
			this.SetLogMetadataEnumProperties();
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00054298 File Offset: 0x00052498
		protected virtual void TraceSuccess(string message)
		{
			this.InternalTraceSuccess(message, (base.CallContext.AccessingPrincipal != null) ? base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString() : null, (base.CallContext.AccessingPrincipal != null) ? base.CallContext.EffectiveCallerSid : null);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000542FC File Offset: 0x000524FC
		protected void ExecuteWithProtocolLogging(Enum logMetadata, Action operation)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			operation();
			stopwatch.Stop();
			this.requestDetailsLogger_Set.Value(base.CallContext.ProtocolLog, logMetadata, stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0005434C File Offset: 0x0005254C
		protected SyncRequestStatistics GetSyncRequestStatisticsForAggregatedAccount(SmtpAddress emailAddress, out Exception getSyncRequestStatisticsTaskException)
		{
			getSyncRequestStatisticsTaskException = null;
			SyncRequestStatistics result = null;
			using (PSLocalTask<GetSyncRequestStatistics, SyncRequestStatistics> pslocalTask = CmdletTaskFactory.Instance.CreateGetSyncRequestStatisticsTask(base.CallContext.AccessingPrincipal))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.Identity = SyncRequestIdParameter.Create(base.CallContext.AccessingADUser, (string)emailAddress);
				pslocalTask.Task.Execute();
				if (pslocalTask.Error != null)
				{
					this.TraceError(string.Format("Could not get aggregated accounts from unified mailbox with NetId {0}. Exception: {1}", this.netId, pslocalTask.Error.Exception));
					getSyncRequestStatisticsTaskException = pslocalTask.Error.Exception;
				}
				result = pslocalTask.Result;
			}
			return result;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00054404 File Offset: 0x00052604
		protected void VerifyOpenTenant()
		{
			if (ExEnvironment.IsSdfDomain && Guid.Parse(this.organizationId.Value.ToExternalDirectoryOrganizationId()) == Constants.ConsumerTenantGuid)
			{
				this.TraceSuccess("Verified open tenant with hard-coded GUID in SDF environment.");
				return;
			}
			string text = this.getTenantResellerId.Value();
			if (!StringComparer.OrdinalIgnoreCase.Equals(text, "MSOnline.BPOS_Unmanaged_Hydrated"))
			{
				this.TraceError(string.Format("{0} service command is not supported for tenants with reseller Id {1}", this.TypeName, text));
				throw new UnifiedMailboxSupportedOnlyWithMicrosoftAccountException(CoreResources.IDs.ErrorUnifiedMailboxSupportedOnlyWithMicrosoftAccount);
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0005448F File Offset: 0x0005268F
		internal static void AssignIfParameterSet(string parameter, Action<string> assignValue)
		{
			if (!string.IsNullOrEmpty(parameter))
			{
				assignValue(parameter);
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000544A0 File Offset: 0x000526A0
		internal void InternalTraceError(string message, string mailbox, SecurityIdentifier callerSid)
		{
			this.tracer.TraceError((long)this.GetHashCode(), "{0}: {1} Mailbox: {2}, AccessType: {3}, AccessingAs SID: {4}", new object[]
			{
				this.TypeName,
				message,
				mailbox,
				base.CallContext.MailboxAccessType,
				callerSid
			});
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000544F4 File Offset: 0x000526F4
		internal void InternalTraceSuccess(string message, string mailbox, SecurityIdentifier callerSid)
		{
			this.tracer.TraceInformation(this.GetHashCode(), 0L, "{0}: {1} Mailbox: {2}, AccessType: {3}, AccessingAs SID: {4}", new object[]
			{
				this.TypeName,
				message,
				mailbox,
				base.CallContext.MailboxAccessType,
				callerSid
			});
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00054549 File Offset: 0x00052749
		private void VerifyEnvironment()
		{
			if (!this.isDatacenter.Value())
			{
				this.TraceError("Called in a non-datacenter environment.");
				throw new NotApplicableOutsideOfDatacenterException(CoreResources.IDs.ErrorNotApplicableOutsideOfDatacenter);
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00054578 File Offset: 0x00052778
		private string GetTenantResellerId()
		{
			string name = this.organizationId.Value.OrganizationalUnit.Name;
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantAcceptedDomain(name), 409, "GetTenantResellerId", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\AggregatedAccountCommandBase.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnitByNameOrAcceptedDomain = tenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(name);
			return exchangeConfigurationUnitByNameOrAcceptedDomain.ResellerId;
		}

		// Token: 0x04000C90 RID: 3216
		internal const string OpenTenantResellerId = "MSOnline.BPOS_Unmanaged_Hydrated";

		// Token: 0x04000C91 RID: 3217
		internal readonly Lazy<OrganizationId> organizationId;

		// Token: 0x04000C92 RID: 3218
		internal readonly Hookable<Func<string>> organizationId_OrganizationalUnit_Name;

		// Token: 0x04000C93 RID: 3219
		protected readonly string netId;

		// Token: 0x04000C94 RID: 3220
		protected Microsoft.Exchange.Diagnostics.Trace tracer;

		// Token: 0x04000C95 RID: 3221
		protected Enum verifyEnvironmentTimeEnum;

		// Token: 0x04000C96 RID: 3222
		protected Enum verifyUserIdentityTypeTimeEnum;

		// Token: 0x04000C97 RID: 3223
		protected Enum totalTimeEnum;

		// Token: 0x04000C98 RID: 3224
		internal readonly Hookable<Func<bool>> isDatacenter = Hookable<Func<bool>>.Create(true, () => VariantConfiguration.InvariantNoFlightingSnapshot.Ews.CreateUnifiedMailbox.Enabled);

		// Token: 0x04000C99 RID: 3225
		internal readonly Hookable<Func<OrganizationId>> getOrganizationId;

		// Token: 0x04000C9A RID: 3226
		internal static Hookable<Func<CallContext, string>> getNetIdFromCallContext = Hookable<Func<CallContext, string>>.Create(true, (CallContext callContext) => callContext.EffectiveCallerNetId);

		// Token: 0x04000C9B RID: 3227
		internal readonly Hookable<Func<RequestDetailsLogger, Enum, object, string>> requestDetailsLogger_Set = Hookable<Func<RequestDetailsLogger, Enum, object, string>>.Create(true, (RequestDetailsLogger protocolLog, Enum property, object value) => protocolLog.Set(property, value));

		// Token: 0x04000C9C RID: 3228
		internal readonly Hookable<Func<ADUser>> callContextAccessingADUser;

		// Token: 0x04000C9D RID: 3229
		internal readonly Hookable<Func<string>> getTenantResellerId;

		// Token: 0x04000C9E RID: 3230
		internal readonly Hookable<Func<CallContext, MailboxSession>> getMailboxIdentityMailboxSession = Hookable<Func<CallContext, MailboxSession>>.Create(true, (CallContext callContext) => callContext.SessionCache.GetMailboxIdentityMailboxSession(false));

		// Token: 0x04000C9F RID: 3231
		internal readonly Hookable<Func<MailboxSession, ADUser, AggregatedAccountHelper>> createAggregatedAccountHelper = Hookable<Func<MailboxSession, ADUser, AggregatedAccountHelper>>.Create(true, (MailboxSession session, ADUser adUser) => new AggregatedAccountHelper(session, adUser));

		// Token: 0x04000CA0 RID: 3232
		private Hookable<Func<SmtpAddress, Exception, SyncRequestStatistics>> getSyncRequestStatisticsForAggregatedAccount;
	}
}
