using System;
using System.Web;

namespace Microsoft.Exchange.HttpProxy.ProxyAssistant
{
	// Token: 0x02000003 RID: 3
	internal class ProxyAssistantDiagnostics : IProxyAssistantDiagnostics
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020D0 File Offset: 0x000002D0
		public ProxyAssistantDiagnostics(HttpContextBase httpContext)
		{
			this.logger = RequestLogger.GetLogger(httpContext);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public void AddErrorInfo(object value)
		{
			this.logger.AppendErrorInfo("ProxyAssistant", value);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F7 File Offset: 0x000002F7
		public void LogUnhandledException(Exception ex)
		{
			this.logger.LastChanceExceptionHandler(ex);
		}

		// Token: 0x04000001 RID: 1
		private RequestLogger logger;
	}
}
