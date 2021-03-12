using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003B9 RID: 953
	internal static class ConversationDataConverter
	{
		// Token: 0x06001AB7 RID: 6839 RVA: 0x0009909C File Offset: 0x0009729C
		public static AttachmentType[] GetAttachments(ItemPart itemPart, IdAndSession itemIdAndSession)
		{
			List<AttachmentType> list = null;
			foreach (AttachmentInfo attachmentInfo in itemPart.Attachments)
			{
				if (list == null)
				{
					list = new List<AttachmentType>();
				}
				list.Add(ConversationDataConverter.CreateAttachment(itemIdAndSession, attachmentInfo));
			}
			if (list != null)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00099108 File Offset: 0x00097308
		public static ItemId GetConversationId(ItemPart itemPart, IMailboxSession session, PropertyDefinition conversationIdDefinition)
		{
			ConversationId valueOrDefault = itemPart.StorePropertyBag.GetValueOrDefault<ConversationId>(conversationIdDefinition, null);
			return new ItemId(IdConverter.ConversationIdToEwsId(session.MailboxGuid, valueOrDefault), null);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00099135 File Offset: 0x00097335
		public static ItemId GetConversationId(IMailboxSession session, ConversationId conversationId)
		{
			return new ItemId(IdConverter.ConversationIdToEwsId(session.MailboxGuid, conversationId), null);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0009914C File Offset: 0x0009734C
		public static BaseItemId[] GetItemIds(IMailboxSession session, StoreObjectId[] itemIds)
		{
			MailboxId mailboxId = new MailboxId(session.MailboxGuid);
			BaseItemId[] array = new BaseItemId[itemIds.Length];
			for (int i = 0; i < itemIds.Length; i++)
			{
				array[i] = IdConverter.GetItemIdFromStoreId(itemIds[i], mailboxId);
			}
			return array;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0009918C File Offset: 0x0009738C
		public static StoreObjectId GetStoreObjectId(IStorePropertyBag storePropertyBag)
		{
			VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
			return versionedId.ObjectId;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000991B0 File Offset: 0x000973B0
		private static AttachmentType CreateAttachment(IdAndSession itemIdAndSession, AttachmentInfo attachmentInfo)
		{
			AttachmentType attachmentType;
			if (attachmentInfo.AttachmentType == AttachmentType.EmbeddedMessage)
			{
				attachmentType = new ItemAttachmentType();
			}
			else if (attachmentInfo.AttachmentType == AttachmentType.Reference && ReferenceAttachmentType.IsReferenceAttachmentSupported())
			{
				attachmentType = new ReferenceAttachmentType();
			}
			else
			{
				attachmentType = new FileAttachmentType();
			}
			IdAndSession idAndSession = itemIdAndSession.Clone();
			idAndSession.AttachmentIds.Add(attachmentInfo.AttachmentId);
			attachmentType.AttachmentId = new AttachmentIdType(idAndSession.GetConcatenatedId().Id);
			attachmentType.Name = (string.IsNullOrEmpty(attachmentInfo.DisplayName) ? attachmentInfo.FileName : attachmentInfo.DisplayName);
			if (!string.IsNullOrEmpty(attachmentInfo.ContentType))
			{
				attachmentType.ContentType = attachmentInfo.ContentType;
			}
			if (!string.IsNullOrEmpty(attachmentInfo.ContentId))
			{
				attachmentType.ContentId = attachmentInfo.ContentId;
			}
			if (attachmentInfo.ContentLocation != null)
			{
				attachmentType.ContentLocation = attachmentInfo.ContentLocation.ToString();
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013))
			{
				if (attachmentInfo.ImageThumbnail != null && attachmentInfo.ImageThumbnail.Length > 0)
				{
					attachmentType.Thumbnail = Convert.ToBase64String(attachmentInfo.ImageThumbnail);
					attachmentType.ThumbnailMimeType = "image/jpeg";
					FileAttachmentType fileAttachmentType = attachmentType as FileAttachmentType;
					if (fileAttachmentType != null)
					{
						fileAttachmentType.ImageThumbnailHeight = attachmentInfo.ImageThumbnailHeight;
						fileAttachmentType.ImageThumbnailWidth = attachmentInfo.ImageThumbnailWidth;
						fileAttachmentType.ImageThumbnailSalientRegions = attachmentInfo.ImageThumbnailSalientRegions;
					}
				}
				ItemAttachmentType itemAttachmentType = attachmentType as ItemAttachmentType;
				if (itemAttachmentType != null)
				{
					itemAttachmentType.EmbeddedItemClass = attachmentInfo.EmbeddedItemClass;
				}
			}
			attachmentType.LastModifiedTime = attachmentInfo.LastModifiedTime.ToString("s");
			attachmentType.Size = (int)attachmentInfo.Size;
			attachmentType.IsInline = attachmentInfo.IsInline;
			if (ReferenceAttachmentType.IsReferenceAttachmentSupported())
			{
				ReferenceAttachmentType referenceAttachmentType = attachmentType as ReferenceAttachmentType;
				if (referenceAttachmentType != null)
				{
					if (attachmentInfo.AttachLongPathName != null)
					{
						referenceAttachmentType.AttachLongPathName = attachmentInfo.AttachLongPathName;
					}
					if (attachmentInfo.ProviderType != null)
					{
						referenceAttachmentType.ProviderType = attachmentInfo.ProviderType;
					}
				}
			}
			return attachmentType;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00099388 File Offset: 0x00097588
		public static string GetDatetimeProperty(ItemPart itemPart, PropertyDefinition propDef)
		{
			ExDateTime valueOrDefault = itemPart.StorePropertyBag.GetValueOrDefault<ExDateTime>(propDef, ExDateTime.MaxValue);
			if (valueOrDefault != ExDateTime.MaxValue)
			{
				return ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(valueOrDefault);
			}
			return null;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000993BC File Offset: 0x000975BC
		public static int CompareDateStrings(string xDateTimeString, string yDateTimeString)
		{
			DateTime minValue;
			if (string.IsNullOrEmpty(xDateTimeString) || !DateTime.TryParse(xDateTimeString, out minValue))
			{
				minValue = DateTime.MinValue;
			}
			DateTime minValue2;
			if (string.IsNullOrEmpty(yDateTimeString) || !DateTime.TryParse(yDateTimeString, out minValue2))
			{
				minValue2 = DateTime.MinValue;
			}
			return minValue.CompareTo(minValue2);
		}

		// Token: 0x04001198 RID: 4504
		private const string ThumbnailImageContentType = "image/jpeg";
	}
}
