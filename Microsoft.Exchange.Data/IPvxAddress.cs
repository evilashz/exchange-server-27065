using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000163 RID: 355
	[CLSCompliant(true)]
	[Serializable]
	public struct IPvxAddress : IEquatable<IPvxAddress>, IComparable<IPvxAddress>
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0002455C File Offset: 0x0002275C
		[CLSCompliant(false)]
		public IPvxAddress(IPvxAddress other)
		{
			this.highBytes = other.highBytes;
			this.lowBytes = other.lowBytes;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00024578 File Offset: 0x00022778
		[CLSCompliant(false)]
		public IPvxAddress(ulong high, ulong low)
		{
			this.highBytes = high;
			this.lowBytes = low;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00024588 File Offset: 0x00022788
		public IPvxAddress(IPAddress address)
		{
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				byte[] addressBytes = address.GetAddressBytes();
				int network = BitConverter.ToInt32(addressBytes, 0);
				this.lowBytes = (ulong)((long)IPAddress.NetworkToHostOrder(network) & (long)((ulong)-1));
				this.highBytes = 0UL;
				this.lowBytes |= 281470681743360UL;
				return;
			}
			if (address.AddressFamily != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(string.Format("Unsupported address family {0}", (address == null) ? string.Empty : address.AddressFamily.ToString()), "address");
			}
			byte[] addressBytes2 = address.GetAddressBytes();
			long network2 = BitConverter.ToInt64(addressBytes2, 0);
			this.highBytes = (ulong)IPAddress.NetworkToHostOrder(network2);
			network2 = BitConverter.ToInt64(addressBytes2, 8);
			this.lowBytes = (ulong)IPAddress.NetworkToHostOrder(network2);
			if (this.IsIPv4Compatible)
			{
				this.lowBytes |= 281470681743360UL;
				return;
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00024664 File Offset: 0x00022864
		public IPvxAddress(byte[] address)
		{
			if (address.Length == 4)
			{
				int network = BitConverter.ToInt32(address, 0);
				this.lowBytes = (ulong)((long)IPAddress.NetworkToHostOrder(network));
				this.highBytes = 0UL;
				this.lowBytes |= 281470681743360UL;
				return;
			}
			if (address.Length != 16)
			{
				throw new ArgumentException("Bad IP address", "address");
			}
			long network2 = BitConverter.ToInt64(address, 0);
			this.highBytes = (ulong)IPAddress.NetworkToHostOrder(network2);
			network2 = BitConverter.ToInt64(address, 8);
			this.lowBytes = (ulong)IPAddress.NetworkToHostOrder(network2);
			if (this.IsIPv4Compatible)
			{
				this.lowBytes |= 281470681743360UL;
				return;
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002470C File Offset: 0x0002290C
		public IPvxAddress(long newAddress)
		{
			if (newAddress < 0L || newAddress > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("newAddress", newAddress, "Unexpected IPv4 address.");
			}
			this.lowBytes = (ulong)IPAddress.NetworkToHostOrder((int)newAddress);
			this.lowBytes |= 281470681743360UL;
			this.highBytes = 0UL;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00024768 File Offset: 0x00022968
		public static implicit operator IPAddress(IPvxAddress address)
		{
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				long newAddress = (long)((ulong)IPAddress.HostToNetworkOrder((int)address.lowBytes));
				return new IPAddress(newAddress);
			}
			return new IPAddress(address.GetBytes(), 0L);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000247A3 File Offset: 0x000229A3
		public static explicit operator byte(IPvxAddress address)
		{
			return (byte)address.lowBytes;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000247AD File Offset: 0x000229AD
		[CLSCompliant(false)]
		public static explicit operator ushort(IPvxAddress address)
		{
			return (ushort)address.lowBytes;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000247B7 File Offset: 0x000229B7
		[CLSCompliant(false)]
		public static explicit operator uint(IPvxAddress address)
		{
			return (uint)address.lowBytes;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000247C1 File Offset: 0x000229C1
		[CLSCompliant(false)]
		public static explicit operator ulong(IPvxAddress address)
		{
			return address.lowBytes;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000247CA File Offset: 0x000229CA
		public bool IsIPv4Mapped
		{
			get
			{
				return (this.lowBytes & 18446744069414584320UL) == 281470681743360UL && this.highBytes == 0UL;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x000247F3 File Offset: 0x000229F3
		public bool IsIPv4Compatible
		{
			get
			{
				return this.highBytes == 0UL && this.lowBytes != 1UL && (this.lowBytes & 18446744069414584320UL) == 0UL && (this.lowBytes & (ulong)-65536) != 0UL;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00024831 File Offset: 0x00022A31
		public bool IsIPv6SiteLocal
		{
			get
			{
				return (this.highBytes & 18428729675200069632UL) == 18356672081162141696UL;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002484E File Offset: 0x00022A4E
		public bool IsIPv6LinkLocal
		{
			get
			{
				return (this.highBytes & 18428729675200069632UL) == 18410715276690587648UL;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0002486B File Offset: 0x00022A6B
		public AddressFamily AddressFamily
		{
			get
			{
				if (!this.IsIPv4Mapped && !this.IsIPv4Compatible)
				{
					return AddressFamily.InterNetworkV6;
				}
				return AddressFamily.InterNetwork;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00024881 File Offset: 0x00022A81
		public bool IsBroadcast
		{
			get
			{
				return this.AddressFamily != AddressFamily.InterNetworkV6 && this.Equals(IPvxAddress.IPv4Broadcast);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002489A File Offset: 0x00022A9A
		public bool IsMulticast
		{
			get
			{
				if (this.AddressFamily == AddressFamily.InterNetworkV6)
				{
					return (this.highBytes & 18374686479671623680UL) == 18374686479671623680UL;
				}
				return (this.lowBytes & (ulong)-268435456) == (ulong)-536870912;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x000248D7 File Offset: 0x00022AD7
		internal ulong LowBytes
		{
			get
			{
				return this.lowBytes;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000248DF File Offset: 0x00022ADF
		internal ulong HighBytes
		{
			get
			{
				return this.highBytes;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000248E8 File Offset: 0x00022AE8
		public static bool TryParse(string ipString, out IPvxAddress address)
		{
			IPAddress address2;
			if (!IPAddress.TryParse(ipString, out address2))
			{
				address = IPvxAddress.Zero;
				return false;
			}
			address = new IPvxAddress(address2);
			return true;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002491C File Offset: 0x00022B1C
		public static IPvxAddress Parse(string ipString)
		{
			IPAddress address = IPAddress.Parse(ipString);
			return new IPvxAddress(address);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00024936 File Offset: 0x00022B36
		public static bool operator ==(IPvxAddress v1, IPvxAddress comparand)
		{
			return v1.Equals(comparand);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00024940 File Offset: 0x00022B40
		public static bool operator !=(IPvxAddress v1, IPvxAddress comparand)
		{
			return !v1.Equals(comparand);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002494D File Offset: 0x00022B4D
		public static bool operator <(IPvxAddress v1, IPvxAddress comparand)
		{
			return IPvxAddress.Compare(v1, comparand) == -1;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00024959 File Offset: 0x00022B59
		public static bool operator <=(IPvxAddress v1, IPvxAddress comparand)
		{
			return IPvxAddress.Compare(v1, comparand) != 1;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00024968 File Offset: 0x00022B68
		public static bool operator >(IPvxAddress v1, IPvxAddress comparand)
		{
			return IPvxAddress.Compare(v1, comparand) == 1;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00024974 File Offset: 0x00022B74
		public static bool operator >=(IPvxAddress v1, IPvxAddress comparand)
		{
			return IPvxAddress.Compare(v1, comparand) != -1;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00024984 File Offset: 0x00022B84
		[CLSCompliant(false)]
		public static IPvxAddress operator +(IPvxAddress v1, ulong operand)
		{
			ulong num = v1.highBytes;
			ulong num2 = v1.lowBytes + operand;
			if (num2 < v1.lowBytes)
			{
				if (num == 18446744073709551615UL)
				{
					throw new OverflowException();
				}
				num += 1UL;
			}
			return new IPvxAddress(num, num2);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000249C4 File Offset: 0x00022BC4
		[CLSCompliant(false)]
		public static IPvxAddress operator -(IPvxAddress v1, IPvxAddress v2)
		{
			ulong num = v1.highBytes - v2.highBytes;
			ulong num2 = v1.lowBytes - v2.lowBytes;
			if (num > v1.highBytes)
			{
				throw new OverflowException();
			}
			if (num2 > v1.lowBytes)
			{
				if (num == 0UL)
				{
					throw new OverflowException();
				}
				num -= 1UL;
			}
			return new IPvxAddress(num, num2);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00024A22 File Offset: 0x00022C22
		public static IPvxAddress operator ^(IPvxAddress v1, IPvxAddress mask)
		{
			return v1.Xor(mask);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00024A2C File Offset: 0x00022C2C
		[CLSCompliant(false)]
		public static IPvxAddress operator ^(IPvxAddress v1, ulong mask)
		{
			return v1.Xor((long)mask);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00024A36 File Offset: 0x00022C36
		public static IPvxAddress operator ^(IPvxAddress v1, long mask)
		{
			return v1.Xor(mask);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00024A40 File Offset: 0x00022C40
		public static IPvxAddress operator &(IPvxAddress v1, IPvxAddress mask)
		{
			return v1.BitwiseAnd(mask);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00024A4A File Offset: 0x00022C4A
		[CLSCompliant(false)]
		public static IPvxAddress operator &(IPvxAddress v1, ulong mask)
		{
			return v1.BitwiseAnd((long)mask);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00024A54 File Offset: 0x00022C54
		public static IPvxAddress operator &(IPvxAddress v1, long mask)
		{
			return v1.BitwiseAnd(mask);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00024A5E File Offset: 0x00022C5E
		public static IPvxAddress operator |(IPvxAddress v1, IPvxAddress mask)
		{
			return v1.BitwiseOr(mask);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00024A68 File Offset: 0x00022C68
		[CLSCompliant(false)]
		public static IPvxAddress operator |(IPvxAddress v1, ulong mask)
		{
			return v1.BitwiseOr((long)mask);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00024A72 File Offset: 0x00022C72
		public static IPvxAddress operator |(IPvxAddress v1, long mask)
		{
			return v1.BitwiseOr(mask);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00024A7C File Offset: 0x00022C7C
		public static IPvxAddress operator ~(IPvxAddress v1)
		{
			return v1.OnesComplement();
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00024A85 File Offset: 0x00022C85
		public static IPvxAddress operator <<(IPvxAddress v1, int shift)
		{
			return v1.LeftShift(shift);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00024A8F File Offset: 0x00022C8F
		public static IPvxAddress operator >>(IPvxAddress v1, int shift)
		{
			return v1.RightShift(shift);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00024A99 File Offset: 0x00022C99
		public IPvxAddress Xor(IPvxAddress mask)
		{
			return new IPvxAddress(this.highBytes ^ mask.highBytes, this.lowBytes ^ mask.lowBytes);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00024ABC File Offset: 0x00022CBC
		public IPvxAddress Xor(long mask)
		{
			return new IPvxAddress(this.highBytes, this.lowBytes ^ (ulong)mask);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00024AD1 File Offset: 0x00022CD1
		public IPvxAddress BitwiseAnd(IPvxAddress mask)
		{
			return new IPvxAddress(this.highBytes & mask.highBytes, this.lowBytes & mask.lowBytes);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00024AF4 File Offset: 0x00022CF4
		public IPvxAddress BitwiseAnd(long mask)
		{
			return new IPvxAddress(0UL, this.lowBytes & (ulong)mask);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00024B05 File Offset: 0x00022D05
		public IPvxAddress BitwiseOr(IPvxAddress mask)
		{
			return new IPvxAddress(this.highBytes | mask.highBytes, this.lowBytes | mask.lowBytes);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00024B28 File Offset: 0x00022D28
		public IPvxAddress BitwiseOr(long mask)
		{
			return new IPvxAddress(this.highBytes, this.lowBytes | (ulong)mask);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00024B3D File Offset: 0x00022D3D
		public IPvxAddress OnesComplement()
		{
			return new IPvxAddress(~this.highBytes, ~this.lowBytes);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00024B54 File Offset: 0x00022D54
		public IPvxAddress LeftShift(int shift)
		{
			if (shift == 0)
			{
				return this;
			}
			if (shift >= 128)
			{
				return IPvxAddress.Zero;
			}
			if (shift == 64)
			{
				return new IPvxAddress(this.lowBytes, 0UL);
			}
			if (shift > 64)
			{
				return new IPvxAddress(this.lowBytes << shift - 64, 0UL);
			}
			ulong num = this.lowBytes >> 64 - shift;
			return new IPvxAddress(this.highBytes << shift | num, this.lowBytes << shift);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00024BD4 File Offset: 0x00022DD4
		public IPvxAddress RightShift(int shift)
		{
			if (shift == 0)
			{
				return this;
			}
			if (shift >= 128)
			{
				return IPvxAddress.Zero;
			}
			if (shift == 64)
			{
				return new IPvxAddress(0UL, this.highBytes);
			}
			if (shift > 64)
			{
				return new IPvxAddress(0UL, this.highBytes >> shift - 64);
			}
			ulong num = this.highBytes << 64 - shift;
			return new IPvxAddress(this.highBytes >> shift, num | this.lowBytes >> shift);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00024C54 File Offset: 0x00022E54
		public byte[] GetBytes()
		{
			byte[] array = new byte[16];
			long value = IPAddress.HostToNetworkOrder((long)this.highBytes);
			ExBitConverter.Write(value, array, 0);
			value = IPAddress.HostToNetworkOrder((long)this.lowBytes);
			ExBitConverter.Write(value, array, 8);
			return array;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00024C94 File Offset: 0x00022E94
		public override bool Equals(object comparand)
		{
			if (comparand is IPvxAddress)
			{
				return this.Equals((IPvxAddress)comparand);
			}
			IPAddress ipaddress = comparand as IPAddress;
			return ipaddress != null && this.Equals(ipaddress);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00024CC9 File Offset: 0x00022EC9
		public bool Equals(IPAddress comparand)
		{
			return this.Equals(new IPvxAddress(comparand));
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00024CD7 File Offset: 0x00022ED7
		public bool Equals(IPvxAddress comparand)
		{
			return this.highBytes == comparand.highBytes && this.lowBytes == comparand.lowBytes;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00024CF9 File Offset: 0x00022EF9
		public static bool Equals(IPvxAddress v1, IPvxAddress v2)
		{
			return v1.Equals(v2);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00024D03 File Offset: 0x00022F03
		public static int Compare(IPvxAddress v1, IPvxAddress v2)
		{
			return v1.CompareTo(v2);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00024D10 File Offset: 0x00022F10
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is IPvxAddress)
			{
				return this.CompareTo((IPvxAddress)value);
			}
			IPAddress ipaddress = value as IPAddress;
			if (ipaddress != null)
			{
				return this.CompareTo(ipaddress);
			}
			throw new ArgumentException("Argument must be IP address");
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00024D53 File Offset: 0x00022F53
		public int CompareTo(IPAddress value)
		{
			return this.CompareTo(new IPvxAddress(value));
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00024D64 File Offset: 0x00022F64
		public int CompareTo(IPvxAddress value)
		{
			if (this.highBytes > value.highBytes)
			{
				return 1;
			}
			if (this.highBytes < value.highBytes)
			{
				return -1;
			}
			if (this.lowBytes > value.lowBytes)
			{
				return 1;
			}
			if (this.lowBytes < value.lowBytes)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00024DB8 File Offset: 0x00022FB8
		public override int GetHashCode()
		{
			return (int)(this.highBytes & (ulong)-1) ^ (int)(this.highBytes >> 16 & (ulong)-1) ^ (int)(this.lowBytes & (ulong)-1) ^ (int)(this.lowBytes >> 16 & (ulong)-1);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00024DF8 File Offset: 0x00022FF8
		public override string ToString()
		{
			IPAddress ipaddress = this;
			return ipaddress.ToString();
		}

		// Token: 0x04000716 RID: 1814
		private const ulong IPv4MappedMaskLow = 281470681743360UL;

		// Token: 0x04000717 RID: 1815
		public static readonly IPvxAddress IPv4Loopback = new IPvxAddress(0UL, 281472812449793UL);

		// Token: 0x04000718 RID: 1816
		public static readonly IPvxAddress IPv6Loopback = new IPvxAddress(0UL, 1UL);

		// Token: 0x04000719 RID: 1817
		public static readonly IPvxAddress None = new IPvxAddress(0UL, 0UL);

		// Token: 0x0400071A RID: 1818
		public static readonly IPvxAddress Zero = IPvxAddress.None;

		// Token: 0x0400071B RID: 1819
		internal static readonly IPvxAddress IPv4MappedMask = new IPvxAddress(0UL, 281470681743360UL);

		// Token: 0x0400071C RID: 1820
		public static readonly IPvxAddress IPv4Broadcast = new IPvxAddress(0UL, 281474976710655UL);

		// Token: 0x0400071D RID: 1821
		private ulong highBytes;

		// Token: 0x0400071E RID: 1822
		private ulong lowBytes;
	}
}
