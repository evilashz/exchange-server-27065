using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005FE RID: 1534
	internal enum MimePartContentType
	{
		// Token: 0x040022AF RID: 8879
		TextPlain,
		// Token: 0x040022B0 RID: 8880
		TextHtml,
		// Token: 0x040022B1 RID: 8881
		TextEnriched,
		// Token: 0x040022B2 RID: 8882
		Tnef,
		// Token: 0x040022B3 RID: 8883
		Calendar,
		// Token: 0x040022B4 RID: 8884
		DsnReportBody,
		// Token: 0x040022B5 RID: 8885
		MdnReportBody,
		// Token: 0x040022B6 RID: 8886
		Attachment,
		// Token: 0x040022B7 RID: 8887
		ItemAttachment,
		// Token: 0x040022B8 RID: 8888
		AppleFile,
		// Token: 0x040022B9 RID: 8889
		JournalReportBody,
		// Token: 0x040022BA RID: 8890
		JournalReportMsg,
		// Token: 0x040022BB RID: 8891
		JournalReportTnef,
		// Token: 0x040022BC RID: 8892
		FirstMultipartType,
		// Token: 0x040022BD RID: 8893
		MultipartAlternative = 13,
		// Token: 0x040022BE RID: 8894
		MultipartRelated,
		// Token: 0x040022BF RID: 8895
		MultipartMixed,
		// Token: 0x040022C0 RID: 8896
		MultipartReportDsn,
		// Token: 0x040022C1 RID: 8897
		MultipartReportMdn,
		// Token: 0x040022C2 RID: 8898
		MultipartDigest,
		// Token: 0x040022C3 RID: 8899
		MultipartFormData,
		// Token: 0x040022C4 RID: 8900
		MultipartParallel,
		// Token: 0x040022C5 RID: 8901
		MultipartAppleDouble
	}
}
