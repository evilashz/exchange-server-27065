using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000036 RID: 54
	[Table]
	public class WorkUnit : TableEntity
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000BE00 File Offset: 0x0000A000
		public WorkUnit()
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BE10 File Offset: 0x0000A010
		public WorkUnit(int id)
		{
			this.WorkUnitId = id;
			this.IsExclusive = false;
			this.Entries = new List<WorkUnitEntry>();
			this.EntryDeploymentXmlFiles = new HashSet<string>();
			this.Cost = 0.0;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000BE5D File Offset: 0x0000A05D
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000BE65 File Offset: 0x0000A065
		[Column]
		public int WorkUnitId { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000BE6E File Offset: 0x0000A06E
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000BE76 File Offset: 0x0000A076
		[Column]
		public bool RunNow { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000BE7F File Offset: 0x0000A07F
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000BE87 File Offset: 0x0000A087
		public List<WorkUnitEntry> Entries { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000BE90 File Offset: 0x0000A090
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000BE98 File Offset: 0x0000A098
		public HashSet<string> EntryDeploymentXmlFiles { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000BEA9 File Offset: 0x0000A0A9
		public bool IsExclusive
		{
			get
			{
				return this.isExclusive;
			}
			internal set
			{
				this.isExclusive = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000BEBA File Offset: 0x0000A0BA
		public double Cost { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		internal static int UnassignedWorkUnitId
		{
			get
			{
				return WorkUnit.unassignedWorkUnitIdInternal;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BECA File Offset: 0x0000A0CA
		internal static int ToRemoveWorkUnitId
		{
			get
			{
				return WorkUnit.toRemoveWorkUnitIdInternal;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000BED1 File Offset: 0x0000A0D1
		internal bool IsUnassignedWorkUnit
		{
			get
			{
				return this.WorkUnitId == WorkUnit.unassignedWorkUnitIdInternal;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		internal bool IsToRemoveWorkUnit
		{
			get
			{
				return this.WorkUnitId == WorkUnit.toRemoveWorkUnitIdInternal;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000BEEF File Offset: 0x0000A0EF
		internal bool IsNew
		{
			get
			{
				return this.WorkUnitId < 0 && !this.IsUnassignedWorkUnit && !this.IsToRemoveWorkUnit;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000BF10 File Offset: 0x0000A110
		public bool AddWorkUnitEntry(WorkUnitEntry entry)
		{
			bool result = false;
			bool flag = this.Cost + entry.Cost <= (double)Settings.MaxWorkUnitCost;
			bool flag2 = flag;
			if (entry.ExclusiveMachineAccess)
			{
				bool flag3 = this.EntryDeploymentXmlFiles.Count == 0 || this.EntryDeploymentXmlFiles.Contains(entry.DeploymentXml);
				flag2 = (flag2 && flag3);
			}
			if (this.IsExclusive)
			{
				bool flag4 = this.EntryDeploymentXmlFiles.Contains(entry.DeploymentXml);
				flag2 = (flag2 && flag4);
			}
			flag2 = ((flag2 && (this.aggregationLevel == -1 || this.aggregationLevel == entry.AggregationLevel)) || this.IsUnassignedWorkUnit || this.IsToRemoveWorkUnit);
			if (flag2)
			{
				if (!this.IsToRemoveWorkUnit)
				{
					entry.WorkUnitId = this.WorkUnitId;
				}
				this.Entries.Add(entry);
				this.EntryDeploymentXmlFiles.Add(entry.DeploymentXml);
				this.Cost += entry.Cost;
				this.isExclusive = entry.ExclusiveMachineAccess;
				this.aggregationLevel = entry.AggregationLevel;
				result = true;
			}
			return result;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C02C File Offset: 0x0000A22C
		public void RemoveWorkUnitEntry(WorkUnitEntry entry)
		{
			this.Entries.Remove(entry);
			this.EntryDeploymentXmlFiles.Remove(entry.DeploymentXml);
			this.Cost -= entry.Cost;
		}

		// Token: 0x04000157 RID: 343
		private const int AggregationLevelNotSet = -1;

		// Token: 0x04000158 RID: 344
		private static int unassignedWorkUnitIdInternal = -1;

		// Token: 0x04000159 RID: 345
		private static int toRemoveWorkUnitIdInternal = -2;

		// Token: 0x0400015A RID: 346
		private bool isExclusive;

		// Token: 0x0400015B RID: 347
		private int aggregationLevel = -1;
	}
}
