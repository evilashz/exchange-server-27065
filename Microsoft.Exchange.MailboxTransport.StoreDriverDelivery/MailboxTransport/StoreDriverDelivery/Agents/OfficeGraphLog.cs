using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000A7 RID: 167
	internal class OfficeGraphLog
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x0001E560 File Offset: 0x0001C760
		public static void Start()
		{
			OfficeGraphLog.officeGraphLogSchema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "Office Graph Log", OfficeGraphLog.Fields);
			OfficeGraphLog.log = new Log(OfficeGraphLogSchema.LogPrefix, new LogHeaderFormatter(OfficeGraphLog.officeGraphLogSchema), "OfficeGraph");
			OfficeGraphLog.log.Configure("D:\\OfficeGraph", TimeSpan.FromDays(1.0), 104857600L, 5242880L);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
		public static void LogSignal(OfficeGraphSignalType signalType, string signal, string organizationId, string sharePointUrl)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(OfficeGraphLog.officeGraphLogSchema);
			logRowFormatter[1] = signalType.ToString();
			logRowFormatter[2] = signal;
			logRowFormatter[3] = organizationId;
			logRowFormatter[4] = sharePointUrl;
			OfficeGraphLog.Append(logRowFormatter);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001E61C File Offset: 0x0001C81C
		public static void Stop()
		{
			if (OfficeGraphLog.log != null)
			{
				OfficeGraphLog.log.Close();
				OfficeGraphLog.log = null;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001E638 File Offset: 0x0001C838
		private static string[] InitializeFields()
		{
			string[] array = new string[Enum.GetValues(typeof(OfficeGraphLog.OfficeGraphLogField)).Length];
			array[0] = "TimeStamp";
			array[1] = "SignalType";
			array[2] = "Signal";
			array[3] = "OrganizationId";
			array[4] = "SharePointUrl";
			return array;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001E688 File Offset: 0x0001C888
		private static void Append(LogRowFormatter row)
		{
			OfficeGraphLog.log.Append(row, 0, DateTime.UtcNow);
		}

		// Token: 0x04000318 RID: 792
		private const string LogComponentName = "OfficeGraph";

		// Token: 0x04000319 RID: 793
		private const string LogPath = "D:\\OfficeGraph";

		// Token: 0x0400031A RID: 794
		private static readonly string[] Fields = OfficeGraphLog.InitializeFields();

		// Token: 0x0400031B RID: 795
		private static LogSchema officeGraphLogSchema;

		// Token: 0x0400031C RID: 796
		private static Log log;

		// Token: 0x020000A8 RID: 168
		internal enum OfficeGraphLogField
		{
			// Token: 0x0400031E RID: 798
			TimeStamp,
			// Token: 0x0400031F RID: 799
			SignalType,
			// Token: 0x04000320 RID: 800
			Signal,
			// Token: 0x04000321 RID: 801
			OrganizationId,
			// Token: 0x04000322 RID: 802
			SharePointUrl
		}
	}
}
