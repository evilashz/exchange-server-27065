using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000598 RID: 1432
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MessageAttachmentWriter : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003A9D RID: 15005 RVA: 0x000F0FF8 File Offset: 0x000EF1F8
		internal MessageAttachmentWriter(InboundMessageWriter messageWriter)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.messageWriter = messageWriter;
			this.coreAttachment = this.CoreItem.AttachmentCollection.InternalCreate(null);
			this.attachMethod = null;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000F1050 File Offset: 0x000EF250
		internal MessageAttachmentWriter(InboundMessageWriter messageWriter, int attachMethod)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.messageWriter = messageWriter;
			AttachmentType attachmentType = CoreAttachmentCollection.GetAttachmentType(new int?(attachMethod));
			this.coreAttachment = this.CoreItem.AttachmentCollection.Create(attachmentType);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000F1093 File Offset: 0x000EF293
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MessageAttachmentWriter>(this);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000F109B File Offset: 0x000EF29B
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000F10B0 File Offset: 0x000EF2B0
		internal Stream CreateOleAttachmentDataStream()
		{
			this.CheckDisposed("CreateOleAttachmentDataStream");
			this.ResetAttachMethod(6);
			return this.coreAttachment.PropertyBag.OpenPropertyStream(InternalSchema.AttachDataObj, PropertyOpenMode.Create);
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000F10DA File Offset: 0x000EF2DA
		internal Stream CreatePropertyStream(PropertyDefinition property)
		{
			this.CheckDisposed("CreatePropertyStream");
			if (property.Equals(InternalSchema.AttachDataBin))
			{
				this.ResetAttachMethod(1);
			}
			return this.coreAttachment.PropertyBag.OpenPropertyStream(property, PropertyOpenMode.Create);
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000F1110 File Offset: 0x000EF310
		internal ICoreItem CreateAttachmentItem()
		{
			this.CheckDisposed("CreateAttachmentItem");
			this.ResetAttachMethod(5);
			bool noMessageDecoding = this.coreAttachment.ParentCollection.ContainerItem.CharsetDetector.NoMessageDecoding;
			return this.coreAttachment.PropertyBag.OpenAttachedItem(PropertyOpenMode.Create, null, noMessageDecoding);
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000F1160 File Offset: 0x000EF360
		internal void SaveAttachment()
		{
			this.CheckDisposed("SaveAttachment");
			if (this.attachMethod == null)
			{
				this.ResetAttachMethod(1);
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<CoreAttachment>(this.coreAttachment);
				using (Attachment attachment = AttachmentCollection.CreateTypedAttachment(this.coreAttachment, null))
				{
					attachment.SaveFlags |= PropertyBagSaveFlags.IgnoreMapiComputedErrors;
					attachment.Save();
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000F120C File Offset: 0x000EF40C
		internal void AddProperty(PropertyDefinition propDefinition, object value)
		{
			this.CheckDisposed("AddProperty");
			if (propDefinition.Equals(InternalSchema.AttachMethod))
			{
				int newAttachMethod = (int)value;
				this.ResetAttachMethod(newAttachMethod);
			}
			if (!propDefinition.Equals(InternalSchema.AttachDataBin))
			{
				this.coreAttachment.PropertyBag[propDefinition] = value;
				return;
			}
			int? num = this.attachMethod;
			int valueOrDefault = num.GetValueOrDefault();
			if (num == null)
			{
				this.data = (byte[])value;
				return;
			}
			if (valueOrDefault != 1)
			{
				return;
			}
			this.coreAttachment.PropertyBag.SetProperty(propDefinition, value);
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000F129A File Offset: 0x000EF49A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000F12A9 File Offset: 0x000EF4A9
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000F12CE File Offset: 0x000EF4CE
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.coreAttachment != null)
				{
					this.coreAttachment.Dispose();
					this.coreAttachment = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000F1300 File Offset: 0x000EF500
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(this.ToString());
			}
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000F1320 File Offset: 0x000EF520
		private void ResetAttachMethod(int newAttachMethod)
		{
			this.CheckDisposed("TransformAttachment");
			if (this.attachMethod == newAttachMethod)
			{
				return;
			}
			if (this.attachMethod == 5 || (this.attachMethod != null && newAttachMethod == 5))
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundMimeTracer, "MessageAttachmentWriter::TransformAttachment: wrong attachment transformation.");
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, null);
			}
			IDirectPropertyBag propertyBag = this.coreAttachment.PropertyBag;
			this.attachMethod = new int?(newAttachMethod);
			propertyBag.SetValue(InternalSchema.AttachMethod, newAttachMethod);
			if (newAttachMethod == 1 && this.data != null)
			{
				propertyBag.SetValue(InternalSchema.AttachDataBin, this.data);
				this.data = null;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000F13EA File Offset: 0x000EF5EA
		private ICoreItem CoreItem
		{
			get
			{
				this.CheckDisposed("Item::get");
				return this.messageWriter.CoreItem;
			}
		}

		// Token: 0x04001F6C RID: 8044
		private bool isDisposed;

		// Token: 0x04001F6D RID: 8045
		private InboundMessageWriter messageWriter;

		// Token: 0x04001F6E RID: 8046
		private byte[] data;

		// Token: 0x04001F6F RID: 8047
		private CoreAttachment coreAttachment;

		// Token: 0x04001F70 RID: 8048
		private int? attachMethod;

		// Token: 0x04001F71 RID: 8049
		private readonly DisposeTracker disposeTracker;
	}
}
