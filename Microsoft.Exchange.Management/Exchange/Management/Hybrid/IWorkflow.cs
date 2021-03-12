using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000910 RID: 2320
	internal interface IWorkflow
	{
		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x06005268 RID: 21096
		IEnumerable<ITask> Tasks { get; }

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06005269 RID: 21097
		int PercentCompleted { get; }

		// Token: 0x0600526A RID: 21098
		void Initialize();

		// Token: 0x0600526B RID: 21099
		void UpdateProgress(ITask task);
	}
}
