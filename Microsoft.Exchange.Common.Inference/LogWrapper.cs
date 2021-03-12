using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x0200000D RID: 13
	public class LogWrapper : DisposeTrackableBase
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002740 File Offset: 0x00000940
		public LogWrapper(ILogConfig config, string[] columnNames)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("config.SoftwareName", config.SoftwareName);
			ArgumentValidator.ThrowIfNull("config.SoftwareVersion", config.SoftwareVersion);
			ArgumentValidator.ThrowIfNull("config.ComponentName", config.ComponentName);
			ArgumentValidator.ThrowIfNull("config.LogType", config.LogType);
			ArgumentValidator.ThrowIfNull("config.LogPrefix", config.LogPrefix);
			ArgumentValidator.ThrowIfNull("config.LogPath", config.LogPath);
			ArgumentValidator.ThrowIfNull("columnNames", columnNames);
			this.inMemoryLogs = new Breadcrumbs<string>(64);
			this.logSchema = new LogSchema(config.SoftwareName, config.SoftwareVersion, config.LogType, columnNames);
			this.log = new Log(config.LogPrefix, new LogHeaderFormatter(this.logSchema), config.ComponentName);
			this.log.Configure(config.LogPath, config.MaxLogAge, (long)config.MaxLogDirectorySize, (long)config.MaxLogFileSize);
			this.isEnabled = config.IsLoggingEnabled;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002845 File Offset: 0x00000A45
		public string[] RecentlyLoggedRows
		{
			get
			{
				return this.inMemoryLogs.BreadCrumb;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002852 File Offset: 0x00000A52
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000285C File Offset: 0x00000A5C
		public void Append(IList<object> values)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (values == null)
			{
				return;
			}
			if (this.logSchema.Fields.Length != values.Count)
			{
				throw new ArgumentException("The number of values to log does not match the number of columns names.");
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			int num = 0;
			foreach (object obj in values)
			{
				object value;
				if (obj is DateTime)
				{
					value = ((DateTime)obj).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo);
				}
				else if (obj is ExDateTime)
				{
					value = ((ExDateTime)obj).UniversalTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo);
				}
				else
				{
					value = obj;
				}
				logRowFormatter[num++] = value;
			}
			if (this.inMemoryLogs != null)
			{
				bool flag;
				string bc = LogRowFormatter.FormatCollection(this.GetRowFormatterEnumerator(logRowFormatter), out flag);
				this.inMemoryLogs.Drop(bc);
			}
			this.log.Append(logRowFormatter, -1);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002970 File Offset: 0x00000B70
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LogWrapper>(this);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002978 File Offset: 0x00000B78
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.log != null)
			{
				this.log.Close();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private IEnumerable GetRowFormatterEnumerator(LogRowFormatter rowFormatter)
		{
			for (int i = 0; i < this.logSchema.Fields.Length; i++)
			{
				yield return rowFormatter[i];
			}
			yield break;
		}

		// Token: 0x04000029 RID: 41
		public const string TimestampFormat = "yyyy-MM-ddTHH\\:mm\\:ss.fffZ";

		// Token: 0x0400002A RID: 42
		private const int InMemoryLogSize = 64;

		// Token: 0x0400002B RID: 43
		private readonly Breadcrumbs<string> inMemoryLogs;

		// Token: 0x0400002C RID: 44
		private readonly LogSchema logSchema;

		// Token: 0x0400002D RID: 45
		private readonly Log log;

		// Token: 0x0400002E RID: 46
		private readonly bool isEnabled;
	}
}
