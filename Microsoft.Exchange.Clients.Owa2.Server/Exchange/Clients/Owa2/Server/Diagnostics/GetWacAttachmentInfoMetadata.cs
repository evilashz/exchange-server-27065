using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000442 RID: 1090
	internal enum GetWacAttachmentInfoMetadata
	{
		// Token: 0x040014B5 RID: 5301
		[DisplayName("GWAI", "LSA")]
		LogonSmtpAddress,
		// Token: 0x040014B6 RID: 5302
		[DisplayName("GWAI", "MSA")]
		MailboxSmtpAddress,
		// Token: 0x040014B7 RID: 5303
		[DisplayName("GWAI", "E")]
		Edit,
		// Token: 0x040014B8 RID: 5304
		[DisplayName("GWAI", "EXT")]
		Extension,
		// Token: 0x040014B9 RID: 5305
		[DisplayName("GWAI", "D")]
		DraftProvided,
		// Token: 0x040014BA RID: 5306
		[DisplayName("GWAI", "URL")]
		WacUrl,
		// Token: 0x040014BB RID: 5307
		[DisplayName("GWAI", "HE")]
		HandledException,
		// Token: 0x040014BC RID: 5308
		[DisplayName("GWAI", "Status")]
		Status,
		// Token: 0x040014BD RID: 5309
		[DisplayName("GWAI", "ATO")]
		OriginalAttachmentType,
		// Token: 0x040014BE RID: 5310
		[DisplayName("GWAI", "ORU")]
		OriginalReferenceAttachmentUrl,
		// Token: 0x040014BF RID: 5311
		[DisplayName("GWAI", "ORSU")]
		OriginalReferenceAttachmentServiceUrl,
		// Token: 0x040014C0 RID: 5312
		[DisplayName("GWAI", "ATR")]
		ResultAttachmentType,
		// Token: 0x040014C1 RID: 5313
		[DisplayName("GWAI", "RAC")]
		ResultAttachmentCreation,
		// Token: 0x040014C2 RID: 5314
		[DisplayName("GWAI", "RRU")]
		ResultReferenceAttachmentUrl,
		// Token: 0x040014C3 RID: 5315
		[DisplayName("GWAI", "RRSU")]
		ResultReferenceAttachmentServiceUrl,
		// Token: 0x040014C4 RID: 5316
		[DisplayName("GWAI", "ADP")]
		AttachmentDataProvider,
		// Token: 0x040014C5 RID: 5317
		[DisplayName("GWAI", "ESID")]
		ExchangeSessionId
	}
}
