using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Message : PropertyServerObject
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0001B3EC File Offset: 0x000195EC
		internal Message(CoreItem coreItem, Logon logon, Encoding string8Encoding) : this(coreItem, logon, string8Encoding, ClientSideProperties.MessageInstance, PropertyConverter.Message)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001B404 File Offset: 0x00019604
		internal Message(CoreItem coreItem, Logon logon, Encoding string8Encoding, ClientSideProperties clientSideProperties, PropertyConverter converter) : base(logon, clientSideProperties, converter)
		{
			this.recipientTranslator = new RecipientTranslator(coreItem, Array<PropertyTag>.Empty, string8Encoding);
			this.coreItemReference = new ReferenceCount<CoreItem>(coreItem);
			this.string8Encoding = string8Encoding;
			this.bestBodyCoreObjectProperties = new BestBodyCoreObjectProperties(coreItem, coreItem.PropertyBag, string8Encoding, new Func<BodyReadConfiguration, Stream>(this.GetBodyConversionStreamCallback));
			this.propertyDefinitionFactory = new CoreObjectPropertyDefinitionFactory(coreItem.Session, coreItem.PropertyBag);
			this.CoreItem.BeforeFlush += this.OnBeforeFlush;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0001B48E File Offset: 0x0001968E
		public override Encoding String8Encoding
		{
			get
			{
				return this.string8Encoding;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0001B496 File Offset: 0x00019696
		protected override IPropertyDefinitionFactory PropertyDefinitionFactory
		{
			get
			{
				return this.propertyDefinitionFactory;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0001B49E File Offset: 0x0001969E
		protected override IStorageObjectProperties StorageObjectProperties
		{
			get
			{
				return this.bestBodyCoreObjectProperties;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001B4A6 File Offset: 0x000196A6
		public ICorePropertyBag PropertyBag
		{
			get
			{
				return this.coreItemReference.ReferencedObject.PropertyBag;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public override StoreSession Session
		{
			get
			{
				return this.coreItemReference.ReferencedObject.Session;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001B4CA File Offset: 0x000196CA
		public override Schema Schema
		{
			get
			{
				return MessageItemSchema.Instance;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0001B4D1 File Offset: 0x000196D1
		public CoreItem CoreItem
		{
			get
			{
				return this.coreItemReference.ReferencedObject;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0001B4DE File Offset: 0x000196DE
		public PropertyTag[] ExtraRecipientPropertyTags
		{
			get
			{
				return this.recipientTranslator.ExtraPropertyTags;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0001B4EB File Offset: 0x000196EB
		protected override bool SupportsPropertyProblems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001B4EE File Offset: 0x000196EE
		protected ReferenceCount<CoreItem> ReferenceCoreItem
		{
			get
			{
				return this.coreItemReference;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0001B4F6 File Offset: 0x000196F6
		private ReferenceCount<CoreItem> CoreItemReference
		{
			get
			{
				return this.coreItemReference;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001B500 File Offset: 0x00019700
		public void OnBeforeFlush()
		{
			if (!this.bestBodyCoreObjectProperties.BodyHelper.IsOpeningStream)
			{
				this.bestBodyCoreObjectProperties.ResetBody();
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).RpcClientAccess.DetectCharsetAndConvertHtmlBodyOnSave.Enabled && this.CoreItem.PropertyBag.IsPropertyDirty(ItemSchema.HtmlBody) && !this.CoreItem.PropertyBag.IsPropertyDirty(ItemSchema.InternetCpid) && ItemCharsetDetector.IsMultipleLanguageCodePage(this.CoreItem.PropertyBag.GetValueOrDefault<int>(ItemSchema.InternetCpid)))
			{
				this.CoreItem.Body.ResetBodyFormat();
				this.CoreItem.Body.ForceRedetectHtmlBodyCharset = true;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public Attachment CreateAttachment()
		{
			CoreAttachmentCollection attachmentCollection = ((ICoreItem)this.CoreItem).AttachmentCollection;
			Attachment result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = attachmentCollection.Create(AttachmentType.NoAttachment);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				int attachmentNumber = coreAttachment.AttachmentNumber;
				Attachment attachment = new Attachment(coreAttachment, this.CoreItemReference, base.LogonObject, this.string8Encoding);
				if (this.ignorePropertySaveErrors)
				{
					attachment.IgnorePropertySaveErrors();
				}
				disposeGuard.Success();
				result = attachment;
			}
			return result;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001B644 File Offset: 0x00019844
		public bool DeleteAttachment(uint attachmentNumber)
		{
			CoreAttachmentCollection attachmentCollection = this.CoreItem.AttachmentCollection;
			foreach (AttachmentHandle attachmentHandle in attachmentCollection)
			{
				if (attachmentHandle.AttachNumber == (int)attachmentNumber)
				{
					attachmentCollection.Remove(attachmentHandle);
					this.CoreItem.Flush(SaveMode.FailOnAnyConflict);
					this.CoreItem.PropertyBag.Clear();
					this.CoreItem.PropertyBag.Load(null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001B6D8 File Offset: 0x000198D8
		public void ModifyRecipients(PropertyTag[] extraPropertyTags, IEnumerable<RecipientRow> recipientRows, Action<RecipientTranslationException> recipientTranslationFailureObserver)
		{
			Util.ThrowOnNullArgument(extraPropertyTags, "extraPropertyTags");
			Util.ThrowOnNullArgument(recipientRows, "recipientRows");
			this.recipientTranslator.ExtraPropertyTags = extraPropertyTags;
			foreach (RecipientRow recipientRow in recipientRows)
			{
				try
				{
					this.recipientTranslator.SetRecipientRow(recipientRow);
				}
				catch (RecipientTranslationException obj)
				{
					if (recipientTranslationFailureObserver != null)
					{
						recipientTranslationFailureObserver(obj);
					}
					this.TryStubRecipient(recipientRow);
				}
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001B76C File Offset: 0x0001996C
		public AttachmentView GetAttachmentTable(Logon logon, TableFlags tableFlags, NotificationHandler notificationHandler, ServerObjectHandle returnNotificationHandle)
		{
			RopHandler.CheckEnum<TableFlags>(tableFlags);
			TableFlags tableFlags2 = TableFlags.DeferredErrors | TableFlags.NoNotifications | TableFlags.MapiUnicode | TableFlags.SuppressNotifications;
			if ((byte)(tableFlags & ~tableFlags2) != 0)
			{
				throw new RopExecutionException(string.Format("Flags {0} not supported on AttachmentViews.", tableFlags), (ErrorCode)2147746050U);
			}
			return new AttachmentView(logon, this.CoreItemReference, TableFlags.NoNotifications | tableFlags, notificationHandler, returnNotificationHandle);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001B7BC File Offset: 0x000199BC
		public void RemoveAllRecipients()
		{
			this.recipientTranslator.RemoveAllRecipients();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001B7CC File Offset: 0x000199CC
		public bool TryOpenAttachment(OpenMode openMode, uint attachmentNumber, out Attachment attachment)
		{
			if (openMode != OpenMode.ReadOnly && openMode != OpenMode.ReadWrite && openMode != OpenMode.BestAccess)
			{
				throw new RopExecutionException(string.Format("OpenMode {0} not supported.", openMode), (ErrorCode)2147746050U);
			}
			CoreAttachmentCollection attachmentCollection = ((ICoreItem)this.CoreItem).AttachmentCollection;
			foreach (AttachmentHandle attachmentHandle in attachmentCollection)
			{
				if (attachmentHandle.AttachNumber == (int)attachmentNumber)
				{
					using (DisposeGuard disposeGuard = default(DisposeGuard))
					{
						CoreAttachment coreAttachment = attachmentCollection.Open(attachmentHandle);
						disposeGuard.Add<CoreAttachment>(coreAttachment);
						if (ExTraceGlobals.MessageTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							string arg = (this.PropertyBag.TryGetProperty(CoreItemSchema.Subject) as string) ?? "<null>";
							ExTraceGlobals.MessageTracer.TraceDebug<uint, string, string>((long)this.GetHashCode(), "TryOpenAttachment. #{0}. OpenMode: {1}, ParentMessage: \"{2}\".", attachmentNumber, openMode.ToString(), arg);
						}
						attachment = new Attachment(coreAttachment, this.CoreItemReference, base.LogonObject, this.string8Encoding);
						if (this.ignorePropertySaveErrors)
						{
							attachment.IgnorePropertySaveErrors();
						}
						disposeGuard.Success();
						return true;
					}
				}
			}
			attachment = null;
			return false;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001B918 File Offset: 0x00019B18
		public RecipientCollector ReadRecipients(uint recipientRowId, PropertyTag[] extraUnicodePropertyTags, Func<PropertyTag[], RecipientCollector> createRecipientCollectorDelegate)
		{
			Util.ThrowOnNullArgument(extraUnicodePropertyTags, "extraUnicodePropertyTags");
			RecipientCollector result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				this.recipientTranslator.ExtraUnicodePropertyTags = extraUnicodePropertyTags;
				RecipientCollector recipientCollector = createRecipientCollectorDelegate(this.recipientTranslator.ExtraPropertyTags);
				disposeGuard.Add<RecipientCollector>(recipientCollector);
				bool flag = false;
				foreach (RecipientRow row in this.recipientTranslator.GetRecipientRows((int)recipientRowId))
				{
					if (!recipientCollector.TryAddRecipientRow(row))
					{
						if (!flag)
						{
							throw new RopExecutionException("Unable to add even one recipient to the RecipientCollector.", ErrorCode.BufferTooSmall);
						}
						break;
					}
					else
					{
						flag = true;
					}
				}
				disposeGuard.Success();
				result = recipientCollector;
			}
			return result;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001B9EC File Offset: 0x00019BEC
		public RecipientCollector ReloadCachedInformation(PropertyTag[] extraUnicodePropertyTags, Func<MessageHeader, PropertyTag[], Encoding, RecipientCollector> createRecipientCollectorDelegate)
		{
			Util.ThrowOnNullArgument(extraUnicodePropertyTags, "extraUnicodePropertyTags");
			MessageHeader messageHeader = Message.GetMessageHeader(this.CoreItem);
			RecipientCollector result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				this.recipientTranslator.ExtraUnicodePropertyTags = extraUnicodePropertyTags;
				RecipientCollector recipientCollector = createRecipientCollectorDelegate(messageHeader, this.recipientTranslator.ExtraPropertyTags, String8Encodings.TemporaryDefault);
				disposeGuard.Add<RecipientCollector>(recipientCollector);
				foreach (RecipientRow row in this.recipientTranslator.GetRecipientRows(0))
				{
					if (!recipientCollector.TryAddRecipientRow(row))
					{
						break;
					}
				}
				disposeGuard.Success();
				result = recipientCollector;
			}
			return result;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001BABC File Offset: 0x00019CBC
		public virtual StoreId SaveChanges(SaveChangesMode saveChangesMode)
		{
			if (ExTraceGlobals.MessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string arg = (this.PropertyBag.TryGetProperty(ItemSchema.Subject) as string) ?? "<null>";
				ExTraceGlobals.MessageTracer.TraceDebug<SaveChangesMode, string>((long)this.GetHashCode(), "SaveChangesMessage. SaveChangesMode = {0}, Subject: \"{1}\".", saveChangesMode, arg);
			}
			if ((byte)(saveChangesMode & (SaveChangesMode.TransportDelivery | SaveChangesMode.IMAPChange | SaveChangesMode.ForceNotificationPublish)) != 0)
			{
				throw new RopExecutionException(string.Format("SaveChangesMode {0} is not supported.", saveChangesMode), (ErrorCode)2147746050U);
			}
			if ((byte)(saveChangesMode & SaveChangesMode.KeepOpenReadOnly) == 1 && (byte)(saveChangesMode & SaveChangesMode.KeepOpenReadWrite) == 2)
			{
				throw new RopExecutionException(string.Format("SaveChangesMode {0} is not supported.", saveChangesMode), (ErrorCode)2147746050U);
			}
			Feature.Stubbed(54480, "SaveChangesMode flags not supported. SaveChangesModes=" + saveChangesMode);
			this.bestBodyCoreObjectProperties.ResetBody();
			if (this.SaveChangesToLinkedDocumentLibraryIfNecessary())
			{
				((MailboxSession)this.Session).TryToSyncSiteMailboxNow();
			}
			else
			{
				SaveMode saveMode = ((byte)(saveChangesMode & SaveChangesMode.ForceSave) == 4) ? SaveMode.NoConflictResolutionForceSave : SaveMode.ResolveConflicts;
				ConflictResolutionResult conflictResolutionResult = this.CoreItem.Save(saveMode);
				if (conflictResolutionResult.SaveStatus == SaveResult.SuccessWithoutSaving)
				{
					return StoreId.Empty;
				}
				if (conflictResolutionResult.SaveStatus != SaveResult.Success && conflictResolutionResult.SaveStatus != SaveResult.SuccessWithConflictResolution)
				{
					throw new RopExecutionException(string.Format("SaveChangesMessage failed due to conflict {0}.", conflictResolutionResult), (ErrorCode)2147746057U);
				}
			}
			this.CoreItem.PropertyBag.Load(null);
			return this.GetMessageIdAfterSave();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001BC04 File Offset: 0x00019E04
		public bool SetReadFlag(SetReadFlagFlags flags)
		{
			if ((byte)(flags & ~(SetReadFlagFlags.SuppressReceipt | SetReadFlagFlags.ClearReadFlag | SetReadFlagFlags.DeferredErrors | SetReadFlagFlags.GenerateReceiptOnly | SetReadFlagFlags.ClearReadNotificationPending | SetReadFlagFlags.ClearNonReadNotificationPending)) != 0)
			{
				throw new RopExecutionException(string.Format("SetReadFlagFlags {0} is not supported.", flags), (ErrorCode)2147746050U);
			}
			bool result;
			this.CoreItem.SetReadFlag((int)flags, out result);
			return result;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001BC48 File Offset: 0x00019E48
		public void SubmitMessage(SubmitMessageFlags submitFlags)
		{
			if ((byte)(submitFlags & ~(SubmitMessageFlags.Preprocess | SubmitMessageFlags.NeedsSpooler)) != 0)
			{
				throw new RopExecutionException(string.Format("SubmitMessageFlags {0} is not supported.", submitFlags), (ErrorCode)2147746050U);
			}
			SubmitMessageFlags submitMessageFlags = SubmitMessageFlags.None;
			if ((byte)(submitFlags & SubmitMessageFlags.Preprocess) != 0)
			{
				submitMessageFlags |= SubmitMessageFlags.Preprocess;
			}
			if ((byte)(submitFlags & SubmitMessageFlags.NeedsSpooler) != 0)
			{
				submitMessageFlags |= SubmitMessageFlags.NeedsSpooler;
			}
			this.UpdateSecureSubmitFlags();
			this.bestBodyCoreObjectProperties.ResetBody();
			this.CoreItem.Submit(submitMessageFlags);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001BCB0 File Offset: 0x00019EB0
		public PropertyValue[] TransportSend()
		{
			this.UpdateSecureSubmitFlags();
			this.bestBodyCoreObjectProperties.ResetBody();
			PropertyDefinition[] properties;
			object[] xsoPropertyValues;
			this.CoreItem.TransportSend(out properties, out xsoPropertyValues);
			bool flag = true;
			ICollection<PropertyTag> propertyTags = MEDSPropertyTranslator.PropertyTagsFromPropertyDefinitions<PropertyDefinition>(this.Session, properties, flag);
			PropertyValue[] array = MEDSPropertyTranslator.TranslatePropertyValues(this.Session, propertyTags, xsoPropertyValues, flag);
			base.PropertyConverter.ConvertPropertyValuesToClientAndSuppressClientSide(this.Session, this.StorageObjectProperties, array, null, base.ClientSideProperties);
			return array;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001BD1F File Offset: 0x00019F1F
		internal static MessageHeader GetMessageHeader(ICoreItem coreItem)
		{
			return new MessageHeader(true, true, coreItem.PropertyBag.GetValueOrDefault<string>(CoreItemSchema.SubjectPrefix), coreItem.PropertyBag.GetValueOrDefault<string>(CoreItemSchema.NormalizedSubject), (ushort)coreItem.Recipients.Count);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001BD54 File Offset: 0x00019F54
		internal void IgnorePropertySaveErrors()
		{
			this.ignorePropertySaveErrors = true;
			this.coreItemReference.ReferencedObject.SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreAccessDeniedErrors);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001BD78 File Offset: 0x00019F78
		internal bool AddRecipients(RecipientCollector recipientCollector)
		{
			bool result = false;
			foreach (RecipientRow row in this.recipientTranslator.GetRecipientRows(0))
			{
				if (!recipientCollector.TryAddRecipientRow(row))
				{
					break;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		protected override FastTransferUpload InternalFastTransferDestinationCopyTo()
		{
			this.IgnorePropertySaveErrors();
			FastTransferUpload result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMessage message = this.CreateUploadMessageAdaptor();
				disposeGuard.Add<IMessage>(message);
				FastTransferMessageCopyTo fastTransferMessageCopyTo = new FastTransferMessageCopyTo(false, message, true);
				disposeGuard.Add<FastTransferMessageCopyTo>(fastTransferMessageCopyTo);
				FastTransferUpload fastTransferUpload = new FastTransferUpload(fastTransferMessageCopyTo, PropertyFilterFactory.IncludeAllFactory, base.LogonObject);
				disposeGuard.Add<FastTransferUpload>(fastTransferUpload);
				disposeGuard.Success();
				result = fastTransferUpload;
			}
			return result;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001BE58 File Offset: 0x0001A058
		protected override FastTransferUpload InternalFastTransferDestinationCopyProperties()
		{
			return this.InternalFastTransferDestinationCopyTo();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001BE60 File Offset: 0x0001A060
		protected override FastTransferDownload InternalFastTransferSourceCopyTo(bool isShallowCopy, FastTransferCopyFlag flags, FastTransferSendOption sendOptions, PropertyTag[] excludePropertyTags)
		{
			if (ExTraceGlobals.MessageTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string arg = (this.PropertyBag.TryGetProperty(ItemSchema.Subject) as string) ?? "<null>";
				ExTraceGlobals.MessageTracer.TraceDebug<string>((long)this.GetHashCode(), "CreateSourceCopyToDownload. Subject: \"{0}\"", arg);
			}
			DownloadBodyOption downloadBodyOption = ((flags & FastTransferCopyFlag.BestBody) == FastTransferCopyFlag.BestBody) ? DownloadBodyOption.BestBodyOnly : DownloadBodyOption.AllBodyProperties;
			if (downloadBodyOption == DownloadBodyOption.BestBodyOnly)
			{
				BodyHelper.RemoveBodyProperties(ref excludePropertyTags);
			}
			return this.InternalFastTransferSourceCopy(isShallowCopy, downloadBodyOption, sendOptions, false, excludePropertyTags, false);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001BEDB File Offset: 0x0001A0DB
		protected override FastTransferDownload InternalFastTransferSourceCopyProperties(bool isShallowCopy, FastTransferCopyPropertiesFlag flags, FastTransferSendOption sendOptions, PropertyTag[] includePropertyTags)
		{
			return this.InternalFastTransferSourceCopy(isShallowCopy, DownloadBodyOption.AllBodyProperties, sendOptions, true, includePropertyTags, true);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		protected override PropertyError[] InternalCopyTo(PropertyServerObject destinationPropertyServerObject, CopySubObjects copySubObjects, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] excludeProperties)
		{
			Message message = RopHandler.Downcast<Message>(destinationPropertyServerObject);
			this.PrepareForCopy(message);
			return this.CoreItem.CopyItem(message.CoreItem, copyPropertiesFlags, copySubObjects, excludeProperties);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001BF1C File Offset: 0x0001A11C
		protected override PropertyError[] InternalCopyProperties(PropertyServerObject destinationPropertyServerObject, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] properties)
		{
			Message message = RopHandler.Downcast<Message>(destinationPropertyServerObject);
			this.PrepareForCopy(message);
			return this.CoreItem.CopyProperties(message.CoreItem, copyPropertiesFlags, properties);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001BF4A File Offset: 0x0001A14A
		protected override void FixBodyPropertiesIfNeeded(PropertyValue[] values)
		{
			this.bestBodyCoreObjectProperties.BodyHelper.FixupProperties(values, FixupMapping.GetProperties);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001BF60 File Offset: 0x0001A160
		protected override bool TryGetOneOffPropertyStream(PropertyTag propertyTag, OpenMode openMode, bool isAppend, out Stream momtStream, out uint length)
		{
			momtStream = null;
			length = 0U;
			if (propertyTag.IsBodyProperty() && (openMode == OpenMode.ReadOnly || openMode == OpenMode.ReadWrite) && this.bestBodyCoreObjectProperties.BodyHelper.IsConversionNeeded(propertyTag))
			{
				try
				{
					using (DisposeGuard disposeGuard = default(DisposeGuard))
					{
						using (StreamSource streamSource = this.GetStreamSource())
						{
							if (openMode == OpenMode.ReadOnly)
							{
								momtStream = Message.BuildBodyConversionStream(base.LogonObject, streamSource, propertyTag, isAppend, this.bestBodyCoreObjectProperties.BodyHelper);
							}
							else
							{
								momtStream = Message.BuildUpgradeableBodyConversionStream(base.LogonObject, streamSource, propertyTag, isAppend, this.String8Encoding, this.bestBodyCoreObjectProperties.BodyHelper);
							}
							disposeGuard.Add<Stream>(momtStream);
						}
						length = momtStream.GetSize();
						disposeGuard.Success();
						return true;
					}
				}
				catch (ObjectNotFoundException)
				{
				}
			}
			return base.TryGetOneOffPropertyStream(propertyTag, openMode, isAppend, out momtStream, out length);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001C068 File Offset: 0x0001A268
		protected override bool ShouldSkipPropertyChange(StorePropertyDefinition propertyDefinition)
		{
			bool isLinked = TeamMailboxClientOperations.IsLinked(this.PropertyBag);
			return MessagePropertyRestriction.Instance.ShouldBlock(propertyDefinition, isLinked);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001C090 File Offset: 0x0001A290
		protected virtual StoreId GetMessageIdAfterSave()
		{
			ICoreItem coreItem = this.CoreItem;
			return new StoreId(coreItem.Session.IdConverter.GetMidFromMessageId(coreItem.StoreObjectId));
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		protected virtual MessageAdaptor CreateDownloadMessageAdaptor(DownloadBodyOption downloadBodyOption, FastTransferSendOption sendOptions, bool isFastTransferCopyProperties)
		{
			this.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
			return new MessageAdaptor(this.bestBodyCoreObjectProperties, this.ReferenceCoreItem, new MessageAdaptor.Options
			{
				IsReadOnly = true,
				IsEmbedded = false,
				DownloadBodyOption = downloadBodyOption,
				IsUpload = sendOptions.IsUpload(),
				IsFastTransferCopyProperties = isFastTransferCopyProperties
			}, base.LogonObject.LogonString8Encoding, sendOptions.WantUnicode(), null);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001C138 File Offset: 0x0001A338
		protected virtual MessageAdaptor CreateUploadMessageAdaptor()
		{
			return new MessageAdaptor(this.bestBodyCoreObjectProperties, this.ReferenceCoreItem, new MessageAdaptor.Options
			{
				IsReadOnly = false,
				IsEmbedded = false,
				DownloadBodyOption = DownloadBodyOption.AllBodyProperties
			}, base.LogonObject.LogonString8Encoding, true, base.LogonObject);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001C18C File Offset: 0x0001A38C
		protected virtual bool SaveChangesToLinkedDocumentLibraryIfNecessary()
		{
			bool result;
			try
			{
				result = TeamMailboxExecutionHelper.SaveChangesToLinkedDocumentLibraryIfNecessary(this.CoreItem, base.LogonObject);
			}
			catch (StoragePermanentException e)
			{
				TeamMailboxExecutionHelper.LogServerFailures(this.CoreItem, base.LogonObject, e);
				throw;
			}
			catch (StorageTransientException e2)
			{
				TeamMailboxExecutionHelper.LogServerFailures(this.CoreItem, base.LogonObject, e2);
				throw;
			}
			return result;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		protected override StreamSource GetStreamSource()
		{
			return new StreamSource<CoreItem>(this.coreItemReference, (CoreItem coreItem) => coreItem.PropertyBag);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001C226 File Offset: 0x0001A426
		protected override PropertyTag[] AdditionalPropertiesForGetPropertiesAll(bool useUnicodeType)
		{
			if (useUnicodeType)
			{
				return BodyHelper.AllBodyPropertiesUnicode;
			}
			return BodyHelper.AllBodyPropertiesString8;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001C236 File Offset: 0x0001A436
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Message>(this);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001C240 File Offset: 0x0001A440
		protected override void InternalDispose()
		{
			ExTraceGlobals.MessageTracer.TraceDebug((long)this.GetHashCode(), "DisposeMessage.");
			this.CoreItem.BeforeFlush -= this.OnBeforeFlush;
			this.coreItemReference.Release();
			base.InternalDispose();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001C28C File Offset: 0x0001A48C
		private static Stream BuildConversionStream(PropertyTag propertyTag, BodyHelper bodyHelper)
		{
			return bodyHelper.GetConversionStream(propertyTag);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001C298 File Offset: 0x0001A498
		private static BodyConversionStream BuildBodyConversionStream(Logon logon, StreamSource streamSource, PropertyTag propertyTag, bool isAppend, BodyHelper bodyHelper)
		{
			BodyConversionStream result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				StreamSource streamSource2 = streamSource.Duplicate();
				disposeGuard.Add<StreamSource>(streamSource2);
				BodyConversionStream bodyConversionStream = new BodyConversionStream(new Func<PropertyTag, BodyHelper, Stream>(Message.BuildConversionStream), logon, streamSource2, propertyTag, isAppend, bodyHelper);
				disposeGuard.Add<BodyConversionStream>(bodyConversionStream);
				disposeGuard.Success();
				result = bodyConversionStream;
			}
			return result;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001C308 File Offset: 0x0001A508
		private static UpgradeableBodyConversionStream BuildUpgradeableBodyConversionStream(Logon logon, StreamSource streamSource, PropertyTag propertyTag, bool isAppend, Encoding string8Encoding, BodyHelper bodyHelper)
		{
			UpgradeableBodyConversionStream result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				StreamSource streamSource2 = streamSource.Duplicate();
				disposeGuard.Add<StreamSource>(streamSource2);
				UpgradeableBodyConversionStream upgradeableBodyConversionStream = new UpgradeableBodyConversionStream(logon, streamSource2, propertyTag, isAppend, string8Encoding, bodyHelper, new Func<Logon, StreamSource, PropertyTag, bool, BodyHelper, BodyConversionStream>(Message.BuildBodyConversionStream), new Func<Logon, StreamSource, PropertyTag, Encoding, PropertyStream>(Message.BuildPropertyStream));
				disposeGuard.Add<UpgradeableBodyConversionStream>(upgradeableBodyConversionStream);
				disposeGuard.Success();
				result = upgradeableBodyConversionStream;
			}
			return result;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001C388 File Offset: 0x0001A588
		private static PropertyStream BuildPropertyStream(Logon logon, StreamSource streamSource, PropertyTag propertyTag, Encoding string8Encoding)
		{
			PropertyStream result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Stream stream = streamSource.PropertyBag.OpenPropertyStream(BodyHelper.GetBodyPropertyDefinition(propertyTag.PropertyId), MEDSPropertyTranslator.OpenModeToPropertyOpenMode(OpenMode.Create, (ErrorCode)2147749887U));
				disposeGuard.Add<Stream>(stream);
				if (propertyTag.PropertyType == PropertyType.String8)
				{
					EncodedStream encodedStream = new EncodedStream(stream, string8Encoding, logon.ResourceTracker);
					disposeGuard.Add<EncodedStream>(encodedStream);
					stream = encodedStream;
				}
				StreamSource streamSource2 = streamSource.Duplicate();
				disposeGuard.Add<StreamSource>(streamSource2);
				PropertyStream propertyStream = new PropertyStream(stream, propertyTag.PropertyType, logon, streamSource2);
				disposeGuard.Add<PropertyStream>(propertyStream);
				disposeGuard.Success();
				result = propertyStream;
			}
			return result;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001C448 File Offset: 0x0001A648
		private static void PrepareMessageForCopy(Message message, SaveMode mode)
		{
			if (message.CoreItem.IsDirty)
			{
				ConflictResolutionResult conflictResolutionResult = message.CoreItem.Flush(mode);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new RopExecutionException(string.Format("Failed to flush properties on message item. {0}", conflictResolutionResult), (ErrorCode)2147746057U);
				}
				message.CoreItem.PropertyBag.Clear();
				message.CoreItem.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		private static CoreAttachment GetCoreAttachment(CoreItem coreItem)
		{
			AttachmentHandle attachmentHandle = null;
			if (coreItem.AttachmentCollection == null)
			{
				return null;
			}
			using (IEnumerator<AttachmentHandle> enumerator = coreItem.AttachmentCollection.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					AttachmentHandle attachmentHandle2 = enumerator.Current;
					attachmentHandle = attachmentHandle2;
				}
			}
			if (attachmentHandle == null)
			{
				return null;
			}
			PropertyDefinition[] preloadProperties = new PropertyDefinition[]
			{
				AttachmentSchema.AttachLongFileName
			};
			return coreItem.AttachmentCollection.Open(attachmentHandle, preloadProperties);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001C530 File Offset: 0x0001A730
		private FastTransferDownload InternalFastTransferSourceCopy(bool isShallowCopy, DownloadBodyOption downloadBodyOption, FastTransferSendOption sendOptions, bool isInclude, PropertyTag[] propertyTags, bool isFastTransferCopyProperties)
		{
			this.PrepareFastTransferSourceForCopy();
			FastTransferDownload result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMessage message = this.CreateDownloadMessageAdaptor(downloadBodyOption, sendOptions, isFastTransferCopyProperties);
				disposeGuard.Add<IMessage>(message);
				FastTransferMessageCopyTo fastTransferMessageCopyTo = new FastTransferMessageCopyTo(isShallowCopy, message, true);
				disposeGuard.Add<FastTransferMessageCopyTo>(fastTransferMessageCopyTo);
				FastTransferDownload fastTransferDownload = new FastTransferDownload(sendOptions, fastTransferMessageCopyTo, 1U, new PropertyFilterFactory(isShallowCopy, isInclude, propertyTags), base.LogonObject);
				disposeGuard.Success();
				result = fastTransferDownload;
			}
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
		private void TryStubRecipient(RecipientRow recipientRow)
		{
			this.recipientTranslator.TryStubRecipient(recipientRow);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001C5C7 File Offset: 0x0001A7C7
		private void PrepareForCopy(Message destinationMessage)
		{
			Message.PrepareMessageForCopy(this, SaveMode.NoConflictResolution);
			Message.PrepareMessageForCopy(destinationMessage, SaveMode.FailOnAnyConflict);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001C5D7 File Offset: 0x0001A7D7
		private void PrepareFastTransferSourceForCopy()
		{
			Message.PrepareMessageForCopy(this, SaveMode.NoConflictResolution);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		private void UpdateSecureSubmitFlags()
		{
			this.CoreItem.PropertyBag.SetProperty(CoreItemSchema.ClientSubmittedSecurely, base.LogonObject.Connection.IsEncrypted);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001C60C File Offset: 0x0001A80C
		private Stream GetBodyConversionStreamCallback(BodyReadConfiguration readConfiguration)
		{
			return this.CoreItem.Body.OpenReadStream(readConfiguration);
		}

		// Token: 0x04000131 RID: 305
		private readonly RecipientTranslator recipientTranslator;

		// Token: 0x04000132 RID: 306
		private readonly ReferenceCount<CoreItem> coreItemReference;

		// Token: 0x04000133 RID: 307
		private readonly Encoding string8Encoding;

		// Token: 0x04000134 RID: 308
		private readonly BestBodyCoreObjectProperties bestBodyCoreObjectProperties;

		// Token: 0x04000135 RID: 309
		private readonly CoreObjectPropertyDefinitionFactory propertyDefinitionFactory;

		// Token: 0x04000136 RID: 310
		private bool ignorePropertySaveErrors;
	}
}
