using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C4 RID: 452
	internal class FederatedOrganizationIdWithDomainStatusCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FCF RID: 4047 RVA: 0x00030600 File Offset: 0x0002E800
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"AccountNamespace",
				"Domains",
				"OrganizationContact",
				"Enabled",
				"DelegationTrustLink"
			};
		}
	}
}
