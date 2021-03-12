using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200039C RID: 924
	public class IRMItemHelper
	{
		// Token: 0x060022DA RID: 8922 RVA: 0x000C79C9 File Offset: 0x000C5BC9
		internal IRMItemHelper(MessageItem message, UserContext userContext, bool isPreviewForm, bool isEmbeddedItem)
		{
			this.message = message;
			this.userContext = userContext;
			this.isPreviewForm = isPreviewForm;
			this.isEmbeddedItem = isEmbeddedItem;
			this.isIrmMessageInJunkEmailFolder = (message.IsRestricted && JunkEmailUtilities.IsInJunkEmailFolder(message, isEmbeddedItem, userContext));
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x000C7A08 File Offset: 0x000C5C08
		public bool IsPrintRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Print);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000C7A11 File Offset: 0x000C5C11
		public bool IsCopyRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Extract);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x000C7A1A File Offset: 0x000C5C1A
		public bool IsReplyRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Reply);
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x000C7A27 File Offset: 0x000C5C27
		public bool IsReplyAllRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.ReplyAll);
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x000C7A34 File Offset: 0x000C5C34
		public bool IsForwardRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Forward);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000C7A41 File Offset: 0x000C5C41
		public bool IsRemoveAllowed
		{
			get
			{
				return !this.isEmbeddedItem && !this.IsUsageRightRestricted(ContentRight.Export);
			}
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000C7A5C File Offset: 0x000C5C5C
		public void IrmDecryptIfRestricted()
		{
			if (!Utilities.IsIrmRestricted(this.message))
			{
				return;
			}
			RightsManagedMessageItem rightsManagedMessageItem = (RightsManagedMessageItem)this.message;
			if (!this.userContext.IsIrmEnabled)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.FeatureDisabled;
				return;
			}
			if (!rightsManagedMessageItem.CanDecode)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.NotSupported;
				return;
			}
			try
			{
				Utilities.IrmDecryptIfRestricted(this.message, this.userContext, !this.isPreviewForm);
			}
			catch (MissingRightsManagementLicenseException ex)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.CreateFromException(ex);
				if (!Utilities.IsPrefetchRequest(OwaContext.Current) && !this.isEmbeddedItem && ex.MessageStoreId != null && !string.IsNullOrEmpty(ex.PublishLicense))
				{
					OwaStoreObjectId messageId = OwaStoreObjectId.CreateFromItemId(ex.MessageStoreId, null, ex.MessageInArchive ? OwaStoreObjectIdType.ArchiveMailboxObject : OwaStoreObjectIdType.MailBoxObject, ex.MailboxOwnerLegacyDN);
					this.userContext.AsyncAcquireIrmLicenses(messageId, ex.PublishLicense, ex.RequestCorrelator);
				}
			}
			catch (RightsManagementPermanentException exception)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.CreateFromException(exception);
			}
			catch (RightsManagementTransientException exception2)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.CreateFromException(exception2);
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000C7B88 File Offset: 0x000C5D88
		private bool IsUsageRightRestricted(ContentRight right)
		{
			if (this.isIrmMessageInJunkEmailFolder)
			{
				if (right <= ContentRight.Forward)
				{
					if (right != ContentRight.Print && right != ContentRight.Forward)
					{
						goto IL_32;
					}
				}
				else if (right != ContentRight.Reply && right != ContentRight.ReplyAll)
				{
					goto IL_32;
				}
				return true;
			}
			IL_32:
			RightsManagedMessageItem rightsManagedMessageItem = this.message as RightsManagedMessageItem;
			if (rightsManagedMessageItem == null || !rightsManagedMessageItem.IsRestricted)
			{
				return false;
			}
			if (!this.userContext.IsIrmEnabled)
			{
				return false;
			}
			if (this.irmDecryptionStatus.Failed)
			{
				return !right.IsUsageRightGranted(ContentRight.Extract) && !right.IsUsageRightGranted(ContentRight.Print);
			}
			return !rightsManagedMessageItem.UsageRights.IsUsageRightGranted(right);
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000C7C21 File Offset: 0x000C5E21
		internal bool IsRestrictedButIrmFeatureDisabledOrDecryptionFailed
		{
			get
			{
				return this.message.IsRestricted && (!this.userContext.IsIrmEnabled || this.irmDecryptionStatus.Failed);
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000C7C4C File Offset: 0x000C5E4C
		internal bool IsRestrictedAndIrmFeatureEnabled
		{
			get
			{
				return this.message.IsRestricted && this.userContext.IsIrmEnabled;
			}
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000C7C68 File Offset: 0x000C5E68
		internal void RenderAlternateBodyForIrm(TextWriter writer, bool isProtectedVoicemail)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(Utilities.GetAlternateBodyForIrm(this.userContext, Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml, this.irmDecryptionStatus, isProtectedVoicemail));
		}

		// Token: 0x0400187E RID: 6270
		private readonly bool isIrmMessageInJunkEmailFolder;

		// Token: 0x0400187F RID: 6271
		private MessageItem message;

		// Token: 0x04001880 RID: 6272
		private UserContext userContext;

		// Token: 0x04001881 RID: 6273
		private bool isPreviewForm;

		// Token: 0x04001882 RID: 6274
		private bool isEmbeddedItem;

		// Token: 0x04001883 RID: 6275
		private RightsManagedMessageDecryptionStatus irmDecryptionStatus;
	}
}
