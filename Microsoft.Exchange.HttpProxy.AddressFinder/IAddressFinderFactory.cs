using System;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000004 RID: 4
	internal interface IAddressFinderFactory
	{
		// Token: 0x0600000A RID: 10
		IAddressFinder CreateAddressFinder(ProtocolType protocolType, string urlAbsolutePath);
	}
}
