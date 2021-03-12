using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	public class Hostname
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x000232FA File Offset: 0x000214FA
		public Hostname(string address) : this(new RoutingHostName(address))
		{
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00023308 File Offset: 0x00021508
		internal Hostname(RoutingHostName routingHostName)
		{
			if (routingHostName.Equals(RoutingHostName.Empty))
			{
				throw new ArgumentException("routingHostName");
			}
			this.routingHostName = routingHostName;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00023330 File Offset: 0x00021530
		public string HostnameString
		{
			get
			{
				return this.routingHostName.HostnameString;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002334B File Offset: 0x0002154B
		public static Hostname Parse(string address)
		{
			return new Hostname(address);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00023354 File Offset: 0x00021554
		public static bool TryParse(string address, out Hostname hostname)
		{
			hostname = null;
			RoutingHostName routingHostName;
			if (RoutingHostName.TryParse(address, out routingHostName))
			{
				hostname = new Hostname(routingHostName);
				return true;
			}
			return false;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002337C File Offset: 0x0002157C
		public override string ToString()
		{
			return this.routingHostName.ToString();
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000233A0 File Offset: 0x000215A0
		public bool Equals(Hostname rhs)
		{
			return rhs != null && this.routingHostName.Equals(rhs.routingHostName);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000233C8 File Offset: 0x000215C8
		public override bool Equals(object comparand)
		{
			Hostname hostname = comparand as Hostname;
			return hostname != null && this.Equals(hostname);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000233E8 File Offset: 0x000215E8
		public override int GetHashCode()
		{
			return this.routingHostName.GetHashCode();
		}

		// Token: 0x04000704 RID: 1796
		private readonly RoutingHostName routingHostName;
	}
}
