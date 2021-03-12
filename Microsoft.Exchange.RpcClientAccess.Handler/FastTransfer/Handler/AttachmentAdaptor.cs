using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200005A RID: 90
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AttachmentAdaptor : BaseObject, IAttachment, IDisposable
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0001C70D File Offset: 0x0001A90D
		internal AttachmentAdaptor(ReferenceCount<CoreAttachment> referenceAttachment, bool isReadOnly, Encoding string8Encoding, bool wantUnicode, bool isUpload)
		{
			this.isReadOnly = isReadOnly;
			this.string8Encoding = string8Encoding;
			this.wantUnicode = wantUnicode;
			this.isUpload = isUpload;
			this.referenceAttachment = referenceAttachment;
			this.referenceAttachment.AddRef();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001C746 File Offset: 0x0001A946
		protected override void InternalDispose()
		{
			this.referenceAttachment.Release();
			base.InternalDispose();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001C75A File Offset: 0x0001A95A
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AttachmentAdaptor>(this);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001C764 File Offset: 0x0001A964
		public IPropertyBag PropertyBag
		{
			get
			{
				base.CheckDisposed();
				if (this.attachmentPropertyBag == null)
				{
					this.attachmentPropertyBag = new CoreAttachmentPropertyBagAdaptor(this.referenceAttachment.ReferencedObject.PropertyBag, this.referenceAttachment.ReferencedObject.Session.Mailbox.CoreObject, this.string8Encoding, this.wantUnicode, this.isUpload);
				}
				return this.attachmentPropertyBag;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		public bool IsEmbeddedMessage
		{
			get
			{
				base.CheckDisposed();
				return this.referenceAttachment.ReferencedObject.AttachmentType == AttachmentType.EmbeddedMessage;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public IMessage GetEmbeddedMessage()
		{
			base.CheckDisposed();
			if (this.isReadOnly && !this.IsEmbeddedMessage)
			{
				throw new InvalidOperationException("GetEmbeddedMessage() should not be called on readonly attachments that do not represent an embedded message");
			}
			PropertyOpenMode openMode = this.isReadOnly ? PropertyOpenMode.ReadOnly : PropertyOpenMode.Create;
			ReferenceCount<CoreItem> referenceCount = ReferenceCount<CoreItem>.Assign((CoreItem)this.referenceAttachment.ReferencedObject.OpenEmbeddedItem(openMode, CoreObjectSchema.AllPropertiesOnStore));
			IMessage result;
			try
			{
				result = new MessageAdaptor(referenceCount, new MessageAdaptor.Options
				{
					IsReadOnly = this.isReadOnly,
					IsEmbedded = true,
					DownloadBodyOption = DownloadBodyOption.RtfOnly,
					IsUpload = this.isUpload
				}, this.string8Encoding, this.wantUnicode, null);
			}
			finally
			{
				referenceCount.Release();
			}
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		public void Save()
		{
			base.CheckDisposed();
			this.referenceAttachment.ReferencedObject.SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreAccessDeniedErrors);
			this.referenceAttachment.ReferencedObject.Save();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001C8D9 File Offset: 0x0001AAD9
		public int AttachmentNumber
		{
			get
			{
				base.CheckDisposed();
				return this.referenceAttachment.ReferencedObject.AttachmentNumber;
			}
		}

		// Token: 0x04000138 RID: 312
		private readonly ReferenceCount<CoreAttachment> referenceAttachment;

		// Token: 0x04000139 RID: 313
		private readonly bool isReadOnly;

		// Token: 0x0400013A RID: 314
		private readonly Encoding string8Encoding;

		// Token: 0x0400013B RID: 315
		private readonly bool wantUnicode;

		// Token: 0x0400013C RID: 316
		private readonly bool isUpload;

		// Token: 0x0400013D RID: 317
		private CoreAttachmentPropertyBagAdaptor attachmentPropertyBag;
	}
}
