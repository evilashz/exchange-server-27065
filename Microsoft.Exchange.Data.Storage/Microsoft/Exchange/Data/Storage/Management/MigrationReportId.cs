using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A33 RID: 2611
	[Serializable]
	public class MigrationReportId : ObjectId
	{
		// Token: 0x06005FD5 RID: 24533 RVA: 0x00194C63 File Offset: 0x00192E63
		internal MigrationReportId(string reportId)
		{
			reportId = reportId.Replace('-', '+').Replace('_', '/');
			this.Id = StoreObjectId.Deserialize(reportId);
		}

		// Token: 0x06005FD6 RID: 24534 RVA: 0x00194C8C File Offset: 0x00192E8C
		internal MigrationReportId(StoreObjectId reportId)
		{
			this.Id = reportId;
		}

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x00194C9B File Offset: 0x00192E9B
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x00194CA3 File Offset: 0x00192EA3
		internal StoreObjectId Id { get; private set; }

		// Token: 0x06005FD9 RID: 24537 RVA: 0x00194CAC File Offset: 0x00192EAC
		public override byte[] GetBytes()
		{
			return this.Id.GetBytes();
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x00194CBC File Offset: 0x00192EBC
		public override string ToString()
		{
			string text = this.Id.ToBase64String();
			return text.Replace('+', '-').Replace('/', '_');
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x00194CE8 File Offset: 0x00192EE8
		public override bool Equals(object obj)
		{
			MigrationReportId migrationReportId = obj as MigrationReportId;
			return migrationReportId != null && this.Id.Equals(migrationReportId.Id);
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x00194D12 File Offset: 0x00192F12
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}
