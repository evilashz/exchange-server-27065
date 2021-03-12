using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200005B RID: 91
	public enum Operation : uint
	{
		// Token: 0x040001C4 RID: 452
		CreateJob = 1U,
		// Token: 0x040001C5 RID: 453
		QueryJob,
		// Token: 0x040001C6 RID: 454
		RemoveJob,
		// Token: 0x040001C7 RID: 455
		PauseJob,
		// Token: 0x040001C8 RID: 456
		ResumeJob,
		// Token: 0x040001C9 RID: 457
		ExecuteJob,
		// Token: 0x040001CA RID: 458
		Count
	}
}
