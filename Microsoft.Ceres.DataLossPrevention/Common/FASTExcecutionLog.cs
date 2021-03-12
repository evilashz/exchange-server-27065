using System;
using System.Collections.Generic;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Office.CompliancePolicy;

namespace Microsoft.Ceres.DataLossPrevention.Common
{
	// Token: 0x0200000A RID: 10
	internal class FASTExcecutionLog : ExecutionLog
	{
		// Token: 0x06000048 RID: 72 RVA: 0x0000323C File Offset: 0x0000143C
		public override void LogOneEntry(string client, string correlationId, ExecutionLog.EventType eventType, string contextData, Exception exception)
		{
			this.LogOneEntry(client, null, correlationId, eventType, null, contextData, exception, null);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000325C File Offset: 0x0000145C
		public override void LogOneEntry(string client, string tenantId, string correlationId, ExecutionLog.EventType eventType, string tag, string contextData, Exception exception, params KeyValuePair<string, object>[] customData)
		{
			ULSTraceLevel ulstraceLevel = FASTExcecutionLog.ConvertEventTypeToULSLevel(eventType);
			if (exception == null)
			{
				ULS.SendTraceTag(4850017U, ULSCat.msoulscat_SEARCH_DataLossPrevention, ulstraceLevel, "Client : [{0}] ; CorrelationId : [{1}] ; ContextData : [{2}]", new object[]
				{
					client,
					correlationId,
					contextData
				});
				return;
			}
			ULS.SendTraceTag(4850018U, ULSCat.msoulscat_SEARCH_DataLossPrevention, ulstraceLevel, "Client=[{0}]  CorrelationId=[{1}]  ContextData=[{2}]  Exception=[{3}]", new object[]
			{
				client,
				correlationId,
				contextData,
				exception
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000032D0 File Offset: 0x000014D0
		private static ULSTraceLevel ConvertEventTypeToULSLevel(ExecutionLog.EventType eventType)
		{
			switch (eventType)
			{
			case ExecutionLog.EventType.Verbose:
				return 100;
			case ExecutionLog.EventType.Information:
				return 50;
			case ExecutionLog.EventType.Warning:
				return 20;
			case ExecutionLog.EventType.Error:
			case ExecutionLog.EventType.CriticalError:
				return 10;
			default:
				return 50;
			}
		}
	}
}
