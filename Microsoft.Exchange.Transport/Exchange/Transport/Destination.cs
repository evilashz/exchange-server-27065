using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000154 RID: 340
	internal class Destination : IEquatable<Destination>
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00039A15 File Offset: 0x00037C15
		public Destination.DestinationType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00039A1D File Offset: 0x00037C1D
		public byte[] Blob
		{
			get
			{
				return this.blob;
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00039A25 File Offset: 0x00037C25
		public Destination(Destination.DestinationType type, byte[] data)
		{
			this.blob = data;
			this.type = type;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00039A3F File Offset: 0x00037C3F
		public Destination(Destination.DestinationType type, Guid data) : this(type, data.ToByteArray())
		{
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00039A4F File Offset: 0x00037C4F
		public Destination(Destination.DestinationType type, string data) : this(type, Encoding.Unicode.GetBytes(data))
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00039A64 File Offset: 0x00037C64
		public bool Equals(Destination other)
		{
			if (other == null || this.type != other.type)
			{
				return false;
			}
			if (this.blob.Length != other.blob.Length)
			{
				return false;
			}
			for (int i = 0; i < this.blob.Length; i++)
			{
				if (this.blob[i] != other.blob[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00039AC0 File Offset: 0x00037CC0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Destination);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00039AD0 File Offset: 0x00037CD0
		public override string ToString()
		{
			if (this.type == Destination.DestinationType.Mdb)
			{
				return this.ToGuid().ToString();
			}
			return Encoding.Unicode.GetString(this.blob);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00039B0B File Offset: 0x00037D0B
		public Guid ToGuid()
		{
			return new Guid(this.blob);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00039B18 File Offset: 0x00037D18
		public override int GetHashCode()
		{
			uint num = 0U;
			for (int i = 0; i < this.blob.Length; i++)
			{
				num = (num << 1 | num >> 31);
				num += (uint)this.blob[i];
			}
			return (int)num;
		}

		// Token: 0x04000750 RID: 1872
		internal static readonly Dictionary<int, Destination.DestinationType> DestinationTypeDictionary = new Dictionary<int, Destination.DestinationType>();

		// Token: 0x04000751 RID: 1873
		private readonly byte[] blob;

		// Token: 0x04000752 RID: 1874
		private readonly Destination.DestinationType type;

		// Token: 0x02000155 RID: 341
		internal enum DestinationType : byte
		{
			// Token: 0x04000754 RID: 1876
			Mdb = 1,
			// Token: 0x04000755 RID: 1877
			Shadow,
			// Token: 0x04000756 RID: 1878
			ExternalFqdn,
			// Token: 0x04000757 RID: 1879
			Conditional
		}
	}
}
