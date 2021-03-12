using System;
using System.Text;

namespace Microsoft.Exchange.Data.Storage.Management.Migration
{
	// Token: 0x02000A26 RID: 2598
	[Serializable]
	public sealed class MigrationEndpointId : ObjectId
	{
		// Token: 0x06005F8B RID: 24459 RVA: 0x001933D7 File Offset: 0x001915D7
		public MigrationEndpointId(string id, Guid guid)
		{
			this.Id = id;
			this.Guid = guid;
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x06005F8C RID: 24460 RVA: 0x001933ED File Offset: 0x001915ED
		// (set) Token: 0x06005F8D RID: 24461 RVA: 0x001933F5 File Offset: 0x001915F5
		public string Id { get; private set; }

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x06005F8E RID: 24462 RVA: 0x001933FE File Offset: 0x001915FE
		// (set) Token: 0x06005F8F RID: 24463 RVA: 0x00193406 File Offset: 0x00191606
		public Guid Guid { get; private set; }

		// Token: 0x06005F90 RID: 24464 RVA: 0x0019340F File Offset: 0x0019160F
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.Id);
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x00193424 File Offset: 0x00191624
		public override string ToString()
		{
			return this.Id ?? this.Guid.ToString();
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x00193450 File Offset: 0x00191650
		public override bool Equals(object obj)
		{
			MigrationEndpointId migrationEndpointId = obj as MigrationEndpointId;
			return migrationEndpointId != null && string.Equals(this.Id, migrationEndpointId.Id, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x0019347B File Offset: 0x0019167B
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x00193488 File Offset: 0x00191688
		public QueryFilter GetFilter()
		{
			if (this.Guid == MigrationEndpointId.Any.Guid)
			{
				return new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.MS-Exchange.MigrationEndpoint");
			}
			if (this.Guid != Guid.Empty)
			{
				return new ComparisonFilter(ComparisonOperator.Equal, MigrationEndpointMessageSchema.MigrationEndpointGuid, this.Guid);
			}
			return new TextFilter(MigrationEndpointMessageSchema.MigrationEndpointName, this.Id, MatchOptions.FullString, MatchFlags.IgnoreCase);
		}

		// Token: 0x040035FF RID: 13823
		public static readonly MigrationEndpointId Any = new MigrationEndpointId("13CA1A28-C866-4CF7-9D81-22B5C0E03AD2", new Guid("13CA1A28-C866-4CF7-9D81-22B5C0E03AD2"));
	}
}
