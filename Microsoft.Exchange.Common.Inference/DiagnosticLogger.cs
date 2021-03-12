using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x0200000A RID: 10
	public class DiagnosticLogger : IDiagnosticLogger, IDisposable
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002580 File Offset: 0x00000780
		public DiagnosticLogger(IDiagnosticLogConfig config) : this(config, null, 0)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000258B File Offset: 0x0000078B
		public DiagnosticLogger(IDiagnosticLogConfig config, Trace tracer, int traceId)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			this.traceId = traceId;
			this.tracer = tracer;
			this.config = config;
			this.log = new LogWrapper(this.config, DiagnosticLogger.Columns);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000025C9 File Offset: 0x000007C9
		internal string[] RecentlyLoggedRows
		{
			get
			{
				return this.log.RecentlyLoggedRows;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000025D6 File Offset: 0x000007D6
		public void LogError(string format, params object[] arguments)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceError((long)this.traceId, format, arguments);
			}
			this.Log(LoggingLevel.Error, format, arguments);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000025FD File Offset: 0x000007FD
		public void LogInformation(string format, params object[] arguments)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceInformation(0, (long)this.traceId, format, arguments);
			}
			this.Log(LoggingLevel.Information, format, arguments);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002625 File Offset: 0x00000825
		public void LogDebug(string format, params object[] arguments)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceDebug((long)this.traceId, format, arguments);
			}
			this.Log(LoggingLevel.Debug, format, arguments);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000264C File Offset: 0x0000084C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000265C File Offset: 0x0000085C
		protected virtual void Log(LoggingLevel loggingLevel, string format, params object[] arguments)
		{
			if (!this.config.IsLoggingEnabled)
			{
				return;
			}
			if (this.config.LoggingLevel < loggingLevel)
			{
				return;
			}
			IList<object> list = new List<object>(DiagnosticLogger.Columns.Length);
			string item = (arguments.Length != 0) ? new StringBuilder(format.Length).AppendFormat(format, arguments).ToString() : format;
			list.Add(DateTime.UtcNow);
			list.Add(loggingLevel);
			list.Add(item);
			this.log.Append(list);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000026E2 File Offset: 0x000008E2
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing && this.log != null)
				{
					this.log.Dispose();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x0400001B RID: 27
		internal static readonly string[] Columns = new string[]
		{
			"date-time",
			"level",
			"message"
		};

		// Token: 0x0400001C RID: 28
		private readonly IDiagnosticLogConfig config;

		// Token: 0x0400001D RID: 29
		private readonly LogWrapper log;

		// Token: 0x0400001E RID: 30
		private readonly Trace tracer;

		// Token: 0x0400001F RID: 31
		private readonly int traceId;

		// Token: 0x04000020 RID: 32
		private bool isDisposed;

		// Token: 0x0200000B RID: 11
		private enum LogField
		{
			// Token: 0x04000022 RID: 34
			Time,
			// Token: 0x04000023 RID: 35
			Level,
			// Token: 0x04000024 RID: 36
			Message
		}
	}
}
