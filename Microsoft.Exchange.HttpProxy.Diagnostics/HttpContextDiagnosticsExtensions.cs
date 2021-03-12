using System;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000B RID: 11
	internal static class HttpContextDiagnosticsExtensions
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000026E4 File Offset: 0x000008E4
		public static LogData GetLogData(this HttpContextBase httpContext)
		{
			LogData logData = httpContext.Items["LogData_da486740-fc1c-4d69-8e87-bfb3757ad732"] as LogData;
			if (logData == null)
			{
				throw new InvalidOperationException("LogData is not initialized.");
			}
			return logData;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002718 File Offset: 0x00000918
		public static void InitializeLogging(this HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (httpContext.Items.Contains("LogData_da486740-fc1c-4d69-8e87-bfb3757ad732"))
			{
				throw new InvalidOperationException("LogData for this request is already initialized in the HttpContext.");
			}
			LogData value = new LogData();
			httpContext.Items.Add("LogData_da486740-fc1c-4d69-8e87-bfb3757ad732", value);
		}

		// Token: 0x0400005F RID: 95
		private const string LogDataKey = "LogData_da486740-fc1c-4d69-8e87-bfb3757ad732";

		// Token: 0x04000060 RID: 96
		private const string LatencyDataKey = "LatencyData_da486740-fc1c-4d69-8e87-bfb3757ad732";
	}
}
