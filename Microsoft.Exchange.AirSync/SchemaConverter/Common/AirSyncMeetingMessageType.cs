using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000176 RID: 374
	internal enum AirSyncMeetingMessageType
	{
		// Token: 0x04000AB1 RID: 2737
		Unspecified,
		// Token: 0x04000AB2 RID: 2738
		NewMeetingRequest,
		// Token: 0x04000AB3 RID: 2739
		FullUpdate,
		// Token: 0x04000AB4 RID: 2740
		InformationalUpdate,
		// Token: 0x04000AB5 RID: 2741
		Outdated,
		// Token: 0x04000AB6 RID: 2742
		PrincipalWantsCopy,
		// Token: 0x04000AB7 RID: 2743
		DelegatedCopy
	}
}
