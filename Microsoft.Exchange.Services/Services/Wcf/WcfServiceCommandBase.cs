using System;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000313 RID: 787
	internal abstract class WcfServiceCommandBase
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x000737CA File Offset: 0x000719CA
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x000737D2 File Offset: 0x000719D2
		internal CallContext CallContext { get; private set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x000737DB File Offset: 0x000719DB
		// (set) Token: 0x0600163E RID: 5694 RVA: 0x000737E3 File Offset: 0x000719E3
		private protected IdConverter IdConverter { protected get; private set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000737EC File Offset: 0x000719EC
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x000737F4 File Offset: 0x000719F4
		private protected bool IsRequestTracingEnabled { protected get; private set; }

		// Token: 0x06001641 RID: 5697 RVA: 0x000737FD File Offset: 0x000719FD
		protected WcfServiceCommandBase(CallContext callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(callContext, "callContext", "ServiceCommand::ServiceCommand");
			this.CallContext = callContext;
			this.IdConverter = new IdConverter(callContext);
			this.IsRequestTracingEnabled = this.CallContext.IsRequestTracingEnabled;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x00073839 File Offset: 0x00071A39
		protected MailboxSession MailboxIdentityMailboxSession
		{
			get
			{
				return this.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0007384C File Offset: 0x00071A4C
		protected void PrepareBudgetAndActivityScope()
		{
			WcfServiceCommandBase.ThrowIfNull(this.CallContext.Budget, "budget", "ServiceCommand::Execute");
			this.CallContext.Budget.StartLocal("ServiceCommand.Execute[" + this.CallContext.MethodName + "]", default(TimeSpan));
			this.scope = ActivityContext.GetCurrentActivityScope();
			if (this.scope != null)
			{
				this.scope.SetProperty(ServiceLatencyMetadata.PreExecutionLatency, this.scope.TotalMilliseconds.ToString(NumberFormatInfo.InvariantInfo));
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000738E4 File Offset: 0x00071AE4
		protected void PreExecute()
		{
			if (string.IsNullOrEmpty(this.scope.Component) && this.CallContext != null)
			{
				this.scope.Component = this.CallContext.WorkloadType.ToString();
			}
			if (string.IsNullOrEmpty(this.scope.Action))
			{
				this.scope.Action = base.GetType().Name;
			}
			this.preExecuteADSnapshot = this.scope.TakeStatisticsSnapshot(AggregatedOperationType.ADCalls);
			this.preExecuteRPCSnapshot = this.scope.TakeStatisticsSnapshot(AggregatedOperationType.StoreRPCs);
			this.executeStopWatch = Stopwatch.StartNew();
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00073984 File Offset: 0x00071B84
		protected void PostExecute()
		{
			this.executeStopWatch.Stop();
			this.scope.SetProperty(ServiceLatencyMetadata.CoreExecutionLatency, this.executeStopWatch.ElapsedMilliseconds.ToString(NumberFormatInfo.InvariantInfo));
			AggregatedOperationStatistics aggregatedOperationStatistics = this.scope.TakeStatisticsSnapshot(AggregatedOperationType.ADCalls) - this.preExecuteADSnapshot;
			if (aggregatedOperationStatistics.Count > 0L)
			{
				this.scope.SetProperty(ServiceTaskMetadata.ADCount, aggregatedOperationStatistics.Count.ToString(NumberFormatInfo.InvariantInfo));
				this.scope.SetProperty(ServiceTaskMetadata.ADLatency, aggregatedOperationStatistics.TotalMilliseconds.ToString(NumberFormatInfo.InvariantInfo));
			}
			AggregatedOperationStatistics aggregatedOperationStatistics2 = this.scope.TakeStatisticsSnapshot(AggregatedOperationType.StoreRPCs) - this.preExecuteRPCSnapshot;
			if (aggregatedOperationStatistics2.Count > 0L)
			{
				this.scope.SetProperty(ServiceTaskMetadata.RpcCount, aggregatedOperationStatistics2.Count.ToString(NumberFormatInfo.InvariantInfo));
				this.scope.SetProperty(ServiceTaskMetadata.RpcLatency, aggregatedOperationStatistics2.TotalMilliseconds.ToString(NumberFormatInfo.InvariantInfo));
			}
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00073A94 File Offset: 0x00071C94
		protected static void ThrowIfNull(object objectToCheck, string parameterName, string methodName)
		{
			if (objectToCheck == null)
			{
				string message = WcfServiceCommandBase.BuildExceptionMessage(methodName, parameterName, "is null.");
				throw new FaultException(new ArgumentNullException(parameterName, message).Message);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00073AC3 File Offset: 0x00071CC3
		protected virtual void LogTracesForCurrentRequest()
		{
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00073AC8 File Offset: 0x00071CC8
		private static string BuildExceptionMessage(string methodName, string parameterName, string message)
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0}] {1} {2}", new object[]
			{
				methodName,
				parameterName,
				message
			});
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00073AF8 File Offset: 0x00071CF8
		protected void LogRequestTraces()
		{
			if (this.IsRequestTracingEnabled)
			{
				this.LogTracesForCurrentRequest();
			}
		}

		// Token: 0x04000EE6 RID: 3814
		protected static readonly TraceToHeadersLoggerFactory TraceLoggerFactory = new TraceToHeadersLoggerFactory(VariantConfiguration.InvariantNoFlightingSnapshot.Diagnostics.TraceToHeadersLogger.Enabled);

		// Token: 0x04000EE7 RID: 3815
		protected IActivityScope scope;

		// Token: 0x04000EE8 RID: 3816
		private Stopwatch executeStopWatch;

		// Token: 0x04000EE9 RID: 3817
		private AggregatedOperationStatistics preExecuteADSnapshot;

		// Token: 0x04000EEA RID: 3818
		private AggregatedOperationStatistics preExecuteRPCSnapshot;
	}
}
