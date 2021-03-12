using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000018 RID: 24
	internal interface IEXADDataProvider
	{
		// Token: 0x06000032 RID: 50
		RawSecurityDescriptor GetSystemConfigurationSecurityDescriptor(string distinguishedName);

		// Token: 0x06000033 RID: 51
		ADRawEntry SystemConfigurationRunQuery(bool useGC, PropertyDefinition[] propertyBags);

		// Token: 0x06000034 RID: 52
		ADRawEntry[] SystemConfigurationRunQuery(bool useGC, PropertyDefinition[] propertyBags, QueryScope queryScope, SortBy sortBy, QueryFilter queryFilter);

		// Token: 0x06000035 RID: 53
		RawSecurityDescriptor GetTopologyConfigurationSecurityDescriptor(string distinguishedName);

		// Token: 0x06000036 RID: 54
		ADRawEntry TopologyConfigurationRunQuery(bool useGC, PropertyDefinition[] propertyBags);

		// Token: 0x06000037 RID: 55
		ADRawEntry[] TopologyConfigurationRunQuery(bool useGC, PropertyDefinition[] propertyBags, QueryScope queryScope, SortBy sortBy, QueryFilter queryFilter);
	}
}
