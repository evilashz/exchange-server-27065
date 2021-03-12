using System;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000009 RID: 9
	internal interface IAddressFinder
	{
		// Token: 0x0600002B RID: 43
		IRoutingKey[] Find(AddressFinderSource source, IAddressFinderDiagnostics diagnostics);
	}
}
