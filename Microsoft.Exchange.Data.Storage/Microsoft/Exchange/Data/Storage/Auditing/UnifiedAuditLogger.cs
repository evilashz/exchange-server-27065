using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F1B RID: 3867
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnifiedAuditLogger : IDisposable
	{
		// Token: 0x0600851B RID: 34075 RVA: 0x002466C4 File Offset: 0x002448C4
		static UnifiedAuditLogger()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				UnifiedAuditLogger.CurrentProcessId = currentProcess.Id;
			}
		}

		// Token: 0x0600851C RID: 34076 RVA: 0x00246720 File Offset: 0x00244920
		public UnifiedAuditLogger()
		{
			string[] fields = (from f in LocalQueueCsvFields.Fields
			select f.Name).ToArray<string>();
			this.logSchema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "audit", fields);
			LogHeaderFormatter headerFormatter = new LogHeaderFormatter(this.logSchema, LogHeaderCsvOption.NotCsvCompatible);
			this.localQueue = new Log("audit", headerFormatter, "Exchange");
			UnifiedAuditLoggerSettings unifiedAuditLoggerSettings = UnifiedAuditLoggerSettings.Load();
			this.localQueue.Configure(Path.Combine(unifiedAuditLoggerSettings.DirectoryPath, "Exchange"), unifiedAuditLoggerSettings.MaxAge, (long)unifiedAuditLoggerSettings.MaxDirectorySize.ToBytes(), (long)unifiedAuditLoggerSettings.MaxFileSize.ToBytes(), (int)unifiedAuditLoggerSettings.CacheSize.ToBytes(), unifiedAuditLoggerSettings.FlushInterval, unifiedAuditLoggerSettings.FlushToDisk);
		}

		// Token: 0x0600851D RID: 34077 RVA: 0x00246800 File Offset: 0x00244A00
		public int WriteAuditRecord(AuditRecord record)
		{
			LogRowFormatter row;
			int result = this.ConvertAuditRecord(record, out row);
			this.localQueue.Append(row, 0);
			return result;
		}

		// Token: 0x0600851E RID: 34078 RVA: 0x00246825 File Offset: 0x00244A25
		public void Dispose()
		{
			this.localQueue.Flush();
			this.localQueue.Close();
		}

		// Token: 0x0600851F RID: 34079 RVA: 0x00246840 File Offset: 0x00244A40
		public int ConvertAuditRecord(AuditRecord record, out LogRowFormatter logRowFormatter)
		{
			logRowFormatter = new LogRowFormatter(this.logSchema, true);
			int byteCount;
			using (RecordSerializer recordSerializer = new RecordSerializer())
			{
				string text = recordSerializer.Write(record, record.RecordType);
				byteCount = Encoding.UTF8.GetByteCount(text);
				logRowFormatter[0] = DateTime.UtcNow;
				logRowFormatter[1] = UnifiedAuditLogger.CurrentProcessId;
				logRowFormatter[2] = UnifiedAuditLogger.CurrentApplicationName;
				logRowFormatter[3] = record.RecordType;
				logRowFormatter[4] = text;
				logRowFormatter[5] = string.Empty;
			}
			return byteCount;
		}

		// Token: 0x0400592A RID: 22826
		private const string FileNamePrefix = "audit";

		// Token: 0x0400592B RID: 22827
		private const string ComponentName = "Exchange";

		// Token: 0x0400592C RID: 22828
		private const string Software = "Microsoft Exchange";

		// Token: 0x0400592D RID: 22829
		private const string Version = "15.00.1497.010";

		// Token: 0x0400592E RID: 22830
		private const string LogType = "audit";

		// Token: 0x0400592F RID: 22831
		private static readonly int CurrentProcessId;

		// Token: 0x04005930 RID: 22832
		private static readonly string CurrentApplicationName = ApplicationName.Current.Name.ToLower();

		// Token: 0x04005931 RID: 22833
		private readonly LogSchema logSchema;

		// Token: 0x04005932 RID: 22834
		private readonly Log localQueue;
	}
}
