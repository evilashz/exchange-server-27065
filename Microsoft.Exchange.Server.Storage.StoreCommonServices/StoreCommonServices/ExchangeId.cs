using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200005E RID: 94
	public struct ExchangeId : IComparable<ExchangeId>, IEquatable<ExchangeId>
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00018260 File Offset: 0x00016460
		private ExchangeId(byte[] binaryValue)
		{
			this.binaryValue = binaryValue;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00018269 File Offset: 0x00016469
		public static ExchangeId Create(Guid guid, ulong itemNbr, ushort replid)
		{
			if (itemNbr != 0UL || !(guid == Guid.Empty))
			{
				return new ExchangeId(ExchangeIdHelpers.To26ByteArray(replid, guid, itemNbr));
			}
			return ExchangeId.zeroId;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00018290 File Offset: 0x00016490
		public static ExchangeId Create(Context context, IReplidGuidMap replidGuidMap, Guid guid, ulong itemNbr)
		{
			return ExchangeId.Create(guid, itemNbr, replidGuidMap.GetReplidFromGuid(context, guid));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000182A4 File Offset: 0x000164A4
		public static ExchangeId Create(Context context, IReplidGuidMap replidGuidMap, ushort replid, byte[] globCnt)
		{
			Guid guidFromReplid = replidGuidMap.GetGuidFromReplid(context, replid);
			ulong itemNbr = ExchangeIdHelpers.GlobcntFromByteArray(globCnt, 0U);
			return ExchangeId.Create(guidFromReplid, itemNbr, replid);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000182CA File Offset: 0x000164CA
		public static ExchangeId CreateFromInt64(Context context, IReplidGuidMap replidGuidMap, long legacyId)
		{
			return ExchangeId.CreateFromInt64(legacyId, replidGuidMap.GetGuidFromReplid(context, (ushort)(legacyId & 65535L)), (ushort)(legacyId & 65535L));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000182EC File Offset: 0x000164EC
		public static ExchangeId CreateFromInternalShortId(Context context, IReplidGuidMap replidGuidMap, ExchangeShortId shortId)
		{
			if (shortId.IsZero)
			{
				return ExchangeId.Zero;
			}
			ushort replid = shortId.Replid;
			return ExchangeId.Create(replidGuidMap.InternalGetGuidFromReplid(context, replid), shortId.Counter, replid);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00018328 File Offset: 0x00016528
		public static ExchangeId CreateFromInt64(long legacyId, Guid guid, ushort replid)
		{
			ulong itemNbr = (ulong)((legacyId & 16711680L) << 24 | (legacyId & (long)((ulong)-16777216)) << 8 | (long)((ulong)(legacyId & 1095216660480L) >> 8) | (long)((ulong)(legacyId & 280375465082880L) >> 24) | (long)((ulong)(legacyId & 71776119061217280L) >> 40) | (long)((ulong)(legacyId & -72057594037927936L) >> 56));
			return ExchangeId.Create(guid, itemNbr, replid);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001838F File Offset: 0x0001658F
		public static ExchangeId CreateFrom26ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes)
		{
			if (bytes == null)
			{
				return ExchangeId.nullId;
			}
			if (bytes[0] != 0 || !ExchangeId.EntryIdBytesEquals(bytes, ExchangeId.zeroIdBinaryValue))
			{
				return new ExchangeId(bytes);
			}
			return ExchangeId.zeroId;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000183B8 File Offset: 0x000165B8
		public static ExchangeId CreateFrom26ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes, int offset)
		{
			Guid guid = ParseSerialize.ParseGuid(bytes, offset);
			ulong itemNbr = ExchangeIdHelpers.GlobcntFromByteArray(bytes, (uint)(offset + 16));
			ushort replid = (ushort)ParseSerialize.ParseInt16(bytes, offset + 24);
			return ExchangeId.Create(guid, itemNbr, replid);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000183EC File Offset: 0x000165EC
		public static ExchangeId CreateFrom9ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes)
		{
			return ExchangeId.CreateFrom8ByteArray(context, replidGuidMap, bytes, 1);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000183F7 File Offset: 0x000165F7
		public static ExchangeId CreateFrom8ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes)
		{
			return ExchangeId.CreateFrom8ByteArray(context, replidGuidMap, bytes, 0);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00018404 File Offset: 0x00016604
		public static ExchangeId CreateFrom8ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes, int offset)
		{
			ushort replid = (ushort)ParseSerialize.ParseInt16(bytes, offset);
			ulong itemNbr = ExchangeIdHelpers.GlobcntFromByteArray(bytes, (uint)(offset + 2));
			Guid guidFromReplid = replidGuidMap.GetGuidFromReplid(context, replid);
			return ExchangeId.Create(guidFromReplid, itemNbr, replid);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00018435 File Offset: 0x00016635
		public static ExchangeId CreateFrom22ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes)
		{
			return ExchangeId.CreateFrom22Or24ByteArray(context, replidGuidMap, bytes, 0);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00018440 File Offset: 0x00016640
		public static ExchangeId CreateFrom22ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes, int offset)
		{
			return ExchangeId.CreateFrom22Or24ByteArray(context, replidGuidMap, bytes, offset);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001844B File Offset: 0x0001664B
		public static ExchangeId CreateFrom24ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes)
		{
			return ExchangeId.CreateFrom22Or24ByteArray(context, replidGuidMap, bytes, 0);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00018456 File Offset: 0x00016656
		public static ExchangeId CreateFrom24ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes, int offset)
		{
			return ExchangeId.CreateFrom22Or24ByteArray(context, replidGuidMap, bytes, offset);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00018461 File Offset: 0x00016661
		public static bool IsGlobCntValid(ulong globCnt)
		{
			return globCnt > 0UL && globCnt <= 281474976645120UL;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001847C File Offset: 0x0001667C
		public static bool EntryIdBytesEquals(byte[] x, byte[] y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			if (x.Length != 0)
			{
				int num = x.Length - 1;
				while (0 <= num)
				{
					if (x[num] != y[num])
					{
						return false;
					}
					num--;
				}
			}
			return true;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000184C8 File Offset: 0x000166C8
		private static ExchangeId CreateFrom22Or24ByteArray(Context context, IReplidGuidMap replidGuidMap, byte[] bytes, int offset)
		{
			Guid guid = ParseSerialize.ParseGuid(bytes, offset);
			ulong itemNbr = ExchangeIdHelpers.GlobcntFromByteArray(bytes, (uint)(offset + 16));
			ushort replidFromGuid = replidGuidMap.GetReplidFromGuid(context, guid);
			return ExchangeId.Create(guid, itemNbr, replidFromGuid);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000184F9 File Offset: 0x000166F9
		public static ExchangeId Zero
		{
			get
			{
				return ExchangeId.zeroId;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00018500 File Offset: 0x00016700
		public static ExchangeId Null
		{
			get
			{
				return ExchangeId.nullId;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00018507 File Offset: 0x00016707
		public Guid Guid
		{
			get
			{
				if (!this.IsNullOrZero)
				{
					return ParseSerialize.ParseGuid(this.binaryValue, 0);
				}
				return Guid.Empty;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00018524 File Offset: 0x00016724
		public ushort Replid
		{
			get
			{
				return this.GetReplid();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00018539 File Offset: 0x00016739
		public bool IsReplidKnown
		{
			get
			{
				return this.GetReplid() != ushort.MaxValue;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0001854C File Offset: 0x0001674C
		public byte[] Globcnt
		{
			get
			{
				byte[] array = new byte[6];
				ExchangeIdHelpers.GlobcntIntoByteArray(this.Counter, array, 0);
				return array;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0001856F File Offset: 0x0001676F
		public ulong Counter
		{
			get
			{
				if (!this.IsNullOrZero)
				{
					return ExchangeIdHelpers.GlobcntFromByteArray(this.binaryValue, 16U);
				}
				return 0UL;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00018589 File Offset: 0x00016789
		public bool IsZero
		{
			get
			{
				return object.ReferenceEquals(this.binaryValue, ExchangeId.zeroIdBinaryValue);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0001859B File Offset: 0x0001679B
		public bool IsNull
		{
			get
			{
				return this.binaryValue == null;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000185A6 File Offset: 0x000167A6
		public bool IsNullOrZero
		{
			get
			{
				return this.IsNull || this.IsZero;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000185B8 File Offset: 0x000167B8
		public bool IsValid
		{
			get
			{
				return !this.IsNullOrZero;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000185C3 File Offset: 0x000167C3
		public static bool operator ==(ExchangeId id1, ExchangeId id2)
		{
			return ExchangeId.EntryIdBytesEquals(id1.binaryValue, id2.binaryValue);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000185D8 File Offset: 0x000167D8
		public static bool operator !=(ExchangeId id1, ExchangeId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000185E4 File Offset: 0x000167E4
		[Obsolete]
		public static bool operator ==(ExchangeId id1, object id2)
		{
			throw new Exception();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000185EB File Offset: 0x000167EB
		[Obsolete]
		public static bool operator !=(ExchangeId id1, object id2)
		{
			throw new Exception();
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000185F2 File Offset: 0x000167F2
		[Obsolete]
		public static bool operator ==(object id1, ExchangeId id2)
		{
			throw new Exception();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000185F9 File Offset: 0x000167F9
		[Obsolete]
		public static bool operator !=(object id1, ExchangeId id2)
		{
			throw new Exception();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00018600 File Offset: 0x00016800
		public long ToLong()
		{
			return ExchangeIdHelpers.ToLong(this.Replid, this.Counter);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00018613 File Offset: 0x00016813
		public ExchangeShortId ToExchangeShortId()
		{
			return ExchangeShortId.Create(this.Counter, this.Replid);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00018626 File Offset: 0x00016826
		public byte[] To8ByteArray()
		{
			return ExchangeIdHelpers.To8ByteArray(this.Replid, this.Counter);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00018639 File Offset: 0x00016839
		public byte[] To9ByteArray()
		{
			return ExchangeIdHelpers.To9ByteArray(this.Replid, this.Counter);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001864C File Offset: 0x0001684C
		public byte[] To22ByteArray()
		{
			return ExchangeIdHelpers.To22ByteArray(this.Guid, this.Counter);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001865F File Offset: 0x0001685F
		public byte[] To24ByteArray()
		{
			return ExchangeIdHelpers.To24ByteArray(this.Guid, this.Counter);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00018672 File Offset: 0x00016872
		public byte[] To26ByteArray()
		{
			return this.binaryValue;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001867A File Offset: 0x0001687A
		public ExchangeId ConvertNullToZero()
		{
			if (!this.IsNull)
			{
				return this;
			}
			return ExchangeId.zeroId;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00018690 File Offset: 0x00016890
		public override int GetHashCode()
		{
			return (int)this.Counter;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00018699 File Offset: 0x00016899
		public override bool Equals(object other)
		{
			return other is ExchangeId && this.Equals((ExchangeId)other);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000186B1 File Offset: 0x000168B1
		public bool Equals(ExchangeId other)
		{
			return ExchangeId.EntryIdBytesEquals(this.binaryValue, other.binaryValue);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000186C8 File Offset: 0x000168C8
		public int CompareTo(ExchangeId other)
		{
			ushort replid = this.GetReplid();
			if (replid != 0 && replid == other.GetReplid())
			{
				return this.Counter.CompareTo(other.Counter);
			}
			return ValueHelper.ArraysCompare<byte>(this.binaryValue, other.binaryValue);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00018714 File Offset: 0x00016914
		public override string ToString()
		{
			ushort replid = this.GetReplid();
			return string.Format("{0}[{1}]-{2:X}", this.Guid, (replid == ushort.MaxValue) ? "?" : replid.ToString(), this.Counter);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001875E File Offset: 0x0001695E
		private ushort GetReplid()
		{
			if (!this.IsNullOrZero)
			{
				return (ushort)ParseSerialize.ParseInt16(this.binaryValue, 24);
			}
			return 0;
		}

		// Token: 0x0400030D RID: 781
		public const ulong MaxGlobCntValue = 281474976645120UL;

		// Token: 0x0400030E RID: 782
		public const ushort UnknownReplid = 65535;

		// Token: 0x0400030F RID: 783
		private static readonly byte[] zeroIdBinaryValue = new byte[26];

		// Token: 0x04000310 RID: 784
		private static readonly ExchangeId zeroId = new ExchangeId(ExchangeId.zeroIdBinaryValue);

		// Token: 0x04000311 RID: 785
		private static readonly ExchangeId nullId = default(ExchangeId);

		// Token: 0x04000312 RID: 786
		private byte[] binaryValue;
	}
}
