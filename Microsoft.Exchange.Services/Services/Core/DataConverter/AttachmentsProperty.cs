using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000151 RID: 337
	internal class AttachmentsProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
		public AttachmentsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002D201 File Offset: 0x0002B401
		public static AttachmentsProperty CreateCommand(CommandContext commandContext)
		{
			return new AttachmentsProperty(commandContext);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002D209 File Offset: 0x0002B409
		public void ToXml()
		{
			throw new InvalidOperationException("AttachmentProperty.ToXml should not be called");
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0002D215 File Offset: 0x0002B415
		public virtual void ToServiceObject()
		{
			this.InternalToServiceObject(false);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002D220 File Offset: 0x0002B420
		protected void InternalToServiceObject(bool idsOnly)
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			IdAndSession idAndSession = commandSettings.IdAndSession;
			this.item = (Item)commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			if (EWSSettings.AttachmentInformation == null)
			{
				EWSSettings.AttachmentInformation = new Dictionary<AttachmentId, AttachmentType>();
			}
			EWSSettings.AttachmentInformation.Clear();
			this.SetAttachmentsPropertyForServiceObject(this.item, idAndSession, serviceObject, idsOnly);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002D280 File Offset: 0x0002B480
		private bool SetAttachmentsPropertyForServiceObject(Item item, IdAndSession idAndSession, ServiceObject serviceObject, bool idsOnly)
		{
			List<AttachmentType> list = new List<AttachmentType>();
			AttachmentCollection effectiveAttachmentCollection = Util.GetEffectiveAttachmentCollection(item, false);
			foreach (AttachmentHandle handle in effectiveAttachmentCollection)
			{
				using (Attachment attachment = effectiveAttachmentCollection.Open(handle))
				{
					list.Add(this.CreateAttachmentType(idAndSession, attachment, idsOnly));
				}
			}
			if (list.Count > 0)
			{
				serviceObject[this.commandContext.PropertyInformation] = list.ToArray();
				return true;
			}
			return false;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002D328 File Offset: 0x0002B528
		private AttachmentType CreateAttachmentType(IdAndSession idAndSession, Attachment attachment, bool idsOnly)
		{
			AttachmentType attachmentType = null;
			if (attachment is ItemAttachment)
			{
				attachmentType = new ItemAttachmentType();
			}
			else if (attachment is ReferenceAttachment && ReferenceAttachmentType.IsReferenceAttachmentSupported())
			{
				attachmentType = new ReferenceAttachmentType();
			}
			else
			{
				attachmentType = new FileAttachmentType();
			}
			ExTraceGlobals.GetItemCallTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "[AttachmentProperty.CreateAttachmentType] IsRefAttachment: {0}, CalculatedAttachmentTypeIsRefAttachmentType: {1}", attachment is ReferenceAttachment, attachmentType is ReferenceAttachmentType);
			if (attachment.Id != null)
			{
				EWSSettings.AttachmentInformation[attachment.Id] = attachmentType;
			}
			if (!PropertyCommand.InMemoryProcessOnly)
			{
				IdAndSession idAndSession2;
				if (idAndSession == null)
				{
					idAndSession2 = new IdAndSession(this.item.Id, this.item.Session);
				}
				else
				{
					idAndSession2 = idAndSession.Clone();
				}
				idAndSession2.AttachmentIds.Add(attachment.Id);
				attachmentType.AttachmentId = new AttachmentIdType(idAndSession2.GetConcatenatedId().Id);
				if (idsOnly)
				{
					ItemAttachment itemAttachment = attachment as ItemAttachment;
					if (itemAttachment != null)
					{
						using (Item item = itemAttachment.GetItem())
						{
							StoreObjectType objectType = ObjectClass.GetObjectType(item.ClassName);
							ItemType serviceObject = ItemType.CreateFromStoreObjectType(objectType);
							if (this.SetAttachmentsPropertyForServiceObject(item, idAndSession2, serviceObject, true))
							{
								(attachmentType as ItemAttachmentType).Item = serviceObject;
							}
						}
					}
					return attachmentType;
				}
			}
			string text = attachment.DisplayName;
			if (string.IsNullOrEmpty(text))
			{
				text = attachment.FileName;
			}
			attachmentType.Name = text;
			if (!string.IsNullOrEmpty(attachment.ContentType))
			{
				attachmentType.ContentType = attachment.ContentType;
			}
			if (!string.IsNullOrEmpty(attachment.ContentId))
			{
				attachmentType.ContentId = attachment.ContentId;
			}
			if (attachment.ContentLocation != null)
			{
				attachmentType.ContentLocation = attachment.ContentLocation.ToString();
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				attachmentType.Size = (int)attachment.Size;
				attachmentType.LastModifiedTime = attachment.LastModifiedTime.ToString("s");
				attachmentType.IsInline = attachment.IsInline;
				if (attachment is StreamAttachment && attachmentType is FileAttachmentType)
				{
					((FileAttachmentType)attachmentType).IsContactPhoto = attachment.IsContactPhoto;
				}
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013))
			{
				StreamAttachment streamAttachment = attachment as StreamAttachment;
				if (streamAttachment != null)
				{
					byte[] array = streamAttachment.LoadAttachmentThumbnail();
					if (array != null && array.Length > 0)
					{
						attachmentType.ThumbnailMimeType = "image/jpeg";
						attachmentType.Thumbnail = Convert.ToBase64String(array);
						FileAttachmentType fileAttachmentType = attachmentType as FileAttachmentType;
						if (fileAttachmentType != null)
						{
							fileAttachmentType.ImageThumbnailHeight = streamAttachment.ImageThumbnailHeight;
							fileAttachmentType.ImageThumbnailWidth = streamAttachment.ImageThumbnailWidth;
							fileAttachmentType.ImageThumbnailSalientRegions = streamAttachment.LoadAttachmentThumbnailSalientRegions();
						}
					}
				}
				ItemAttachment itemAttachment2 = attachment as ItemAttachment;
				if (itemAttachment2 != null)
				{
					using (Item item2 = itemAttachment2.GetItem())
					{
						(attachmentType as ItemAttachmentType).EmbeddedItemClass = item2.ClassName;
					}
				}
			}
			if (attachment is ReferenceAttachment && ReferenceAttachmentType.IsReferenceAttachmentSupported())
			{
				ReferenceAttachmentType referenceAttachmentType = attachmentType as ReferenceAttachmentType;
				ReferenceAttachment referenceAttachment = attachment as ReferenceAttachment;
				referenceAttachmentType.AttachLongPathName = referenceAttachment.AttachLongPathName;
				referenceAttachmentType.ProviderType = referenceAttachment.ProviderType;
			}
			return attachmentType;
		}

		// Token: 0x0400077D RID: 1917
		private Item item;
	}
}
