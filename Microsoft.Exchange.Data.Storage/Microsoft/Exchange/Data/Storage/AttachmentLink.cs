using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000350 RID: 848
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttachmentLink
	{
		// Token: 0x060025B7 RID: 9655 RVA: 0x00097510 File Offset: 0x00095710
		internal AttachmentLink(Attachment attachment)
		{
			this.attachmentType = attachment.AttachmentType;
			this.attachmentId = attachment.Id;
			this.contentId = attachment.ContentId;
			this.contentBase = attachment.ContentBase;
			this.contentLocation = attachment.ContentLocation;
			this.filename = attachment.FileName;
			this.displayName = attachment.DisplayName;
			this.size = attachment.Size;
			this.originalIsInline = attachment.IsInline;
			this.markedInline = null;
			this.contentType = AttachmentLink.GetContentType(attachment);
			this.renderingPosition = attachment.GetValueOrDefault<int>(InternalSchema.RenderingPosition, -1);
			this.isHidden = attachment.GetValueOrDefault<bool>(InternalSchema.AttachCalendarHidden);
			this.isChanged = false;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000975D4 File Offset: 0x000957D4
		internal static string CreateContentId(ICoreItem containerItem, AttachmentId id, string domain)
		{
			byte[] array;
			if (containerItem != null && containerItem.Session != null && containerItem.Id != null && id != null)
			{
				array = Util.MergeArrays<byte>(new ICollection<byte>[]
				{
					containerItem.Id.GetBytes(),
					id.ToByteArray()
				});
				array = CryptoUtil.GetSha1Hash(array);
			}
			else
			{
				array = Guid.NewGuid().ToByteArray();
			}
			if (!string.IsNullOrEmpty(domain))
			{
				return string.Format("{0}@{1}", HexConverter.ByteArrayToHexString(array), domain);
			}
			return string.Format("{0}@1", HexConverter.ByteArrayToHexString(array));
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00097660 File Offset: 0x00095860
		private static string GetContentType(Attachment attachment)
		{
			string calculatedContentType = attachment.ContentType;
			if (calculatedContentType == null || calculatedContentType.Equals("application/octet-stream", StringComparison.OrdinalIgnoreCase))
			{
				calculatedContentType = attachment.CalculatedContentType;
			}
			return calculatedContentType;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00097690 File Offset: 0x00095890
		internal static AttachmentLink Find(AttachmentId attachmentId, IList<AttachmentLink> attachmentLinks)
		{
			if (attachmentId != null && attachmentLinks != null)
			{
				foreach (AttachmentLink attachmentLink in attachmentLinks)
				{
					if (attachmentId.Equals(attachmentLink.AttachmentId))
					{
						return attachmentLink;
					}
				}
			}
			return null;
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000976EC File Offset: 0x000958EC
		internal static ReadOnlyCollection<AttachmentLink> MergeAttachmentLinks(IList<AttachmentLink> existingLinks, CoreAttachmentCollection attachments)
		{
			IList<AttachmentLink> list;
			if (attachments != null)
			{
				list = ((existingLinks == null) ? new List<AttachmentLink>(attachments.Count) : new List<AttachmentLink>(existingLinks));
				ICollection<PropertyDefinition> preloadProperties = new PropertyDefinition[]
				{
					AttachmentSchema.AttachContentId
				};
				using (IEnumerator<AttachmentHandle> enumerator = attachments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AttachmentHandle handle = enumerator.Current;
						using (CoreAttachment coreAttachment = attachments.Open(handle, preloadProperties))
						{
							using (Attachment attachment = AttachmentCollection.CreateTypedAttachment(coreAttachment, null))
							{
								if (AttachmentLink.Find(attachment.Id, list) == null)
								{
									AttachmentLink item = new AttachmentLink(attachment);
									list.Add(item);
								}
							}
						}
					}
					goto IL_C5;
				}
			}
			list = ((existingLinks == null) ? new List<AttachmentLink>(0) : new List<AttachmentLink>(existingLinks));
			IL_C5:
			return new ReadOnlyCollection<AttachmentLink>(list);
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000977F0 File Offset: 0x000959F0
		public void ConvertToImage()
		{
			this.needConversionToImage = true;
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x000977F9 File Offset: 0x000959F9
		public AttachmentType AttachmentType
		{
			get
			{
				return this.attachmentType;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x00097801 File Offset: 0x00095A01
		public AttachmentId AttachmentId
		{
			get
			{
				return this.attachmentId;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x00097809 File Offset: 0x00095A09
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x00097811 File Offset: 0x00095A11
		public string ContentId
		{
			get
			{
				return this.contentId;
			}
			set
			{
				if (this.contentId != value)
				{
					this.contentId = value;
					this.contentBase = null;
					this.contentLocation = null;
					this.isChanged = true;
				}
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x0009783D File Offset: 0x00095A3D
		// (set) Token: 0x060025C2 RID: 9666 RVA: 0x00097845 File Offset: 0x00095A45
		public int RenderingPosition
		{
			get
			{
				return this.renderingPosition;
			}
			set
			{
				if (this.renderingPosition != value)
				{
					this.renderingPosition = value;
					this.isChanged = true;
				}
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x0009785E File Offset: 0x00095A5E
		public bool IsOriginallyInline
		{
			get
			{
				return this.originalIsInline;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x00097866 File Offset: 0x00095A66
		public bool? IsMarkedInline
		{
			get
			{
				return this.markedInline;
			}
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0009786E File Offset: 0x00095A6E
		public bool IsInline(bool requireMarkInline)
		{
			if (this.markedInline != null)
			{
				return this.markedInline.Value;
			}
			return !requireMarkInline && this.originalIsInline;
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x00097894 File Offset: 0x00095A94
		// (set) Token: 0x060025C7 RID: 9671 RVA: 0x0009789C File Offset: 0x00095A9C
		public bool IsHidden
		{
			get
			{
				return this.isHidden;
			}
			set
			{
				if (this.isHidden != value)
				{
					this.isHidden = value;
					this.isChanged = true;
				}
			}
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000978B5 File Offset: 0x00095AB5
		public bool NeedsSave(bool requireMarkInline)
		{
			return this.isChanged || this.IsInline(requireMarkInline) != this.originalIsInline;
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000978D3 File Offset: 0x00095AD3
		public bool NeedsConversionToImage
		{
			get
			{
				return this.needConversionToImage;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000978DB File Offset: 0x00095ADB
		public Uri ContentBase
		{
			get
			{
				return this.contentBase;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000978E3 File Offset: 0x00095AE3
		public Uri ContentLocation
		{
			get
			{
				return this.contentLocation;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000978EB File Offset: 0x00095AEB
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000978F3 File Offset: 0x00095AF3
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000978FB File Offset: 0x00095AFB
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x00097904 File Offset: 0x00095B04
		public string FileExtension
		{
			get
			{
				string text = null;
				string text2 = null;
				Attachment.TryFindFileExtension(this.Filename, out text, out text2);
				return text ?? string.Empty;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x0009792F File Offset: 0x00095B2F
		public long Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x00097937 File Offset: 0x00095B37
		public void MarkInline(bool isInline)
		{
			this.markedInline = new bool?(isInline);
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00097948 File Offset: 0x00095B48
		internal bool MakeAttachmentChanges(Attachment attachment, bool requireMarkInline)
		{
			if (this.NeedsSave(requireMarkInline))
			{
				attachment.IsInline = this.IsInline(requireMarkInline);
				attachment.ContentId = this.contentId;
				attachment.RenderingPosition = (attachment.IsInline ? this.renderingPosition : -1);
				attachment[InternalSchema.AttachCalendarHidden] = this.isHidden;
				return true;
			}
			return false;
		}

		// Token: 0x040016B5 RID: 5813
		private static Guid defaultContentIdPrefix = new Guid("b4dcd1dd-dee4-4724-81cc-6dde78879c0d");

		// Token: 0x040016B6 RID: 5814
		private AttachmentId attachmentId;

		// Token: 0x040016B7 RID: 5815
		private Uri contentBase;

		// Token: 0x040016B8 RID: 5816
		private Uri contentLocation;

		// Token: 0x040016B9 RID: 5817
		private string contentId;

		// Token: 0x040016BA RID: 5818
		private string contentType;

		// Token: 0x040016BB RID: 5819
		private string filename;

		// Token: 0x040016BC RID: 5820
		private string displayName;

		// Token: 0x040016BD RID: 5821
		private int renderingPosition;

		// Token: 0x040016BE RID: 5822
		private long size;

		// Token: 0x040016BF RID: 5823
		private bool? markedInline;

		// Token: 0x040016C0 RID: 5824
		private bool originalIsInline;

		// Token: 0x040016C1 RID: 5825
		private bool isHidden;

		// Token: 0x040016C2 RID: 5826
		private bool isChanged;

		// Token: 0x040016C3 RID: 5827
		private bool needConversionToImage;

		// Token: 0x040016C4 RID: 5828
		private AttachmentType attachmentType;
	}
}
