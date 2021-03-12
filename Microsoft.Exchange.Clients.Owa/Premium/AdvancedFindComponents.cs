using System;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004B4 RID: 1204
	[Flags]
	public enum AdvancedFindComponents
	{
		// Token: 0x04001F78 RID: 8056
		Categories = 1,
		// Token: 0x04001F79 RID: 8057
		FromTo = 2,
		// Token: 0x04001F7A RID: 8058
		SubjectBody = 4,
		// Token: 0x04001F7B RID: 8059
		SearchTextInSubject = 8,
		// Token: 0x04001F7C RID: 8060
		SearchButton = 16,
		// Token: 0x04001F7D RID: 8061
		SearchTextInName = 32
	}
}
