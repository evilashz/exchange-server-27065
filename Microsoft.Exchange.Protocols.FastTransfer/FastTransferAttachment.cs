using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.FastTransfer;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000025 RID: 37
	internal class FastTransferAttachment : FastTransferPropertyBag, IAttachment, IDisposable, IAttachmentHandle
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000D6A3 File Offset: 0x0000B8A3
		public FastTransferAttachment(FastTransferDownloadContext downloadContext, MapiAttachment mapiAttachment, bool excludeProps, HashSet<StorePropTag> propList, bool topLevel, FastTransferCopyFlag flags) : base(downloadContext, mapiAttachment, excludeProps, propList)
		{
			this.topLevel = topLevel;
			this.flags = flags;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public FastTransferAttachment(FastTransferUploadContext uploadContext, MapiAttachment mapiAttachment, bool topLevel, FastTransferCopyFlag flags) : base(uploadContext, mapiAttachment)
		{
			this.topLevel = topLevel;
			this.flags = flags;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000D6D9 File Offset: 0x0000B8D9
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000D6E6 File Offset: 0x0000B8E6
		private MapiAttachment MapiAttachment
		{
			get
			{
				return (MapiAttachment)base.MapiPropBag;
			}
			set
			{
				base.MapiPropBag = value;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000D6EF File Offset: 0x0000B8EF
		public IAttachment GetAttachment()
		{
			return this;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
		public IMessage GetEmbeddedMessage()
		{
			if (base.ReadOnly)
			{
				MapiMessage mapiMessage;
				ErrorCode errorCode = this.MapiAttachment.OpenEmbeddedMessage(base.Context.CurrentOperationContext, MessageConfigureFlags.None, this.MapiAttachment.Logon.CodePage, out mapiMessage);
				if (errorCode != ErrorCode.NoError)
				{
					throw new StoreException((LID)63544U, errorCode);
				}
				ExTraceGlobals.SourceSendTracer.TraceDebug(0L, "Send Embedded Message");
				return new FastTransferMessage(base.DownloadContext, mapiMessage, true, null, false, false, false, this.flags);
			}
			else
			{
				MapiMessage mapiMessage;
				ErrorCode errorCode2 = this.MapiAttachment.OpenEmbeddedMessage(base.Context.CurrentOperationContext, MessageConfigureFlags.CreateNewMessage, this.MapiAttachment.Logon.CodePage, out mapiMessage);
				if (errorCode2 != ErrorCode.NoError)
				{
					throw new StoreException((LID)38968U, errorCode2);
				}
				ExTraceGlobals.SourceSendTracer.TraceDebug(0L, "Receive Embedded Message");
				return new FastTransferMessage(base.UploadContext, mapiMessage, false, this.flags);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public bool IsEmbeddedMessage
		{
			get
			{
				return this.MapiAttachment.IsEmbeddedMessage(base.Context.CurrentOperationContext);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000D808 File Offset: 0x0000BA08
		public IPropertyBag PropertyBag
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000D80B File Offset: 0x0000BA0B
		public void Save()
		{
			this.MapiAttachment.SaveChanges(base.Context.CurrentOperationContext);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000D823 File Offset: 0x0000BA23
		public int AttachmentNumber
		{
			get
			{
				return this.MapiAttachment.GetAttachmentNumber();
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000D830 File Offset: 0x0000BA30
		protected override List<Property> LoadAllPropertiesImp()
		{
			List<Property> list = base.LoadAllPropertiesImp();
			if (base.ForMoveUser)
			{
				FastTransferPropertyBag.AddNullPropertyIfNotPresent(list, PropTag.Attachment.ReplicaChangeNumber);
				FastTransferPropertyBag.ResetPropertyIfPresent(list, PropTag.Attachment.Inid);
			}
			return list;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000D864 File Offset: 0x0000BA64
		protected override Property GetPropertyImp(StorePropTag propTag)
		{
			if (base.ForMoveUser && propTag == PropTag.Attachment.Inid)
			{
				return new Property(PropTag.Attachment.LTID, this.MapiAttachment.Logon.StoreMailbox.GetNextObjectId(base.Context.CurrentOperationContext).To24ByteArray());
			}
			if (propTag == PropTag.Attachment.Content || propTag == PropTag.Attachment.ContentObj)
			{
				return Property.NotEnoughMemoryError(propTag);
			}
			return base.GetPropertyImp(propTag);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		protected override bool IncludeTag(StorePropTag propTag)
		{
			if (base.ForMoveUser && propTag.IsCategory(4))
			{
				return true;
			}
			if (!base.ForMoveUser && !base.ForUpload && 26112 <= propTag.PropId && propTag.PropId <= 26623)
			{
				return false;
			}
			ushort propId = propTag.PropId;
			if (propId == 3617)
			{
				return false;
			}
			if (propId != 4094)
			{
				if (propId == 14081)
				{
					if (this.IsEmbeddedMessage)
					{
						return false;
					}
				}
				return base.IncludeTag(propTag);
			}
			return false;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000D96A File Offset: 0x0000BB6A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferAttachment>(this);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000D972 File Offset: 0x0000BB72
		protected override void InternalDispose(bool isCalledFromDispose)
		{
			if (isCalledFromDispose && this.MapiAttachment != null && !this.topLevel)
			{
				this.MapiAttachment.Dispose();
				this.MapiAttachment = null;
			}
			base.InternalDispose(isCalledFromDispose);
		}

		// Token: 0x040000C2 RID: 194
		private bool topLevel;

		// Token: 0x040000C3 RID: 195
		private FastTransferCopyFlag flags;
	}
}
