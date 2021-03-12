using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B57 RID: 2903
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RpMsgToMsgConverter
	{
		// Token: 0x0600693E RID: 26942 RVA: 0x001C3AD8 File Offset: 0x001C1CD8
		public RpMsgToMsgConverter(DrmEmailMessageContainer drmMsgContainer, OrganizationId orgId, bool decryptAttachments)
		{
			Util.ThrowOnNullArgument(drmMsgContainer, "drmMsgContainer");
			Util.ThrowOnNullArgument(orgId, "orgId");
			this.drmMsgContainer = drmMsgContainer;
			this.orgId = orgId;
			this.decryptAttachments = decryptAttachments;
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x001C3B0C File Offset: 0x001C1D0C
		public MessageItem ConvertRpmsgToMsg(MessageItem rightsProtectedMessage, SafeRightsManagementHandle decryptorHandle, string useLicense)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.GetHashCode(), "RpMsgToMsgConverter::ConvertRpmsgToMsg");
			if (string.IsNullOrEmpty(useLicense))
			{
				throw new ArgumentNullException("useLicense");
			}
			this.DecryptMsg(rightsProtectedMessage, useLicense, decryptorHandle);
			if (this.decryptedItem == null)
			{
				throw new RightsManagementPermanentException(RightsManagementFailureCode.UnknownFailure, ServerStrings.GenericFailureRMDecryption);
			}
			return this.decryptedItem;
		}

		// Token: 0x06006940 RID: 26944 RVA: 0x001C3B68 File Offset: 0x001C1D68
		public MessageItem ConvertRpmsgToMsg(MessageItem rightsProtectedMessage, string useLicense, SafeRightsManagementHandle enablingPrincipalRac)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.GetHashCode(), "RpMsgToMsgConverter::ConvertRpmsgToMsg");
			if (string.IsNullOrEmpty(useLicense))
			{
				throw new ArgumentNullException("useLicense");
			}
			if (enablingPrincipalRac == null)
			{
				throw new ArgumentNullException("enablingPrincipalRac");
			}
			if (enablingPrincipalRac.IsInvalid)
			{
				throw new ArgumentException("enablingPrincipalRac");
			}
			SafeRightsManagementHandle safeRightsManagementHandle = null;
			try
			{
				RmsClientManager.BindUseLicenseForDecryption(enablingPrincipalRac, useLicense, this.drmMsgContainer.PublishLicense, out safeRightsManagementHandle);
				this.DecryptMsg(rightsProtectedMessage, useLicense, safeRightsManagementHandle);
			}
			finally
			{
				if (safeRightsManagementHandle != null)
				{
					safeRightsManagementHandle.Close();
					safeRightsManagementHandle = null;
				}
			}
			if (this.decryptedItem == null)
			{
				throw new RightsManagementPermanentException(RightsManagementFailureCode.UnknownFailure, ServerStrings.GenericFailureRMDecryption);
			}
			return this.decryptedItem;
		}

		// Token: 0x06006941 RID: 26945 RVA: 0x001C3C18 File Offset: 0x001C1E18
		private void DecryptMsg(MessageItem rightsProtectedMessage, string useLicense, SafeRightsManagementHandle decryptorHandle)
		{
			if (decryptorHandle == null)
			{
				throw new ArgumentNullException("decryptorHandle");
			}
			if (decryptorHandle.IsInvalid)
			{
				throw new ArgumentException("decryptorHandle");
			}
			this.originalItem = rightsProtectedMessage;
			DrmEmailMessage drmEmailMessage = null;
			bool flag = false;
			try
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.GetHashCode(), "Found a use license for server. Decrypting message");
				DrmEmailMessageBinding messageBinding = new DrmEmailMessageBinding(this.drmMsgContainer.PublishLicense, decryptorHandle);
				this.drmMsgContainer.Bind(messageBinding, new CreateStreamCallbackDelegate(this.BodyStreamCallback), new CreateStreamCallbackDelegate(this.AttachmentsStreamCallback));
				drmEmailMessage = this.drmMsgContainer.EmailMessage;
				this.SaveAndCloseCurrentAttachment();
				this.decryptedItem.SafeSetProperty(InternalSchema.DRMServerLicense, useLicense);
				this.decryptedItem.SafeSetProperty(InternalSchema.DrmPublishLicense, this.drmMsgContainer.PublishLicense);
				if (drmEmailMessage.Attachments.Count > 0)
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug<int>((long)this.GetHashCode(), "Number of attachments in the rights protected message : {0}", drmEmailMessage.Attachments.Count);
					int num = 0;
					foreach (DrmEmailAttachment drmEmailAttachment in drmEmailMessage.Attachments)
					{
						using (Attachment attachment = this.decryptedItem.AttachmentCollection.Open(this.messageAttachmentIds[num++], null))
						{
							attachment.FileName = drmEmailAttachment.FileName;
							attachment[InternalSchema.DisplayName] = drmEmailAttachment.DisplayName;
							attachment.ContentId = drmEmailAttachment.ContentId;
							attachment[InternalSchema.AttachContentLocation] = drmEmailAttachment.ContentLocation;
							attachment[InternalSchema.AttachMhtmlFlags] = drmEmailAttachment.AttachFlags;
							attachment[InternalSchema.AttachCalendarHidden] = drmEmailAttachment.AttachHidden;
							if (drmEmailAttachment.AttachHidden)
							{
								attachment.IsInline = true;
							}
							if (drmEmailMessage.BodyFormat == BodyFormat.Rtf)
							{
								attachment.RenderingPosition = (int)drmEmailAttachment.CharacterPosition;
								if (attachment.AttachmentType == AttachmentType.EmbeddedMessage || attachment.AttachmentType == AttachmentType.Stream)
								{
									attachment[InternalSchema.AttachRendering] = drmEmailAttachment.AttachRendering;
								}
							}
							if (this.decryptAttachments)
							{
								Stream stream = null;
								StreamAttachment streamAttachment = attachment as StreamAttachment;
								if (streamAttachment != null && StreamAttachment.TryOpenRestrictedContent(streamAttachment, this.orgId, out stream))
								{
									using (stream)
									{
										using (Stream contentStream = streamAttachment.GetContentStream(PropertyOpenMode.Create))
										{
											Util.StreamHandler.CopyStreamData(stream, contentStream);
										}
									}
								}
							}
							attachment.Save();
						}
					}
				}
				drmEmailMessage.Close();
				drmEmailMessage = null;
				if (this.originalItem != null)
				{
					PersistablePropertyBag.CopyProperty(this.originalItem.PropertyBag, InternalSchema.TransportMessageHeaders, this.decryptedItem.PropertyBag);
					this.decryptedItem.Recipients.CopyRecipientsFrom(this.originalItem.Recipients);
				}
				this.decryptedItem.Save(SaveMode.NoConflictResolution);
				this.decryptedItem.Load(InternalSchema.ContentConversionProperties);
				flag = true;
			}
			finally
			{
				if (drmEmailMessage != null)
				{
					drmEmailMessage.Close();
					drmEmailMessage = null;
				}
				if (!flag && this.decryptedItem != null)
				{
					this.decryptedItem.Dispose();
					this.decryptedItem = null;
				}
			}
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x001C3FB0 File Offset: 0x001C21B0
		private Stream BodyStreamCallback(object context)
		{
			DrmEmailBodyInfo drmEmailBodyInfo = (DrmEmailBodyInfo)context;
			BodyFormat bodyFormat = drmEmailBodyInfo.BodyFormat;
			Charset charset;
			if (!Charset.TryGetCharset(drmEmailBodyInfo.CodePage, out charset))
			{
				charset = Charset.DefaultMimeCharset;
			}
			BodyFormat bodyFormat2;
			switch (bodyFormat)
			{
			case BodyFormat.PlainText:
				charset = Charset.Unicode;
				bodyFormat2 = BodyFormat.TextPlain;
				break;
			case BodyFormat.Html:
				bodyFormat2 = BodyFormat.TextHtml;
				break;
			case BodyFormat.Rtf:
				bodyFormat2 = BodyFormat.ApplicationRtf;
				break;
			default:
				throw new StoragePermanentException(ServerStrings.InvalidBodyFormat);
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug<BodyFormat, string>((long)this.GetHashCode(), "Decrypting Body. BodyFormat {0}, Charset {1}", bodyFormat2, charset.Name);
			bool flag = false;
			Stream result;
			try
			{
				this.decryptedItem = MessageItem.CreateInMemory(InternalSchema.ContentConversionProperties);
				if (this.originalItem != null)
				{
					this.originalItem.Load(new PropertyDefinition[]
					{
						InternalSchema.TransportMessageHeaders
					});
					foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in this.originalItem.AllNativeProperties)
					{
						if (ConvertUtils.IsPropertyTransmittable(nativeStorePropertyDefinition) && !Body.BodyPropSet.Contains(nativeStorePropertyDefinition) && nativeStorePropertyDefinition != InternalSchema.TransportMessageHeaders)
						{
							PersistablePropertyBag.CopyProperty(this.originalItem.PropertyBag, nativeStorePropertyDefinition, this.decryptedItem.PropertyBag);
						}
					}
				}
				this.messageAttachmentIds = new List<AttachmentId>();
				flag = true;
				result = this.decryptedItem.Body.OpenWriteStream(new BodyWriteConfiguration(bodyFormat2, charset));
			}
			finally
			{
				if (!flag && this.decryptedItem != null)
				{
					this.decryptedItem.Dispose();
					this.decryptedItem = null;
				}
			}
			return result;
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x001C414C File Offset: 0x001C234C
		private Stream AttachmentsStreamCallback(object context)
		{
			if (this.decryptedItem == null)
			{
				throw new StoragePermanentException(ServerStrings.FailedToAddAttachments);
			}
			this.SaveAndCloseCurrentAttachment();
			AttachmentType attachmentType = (AttachmentType)context;
			AttachmentType attachmentType2 = attachmentType;
			AttachmentType attachmentType3;
			if (attachmentType2 != AttachmentType.OleObject)
			{
				if (attachmentType2 != AttachmentType.ByValue)
				{
					if (attachmentType2 != AttachmentType.EmbeddedMessage)
					{
						throw new StoragePermanentException(ServerStrings.InvalidAttachmentType);
					}
					attachmentType3 = AttachmentType.EmbeddedMessage;
				}
				else
				{
					attachmentType3 = AttachmentType.Stream;
				}
			}
			else
			{
				attachmentType3 = AttachmentType.Ole;
			}
			AttachmentCollection attachmentCollection = this.decryptedItem.AttachmentCollection;
			this.currentAttachment = attachmentCollection.Create(attachmentType3);
			ExTraceGlobals.RightsManagementTracer.TraceDebug<AttachmentType>((long)this.GetHashCode(), "Decrypting Attachment. AttachmentType {0}", attachmentType3);
			StreamAttachmentBase streamAttachmentBase = this.currentAttachment as StreamAttachmentBase;
			if (streamAttachmentBase != null)
			{
				return streamAttachmentBase.GetContentStream(PropertyOpenMode.Create);
			}
			this.temporaryStreamForEmbeddedMessage = Streams.CreateTemporaryStorageStream();
			return this.temporaryStreamForEmbeddedMessage;
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x001C41FC File Offset: 0x001C23FC
		private void SaveAndCloseCurrentAttachment()
		{
			if (this.currentAttachment != null)
			{
				if (this.currentAttachment.AttachmentType == AttachmentType.EmbeddedMessage)
				{
					ItemAttachment itemAttachment = this.currentAttachment as ItemAttachment;
					if (itemAttachment != null)
					{
						using (Item item = itemAttachment.GetItem())
						{
							ItemConversion.ConvertMsgStorageToItem(this.temporaryStreamForEmbeddedMessage, item, new InboundConversionOptions(new EmptyRecipientCache(), string.Empty));
							this.temporaryStreamForEmbeddedMessage = null;
							item.Save(SaveMode.NoConflictResolution);
						}
					}
				}
				this.currentAttachment.Save();
				this.currentAttachment.Load(null);
				this.messageAttachmentIds.Add(this.currentAttachment.Id);
				this.currentAttachment.Dispose();
				this.currentAttachment = null;
			}
		}

		// Token: 0x04003BE5 RID: 15333
		private MessageItem originalItem;

		// Token: 0x04003BE6 RID: 15334
		private DrmEmailMessageContainer drmMsgContainer;

		// Token: 0x04003BE7 RID: 15335
		private MessageItem decryptedItem;

		// Token: 0x04003BE8 RID: 15336
		private bool decryptAttachments;

		// Token: 0x04003BE9 RID: 15337
		private List<AttachmentId> messageAttachmentIds;

		// Token: 0x04003BEA RID: 15338
		private Attachment currentAttachment;

		// Token: 0x04003BEB RID: 15339
		private Stream temporaryStreamForEmbeddedMessage;

		// Token: 0x04003BEC RID: 15340
		private OrganizationId orgId;
	}
}
