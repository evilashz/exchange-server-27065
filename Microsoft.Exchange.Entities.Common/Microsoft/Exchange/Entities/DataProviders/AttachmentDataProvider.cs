using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x0200000E RID: 14
	internal class AttachmentDataProvider : StorageDataProvider<IStoreSession, IAttachment, string>
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000027BF File Offset: 0x000009BF
		public AttachmentDataProvider(IStorageEntitySetScope<IStoreSession> scope, StoreId parentItemId) : base(scope, ExTraceGlobals.AttachmentDataProviderTracer)
		{
			this.parentItemId = parentItemId;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027D4 File Offset: 0x000009D4
		public override IAttachment Create(IAttachment entity)
		{
			return this.Create(entity, null);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027E0 File Offset: 0x000009E0
		public IAttachment Create(IAttachment entity, CommandContext commandContext)
		{
			IAttachment result;
			using (IItem item = this.BindToParentItem())
			{
				item.OpenAsReadWrite();
				AttachmentType attachmentType;
				if (entity is ItemIdAttachment)
				{
					attachmentType = AttachmentType.EmbeddedMessage;
				}
				else if (entity is ReferenceAttachment)
				{
					attachmentType = AttachmentType.Reference;
				}
				else
				{
					attachmentType = AttachmentType.Stream;
				}
				using (IAttachment attachment = this.CreateAttachment(item, entity, attachmentType))
				{
					attachment.Save();
					attachment.Load();
					this.SaveParentItem(item, commandContext);
					item.Load();
					StorageTranslator<IAttachment, IAttachment> attachmentTranslator = this.GetAttachmentTranslator(attachment.AttachmentType, true);
					IAttachment attachment2 = attachmentTranslator.ConvertToEntity(attachment);
					result = attachment2;
				}
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002888 File Offset: 0x00000A88
		public override void Delete(string attachmentId, DeleteItemFlags flags)
		{
			this.Delete(attachmentId, null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002894 File Offset: 0x00000A94
		public void Delete(string attachmentId, CommandContext commandContext = null)
		{
			IList<AttachmentId> attachmentIds = IdConverter.GetAttachmentIds(attachmentId);
			if (attachmentIds.Count > 1)
			{
				throw new InvalidRequestException(Strings.ErrorNestedAttachmentsCannotBeRemoved);
			}
			using (IItem item = this.BindToParentItem())
			{
				item.OpenAsReadWrite();
				this.GetAttachmentCollection(item).Remove(attachmentIds[0]);
				this.SaveParentItem(item, commandContext);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public IEnumerable<IAttachment> GetAllAttachments()
		{
			using (IItem parentItem = this.BindToParentItem())
			{
				AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(parentItem);
				foreach (AttachmentHandle attachmentHandle in attachmentCollection.GetHandles())
				{
					using (Attachment attachment = attachmentCollection.Open(attachmentHandle))
					{
						StorageTranslator<IAttachment, IAttachment> translator = this.GetAttachmentTranslator(attachment.AttachmentType, false);
						IAttachment resultAttachment = translator.ConvertToEntity(attachment);
						yield return resultAttachment;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public override IAttachment Read(string attachmentId)
		{
			IList<AttachmentId> attachmentIds = IdConverter.GetAttachmentIds(attachmentId);
			IAttachment result;
			using (IItem item = this.BindToParentItem())
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					IItem item2 = item;
					Attachment attachment = null;
					for (int i = 0; i < attachmentIds.Count; i++)
					{
						attachment = IrmUtils.GetAttachmentCollection(item2).Open(attachmentIds[i]);
						disposeGuard.Add<Attachment>(attachment);
						if (i < attachmentIds.Count - 1)
						{
							if (!(attachment is ItemAttachment))
							{
								throw new CorruptDataException(Strings.ErrorAllButLastNestedAttachmentMustBeItemAttachment);
							}
							ItemAttachment itemAttachment = attachment as ItemAttachment;
							item2 = itemAttachment.GetItem();
							disposeGuard.Add<IItem>(item2);
						}
					}
					StorageTranslator<IAttachment, IAttachment> attachmentTranslator = this.GetAttachmentTranslator(attachment.AttachmentType, false);
					IAttachment attachment2 = attachmentTranslator.ConvertToEntity(attachment);
					attachment2.Id = attachmentId;
					result = attachment2;
				}
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CDC File Offset: 0x00000EDC
		public void SaveParentItem(IItem parentItem, CommandContext commandContext)
		{
			this.BeforeParentItemSave(parentItem);
			SaveMode saveMode = base.GetSaveMode(null, commandContext);
			ConflictResolutionResult result = parentItem.Save(saveMode);
			result.ThrowOnIrresolvableConflict();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D07 File Offset: 0x00000F07
		public override IAttachment Update(IAttachment entity, CommandContext commandContext)
		{
			throw new NotSupportedException(Strings.ErrorUnsupportedOperation("Update"));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D1D File Offset: 0x00000F1D
		protected virtual void BeforeParentItemSave(IItem parentItem)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D1F File Offset: 0x00000F1F
		protected virtual IItem BindToParentItem()
		{
			return this.BindToItem(this.parentItemId);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D2D File Offset: 0x00000F2D
		protected virtual AttachmentCollection GetAttachmentCollection(IItem parentItem)
		{
			return IrmUtils.GetAttachmentCollection(parentItem);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D38 File Offset: 0x00000F38
		private IAttachment CreateAttachment(IItem parentItem, IAttachment entity, AttachmentType attachmentType)
		{
			ItemIdAttachment itemIdAttachment = entity as ItemIdAttachment;
			AttachmentCollection attachmentCollection = this.GetAttachmentCollection(parentItem);
			IAttachment attachment;
			StorageTranslator<IAttachment, IAttachment> storageTranslator;
			if (itemIdAttachment != null)
			{
				using (IItem item = this.BindToItem(base.IdConverter.ToStoreObjectId(itemIdAttachment.ItemToAttachId)))
				{
					attachment = attachmentCollection.AddExistingItem(item);
					storageTranslator = AttachmentTranslator<ItemIdAttachment, ItemIdAttachmentSchema>.MetadataInstance;
					goto IL_5E;
				}
			}
			attachment = attachmentCollection.CreateIAttachment(attachmentType);
			storageTranslator = this.GetAttachmentTranslator(attachment.AttachmentType, false);
			IL_5E:
			storageTranslator.SetPropertiesFromEntityOnStorageObject(entity, attachment);
			return attachment;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DBC File Offset: 0x00000FBC
		private StorageTranslator<IAttachment, IAttachment> GetAttachmentTranslator(AttachmentType attachmentType, bool metadataOnly = false)
		{
			switch (attachmentType)
			{
			case AttachmentType.EmbeddedMessage:
				return AttachmentTranslator<ItemAttachment, ItemAttachmentSchema>.GetTranslatorInstance(metadataOnly);
			case AttachmentType.Reference:
				return AttachmentTranslator<ReferenceAttachment, ReferenceAttachmentSchema>.GetTranslatorInstance(metadataOnly);
			}
			return AttachmentTranslator<FileAttachment, FileAttachmentSchema>.GetTranslatorInstance(metadataOnly);
		}

		// Token: 0x04000026 RID: 38
		private readonly StoreId parentItemId;
	}
}
