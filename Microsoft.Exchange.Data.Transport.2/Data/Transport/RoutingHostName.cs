using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	internal struct RoutingHostName
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00008588 File Offset: 0x00006788
		public RoutingHostName(string address)
		{
			if (!RoutingHostName.IsValidName(address))
			{
				throw new ArgumentException(string.Format("The specified host name '{0}' isn't valid", address));
			}
			this.hostnamestring = address;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000085AA File Offset: 0x000067AA
		public string HostnameString
		{
			get
			{
				return this.hostnamestring;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000085B2 File Offset: 0x000067B2
		public static RoutingHostName Parse(string address)
		{
			return new RoutingHostName(address);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000085BA File Offset: 0x000067BA
		public static bool TryParse(string address, out RoutingHostName hostname)
		{
			hostname = RoutingHostName.Empty;
			if (string.IsNullOrEmpty(address))
			{
				return false;
			}
			if (!RoutingHostName.IsValidName(address))
			{
				return false;
			}
			hostname = new RoutingHostName(address);
			return true;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000085E8 File Offset: 0x000067E8
		internal static bool IsValidName(string name)
		{
			return RoutingHostName.ValidateName(DNSNameFormat.Domain, name) == DNSNameStatus.Valid;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000085F4 File Offset: 0x000067F4
		internal static DNSNameStatus ValidateName(DNSNameFormat format, string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return (DNSNameStatus)DnsNativeMethods2.ValidateName(name, (int)format);
			}
			return DNSNameStatus.InvalidName;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008608 File Offset: 0x00006808
		public override string ToString()
		{
			if (this.hostnamestring != null)
			{
				return this.hostnamestring;
			}
			return string.Empty;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000861E File Offset: 0x0000681E
		public bool Equals(RoutingHostName rhs)
		{
			if (this.hostnamestring != null)
			{
				return this.hostnamestring.Equals(rhs.hostnamestring, StringComparison.OrdinalIgnoreCase);
			}
			return rhs.hostnamestring == null;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00008648 File Offset: 0x00006848
		public override bool Equals(object comparand)
		{
			RoutingHostName? routingHostName = comparand as RoutingHostName?;
			return routingHostName != null && this.Equals(routingHostName.Value);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00008679 File Offset: 0x00006879
		public override int GetHashCode()
		{
			if (this.hostnamestring != null)
			{
				return this.hostnamestring.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04000200 RID: 512
		private readonly string hostnamestring;

		// Token: 0x04000201 RID: 513
		public static RoutingHostName Empty = default(RoutingHostName);
	}
}
