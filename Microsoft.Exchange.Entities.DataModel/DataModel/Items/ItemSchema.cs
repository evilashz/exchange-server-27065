using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200004F RID: 79
	public class ItemSchema : StorageEntitySchema
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public ItemSchema()
		{
			base.RegisterPropertyDefinition(ItemSchema.StaticAttachmentsProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticBodyProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticCategoriesProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticDateTimeCreatedProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticHasAttachmentsProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticImportanceProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticLastModifiedTimeProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticPreviewProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticReceivedTimeProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticSensitivityProperty);
			base.RegisterPropertyDefinition(ItemSchema.StaticSubjectProperty);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00005C60 File Offset: 0x00003E60
		public TypedPropertyDefinition<List<IAttachment>> AttachmentsProperty
		{
			get
			{
				return ItemSchema.StaticAttachmentsProperty;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00005C67 File Offset: 0x00003E67
		public TypedPropertyDefinition<ItemBody> BodyProperty
		{
			get
			{
				return ItemSchema.StaticBodyProperty;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00005C6E File Offset: 0x00003E6E
		public TypedPropertyDefinition<List<string>> CategoriesProperty
		{
			get
			{
				return ItemSchema.StaticCategoriesProperty;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00005C75 File Offset: 0x00003E75
		public TypedPropertyDefinition<ExDateTime> DateTimeCreatedProperty
		{
			get
			{
				return ItemSchema.StaticDateTimeCreatedProperty;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00005C7C File Offset: 0x00003E7C
		public TypedPropertyDefinition<bool> HasAttachmentsProperty
		{
			get
			{
				return ItemSchema.StaticHasAttachmentsProperty;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00005C83 File Offset: 0x00003E83
		public TypedPropertyDefinition<Importance> ImportanceProperty
		{
			get
			{
				return ItemSchema.StaticImportanceProperty;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00005C8A File Offset: 0x00003E8A
		public TypedPropertyDefinition<ExDateTime> LastModifiedTimeProperty
		{
			get
			{
				return ItemSchema.StaticLastModifiedTimeProperty;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00005C91 File Offset: 0x00003E91
		public TypedPropertyDefinition<string> PreviewProperty
		{
			get
			{
				return ItemSchema.StaticPreviewProperty;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00005C98 File Offset: 0x00003E98
		public TypedPropertyDefinition<ExDateTime> ReceivedTimeProperty
		{
			get
			{
				return ItemSchema.StaticReceivedTimeProperty;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00005C9F File Offset: 0x00003E9F
		public TypedPropertyDefinition<Sensitivity> SensitivityProperty
		{
			get
			{
				return ItemSchema.StaticSensitivityProperty;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00005CA6 File Offset: 0x00003EA6
		public TypedPropertyDefinition<string> SubjectProperty
		{
			get
			{
				return ItemSchema.StaticSubjectProperty;
			}
		}

		// Token: 0x04000123 RID: 291
		private static readonly TypedPropertyDefinition<List<IAttachment>> StaticAttachmentsProperty = new TypedPropertyDefinition<List<IAttachment>>("Item.Attachments", null, false);

		// Token: 0x04000124 RID: 292
		private static readonly TypedPropertyDefinition<ItemBody> StaticBodyProperty = new TypedPropertyDefinition<ItemBody>("Item.Body", null, false);

		// Token: 0x04000125 RID: 293
		private static readonly TypedPropertyDefinition<List<string>> StaticCategoriesProperty = new TypedPropertyDefinition<List<string>>("Item.Categories", null, true);

		// Token: 0x04000126 RID: 294
		private static readonly TypedPropertyDefinition<ExDateTime> StaticDateTimeCreatedProperty = new TypedPropertyDefinition<ExDateTime>("Item.DateTimeCreated", default(ExDateTime), true);

		// Token: 0x04000127 RID: 295
		private static readonly TypedPropertyDefinition<bool> StaticHasAttachmentsProperty = new TypedPropertyDefinition<bool>("Item.HasAttachments", false, true);

		// Token: 0x04000128 RID: 296
		private static readonly TypedPropertyDefinition<Importance> StaticImportanceProperty = new TypedPropertyDefinition<Importance>("Item.Importance", Importance.Low, true);

		// Token: 0x04000129 RID: 297
		private static readonly TypedPropertyDefinition<ExDateTime> StaticLastModifiedTimeProperty = new TypedPropertyDefinition<ExDateTime>("Item.LastModifiedTime", default(ExDateTime), true);

		// Token: 0x0400012A RID: 298
		private static readonly TypedPropertyDefinition<string> StaticPreviewProperty = new TypedPropertyDefinition<string>("Item.Preview", null, true);

		// Token: 0x0400012B RID: 299
		private static readonly TypedPropertyDefinition<ExDateTime> StaticReceivedTimeProperty = new TypedPropertyDefinition<ExDateTime>("Item.ReceivedTime", default(ExDateTime), true);

		// Token: 0x0400012C RID: 300
		private static readonly TypedPropertyDefinition<Sensitivity> StaticSensitivityProperty = new TypedPropertyDefinition<Sensitivity>("Item.Sensitivity", Sensitivity.Normal, true);

		// Token: 0x0400012D RID: 301
		private static readonly TypedPropertyDefinition<string> StaticSubjectProperty = new TypedPropertyDefinition<string>("Item.Subject", null, true);
	}
}
