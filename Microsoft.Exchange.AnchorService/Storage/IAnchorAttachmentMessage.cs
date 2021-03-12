using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorAttachmentMessage
	{
		// Token: 0x060001EE RID: 494
		AnchorAttachment CreateAttachment(string name);

		// Token: 0x060001EF RID: 495
		AnchorAttachment GetAttachment(string name, PropertyOpenMode openMode);

		// Token: 0x060001F0 RID: 496
		void DeleteAttachment(string name);
	}
}
