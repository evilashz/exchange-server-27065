using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C97 RID: 3223
	internal class TargetHost
	{
		// Token: 0x060046F7 RID: 18167 RVA: 0x000BEE5D File Offset: 0x000BD05D
		public TargetHost(string targetName, IPAddress[] addresses, TimeSpan timeToLive) : this(targetName, new List<IPAddress>(addresses), timeToLive)
		{
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x000BEE6D File Offset: 0x000BD06D
		public TargetHost(string targetName, List<IPAddress> addresses, TimeSpan timeToLive)
		{
			this.name = targetName;
			this.addresses = addresses;
			this.TimeToLive = timeToLive;
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x000BEE8A File Offset: 0x000BD08A
		private TargetHost()
		{
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x000BEE92 File Offset: 0x000BD092
		public List<IPAddress> IPAddresses
		{
			get
			{
				return this.addresses;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x000BEE9A File Offset: 0x000BD09A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x000BEEA2 File Offset: 0x000BD0A2
		// (set) Token: 0x060046FD RID: 18173 RVA: 0x000BEEAA File Offset: 0x000BD0AA
		public TimeSpan TimeToLive
		{
			get
			{
				return this.timeToLive;
			}
			private set
			{
				this.timeToLive = value;
			}
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x000BEEB3 File Offset: 0x000BD0B3
		public override string ToString()
		{
			return string.Format("Name={0};TTL={1};IPs={2}", this.name, this.timeToLive, (this.addresses == null) ? string.Empty : string.Join<IPAddress>(",", this.addresses));
		}

		// Token: 0x04003C23 RID: 15395
		private List<IPAddress> addresses;

		// Token: 0x04003C24 RID: 15396
		private string name;

		// Token: 0x04003C25 RID: 15397
		private TimeSpan timeToLive;
	}
}
