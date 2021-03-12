using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008F1 RID: 2289
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttachmentInfo
	{
		// Token: 0x060055CA RID: 21962 RVA: 0x00162F84 File Offset: 0x00161184
		internal AttachmentInfo(StoreObjectId messageId, Attachment attachment)
		{
			this.fileName = attachment.FileName;
			this.fileExtension = attachment.FileExtension;
			this.displayName = attachment.DisplayName;
			this.contentType = attachment.ContentType;
			if (string.IsNullOrEmpty(this.contentType))
			{
				this.contentType = attachment.CalculatedContentType;
			}
			this.isInline = attachment.IsInline;
			this.size = attachment.Size;
			this.attachmentType = attachment.AttachmentType;
			this.messageId = messageId;
			this.attachmentId = attachment.Id;
			this.contentId = attachment.ContentId;
			this.lastModifiedTime = attachment.LastModifiedTime;
			this.contentLocation = attachment.ContentLocation;
			if (attachment.AttachmentType == AttachmentType.Stream)
			{
				StreamAttachment streamAttachment = attachment as StreamAttachment;
				if (streamAttachment != null)
				{
					this.imageThumbnail = streamAttachment.LoadAttachmentThumbnail();
					this.imageThumbnailHeight = streamAttachment.ImageThumbnailHeight;
					this.imageThumbnailWidth = streamAttachment.ImageThumbnailWidth;
					if (this.imageThumbnail != null)
					{
						this.salientRegions = streamAttachment.LoadAttachmentThumbnailSalientRegions();
					}
				}
			}
			if (attachment.AttachmentType == AttachmentType.EmbeddedMessage)
			{
				ItemAttachment itemAttachment = attachment as ItemAttachment;
				if (itemAttachment != null)
				{
					using (Item item = itemAttachment.GetItem())
					{
						this.embeddedItemClass = item.ClassName;
					}
				}
			}
			if (attachment.AttachmentType == AttachmentType.Reference)
			{
				ReferenceAttachment referenceAttachment = attachment as ReferenceAttachment;
				if (referenceAttachment != null)
				{
					this.attachLongPathName = referenceAttachment.AttachLongPathName;
					this.providerType = referenceAttachment.ProviderType;
				}
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x060055CB RID: 21963 RVA: 0x001630F4 File Offset: 0x001612F4
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x060055CC RID: 21964 RVA: 0x001630FC File Offset: 0x001612FC
		public string FileExtension
		{
			get
			{
				return this.fileExtension;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x060055CD RID: 21965 RVA: 0x00163104 File Offset: 0x00161304
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x0016310C File Offset: 0x0016130C
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x00163114 File Offset: 0x00161314
		public bool IsInline
		{
			get
			{
				return this.isInline;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x060055D0 RID: 21968 RVA: 0x0016311C File Offset: 0x0016131C
		public long Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x060055D1 RID: 21969 RVA: 0x00163124 File Offset: 0x00161324
		public byte[] ImageThumbnail
		{
			get
			{
				return this.imageThumbnail;
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x060055D2 RID: 21970 RVA: 0x0016312C File Offset: 0x0016132C
		public byte[] ImageThumbnailSalientRegions
		{
			get
			{
				return this.salientRegions;
			}
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x060055D3 RID: 21971 RVA: 0x00163134 File Offset: 0x00161334
		public int ImageThumbnailHeight
		{
			get
			{
				return this.imageThumbnailHeight;
			}
		}

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x060055D4 RID: 21972 RVA: 0x0016313C File Offset: 0x0016133C
		public int ImageThumbnailWidth
		{
			get
			{
				return this.imageThumbnailWidth;
			}
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x060055D5 RID: 21973 RVA: 0x00163144 File Offset: 0x00161344
		public StoreObjectId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x0016314C File Offset: 0x0016134C
		public AttachmentId AttachmentId
		{
			get
			{
				return this.attachmentId;
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x060055D7 RID: 21975 RVA: 0x00163154 File Offset: 0x00161354
		public AttachmentType AttachmentType
		{
			get
			{
				return this.attachmentType;
			}
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x060055D8 RID: 21976 RVA: 0x0016315C File Offset: 0x0016135C
		public string ContentId
		{
			get
			{
				return this.contentId;
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x00163164 File Offset: 0x00161364
		public Uri ContentLocation
		{
			get
			{
				return this.contentLocation;
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x060055DA RID: 21978 RVA: 0x0016316C File Offset: 0x0016136C
		public ExDateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x060055DB RID: 21979 RVA: 0x00163174 File Offset: 0x00161374
		public string EmbeddedItemClass
		{
			get
			{
				return this.embeddedItemClass;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x0016317C File Offset: 0x0016137C
		public string AttachLongPathName
		{
			get
			{
				return this.attachLongPathName;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x00163184 File Offset: 0x00161384
		public string ProviderType
		{
			get
			{
				return this.providerType;
			}
		}

		// Token: 0x04002E0F RID: 11791
		private readonly string displayName;

		// Token: 0x04002E10 RID: 11792
		private readonly string fileExtension;

		// Token: 0x04002E11 RID: 11793
		private readonly string fileName;

		// Token: 0x04002E12 RID: 11794
		private readonly string contentType;

		// Token: 0x04002E13 RID: 11795
		private readonly long size;

		// Token: 0x04002E14 RID: 11796
		private readonly bool isInline;

		// Token: 0x04002E15 RID: 11797
		private readonly AttachmentType attachmentType;

		// Token: 0x04002E16 RID: 11798
		private readonly StoreObjectId messageId;

		// Token: 0x04002E17 RID: 11799
		private readonly AttachmentId attachmentId;

		// Token: 0x04002E18 RID: 11800
		private readonly string contentId;

		// Token: 0x04002E19 RID: 11801
		private readonly Uri contentLocation;

		// Token: 0x04002E1A RID: 11802
		private readonly ExDateTime lastModifiedTime;

		// Token: 0x04002E1B RID: 11803
		private readonly byte[] imageThumbnail;

		// Token: 0x04002E1C RID: 11804
		private readonly string embeddedItemClass;

		// Token: 0x04002E1D RID: 11805
		private readonly string attachLongPathName;

		// Token: 0x04002E1E RID: 11806
		private readonly string providerType;

		// Token: 0x04002E1F RID: 11807
		private readonly byte[] salientRegions;

		// Token: 0x04002E20 RID: 11808
		private readonly int imageThumbnailHeight;

		// Token: 0x04002E21 RID: 11809
		private readonly int imageThumbnailWidth;
	}
}
