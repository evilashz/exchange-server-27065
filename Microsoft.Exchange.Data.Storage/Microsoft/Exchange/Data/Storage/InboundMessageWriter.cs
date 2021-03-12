using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005FB RID: 1531
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class InboundMessageWriter : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003EFE RID: 16126 RVA: 0x001063E0 File Offset: 0x001045E0
		internal InboundMessageWriter(ICoreItem item, InboundConversionOptions options, InboundAddressCache addressCache, ConversionLimitsTracker limitsTracker, MimeMessageLevel messageLevel)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.parent = null;
			this.isTopLevelMessage = (messageLevel == MimeMessageLevel.TopLevelMessage);
			this.SetItem(item, options, false);
			this.SetAddressCache(addressCache, false);
			this.SetLimitsTracker(limitsTracker);
			this.componentType = ConversionComponentType.Message;
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x00106438 File Offset: 0x00104638
		internal InboundMessageWriter(ICoreItem item, InboundConversionOptions options, MimeMessageLevel messageLevel)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.parent = null;
			this.isTopLevelMessage = (messageLevel == MimeMessageLevel.TopLevelMessage);
			this.SetItem(item, options, false);
			ConversionLimitsTracker conversionLimitsTracker = new ConversionLimitsTracker(options.Limits);
			InboundAddressCache addressCache = new InboundAddressCache(options, conversionLimitsTracker, messageLevel);
			this.SetAddressCache(addressCache, true);
			this.SetLimitsTracker(conversionLimitsTracker);
			this.componentType = ConversionComponentType.Message;
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x001064A4 File Offset: 0x001046A4
		private InboundMessageWriter(InboundMessageWriter parent, ICoreItem item)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.parent = parent;
			this.isTopLevelMessage = false;
			this.SetItem(item, parent.ConversionOptions, true);
			ConversionLimitsTracker conversionLimitsTracker = parent.LimitsTracker;
			conversionLimitsTracker.StartEmbeddedMessage();
			InboundAddressCache addressCache = new InboundAddressCache(parent.ConversionOptions, conversionLimitsTracker, MimeMessageLevel.AttachedMessage);
			this.SetAddressCache(addressCache, true);
			this.SetLimitsTracker(conversionLimitsTracker);
			this.componentType = ConversionComponentType.Message;
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06003F01 RID: 16129 RVA: 0x00106516 File Offset: 0x00104716
		internal ICoreItem CoreItem
		{
			get
			{
				this.CheckDisposed("InboundMessageWriter::get_Item");
				return this.coreItem;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06003F02 RID: 16130 RVA: 0x00106529 File Offset: 0x00104729
		internal InboundConversionOptions ConversionOptions
		{
			get
			{
				this.CheckDisposed("InboundMessageWriter::get_ConversionOptions");
				return this.conversionOptions;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x0010653C File Offset: 0x0010473C
		internal bool IsTopLevelMessage
		{
			get
			{
				this.CheckDisposed("InboundMessageWriter::get_IsTopLevelMessage");
				return this.isTopLevelMessage;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x0010654F File Offset: 0x0010474F
		internal bool IsTopLevelWriter
		{
			get
			{
				this.CheckDisposed("InboundMessageWriter::get_IsTopLevelWriter");
				return this.parent == null;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x00106565 File Offset: 0x00104765
		internal ConversionComponentType ComponentType
		{
			get
			{
				this.CheckDisposed("InboundMessageWriter::get_ComponentType");
				return this.componentType;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x00106578 File Offset: 0x00104778
		// (set) Token: 0x06003F07 RID: 16135 RVA: 0x0010658B File Offset: 0x0010478B
		internal bool ForceParticipantResolution
		{
			get
			{
				this.CheckDisposed("InboundMessageWriter::get_ForceParticipantResolution");
				return this.forceParticipantResolution;
			}
			set
			{
				this.CheckDisposed("InboundMessageWriter::set_ForceParticipantResolution");
				this.forceParticipantResolution = value;
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003F08 RID: 16136 RVA: 0x0010659F File Offset: 0x0010479F
		private ConversionLimitsTracker LimitsTracker
		{
			get
			{
				this.CheckDisposed("InboundMessageWriter::get_LimitsTracker");
				return this.limitsTracker;
			}
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x001065B2 File Offset: 0x001047B2
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<InboundMessageWriter>(this);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x001065BA File Offset: 0x001047BA
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x001065CF File Offset: 0x001047CF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x001065E0 File Offset: 0x001047E0
		internal void ClearRecipientTable()
		{
			this.CheckDisposed("ClearRecipientTable");
			MessageItem messageItem = this.coreItem as MessageItem;
			if (messageItem != null)
			{
				messageItem.Recipients.Clear();
			}
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x00106612 File Offset: 0x00104812
		internal void ClearAttachmentTable()
		{
			this.CheckDisposed("ClearAttachmentTable");
			this.coreItem.AttachmentCollection.RemoveAll();
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0010662F File Offset: 0x0010482F
		internal void StartNewRecipient()
		{
			this.CheckDisposed("StartNewRecipient");
			this.componentType = ConversionComponentType.Recipient;
			this.currentRecipient = new ConversionRecipientEntry();
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x00106650 File Offset: 0x00104850
		internal void EndRecipient()
		{
			this.CheckDisposed("EndRecipient");
			if (this.currentRecipient != null)
			{
				if (this.currentRecipient.Participant != null && ConvertUtils.IsRecipientTransmittable(this.currentRecipient.RecipientItemType))
				{
					this.conversionAddressCache.AddRecipient(this.currentRecipient);
				}
				else
				{
					StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "Tnef didn't have enough information to construct a Participant");
				}
				this.currentRecipient = null;
			}
			this.componentType = ConversionComponentType.Message;
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x001066C5 File Offset: 0x001048C5
		internal void StartNewAttachment()
		{
			this.CheckDisposed("StartNewAttachment");
			this.limitsTracker.CountMessageAttachment();
			this.attachment = new MessageAttachmentWriter(this);
			this.componentType = ConversionComponentType.FileAttachment;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x001066F0 File Offset: 0x001048F0
		internal void StartNewAttachment(int attachMethod)
		{
			this.CheckDisposed("StartNewAttachment");
			this.limitsTracker.CountMessageAttachment();
			this.attachment = new MessageAttachmentWriter(this, attachMethod);
			this.componentType = ConversionComponentType.FileAttachment;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x0010671C File Offset: 0x0010491C
		internal void EndAttachment()
		{
			this.CheckDisposed("EndAttachment");
			if (this.attachment == null)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "InboundMessageWriter::EndAttachment: the attachment is null.");
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, null);
			}
			this.attachment.SaveAttachment();
			this.attachment.Dispose();
			this.attachment = null;
			this.componentType = ConversionComponentType.Message;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00106778 File Offset: 0x00104978
		internal void SetProperty(StorePropertyDefinition property, object value)
		{
			this.CheckDisposed("MessageWriter::SetProperty");
			switch (this.componentType)
			{
			case ConversionComponentType.Message:
				this.SetMessageProperty(property, value);
				return;
			case ConversionComponentType.Recipient:
				this.SetRecipientProperty(property, value);
				return;
			case ConversionComponentType.FileAttachment:
				this.SetAttachmentProperty(property, value);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x001067C4 File Offset: 0x001049C4
		internal void SetSubjectProperty(NativeStorePropertyDefinition property, string value)
		{
			this.CheckDisposed("SetSubjectProperty");
			SubjectProperty.ModifySubjectProperty(CoreObject.GetPersistablePropertyBag(this.coreItem), property, value);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x001067E3 File Offset: 0x001049E3
		internal void SetAddressProperty(StorePropertyDefinition property, object value)
		{
			this.conversionAddressCache.SetProperty((NativeStorePropertyDefinition)property, value);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x001067F7 File Offset: 0x001049F7
		internal void DeleteMessageProperty(StorePropertyDefinition property)
		{
			this.CheckDisposed("DeleteMessageProperty");
			this.coreItem.PropertyBag.Delete(property);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x00106818 File Offset: 0x00104A18
		internal Stream OpenPropertyStream(StorePropertyDefinition property)
		{
			this.CheckDisposed("OpenPropertyStream");
			switch (this.componentType)
			{
			case ConversionComponentType.Message:
				return this.OpenMessagePropertyStream(property);
			case ConversionComponentType.FileAttachment:
				return this.OpenAttachmentPropertyStream(property);
			}
			StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundTnefTracer, "MessageWriter.OpenPropertyStream: can't open stream on the component");
			throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x00106870 File Offset: 0x00104A70
		internal Stream OpenOleAttachmentDataStream()
		{
			this.CheckDisposed("OpenOleAttachmentDataStream");
			return this.attachment.CreateOleAttachmentDataStream();
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x00106888 File Offset: 0x00104A88
		internal InboundMessageWriter OpenAttachedMessageWriter()
		{
			this.CheckDisposed("OpenAttachedMessage");
			ICoreItem coreItem = null;
			InboundMessageWriter inboundMessageWriter = null;
			try
			{
				coreItem = this.attachment.CreateAttachmentItem();
				inboundMessageWriter = new InboundMessageWriter(this, coreItem);
			}
			finally
			{
				if (inboundMessageWriter == null && coreItem != null)
				{
					coreItem.Dispose();
					coreItem = null;
				}
			}
			return inboundMessageWriter;
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x001068DC File Offset: 0x00104ADC
		internal void SuppressLimitChecks()
		{
			this.limitsTracker.SuppressLimitChecks();
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x001068EC File Offset: 0x00104AEC
		internal void UndoTnef()
		{
			this.CheckDisposed("UndoTnef");
			this.ClearRecipientTable();
			this.ClearAttachmentTable();
			foreach (PropertyDefinition propertyDefinition in InboundMessageWriter.UndoPropertyList)
			{
				this.coreItem.PropertyBag.Delete(propertyDefinition);
			}
			PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(this.CoreItem);
			IDirectPropertyBag directPropertyBag = persistablePropertyBag;
			directPropertyBag.SetValue(InternalSchema.ItemClass, "IPM.Note");
			MessageItem messageItem = this.coreItem as MessageItem;
			if (messageItem != null)
			{
				messageItem.ReplyTo.Clear();
			}
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0010697C File Offset: 0x00104B7C
		internal void Commit()
		{
			if (this.ownsAddressCache)
			{
				if (this.ForceParticipantResolution)
				{
					this.conversionAddressCache.Resolve();
				}
				else
				{
					this.conversionAddressCache.ReplyTo.Resync(true);
				}
				this.conversionAddressCache.CopyDataToItem(this.coreItem);
			}
			if (this.ownsItem)
			{
				this.CoreItem.CharsetDetector.DetectionOptions = this.ConversionOptions.DetectionOptions;
				this.CoreItem.Save(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x001069F8 File Offset: 0x00104BF8
		private void SetItem(ICoreItem coreItem, InboundConversionOptions options, bool ownsItem)
		{
			this.coreItem = coreItem;
			this.conversionOptions = options;
			this.ownsItem = ownsItem;
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x00106A0F File Offset: 0x00104C0F
		private void SetAddressCache(InboundAddressCache addressCache, bool ownsCache)
		{
			this.conversionAddressCache = addressCache;
			this.ownsAddressCache = ownsCache;
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x00106A1F File Offset: 0x00104C1F
		private void SetLimitsTracker(ConversionLimitsTracker limitsTracker)
		{
			this.limitsTracker = limitsTracker;
			if (this.IsTopLevelMessage)
			{
				this.limitsTracker.CountMessageBody();
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x00106A3B File Offset: 0x00104C3B
		private void SetMessageProperty(StorePropertyDefinition property, object value)
		{
			this.CheckDisposed("SetProperty");
			this.coreItem.PropertyBag.SetProperty(property, value);
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x00106A5A File Offset: 0x00104C5A
		private Stream OpenMessagePropertyStream(StorePropertyDefinition property)
		{
			return this.coreItem.PropertyBag.OpenPropertyStream(property, PropertyOpenMode.Create);
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x00106A6E File Offset: 0x00104C6E
		private void SetRecipientProperty(StorePropertyDefinition property, object value)
		{
			this.CheckDisposed("SetRecipientProperty");
			if (this.currentRecipient != null)
			{
				this.currentRecipient.SetProperty(property, value, true);
			}
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00106A91 File Offset: 0x00104C91
		private void SetAttachmentProperty(StorePropertyDefinition property, object value)
		{
			this.attachment.AddProperty(property, value);
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x00106AA0 File Offset: 0x00104CA0
		private Stream OpenAttachmentPropertyStream(StorePropertyDefinition property)
		{
			this.CheckDisposed("OpenAttachmentPropertyStream");
			return this.attachment.CreatePropertyStream(property);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x00106AB9 File Offset: 0x00104CB9
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(this.ToString());
			}
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00106AD6 File Offset: 0x00104CD6
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.InternalDispose(disposing);
				this.isDisposed = true;
			}
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00106AFC File Offset: 0x00104CFC
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.parent != null)
				{
					this.LimitsTracker.EndEmbeddedMessage();
					this.parent = null;
				}
				if (this.ownsItem && this.coreItem != null)
				{
					this.coreItem.Dispose();
					this.coreItem = null;
				}
				if (this.attachment != null)
				{
					this.attachment.Dispose();
					this.attachment = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x040021A1 RID: 8609
		private static readonly PropertyDefinition[] UndoPropertyList = new PropertyDefinition[]
		{
			InternalSchema.SenderAddressType,
			InternalSchema.SenderDisplayName,
			InternalSchema.SenderEmailAddress,
			InternalSchema.SenderEntryId,
			InternalSchema.SenderSearchKey,
			InternalSchema.SentRepresentingDisplayName,
			InternalSchema.SentRepresentingEmailAddress,
			InternalSchema.SentRepresentingEntryId,
			InternalSchema.SentRepresentingType,
			InternalSchema.SentRepresentingSearchKey,
			InternalSchema.SenderFlags,
			InternalSchema.SentRepresentingFlags,
			InternalSchema.RtfBody,
			InternalSchema.RtfInSync,
			InternalSchema.RtfSyncBodyCount,
			InternalSchema.RtfSyncBodyCrc,
			InternalSchema.RtfSyncBodyTag,
			InternalSchema.RtfSyncPrefixCount,
			InternalSchema.RtfSyncTrailingCount,
			InternalSchema.HtmlBody,
			InternalSchema.TextBody,
			InternalSchema.MapiSubject,
			InternalSchema.NormalizedSubjectInternal,
			InternalSchema.SubjectPrefixInternal,
			InternalSchema.ConversationIndex,
			InternalSchema.ConversationTopic
		};

		// Token: 0x040021A2 RID: 8610
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040021A3 RID: 8611
		private ICoreItem coreItem;

		// Token: 0x040021A4 RID: 8612
		private InboundAddressCache conversionAddressCache;

		// Token: 0x040021A5 RID: 8613
		private ConversionRecipientEntry currentRecipient;

		// Token: 0x040021A6 RID: 8614
		private MessageAttachmentWriter attachment;

		// Token: 0x040021A7 RID: 8615
		private InboundMessageWriter parent;

		// Token: 0x040021A8 RID: 8616
		private InboundConversionOptions conversionOptions;

		// Token: 0x040021A9 RID: 8617
		private ConversionLimitsTracker limitsTracker;

		// Token: 0x040021AA RID: 8618
		private ConversionComponentType componentType;

		// Token: 0x040021AB RID: 8619
		private bool ownsItem;

		// Token: 0x040021AC RID: 8620
		private bool ownsAddressCache;

		// Token: 0x040021AD RID: 8621
		private bool isTopLevelMessage;

		// Token: 0x040021AE RID: 8622
		private bool forceParticipantResolution;

		// Token: 0x040021AF RID: 8623
		private bool isDisposed;
	}
}
