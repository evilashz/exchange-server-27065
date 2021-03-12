using System;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x02000003 RID: 3
	internal interface IRouteSelectorDiagnostics : IRoutingDiagnostics
	{
		// Token: 0x06000003 RID: 3
		void SetOrganization(string value);

		// Token: 0x06000004 RID: 4
		void AddRoutingEntry(string value);

		// Token: 0x06000005 RID: 5
		void AddErrorInfo(object value);

		// Token: 0x06000006 RID: 6
		void ProcessRoutingKey(IRoutingKey key);

		// Token: 0x06000007 RID: 7
		void ProcessRoutingEntry(IRoutingEntry entry);
	}
}
