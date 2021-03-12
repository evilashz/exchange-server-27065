using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000121 RID: 289
	internal abstract class JobBase
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x0001931C File Offset: 0x0001751C
		public JobBase(IEnumerable<WorkItemBase> workItems, Action<JobBase> onJobCompleted, SyncAgentContext syncAgentContext)
		{
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<WorkItemBase>("workItems", workItems);
			ArgumentValidator.ThrowIfNull("onJobCompleted", onJobCompleted);
			ArgumentValidator.ThrowIfNull("syncAgentContext", syncAgentContext);
			this.WorkItems = workItems;
			this.HostStateProvider = syncAgentContext.HostStateProvider;
			this.OnJobCompleted = onJobCompleted;
			this.LogProvider = syncAgentContext.LogProvider;
			this.SyncAgentContext = syncAgentContext;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001937D File Offset: 0x0001757D
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00019385 File Offset: 0x00017585
		internal HostStateProvider HostStateProvider { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001938E File Offset: 0x0001758E
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00019396 File Offset: 0x00017596
		internal ExecutionLog LogProvider { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001939F File Offset: 0x0001759F
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x000193A7 File Offset: 0x000175A7
		internal SyncAgentContext SyncAgentContext { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x000193B0 File Offset: 0x000175B0
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x000193B8 File Offset: 0x000175B8
		private protected IEnumerable<WorkItemBase> WorkItems { protected get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000193C1 File Offset: 0x000175C1
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x000193C9 File Offset: 0x000175C9
		private protected Action<JobBase> OnJobCompleted { protected get; private set; }

		// Token: 0x06000841 RID: 2113
		public abstract void Begin(object state);

		// Token: 0x06000842 RID: 2114 RVA: 0x000193D2 File Offset: 0x000175D2
		public IEnumerable<WorkItemBase> End()
		{
			return this.WorkItems;
		}
	}
}
