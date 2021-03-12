using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x02000058 RID: 88
	internal class AttachmentConverter : IConverter<IAttachment, IAttachment>
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public IAttachment Convert(IAttachment storageAttachment)
		{
			switch (storageAttachment.AttachmentType)
			{
			case AttachmentType.EmbeddedMessage:
				return AttachmentTranslator<ItemAttachment, ItemAttachmentSchema>.MetadataInstance.ConvertToEntity(storageAttachment);
			case AttachmentType.Reference:
				return AttachmentTranslator<ReferenceAttachment, ReferenceAttachmentSchema>.MetadataInstance.ConvertToEntity(storageAttachment);
			}
			return AttachmentTranslator<FileAttachment, FileAttachmentSchema>.MetadataInstance.ConvertToEntity(storageAttachment);
		}
	}
}
