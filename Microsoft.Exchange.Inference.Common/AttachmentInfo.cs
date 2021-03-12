using System;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000002 RID: 2
	internal class AttachmentInfo
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AttachmentInfo(AttachmentType attachmentType, string contentType, string fileExtension, bool isInline, long size)
		{
			this.AttachmentType = attachmentType;
			this.ContentType = contentType;
			this.FileExtension = fileExtension;
			this.IsInline = isInline;
			this.Size = size;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020FD File Offset: 0x000002FD
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002105 File Offset: 0x00000305
		public AttachmentType AttachmentType { get; protected set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000210E File Offset: 0x0000030E
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002116 File Offset: 0x00000316
		public string ContentType { get; protected set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000211F File Offset: 0x0000031F
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002127 File Offset: 0x00000327
		public string FileExtension { get; protected set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002138 File Offset: 0x00000338
		public bool IsInline { get; protected set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002141 File Offset: 0x00000341
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002149 File Offset: 0x00000349
		public long Size { get; protected set; }
	}
}
