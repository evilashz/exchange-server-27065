using System;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000099 RID: 153
	public class LocalDataAccessMetaData
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0001DAAC File Offset: 0x0001BCAC
		internal LocalDataAccessMetaData(EventRecord record)
		{
			if (record != null)
			{
				this.RecordId = record.RecordId.Value;
				this.TimeStamp = record.TimeCreated.Value;
				this.Bookmark = record.Bookmark;
				this.EventId = record.Id;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001DB02 File Offset: 0x0001BD02
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001DB0A File Offset: 0x0001BD0A
		public long RecordId { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001DB13 File Offset: 0x0001BD13
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001DB1B File Offset: 0x0001BD1B
		public DateTime TimeStamp { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001DB24 File Offset: 0x0001BD24
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public EventBookmark Bookmark { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001DB35 File Offset: 0x0001BD35
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001DB3D File Offset: 0x0001BD3D
		public int EventId { get; private set; }
	}
}
