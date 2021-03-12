using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008E7 RID: 2279
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ItemPartIrmInfo
	{
		// Token: 0x06005589 RID: 21897 RVA: 0x001627E4 File Offset: 0x001609E4
		private ItemPartIrmInfo(bool isRestricted, ContentRight usageRights, string templateName, string templateDescription, string conversationOwner, RightsManagedMessageDecryptionStatus decryptionStatus, ExDateTime userLicenseExpiryTime, bool requiresRepublishingWhenRecipientsChange, bool canRepublish)
		{
			this.isRestricted = isRestricted;
			this.usageRights = usageRights;
			this.templateName = templateName;
			this.templateDescription = templateDescription;
			this.conversationOwner = conversationOwner;
			this.decryptionStatus = decryptionStatus;
			this.userLicenseExpiryTime = userLicenseExpiryTime;
			this.requiresRepublishingWhenRecipientsChange = requiresRepublishingWhenRecipientsChange;
			this.canRepublish = canRepublish;
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x0600558A RID: 21898 RVA: 0x0016283C File Offset: 0x00160A3C
		public bool IsRestricted
		{
			get
			{
				return this.isRestricted;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x0600558B RID: 21899 RVA: 0x00162844 File Offset: 0x00160A44
		public ContentRight UsageRights
		{
			get
			{
				return this.usageRights;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x0600558C RID: 21900 RVA: 0x0016284C File Offset: 0x00160A4C
		public string TemplateName
		{
			get
			{
				return this.templateName;
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x0600558D RID: 21901 RVA: 0x00162854 File Offset: 0x00160A54
		public string TemplateDescription
		{
			get
			{
				return this.templateDescription;
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x0600558E RID: 21902 RVA: 0x0016285C File Offset: 0x00160A5C
		public string ConversationOwner
		{
			get
			{
				return this.conversationOwner;
			}
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x0600558F RID: 21903 RVA: 0x00162864 File Offset: 0x00160A64
		public RightsManagedMessageDecryptionStatus DecryptionStatus
		{
			get
			{
				return this.decryptionStatus;
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x06005590 RID: 21904 RVA: 0x0016286C File Offset: 0x00160A6C
		public ExDateTime UserLicenseExpiryTime
		{
			get
			{
				return this.userLicenseExpiryTime;
			}
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06005591 RID: 21905 RVA: 0x00162874 File Offset: 0x00160A74
		public bool RequiresRepublishingWhenRecipientsChange
		{
			get
			{
				return this.requiresRepublishingWhenRecipientsChange;
			}
		}

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06005592 RID: 21906 RVA: 0x0016287C File Offset: 0x00160A7C
		public bool CanRepublish
		{
			get
			{
				return this.canRepublish;
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06005593 RID: 21907 RVA: 0x00162884 File Offset: 0x00160A84
		// (set) Token: 0x06005594 RID: 21908 RVA: 0x0016288C File Offset: 0x00160A8C
		public BodyFormat BodyFormat { get; set; }

		// Token: 0x06005595 RID: 21909 RVA: 0x00162898 File Offset: 0x00160A98
		internal static ItemPartIrmInfo Create(ContentRight usageRights, string templateName, string templateDescription, string conversationOwner, ExDateTime userLicenseExpiryTime, bool requiresRepublishingWhenRecipientsChange, bool canRepublish)
		{
			return new ItemPartIrmInfo(true, usageRights, templateName, templateDescription, conversationOwner, RightsManagedMessageDecryptionStatus.Success, userLicenseExpiryTime, requiresRepublishingWhenRecipientsChange, canRepublish);
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x001628BC File Offset: 0x00160ABC
		internal static ItemPartIrmInfo CreateForDecryptionFailure(Exception exception)
		{
			return new ItemPartIrmInfo(true, ContentRight.None, string.Empty, string.Empty, string.Empty, RightsManagedMessageDecryptionStatus.CreateFromException(exception), ExDateTime.MinValue, true, false);
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x001628EC File Offset: 0x00160AEC
		internal static ItemPartIrmInfo CreateForIrmDisabled()
		{
			return new ItemPartIrmInfo(true, ContentRight.None, string.Empty, string.Empty, string.Empty, RightsManagedMessageDecryptionStatus.FeatureDisabled, ExDateTime.MinValue, true, false);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x0016291C File Offset: 0x00160B1C
		internal static ItemPartIrmInfo CreateForUnsupportedScenario()
		{
			return new ItemPartIrmInfo(true, ContentRight.None, string.Empty, string.Empty, string.Empty, RightsManagedMessageDecryptionStatus.NotSupported, ExDateTime.MinValue, true, false);
		}

		// Token: 0x04002DEA RID: 11754
		internal static readonly ItemPartIrmInfo NotRestricted = new ItemPartIrmInfo(false, ContentRight.Owner, string.Empty, string.Empty, string.Empty, RightsManagedMessageDecryptionStatus.Success, ExDateTime.MaxValue, false, true);

		// Token: 0x04002DEB RID: 11755
		private readonly ExDateTime userLicenseExpiryTime;

		// Token: 0x04002DEC RID: 11756
		private readonly bool requiresRepublishingWhenRecipientsChange;

		// Token: 0x04002DED RID: 11757
		private readonly bool canRepublish;

		// Token: 0x04002DEE RID: 11758
		private bool isRestricted;

		// Token: 0x04002DEF RID: 11759
		private ContentRight usageRights;

		// Token: 0x04002DF0 RID: 11760
		private string templateName;

		// Token: 0x04002DF1 RID: 11761
		private string templateDescription;

		// Token: 0x04002DF2 RID: 11762
		private string conversationOwner;

		// Token: 0x04002DF3 RID: 11763
		private RightsManagedMessageDecryptionStatus decryptionStatus;
	}
}
