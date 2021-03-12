using System;
using System.Text;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A3F RID: 2623
	[Serializable]
	public class MigrationUserId : ObjectId
	{
		// Token: 0x06006013 RID: 24595 RVA: 0x001954A7 File Offset: 0x001936A7
		internal MigrationUserId(string userId, Guid jobItemGuid)
		{
			this.Id = userId;
			this.JobItemGuid = jobItemGuid;
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x06006014 RID: 24596 RVA: 0x001954BD File Offset: 0x001936BD
		// (set) Token: 0x06006015 RID: 24597 RVA: 0x001954C5 File Offset: 0x001936C5
		public string Id { get; private set; }

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x06006016 RID: 24598 RVA: 0x001954CE File Offset: 0x001936CE
		// (set) Token: 0x06006017 RID: 24599 RVA: 0x001954D6 File Offset: 0x001936D6
		public Guid JobItemGuid { get; private set; }

		// Token: 0x06006018 RID: 24600 RVA: 0x001954DF File Offset: 0x001936DF
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.Id);
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x001954F4 File Offset: 0x001936F4
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Id) && this.JobItemGuid != Guid.Empty)
			{
				return this.JobItemGuid.ToString();
			}
			return this.Id;
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x0019553C File Offset: 0x0019373C
		public override bool Equals(object obj)
		{
			MigrationUserId migrationUserId = obj as MigrationUserId;
			if (migrationUserId == null)
			{
				return false;
			}
			if (this.JobItemGuid == Guid.Empty && migrationUserId.JobItemGuid == Guid.Empty)
			{
				return string.Equals(this.Id, migrationUserId.Id, StringComparison.OrdinalIgnoreCase);
			}
			return this.JobItemGuid == migrationUserId.JobItemGuid;
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x001955A0 File Offset: 0x001937A0
		public override int GetHashCode()
		{
			if (this.JobItemGuid != Guid.Empty)
			{
				return this.JobItemGuid.GetHashCode();
			}
			return this.Id.GetHashCode();
		}
	}
}
