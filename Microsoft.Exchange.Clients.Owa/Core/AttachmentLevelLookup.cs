using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000C4 RID: 196
	internal sealed class AttachmentLevelLookup
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00037E48 File Offset: 0x00036048
		public static AttachmentPolicy.Level GetLevelForAttachment(string fileExtension, string mimeType, UserContext userContext)
		{
			if (fileExtension == null)
			{
				throw new ArgumentNullException("fileExtension");
			}
			AttachmentPolicy attachmentPolicy;
			if (userContext != null)
			{
				attachmentPolicy = userContext.AttachmentPolicy;
			}
			else
			{
				attachmentPolicy = OwaConfigurationManager.Configuration.AttachmentPolicy;
			}
			if (mimeType == null || !attachmentPolicy.DirectFileAccessEnabled)
			{
				return AttachmentPolicy.Level.Block;
			}
			AttachmentPolicy.Level level = attachmentPolicy.GetLevel(fileExtension, AttachmentPolicy.TypeSignifier.File);
			if (level == AttachmentPolicy.Level.Allow)
			{
				return level;
			}
			AttachmentPolicy.Level level2 = attachmentPolicy.GetLevel(mimeType, AttachmentPolicy.TypeSignifier.Mime);
			if (level2 == AttachmentPolicy.Level.Allow)
			{
				return level2;
			}
			if (level == AttachmentPolicy.Level.Unknown && level2 == AttachmentPolicy.Level.Unknown)
			{
				return attachmentPolicy.TreatUnknownTypeAs;
			}
			if (level < level2)
			{
				return level;
			}
			return level2;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00037EBF File Offset: 0x000360BF
		private static AttachmentPolicy.Level GetAttachmentLevel(AttachmentType attachmentType, string fileExtension, string mimeType, UserContext userContext)
		{
			if (attachmentType == AttachmentType.Ole || attachmentType == AttachmentType.EmbeddedMessage)
			{
				return AttachmentPolicy.Level.Allow;
			}
			return AttachmentLevelLookup.GetLevelForAttachment(fileExtension, mimeType, userContext);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00037ED3 File Offset: 0x000360D3
		public static AttachmentPolicy.Level GetAttachmentLevel(Attachment attachment, UserContext userContext)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}
			return AttachmentLevelLookup.GetAttachmentLevel(attachment.AttachmentType, attachment.FileExtension, attachment.ContentType ?? attachment.CalculatedContentType, userContext);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00037F05 File Offset: 0x00036105
		public static AttachmentPolicy.Level GetAttachmentLevel(AttachmentLink attachmentLink, UserContext userContext)
		{
			if (attachmentLink == null)
			{
				throw new ArgumentNullException("attachmentLink");
			}
			return AttachmentLevelLookup.GetAttachmentLevel(attachmentLink.AttachmentType, attachmentLink.FileExtension, attachmentLink.ContentType, userContext);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00037F2D File Offset: 0x0003612D
		public static AttachmentPolicy.Level GetAttachmentLevel(AttachmentInfo attachmentInfo, UserContext userContext)
		{
			if (attachmentInfo == null)
			{
				throw new ArgumentNullException("attachmentInfo");
			}
			return AttachmentLevelLookup.GetAttachmentLevel(attachmentInfo.AttachmentType, attachmentInfo.FileExtension, attachmentInfo.ContentType, userContext);
		}
	}
}
