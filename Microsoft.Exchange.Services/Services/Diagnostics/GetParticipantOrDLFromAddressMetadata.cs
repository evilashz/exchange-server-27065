using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000043 RID: 67
	public enum GetParticipantOrDLFromAddressMetadata
	{
		// Token: 0x040002FE RID: 766
		[DisplayName("GPDA", "OT")]
		ObjectType,
		// Token: 0x040002FF RID: 767
		[DisplayName("GPDA", "EA")]
		EmailAddress,
		// Token: 0x04000300 RID: 768
		[DisplayName("GPDA", "NM")]
		Name,
		// Token: 0x04000301 RID: 769
		[DisplayName("GPDA", "ODN")]
		OriginalDisplayName,
		// Token: 0x04000302 RID: 770
		[DisplayName("GPDA", "MT")]
		MailboxType,
		// Token: 0x04000303 RID: 771
		[DisplayName("GPDA", "IID")]
		ItemId
	}
}
