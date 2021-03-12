using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Mapi
{
	// Token: 0x02000253 RID: 595
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FaultInjectionUtils
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x000332A8 File Offset: 0x000314A8
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if ("Microsoft.Mapi.MapiExceptionNetworkError".Contains(exceptionType))
				{
					result = new MapiExceptionNetworkError("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionExiting".Contains(exceptionType))
				{
					result = new MapiExceptionExiting("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionMdbOffline".Contains(exceptionType))
				{
					result = new MapiExceptionMdbOffline("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionBackupInProgress".Contains(exceptionType))
				{
					result = new MapiExceptionBackupInProgress("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionNotFound".Contains(exceptionType))
				{
					result = new MapiExceptionNotFound("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionUnableToComplete".Contains(exceptionType))
				{
					result = new MapiExceptionUnableToComplete("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionWatermarkError".Contains(exceptionType))
				{
					result = new MapiExceptionWatermarkError("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionWrongServer".Contains(exceptionType))
				{
					result = new MapiExceptionWrongServer("FaultInjection", -1, -1, null, null);
				}
				else if ("Microsoft.Mapi.MapiExceptionNoAccess".Contains(exceptionType))
				{
					result = new MapiExceptionNoAccess("FaultInjection", -2147024891, -2147024891, null, null);
				}
			}
			return result;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000333DE File Offset: 0x000315DE
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (FaultInjectionUtils.faultInjectionTracer == null)
				{
					FaultInjectionUtils.faultInjectionTracer = new FaultInjectionTrace(FaultInjectionUtils.MapiNetComponent, FaultInjectionUtils.tagFaultInjection);
					FaultInjectionUtils.faultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(FaultInjectionUtils.Callback));
				}
				return FaultInjectionUtils.faultInjectionTracer;
			}
		}

		// Token: 0x04001082 RID: 4226
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001083 RID: 4227
		private static Guid MapiNetComponent = new Guid("82914ab6-016b-442c-8e49-2562a4333be0");

		// Token: 0x04001084 RID: 4228
		private static int tagFaultInjection = 35;
	}
}
