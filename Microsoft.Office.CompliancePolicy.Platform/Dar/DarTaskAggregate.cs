using System;
using System.ComponentModel;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000069 RID: 105
	public class DarTaskAggregate
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000A638 File Offset: 0x00008838
		public DarTaskAggregate()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000A664 File Offset: 0x00008864
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000A66C File Offset: 0x0000886C
		public string Id { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000A675 File Offset: 0x00008875
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000A67D File Offset: 0x0000887D
		public string TaskType { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000A686 File Offset: 0x00008886
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000A68E File Offset: 0x0000888E
		public bool Enabled { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000A697 File Offset: 0x00008897
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000A69F File Offset: 0x0000889F
		public string ScopeId { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000A6A8 File Offset: 0x000088A8
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000A6B0 File Offset: 0x000088B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual object WorkloadContext { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000A6B9 File Offset: 0x000088B9
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000A6C1 File Offset: 0x000088C1
		public int MaxRunningTasks { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000A6CA File Offset: 0x000088CA
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000A6D2 File Offset: 0x000088D2
		public RecurrenceType RecurrenceType { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000A6DB File Offset: 0x000088DB
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public RecurrenceFrequency RecurrenceFrequency { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000A6EC File Offset: 0x000088EC
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000A6F4 File Offset: 0x000088F4
		public int RecurrenceInterval { get; set; }
	}
}
