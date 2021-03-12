using System;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A2 RID: 674
	internal abstract class AvailabilityCommandBase<RequestType, SingleItemType> : SingleStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x00057FC4 File Offset: 0x000561C4
		public AvailabilityCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00057FCE File Offset: 0x000561CE
		internal override void InternalCancelStep(LocalizedException exception, out bool isBatchStopResponse)
		{
			if (exception is OverBudgetException || exception is ResourceUnhealthyException)
			{
				throw FaultExceptionUtilities.CreateAvailabilityFault(new ServerBusyException(exception), FaultParty.Receiver);
			}
			throw FaultExceptionUtilities.CreateAvailabilityFault(exception, FaultParty.Receiver);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00057FF4 File Offset: 0x000561F4
		protected static void CheckRequestStreamSize(HttpRequest currentRequest)
		{
			int maximumRequestStreamSize = Configuration.MaximumRequestStreamSize;
			if (currentRequest.ContentLength > maximumRequestStreamSize)
			{
				throw new RequestStreamTooBigException((long)maximumRequestStreamSize, (long)currentRequest.ContentLength);
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00058020 File Offset: 0x00056220
		protected void LogLatency(PerformanceContext ldapInitialPerformanceContext, PerformanceContext rpcInitialPerformanceContext)
		{
			PerformanceContext performanceContext;
			if (NativeMethods.GetTLSPerformanceContext(out performanceContext))
			{
				uint num = performanceContext.rpcCount - rpcInitialPerformanceContext.rpcCount;
				ulong num2 = (performanceContext.rpcLatency - rpcInitialPerformanceContext.rpcLatency) / 10000UL;
				base.CallContext.HttpContext.Items["TotalRpcRequestCount"] = num;
				base.CallContext.HttpContext.Items["TotalRpcRequestLatency"] = num2;
			}
			PerformanceContext performanceContext2 = PerformanceContext.Current;
			uint num3 = performanceContext2.RequestCount - ldapInitialPerformanceContext.RequestCount;
			int num4 = performanceContext2.RequestLatency - ldapInitialPerformanceContext.RequestLatency;
			base.CallContext.HttpContext.Items["TotalLdapRequestCount"] = num3;
			base.CallContext.HttpContext.Items["TotalLdapRequestLatency"] = num4;
		}

		// Token: 0x04000CE0 RID: 3296
		protected static readonly Trace ConfigurationTracer = ExTraceGlobals.ConfigurationTracer;

		// Token: 0x04000CE1 RID: 3297
		protected static readonly Trace CalendarViewTracer = ExTraceGlobals.CalendarViewTracer;

		// Token: 0x04000CE2 RID: 3298
		protected static readonly Trace SecurityTracer = ExTraceGlobals.SecurityTracer;
	}
}
