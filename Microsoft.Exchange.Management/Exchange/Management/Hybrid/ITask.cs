using System;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200090E RID: 2318
	internal interface ITask
	{
		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x06005249 RID: 21065
		string Name { get; }

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x0600524A RID: 21066
		int Weight { get; }

		// Token: 0x0600524B RID: 21067
		bool CheckPrereqs(ITaskContext taskContext);

		// Token: 0x0600524C RID: 21068
		bool NeedsConfiguration(ITaskContext taskContext);

		// Token: 0x0600524D RID: 21069
		bool Configure(ITaskContext taskContext);

		// Token: 0x0600524E RID: 21070
		bool ValidateConfiguration(ITaskContext taskContext);
	}
}
