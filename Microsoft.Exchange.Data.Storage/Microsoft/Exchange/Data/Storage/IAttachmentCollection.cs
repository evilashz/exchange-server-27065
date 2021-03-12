using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002BE RID: 702
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAttachmentCollection : IEnumerable<AttachmentHandle>, IEnumerable
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001D68 RID: 7528
		int Count { get; }

		// Token: 0x06001D69 RID: 7529
		bool Remove(AttachmentId attachmentId);

		// Token: 0x06001D6A RID: 7530
		bool Remove(AttachmentHandle handle);

		// Token: 0x06001D6B RID: 7531
		IAttachment CreateIAttachment(AttachmentType type);

		// Token: 0x06001D6C RID: 7532
		IAttachment CreateIAttachment(AttachmentType type, IAttachment attachment);

		// Token: 0x06001D6D RID: 7533
		IAttachment OpenIAttachment(AttachmentHandle handle);

		// Token: 0x06001D6E RID: 7534
		IList<AttachmentHandle> GetHandles();
	}
}
