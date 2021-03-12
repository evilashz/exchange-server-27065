using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002EE RID: 750
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDirectoryAccessor : IADUserFinder
	{
		// Token: 0x06002140 RID: 8512
		bool TryGetOrganizationContentConversionProperties(OrganizationId organizationId, out OrganizationContentConversionProperties organizationContentConversionProperties);

		// Token: 0x06002141 RID: 8513
		SmtpAddress GetOrganizationFederatedMailboxIdentity(IConfigurationSession configurationSession);

		// Token: 0x06002142 RID: 8514
		OrganizationRelationship GetOrganizationRelationship(OrganizationId organizationId, string domain);

		// Token: 0x06002143 RID: 8515
		int? GetPrimaryGroupId(IRecipientSession recipientSession, SecurityIdentifier userSid);

		// Token: 0x06002144 RID: 8516
		bool IsLicensingEnforcedInOrg(OrganizationId organizationId);

		// Token: 0x06002145 RID: 8517
		bool IsTenantAccessBlocked(OrganizationId organizationId);

		// Token: 0x06002146 RID: 8518
		Server GetLocalServer();
	}
}
