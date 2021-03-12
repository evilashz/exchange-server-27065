using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharePointUrlResolver
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0001192B File Offset: 0x0000FB2B
		public SharePointUrlResolver(ADUser groupAdUser)
		{
			ArgumentValidator.ThrowIfNull("groupADUser", groupAdUser);
			this.groupADUser = groupAdUser;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00011945 File Offset: 0x0000FB45
		public Uri RootUri
		{
			get
			{
				if (this.rootUri == null)
				{
					this.rootUri = SharePointUrl.GetRootSiteUrl(this.groupADUser.OrganizationId);
				}
				return this.rootUri;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00011974 File Offset: 0x0000FB74
		public string GetSiteUrl()
		{
			if (this.groupADUser.SharePointSiteUrl != null)
			{
				return this.groupADUser.SharePointSiteUrl;
			}
			if (this.groupADUser.SharePointUrl != null)
			{
				return this.groupADUser.SharePointUrl.ToString();
			}
			if (this.RootUri != null)
			{
				return new Uri(this.RootUri, "_layouts/groupstatus.aspx?id=" + this.groupADUser.ExternalDirectoryObjectId + "&target=site").ToString();
			}
			return null;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000119F8 File Offset: 0x0000FBF8
		public string GetDocumentsUrl()
		{
			if (this.groupADUser.SharePointDocumentsUrl != null)
			{
				return this.groupADUser.SharePointDocumentsUrl;
			}
			if (this.RootUri != null)
			{
				return new Uri(this.RootUri, "_layouts/groupstatus.aspx?id=" + this.groupADUser.ExternalDirectoryObjectId + "&target=documents").ToString();
			}
			return null;
		}

		// Token: 0x040001A0 RID: 416
		private ADUser groupADUser;

		// Token: 0x040001A1 RID: 417
		private Uri rootUri;
	}
}
