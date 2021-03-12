using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.Protectors;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B35 RID: 2869
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MsgToRpMsgConverter : DisposableObject
	{
		// Token: 0x060067C9 RID: 26569 RVA: 0x001B6AE8 File Offset: 0x001B4CE8
		internal MsgToRpMsgConverter(MessageItem envelopeMessage, OrganizationId orgId, string publishLicense, string serverUseLicense, OutboundConversionOptions options)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.serverUseLicense = serverUseLicense;
				this.publishLicense = publishLicense;
				this.options = options;
				this.charset = ConvertUtils.GetItemMimeCharset(envelopeMessage.CoreItem.PropertyBag);
				this.messageId = envelopeMessage.InternetMessageId;
				this.orgId = RmsClientManagerUtils.OrgIdFromPublishingLicenseOrDefault(publishLicense, orgId);
				this.InitTenantLicenses();
				MsgToRpMsgConverter.CallRM(delegate
				{
					DrmClientUtils.GetContentIdFromLicense(this.publishLicense, out this.contentId, out this.contentIdType);
				}, ServerStrings.FailedToParseUseLicense);
				disposeGuard.Success();
			}
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x001B6C68 File Offset: 0x001B4E68
		internal MsgToRpMsgConverter(MessageItem envelopeMessage, Participant conversationOwner, OrganizationId orgId, RmsTemplate restriction, OutboundConversionOptions options)
		{
			MsgToRpMsgConverter <>4__this = this;
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.options = options;
				this.orgId = orgId;
				this.charset = ConvertUtils.GetItemMimeCharset(envelopeMessage.CoreItem.PropertyBag);
				this.messageId = envelopeMessage.InternetMessageId;
				MsgToRpMsgConverter.RpMsgConversionAddressCache rpMsgConversionAddressCache = new MsgToRpMsgConverter.RpMsgConversionAddressCache(this.options);
				rpMsgConversionAddressCache.CopyRecipientsFromItem(envelopeMessage);
				rpMsgConversionAddressCache.Sender = conversationOwner;
				if (envelopeMessage.From != null)
				{
					rpMsgConversionAddressCache.From = envelopeMessage.From;
				}
				rpMsgConversionAddressCache.Resolve();
				string senderAddress = ItemToMimeConverter.TryGetParticipantSmtpAddress(rpMsgConversionAddressCache.Sender ?? conversationOwner);
				if (string.IsNullOrEmpty(senderAddress))
				{
					ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Message sender must have SMTP address to protect the message: {0}", conversationOwner.DisplayName);
					throw new CorruptDataException(ServerStrings.CannotProtectMessageForNonSmtpSender);
				}
				List<string> recipientAddresses = new List<string>(envelopeMessage.Recipients.Count);
				for (int i = 0; i < envelopeMessage.Recipients.Count; i++)
				{
					string text = ItemToMimeConverter.TryGetParticipantSmtpAddress(rpMsgConversionAddressCache.GetRecipient(i) ?? envelopeMessage.Recipients[i].Participant);
					if (!string.IsNullOrEmpty(text))
					{
						recipientAddresses.Add(text);
					}
					else
					{
						ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Message recipient must have SMTP address to be included in the IRM message license: {0}", envelopeMessage.Recipients[i].Participant.DisplayName);
					}
				}
				string fromAddress = string.Empty;
				if (rpMsgConversionAddressCache.From != null)
				{
					fromAddress = ItemToMimeConverter.TryGetParticipantSmtpAddress(rpMsgConversionAddressCache.From);
					if (string.IsNullOrEmpty(fromAddress))
					{
						ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Message from participant must have SMTP address to be included in the IRM message license: {0}", rpMsgConversionAddressCache.From.DisplayName);
					}
				}
				this.InitTenantLicenses();
				MsgToRpMsgConverter.CallRM(delegate
				{
					<>4__this.publishLicense = restriction.CreatePublishLicense(senderAddress, fromAddress, recipientAddresses, null, <>4__this.licensePair, RmsClientManager.EnvironmentHandle, out <>4__this.serverUseLicense, out <>4__this.contentId, out <>4__this.contentIdType);
					if (<>4__this.publishLicense == null)
					{
						ExTraceGlobals.StorageTracer.TraceError<string>(0L, "Failed to acquire publish license for sender {0}.", senderAddress);
						throw new RightsManagementPermanentException(RightsManagementFailureCode.InvalidIssuanceLicense, ServerStrings.FailedToAquirePublishLicense(senderAddress));
					}
				}, ServerStrings.FailedToAquirePublishLicense(senderAddress));
				disposeGuard.Success();
			}
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x001B6F38 File Offset: 0x001B5138
		internal void Convert(MessageItem item, Stream outStream)
		{
			MsgToRpMsgConverter.<>c__DisplayClass9 CS$<>8__locals1 = new MsgToRpMsgConverter.<>c__DisplayClass9();
			CS$<>8__locals1.outStream = outStream;
			CS$<>8__locals1.<>4__this = this;
			Stream stream = null;
			Stream stream2 = null;
			if (item.AttachmentCollection.Count > this.options.Limits.MaxBodyPartsTotal)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "The message has too many attachments to be protected.");
				throw new CorruptDataException(ServerStrings.TooManyAttachmentsOnProtectedMessage);
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				try
				{
					MsgToRpMsgConverter.<>c__DisplayClassb CS$<>8__locals2 = new MsgToRpMsgConverter.<>c__DisplayClassb();
					CS$<>8__locals2.CS$<>8__localsa = CS$<>8__locals1;
					if (item.Body.Format == BodyFormat.TextPlain)
					{
						stream = item.Body.OpenReadStream(new BodyReadConfiguration(item.Body.Format, Charset.Unicode.Name));
					}
					else
					{
						stream = item.Body.OpenReadStream(new BodyReadConfiguration(item.Body.Format, this.charset.Name));
					}
					if (item.Body.Format != BodyFormat.TextHtml)
					{
						Stream stream3 = Streams.CreateTemporaryStorageStream();
						try
						{
							Util.StreamHandler.CopyStreamData(stream, stream3);
							stream.Dispose();
							stream = stream3;
							stream.Position = 0L;
							stream3 = null;
						}
						finally
						{
							Util.DisposeIfPresent(stream3);
						}
						stream2 = item.Body.OpenReadStream(new BodyReadConfiguration(BodyFormat.TextHtml, "Windows-1252"));
					}
					BodyFormat bodyFormat = (item.Body.Format == BodyFormat.ApplicationRtf) ? BodyFormat.Rtf : ((item.Body.Format == BodyFormat.TextHtml) ? BodyFormat.Html : BodyFormat.PlainText);
					DrmEmailMessage drmEmailMessage = new DrmEmailMessage(stream, stream2, bodyFormat, this.charset.CodePage);
					foreach (AttachmentHandle handle in item.AttachmentCollection)
					{
						Attachment attachment = item.AttachmentCollection.Open(handle);
						disposeGuard.Add<Attachment>(attachment);
						AttachmentType attachmentType = (attachment.AttachmentType == AttachmentType.Stream) ? AttachmentType.ByValue : ((attachment.AttachmentType == AttachmentType.EmbeddedMessage) ? AttachmentType.EmbeddedMessage : AttachmentType.OleObject);
						Stream stream4 = null;
						string fileName;
						if (attachment.AttachmentType == AttachmentType.EmbeddedMessage)
						{
							stream4 = Streams.CreateTemporaryStorageStream();
							disposeGuard.Add<Stream>(stream4);
							ItemAttachment itemAttachment = attachment as ItemAttachment;
							using (Item item2 = itemAttachment.GetItem(StoreObjectSchema.ContentConversionProperties))
							{
								ItemConversion.ConvertItemToMsgStorage(item2, stream4, this.options);
							}
							stream4.Position = 0L;
							fileName = attachment.FileName;
						}
						else if (attachment.AttachmentType == AttachmentType.Ole && item.Body.Format != BodyFormat.ApplicationRtf)
						{
							OleAttachment oleAttachment = attachment as OleAttachment;
							stream4 = oleAttachment.ConvertToImage(ImageFormat.Jpeg);
							disposeGuard.Add<Stream>(stream4);
							attachmentType = AttachmentType.ByValue;
							fileName = string.Format("{0}.jpg", oleAttachment.FileName);
						}
						else
						{
							if (attachment.AttachmentType != AttachmentType.Ole && attachment.AttachmentType != AttachmentType.Stream)
							{
								continue;
							}
							StreamAttachmentBase streamAttachmentBase = attachment as StreamAttachmentBase;
							stream4 = streamAttachmentBase.GetContentStream(PropertyOpenMode.ReadOnly);
							disposeGuard.Add<Stream>(stream4);
							fileName = attachment.FileName;
						}
						Uri contentLocation = attachment.ContentLocation;
						DrmEmailAttachment item3 = new DrmEmailAttachment(attachmentType, stream4, (uint)attachment.RenderingPosition, attachment.ContentId, (contentLocation == null) ? string.Empty : contentLocation.ToString(), attachment.GetValueOrDefault<byte[]>(AttachmentSchema.AttachRendering, Array<byte>.Empty), attachment.DisplayName, fileName, attachment.GetValueOrDefault<int>(AttachmentSchema.AttachMhtmlFlags, 0), attachment.GetValueOrDefault<bool>(AttachmentSchema.AttachCalendarHidden, false));
						drmEmailMessage.Attachments.Add(item3);
					}
					CS$<>8__locals2.encryptorHandle = null;
					CS$<>8__locals2.decryptorHandle = null;
					MsgToRpMsgConverter.CallRM(delegate
					{
						RmsClientManager.BindUseLicenseForEncryption(CS$<>8__locals2.CS$<>8__localsa.<>4__this.licensePair.EnablingPrincipalRac, CS$<>8__locals2.CS$<>8__localsa.<>4__this.serverUseLicense, CS$<>8__locals2.CS$<>8__localsa.<>4__this.contentId, CS$<>8__locals2.CS$<>8__localsa.<>4__this.contentIdType, out CS$<>8__locals2.encryptorHandle, out CS$<>8__locals2.decryptorHandle);
					}, ServerStrings.RmExceptionGenericMessage);
					disposeGuard.Add<SafeRightsManagementHandle>(CS$<>8__locals2.encryptorHandle);
					disposeGuard.Add<SafeRightsManagementHandle>(CS$<>8__locals2.decryptorHandle);
					CS$<>8__locals2.binding = new DrmEmailMessageBinding(this.publishLicense, CS$<>8__locals2.encryptorHandle, CS$<>8__locals2.decryptorHandle);
					using (DrmEmailMessageContainer drmContainer = new DrmEmailMessageContainer(this.publishLicense, drmEmailMessage))
					{
						MsgToRpMsgConverter.CallRM(delegate
						{
							drmContainer.Save(CS$<>8__locals1.outStream, CS$<>8__locals2.binding);
						}, ServerStrings.RmExceptionGenericMessage);
					}
				}
				finally
				{
					Util.DisposeIfPresent(stream);
					Util.DisposeIfPresent(stream2);
				}
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x060067CC RID: 26572 RVA: 0x001B73F4 File Offset: 0x001B55F4
		internal string ServerUseLicense
		{
			get
			{
				return this.serverUseLicense;
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x060067CD RID: 26573 RVA: 0x001B73FC File Offset: 0x001B55FC
		internal string PublishLicense
		{
			get
			{
				return this.publishLicense;
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x060067CE RID: 26574 RVA: 0x001B7404 File Offset: 0x001B5604
		internal DisposableTenantLicensePair LicensePair
		{
			get
			{
				return this.licensePair;
			}
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x001B74E5 File Offset: 0x001B56E5
		private void InitTenantLicenses()
		{
			MsgToRpMsgConverter.CallRM(delegate
			{
				Uri firstLicensingLocation;
				if (!string.IsNullOrEmpty(this.publishLicense))
				{
					XmlNode[] array;
					bool flag;
					RmsClientManager.GetLicensingUri(this.orgId, this.publishLicense, out firstLicensingLocation, out array, out flag);
				}
				else
				{
					firstLicensingLocation = RmsClientManager.GetFirstLicensingLocation(this.orgId);
				}
				if (firstLicensingLocation == null)
				{
					ExTraceGlobals.StorageTracer.TraceError<OrganizationId>(0L, "Failed to find the license Uri for tenant {0}.", this.orgId);
					throw new RightsManagementPermanentException(RightsManagementFailureCode.InvalidLicensingLocation, ServerStrings.FailedToFindLicenseUri(this.orgId.ToString()));
				}
				this.licensePair = RmsClientManager.AcquireTenantLicenses(new RmsClientManagerContext(this.orgId, RmsClientManagerContext.ContextId.MessageId, this.messageId, null), firstLicensingLocation);
				if (this.licensePair == null)
				{
					ExTraceGlobals.StorageTracer.TraceError<OrganizationId>(0L, "Failed to acquire server box RAC and CLC for the tenant {0}.", this.orgId);
					throw new RightsManagementPermanentException(RightsManagementFailureCode.InvalidTenantLicense, ServerStrings.FailedToAcquireTenantLicenses(this.orgId.ToString(), firstLicensingLocation.AbsoluteUri));
				}
			}, ServerStrings.FailedToAcquireTenantLicenses(this.orgId.ToString(), string.Empty));
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x001B750D File Offset: 0x001B570D
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MsgToRpMsgConverter>(this);
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x001B7515 File Offset: 0x001B5715
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.licensePair != null)
			{
				this.licensePair.Dispose();
				this.licensePair = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x001B753C File Offset: 0x001B573C
		internal static void CallRM(MsgToRpMsgConverter.RMCall call, LocalizedString error)
		{
			try
			{
				call();
			}
			catch (RightsManagementException ex)
			{
				ExTraceGlobals.StorageTracer.TraceError<RightsManagementException>(0L, "Got RightsManagementException: {0}", ex);
				if (ex.IsPermanent)
				{
					throw new RightsManagementPermanentException(error, ex);
				}
				throw new RightsManagementTransientException(error, ex);
			}
			catch (InvalidRpmsgFormatException ex2)
			{
				ExTraceGlobals.StorageTracer.TraceError<InvalidRpmsgFormatException>(0L, "Got InvalidRpmsgFormatException: {0}", ex2);
				throw new RightsManagementPermanentException(error, ex2);
			}
			catch (ExchangeConfigurationException ex3)
			{
				ExTraceGlobals.StorageTracer.TraceError<ExchangeConfigurationException>(0L, "Got ExchangeConfigurationException: {0}", ex3);
				throw new RightsManagementTransientException(error, ex3);
			}
			catch (AttachmentProtectionException ex4)
			{
				ExTraceGlobals.StorageTracer.TraceError<AttachmentProtectionException>(0L, "Got AttachmentProtectionException: {0}", ex4);
				throw new RightsManagementPermanentException(error, ex4);
			}
			catch (InvalidRmsUrlException ex5)
			{
				ExTraceGlobals.StorageTracer.TraceError<InvalidRmsUrlException>(0L, "Got InvalidRmsUrlException: {0}", ex5);
				throw new RightsManagementPermanentException(error, ex5);
			}
		}

		// Token: 0x04003AD4 RID: 15060
		private const string HTMLEncodingCharset = "Windows-1252";

		// Token: 0x04003AD5 RID: 15061
		private DisposableTenantLicensePair licensePair;

		// Token: 0x04003AD6 RID: 15062
		private string serverUseLicense;

		// Token: 0x04003AD7 RID: 15063
		private string publishLicense;

		// Token: 0x04003AD8 RID: 15064
		private string contentId;

		// Token: 0x04003AD9 RID: 15065
		private string contentIdType;

		// Token: 0x04003ADA RID: 15066
		private string messageId;

		// Token: 0x04003ADB RID: 15067
		private readonly OutboundConversionOptions options;

		// Token: 0x04003ADC RID: 15068
		private readonly OrganizationId orgId;

		// Token: 0x04003ADD RID: 15069
		private readonly Charset charset;

		// Token: 0x02000B36 RID: 2870
		private class RpMsgConversionAddressCache : ConversionAddressCollection
		{
			// Token: 0x060067D5 RID: 26581 RVA: 0x001B7630 File Offset: 0x001B5830
			internal RpMsgConversionAddressCache(OutboundConversionOptions options) : base(options.UseSimpleDisplayName, false)
			{
				this.options = options;
				base.AddParticipantList(this.participants);
			}

			// Token: 0x060067D6 RID: 26582 RVA: 0x001B765D File Offset: 0x001B585D
			protected override IADRecipientCache GetRecipientCache(int count)
			{
				return this.options.InternalGetRecipientCache(count);
			}

			// Token: 0x060067D7 RID: 26583 RVA: 0x001B766B File Offset: 0x001B586B
			protected override bool CanResolveParticipant(Participant participant)
			{
				return participant != null;
			}

			// Token: 0x17001C90 RID: 7312
			// (get) Token: 0x060067D8 RID: 26584 RVA: 0x001B7674 File Offset: 0x001B5874
			protected override string TargetResolutionType
			{
				get
				{
					return "SMTP";
				}
			}

			// Token: 0x060067D9 RID: 26585 RVA: 0x001B767C File Offset: 0x001B587C
			internal void Resolve()
			{
				ConversionAddressCollection.ParticipantResolutionList participantResolutionList = base.CreateResolutionList();
				base.ResolveParticipants(participantResolutionList);
				base.SetResolvedParticipants(participantResolutionList);
			}

			// Token: 0x060067DA RID: 26586 RVA: 0x001B76A0 File Offset: 0x001B58A0
			internal void CopyRecipientsFromItem(MessageItem item)
			{
				foreach (Recipient recipient in item.Recipients)
				{
					this.participants.Add(recipient.Participant);
				}
			}

			// Token: 0x17001C91 RID: 7313
			// (get) Token: 0x060067DB RID: 26587 RVA: 0x001B76F8 File Offset: 0x001B58F8
			// (set) Token: 0x060067DC RID: 26588 RVA: 0x001B7706 File Offset: 0x001B5906
			internal Participant Sender
			{
				get
				{
					return this.participants[0];
				}
				set
				{
					this.participants[0] = value;
				}
			}

			// Token: 0x17001C92 RID: 7314
			// (get) Token: 0x060067DD RID: 26589 RVA: 0x001B7715 File Offset: 0x001B5915
			// (set) Token: 0x060067DE RID: 26590 RVA: 0x001B7723 File Offset: 0x001B5923
			internal Participant From
			{
				get
				{
					return this.participants[1];
				}
				set
				{
					this.participants[1] = value;
				}
			}

			// Token: 0x060067DF RID: 26591 RVA: 0x001B7732 File Offset: 0x001B5932
			internal Participant GetRecipient(int idx)
			{
				return this.participants[idx + 2];
			}

			// Token: 0x04003ADE RID: 15070
			private const int SenderIndex = 0;

			// Token: 0x04003ADF RID: 15071
			private const int FromIndex = 1;

			// Token: 0x04003AE0 RID: 15072
			private const int OneOffParticipantCount = 2;

			// Token: 0x04003AE1 RID: 15073
			private readonly OutboundConversionOptions options;

			// Token: 0x04003AE2 RID: 15074
			private readonly MsgToRpMsgConverter.RpMsgConversionAddressCache.RpMsgParticipantResolutionList participants = new MsgToRpMsgConverter.RpMsgConversionAddressCache.RpMsgParticipantResolutionList();

			// Token: 0x02000B37 RID: 2871
			private class RpMsgParticipantResolutionList : List<Participant>, IConversionParticipantList
			{
				// Token: 0x060067E0 RID: 26592 RVA: 0x001B7742 File Offset: 0x001B5942
				internal RpMsgParticipantResolutionList()
				{
				}

				// Token: 0x17001C93 RID: 7315
				// (get) Token: 0x060067E1 RID: 26593 RVA: 0x001B774A File Offset: 0x001B594A
				public new int Count
				{
					get
					{
						return base.Count + 2;
					}
				}

				// Token: 0x17001C94 RID: 7316
				public new Participant this[int index]
				{
					get
					{
						switch (index)
						{
						case 0:
							return this.sender;
						case 1:
							return this.from;
						default:
							return base[index - 2];
						}
					}
					set
					{
						switch (index)
						{
						case 0:
							this.sender = value;
							return;
						case 1:
							this.from = value;
							return;
						default:
							base[index - 2] = value;
							return;
						}
					}
				}

				// Token: 0x060067E4 RID: 26596 RVA: 0x001B77C5 File Offset: 0x001B59C5
				public bool IsConversionParticipantAlwaysResolvable(int index)
				{
					return true;
				}

				// Token: 0x04003AE3 RID: 15075
				private Participant sender;

				// Token: 0x04003AE4 RID: 15076
				private Participant from;
			}
		}

		// Token: 0x02000B38 RID: 2872
		// (Invoke) Token: 0x060067E6 RID: 26598
		internal delegate void RMCall();
	}
}
