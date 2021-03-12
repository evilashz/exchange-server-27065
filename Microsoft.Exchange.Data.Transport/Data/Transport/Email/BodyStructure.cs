using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000D5 RID: 213
	internal enum BodyStructure
	{
		// Token: 0x040002FC RID: 764
		Undefined,
		// Token: 0x040002FD RID: 765
		None,
		// Token: 0x040002FE RID: 766
		SingleBody,
		// Token: 0x040002FF RID: 767
		AlternativeBodies,
		// Token: 0x04000300 RID: 768
		SingleBodyWithRelatedAttachments,
		// Token: 0x04000301 RID: 769
		AlternativeBodiesWithMhtml,
		// Token: 0x04000302 RID: 770
		AlternativeBodiesWithSharedAttachments
	}
}
