using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000827 RID: 2087
	[Flags]
	internal enum CalendarMethod
	{
		// Token: 0x04002A76 RID: 10870
		None = 0,
		// Token: 0x04002A77 RID: 10871
		Publish = 1,
		// Token: 0x04002A78 RID: 10872
		Request = 2,
		// Token: 0x04002A79 RID: 10873
		Reply = 4,
		// Token: 0x04002A7A RID: 10874
		Add = 8,
		// Token: 0x04002A7B RID: 10875
		Cancel = 16,
		// Token: 0x04002A7C RID: 10876
		Refresh = 32,
		// Token: 0x04002A7D RID: 10877
		Counter = 64,
		// Token: 0x04002A7E RID: 10878
		DeclineCounter = 128,
		// Token: 0x04002A7F RID: 10879
		All = 255
	}
}
