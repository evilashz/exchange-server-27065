using System;
using System.Net;
using System.Security.Principal;
using System.ServiceModel.Web;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x02000006 RID: 6
	public abstract class ServiceBase
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002304 File Offset: 0x00000504
		internal ServiceBase(IUserWorkloadManager userWorkloadManager)
		{
			ArgumentValidator.ThrowIfNull("userWorkloadManager", userWorkloadManager);
			this.UserWorkloadManager = userWorkloadManager;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000231E File Offset: 0x0000051E
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002326 File Offset: 0x00000526
		internal IUserWorkloadManager UserWorkloadManager { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x00002330 File Offset: 0x00000530
		internal IAsyncResult BeginServiceCommand(IServiceCommand command)
		{
			ExAssert.RetailAssert(Thread.CurrentPrincipal.Identity.IsAuthenticated, "BeginServiceCommand call must be authenticated.");
			IStandardBudget standardBudget = this.AcquireAndCheckBudget(command);
			command.Initialize(standardBudget);
			if (!this.UserWorkloadManager.TrySubmitNewTask(command))
			{
				ServiceBusyException ex = new ServiceBusyException(command.Description);
				command.Complete(ex);
				this.ThrowServiceBusyException(command.Description, standardBudget.Owner, ex);
			}
			return command.CommandAsyncResult;
		}

		// Token: 0x0600001E RID: 30
		internal abstract IStandardBudget AcquireBudget(IServiceCommand serviceCommand);

		// Token: 0x0600001F RID: 31 RVA: 0x000023A0 File Offset: 0x000005A0
		internal T EndServiceCommand<T>(IAsyncResult asyncResult)
		{
			ExAssert.RetailAssert(Thread.CurrentPrincipal.Identity.IsAuthenticated, "BeginServiceCommand call must be authenticated.");
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ServiceCommandAsyncResult<T> serviceCommandAsyncResult = asyncResult as ServiceCommandAsyncResult<T>;
			return serviceCommandAsyncResult.End();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E0 File Offset: 0x000005E0
		internal void ThrowServiceBusyException(string command, BudgetKey budgetKey, OverBudgetException overBudget)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("command", command);
			ArgumentValidator.ThrowIfNull("budgetKey", budgetKey);
			ArgumentValidator.ThrowIfNull("overBudget", overBudget);
			this.ThrowServiceBusyException(string.Format("{0}-{1}", command, budgetKey.ToString()), new PushNotificationFault(overBudget, overBudget.BackoffTime, true), overBudget);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002433 File Offset: 0x00000633
		internal void ThrowServiceBusyException(string command, BudgetKey budgetKey, Exception ex)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("command", command);
			ArgumentValidator.ThrowIfNull("budgetKey", budgetKey);
			ArgumentValidator.ThrowIfNull("ex", ex);
			this.ThrowServiceBusyException(string.Format("{0}-{1}", command, budgetKey.ToString()), ex);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000246E File Offset: 0x0000066E
		protected void ThrowServiceBusyException(string userId, Exception ex)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("userId", userId);
			ArgumentValidator.ThrowIfNull("ex", ex);
			this.ThrowServiceBusyException(userId, new PushNotificationFault(ex), ex);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002494 File Offset: 0x00000694
		private void ThrowServiceBusyException(string userId, PushNotificationFault fault, Exception ex)
		{
			PushNotificationsCrimsonEvents.ServerBusy.LogPeriodic<string, string>(userId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, userId, ex.ToTraceString());
			if (ExTraceGlobals.PushNotificationServiceTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.PushNotificationServiceTracer.TraceError<string>((long)this.GetHashCode(), "The service is too busy to process the request '{0}'.", string.Format("{0}-{1}", userId, ex.ToTraceString()));
			}
			throw new WebFaultException<PushNotificationFault>(fault, HttpStatusCode.Forbidden);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024F8 File Offset: 0x000006F8
		private IStandardBudget AcquireAndCheckBudget(IServiceCommand serviceCommand)
		{
			IStandardBudget standardBudget = this.AcquireBudget(serviceCommand);
			try
			{
				standardBudget.CheckOverBudget();
				standardBudget.StartConnection("ServiceBase.AcquireAndCheckBudget");
			}
			catch (OverBudgetException overBudget)
			{
				BudgetKey owner = standardBudget.Owner;
				standardBudget.Dispose();
				this.ThrowServiceBusyException(serviceCommand.Description, standardBudget.Owner, overBudget);
			}
			return standardBudget;
		}

		// Token: 0x04000014 RID: 20
		protected const int DefaultWlmMaxRequestsQueued = 500;

		// Token: 0x04000015 RID: 21
		protected const int DefaultWlmMaxWorkerThreadsPerProc = 10;

		// Token: 0x04000016 RID: 22
		internal static readonly SidBudgetKey LocalSystemBudgetKey = new SidBudgetKey(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), BudgetType.PushNotificationTenant, true, ADSessionSettings.FromRootOrgScopeSet());
	}
}
