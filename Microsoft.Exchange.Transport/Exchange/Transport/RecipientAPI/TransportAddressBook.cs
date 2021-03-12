using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x0200052D RID: 1325
	internal abstract class TransportAddressBook : AddressBook
	{
		// Token: 0x06003DC5 RID: 15813
		public abstract ADOperationResult TryGetIsInternal(RoutingAddress address, bool acceptedDomainsOnly, out bool isInternal);
	}
}
