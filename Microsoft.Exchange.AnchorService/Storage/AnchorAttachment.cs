using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorAttachment : DisposeTrackableBase
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00006BD4 File Offset: 0x00004DD4
		internal AnchorAttachment(AnchorContext anchorContext, StreamAttachment attachment, PropertyOpenMode openMode) : this(anchorContext, attachment.GetContentStream(openMode), attachment.LastModifiedTime, null)
		{
			this.attachment = attachment;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006BF2 File Offset: 0x00004DF2
		protected AnchorAttachment(AnchorContext anchorContext, Stream stream, ExDateTime lastModifiedTime, string id)
		{
			AnchorUtil.ThrowOnNullArgument(anchorContext, "anchorContext");
			AnchorUtil.ThrowOnNullArgument(stream, "stream");
			this.anchorContext = anchorContext;
			this.stream = stream;
			this.lastModifiedTime = lastModifiedTime;
			this.Id = id;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006C2D File Offset: 0x00004E2D
		public ExDateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006C35 File Offset: 0x00004E35
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00006C3D File Offset: 0x00004E3D
		public string Id { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006C46 File Offset: 0x00004E46
		public Stream Stream
		{
			get
			{
				base.CheckDisposed();
				return this.stream;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006C54 File Offset: 0x00004E54
		public virtual void Save(string contentId)
		{
			base.CheckDisposed();
			if (!string.IsNullOrEmpty(contentId))
			{
				this.attachment.ContentId = contentId;
				this.attachment.IsInline = true;
			}
			this.attachment.Save();
			this.attachment.Load(AnchorStoreObject.IdPropertyDefinition);
			this.Id = this.attachment.Id.ToBase64String();
			this.anchorContext.Logger.Log(MigrationEventType.Information, "Saved attachment with id {0}", new object[]
			{
				this.Id
			});
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006CDF File Offset: 0x00004EDF
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				if (this.attachment != null)
				{
					this.attachment.Dispose();
					this.attachment = null;
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006D18 File Offset: 0x00004F18
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AnchorAttachment>(this);
		}

		// Token: 0x04000083 RID: 131
		private StreamAttachment attachment;

		// Token: 0x04000084 RID: 132
		private Stream stream;

		// Token: 0x04000085 RID: 133
		private ExDateTime lastModifiedTime;

		// Token: 0x04000086 RID: 134
		private AnchorContext anchorContext;
	}
}
