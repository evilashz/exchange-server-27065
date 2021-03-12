using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200008D RID: 141
	internal struct StoreId : IEquatable<StoreId>
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000CCCE File Offset: 0x0000AECE
		public StoreId(long nativeId)
		{
			this = new StoreId((ulong)nativeId);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000CCD7 File Offset: 0x0000AED7
		public StoreId(ulong nativeId)
		{
			this.nativeId = nativeId;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		internal StoreId(ReplId replId, byte[] globCnt)
		{
			if (globCnt == null || globCnt.Length != 6)
			{
				throw new ArgumentException("Should be exactly 6 bytes", "globCnt");
			}
			byte[] array = new byte[8];
			using (BufferWriter bufferWriter = new BufferWriter(array))
			{
				bufferWriter.WriteUInt16(replId.Value);
				bufferWriter.WriteBytes(globCnt);
			}
			this.nativeId = BitConverter.ToUInt64(array, 0);
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000CD54 File Offset: 0x0000AF54
		public ReplId ReplId
		{
			get
			{
				return new ReplId((ushort)(this.nativeId & 65535UL));
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public ulong GlobCount
		{
			get
			{
				return ((this.nativeId & 16711680UL) << 24) + ((this.nativeId & (ulong)-16777216) << 8) + ((this.nativeId & 1095216660480UL) >> 8) + ((this.nativeId & 280375465082880UL) >> 24) + ((this.nativeId & 71776119061217280UL) >> 40) + ((this.nativeId & 18374686479671623680UL) >> 56);
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public static implicit operator long(StoreId storeId)
		{
			return (long)storeId.nativeId;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		public static implicit operator ulong(StoreId storeId)
		{
			return storeId.nativeId;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000CDFA File Offset: 0x0000AFFA
		public override bool Equals(object obj)
		{
			return obj is StoreId && this.Equals((StoreId)obj);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000CE14 File Offset: 0x0000B014
		public override int GetHashCode()
		{
			return this.nativeId.GetHashCode();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000CE2F File Offset: 0x0000B02F
		public bool Equals(StoreId other)
		{
			return this.nativeId == other.nativeId;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CE40 File Offset: 0x0000B040
		public override string ToString()
		{
			return string.Format("{0:B}-{1:X}", this.ReplId, this.GlobCount);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CE64 File Offset: 0x0000B064
		internal static StoreId Parse(Reader reader)
		{
			ulong num = reader.ReadUInt64();
			return new StoreId(num);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CE7E File Offset: 0x0000B07E
		internal void Serialize(Writer writer)
		{
			writer.WriteUInt64(this.nativeId);
		}

		// Token: 0x040001FB RID: 507
		private readonly ulong nativeId;

		// Token: 0x040001FC RID: 508
		public static readonly StoreId Empty = new StoreId(0L);
	}
}
