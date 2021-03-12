using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200008E RID: 142
	internal struct StoreIdPair : IEquatable<StoreIdPair>
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0000CE9A File Offset: 0x0000B09A
		public StoreIdPair(StoreId first, StoreId second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000CEAA File Offset: 0x0000B0AA
		public StoreId First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000CEB2 File Offset: 0x0000B0B2
		public StoreId Second
		{
			get
			{
				return this.second;
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000CEBA File Offset: 0x0000B0BA
		public override bool Equals(object obj)
		{
			return obj is StoreIdPair && this.Equals((StoreIdPair)obj);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		public override int GetHashCode()
		{
			int hashCode = this.first.GetHashCode();
			int hashCode2 = this.second.GetHashCode();
			return hashCode ^ hashCode2;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000CF0E File Offset: 0x0000B10E
		public bool Equals(StoreIdPair other)
		{
			return this.first == other.first && this.second == other.second;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000CF44 File Offset: 0x0000B144
		public override string ToString()
		{
			return string.Format("{0}/{1}", this.first.ToString(), this.second.ToString());
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000CF84 File Offset: 0x0000B184
		internal static StoreIdPair Parse(Reader reader)
		{
			StoreId storeId = StoreId.Parse(reader);
			StoreId storeId2 = StoreId.Parse(reader);
			return new StoreIdPair(storeId, storeId2);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		internal void Serialize(Writer writer)
		{
			this.first.Serialize(writer);
			this.second.Serialize(writer);
		}

		// Token: 0x040001FD RID: 509
		private readonly StoreId first;

		// Token: 0x040001FE RID: 510
		private readonly StoreId second;

		// Token: 0x040001FF RID: 511
		public static readonly StoreIdPair Empty = new StoreIdPair(StoreId.Empty, StoreId.Empty);
	}
}
