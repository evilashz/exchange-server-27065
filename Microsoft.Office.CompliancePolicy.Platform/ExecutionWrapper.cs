using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200004E RID: 78
	internal static class ExecutionWrapper
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00006040 File Offset: 0x00004240
		public static void DoRetriableWork(Action doWork, uint retryTimes, Func<Exception, bool> isRetriableError, bool mapGrayException = true)
		{
			uint num = 0U;
			try
			{
				IL_02:
				if (mapGrayException)
				{
					GrayException.MapAndReportGrayExceptions(doWork);
				}
				else
				{
					doWork();
				}
			}
			catch (Exception ex)
			{
				if (isRetriableError(ex) && num < retryTimes)
				{
					num += 1U;
					Thread.Sleep(1000);
					goto IL_02;
				}
				throw ex;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006094 File Offset: 0x00004294
		public static void DoRetriableWorkAndLogIfFail(Action doWork, uint retryTimes, Func<Exception, bool> isRetriableError, ExecutionLog logger, string logClient, string tenantId, string correlationId, ExecutionLog.EventType eventType, string tag, string contextData, bool mapGrayException = true)
		{
			try
			{
				ExecutionWrapper.DoRetriableWork(doWork, retryTimes, isRetriableError, mapGrayException);
			}
			catch (Exception ex)
			{
				logger.LogOneEntry(logClient, tenantId, correlationId, eventType, tag, contextData, ex, new KeyValuePair<string, object>[0]);
				throw ex;
			}
		}
	}
}
