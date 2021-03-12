using System;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000037 RID: 55
	public enum InputState
	{
		// Token: 0x040000D6 RID: 214
		NotAllowed,
		// Token: 0x040000D7 RID: 215
		NotStarted,
		// Token: 0x040000D8 RID: 216
		StartedNotComplete,
		// Token: 0x040000D9 RID: 217
		StartedCompleteNotAmbiguous,
		// Token: 0x040000DA RID: 218
		StartedCompleteAmbiguous
	}
}
