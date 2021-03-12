using System;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000004 RID: 4
	internal sealed class IPNetwork
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021AD File Offset: 0x000003AD
		private IPNetwork()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021B5 File Offset: 0x000003B5
		public static IPNetwork Create(IPAddress network, int cidrLength)
		{
			return IPNetwork.InternalCreate(network, cidrLength);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021C0 File Offset: 0x000003C0
		public static IPNetwork Create(IPAddress network, int ip4CidrLength, int ip6CidrLength)
		{
			AddressFamily addressFamily = network.AddressFamily;
			if (addressFamily == AddressFamily.InterNetwork)
			{
				return IPNetwork.InternalCreate(network, ip4CidrLength);
			}
			if (addressFamily != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentOutOfRangeException("network", network, "Invalid address family");
			}
			return IPNetwork.InternalCreate(network, ip6CidrLength);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002200 File Offset: 0x00000400
		public static IPNetwork Create(IPAddress address)
		{
			return new IPNetwork
			{
				network = address,
				networkBytes = null,
				cidrLength = -1
			};
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000222C File Offset: 0x0000042C
		private static IPNetwork InternalCreate(IPAddress network, int cidrLength)
		{
			if (cidrLength < 0)
			{
				return null;
			}
			IPNetwork ipnetwork = new IPNetwork();
			ipnetwork.network = network;
			ipnetwork.networkBytes = IPNetwork.GetMaskedAddress(network, cidrLength);
			int num = ipnetwork.networkBytes.Length * 8;
			if (cidrLength > num)
			{
				return null;
			}
			if (num > cidrLength)
			{
				ipnetwork.cidrLength = cidrLength;
				ipnetwork.bytesToConsider = ipnetwork.cidrLength / 8;
				if (ipnetwork.cidrLength % 8 != 0)
				{
					ipnetwork.bytesToConsider++;
				}
			}
			else
			{
				ipnetwork.cidrLength = -1;
			}
			return ipnetwork;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A8 File Offset: 0x000004A8
		public bool Contains(IPAddress address)
		{
			if (this.network.AddressFamily != address.AddressFamily)
			{
				return false;
			}
			if (this.cidrLength == -1)
			{
				return this.network.Equals(address);
			}
			byte[] maskedAddress = IPNetwork.GetMaskedAddress(address, this.cidrLength);
			for (int i = 0; i < this.bytesToConsider; i++)
			{
				if (this.networkBytes[i] != maskedAddress[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002310 File Offset: 0x00000510
		private static byte[] GetMaskedAddress(IPAddress address, int cidrLength)
		{
			byte[] addressBytes = address.GetAddressBytes();
			byte[] array = new byte[addressBytes.Length];
			int i;
			for (i = 0; i < cidrLength / 8; i++)
			{
				array[i] = addressBytes[i];
			}
			int num = cidrLength % 8;
			if (num != 0)
			{
				int num2 = 8 - num;
				byte b = (byte)(255 << num2);
				array[i] = (addressBytes[i] & b);
			}
			return array;
		}

		// Token: 0x04000004 RID: 4
		private const int FullCIDRLength = -1;

		// Token: 0x04000005 RID: 5
		private IPAddress network;

		// Token: 0x04000006 RID: 6
		private byte[] networkBytes;

		// Token: 0x04000007 RID: 7
		private int cidrLength;

		// Token: 0x04000008 RID: 8
		private int bytesToConsider;
	}
}
