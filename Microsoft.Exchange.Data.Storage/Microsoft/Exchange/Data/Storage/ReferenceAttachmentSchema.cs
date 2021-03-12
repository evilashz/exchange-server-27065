using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CB5 RID: 3253
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReferenceAttachmentSchema : AttachmentSchema
	{
		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x06007140 RID: 28992 RVA: 0x001F662E File Offset: 0x001F482E
		public new static ReferenceAttachmentSchema Instance
		{
			get
			{
				if (ReferenceAttachmentSchema.instance == null)
				{
					ReferenceAttachmentSchema.instance = new ReferenceAttachmentSchema();
				}
				return ReferenceAttachmentSchema.instance;
			}
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x001F6646 File Offset: 0x001F4846
		internal override void CoreObjectUpdate(CoreAttachment coreAttachment)
		{
			base.CoreObjectUpdate(coreAttachment);
			ReferenceAttachment.CoreObjectUpdateReferenceAttachmentName(coreAttachment);
		}

		// Token: 0x04004EA3 RID: 20131
		private static ReferenceAttachmentSchema instance;
	}
}
