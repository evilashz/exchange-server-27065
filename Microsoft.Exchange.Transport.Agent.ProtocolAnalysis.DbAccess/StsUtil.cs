using System;
using System.Net;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000017 RID: 23
	internal sealed class StsUtil
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000040AF File Offset: 0x000022AF
		private StsUtil()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000040B8 File Offset: 0x000022B8
		public static bool IsValidSenderIP(IPAddress senderIP)
		{
			return senderIP != null && !senderIP.Equals(IPAddress.Any) && !senderIP.Equals(IPAddress.Broadcast) && !senderIP.Equals(IPAddress.IPv6Any) && !senderIP.Equals(IPAddress.IPv6Loopback) && !senderIP.Equals(IPAddress.IPv6None) && !senderIP.Equals(IPAddress.Loopback) && !senderIP.Equals(IPAddress.None);
		}

		// Token: 0x04000048 RID: 72
		public static readonly DateTime NoAbsoluteExpiration = DateTime.MaxValue;

		// Token: 0x04000049 RID: 73
		public static readonly TimeSpan NoSlidingExpiration = TimeSpan.Zero;
	}
}
