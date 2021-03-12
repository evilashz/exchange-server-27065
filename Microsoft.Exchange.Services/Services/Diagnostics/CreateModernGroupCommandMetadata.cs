using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200002E RID: 46
	internal enum CreateModernGroupCommandMetadata
	{
		// Token: 0x04000200 RID: 512
		[DisplayName("CMG", "SMTP")]
		GroupSmtpAddress,
		// Token: 0x04000201 RID: 513
		[DisplayName("CMG", "GCT")]
		GroupCreationTime,
		// Token: 0x04000202 RID: 514
		[DisplayName("CMG", "AADT")]
		AADIdentityCreationTime,
		// Token: 0x04000203 RID: 515
		[DisplayName("CMG", "AADCCT")]
		AADCompleteCallbackTime,
		// Token: 0x04000204 RID: 516
		[DisplayName("CMG", "SPNT")]
		SharePointNotificationTime,
		// Token: 0x04000205 RID: 517
		[DisplayName("CMG", "MBT")]
		MailboxCreationTime,
		// Token: 0x04000206 RID: 518
		[DisplayName("CMG", "TPT")]
		TotalProcessingTime,
		// Token: 0x04000207 RID: 519
		[DisplayName("CMG", "DES")]
		DescriptionSpecified,
		// Token: 0x04000208 RID: 520
		[DisplayName("CMG", "MC")]
		MemberCount,
		// Token: 0x04000209 RID: 521
		[DisplayName("CMG", "OC")]
		OwnerCount,
		// Token: 0x0400020A RID: 522
		[DisplayName("CMG", "ERTP")]
		ExceptionType,
		// Token: 0x0400020B RID: 523
		[DisplayName("CMG", "ER")]
		Exception,
		// Token: 0x0400020C RID: 524
		[DisplayName("CMG", "ERLOC")]
		ExceptionLocation,
		// Token: 0x0400020D RID: 525
		[DisplayName("CMG", "CID")]
		CmdletCorrelationId,
		// Token: 0x0400020E RID: 526
		[DisplayName("CMG", "EA")]
		ErrorAction,
		// Token: 0x0400020F RID: 527
		[DisplayName("CMG", "EC")]
		ErrorCode,
		// Token: 0x04000210 RID: 528
		[DisplayName("CMG", "AADAQT")]
		AADAliasQueryTime,
		// Token: 0x04000211 RID: 529
		[DisplayName("CMG", "ASDV")]
		AutoSubscribeOptionDefault,
		// Token: 0x04000212 RID: 530
		[DisplayName("CMG", "ASRV")]
		AutoSubscribeOptionReceived
	}
}
