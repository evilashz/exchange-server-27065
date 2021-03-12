using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000FA RID: 250
	internal class SidOAuthIdentity : GenericSidIdentity
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x0003794B File Offset: 0x00035B4B
		private SidOAuthIdentity(string name, SidOAuthIdentity.AccountType accountType, SecurityIdentifier sid, string partitionId, OAuthIdentity oauthIdentity) : base(name, accountType.ToString(), sid, partitionId)
		{
			this.OAuthAccountType = accountType;
			this.OAuthIdentity = oauthIdentity;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00037971 File Offset: 0x00035B71
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00037979 File Offset: 0x00035B79
		public OAuthIdentity OAuthIdentity { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00037984 File Offset: 0x00035B84
		public string ManagedTenantName
		{
			get
			{
				if (this.OAuthAccountType == SidOAuthIdentity.AccountType.OAuthLinkedAccount)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug(0, 0L, "[SidOAuthIdentity::ManagedTenantNameGetter] Return org name for OAuthLinkedAccount.");
					return this.OAuthIdentity.OrganizationId.OrganizationalUnit.Name;
				}
				ExTraceGlobals.OAuthTracer.TraceDebug(0, 0L, "[SidOAuthIdentity::ManagedTenantNameGetter] Return null.");
				return null;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000379D5 File Offset: 0x00035BD5
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000379DD File Offset: 0x00035BDD
		public SidOAuthIdentity.AccountType OAuthAccountType { get; private set; }

		// Token: 0x0600085F RID: 2143 RVA: 0x000379E8 File Offset: 0x00035BE8
		public static SidOAuthIdentity Create(OAuthIdentity oauthIdentity)
		{
			if (oauthIdentity == null)
			{
				throw new InvalidOAuthTokenException(OAuthErrors.UnexpectedErrorOccurred, null, null);
			}
			SidOAuthIdentity.AccountType accountType;
			string text;
			SecurityIdentifier sid;
			string text2;
			if (!oauthIdentity.IsAppOnly)
			{
				accountType = SidOAuthIdentity.AccountType.OAuthActAsUser;
				text = oauthIdentity.ActAsUser.UserPrincipalName;
				sid = oauthIdentity.ActAsUser.Sid;
				text2 = oauthIdentity.OrganizationId.PartitionId.ToString();
			}
			else
			{
				if (oauthIdentity.OAuthApplication == null || oauthIdentity.OAuthApplication.PartnerApplication == null)
				{
					throw new InvalidOAuthTokenException(OAuthErrors.UnexpectedErrorOccurred, null, null);
				}
				ADObjectId linkedAccount = oauthIdentity.OAuthApplication.PartnerApplication.LinkedAccount;
				if (linkedAccount == null)
				{
					throw new InvalidOAuthLinkedAccountException(oauthIdentity.OAuthApplication.PartnerApplication.Name, string.Empty);
				}
				if (linkedAccount.IsDeleted)
				{
					ExTraceGlobals.OAuthTracer.TraceError<ADObjectId>(0L, "[SidOAuthIdentity::Create] LinkedAccount is deleted. LinkedAccount: {0}", linkedAccount);
					throw new InvalidOAuthLinkedAccountException(oauthIdentity.OAuthApplication.PartnerApplication.Name, linkedAccount.ToString());
				}
				ADUser aduser = LinkedAccountCache.Instance.Get(linkedAccount);
				if (aduser == null)
				{
					ExTraceGlobals.OAuthTracer.TraceError<string, ADObjectId>(0L, "[SidOAuthIdentity::Create] Failed to find the LinkedAccount. PartnerApplication: {0}; LinkedAccount: {1}", oauthIdentity.OAuthApplication.PartnerApplication.Name, linkedAccount);
					throw new InvalidOAuthLinkedAccountException(oauthIdentity.OAuthApplication.PartnerApplication.Name, linkedAccount.ToString());
				}
				accountType = SidOAuthIdentity.AccountType.OAuthLinkedAccount;
				text = aduser.Name;
				sid = aduser.Sid;
				if (aduser.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					text2 = null;
				}
				else
				{
					text2 = aduser.OrganizationId.PartitionId.ToString();
				}
			}
			ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[SidOAuthIdentity::Create] Create SidOAuthIdentity. Name: {0}; accountType: {1}; sid: {2}; partitionId: {3}", new object[]
			{
				text,
				accountType,
				sid,
				text2
			});
			return new SidOAuthIdentity(text, accountType, sid, text2, oauthIdentity);
		}

		// Token: 0x020000FB RID: 251
		public enum AccountType
		{
			// Token: 0x040007C4 RID: 1988
			OAuthActAsUser,
			// Token: 0x040007C5 RID: 1989
			OAuthLinkedAccount
		}
	}
}
