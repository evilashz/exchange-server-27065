using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C3 RID: 195
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationAttachment : DisposeTrackableBase, IMigrationAttachment, IDisposable
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x0002A3FD File Offset: 0x000285FD
		internal MigrationAttachment(StreamAttachment attachment, PropertyOpenMode openMode) : this(attachment.GetContentStream(openMode), attachment.LastModifiedTime, null)
		{
			this.attachment = attachment;
			this.size = attachment.Size;
			this.readOnly = (openMode == PropertyOpenMode.ReadOnly);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002A430 File Offset: 0x00028630
		protected MigrationAttachment(Stream stream, ExDateTime lastModifiedTime, string id)
		{
			MigrationUtil.ThrowOnNullArgument(stream, "stream");
			this.stream = stream;
			this.lastModifiedTime = lastModifiedTime;
			this.size = 0L;
			this.Id = id;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002A460 File Offset: 0x00028660
		public ExDateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0002A468 File Offset: 0x00028668
		public long Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0002A470 File Offset: 0x00028670
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0002A478 File Offset: 0x00028678
		public string Id { get; private set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002A481 File Offset: 0x00028681
		public Stream Stream
		{
			get
			{
				base.CheckDisposed();
				return this.stream;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002A490 File Offset: 0x00028690
		public virtual void Save(string contentId)
		{
			base.CheckDisposed();
			if (!string.IsNullOrEmpty(contentId))
			{
				this.attachment.ContentId = contentId;
				this.attachment.IsInline = true;
			}
			this.stream.Dispose();
			this.stream = null;
			this.attachment.Save();
			this.attachment.Load(MigrationStoreObject.IdPropertyDefinition);
			this.Id = this.attachment.Id.ToBase64String();
			this.lastModifiedTime = this.attachment.LastModifiedTime;
			this.size = this.attachment.Size;
			this.stream = this.attachment.GetContentStream(PropertyOpenMode.Modify);
			MigrationLogger.Log(MigrationEventType.Information, "Saved attachment with id {0}", new object[]
			{
				this.Id
			});
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002A556 File Offset: 0x00028756
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

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002A58F File Offset: 0x0002878F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationAttachment>(this);
		}

		// Token: 0x04000401 RID: 1025
		private StreamAttachment attachment;

		// Token: 0x04000402 RID: 1026
		private Stream stream;

		// Token: 0x04000403 RID: 1027
		private ExDateTime lastModifiedTime;

		// Token: 0x04000404 RID: 1028
		private long size;

		// Token: 0x04000405 RID: 1029
		private readonly bool readOnly;
	}
}
