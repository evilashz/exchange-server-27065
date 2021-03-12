using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B45 RID: 2885
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RightsManagedMessageItem : MessageItem
	{
		// Token: 0x06006829 RID: 26665 RVA: 0x001B8A43 File Offset: 0x001B6C43
		internal RightsManagedMessageItem(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x0600682A RID: 26666 RVA: 0x001B8A5F File Offset: 0x001B6C5F
		public RmsTemplate Restriction
		{
			get
			{
				this.CheckDisposed("Restriction::get");
				this.EnsureIsDecoded();
				return this.rmsTemplate;
			}
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x0600682B RID: 26667 RVA: 0x001B8A78 File Offset: 0x001B6C78
		public ContentRight UsageRights
		{
			get
			{
				this.CheckDisposed("UsageRights::get");
				this.EnsureIsDecoded();
				return this.UsageRightsInternal;
			}
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x0600682C RID: 26668 RVA: 0x001B8A91 File Offset: 0x001B6C91
		public Participant ConversationOwner
		{
			get
			{
				this.CheckDisposed("ConversationOwner::get");
				this.EnsureIsDecoded();
				if (this.conversationOwner == null)
				{
					return base.Sender;
				}
				return this.conversationOwner;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x0600682D RID: 26669 RVA: 0x001B8ABF File Offset: 0x001B6CBF
		public bool IsDecoded
		{
			get
			{
				this.CheckDisposed("IsDecoded::get");
				return this.decodedItem != null;
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x0600682E RID: 26670 RVA: 0x001B8AD8 File Offset: 0x001B6CD8
		public bool CanDecode
		{
			get
			{
				this.CheckDisposed("CanDecode::get");
				MailboxSession internalSession = this.InternalSession;
				return internalSession != null && (internalSession.MailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || internalSession.LogonType != LogonType.Delegated);
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x0600682F RID: 26671 RVA: 0x001B8B1C File Offset: 0x001B6D1C
		public bool CanRepublish
		{
			get
			{
				this.CheckDisposed("CanRepublish::get");
				return !this.publishedByExternalRMS;
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x06006830 RID: 26672 RVA: 0x001B8B32 File Offset: 0x001B6D32
		public Body ProtectedBody
		{
			get
			{
				this.CheckDisposed("ProtectedBody::get");
				this.EnsureIsDecoded();
				this.CheckPermission(ContentRight.View);
				return this.decodedItem.Body;
			}
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06006831 RID: 26673 RVA: 0x001B8B57 File Offset: 0x001B6D57
		public AttachmentCollection ProtectedAttachmentCollection
		{
			get
			{
				this.CheckDisposed("ProtectedAttachmentCollection::get");
				this.EnsureIsDecoded();
				this.CheckPermission(ContentRight.View);
				return this.decodedItem.AttachmentCollection;
			}
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x06006832 RID: 26674 RVA: 0x001B8B7C File Offset: 0x001B6D7C
		public RightsManagedMessageDecryptionStatus DecryptionStatus
		{
			get
			{
				this.CheckDisposed("DecryptionStatus::get");
				return this.decryptionStatus;
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06006833 RID: 26675 RVA: 0x001B8B8F File Offset: 0x001B6D8F
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return RightsManagedMessageItemSchema.Instance;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x06006834 RID: 26676 RVA: 0x001B8BA1 File Offset: 0x001B6DA1
		private ContentRight UsageRightsInternal
		{
			get
			{
				if (this.restrictionInfo == null)
				{
					return ContentRight.Owner;
				}
				return this.restrictionInfo.UsageRights;
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x06006835 RID: 26677 RVA: 0x001B8BB9 File Offset: 0x001B6DB9
		private CultureInfo Culture
		{
			get
			{
				if (this.InternalSession == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this.InternalSession.InternalCulture;
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x06006836 RID: 26678 RVA: 0x001B8BD4 File Offset: 0x001B6DD4
		private MailboxSession InternalSession
		{
			get
			{
				return (base.Session ?? ((base.CoreItem.TopLevelItem != null) ? base.CoreItem.TopLevelItem.Session : null)) as MailboxSession;
			}
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x001B8C08 File Offset: 0x001B6E08
		public static RightsManagedMessageItem Create(MailboxSession session, StoreId destFolderId, OutboundConversionOptions options)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(destFolderId, "destFolderId");
			Util.ThrowOnNullArgument(options, "options");
			RightsManagedMessageItem.CheckSession(session);
			RightsManagedMessageItem rightsManagedMessageItem = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				rightsManagedMessageItem = ItemBuilder.CreateNewItem<RightsManagedMessageItem>(session, destFolderId, ItemCreateInfo.RightsManagedMessageItemInfo, CreateMessageType.Normal);
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				rightsManagedMessageItem[InternalSchema.ItemClass] = "IPM.Note";
				rightsManagedMessageItem.InitNewItem(options);
				rightsManagedMessageItem.SetDefaultEnvelopeBody(null);
				disposeGuard.Success();
			}
			return rightsManagedMessageItem;
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x001B8CB8 File Offset: 0x001B6EB8
		public static RightsManagedMessageItem CreateInMemory(OutboundConversionOptions options)
		{
			Util.ThrowOnNullArgument(options, "options");
			RightsManagedMessageItem rightsManagedMessageItem = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				rightsManagedMessageItem = ItemBuilder.ConstructItem<RightsManagedMessageItem>(null, null, null, StoreObjectSchema.ContentConversionProperties, () => new InMemoryPersistablePropertyBag(StoreObjectSchema.ContentConversionProperties), ItemCreateInfo.RightsManagedMessageItemInfo.Creator, Origin.Existing, ItemLevel.TopLevel);
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				rightsManagedMessageItem[InternalSchema.ItemClass] = "IPM.Note";
				rightsManagedMessageItem.InitNewItem(options);
				rightsManagedMessageItem.SetDefaultEnvelopeBody(null);
				disposeGuard.Success();
			}
			return rightsManagedMessageItem;
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x001B8D68 File Offset: 0x001B6F68
		public static RightsManagedMessageItem CreateFromInMemory(MessageItem item, MailboxSession session, StoreId destFolderId, OutboundConversionOptions options)
		{
			Util.ThrowOnNullArgument(item, "item");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(destFolderId, "destFolderId");
			Util.ThrowOnNullArgument(options, "options");
			RightsManagedMessageItem.CheckSession(session);
			if (item.Session != null)
			{
				throw new InvalidOperationException("Item should be in-memory, not backed by store.");
			}
			RightsManagedMessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				RightsManagedMessageItem rightsManagedMessageItem = RightsManagedMessageItem.Create(session, destFolderId, options);
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				RightsManagedMessageItem.CopyProtectableData(item, rightsManagedMessageItem.decodedItem);
				foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in item.AllNativeProperties)
				{
					if (!Body.BodyPropSet.Contains(nativeStorePropertyDefinition) && nativeStorePropertyDefinition != StoreObjectSchema.ContentClass)
					{
						object obj = item.TryGetProperty(nativeStorePropertyDefinition);
						if (!(obj is PropertyError))
						{
							rightsManagedMessageItem[nativeStorePropertyDefinition] = obj;
						}
						else if (PropertyError.IsPropertyValueTooBig(obj))
						{
							using (Stream stream = item.OpenPropertyStream(nativeStorePropertyDefinition, PropertyOpenMode.ReadOnly))
							{
								using (Stream stream2 = rightsManagedMessageItem.OpenPropertyStream(nativeStorePropertyDefinition, PropertyOpenMode.Create))
								{
									Util.StreamHandler.CopyStreamData(stream, stream2);
								}
							}
						}
					}
				}
				rightsManagedMessageItem.Recipients.CopyRecipientsFrom(item.Recipients);
				rightsManagedMessageItem.SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreUnresolvedHeaders);
				disposeGuard.Success();
				result = rightsManagedMessageItem;
			}
			return result;
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x001B8EF4 File Offset: 0x001B70F4
		public static RightsManagedMessageItem Create(MessageItem item, OutboundConversionOptions options)
		{
			Util.ThrowOnNullArgument(item, "item");
			Util.ThrowOnNullArgument(options, "options");
			RightsManagedMessageItem.CheckSession(item.Session);
			RightsManagedMessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				RightsManagedMessageItem rightsManagedMessageItem = new RightsManagedMessageItem(new CoreItemWrapper(item.CoreItem));
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				rightsManagedMessageItem.InitNewItem(options);
				RightsManagedMessageItem.CopyProtectableData(item, rightsManagedMessageItem.decodedItem);
				rightsManagedMessageItem.SetDefaultEnvelopeBody(null);
				disposeGuard.Success();
				rightsManagedMessageItem.originalItem = item;
				result = rightsManagedMessageItem;
			}
			return result;
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x001B8F98 File Offset: 0x001B7198
		public static RightsManagedMessageItem ReBind(MessageItem item, OutboundConversionOptions options, bool acquireLicense)
		{
			Util.ThrowOnNullArgument(item, "item");
			Util.ThrowOnNullArgument(options, "options");
			StoreSession storeSession = item.Session ?? ((item.CoreItem.TopLevelItem != null) ? item.CoreItem.TopLevelItem.Session : null);
			if (storeSession == null)
			{
				throw new ArgumentException("Cannot use ReBind() for in-memory message.", "item");
			}
			RightsManagedMessageItem.CheckSession(storeSession);
			if (!item.IsRestricted)
			{
				throw new ArgumentException("Only protected messages can be used for ReBind()");
			}
			RightsManagedMessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				RightsManagedMessageItem rightsManagedMessageItem = new RightsManagedMessageItem(new CoreItemWrapper(item.CoreItem));
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				rightsManagedMessageItem.Decode(options, acquireLicense);
				disposeGuard.Success();
				rightsManagedMessageItem.originalItem = item;
				result = rightsManagedMessageItem;
			}
			return result;
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x001B9070 File Offset: 0x001B7270
		public static RightsManagedMessageItem Bind(MailboxSession session, StoreId messageId, OutboundConversionOptions options)
		{
			return RightsManagedMessageItem.Bind(session, messageId, options, true, new PropertyDefinition[0]);
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x001B9081 File Offset: 0x001B7281
		public static RightsManagedMessageItem Bind(MailboxSession session, StoreId messageId, OutboundConversionOptions options, bool acquireLicense)
		{
			return RightsManagedMessageItem.Bind(session, messageId, options, acquireLicense, new PropertyDefinition[0]);
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x001B9092 File Offset: 0x001B7292
		public static RightsManagedMessageItem Bind(MailboxSession session, StoreId messageId, OutboundConversionOptions options, ICollection<PropertyDefinition> propsToReturn)
		{
			return RightsManagedMessageItem.Bind(session, messageId, options, true, propsToReturn);
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x001B90A0 File Offset: 0x001B72A0
		public static RightsManagedMessageItem Bind(MailboxSession session, StoreId messageId, OutboundConversionOptions options, bool acquireLicense, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(messageId, "messageId");
			Util.ThrowOnNullArgument(options, "options");
			RightsManagedMessageItem.CheckSession(session);
			RightsManagedMessageItem rightsManagedMessageItem = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				rightsManagedMessageItem = ItemBuilder.ItemBind<RightsManagedMessageItem>(session, messageId, RightsManagedMessageItemSchema.Instance, propsToReturn);
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				rightsManagedMessageItem.Decode(options, acquireLicense);
				disposeGuard.Success();
			}
			return rightsManagedMessageItem;
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x001B9128 File Offset: 0x001B7328
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RightsManagedMessageItem>(this);
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x001B9130 File Offset: 0x001B7330
		public RightsManagedMessageDecryptionStatus TryDecode(OutboundConversionOptions options, bool acquireLicense)
		{
			try
			{
				this.Decode(options, acquireLicense);
			}
			catch (RightsManagementPermanentException)
			{
			}
			catch (RightsManagementTransientException)
			{
			}
			return this.decryptionStatus;
		}

		// Token: 0x06006842 RID: 26690 RVA: 0x001B918C File Offset: 0x001B738C
		public bool Decode(OutboundConversionOptions options, bool acquireLicense)
		{
			this.CheckDisposed("Decode");
			Util.ThrowOnNullArgument(options, "options");
			this.decryptionStatus = RightsManagedMessageDecryptionStatus.Success;
			if (this.decodedItem != null)
			{
				return true;
			}
			if (this.InternalSession == null)
			{
				this.decryptionStatus = RightsManagedMessageDecryptionStatus.NotSupported;
				throw new InvalidOperationException("Decoding of in-memory messages is not supported.");
			}
			RightsManagedMessageItem.CheckSession(this.InternalSession);
			this.SetConversionOptions(options);
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				try
				{
					MessageItem messageItem = ItemConversion.OpenRestrictedContent(this, this.orgId, acquireLicense, out this.licenseAcquired, out this.useLicenseValue, out this.restrictionInfo);
					if (messageItem == null)
					{
						ExTraceGlobals.StorageTracer.TraceError(0L, "Failed to decode protected message - no user license is present.");
						throw new RightsManagementPermanentException(RightsManagementFailureCode.UnknownFailure, ServerStrings.GenericFailureRMDecryption);
					}
					disposeGuard.Add<MessageItem>(messageItem);
					this.UpdateEffectiveRights();
					this.conversationOwner = new Participant(this.restrictionInfo.ConversationOwner, this.restrictionInfo.ConversationOwner, "SMTP");
					this.CheckPermission(ContentRight.View);
					messageItem.CoreItem.TopLevelItem = (base.CoreItem.TopLevelItem ?? base.CoreItem);
					this.serverUseLicense = (messageItem.TryGetProperty(MessageItemSchema.DRMServerLicense) as string);
					this.publishLicense = (messageItem.TryGetProperty(MessageItemSchema.DrmPublishLicense) as string);
					this.rmsTemplate = RmsTemplate.CreateFromPublishLicense(this.publishLicense);
					MsgToRpMsgConverter.CallRM(delegate
					{
						this.publishedByExternalRMS = !RmsClientManager.IsPublishedByOrganizationRMS(this.orgId, this.publishLicense);
					}, ServerStrings.FailedToCheckPublishLicenseOwnership(this.orgId.ToString()));
					this.decodedItem = messageItem;
					disposeGuard.Success();
				}
				catch (RightsManagementPermanentException exception)
				{
					this.decryptionStatus = RightsManagedMessageDecryptionStatus.CreateFromException(exception);
					throw;
				}
				catch (RightsManagementTransientException exception2)
				{
					this.decryptionStatus = RightsManagedMessageDecryptionStatus.CreateFromException(exception2);
					throw;
				}
			}
			return true;
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x001B938C File Offset: 0x001B758C
		public void UnprotectAttachment(AttachmentId attachmentId)
		{
			using (Attachment attachment = this.ProtectedAttachmentCollection.Open(attachmentId))
			{
				StreamAttachment streamAttachment = attachment as StreamAttachment;
				if (streamAttachment == null)
				{
					throw new ObjectNotFoundException(ServerStrings.MapiCannotOpenAttachment);
				}
				using (Stream stream = StreamAttachment.OpenRestrictedContent(streamAttachment, this.orgId))
				{
					using (Stream contentStream = streamAttachment.GetContentStream(PropertyOpenMode.Create))
					{
						Util.StreamHandler.CopyStreamData(stream, contentStream);
					}
				}
				attachment.Save();
			}
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x001B942C File Offset: 0x001B762C
		public void SetRestriction(RmsTemplate template)
		{
			this.CheckDisposed("Restriction::set");
			this.EnsureIsDecoded();
			this.CheckPermission(ContentRight.Export);
			this.rmsTemplate = template;
			if (this.rmsTemplate != null)
			{
				this.restrictionInfo = new RestrictionInfo(ContentRight.Owner, ExDateTime.MaxValue, string.Empty);
			}
			else
			{
				this.restrictionInfo = null;
				switch (base.IconIndex)
				{
				case IconIndex.MailIrm:
				case IconIndex.MailIrmForwarded:
				case IconIndex.MailIrmReplied:
					base.IconIndex = IconIndex.Default;
					break;
				}
			}
			this.conversationOwner = null;
			this.serverUseLicense = null;
			this.publishLicense = null;
			this.UpdateEffectiveRights();
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x001B94C8 File Offset: 0x001B76C8
		public void SetDefaultEnvelopeBody(LocalizedString? bodyString)
		{
			this.CheckDisposed("SetDefaultEnvelopeBody");
			this.EnsureIsDecoded();
			using (TextWriter textWriter = base.Body.OpenTextWriter(BodyFormat.TextPlain))
			{
				if (bodyString != null)
				{
					textWriter.Write(bodyString.Value.ToString(this.Culture));
				}
				else
				{
					string value = string.Format("{0} {1}", SystemMessages.BodyReceiveRMEmail.ToString(this.Culture), SystemMessages.BodyDownload.ToString(this.Culture));
					textWriter.Write(value);
				}
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06006846 RID: 26694 RVA: 0x001B9570 File Offset: 0x001B7770
		public MessageItem DecodedItem
		{
			get
			{
				this.CheckDisposed("DecodedItem::get");
				this.EnsureIsDecoded();
				this.CheckPermission(ContentRight.View);
				return this.decodedItem;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06006847 RID: 26695 RVA: 0x001B9590 File Offset: 0x001B7790
		public ExDateTime UserLicenseExpiryTime
		{
			get
			{
				this.CheckDisposed("UseLicenseExpiry::get");
				this.EnsureIsDecoded();
				return this.restrictionInfo.ExpiryTime;
			}
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x001B95AE File Offset: 0x001B77AE
		public void SetProtectedData(Body body, AttachmentCollection attachments)
		{
			this.CheckDisposed("SetProtectedData");
			this.EnsureIsDecoded();
			RightsManagedMessageItem.CopyProtectableData(body, attachments, this.decodedItem);
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x001B95D0 File Offset: 0x001B77D0
		public void AbandonChangesOnProtectedData()
		{
			this.CheckDisposed("AbandonChangesOnProtectedData");
			if (this.decodedItem != null)
			{
				this.decodedItem.Dispose();
				this.decodedItem = null;
				this.effectiveRights = ContentRight.Owner;
				this.publishLicense = null;
				this.restrictionInfo = null;
				this.rmsTemplate = null;
				this.serverUseLicense = null;
				this.conversationOwner = null;
			}
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x001B962D File Offset: 0x001B782D
		public void SaveUseLicense()
		{
			this.CheckDisposed("SaveUSeLicense");
			if (this.licenseAcquired)
			{
				base.OpenAsReadWrite();
				this.PrepareAcquiredLicensesBeforeSave();
				base.Save(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x001B9658 File Offset: 0x001B7858
		public void PrepareAcquiredLicensesBeforeSave()
		{
			this.CheckDisposed("PrepareAcquiredLicensesBeforeSave");
			if (this.licenseAcquired)
			{
				this[MessageItemSchema.DRMRights] = this.useLicenseValue.UsageRights;
				this[MessageItemSchema.DRMExpiryTime] = this.useLicenseValue.ExpiryTime;
				if (!DrmClientUtils.IsCachingOfLicenseDisabled(this.useLicenseValue.UseLicense))
				{
					using (Stream stream = base.OpenPropertyStream(MessageItemSchema.DRMServerLicenseCompressed, PropertyOpenMode.Create))
					{
						DrmEmailCompression.CompressUseLicense(this.useLicenseValue.UseLicense, stream);
					}
				}
				this[MessageItemSchema.DRMPropsSignature] = this.useLicenseValue.DRMPropsSignature;
			}
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x001B9730 File Offset: 0x001B7930
		public override MessageItem CreateForward(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateForward");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "RightsManagedMessageItem::CreateForward.");
			if (this.decodedItem == null)
			{
				return base.CreateForward(session, parentFolderId, configuration);
			}
			this.CheckPermission(ContentRight.Forward);
			return this.CreateReplyForwardInternal(session, parentFolderId, configuration, delegate(RightsManagedMessageItem original, RightsManagedMessageItem result, ReplyForwardConfiguration configurationPassed)
			{
				RightsManagedForwardCreation rightsManagedForwardCreation = new RightsManagedForwardCreation(original, result, configurationPassed);
				rightsManagedForwardCreation.PopulateProperties();
			});
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x001B97C4 File Offset: 0x001B79C4
		public override MessageItem CreateReply(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateReply");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "RightsManagedMessageItem::CreateReply.");
			if (this.decodedItem == null)
			{
				return base.CreateReply(session, parentFolderId, configuration);
			}
			this.CheckPermission(ContentRight.Reply);
			return this.CreateReplyForwardInternal(session, parentFolderId, configuration, delegate(RightsManagedMessageItem original, RightsManagedMessageItem result, ReplyForwardConfiguration configurationPassed)
			{
				RightsManagedReplyCreation rightsManagedReplyCreation = new RightsManagedReplyCreation(original, result, configurationPassed, false);
				rightsManagedReplyCreation.PopulateProperties();
			});
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x001B9858 File Offset: 0x001B7A58
		public override MessageItem CreateReplyAll(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration)
		{
			this.CheckDisposed("CreateReplyAll");
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "RightsManagedMessageItem::CreateReplyAll.");
			if (this.decodedItem == null)
			{
				return base.CreateReplyAll(session, parentFolderId, configuration);
			}
			this.CheckPermission(ContentRight.ReplyAll);
			return this.CreateReplyForwardInternal(session, parentFolderId, configuration, delegate(RightsManagedMessageItem original, RightsManagedMessageItem result, ReplyForwardConfiguration configurationPassed)
			{
				RightsManagedReplyCreation rightsManagedReplyCreation = new RightsManagedReplyCreation(original, result, configurationPassed, true);
				rightsManagedReplyCreation.PopulateProperties();
			});
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x001B98EA File Offset: 0x001B7AEA
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.decodedItem);
				Util.DisposeIfPresent(this.originalItem);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x001B990C File Offset: 0x001B7B0C
		protected override void InternalGetContextCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags)
		{
			base.InternalGetContextCharsetDetectionData(stringBuilder, flags);
			if (this.charsetDetectionStringForProtectedData != null)
			{
				stringBuilder.Append(this.charsetDetectionStringForProtectedData.ToString());
				return;
			}
			if (this.decodedItem == null && ((flags & CharsetDetectionDataFlags.Complete) != CharsetDetectionDataFlags.Complete || (flags & CharsetDetectionDataFlags.NoMessageDecoding) == CharsetDetectionDataFlags.NoMessageDecoding || base.AttachmentCollection.IsDirty))
			{
				return;
			}
			this.GetCharsetDetectionStringFromProtectedData(stringBuilder);
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x001B9965 File Offset: 0x001B7B65
		protected override void OnAfterSave(ConflictResolutionResult acrResults)
		{
			this.charsetDetectionStringForProtectedData = null;
			base.OnAfterSave(acrResults);
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x001B9978 File Offset: 0x001B7B78
		protected override void OnBeforeSave()
		{
			if (this.decodedItem == null && !base.AttachmentCollection.IsDirty && base.IsRestricted && (base.Recipients.IsDirty || base.IsPropertyDirty(ItemSchema.Sender)))
			{
				this.EnsureIsDecoded();
			}
			if (this.decodedItem != null)
			{
				string contentClass = base.TryGetProperty(InternalSchema.ContentClass) as string;
				if (this.rmsTemplate == null)
				{
					this.UnprotectAllAttachments();
					RightsManagedMessageItem.CopyProtectableData(this.decodedItem, this);
					if (ObjectClass.IsRightsManagedContentClass(contentClass))
					{
						base.Delete(StoreObjectSchema.ContentClass);
					}
				}
				else
				{
					this.charsetDetectionStringForProtectedData = new StringBuilder((int)Math.Min(this.ProtectedBody.Size, 2147483647L));
					this.GetCharsetDetectionStringFromProtectedData(this.charsetDetectionStringForProtectedData);
					if (!ObjectClass.IsRightsManagedContentClass(contentClass))
					{
						this[StoreObjectSchema.ContentClass] = "rpmsg.message";
					}
					if (this.isSending)
					{
						byte[][] valueOrDefault = base.GetValueOrDefault<byte[][]>(InternalSchema.DRMLicense);
						if (valueOrDefault != null && valueOrDefault.Length == RightsManagedMessageItem.EmptyDrmLicense.Length && valueOrDefault[0].Length == RightsManagedMessageItem.EmptyDrmLicense[0].Length)
						{
							base.DeleteProperties(new PropertyDefinition[]
							{
								InternalSchema.DRMLicense
							});
						}
					}
					else if (base.IsDraft && base.GetValueOrDefault<byte[][]>(InternalSchema.DRMLicense) == null)
					{
						this[InternalSchema.DRMLicense] = RightsManagedMessageItem.EmptyDrmLicense;
					}
					base.AttachmentCollection.RemoveAll();
					using (StreamAttachment streamAttachment = base.AttachmentCollection.Create(AttachmentType.Stream) as StreamAttachment)
					{
						streamAttachment.FileName = "message.rpmsg";
						streamAttachment.ContentType = "application/x-microsoft-rpmsg-message";
						using (Stream stream = new PooledMemoryStream(131072))
						{
							if (this.serverUseLicense == null || ((this.UsageRights & ContentRight.Owner) == ContentRight.Owner && this.rmsTemplate.RequiresRepublishingWhenRecipientsChange && this.CanRepublish && (base.Recipients.IsDirty || (base.IsPropertyDirty(ItemSchema.Sender) && this.conversationOwner == null))))
							{
								if (this.ConversationOwner == null)
								{
									throw new InvalidOperationException("Conversation owner must be set before protecting the message.");
								}
								this.UnprotectAllAttachments();
								using (MsgToRpMsgConverter msgToRpMsgConverter = new MsgToRpMsgConverter(this, this.ConversationOwner, this.orgId, this.rmsTemplate, this.options))
								{
									msgToRpMsgConverter.Convert(this.decodedItem, stream);
									using (Stream stream2 = base.OpenPropertyStream(MessageItemSchema.DRMServerLicenseCompressed, PropertyOpenMode.Create))
									{
										DrmEmailCompression.CompressUseLicense(msgToRpMsgConverter.ServerUseLicense, stream2);
									}
									if (this.InternalSession != null && this.InternalSession.MailboxOwner.Sid != null)
									{
										ExDateTime useLicenseExpiryTime = RmsClientManagerUtils.GetUseLicenseExpiryTime(msgToRpMsgConverter.ServerUseLicense, this.UsageRights);
										this[MessageItemSchema.DRMRights] = (int)this.UsageRights;
										this[MessageItemSchema.DRMExpiryTime] = useLicenseExpiryTime;
										using (RightsSignatureBuilder rightsSignatureBuilder = new RightsSignatureBuilder(msgToRpMsgConverter.ServerUseLicense, msgToRpMsgConverter.PublishLicense, RmsClientManager.EnvironmentHandle, msgToRpMsgConverter.LicensePair))
										{
											this[MessageItemSchema.DRMPropsSignature] = rightsSignatureBuilder.Sign(this.UsageRights, useLicenseExpiryTime, this.InternalSession.MailboxOwner.Sid);
										}
									}
									goto IL_362;
								}
							}
							using (MsgToRpMsgConverter msgToRpMsgConverter2 = new MsgToRpMsgConverter(this, this.orgId, this.publishLicense, this.serverUseLicense, this.options))
							{
								msgToRpMsgConverter2.Convert(this.decodedItem, stream);
							}
							IL_362:
							using (Stream contentStream = streamAttachment.GetContentStream(PropertyOpenMode.Create))
							{
								stream.Seek(0L, SeekOrigin.Begin);
								Util.StreamHandler.CopyStreamData(stream, contentStream);
							}
						}
						bool flag = false;
						foreach (AttachmentHandle handle in this.decodedItem.AttachmentCollection)
						{
							if (!CoreAttachmentCollection.IsInlineAttachment(handle))
							{
								flag = true;
								break;
							}
						}
						this[InternalSchema.AllAttachmentsHidden] = !flag;
						streamAttachment.Save();
					}
				}
				this.decodedItem.Dispose();
				this.decodedItem = null;
				this.effectiveRights = ContentRight.Owner;
				this.publishLicense = null;
				this.restrictionInfo = null;
				this.rmsTemplate = null;
				this.serverUseLicense = null;
				this.conversationOwner = null;
			}
			base.OnBeforeSave();
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x001B9E90 File Offset: 0x001B8090
		protected override void OnBeforeSend()
		{
			try
			{
				this.isSending = true;
				base.OnBeforeSend();
			}
			finally
			{
				this.isSending = false;
			}
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x001B9EC4 File Offset: 0x001B80C4
		private static void CopyProtectableData(MessageItem sourceItem, MessageItem targetItem)
		{
			RightsManagedMessageItem rightsManagedMessageItem = targetItem as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null)
			{
				if (!rightsManagedMessageItem.isFullCharsetDetectionEnabled)
				{
					throw new InvalidOperationException();
				}
				rightsManagedMessageItem.isFullCharsetDetectionEnabled = false;
			}
			try
			{
				RightsManagedMessageItem.CopyProtectableData(sourceItem.Body, sourceItem.AttachmentCollection, targetItem);
			}
			finally
			{
				if (rightsManagedMessageItem != null)
				{
					rightsManagedMessageItem.isFullCharsetDetectionEnabled = true;
				}
			}
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x001B9F20 File Offset: 0x001B8120
		private static void CopyProtectableData(RightsManagedMessageItem sourceItem, MessageItem targetItem)
		{
			RightsManagedMessageItem.CopyProtectableData(sourceItem.Body, sourceItem.AttachmentCollection, targetItem);
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x001B9F34 File Offset: 0x001B8134
		private static void CopyProtectableData(Body sourceBody, AttachmentCollection sourceAttachmentCollection, MessageItem targetItem)
		{
			if (sourceBody.IsBodyDefined)
			{
				using (Stream stream = sourceBody.OpenReadStream(new BodyReadConfiguration(sourceBody.Format, sourceBody.RawCharset.Name)))
				{
					using (Stream stream2 = targetItem.Body.OpenWriteStream(new BodyWriteConfiguration(sourceBody.Format, sourceBody.RawCharset)))
					{
						Util.StreamHandler.CopyStreamData(stream, stream2);
					}
					goto IL_6D;
				}
			}
			targetItem.DeleteProperties(Body.BodyProps);
			IL_6D:
			targetItem.AttachmentCollection.RemoveAll();
			foreach (AttachmentHandle handle in sourceAttachmentCollection)
			{
				using (Attachment attachment = sourceAttachmentCollection.Open(handle))
				{
					using (Attachment attachment2 = attachment.CreateCopy(targetItem.AttachmentCollection, new BodyFormat?(targetItem.Body.Format)))
					{
						attachment2.Save();
					}
				}
			}
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x001BA06C File Offset: 0x001B826C
		private static void CheckSession(StoreSession session)
		{
			if (session == null)
			{
				return;
			}
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new NotSupportedException("RightsManagedMessageItem can only be backed by a mailbox.");
			}
			if (mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox && mailboxSession.LogonType == LogonType.Delegated)
			{
				throw new NotSupportedException("RightsManagedMessageItem doesn't support delegate scenario.");
			}
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x001BA0BC File Offset: 0x001B82BC
		private void GetCharsetDetectionStringFromProtectedData(StringBuilder stringBuilder)
		{
			this.EnsureIsDecoded();
			this.decodedItem.CoreItem.GetCharsetDetectionData(stringBuilder, CharsetDetectionDataFlags.Complete);
			if (this.isFullCharsetDetectionEnabled)
			{
				using (TextReader textReader = this.ProtectedBody.OpenTextReader(BodyFormat.TextPlain))
				{
					char[] array = new char[32768];
					int charCount = textReader.ReadBlock(array, 0, array.Length);
					stringBuilder.Append(array, 0, charCount);
				}
			}
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x001BA134 File Offset: 0x001B8334
		private void CheckPermission(ContentRight perms)
		{
			if ((this.effectiveRights & ContentRight.Owner) == ContentRight.Owner)
			{
				return;
			}
			if ((this.effectiveRights & perms) != perms)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Not enough permissions to perform the requested operation.");
				throw new RightsManagementPermanentException(RightsManagementFailureCode.UserRightNotGranted, ServerStrings.NotEnoughPermissionsToPerformOperation);
			}
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x001BA170 File Offset: 0x001B8370
		private void UpdateEffectiveRights()
		{
			ContentRight usageRightsInternal = this.UsageRightsInternal;
			this.effectiveRights = usageRightsInternal;
			foreach (ContentRight[] array2 in RightsManagedMessageItem.impliedRights)
			{
				if ((usageRightsInternal & array2[0]) == array2[0])
				{
					this.effectiveRights |= array2[1];
				}
			}
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x001BA1C0 File Offset: 0x001B83C0
		private void CopyLicenseDataFrom(RightsManagedMessageItem source)
		{
			this.rmsTemplate = source.rmsTemplate;
			this.conversationOwner = source.conversationOwner;
			this.publishLicense = source.publishLicense;
			this.serverUseLicense = source.serverUseLicense;
			this.restrictionInfo = source.restrictionInfo;
			this.publishedByExternalRMS = source.publishedByExternalRMS;
			this.UpdateEffectiveRights();
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x001BA21C File Offset: 0x001B841C
		private RightsManagedMessageItem CreateReplyForwardInternal(MailboxSession session, StoreId parentFolderId, ReplyForwardConfiguration configuration, RightsManagedMessageItem.ReplyForwardCreationCall call)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(configuration, "configuration");
			if ((configuration.ForwardCreationFlags & (ForwardCreationFlags.PreserveSender | ForwardCreationFlags.TreatAsMeetingMessage)) != ForwardCreationFlags.None)
			{
				throw new InvalidOperationException("Invalid forward creation flags used.");
			}
			RightsManagedMessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				RightsManagedMessageItem rightsManagedMessageItem = RightsManagedMessageItem.Create(session, parentFolderId, this.options);
				disposeGuard.Add<RightsManagedMessageItem>(rightsManagedMessageItem);
				call(this, rightsManagedMessageItem, configuration);
				rightsManagedMessageItem.CopyLicenseDataFrom(this);
				disposeGuard.Success();
				result = rightsManagedMessageItem;
			}
			return result;
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x001BA2BC File Offset: 0x001B84BC
		private void InitNewItem(OutboundConversionOptions options)
		{
			this[StoreObjectSchema.ContentClass] = "rpmsg.message";
			base.IconIndex = IconIndex.MailIrm;
			this.decodedItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties);
			this.SetConversionOptions(options);
			this.UpdateEffectiveRights();
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x001BA2F8 File Offset: 0x001B84F8
		private void SetConversionOptions(OutboundConversionOptions options)
		{
			this.options = options;
			if (this.InternalSession != null)
			{
				this.orgId = this.InternalSession.MailboxOwner.MailboxInfo.OrganizationId;
				return;
			}
			if (options.UserADSession != null)
			{
				this.orgId = options.UserADSession.SessionSettings.CurrentOrganizationId;
				return;
			}
			this.orgId = OrganizationId.ForestWideOrgId;
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x001BA35C File Offset: 0x001B855C
		private void UnprotectAllAttachments()
		{
			if (!(this.decodedItem.TryGetProperty(MessageItemSchema.DRMServerLicense) is string))
			{
				return;
			}
			foreach (AttachmentHandle handle in this.decodedItem.AttachmentCollection)
			{
				using (Attachment attachment = this.decodedItem.AttachmentCollection.Open(handle))
				{
					Stream stream = null;
					StreamAttachment streamAttachment = attachment as StreamAttachment;
					if (StreamAttachment.TryOpenRestrictedContent(streamAttachment, this.orgId, out stream))
					{
						using (stream)
						{
							using (Stream contentStream = streamAttachment.GetContentStream(PropertyOpenMode.Create))
							{
								Util.StreamHandler.CopyStreamData(stream, contentStream);
							}
						}
						attachment.Save();
					}
				}
			}
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x001BA460 File Offset: 0x001B8660
		private void EnsureIsDecoded()
		{
			if (this.decodedItem == null)
			{
				if (this.options != null)
				{
					this.Decode(this.options, true);
				}
				if (this.decodedItem == null)
				{
					throw new InvalidOperationException("Message is not decoded yet.");
				}
			}
		}

		// Token: 0x04003B45 RID: 15173
		private const int InitialBufferCapacityForRpmsgStream = 131072;

		// Token: 0x04003B46 RID: 15174
		private static readonly byte[][] EmptyDrmLicense = new byte[][]
		{
			Array<byte>.Empty
		};

		// Token: 0x04003B47 RID: 15175
		private static ContentRight[][] impliedRights = new ContentRight[][]
		{
			new ContentRight[]
			{
				ContentRight.Edit,
				ContentRight.View | ContentRight.DocumentEdit
			},
			new ContentRight[]
			{
				ContentRight.Print,
				ContentRight.View
			},
			new ContentRight[]
			{
				ContentRight.Extract,
				ContentRight.View
			},
			new ContentRight[]
			{
				ContentRight.ObjectModel,
				ContentRight.View
			},
			new ContentRight[]
			{
				ContentRight.ViewRightsData,
				ContentRight.View
			},
			new ContentRight[]
			{
				ContentRight.Forward,
				ContentRight.View
			},
			new ContentRight[]
			{
				ContentRight.Reply,
				ContentRight.View
			},
			new ContentRight[]
			{
				ContentRight.ReplyAll,
				ContentRight.View
			},
			new ContentRight[]
			{
				ContentRight.DocumentEdit,
				ContentRight.View | ContentRight.Edit
			},
			new ContentRight[]
			{
				ContentRight.Export,
				ContentRight.View | ContentRight.Edit | ContentRight.DocumentEdit
			}
		};

		// Token: 0x04003B48 RID: 15176
		private MessageItem decodedItem;

		// Token: 0x04003B49 RID: 15177
		private RestrictionInfo restrictionInfo;

		// Token: 0x04003B4A RID: 15178
		private Participant conversationOwner;

		// Token: 0x04003B4B RID: 15179
		private ContentRight effectiveRights;

		// Token: 0x04003B4C RID: 15180
		private RmsTemplate rmsTemplate;

		// Token: 0x04003B4D RID: 15181
		private string publishLicense;

		// Token: 0x04003B4E RID: 15182
		private string serverUseLicense;

		// Token: 0x04003B4F RID: 15183
		private MessageItem originalItem;

		// Token: 0x04003B50 RID: 15184
		private bool publishedByExternalRMS;

		// Token: 0x04003B51 RID: 15185
		private OutboundConversionOptions options;

		// Token: 0x04003B52 RID: 15186
		private OrganizationId orgId;

		// Token: 0x04003B53 RID: 15187
		private bool isSending;

		// Token: 0x04003B54 RID: 15188
		private bool isFullCharsetDetectionEnabled = true;

		// Token: 0x04003B55 RID: 15189
		private RightsManagedMessageDecryptionStatus decryptionStatus = RightsManagedMessageDecryptionStatus.Success;

		// Token: 0x04003B56 RID: 15190
		private StringBuilder charsetDetectionStringForProtectedData;

		// Token: 0x04003B57 RID: 15191
		private bool licenseAcquired;

		// Token: 0x04003B58 RID: 15192
		private UseLicenseAndUsageRights useLicenseValue;

		// Token: 0x02000B46 RID: 2886
		// (Invoke) Token: 0x06006868 RID: 26728
		private delegate void ReplyForwardCreationCall(RightsManagedMessageItem original, RightsManagedMessageItem result, ReplyForwardConfiguration configuration);
	}
}
