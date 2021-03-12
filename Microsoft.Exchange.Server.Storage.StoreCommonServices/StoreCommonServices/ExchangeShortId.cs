using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000061 RID: 97
	public struct ExchangeShortId : IComparable<ExchangeShortId>, IEquatable<ExchangeShortId>
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00018E44 File Offset: 0x00017044
		private ExchangeShortId(long value)
		{
			this.value = (ulong)value;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00018E4D File Offset: 0x0001704D
		public static ExchangeShortId Create(ulong itemNbr, ushort replid)
		{
			return new ExchangeShortId(ExchangeIdHelpers.ToLong(replid, itemNbr));
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00018E5B File Offset: 0x0001705B
		public static ExchangeShortId Zero
		{
			get
			{
				return new ExchangeShortId(0L);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00018E64 File Offset: 0x00017064
		public ushort Replid
		{
			get
			{
				ushort result;
				ulong num;
				ExchangeIdHelpers.FromLong((long)this.value, out result, out num);
				return result;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00018E84 File Offset: 0x00017084
		public ulong Counter
		{
			get
			{
				ushort num;
				ulong result;
				ExchangeIdHelpers.FromLong((long)this.value, out num, out result);
				return result;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00018EA1 File Offset: 0x000170A1
		public bool IsZero
		{
			get
			{
				return this.value == 0UL;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00018EAD File Offset: 0x000170AD
		public static bool operator ==(ExchangeShortId id1, ExchangeShortId id2)
		{
			return id1.value == id2.value;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00018EBF File Offset: 0x000170BF
		public static bool operator !=(ExchangeShortId id1, ExchangeShortId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00018ECB File Offset: 0x000170CB
		[Obsolete]
		public static bool operator ==(ExchangeShortId id1, object id2)
		{
			throw new Exception();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00018ED2 File Offset: 0x000170D2
		[Obsolete]
		public static bool operator !=(ExchangeShortId id1, object id2)
		{
			throw new Exception();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00018ED9 File Offset: 0x000170D9
		[Obsolete]
		public static bool operator ==(object id1, ExchangeShortId id2)
		{
			throw new Exception();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00018EE0 File Offset: 0x000170E0
		[Obsolete]
		public static bool operator !=(object id1, ExchangeShortId id2)
		{
			throw new Exception();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00018EE7 File Offset: 0x000170E7
		public override int GetHashCode()
		{
			return (int)this.Counter;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00018EF0 File Offset: 0x000170F0
		public override bool Equals(object other)
		{
			return other is ExchangeShortId && this.Equals((ExchangeShortId)other);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00018F08 File Offset: 0x00017108
		public bool Equals(ExchangeShortId other)
		{
			return this.value == other.value;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00018F19 File Offset: 0x00017119
		public int CompareTo(ExchangeShortId other)
		{
			return this.value.CompareTo(other.value);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00018F2D File Offset: 0x0001712D
		public override string ToString()
		{
			return string.Format("[{0}]-{1:X}", this.Replid, this.Counter);
		}

		// Token: 0x04000315 RID: 789
		private ulong value;
	}
}
