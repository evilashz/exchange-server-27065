using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200012E RID: 302
	internal class InlineAttachmentInfo
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x00045688 File Offset: 0x00043888
		public InlineAttachmentInfo()
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00045690 File Offset: 0x00043890
		public InlineAttachmentInfo(AttachmentId id, string contentId, int renderingPosition, bool isInline)
		{
			this.Id = id;
			this.ContentId = contentId;
			this.RenderingPosition = new int?(renderingPosition);
			this.IsInline = new bool?(isInline);
		}

		// Token: 0x04000767 RID: 1895
		public AttachmentId Id;

		// Token: 0x04000768 RID: 1896
		public string ContentId;

		// Token: 0x04000769 RID: 1897
		public int? RenderingPosition;

		// Token: 0x0400076A RID: 1898
		public bool? IsInline;
	}
}
