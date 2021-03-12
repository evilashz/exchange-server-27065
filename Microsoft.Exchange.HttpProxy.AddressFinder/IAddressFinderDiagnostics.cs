using System;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000002 RID: 2
	internal interface IAddressFinderDiagnostics
	{
		// Token: 0x06000001 RID: 1
		void AddErrorInfo(object value);

		// Token: 0x06000002 RID: 2
		void AddRoutingkey(IRoutingKey routingKey, string routingHint);

		// Token: 0x06000003 RID: 3
		void LogRoutingKeys();

		// Token: 0x06000004 RID: 4
		void LogUnhandledException(Exception ex);
	}
}
