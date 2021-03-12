using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200028F RID: 655
	public enum JET_IdxInfo
	{
		// Token: 0x040006E0 RID: 1760
		Default,
		// Token: 0x040006E1 RID: 1761
		List,
		// Token: 0x040006E2 RID: 1762
		[Obsolete("This value is not used, and is provided for completeness to match the published header in the SDK.")]
		SysTabCursor,
		// Token: 0x040006E3 RID: 1763
		[Obsolete("This value is not used, and is provided for completeness to match the published header in the SDK.")]
		OLC,
		// Token: 0x040006E4 RID: 1764
		[Obsolete("This value is not used, and is provided for completeness to match the published header in the SDK.")]
		ResetOLC,
		// Token: 0x040006E5 RID: 1765
		SpaceAlloc,
		// Token: 0x040006E6 RID: 1766
		LCID,
		// Token: 0x040006E7 RID: 1767
		[Obsolete("Use JET_IdxInfo.LCID")]
		Langid = 6,
		// Token: 0x040006E8 RID: 1768
		Count,
		// Token: 0x040006E9 RID: 1769
		VarSegMac,
		// Token: 0x040006EA RID: 1770
		IndexId,
		// Token: 0x040006EB RID: 1771
		KeyMost
	}
}
