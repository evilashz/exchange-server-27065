using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000033 RID: 51
	internal class AttachmentPolicyChecker : AttachmentHandler.IAttachmentPolicyChecker
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00004C78 File Offset: 0x00002E78
		private AttachmentPolicyChecker()
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004C80 File Offset: 0x00002E80
		private AttachmentPolicyChecker(AttachmentPolicy policy)
		{
			this.policy = policy;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004C90 File Offset: 0x00002E90
		public AttachmentPolicyLevel GetPolicy(IAttachment attachment, bool isPublicLogin)
		{
			AttachmentPolicyLevel attachmentPolicyLevel = AttachmentPolicyLevel.Unknown;
			AttachmentPolicyLevel attachmentPolicyLevel2 = AttachmentPolicyLevel.Unknown;
			string fileExtension = attachment.FileExtension;
			string text = attachment.ContentType ?? attachment.CalculatedContentType;
			bool directFileAccessEnabled = this.policy.GetDirectFileAccessEnabled(isPublicLogin);
			if (text == null || !directFileAccessEnabled)
			{
				return AttachmentPolicyLevel.Block;
			}
			if (!string.IsNullOrEmpty(fileExtension))
			{
				attachmentPolicyLevel2 = this.policy.GetLevel(fileExtension, AttachmentPolicy.TypeSignifier.File);
			}
			if (!string.IsNullOrEmpty(text))
			{
				attachmentPolicyLevel = this.policy.GetLevel(text, AttachmentPolicy.TypeSignifier.Mime);
			}
			if (attachmentPolicyLevel2 == AttachmentPolicyLevel.Allow || attachmentPolicyLevel == AttachmentPolicyLevel.Allow)
			{
				return AttachmentPolicyLevel.Allow;
			}
			if (attachmentPolicyLevel2 == AttachmentPolicyLevel.Block || attachmentPolicyLevel == AttachmentPolicyLevel.Block)
			{
				return AttachmentPolicyLevel.Block;
			}
			if (attachmentPolicyLevel2 == AttachmentPolicyLevel.ForceSave || attachmentPolicyLevel == AttachmentPolicyLevel.ForceSave)
			{
				return AttachmentPolicyLevel.ForceSave;
			}
			return this.policy.TreatUnknownTypeAs;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004D25 File Offset: 0x00002F25
		internal static AttachmentHandler.IAttachmentPolicyChecker CreateInstance(AttachmentPolicy policy)
		{
			return new AttachmentPolicyChecker(policy);
		}

		// Token: 0x0400006D RID: 109
		private AttachmentPolicy policy;
	}
}
