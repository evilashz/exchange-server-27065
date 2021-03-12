using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200003E RID: 62
	internal interface IPropertyConstraintProvider
	{
		// Token: 0x0600025A RID: 602
		PropertyDefinitionConstraint[] GetPropertyDefinitionConstraints(string propertyName);
	}
}
