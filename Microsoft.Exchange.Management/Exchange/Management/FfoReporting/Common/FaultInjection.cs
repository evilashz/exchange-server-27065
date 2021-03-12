using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.FfoReporting;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Management.ReportingTask;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003D2 RID: 978
	internal static class FaultInjection
	{
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x0008E2F0 File Offset: 0x0008C4F0
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (FaultInjection.faultInjectionTracer == null)
				{
					lock (FaultInjection.lockObject)
					{
						if (FaultInjection.faultInjectionTracer == null)
						{
							FaultInjectionTrace faultInjectionTrace = ExTraceGlobals.FaultInjectionTracer;
							faultInjectionTrace.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(FaultInjection.Callback));
							FaultInjection.faultInjectionTracer = faultInjectionTrace;
						}
					}
				}
				return FaultInjection.faultInjectionTracer;
			}
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0008E35C File Offset: 0x0008C55C
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if (typeof(InvalidExpressionException).FullName.Contains(exceptionType))
				{
					return new InvalidExpressionException(new LocalizedString("Fault Injection"));
				}
				if (typeof(Exception).FullName.Contains(exceptionType))
				{
					return new Exception("Fault Injection");
				}
			}
			return result;
		}

		// Token: 0x04001B9A RID: 7066
		internal const uint InternalValidateLid = 3355847997U;

		// Token: 0x04001B9B RID: 7067
		internal const uint InternalProcessRecordLid = 3070635325U;

		// Token: 0x04001B9C RID: 7068
		internal const uint RetrieveDalObjectsLid = 4270206269U;

		// Token: 0x04001B9D RID: 7069
		internal const uint InternalValidateAddMailboxErrors = 3783667005U;

		// Token: 0x04001B9E RID: 7070
		private const string ExceptionMessage = "Fault Injection";

		// Token: 0x04001B9F RID: 7071
		private static object lockObject = new object();

		// Token: 0x04001BA0 RID: 7072
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
