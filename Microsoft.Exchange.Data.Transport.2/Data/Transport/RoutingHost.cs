using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class RoutingHost : INextHopServer
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000811C File Offset: 0x0000631C
		public RoutingHost(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				throw new ArgumentException("A null or empty routing host name was specified");
			}
			address = address.Trim();
			int length = address.Length;
			if (address[0] == '[' && address[length - 1] == ']')
			{
				address = address.Substring(1, length - 2);
			}
			if (IPAddress.TryParse(address, out this.ipAddress))
			{
				if (this.ipAddress.Equals(IPAddress.Any) || this.ipAddress.Equals(IPAddress.IPv6Any))
				{
					throw new ArgumentException(string.Format("The specified IP address '{0}' isn't valid as a routing host", this.ipAddress));
				}
				return;
			}
			else
			{
				this.ipAddress = IPAddress.Any;
				if (!RoutingHostName.TryParse(address, out this.domain))
				{
					throw new ArgumentException(string.Format("The specified routing host name '{0}' isn't valid", address));
				}
				return;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000081F3 File Offset: 0x000063F3
		private RoutingHost()
		{
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00008206 File Offset: 0x00006406
		public IPAddress Address
		{
			get
			{
				return this.ipAddress;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000820E File Offset: 0x0000640E
		public string HostName
		{
			get
			{
				return this.domain.ToString();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00008221 File Offset: 0x00006421
		public bool IsIPAddress
		{
			get
			{
				return !this.ipAddress.Equals(IPAddress.Any);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008236 File Offset: 0x00006436
		internal RoutingHostName Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000823E File Offset: 0x0000643E
		string INextHopServer.Fqdn
		{
			get
			{
				return this.HostName;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00008246 File Offset: 0x00006446
		bool INextHopServer.IsFrontendAndHubColocatedServer
		{
			get
			{
				return this.isFrontendAndHubColocatedServer;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000824E File Offset: 0x0000644E
		public static RoutingHost Parse(string address)
		{
			return new RoutingHost(address);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00008258 File Offset: 0x00006458
		public static bool TryParse(string address, out RoutingHost routinghost)
		{
			routinghost = null;
			if (string.IsNullOrEmpty(address))
			{
				return false;
			}
			address = address.Trim();
			RoutingHost routingHost = new RoutingHost();
			int length = address.Length;
			if (address[0] == '[' && address[length - 1] == ']')
			{
				address = address.Substring(1, length - 2);
			}
			if (IPAddress.TryParse(address, out routingHost.ipAddress))
			{
				if (routingHost.ipAddress.Equals(IPAddress.Any))
				{
					return false;
				}
			}
			else
			{
				if (!RoutingHostName.TryParse(address, out routingHost.domain))
				{
					return false;
				}
				routingHost.ipAddress = IPAddress.Any;
			}
			routinghost = routingHost;
			return true;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000082F0 File Offset: 0x000064F0
		public bool Equals(RoutingHost value)
		{
			if (object.ReferenceEquals(this, value))
			{
				return true;
			}
			if (value == null)
			{
				return false;
			}
			if (this.IsIPAddress != value.IsIPAddress)
			{
				return false;
			}
			if (this.IsIPAddress)
			{
				return this.ipAddress.Equals(value.ipAddress);
			}
			return this.domain.Equals(value.domain);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008348 File Offset: 0x00006548
		public override bool Equals(object comparand)
		{
			RoutingHost routingHost = comparand as RoutingHost;
			return routingHost != null && this.Equals(routingHost);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008368 File Offset: 0x00006568
		public override int GetHashCode()
		{
			if (this.IsIPAddress)
			{
				return this.ipAddress.GetHashCode();
			}
			return this.domain.GetHashCode();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000838F File Offset: 0x0000658F
		public override string ToString()
		{
			if (this.IsIPAddress)
			{
				return this.IpAddressToString();
			}
			return this.domain.ToString();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000083B4 File Offset: 0x000065B4
		internal static string ConvertRoutingHostsToString<T>(IList<T> routingHostWrappers, Func<T, RoutingHost> routingHostGetter)
		{
			if (routingHostWrappers == null || routingHostWrappers.Count == 0 || routingHostGetter == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (T arg in routingHostWrappers)
			{
				RoutingHost routingHost = routingHostGetter(arg);
				num++;
				stringBuilder.Append(routingHost.ToString());
				if (num < routingHostWrappers.Count)
				{
					stringBuilder.Append(',');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008448 File Offset: 0x00006648
		internal static List<T> GetRoutingHostsFromString<T>(string routingHostsString, Func<RoutingHost, T> routingHostWrapperGetter)
		{
			if (routingHostWrapperGetter == null)
			{
				throw new ArgumentNullException("routingHostWrapperGetter");
			}
			List<T> list = new List<T>();
			int num;
			for (int i = 0; i < routingHostsString.Length; i = num + 1)
			{
				num = routingHostsString.IndexOfAny(RoutingHost.routingHostDelimiters, i);
				if (-1 == num)
				{
					num = routingHostsString.Length;
				}
				RoutingHost routingHost = RoutingHost.InternalParseRoutingHost(routingHostsString, i, num - i);
				if (routingHost != null)
				{
					T item = routingHostWrapperGetter(routingHost);
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000084BC File Offset: 0x000066BC
		internal void SetIsColocatedFrontendAndHub(bool value)
		{
			this.isFrontendAndHubColocatedServer = value;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000084C8 File Offset: 0x000066C8
		private static RoutingHost InternalParseRoutingHost(string routingHostString, int startPos, int count)
		{
			string text = routingHostString.Substring(startPos, count);
			text = text.Trim();
			if (text.Length == 0)
			{
				return null;
			}
			RoutingHost result;
			try
			{
				if ('[' != text[0])
				{
					result = RoutingHost.Parse(text);
				}
				else if (']' != text[count - 1])
				{
					result = null;
				}
				else
				{
					result = RoutingHost.Parse(text.Substring(1, count - 2));
				}
			}
			catch (ArgumentException)
			{
				result = null;
			}
			catch (FormatException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008550 File Offset: 0x00006750
		private string IpAddressToString()
		{
			return string.Format("[{0}]", this.ipAddress);
		}

		// Token: 0x040001FC RID: 508
		private static readonly char[] routingHostDelimiters = new char[]
		{
			';',
			','
		};

		// Token: 0x040001FD RID: 509
		private RoutingHostName domain = RoutingHostName.Empty;

		// Token: 0x040001FE RID: 510
		private IPAddress ipAddress;

		// Token: 0x040001FF RID: 511
		private bool isFrontendAndHubColocatedServer;
	}
}
