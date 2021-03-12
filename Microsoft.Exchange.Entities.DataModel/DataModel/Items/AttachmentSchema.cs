using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000082 RID: 130
	public abstract class AttachmentSchema : EntitySchema
	{
		// Token: 0x0600038B RID: 907 RVA: 0x00006D9D File Offset: 0x00004F9D
		protected AttachmentSchema()
		{
			base.RegisterPropertyDefinition(AttachmentSchema.StaticContentTypeProperty);
			base.RegisterPropertyDefinition(AttachmentSchema.StaticIsInlineProperty);
			base.RegisterPropertyDefinition(AttachmentSchema.StaticLastModifiedTimeProperty);
			base.RegisterPropertyDefinition(AttachmentSchema.StaticNameProperty);
			base.RegisterPropertyDefinition(AttachmentSchema.StaticSizeProperty);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00006DDC File Offset: 0x00004FDC
		public TypedPropertyDefinition<string> ContentTypeProperty
		{
			get
			{
				return AttachmentSchema.StaticContentTypeProperty;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00006DE3 File Offset: 0x00004FE3
		public TypedPropertyDefinition<bool> IsInlineProperty
		{
			get
			{
				return AttachmentSchema.StaticIsInlineProperty;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00006DEA File Offset: 0x00004FEA
		public TypedPropertyDefinition<ExDateTime> LastModifiedTimeProperty
		{
			get
			{
				return AttachmentSchema.StaticLastModifiedTimeProperty;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public TypedPropertyDefinition<long> SizeProperty
		{
			get
			{
				return AttachmentSchema.StaticSizeProperty;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public TypedPropertyDefinition<string> NameProperty
		{
			get
			{
				return AttachmentSchema.StaticNameProperty;
			}
		}

		// Token: 0x040001BA RID: 442
		private static readonly TypedPropertyDefinition<string> StaticContentTypeProperty = new TypedPropertyDefinition<string>("Attachment.ContentType", null, true);

		// Token: 0x040001BB RID: 443
		private static readonly TypedPropertyDefinition<bool> StaticIsInlineProperty = new TypedPropertyDefinition<bool>("Attachment.IsInline", false, true);

		// Token: 0x040001BC RID: 444
		private static readonly TypedPropertyDefinition<ExDateTime> StaticLastModifiedTimeProperty = new TypedPropertyDefinition<ExDateTime>("Attachment.LastModifiedTime", default(ExDateTime), true);

		// Token: 0x040001BD RID: 445
		private static readonly TypedPropertyDefinition<string> StaticNameProperty = new TypedPropertyDefinition<string>("Attachment.Name", null, true);

		// Token: 0x040001BE RID: 446
		private static readonly TypedPropertyDefinition<long> StaticSizeProperty = new TypedPropertyDefinition<long>("Attachment.Size", 0L, true);
	}
}
