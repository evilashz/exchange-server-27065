using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.TypeConversion.Translators
{
	// Token: 0x02000082 RID: 130
	internal class AttachmentTranslator<TAttachment, TSchema> : StorageTranslator<IAttachment, IAttachment> where TAttachment : Attachment<TSchema>, new() where TSchema : AttachmentSchema, new()
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00009821 File Offset: 0x00007A21
		protected AttachmentTranslator(bool includeContentProperties, IEnumerable<ITranslationRule<IAttachment, IAttachment>> additionalRules = null) : base(AttachmentTranslator<TAttachment, TSchema>.CreateTranslationRules(includeContentProperties).AddRules(additionalRules))
		{
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009835 File Offset: 0x00007A35
		public static AttachmentTranslator<TAttachment, TSchema> ContentInstance
		{
			get
			{
				return AttachmentTranslator<TAttachment, TSchema>.SingletonContentInstance;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000983C File Offset: 0x00007A3C
		public static AttachmentTranslator<TAttachment, TSchema> MetadataInstance
		{
			get
			{
				return AttachmentTranslator<TAttachment, TSchema>.SingletonMetadataInstance;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00009843 File Offset: 0x00007A43
		public static AttachmentTranslator<TAttachment, TSchema> GetTranslatorInstance(bool metadataOnly = false)
		{
			if (!metadataOnly)
			{
				return AttachmentTranslator<TAttachment, TSchema>.ContentInstance;
			}
			return AttachmentTranslator<TAttachment, TSchema>.MetadataInstance;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009853 File Offset: 0x00007A53
		protected override IAttachment CreateEntity()
		{
			return Activator.CreateInstance<TAttachment>();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009860 File Offset: 0x00007A60
		private static List<ITranslationRule<IAttachment, IAttachment>> CreateTranslationRules(bool includeContentProperties)
		{
			TranslationStrategy<IAttachment, PropertyDefinition, Attachment<TSchema>> internalRule = new TranslationStrategy<IAttachment, PropertyDefinition, Attachment<TSchema>>(new ITranslationRule<IAttachment, Attachment<TSchema>>[0])
			{
				AttachmentAccessors.ContentType.MapTo(Attachment<TSchema>.Accessors.ContentType),
				AttachmentAccessors.Id.MapTo(Entity<TSchema>.Accessors.Id),
				AttachmentAccessors.IsInline.MapTo(Attachment<TSchema>.Accessors.IsInline),
				AttachmentAccessors.LastModifiedTime.MapTo(Attachment<TSchema>.Accessors.LastModifiedTime),
				AttachmentAccessors.Name.MapTo(Attachment<TSchema>.Accessors.Name),
				AttachmentAccessors.Size.MapTo(Attachment<TSchema>.Accessors.Size)
			};
			TranslationStrategy<IStreamAttachment, PropertyDefinition, FileAttachment> translationStrategy = new TranslationStrategy<IStreamAttachment, PropertyDefinition, FileAttachment>(new ITranslationRule<IStreamAttachment, FileAttachment>[0])
			{
				AttachmentAccessors.ContentId.MapTo(FileAttachment.Accessors.ContentId),
				AttachmentAccessors.ContentLocation.MapTo(FileAttachment.Accessors.ContentLocation)
			};
			if (includeContentProperties)
			{
				translationStrategy.Add(AttachmentAccessors.Content.MapTo(FileAttachment.Accessors.Content));
			}
			TranslationStrategy<IReferenceAttachment, PropertyDefinition, ReferenceAttachment> internalRule2 = new TranslationStrategy<IReferenceAttachment, PropertyDefinition, ReferenceAttachment>(new ITranslationRule<IReferenceAttachment, ReferenceAttachment>[0])
			{
				AttachmentAccessors.PathName.MapTo(ReferenceAttachment.Accessors.PathName),
				AttachmentAccessors.ProviderEndpointUrl.MapTo(ReferenceAttachment.Accessors.ProviderEndpointUrl),
				AttachmentAccessors.ProviderType.MapTo(ReferenceAttachment.Accessors.ProviderType)
			};
			List<ITranslationRule<IAttachment, IAttachment>> list = new List<ITranslationRule<IAttachment, IAttachment>>
			{
				internalRule.OfType<IAttachment, IAttachment, IAttachment, Attachment<TSchema>>(),
				translationStrategy.OfType<IAttachment, IAttachment, IStreamAttachment, FileAttachment>(),
				internalRule2.OfType<IAttachment, IAttachment, IReferenceAttachment, ReferenceAttachment>()
			};
			if (includeContentProperties)
			{
				list.Add(new EmbededItemTranslationRule().OfType<IAttachment, IAttachment, ItemAttachment, ItemAttachment>());
			}
			return list;
		}

		// Token: 0x0400010B RID: 267
		private static readonly AttachmentTranslator<TAttachment, TSchema> SingletonContentInstance = new AttachmentTranslator<TAttachment, TSchema>(true, null);

		// Token: 0x0400010C RID: 268
		private static readonly AttachmentTranslator<TAttachment, TSchema> SingletonMetadataInstance = new AttachmentTranslator<TAttachment, TSchema>(false, null);
	}
}
