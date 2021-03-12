using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000254 RID: 596
	[ImmutableObject(true)]
	public abstract class ProtocolAddress : IComparable, IComparable<ProtocolAddress>
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x0003F965 File Offset: 0x0003DB65
		protected ProtocolAddress(Protocol protocol, string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				throw new ArgumentNullException("address");
			}
			this.protocol = protocol;
			this.address = address;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0003F98E File Offset: 0x0003DB8E
		public Protocol ProtocolType
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003F996 File Offset: 0x0003DB96
		public string AddressString
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0003F99E File Offset: 0x0003DB9E
		public sealed override string ToString()
		{
			return string.Format("{0}{1}{2}", this.protocol.ProtocolName, ':', this.address);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0003F9C2 File Offset: 0x0003DBC2
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0003F9CF File Offset: 0x0003DBCF
		public override bool Equals(object obj)
		{
			return this == obj as ProtocolAddress;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0003F9DD File Offset: 0x0003DBDD
		public static bool operator ==(ProtocolAddress a, ProtocolAddress b)
		{
			return a == b || (a != null && b != null && a.protocol.Equals(b.protocol) && a.address.Equals(b.address, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0003FA14 File Offset: 0x0003DC14
		public static bool operator !=(ProtocolAddress a, ProtocolAddress b)
		{
			return !(a == b);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0003FA20 File Offset: 0x0003DC20
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ProtocolAddress))
			{
				throw new ArgumentException(DataStrings.InvalidTypeArgumentException("other", other.GetType(), typeof(ProtocolAddress)), "other");
			}
			return this.CompareTo((ProtocolAddress)other);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0003FA70 File Offset: 0x0003DC70
		public int CompareTo(ProtocolAddress other)
		{
			if (other == null)
			{
				return 1;
			}
			return StringComparer.OrdinalIgnoreCase.Compare(this.ToString(), other.ToString());
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0003FA93 File Offset: 0x0003DC93
		public bool Equals(ProtocolAddress other)
		{
			return this == other;
		}

		// Token: 0x04000BE5 RID: 3045
		public const char Separator = ':';

		// Token: 0x04000BE6 RID: 3046
		private Protocol protocol;

		// Token: 0x04000BE7 RID: 3047
		private string address;
	}
}
