using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200025C RID: 604
	[Flags]
	internal enum InferenceUserCapabilityFlags
	{
		// Token: 0x0400120E RID: 4622
		None = 0,
		// Token: 0x0400120F RID: 4623
		ClassificationReady = 1,
		// Token: 0x04001210 RID: 4624
		UIReady = 2,
		// Token: 0x04001211 RID: 4625
		ClutterEnabled = 4,
		// Token: 0x04001212 RID: 4626
		ClassificationEnabled = 8,
		// Token: 0x04001213 RID: 4627
		HasBeenClutterInvited = 16
	}
}
