using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C3 RID: 451
	internal class FederationTrustCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x000305A8 File Offset: 0x0002E7A8
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"OrgCertificate",
				"OrgNextCertificate",
				"OrgPrevCertificate",
				"NamespaceProvisioner",
				"ApplicationIdentifier"
			};
		}
	}
}
