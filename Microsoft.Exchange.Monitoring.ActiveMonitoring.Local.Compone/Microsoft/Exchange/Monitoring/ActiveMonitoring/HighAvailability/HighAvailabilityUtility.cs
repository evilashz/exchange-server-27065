using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Common.Extensions;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000198 RID: 408
	internal static class HighAvailabilityUtility
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0004A11A File Offset: 0x0004831A
		public static IRegistryReader RegReader
		{
			get
			{
				return CachedRegistryReader.Instance;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0004A121 File Offset: 0x00048321
		public static IRegistryReader NonCachedRegReader
		{
			get
			{
				return RegistryReader.Instance;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0004A128 File Offset: 0x00048328
		public static IRegistryWriter RegWriter
		{
			get
			{
				return RegistryWriter.Instance;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0004A12F File Offset: 0x0004832F
		public static string ConstructProbeName(string mask, string categoryName)
		{
			return string.Format("{0}/{1}", mask, categoryName);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0004A140 File Offset: 0x00048340
		public static void LogEvent(string eventLogName, string eventSource, long eventId, int categoryId, EventLogEntryType eventType, object[] eventData)
		{
			using (EventLog eventLog = new EventLog(eventLogName))
			{
				eventLog.Source = eventSource;
				EventInstance instance = new EventInstance(eventId, categoryId, eventType);
				eventLog.WriteEvent(instance, eventData);
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0004A18C File Offset: 0x0004838C
		public static bool CheckCancellationRequested(CancellationToken token)
		{
			bool result = false;
			try
			{
				result = token.IsCancellationRequested;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
