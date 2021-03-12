using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class DeltaSyncCommon
	{
		// Token: 0x04000255 RID: 597
		internal static readonly string TextXmlContentType = "text/xml";

		// Token: 0x04000256 RID: 598
		internal static readonly string ApplicationXopXmlContentType = "application/xop+xml";

		// Token: 0x04000257 RID: 599
		internal static readonly string DefaultSyncKey = "0";

		// Token: 0x04000258 RID: 600
		internal static readonly string FolderCollectionName = "Folder";

		// Token: 0x04000259 RID: 601
		internal static readonly string EmailCollectionName = "Email";

		// Token: 0x0400025A RID: 602
		internal static readonly string SevenBit = "7bit";

		// Token: 0x0400025B RID: 603
		internal static readonly string Binary = "binary";

		// Token: 0x0400025C RID: 604
		internal static readonly string Charset = "charset";

		// Token: 0x0400025D RID: 605
		internal static readonly string MultipartRelated = "multipart/related";

		// Token: 0x0400025E RID: 606
		internal static readonly string Boundary = "boundary";

		// Token: 0x0400025F RID: 607
		internal static readonly string Type = "type";

		// Token: 0x04000260 RID: 608
		internal static readonly string Start = "start";

		// Token: 0x04000261 RID: 609
		internal static readonly string StartInfo = "start-info";

		// Token: 0x04000262 RID: 610
		internal static readonly string VersionOneDotZero = "1.0";

		// Token: 0x04000263 RID: 611
		internal static readonly string ApplicationRFC822 = "application/rfc822";

		// Token: 0x04000264 RID: 612
		internal static readonly string NormalMessageClass = "IPM.Note";

		// Token: 0x04000265 RID: 613
		internal static readonly string DraftMessageClass = "HM.Note.Draft";

		// Token: 0x04000266 RID: 614
		internal static readonly string DateTimeFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ";

		// Token: 0x04000267 RID: 615
		internal static readonly string DefaultStringCharset = "iso-8859-1";

		// Token: 0x04000268 RID: 616
		internal static readonly int DefaultEncodingVersion = 1;

		// Token: 0x04000269 RID: 617
		internal static readonly string[] SupportedMessageClasses = new string[]
		{
			"ipm.note",
			"hm.note.fax",
			"hm.note.voicemail",
			"hm.schedule.related",
			"ipm.schedule.meeting.request",
			"ipm.schedule.meeting.cancelled",
			"ipm.schedule.meeting.resp.pos",
			"ipm.schedule.meeting.resp.tent",
			"ipm.schedule.meeting.resp.neq",
			"hm.note.draft",
			"hm.note.photomail",
			"hm.msngr.oim.text",
			"hm.msngr.oim.sms",
			"hm.msngr.bubble",
			"hm.note.calllog"
		};
	}
}
