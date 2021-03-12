using System;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000008 RID: 8
	internal class FileRequestLogger : RequestLogger
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000238F File Offset: 0x0000058F
		public FileRequestLogger(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			this.logData = httpContext.GetLogData();
			this.latencyTracker = new LatencyTracker(this.logData);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023C2 File Offset: 0x000005C2
		public override LatencyTracker LatencyTracker
		{
			get
			{
				return this.latencyTracker;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023CA File Offset: 0x000005CA
		public override void LogField(LogKey key, object value)
		{
			this.logData[key] = value;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023D9 File Offset: 0x000005D9
		public override void AppendGenericInfo(string key, object value)
		{
			this.logData.AppendGenericInfo(key, value);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023E8 File Offset: 0x000005E8
		public override void AppendErrorInfo(string key, object value)
		{
			this.logData.AppendErrorInfo(key, value);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023F8 File Offset: 0x000005F8
		public override void LogExceptionDetails(string key, Exception ex)
		{
			if (ex != null)
			{
				string value = ex.ToString();
				this.AppendErrorInfo(key, value);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002417 File Offset: 0x00000617
		public override void Flush()
		{
			RequestLogListener.AppendLog(this.logData);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002424 File Offset: 0x00000624
		internal LogData ExposeLogDataForTesting()
		{
			return this.logData;
		}

		// Token: 0x04000010 RID: 16
		private LatencyTracker latencyTracker;

		// Token: 0x04000011 RID: 17
		private LogData logData;
	}
}
