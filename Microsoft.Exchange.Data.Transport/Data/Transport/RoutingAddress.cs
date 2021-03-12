using System;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	public struct RoutingAddress : IEquatable<RoutingAddress>, IComparable<RoutingAddress>
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00007226 File Offset: 0x00005426
		public RoutingAddress(string address)
		{
			this.address = (string.IsNullOrEmpty(address) ? null : address);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000723A File Offset: 0x0000543A
		internal RoutingAddress(byte[] address)
		{
			this.address = ((address != null && address.Length != 0) ? ByteString.BytesToString(address, true) : null);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007254 File Offset: 0x00005454
		public RoutingAddress(string local, string domain)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			if (domain != null)
			{
				this.address = local + "@" + domain;
				return;
			}
			if (local != RoutingAddress.NullReversePath.address)
			{
				throw new ArgumentNullException("domain");
			}
			this = RoutingAddress.NullReversePath;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000072AD File Offset: 0x000054AD
		public int Length
		{
			get
			{
				if (this.address != null)
				{
					return this.address.Length;
				}
				return 0;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000072C4 File Offset: 0x000054C4
		public string LocalPart
		{
			get
			{
				if (this.address == null)
				{
					return null;
				}
				int num;
				if (MimeAddressParser.IsValidSmtpAddress(this.address, true, out num, RoutingAddress.IsEaiEnabled()))
				{
					return this.address.Substring(0, num - 1);
				}
				if (this.address == RoutingAddress.NullReversePath.address)
				{
					return this.address;
				}
				return null;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00007320 File Offset: 0x00005520
		public string DomainPart
		{
			get
			{
				if (this.address == null)
				{
					return null;
				}
				int startIndex;
				if (!MimeAddressParser.IsValidSmtpAddress(this.address, true, out startIndex, RoutingAddress.IsEaiEnabled()))
				{
					return null;
				}
				return this.address.Substring(startIndex);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000735A File Offset: 0x0000555A
		public bool IsValid
		{
			get
			{
				return this.address != null && RoutingAddress.IsValidAddress(this.address);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00007371 File Offset: 0x00005571
		public bool IsUTF8
		{
			get
			{
				return RoutingAddress.IsUTF8Address(this.address);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007380 File Offset: 0x00005580
		public static bool IsValidAddress(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			int domainStart;
			return (MimeAddressParser.IsValidSmtpAddress(address, true, out domainStart, RoutingAddress.IsEaiEnabled()) && !RoutingAddress.IsValidDomainIPLiteral(address, domainStart)) || address == RoutingAddress.NullReversePath.address;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000073C6 File Offset: 0x000055C6
		public static bool IsUTF8Address(string address)
		{
			return !MimeString.IsPureASCII(address);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000073D1 File Offset: 0x000055D1
		internal static bool IsValidDomain(string domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return MimeAddressParser.IsValidDomain(domain, 0, true, RoutingAddress.IsEaiEnabled()) && !RoutingAddress.IsValidDomainIPLiteral(domain);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000073FC File Offset: 0x000055FC
		public static RoutingAddress Parse(string address)
		{
			RoutingAddress routingAddress = new RoutingAddress(address);
			if (!routingAddress.IsValid)
			{
				throw new FormatException(string.Format("The specified address is an invalid SMTP address - {0}", routingAddress));
			}
			return routingAddress;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007431 File Offset: 0x00005631
		public static bool IsEmpty(RoutingAddress address)
		{
			return RoutingAddress.Empty == address;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000743E File Offset: 0x0000563E
		public static bool operator ==(RoutingAddress value1, RoutingAddress value2)
		{
			return value1.Equals(value2);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007448 File Offset: 0x00005648
		public static bool operator !=(RoutingAddress value1, RoutingAddress value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007454 File Offset: 0x00005654
		public static explicit operator string(RoutingAddress address)
		{
			return address.address ?? string.Empty;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00007466 File Offset: 0x00005666
		public static explicit operator RoutingAddress(string address)
		{
			return new RoutingAddress(address);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000746E File Offset: 0x0000566E
		internal static bool IsDomainIPLiteral(string domain)
		{
			return !string.IsNullOrEmpty(domain) && (domain[0] == '[' && domain[domain.Length - 1] == ']') && MimeAddressParser.IsValidDomain(domain, 0, true, false);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000074A2 File Offset: 0x000056A2
		public override int GetHashCode()
		{
			if (this.address == null)
			{
				return 0;
			}
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.address);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000074BE File Offset: 0x000056BE
		public int CompareTo(RoutingAddress address)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(this.address, address.address);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000074D7 File Offset: 0x000056D7
		public int CompareTo(object address)
		{
			if (!(address is RoutingAddress))
			{
				throw new ArgumentException("Argument is not a RoutingAddress", "address");
			}
			return this.CompareTo((RoutingAddress)address);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000074FD File Offset: 0x000056FD
		internal byte[] GetBytes()
		{
			if (this.address == null)
			{
				return null;
			}
			return ByteString.StringToBytes(this.address, RoutingAddress.IsEaiEnabled());
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000751C File Offset: 0x0000571C
		internal int GetBytes(byte[] array, int offset)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset", string.Format("offset value must be non-negative - {0}", offset));
			}
			if (this.address == null)
			{
				return 0;
			}
			int length = this.Length;
			if (length == 0)
			{
				return 0;
			}
			if (length > array.Length - offset)
			{
				throw new ArgumentException(string.Format("Insufficient array space to store the values - required {0}, actual {1}", length, array.Length - offset));
			}
			return ByteString.StringToBytes(this.address, 0, length, array, offset, RoutingAddress.IsEaiEnabled());
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000075A8 File Offset: 0x000057A8
		public override bool Equals(object obj)
		{
			if (!(obj is RoutingAddress))
			{
				throw new ArgumentException("Argument is not a RoutingAddress", "obj");
			}
			return this.Equals((RoutingAddress)obj);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000075CE File Offset: 0x000057CE
		public bool Equals(RoutingAddress obj)
		{
			return string.Equals(this.address, obj.address, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000075E3 File Offset: 0x000057E3
		public override string ToString()
		{
			return this.address ?? string.Empty;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000075F4 File Offset: 0x000057F4
		private static bool IsValidDomainIPLiteral(string domain)
		{
			return !string.IsNullOrEmpty(domain) && domain[0] == '[' && domain[domain.Length - 1] == ']';
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000761F File Offset: 0x0000581F
		private static bool IsValidDomainIPLiteral(string address, int domainStart)
		{
			return !string.IsNullOrEmpty(address) && domainStart >= 0 && domainStart < address.Length && address[domainStart] == '[' && address[address.Length - 1] == ']';
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00007658 File Offset: 0x00005858
		private static bool IsEaiEnabled()
		{
			bool result;
			try
			{
				result = InternalConfiguration.IsEaiEnabled();
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040001E2 RID: 482
		public static readonly RoutingAddress Empty = default(RoutingAddress);

		// Token: 0x040001E3 RID: 483
		public static readonly RoutingAddress NullReversePath = new RoutingAddress("<>");

		// Token: 0x040001E4 RID: 484
		internal static readonly RoutingAddress PostMasterAddress = new RoutingAddress("postmaster");

		// Token: 0x040001E5 RID: 485
		private readonly string address;
	}
}
