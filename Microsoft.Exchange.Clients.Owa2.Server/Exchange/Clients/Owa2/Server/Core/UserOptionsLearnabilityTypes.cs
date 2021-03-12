using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000100 RID: 256
	[Flags]
	public enum UserOptionsLearnabilityTypes
	{
		// Token: 0x04000664 RID: 1636
		None = 0,
		// Token: 0x04000665 RID: 1637
		Clutter = 1,
		// Token: 0x04000666 RID: 1638
		ClutterDeleteAll = 2,
		// Token: 0x04000667 RID: 1639
		PeopleCentricTriage = 4,
		// Token: 0x04000668 RID: 1640
		ModernGroups = 8,
		// Token: 0x04000669 RID: 1641
		ModernGroupsCompose = 16,
		// Token: 0x0400066A RID: 1642
		DocCollabEditACopy = 32,
		// Token: 0x0400066B RID: 1643
		PeopleCentricTriageReadingPane = 64,
		// Token: 0x0400066C RID: 1644
		ModernGroupsComposeTNarrow = 128,
		// Token: 0x0400066D RID: 1645
		HelpPanel = 256,
		// Token: 0x0400066E RID: 1646
		ModernAttachments = 512
	}
}
