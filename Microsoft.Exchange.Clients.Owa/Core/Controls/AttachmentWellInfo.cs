using System;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002AB RID: 683
	internal sealed class AttachmentWellInfo
	{
		// Token: 0x06001A7A RID: 6778 RVA: 0x00099D10 File Offset: 0x00097F10
		public AttachmentWellInfo(AttachmentCollection collection, Attachment attachment, bool isJunkOrPhishing)
		{
			this.collection = collection;
			this.attachmentId = attachment.Id;
			if (isJunkOrPhishing)
			{
				this.attachmentLevel = AttachmentPolicy.Level.Block;
			}
			else
			{
				this.attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(attachment, UserContextManager.GetUserContext());
			}
			this.attachmentType = attachment.AttachmentType;
			this.fileName = attachment.FileName;
			if (this.attachmentType == AttachmentType.EmbeddedMessage)
			{
				using (Item itemAsReadOnly = ((ItemAttachment)attachment).GetItemAsReadOnly(null))
				{
					this.displayName = AttachmentUtility.GetEmbeddedAttachmentDisplayName(itemAsReadOnly);
				}
				this.fileExtension = ".msg";
			}
			else
			{
				this.displayName = attachment.DisplayName;
				this.fileExtension = ((attachment.FileExtension == null) ? string.Empty : attachment.FileExtension);
			}
			this.isInline = attachment.IsInline;
			this.attachmentSize = attachment.Size;
			this.attachmentName = AttachmentUtility.CalculateAttachmentName(this.displayName, this.fileName);
			this.mimeType = AttachmentUtility.CalculateContentType(attachment);
			this.textCharset = attachment.TextCharset;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00099E24 File Offset: 0x00098024
		public AttachmentWellInfo(AttachmentCollection collection, AttachmentLink attachmentLink, bool isJunkOrPhishing)
		{
			this.collection = collection;
			this.attachmentId = attachmentLink.AttachmentId;
			if (isJunkOrPhishing)
			{
				this.attachmentLevel = AttachmentPolicy.Level.Block;
			}
			else
			{
				this.attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(attachmentLink, UserContextManager.GetUserContext());
			}
			this.attachmentType = attachmentLink.AttachmentType;
			this.fileName = attachmentLink.Filename;
			this.displayName = attachmentLink.DisplayName;
			this.isInline = attachmentLink.IsInline(true);
			this.attachmentSize = attachmentLink.Size;
			this.fileExtension = attachmentLink.FileExtension;
			this.attachmentName = AttachmentUtility.CalculateAttachmentName(attachmentLink.DisplayName, attachmentLink.Filename);
			this.mimeType = attachmentLink.ContentType;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00099ED4 File Offset: 0x000980D4
		public AttachmentWellInfo(OwaStoreObjectId owaConversationId, AttachmentInfo attachmentInfo, bool isJunkOrPhishing)
		{
			this.messageId = OwaStoreObjectId.CreateFromStoreObjectId(attachmentInfo.MessageId, owaConversationId);
			this.attachmentId = attachmentInfo.AttachmentId;
			if (isJunkOrPhishing)
			{
				this.attachmentLevel = AttachmentPolicy.Level.Block;
			}
			else
			{
				this.attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(attachmentInfo, UserContextManager.GetUserContext());
			}
			this.attachmentType = attachmentInfo.AttachmentType;
			this.fileName = attachmentInfo.FileName;
			this.displayName = attachmentInfo.DisplayName;
			this.isInline = attachmentInfo.IsInline;
			this.attachmentSize = attachmentInfo.Size;
			this.fileExtension = attachmentInfo.FileExtension;
			this.attachmentName = AttachmentUtility.CalculateAttachmentName(attachmentInfo.DisplayName, attachmentInfo.FileName);
			this.mimeType = attachmentInfo.ContentType;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00099F8D File Offset: 0x0009818D
		public Attachment OpenAttachment()
		{
			if (this.collection == null)
			{
				throw new InvalidOperationException("Attachment collection is null, this attachment might have been generated from a conversation item part.  OpenAttachment is not supported for these.");
			}
			return this.collection.Open(this.attachmentId);
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00099FB3 File Offset: 0x000981B3
		public OwaStoreObjectId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00099FBB File Offset: 0x000981BB
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x00099FC3 File Offset: 0x000981C3
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00099FCC File Offset: 0x000981CC
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00099FD4 File Offset: 0x000981D4
		// (set) Token: 0x06001A83 RID: 6787 RVA: 0x00099FDC File Offset: 0x000981DC
		public string MimeType
		{
			get
			{
				return this.mimeType;
			}
			set
			{
				this.mimeType = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00099FE5 File Offset: 0x000981E5
		public AttachmentId AttachmentId
		{
			get
			{
				return this.attachmentId;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00099FED File Offset: 0x000981ED
		public AttachmentType AttachmentType
		{
			get
			{
				return this.attachmentType;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x00099FF5 File Offset: 0x000981F5
		public bool IsInline
		{
			get
			{
				return this.isInline;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00099FFD File Offset: 0x000981FD
		public AttachmentPolicy.Level AttachmentLevel
		{
			get
			{
				return this.attachmentLevel;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0009A005 File Offset: 0x00098205
		public long Size
		{
			get
			{
				return this.attachmentSize;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0009A00D File Offset: 0x0009820D
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x0009A015 File Offset: 0x00098215
		public string FileExtension
		{
			get
			{
				return this.fileExtension;
			}
			set
			{
				this.fileExtension = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0009A01E File Offset: 0x0009821E
		public string AttachmentName
		{
			get
			{
				return this.attachmentName;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0009A026 File Offset: 0x00098226
		public Charset TextCharset
		{
			get
			{
				return this.textCharset;
			}
		}

		// Token: 0x040012FB RID: 4859
		private string attachmentName;

		// Token: 0x040012FC RID: 4860
		private string fileExtension;

		// Token: 0x040012FD RID: 4861
		private string fileName;

		// Token: 0x040012FE RID: 4862
		private string displayName;

		// Token: 0x040012FF RID: 4863
		private bool isInline;

		// Token: 0x04001300 RID: 4864
		private long attachmentSize;

		// Token: 0x04001301 RID: 4865
		private string mimeType;

		// Token: 0x04001302 RID: 4866
		private AttachmentType attachmentType;

		// Token: 0x04001303 RID: 4867
		private AttachmentCollection collection;

		// Token: 0x04001304 RID: 4868
		private AttachmentId attachmentId;

		// Token: 0x04001305 RID: 4869
		private AttachmentPolicy.Level attachmentLevel;

		// Token: 0x04001306 RID: 4870
		private Charset textCharset;

		// Token: 0x04001307 RID: 4871
		private OwaStoreObjectId messageId;
	}
}
