using System;

namespace Microsoft.Exchange.HttpProxy.ProxyAssistant
{
	// Token: 0x02000002 RID: 2
	internal interface IProxyAssistantDiagnostics
	{
		// Token: 0x06000001 RID: 1
		void AddErrorInfo(object value);

		// Token: 0x06000002 RID: 2
		void LogUnhandledException(Exception ex);
	}
}
