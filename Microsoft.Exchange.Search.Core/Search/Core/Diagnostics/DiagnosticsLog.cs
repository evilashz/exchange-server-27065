using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x02000093 RID: 147
	internal sealed class DiagnosticsLog
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		internal DiagnosticsLog(IDiagnosticsLogConfig diagnosticsLogConfig, string[] columns)
		{
			Util.ThrowOnNullArgument(diagnosticsLogConfig, "diagnosticsLogConfig");
			this.diagnosticsLogConfig = diagnosticsLogConfig;
			this.columns = columns;
			this.IsEnabled = diagnosticsLogConfig.IsEnabled;
			if (this.IsEnabled)
			{
				this.Schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", this.diagnosticsLogConfig.LogTypeName, columns);
				if (!DiagnosticsLog.logSingletons.TryGetValue(this.diagnosticsLogConfig.LogTypeName, out this.log))
				{
					lock (DiagnosticsLog.lockObject)
					{
						if (!DiagnosticsLog.logSingletons.TryGetValue(this.diagnosticsLogConfig.LogTypeName, out this.log))
						{
							this.log = new Log(this.diagnosticsLogConfig.LogFilePrefix, new LogHeaderFormatter(this.Schema), this.diagnosticsLogConfig.LogComponent);
							this.log.Configure(this.diagnosticsLogConfig.LogFilePath, TimeSpan.FromHours((double)diagnosticsLogConfig.MaxAge), (long)(this.diagnosticsLogConfig.MaxDirectorySize * 1024), (long)(this.diagnosticsLogConfig.MaxFileSize * 1024));
							DiagnosticsLog.logSingletons.Add(this.diagnosticsLogConfig.LogTypeName, this.log);
						}
					}
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000CF0C File Offset: 0x0000B10C
		internal static int CountOfLogInstances
		{
			get
			{
				return DiagnosticsLog.logSingletons.Count;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000CF18 File Offset: 0x0000B118
		internal Log Log
		{
			get
			{
				return this.log;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000CF20 File Offset: 0x0000B120
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000CF28 File Offset: 0x0000B128
		internal LogSchema Schema { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000CF31 File Offset: 0x0000B131
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000CF39 File Offset: 0x0000B139
		internal bool IsEnabled { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000CF42 File Offset: 0x0000B142
		internal IDiagnosticsLogConfig Config
		{
			get
			{
				return this.diagnosticsLogConfig;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000CF4A File Offset: 0x0000B14A
		private static Breadcrumbs<string> ExtendedLoggingRows
		{
			get
			{
				if (DiagnosticsLog.extendedLoggingRows == null)
				{
					DiagnosticsLog.extendedLoggingRows = new Breadcrumbs<string>(16);
				}
				return DiagnosticsLog.extendedLoggingRows;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000CF64 File Offset: 0x0000B164
		public string[] GetFormattedExtendedLogging()
		{
			if (!this.diagnosticsLogConfig.IncludeExtendedLogging)
			{
				return null;
			}
			return DiagnosticsLog.ExtendedLoggingRows.BreadCrumb;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000CF80 File Offset: 0x0000B180
		internal string Append(params object[] values)
		{
			if (this.columns.Length != values.Length)
			{
				throw new ArgumentException("The number of values to log does not match the number of columns names.");
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.Schema);
			int num = 0;
			foreach (object value in values)
			{
				logRowFormatter[num++] = value;
			}
			string text = string.Empty;
			if (this.diagnosticsLogConfig.IncludeExtendedLogging)
			{
				bool flag;
				text = ExDateTime.UtcNow + LogRowFormatter.FormatCollection(this.GetRowFormatterEnumerator(logRowFormatter), out flag);
				DiagnosticsLog.ExtendedLoggingRows.Drop(text);
			}
			this.log.Append(logRowFormatter, 0);
			return text;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000D134 File Offset: 0x0000B334
		private IEnumerable GetRowFormatterEnumerator(LogRowFormatter rowFormatter)
		{
			for (int i = 0; i < this.columns.Length; i++)
			{
				yield return rowFormatter[i];
			}
			yield break;
		}

		// Token: 0x040001C0 RID: 448
		private static readonly object lockObject = new object();

		// Token: 0x040001C1 RID: 449
		private static Dictionary<string, Log> logSingletons = new Dictionary<string, Log>();

		// Token: 0x040001C2 RID: 450
		[ThreadStatic]
		private static Breadcrumbs<string> extendedLoggingRows;

		// Token: 0x040001C3 RID: 451
		private IDiagnosticsLogConfig diagnosticsLogConfig;

		// Token: 0x040001C4 RID: 452
		private string[] columns;

		// Token: 0x040001C5 RID: 453
		private Log log;
	}
}
