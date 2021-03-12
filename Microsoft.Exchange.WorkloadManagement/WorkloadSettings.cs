using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200000E RID: 14
	internal struct WorkloadSettings
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003091 File Offset: 0x00001291
		public WorkloadSettings(WorkloadType workloadType)
		{
			this = new WorkloadSettings(workloadType, false);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000309B File Offset: 0x0000129B
		public WorkloadSettings(WorkloadType workloadType, bool backgroundLoad)
		{
			this = default(WorkloadSettings);
			this.WorkloadType = workloadType;
			this.IsBackgroundLoad = backgroundLoad;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000030B2 File Offset: 0x000012B2
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000030BA File Offset: 0x000012BA
		public WorkloadType WorkloadType { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000030C3 File Offset: 0x000012C3
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000030CB File Offset: 0x000012CB
		public bool IsBackgroundLoad { get; private set; }

		// Token: 0x06000089 RID: 137 RVA: 0x000030D4 File Offset: 0x000012D4
		public override string ToString()
		{
			return string.Format("WorkloadType: {0}, IsBackgroundLoad: {1}", this.WorkloadType, this.IsBackgroundLoad);
		}
	}
}
