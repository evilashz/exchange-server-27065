using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Office.CompliancePolicy.ComplianceTask
{
	// Token: 0x02000059 RID: 89
	public abstract class ComplianceTask : DarTask
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00006F22 File Offset: 0x00005122
		public ComplianceTask()
		{
			this.complianceServiceProvider = null;
			this.TaskStatsData = new ComplianceTaskStatistics();
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00006F3C File Offset: 0x0000513C
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00006F44 File Offset: 0x00005144
		public virtual ComplianceServiceProvider ComplianceServiceProvider
		{
			get
			{
				return this.complianceServiceProvider;
			}
			set
			{
				this.complianceServiceProvider = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00006F4D File Offset: 0x0000514D
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00006F54 File Offset: 0x00005154
		public virtual string WorkloadData
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00006F56 File Offset: 0x00005156
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00006F5E File Offset: 0x0000515E
		[SerializableTaskData]
		public ComplianceTaskStatistics TaskStatsData { get; set; }

		// Token: 0x06000248 RID: 584 RVA: 0x00006F67 File Offset: 0x00005167
		public virtual string GetWorkloadDataFromWorkload()
		{
			return string.Empty;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00006F70 File Offset: 0x00005170
		protected override IEnumerable<Type> GetKnownTypes()
		{
			return base.GetKnownTypes().Concat(new Type[]
			{
				typeof(ComplianceTaskStatistics)
			});
		}

		// Token: 0x04000124 RID: 292
		private ComplianceServiceProvider complianceServiceProvider;
	}
}
