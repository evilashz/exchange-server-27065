using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000087 RID: 135
	public sealed class FileAttachmentSchema : AttachmentSchema
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x00007013 File Offset: 0x00005213
		public FileAttachmentSchema()
		{
			base.RegisterPropertyDefinition(FileAttachmentSchema.StaticContentProperty);
			base.RegisterPropertyDefinition(FileAttachmentSchema.StaticContentIdProperty);
			base.RegisterPropertyDefinition(FileAttachmentSchema.StaticContentLocationProperty);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000703C File Offset: 0x0000523C
		public TypedPropertyDefinition<byte[]> ContentProperty
		{
			get
			{
				return FileAttachmentSchema.StaticContentProperty;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00007043 File Offset: 0x00005243
		public TypedPropertyDefinition<string> ContentIdProperty
		{
			get
			{
				return FileAttachmentSchema.StaticContentIdProperty;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000704A File Offset: 0x0000524A
		public TypedPropertyDefinition<string> ContentLocationProperty
		{
			get
			{
				return FileAttachmentSchema.StaticContentLocationProperty;
			}
		}

		// Token: 0x040001CB RID: 459
		private static readonly TypedPropertyDefinition<byte[]> StaticContentProperty = new TypedPropertyDefinition<byte[]>("FileAttachment.Content", null, false);

		// Token: 0x040001CC RID: 460
		private static readonly TypedPropertyDefinition<string> StaticContentIdProperty = new TypedPropertyDefinition<string>("FileAttachment.ContentId", null, true);

		// Token: 0x040001CD RID: 461
		private static readonly TypedPropertyDefinition<string> StaticContentLocationProperty = new TypedPropertyDefinition<string>("FileAttachment.ContentLocation", null, true);
	}
}
