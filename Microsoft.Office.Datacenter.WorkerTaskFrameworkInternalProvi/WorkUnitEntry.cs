using System;
using System.Data.Linq.Mapping;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000037 RID: 55
	[Table]
	public class WorkUnitEntry : TableEntity
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000C06F File Offset: 0x0000A26F
		public WorkUnitEntry()
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C078 File Offset: 0x0000A278
		public WorkUnitEntry(string deploymentXml, int aggregationLevel, string scope, double cost, bool exclusiveMachineAccess, bool isFrameworkEntry)
		{
			this.WorkUnitId = -1;
			this.DeploymentXml = deploymentXml;
			this.AggregationLevel = aggregationLevel;
			this.Scope = scope;
			this.Cost = cost;
			this.ExclusiveMachineAccess = exclusiveMachineAccess;
			this.IsFrameworkEntry = isFrameworkEntry;
			this.IsNew = true;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C0C6 File Offset: 0x0000A2C6
		public static string FrameworkEntryScope
		{
			get
			{
				return "Framework";
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000C0CD File Offset: 0x0000A2CD
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000C0D5 File Offset: 0x0000A2D5
		[Column(DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000C0E6 File Offset: 0x0000A2E6
		[Column]
		public int WorkUnitId { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000C0EF File Offset: 0x0000A2EF
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
		[Column]
		public string DeploymentXml { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000C100 File Offset: 0x0000A300
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000C108 File Offset: 0x0000A308
		[Column]
		public int AggregationLevel { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000C111 File Offset: 0x0000A311
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000C119 File Offset: 0x0000A319
		[Column]
		public string Scope { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000C122 File Offset: 0x0000A322
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000C12A File Offset: 0x0000A32A
		[Column]
		public bool Remove { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000C133 File Offset: 0x0000A333
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000C13B File Offset: 0x0000A33B
		[Column]
		public bool RunNow { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000C144 File Offset: 0x0000A344
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000C14C File Offset: 0x0000A34C
		[Column]
		public int WorkUnitState { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000C155 File Offset: 0x0000A355
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000C15D File Offset: 0x0000A35D
		[Column]
		public bool IsFrameworkEntry { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000C166 File Offset: 0x0000A366
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000C16E File Offset: 0x0000A36E
		[Column]
		public bool ExclusiveMachineAccess { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000C177 File Offset: 0x0000A377
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000C17F File Offset: 0x0000A37F
		public double Cost { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C188 File Offset: 0x0000A388
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000C190 File Offset: 0x0000A390
		public bool IsNew { get; set; }

		// Token: 0x0600037F RID: 895 RVA: 0x0000C19C File Offset: 0x0000A39C
		public static int Compare(WorkUnitEntry entry1, WorkUnitEntry entry2)
		{
			int num = string.Compare(entry1.Scope, entry2.Scope, StringComparison.OrdinalIgnoreCase);
			if (num == 0)
			{
				num = string.Compare(entry1.DeploymentXml, entry2.DeploymentXml, StringComparison.OrdinalIgnoreCase);
			}
			return num;
		}
	}
}
