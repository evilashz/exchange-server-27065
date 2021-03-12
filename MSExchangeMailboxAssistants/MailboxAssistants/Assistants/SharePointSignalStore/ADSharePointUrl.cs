using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000220 RID: 544
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ADSharePointUrl : ISharePointUrl
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x00077198 File Offset: 0x00075398
		public string GetUrl(IExchangePrincipal userIdentity, IRecipientSession recipientSession)
		{
			OrganizationId userOrganizationId = this.GetUserOrganizationId((ADUser)DirectoryHelper.ReadADRecipient(userIdentity.MailboxInfo.MailboxGuid, userIdentity.MailboxInfo.IsArchive, recipientSession));
			return this.GetUrl(userOrganizationId);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000771D4 File Offset: 0x000753D4
		public virtual Uri GetRootSiteUrlWithoutFallback(OrganizationId organization)
		{
			return SharePointUrl.GetRootSiteUrl(organization);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000771DC File Offset: 0x000753DC
		internal string GetUrl(OrganizationId organization)
		{
			Uri rootSiteUrlWithoutFallback = this.GetRootSiteUrlWithoutFallback(organization);
			if (!(rootSiteUrlWithoutFallback != null))
			{
				return null;
			}
			return rootSiteUrlWithoutFallback.ToString();
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00077202 File Offset: 0x00075402
		internal OrganizationId GetUserOrganizationId(ADUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return user.OrganizationId;
		}
	}
}
