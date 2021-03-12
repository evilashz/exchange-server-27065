using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000231 RID: 561
	internal class EnhancedDnsTargetHost : TargetHost
	{
		// Token: 0x060018D7 RID: 6359 RVA: 0x00064C19 File Offset: 0x00062E19
		public EnhancedDnsTargetHost(string targetName, List<IPAddress> addresses, TimeSpan timeToLive, ushort port) : base(targetName, addresses, timeToLive)
		{
			this.port = port;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00064C2C File Offset: 0x00062E2C
		public ushort Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x04000BF6 RID: 3062
		private readonly ushort port;
	}
}
