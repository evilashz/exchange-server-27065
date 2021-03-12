using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200001E RID: 30
	public interface IWorkItemFactory
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002AC RID: 684
		string LocalPath { get; }

		// Token: 0x060002AD RID: 685
		T CreateWorkItem<T>(WorkDefinition definition) where T : WorkItem;
	}
}
