using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CD6 RID: 3286
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StreamAttachmentBaseSchema : AttachmentSchema
	{
		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x060071D0 RID: 29136 RVA: 0x001F82CC File Offset: 0x001F64CC
		public new static StreamAttachmentBaseSchema Instance
		{
			get
			{
				if (StreamAttachmentBaseSchema.instance == null)
				{
					StreamAttachmentBaseSchema.instance = new StreamAttachmentBaseSchema();
				}
				return StreamAttachmentBaseSchema.instance;
			}
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x001F82E4 File Offset: 0x001F64E4
		internal override void CoreObjectUpdate(CoreAttachment coreAttachment)
		{
			base.CoreObjectUpdate(coreAttachment);
			StreamAttachmentBase.CoreObjectUpdateStreamAttachmentName(coreAttachment);
			try
			{
				StreamAttachmentBase.CoreObjectUpdateImageThumbnail(coreAttachment);
			}
			catch (FileNotFoundException)
			{
			}
		}

		// Token: 0x04004F0F RID: 20239
		private static StreamAttachmentBaseSchema instance;
	}
}
