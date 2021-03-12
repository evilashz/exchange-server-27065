using System;
using System.Web;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000006 RID: 6
	public abstract class RequestLogger
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16
		public abstract LatencyTracker LatencyTracker { get; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002334 File Offset: 0x00000534
		public static RequestLogger GetLogger(HttpContextBase httpContext)
		{
			if (HttpProxySettings.DiagnosticsEnabled.Value)
			{
				return new FileRequestLogger(httpContext);
			}
			return new DisabledRequestLogger();
		}

		// Token: 0x06000012 RID: 18
		public abstract void LogField(LogKey key, object value);

		// Token: 0x06000013 RID: 19
		public abstract void AppendGenericInfo(string key, object value);

		// Token: 0x06000014 RID: 20
		public abstract void AppendErrorInfo(string key, object value);

		// Token: 0x06000015 RID: 21
		public abstract void LogExceptionDetails(string key, Exception ex);

		// Token: 0x06000016 RID: 22 RVA: 0x0000234E File Offset: 0x0000054E
		public void LastChanceExceptionHandler(Exception ex)
		{
			this.LogExceptionDetails("Watson", ex);
			this.Flush();
		}

		// Token: 0x06000017 RID: 23
		public abstract void Flush();
	}
}
