using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007FD RID: 2045
	internal class TenantRelocationSyncLogger : DisposeTrackableBase
	{
		// Token: 0x060064F1 RID: 25841 RVA: 0x0015FEA0 File Offset: 0x0015E0A0
		private TenantRelocationSyncLogger()
		{
			this.logSchema = new LogSchema("Microsoft Exchange", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Tenant Relocation Sync Log", Enum.GetNames(typeof(TenantRelocationSyncLogger.TenantRelocationSyncLogFields)));
			this.log = new Log("TenantRelocationSyncLog", new LogHeaderFormatter(this.logSchema, true), "TenantRelocationSyncLogger");
			this.Configure();
		}

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x060064F2 RID: 25842 RVA: 0x0015FF1D File Offset: 0x0015E11D
		public static TenantRelocationSyncLogger Instance
		{
			get
			{
				return TenantRelocationSyncLogger.instance;
			}
		}

		// Token: 0x060064F3 RID: 25843 RVA: 0x0015FF24 File Offset: 0x0015E124
		public void Configure()
		{
			if (!base.IsDisposed)
			{
				lock (this.logLock)
				{
					this.log.Configure(Path.Combine(TenantRelocationSyncLogger.GetExchangeInstallPath(), "Logging\\TenantRelocationLogs\\SyncLog\\"), TimeSpan.FromDays(30.0), 104857600L, 104857600L, 10485760, TimeSpan.FromSeconds(10.0), true);
				}
			}
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x0015FFB0 File Offset: 0x0015E1B0
		public void Close()
		{
			if (!base.IsDisposed && this.log != null)
			{
				this.log.Close();
				this.log = null;
			}
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x0015FFD4 File Offset: 0x0015E1D4
		public void Log(TenantRelocationSyncData syncData, string messageType, string errCode = null, string errorMessage = null, byte[] data = null)
		{
			this.Log(syncData.Source.TenantOrganizationUnit.Rdn.UnescapedName, syncData.Source.PartitionRoot.Rdn.UnescapedName, syncData.Target.PartitionRoot.Rdn.UnescapedName, messageType, errCode, errorMessage, data);
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x0016002C File Offset: 0x0015E22C
		public void Log(string tenantName, string sourceForest, string targetForest, string messageType, string errorCode, string errorMessage, byte[] data)
		{
			this.LogRow(tenantName, Environment.CurrentManagedThreadId, sourceForest, targetForest, messageType, errorCode, errorMessage, data);
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x0016004F File Offset: 0x0015E24F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x0016005A File Offset: 0x0015E25A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TenantRelocationSyncLogger>(this);
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x00160062 File Offset: 0x0015E262
		private static string TruncateAndConvertByteArray(byte[] bytes, int maxLength)
		{
			if (bytes.Length > maxLength)
			{
				return Convert.ToBase64String(bytes, 0, maxLength) + "[Truncated]";
			}
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x00160084 File Offset: 0x0015E284
		private void LogRow(string tenantName, int threadId, string sourceForest, string targetForest, string messageType, string errorCode, string errorMessage, byte[] data)
		{
			LogRowFormatter logRowFormatter = this.NewLogRow();
			logRowFormatter[1] = tenantName;
			logRowFormatter[2] = threadId;
			logRowFormatter[3] = sourceForest;
			logRowFormatter[4] = targetForest;
			logRowFormatter[5] = messageType;
			logRowFormatter[6] = errorCode;
			logRowFormatter[7] = this.TruncateString(errorMessage, 10000);
			logRowFormatter[8] = ((data == null) ? null : Convert.ToBase64String(data));
			this.AppendLogRow(logRowFormatter);
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x00160100 File Offset: 0x0015E300
		private static string GetExchangeInstallPath()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					object value = registryKey.GetValue("MsiInstallPath");
					registryKey.Close();
					if (value == null)
					{
						result = string.Empty;
					}
					else
					{
						result = value.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x0016016C File Offset: 0x0015E36C
		private LogRowFormatter NewLogRow()
		{
			return new LogRowFormatter(this.logSchema);
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x00160179 File Offset: 0x0015E379
		private void AppendLogRow(LogRowFormatter row)
		{
			if (!base.IsDisposed)
			{
				this.log.Append(row, 0);
			}
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x00160190 File Offset: 0x0015E390
		private string TruncateString(string str, int maxLength)
		{
			if (str != null && str.Length > maxLength)
			{
				return str.Substring(0, maxLength) + "[Truncated]";
			}
			return str;
		}

		// Token: 0x04004306 RID: 17158
		private const string LogTypeName = "Tenant Relocation Sync Log";

		// Token: 0x04004307 RID: 17159
		private const string FileNamePrefixName = "TenantRelocationSyncLog";

		// Token: 0x04004308 RID: 17160
		private const int MaxStringLength = 10000;

		// Token: 0x04004309 RID: 17161
		private const int MaxByteArrayLength = 20;

		// Token: 0x0400430A RID: 17162
		private const string LogComponentName = "TenantRelocationSyncLogger";

		// Token: 0x0400430B RID: 17163
		private const string SoftwareName = "Microsoft Exchange";

		// Token: 0x0400430C RID: 17164
		private const string TruncatedSuffix = "[Truncated]";

		// Token: 0x0400430D RID: 17165
		private static readonly TenantRelocationSyncLogger instance = new TenantRelocationSyncLogger();

		// Token: 0x0400430E RID: 17166
		private Log log;

		// Token: 0x0400430F RID: 17167
		private LogSchema logSchema;

		// Token: 0x04004310 RID: 17168
		private object logLock = new object();

		// Token: 0x020007FE RID: 2046
		internal enum TenantRelocationSyncLogFields
		{
			// Token: 0x04004312 RID: 17170
			Timestamp,
			// Token: 0x04004313 RID: 17171
			TenantName,
			// Token: 0x04004314 RID: 17172
			ThreadId,
			// Token: 0x04004315 RID: 17173
			SourceForest,
			// Token: 0x04004316 RID: 17174
			TargetForest,
			// Token: 0x04004317 RID: 17175
			MessageType,
			// Token: 0x04004318 RID: 17176
			ErrorCode,
			// Token: 0x04004319 RID: 17177
			ErrorMessage,
			// Token: 0x0400431A RID: 17178
			Data
		}
	}
}
