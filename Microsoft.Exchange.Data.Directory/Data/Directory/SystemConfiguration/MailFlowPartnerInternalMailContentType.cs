using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A9 RID: 1193
	public enum MailFlowPartnerInternalMailContentType
	{
		// Token: 0x040024DB RID: 9435
		[LocDescription(DirectoryStrings.IDs.MailFlowPartnerInternalMailContentTypeNone)]
		None,
		// Token: 0x040024DC RID: 9436
		[LocDescription(DirectoryStrings.IDs.MailFlowPartnerInternalMailContentTypeTNEF)]
		TNEF,
		// Token: 0x040024DD RID: 9437
		[LocDescription(DirectoryStrings.IDs.MailFlowPartnerInternalMailContentTypeMimeHtmlText)]
		MimeHtmlText,
		// Token: 0x040024DE RID: 9438
		[LocDescription(DirectoryStrings.IDs.MailFlowPartnerInternalMailContentTypeMimeText)]
		MimeText,
		// Token: 0x040024DF RID: 9439
		[LocDescription(DirectoryStrings.IDs.MailFlowPartnerInternalMailContentTypeMimeHtml)]
		MimeHtml
	}
}
