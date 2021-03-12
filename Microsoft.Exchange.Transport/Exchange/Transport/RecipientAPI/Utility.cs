using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x02000534 RID: 1332
	internal static class Utility
	{
		// Token: 0x06003E18 RID: 15896 RVA: 0x00104E24 File Offset: 0x00103024
		internal static bool IsValidAddress(RoutingAddress address)
		{
			return address.IsValid && !(address == RoutingAddress.NullReversePath);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00104E44 File Offset: 0x00103044
		internal static void Separate(string address, out string local, out string domain)
		{
			RoutingAddress address2 = (RoutingAddress)address;
			if (!Utility.IsValidAddress(address2))
			{
				local = null;
				domain = null;
			}
			local = address2.LocalPart;
			domain = address2.DomainPart;
		}
	}
}
