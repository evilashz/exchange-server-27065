using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000002 RID: 2
	internal sealed class CachedSenderIdResults
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CachedSenderIdResults()
		{
			this.cachedDomains = new Dictionary<string, List<CachedSenderIdResults.IPAddressResultPair>>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E4 File Offset: 0x000002E4
		public SenderIdResult GetCachedResult(string domain, IPAddress ipAddress)
		{
			List<CachedSenderIdResults.IPAddressResultPair> list = null;
			if (this.cachedDomains.TryGetValue(domain, out list))
			{
				foreach (CachedSenderIdResults.IPAddressResultPair ipaddressResultPair in list)
				{
					if (ipaddressResultPair.IpAddress.Equals(ipAddress))
					{
						return ipaddressResultPair.SenderIdResult;
					}
				}
			}
			return null;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002158 File Offset: 0x00000358
		public void SaveResult(string domain, IPAddress ipAddress, SenderIdResult senderIdResult)
		{
			List<CachedSenderIdResults.IPAddressResultPair> list = null;
			if (!this.cachedDomains.TryGetValue(domain, out list))
			{
				list = new List<CachedSenderIdResults.IPAddressResultPair>();
				this.cachedDomains[domain] = list;
			}
			list.Add(new CachedSenderIdResults.IPAddressResultPair(ipAddress, senderIdResult));
		}

		// Token: 0x04000001 RID: 1
		private Dictionary<string, List<CachedSenderIdResults.IPAddressResultPair>> cachedDomains;

		// Token: 0x02000003 RID: 3
		private class IPAddressResultPair
		{
			// Token: 0x06000004 RID: 4 RVA: 0x00002197 File Offset: 0x00000397
			public IPAddressResultPair(IPAddress ipAddress, SenderIdResult senderIdResult)
			{
				this.IpAddress = ipAddress;
				this.SenderIdResult = senderIdResult;
			}

			// Token: 0x04000002 RID: 2
			public readonly IPAddress IpAddress;

			// Token: 0x04000003 RID: 3
			public readonly SenderIdResult SenderIdResult;
		}
	}
}
