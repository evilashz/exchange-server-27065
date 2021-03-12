using System;
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

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000055 RID: 85
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Attachment : PropertyServerObject
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0001A8C5 File Offset: 0x00018AC5
		internal Attachment(CoreAttachment coreAttachment, ReferenceCount<CoreItem> coreItemReference, Logon logon, Encoding string8Encoding) : this(coreAttachment, coreItemReference, logon, string8Encoding, ClientSideProperties.AttachmentInstance, PropertyConverter.Attachment)
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001A8DC File Offset: 0x00018ADC
		internal Attachment(CoreAttachment coreAttachment, ReferenceCount<CoreItem> coreItemReference, Logon logon, Encoding string8Encoding, ClientSideProperties clientSideProperties, PropertyConverter converter) : base(logon, clientSideProperties, converter)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.coreAttachmentReference = new ReferenceCount<CoreAttachment>(coreAttachment);
				this.coreItemReference = coreItemReference;
				this.coreItemReference.AddRef();
				this.propertyDefinitionFactory = new CoreObjectPropertyDefinitionFactory(coreAttachment.Session, coreAttachment.PropertyBag);
				this.storageObjectProperties = new CoreObjectProperties(coreAttachment.PropertyBag);
				this.string8Encoding = string8Encoding;
				disposeGuard.Success();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0001A974 File Offset: 0x00018B74
		public override Encoding String8Encoding
		{
			get
			{
				return this.string8Encoding;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001A97C File Offset: 0x00018B7C
		protected override IPropertyDefinitionFactory PropertyDefinitionFactory
		{
			get
			{
				return this.propertyDefinitionFactory;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0001A984 File Offset: 0x00018B84
		protected override IStorageObjectProperties StorageObjectProperties
		{
			get
			{
				return this.storageObjectProperties;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0001A98C File Offset: 0x00018B8C
		public ICorePropertyBag PropertyBag
		{
			get
			{
				return this.CoreAttachment.PropertyBag;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0001A999 File Offset: 0x00018B99
		public override StoreSession Session
		{
			get
			{
				return this.CoreAttachment.Session;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001A9A6 File Offset: 0x00018BA6
		public override Schema Schema
		{
			get
			{
				return AttachmentSchema.Instance;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001A9AD File Offset: 0x00018BAD
		internal CoreAttachment CoreAttachment
		{
			get
			{
				return this.coreAttachmentReference.ReferencedObject;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001A9BA File Offset: 0x00018BBA
		private CoreItem CoreItem
		{
			get
			{
				return this.coreItemReference.ReferencedObject;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0001A9C7 File Offset: 0x00018BC7
		protected override bool SupportsPropertyProblems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001A9CC File Offset: 0x00018BCC
		public EmbeddedMessage OpenEmbeddedMessage(OpenMode openMode, Encoding string8Encoding)
		{
			if (openMode == OpenMode.BestAccess)
			{
				if (this.CoreAttachment.IsReadOnly)
				{
					openMode = OpenMode.ReadOnly;
				}
				else
				{
					openMode = OpenMode.ReadWrite;
				}
			}
			PropertyOpenMode openMode2 = MEDSPropertyTranslator.OpenModeToPropertyOpenMode(openMode, (ErrorCode)2147746050U);
			if ((byte)(openMode & OpenMode.Create) == 0 && this.CoreAttachment.AttachmentType != AttachmentType.EmbeddedMessage)
			{
				throw new RopExecutionException("The attachment does not contain an embedded message.", (ErrorCode)2147746063U);
			}
			if (openMode != OpenMode.ReadOnly && this.CoreAttachment.IsReadOnly)
			{
				throw new RopExecutionException("The attachment is opened for read-only.", (ErrorCode)2147942405U);
			}
			EmbeddedMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreItem coreItem = (CoreItem)this.CoreAttachment.OpenEmbeddedItem(openMode2, new PropertyDefinition[0]);
				disposeGuard.Add<CoreItem>(coreItem);
				EmbeddedMessage embeddedMessage = new EmbeddedMessage(coreItem, base.LogonObject, string8Encoding);
				if (this.ignorePropertySaveErrors)
				{
					embeddedMessage.IgnorePropertySaveErrors();
				}
				disposeGuard.Success();
				result = embeddedMessage;
			}
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		public void SaveChanges(SaveChangesMode saveChangesMode)
		{
			if (ExTraceGlobals.AttachmentTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string arg = (this.PropertyBag.TryGetProperty(AttachmentSchema.DisplayName) as string) ?? "<null>";
				ExTraceGlobals.AttachmentTracer.TraceDebug<int, SaveChangesMode, string>((long)this.GetHashCode(), "SaveChangesAttachment. #{0}. SaveChangesMode = {1}, DisplayName: \"{2}\".", this.CoreAttachment.AttachmentNumber, saveChangesMode, arg);
			}
			if ((byte)(saveChangesMode & (SaveChangesMode.TransportDelivery | SaveChangesMode.IMAPChange | SaveChangesMode.ForceNotificationPublish)) != 0)
			{
				throw new RopExecutionException(string.Format("The mode is not supported. saveChangesMode = {0}.", saveChangesMode), (ErrorCode)2147746050U);
			}
			if ((byte)(saveChangesMode & SaveChangesMode.KeepOpenReadOnly) == 1 && (byte)(saveChangesMode & SaveChangesMode.KeepOpenReadWrite) == 2)
			{
				throw new RopExecutionException(string.Format("The special mode is not supported. saveChangesMode = {0}.", saveChangesMode), (ErrorCode)2147746050U);
			}
			Feature.Stubbed(54480, "SaveChangesMode flags not supported. SaveChangesModes=" + saveChangesMode);
			this.CoreAttachment.Save();
			this.CoreItem.Flush(SaveMode.FailOnAnyConflict);
			this.CoreItem.PropertyBag.Clear();
			this.CoreItem.PropertyBag.Load(null);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001ABB1 File Offset: 0x00018DB1
		internal void IgnorePropertySaveErrors()
		{
			this.ignorePropertySaveErrors = true;
			this.coreAttachmentReference.ReferencedObject.SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreAccessDeniedErrors);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		protected override FastTransferUpload InternalFastTransferDestinationCopyTo()
		{
			this.IgnorePropertySaveErrors();
			FastTransferUpload result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				bool isReadOnly = false;
				this.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
				AttachmentAdaptor attachmentAdaptor = new AttachmentAdaptor(this.coreAttachmentReference, isReadOnly, base.LogonObject.LogonString8Encoding, true, false);
				disposeGuard.Add<AttachmentAdaptor>(attachmentAdaptor);
				IFastTransferProcessor<FastTransferUploadContext> fastTransferProcessor = FastTransferAttachmentCopyTo.CreateUploadStateMachine(attachmentAdaptor);
				disposeGuard.Add<IFastTransferProcessor<FastTransferUploadContext>>(fastTransferProcessor);
				FastTransferUpload fastTransferUpload = new FastTransferUpload(fastTransferProcessor, PropertyFilterFactory.IncludeAllFactory, base.LogonObject);
				disposeGuard.Success();
				result = fastTransferUpload;
			}
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001AC74 File Offset: 0x00018E74
		protected override FastTransferDownload InternalFastTransferSourceCopyProperties(bool isShallowCopy, FastTransferCopyPropertiesFlag flags, FastTransferSendOption sendOptions, PropertyTag[] includedProperties)
		{
			if (flags != FastTransferCopyPropertiesFlag.None)
			{
				Feature.Stubbed(185369, "Support FastTransferCopyPropertiesFlag.Move, which is the only flag in FastTransferCopyPropertiesFlag.");
			}
			return this.InternalFastTransferSourceCopyOperation(isShallowCopy, FastTransferCopyFlag.None, sendOptions, true, includedProperties);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001AC94 File Offset: 0x00018E94
		protected override FastTransferUpload InternalFastTransferDestinationCopyProperties()
		{
			return this.InternalFastTransferDestinationCopyTo();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001AC9C File Offset: 0x00018E9C
		protected override FastTransferDownload InternalFastTransferSourceCopyTo(bool isShallowCopy, FastTransferCopyFlag flags, FastTransferSendOption options, PropertyTag[] excludedPropertyTags)
		{
			if (ExTraceGlobals.AttachmentTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string arg = (this.PropertyBag.TryGetProperty(AttachmentSchema.DisplayName) as string) ?? "<null>";
				ExTraceGlobals.AttachmentTracer.TraceDebug<int, string>((long)this.GetHashCode(), "CreateSourceCopyToDownload. #{0}, \"{1}\"", this.CoreAttachment.AttachmentNumber, arg);
			}
			return this.InternalFastTransferSourceCopyOperation(isShallowCopy, flags, options, false, excludedPropertyTags);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001AD04 File Offset: 0x00018F04
		protected override PropertyError[] InternalCopyTo(PropertyServerObject destinationPropertyServerObject, CopySubObjects copySubObjects, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] excludeProperties)
		{
			Attachment attachment = RopHandler.Downcast<Attachment>(destinationPropertyServerObject);
			if (attachment.PropertyBag.IsDirty)
			{
				attachment.FlushAndReload();
			}
			PropertyError[] result = this.CoreAttachment.CopyAttachment(attachment.CoreAttachment, copyPropertiesFlags, copySubObjects, excludeProperties);
			attachment.PropertyBag.Reload();
			return result;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001AD50 File Offset: 0x00018F50
		protected override PropertyError[] InternalCopyProperties(PropertyServerObject destinationPropertyServerObject, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] properties)
		{
			Attachment attachment = RopHandler.Downcast<Attachment>(destinationPropertyServerObject);
			if (attachment.PropertyBag.IsDirty)
			{
				attachment.FlushAndReload();
			}
			PropertyError[] result = this.CoreAttachment.CopyProperties(attachment.CoreAttachment, copyPropertiesFlags, properties);
			attachment.PropertyBag.Reload();
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001AD97 File Offset: 0x00018F97
		protected override bool ShouldSkipPropertyChange(StorePropertyDefinition propertyDefinition)
		{
			return TeamMailboxClientOperations.IsLinked(this.CoreAttachment) && AttachmentPropertyRestriction.Instance.ShouldBlock(propertyDefinition, true);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001ADBC File Offset: 0x00018FBC
		protected override StreamSource GetStreamSource()
		{
			return new StreamSource<CoreAttachment>(this.coreAttachmentReference, (CoreAttachment coreAttachment) => coreAttachment.PropertyBag);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		private FastTransferDownload InternalFastTransferSourceCopyOperation(bool isShallowCopy, FastTransferCopyFlag flags, FastTransferSendOption sendOptions, bool isInclusion, PropertyTag[] propertyTags)
		{
			FastTransferDownload result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				bool isReadOnly = true;
				this.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
				AttachmentAdaptor attachmentAdaptor = new AttachmentAdaptor(this.coreAttachmentReference, isReadOnly, base.LogonObject.LogonString8Encoding, sendOptions.WantUnicode(), sendOptions.IsUpload());
				disposeGuard.Add<AttachmentAdaptor>(attachmentAdaptor);
				IFastTransferProcessor<FastTransferDownloadContext> fastTransferProcessor = FastTransferAttachmentCopyTo.CreateDownloadStateMachine(attachmentAdaptor);
				disposeGuard.Add<IFastTransferProcessor<FastTransferDownloadContext>>(fastTransferProcessor);
				FastTransferDownload fastTransferDownload = new FastTransferDownload(sendOptions, fastTransferProcessor, 1U, new PropertyFilterFactory(isShallowCopy, isInclusion, propertyTags), base.LogonObject);
				disposeGuard.Success();
				result = fastTransferDownload;
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001AE94 File Offset: 0x00019094
		private void FlushAndReload()
		{
			this.CoreAttachment.Flush();
			this.PropertyBag.Reload();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001AEAC File Offset: 0x000190AC
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Attachment>(this);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001AEB4 File Offset: 0x000190B4
		protected override void InternalDispose()
		{
			this.coreAttachmentReference.Release();
			this.coreItemReference.Release();
			base.InternalDispose();
		}

		// Token: 0x04000121 RID: 289
		private readonly ReferenceCount<CoreAttachment> coreAttachmentReference;

		// Token: 0x04000122 RID: 290
		private readonly ReferenceCount<CoreItem> coreItemReference;

		// Token: 0x04000123 RID: 291
		private readonly CoreObjectPropertyDefinitionFactory propertyDefinitionFactory;

		// Token: 0x04000124 RID: 292
		private readonly CoreObjectProperties storageObjectProperties;

		// Token: 0x04000125 RID: 293
		private readonly Encoding string8Encoding;

		// Token: 0x04000126 RID: 294
		private bool ignorePropertySaveErrors;
	}
}
