using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel.Pickers
{
	// Token: 0x0200032F RID: 815
	public interface IRecipientObjectResolver
	{
		// Token: 0x06002EEB RID: 12011
		IEnumerable<ADRecipient> ResolveLegacyDNs(IEnumerable<string> legacyDNs);

		// Token: 0x06002EEC RID: 12012
		IEnumerable<RecipientObjectResolverRow> ResolveObjects(IEnumerable<ADObjectId> identities);

		// Token: 0x06002EED RID: 12013
		IEnumerable<Identity> ResolveOrganizationUnitIdentity(IEnumerable<ADObjectId> identities);

		// Token: 0x06002EEE RID: 12014
		IEnumerable<PeopleRecipientObject> ResolvePeople(IEnumerable<ADObjectId> identities);

		// Token: 0x06002EEF RID: 12015
		IEnumerable<ADRecipient> ResolveProxyAddresses(IEnumerable<ProxyAddress> proxyAddresses);

		// Token: 0x06002EF0 RID: 12016
		IEnumerable<AcePermissionRecipientRow> ResolveSecurityPrincipalId(IEnumerable<SecurityPrincipalIdParameter> sidPrincipalId);

		// Token: 0x06002EF1 RID: 12017
		IEnumerable<RecipientObjectResolverRow> ResolveSmtpAddress(SmtpAddress[] smtpAddresses);

		// Token: 0x06002EF2 RID: 12018
		IEnumerable<ADRecipient> ResolveSmtpAddress(IEnumerable<string> addresses);

		// Token: 0x06002EF3 RID: 12019
		List<SecurityIdentifier> ConvertGuidsToSid(string[] rawGuids);
	}
}
