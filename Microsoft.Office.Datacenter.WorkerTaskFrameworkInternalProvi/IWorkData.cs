using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000013 RID: 19
	public interface IWorkData
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001D2 RID: 466
		string InternalStorageKey { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001D3 RID: 467
		string ExternalStorageKey { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001D4 RID: 468
		string SecondaryExternalStorageKey { get; }
	}
}
