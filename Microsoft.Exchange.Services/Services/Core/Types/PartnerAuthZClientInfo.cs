using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.PartnerToken;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200083E RID: 2110
	internal sealed class PartnerAuthZClientInfo : AuthZClientInfo
	{
		// Token: 0x06003CEA RID: 15594 RVA: 0x000D6ED0 File Offset: 0x000D50D0
		private PartnerAuthZClientInfo(OrganizationId delegatedOrganizationId, string delegatedOrganizationName, string partnerUser)
		{
			this.delegatedOrganizationId = delegatedOrganizationId;
			this.delegatedOrganizationName = delegatedOrganizationName;
			this.partnerUser = partnerUser;
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000D6EED File Offset: 0x000D50ED
		public static AuthZClientInfo FromPartnerIdentity(PartnerIdentity partnerIdentity)
		{
			return new PartnerAuthZClientInfo(partnerIdentity.DelegatedOrganizationId, partnerIdentity.DelegatedPrincipal.DelegatedOrganization, partnerIdentity.DelegatedPrincipal.UserId);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000D6F10 File Offset: 0x000D5110
		public static AuthZClientInfo FromPartnerAccessToken(PartnerAccessToken token)
		{
			return new PartnerAuthZClientInfo(token.OrganizationId, token.OrganizationName, token.PartnerUser);
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000D6F29 File Offset: 0x000D5129
		internal override ADRecipientSessionContext GetADRecipientSessionContext()
		{
			return ADRecipientSessionContext.CreateForPartner(this.delegatedOrganizationId);
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000D6F36 File Offset: 0x000D5136
		public override void ApplyManagementRole(ManagementRoleType managementRoleType, WebMethodEntry methodEntry)
		{
			if (managementRoleType == null)
			{
				return;
			}
			throw new InvalidManagementRoleHeaderException(CoreResources.IDs.MessageManagementRoleHeaderNotSupportedForPartnerIdentity);
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000D6F4B File Offset: 0x000D514B
		public override AuthZBehavior GetAuthZBehavior()
		{
			return AuthZBehavior.DefaultBehavior;
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000D6F52 File Offset: 0x000D5152
		public override string ToCallerString()
		{
			return string.Format("{0}\\{1}", this.delegatedOrganizationName, this.partnerUser);
		}

		// Token: 0x04002194 RID: 8596
		private readonly OrganizationId delegatedOrganizationId;

		// Token: 0x04002195 RID: 8597
		private readonly string delegatedOrganizationName;

		// Token: 0x04002196 RID: 8598
		private readonly string partnerUser;
	}
}
