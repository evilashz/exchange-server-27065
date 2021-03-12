using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200003F RID: 63
	internal enum GetConversationItemsMetadata
	{
		// Token: 0x040002CE RID: 718
		[DisplayName("GCI", "ID")]
		ConversationId,
		// Token: 0x040002CF RID: 719
		[DisplayName("GCI", "NC")]
		TotalNodeCount,
		// Token: 0x040002D0 RID: 720
		[DisplayName("GCI", "LC")]
		LeafNodeCount,
		// Token: 0x040002D1 RID: 721
		[DisplayName("GCI", "IE")]
		ItemsExtracted,
		// Token: 0x040002D2 RID: 722
		[DisplayName("GCI", "IO")]
		ItemsOpened,
		// Token: 0x040002D3 RID: 723
		[DisplayName("GCI", "S")]
		SummariesConstructed,
		// Token: 0x040002D4 RID: 724
		[DisplayName("GCI", "TC")]
		BodyTagMatchingAttemptsCount,
		// Token: 0x040002D5 RID: 725
		[DisplayName("GCI", "TI")]
		BodyTagMatchingIssuesCount,
		// Token: 0x040002D6 RID: 726
		[DisplayName("GCI", "TNP")]
		BodyTagNotPresentCount,
		// Token: 0x040002D7 RID: 727
		[DisplayName("GCI", "TM")]
		BodyTagMismatchedCount,
		// Token: 0x040002D8 RID: 728
		[DisplayName("GCI", "FM")]
		BodyFormatMismatchedCount,
		// Token: 0x040002D9 RID: 729
		[DisplayName("GCI", "NMSH")]
		NonMSHeaderCount,
		// Token: 0x040002DA RID: 730
		[DisplayName("GCI", "EPN")]
		ExtraPropertiesNeededCount,
		// Token: 0x040002DB RID: 731
		[DisplayName("GCI", "PNF")]
		ParticipantNotFoundCount,
		// Token: 0x040002DC RID: 732
		[DisplayName("GCI", "A")]
		AttachmentPresentCount,
		// Token: 0x040002DD RID: 733
		[DisplayName("GCI", "AM")]
		MapiAttachmentPresentCount,
		// Token: 0x040002DE RID: 734
		[DisplayName("GCI", "IL")]
		PossibleInlinesCount,
		// Token: 0x040002DF RID: 735
		[DisplayName("GCI", "IRM")]
		IrmProtectedCount,
		// Token: 0x040002E0 RID: 736
		[DisplayName("GCI", "SS")]
		SyncState
	}
}
