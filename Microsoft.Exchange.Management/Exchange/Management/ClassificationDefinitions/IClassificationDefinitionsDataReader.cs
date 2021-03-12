using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000825 RID: 2085
	internal interface IClassificationDefinitionsDataReader
	{
		// Token: 0x06004826 RID: 18470
		IEnumerable<TransportRule> GetAllClassificationRuleCollection(OrganizationId organizationId, IConfigurationSession currentDataSession, QueryFilter additionalFilter);

		// Token: 0x06004827 RID: 18471
		DataClassificationConfig GetDataClassificationConfig(OrganizationId organizationId, IConfigurationSession currentDataSession);
	}
}
