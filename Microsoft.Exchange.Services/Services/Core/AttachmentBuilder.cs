using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A1 RID: 673
	internal sealed class AttachmentBuilder : IDisposeTrackable, IDisposable
	{
		// Token: 0x060011E6 RID: 4582 RVA: 0x00057640 File Offset: 0x00055840
		static AttachmentBuilder()
		{
			AttachmentBuilder.storeObjectTypeMapper.Add(typeof(ItemType), StoreObjectType.Note);
			AttachmentBuilder.storeObjectTypeMapper.Add(typeof(MessageType), StoreObjectType.Message);
			AttachmentBuilder.storeObjectTypeMapper.Add(typeof(EwsCalendarItemType), StoreObjectType.CalendarItem);
			AttachmentBuilder.storeObjectTypeMapper.Add(typeof(ContactItemType), StoreObjectType.Contact);
			AttachmentBuilder.storeObjectTypeMapper.Add(typeof(TaskType), StoreObjectType.Task);
			AttachmentBuilder.storeObjectTypeMapper.Add(typeof(PostItemType), StoreObjectType.Post);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000576DB File Offset: 0x000558DB
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AttachmentBuilder>(this);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000576E3 File Offset: 0x000558E3
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000576F8 File Offset: 0x000558F8
		public AttachmentBuilder(AttachmentHierarchy attachments, AttachmentType[] attachmentTypes, IdConverter idConverter) : this(attachments, attachmentTypes, idConverter, false)
		{
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00057704 File Offset: 0x00055904
		public AttachmentBuilder(AttachmentHierarchy attachments, AttachmentType[] attachmentTypes, IdConverter idConverter, bool clientSupportsIrm)
		{
			this.attachmentTypes = attachmentTypes;
			this.attachments = attachments;
			this.idConverter = idConverter;
			this.clientSupportsIrm = clientSupportsIrm;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00057735 File Offset: 0x00055935
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00057758 File Offset: 0x00055958
		private static ExceptionPropertyUriEnum GetPropertyUri(string elementName)
		{
			ExceptionPropertyUriEnum result = ExceptionPropertyUriEnum.Month;
			if (elementName != null)
			{
				if (!(elementName == "Content"))
				{
					if (!(elementName == "Name"))
					{
						if (elementName == "ContentType")
						{
							result = ExceptionPropertyUriEnum.ContentType;
						}
					}
					else
					{
						result = ExceptionPropertyUriEnum.AttachmentName;
					}
				}
				else
				{
					result = ExceptionPropertyUriEnum.Content;
				}
			}
			return result;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000577A0 File Offset: 0x000559A0
		private static void SetStandardAttachmentProperties(Attachment attachment, AttachmentType attachmentType)
		{
			if (string.IsNullOrEmpty(attachmentType.Name))
			{
				throw new RequiredPropertyMissingException(new ExceptionPropertyUri(AttachmentBuilder.GetPropertyUri("Name")));
			}
			attachment.FileName = attachmentType.Name;
			if (!string.IsNullOrEmpty(attachmentType.ContentType))
			{
				attachment.ContentType = attachmentType.ContentType;
			}
			if (!string.IsNullOrEmpty(attachmentType.ContentId))
			{
				attachment[AttachmentSchema.AttachContentId] = attachmentType.ContentId;
			}
			if (!string.IsNullOrEmpty(attachmentType.ContentLocation))
			{
				attachment[AttachmentSchema.AttachContentLocation] = attachmentType.ContentLocation;
			}
			attachment.IsInline = attachmentType.IsInline;
			if (attachmentType is FileAttachmentType)
			{
				bool isContactPhoto = (attachmentType as FileAttachmentType).IsContactPhoto;
				attachment[AttachmentSchema.IsContactPhoto] = isContactPhoto;
				if (isContactPhoto)
				{
					attachment[AttachmentSchema.DisplayName] = "ContactPicture.jpg";
					attachment.FileName = "ContactPicture.jpg";
				}
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00057880 File Offset: 0x00055A80
		public void CreateAllAttachments()
		{
			foreach (AttachmentType attachmentType in this.attachmentTypes)
			{
				ServiceError serviceError;
				using (this.CreateAttachment(attachmentType, out serviceError))
				{
				}
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000578D4 File Offset: 0x00055AD4
		public Attachment CreateAttachment(AttachmentType attachmentType, out ServiceError warning)
		{
			warning = null;
			CalendarItemOccurrence calendarItemOccurrence = this.attachments.LastAsXsoItem as CalendarItemOccurrence;
			if (calendarItemOccurrence != null && calendarItemOccurrence.CalendarItemType != CalendarItemType.Exception)
			{
				calendarItemOccurrence.MakeModifiedOccurrence();
			}
			if (string.Compare(attachmentType.ContentType, "text/html", StringComparison.OrdinalIgnoreCase) == 0)
			{
				attachmentType.IsInline = false;
			}
			Type type = attachmentType.GetType();
			if (type == typeof(FileAttachmentType))
			{
				FileAttachmentType fileAttachmentType = attachmentType as FileAttachmentType;
				if (fileAttachmentType.IsContactPhoto)
				{
					this.DeleteExistingContactPhotoAttachments();
				}
				this.attachments.RootItem[ContactSchema.HasPicturePropertyDef] = true;
				return this.CreateFileAttachment(fileAttachmentType);
			}
			if (type == typeof(ReferenceAttachmentType))
			{
				return this.CreateReferenceAttachment(attachmentType as ReferenceAttachmentType);
			}
			if (type == typeof(ItemIdAttachmentType))
			{
				return this.CreateItemAttachment(attachmentType as ItemIdAttachmentType);
			}
			return this.CreateItemAttachment(attachmentType as ItemAttachmentType, out warning);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000579C0 File Offset: 0x00055BC0
		private void DeleteExistingContactPhotoAttachments()
		{
			List<AttachmentId> list = new List<AttachmentId>();
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(this.attachments.RootItem);
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					if (attachment.IsContactPhoto)
					{
						list.Add(attachment.Id);
					}
				}
			}
			foreach (AttachmentId attachmentId in list)
			{
				attachmentCollection.Remove(attachmentId);
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00057A94 File Offset: 0x00055C94
		private Attachment CreateFileAttachment(FileAttachmentType fileAttachmentType)
		{
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(this.attachments.LastAsXsoItem);
			StreamAttachment streamAttachment = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				streamAttachment = (attachmentCollection.Create(AttachmentType.Stream) as StreamAttachment);
				disposeGuard.Add<StreamAttachment>(streamAttachment);
				AttachmentBuilder.SetStandardAttachmentProperties(streamAttachment, fileAttachmentType);
				byte[] content = fileAttachmentType.Content;
				if (content == null)
				{
					throw new RequiredPropertyMissingException(new ExceptionPropertyUri(AttachmentBuilder.GetPropertyUri("Content")));
				}
				if (content.Length > 2147483647)
				{
					throw new AttachmentSizeLimitExceededException();
				}
				using (Stream contentStream = streamAttachment.GetContentStream())
				{
					contentStream.Write(content, 0, content.Length);
				}
				streamAttachment.Save();
				disposeGuard.Success();
			}
			return streamAttachment;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00057B64 File Offset: 0x00055D64
		private Attachment CreateItemAttachment(ItemAttachmentType itemAttachmentType, out ServiceError warning)
		{
			warning = null;
			ServiceObject item = itemAttachmentType.Item;
			if (item == null)
			{
				throw new MissingItemForCreateItemAttachmentException();
			}
			StoreObjectType type;
			try
			{
				type = AttachmentBuilder.storeObjectTypeMapper[item.GetType()];
			}
			catch (KeyNotFoundException)
			{
				throw new InvalidItemForOperationException("CreateItemAttachment");
			}
			if ((item is EwsCalendarItemType || item is TaskType) && !ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				TimeZoneType meetingTimeZone = item.PropertyBag.Contains(CalendarItemSchema.MeetingTimeZone) ? (item.PropertyBag[CalendarItemSchema.MeetingTimeZone] as TimeZoneType) : null;
				this.attachments.RootItem.Session.ExTimeZone = RecurrenceHelper.MeetingTimeZone.GetMeetingTimeZone(meetingTimeZone, out warning);
			}
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(this.attachments.LastAsXsoItem);
			ItemAttachment itemAttachment = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				itemAttachment = attachmentCollection.Create(type);
				disposeGuard.Add<ItemAttachment>(itemAttachment);
				AttachmentBuilder.SetStandardAttachmentProperties(itemAttachment, itemAttachmentType);
				using (Item item2 = itemAttachment.GetItem())
				{
					if (item.LoadedProperties.Count > 0)
					{
						string className = item2.ClassName;
						XsoDataConverter.SetProperties(item2, item, this.idConverter);
						ServiceCommandBase.ValidateClassChange(item2, className);
					}
					item2.Save(SaveMode.NoConflictResolution);
				}
				itemAttachment.Save();
				disposeGuard.Success();
			}
			return itemAttachment;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00057CD8 File Offset: 0x00055ED8
		private Attachment CreateItemAttachment(ItemIdAttachmentType itemIdAttachmentType)
		{
			IdAndSession idAndSession;
			if (itemIdAttachmentType.ItemId != null)
			{
				idAndSession = this.idConverter.ConvertItemIdToIdAndSessionReadOnly(itemIdAttachmentType.ItemId);
			}
			else
			{
				if (string.IsNullOrEmpty(itemIdAttachmentType.AttachmentIdToAttach))
				{
					throw new MissingItemIdForCreateItemAttachmentException();
				}
				idAndSession = this.idConverter.ConvertAttachmentIdToIdAndSessionReadOnly(new AttachmentIdType(itemIdAttachmentType.AttachmentIdToAttach));
			}
			Item lastAsXsoItem = this.attachments.LastAsXsoItem;
			if (idAndSession.Id.Equals(lastAsXsoItem.Id) || idAndSession.Id.Equals(lastAsXsoItem.Id.ObjectId))
			{
				throw new CannotAttachSelfException();
			}
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(this.attachments.LastAsXsoItem);
			if (itemIdAttachmentType.ItemId != null)
			{
				using (Item rootXsoItem = idAndSession.GetRootXsoItem(null))
				{
					ItemAttachment itemAttachment = null;
					using (DisposeGuard disposeGuard = default(DisposeGuard))
					{
						itemAttachment = attachmentCollection.AddExistingItem(rootXsoItem);
						disposeGuard.Add<ItemAttachment>(itemAttachment);
						itemAttachment[AttachmentSchema.DisplayName] = itemIdAttachmentType.Name;
						itemAttachment.Save();
						disposeGuard.Success();
					}
					return itemAttachment;
				}
			}
			Attachment result;
			using (AttachmentHierarchy attachmentHierarchy = new AttachmentHierarchy(idAndSession, false, this.clientSupportsIrm))
			{
				Attachment attachment = attachmentHierarchy.Last.Attachment;
				Attachment attachment2 = null;
				using (DisposeGuard disposeGuard2 = default(DisposeGuard))
				{
					attachment2 = attachmentCollection.Create(new AttachmentType?(attachment.AttachmentType), attachment);
					disposeGuard2.Add<Attachment>(attachment2);
					attachment2.Save();
					disposeGuard2.Success();
				}
				result = attachment2;
			}
			return result;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00057E9C File Offset: 0x0005609C
		private Attachment CreateReferenceAttachment(ReferenceAttachmentType referenceAttachmentType)
		{
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(this.attachments.LastAsXsoItem);
			ReferenceAttachment referenceAttachment = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				referenceAttachment = (attachmentCollection.Create(AttachmentType.Reference) as ReferenceAttachment);
				disposeGuard.Add<ReferenceAttachment>(referenceAttachment);
				AttachmentBuilder.SetStandardAttachmentProperties(referenceAttachment, referenceAttachmentType);
				string attachLongPathName = referenceAttachmentType.AttachLongPathName;
				if (string.IsNullOrWhiteSpace(attachLongPathName))
				{
					throw new RequiredPropertyMissingException(new ExceptionPropertyUri(AttachmentBuilder.GetPropertyUri("AttachLongPathName")));
				}
				string providerEndpointUrl = referenceAttachmentType.ProviderEndpointUrl;
				if (string.IsNullOrWhiteSpace(providerEndpointUrl))
				{
					throw new RequiredPropertyMissingException(new ExceptionPropertyUri(AttachmentBuilder.GetPropertyUri("ProviderEndpointUrl")));
				}
				string providerType = referenceAttachmentType.ProviderType;
				if (string.IsNullOrWhiteSpace(providerType))
				{
					throw new RequiredPropertyMissingException(new ExceptionPropertyUri(AttachmentBuilder.GetPropertyUri("ProviderType")));
				}
				referenceAttachment[AttachmentSchema.AttachLongPathName] = attachLongPathName;
				referenceAttachment[AttachmentSchema.AttachmentProviderEndpointUrl] = providerEndpointUrl;
				referenceAttachment[AttachmentSchema.AttachmentProviderType] = providerType;
				referenceAttachment.Save();
				disposeGuard.Success();
			}
			return referenceAttachment;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00057FA8 File Offset: 0x000561A8
		private void Dispose(bool fromDispose)
		{
			if (this.attachments != null)
			{
				this.attachments.Dispose();
				this.attachments = null;
			}
		}

		// Token: 0x04000CD9 RID: 3289
		private const string ContactPictureName = "ContactPicture.jpg";

		// Token: 0x04000CDA RID: 3290
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000CDB RID: 3291
		private readonly IdConverter idConverter;

		// Token: 0x04000CDC RID: 3292
		private readonly AttachmentType[] attachmentTypes;

		// Token: 0x04000CDD RID: 3293
		private readonly bool clientSupportsIrm;

		// Token: 0x04000CDE RID: 3294
		private static Dictionary<Type, StoreObjectType> storeObjectTypeMapper = new Dictionary<Type, StoreObjectType>();

		// Token: 0x04000CDF RID: 3295
		private AttachmentHierarchy attachments;
	}
}
