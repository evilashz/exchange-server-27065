using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000287 RID: 647
	internal static class LocalComputer
	{
		// Token: 0x06001BD7 RID: 7127 RVA: 0x000725B0 File Offset: 0x000707B0
		public static bool TryGetIPAddresses(out List<IPAddress> localIPAddresses, out NetworkInformationException exception)
		{
			try
			{
				localIPAddresses = ComputerInformation.GetLocalIPAddresses();
				exception = null;
				return true;
			}
			catch (NetworkInformationException ex)
			{
				exception = ex;
			}
			localIPAddresses = null;
			return false;
		}
	}
}
