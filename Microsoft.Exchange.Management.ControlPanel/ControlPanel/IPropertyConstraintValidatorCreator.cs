using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006BE RID: 1726
	internal interface IPropertyConstraintValidatorCreator
	{
		// Token: 0x060049AF RID: 18863
		ValidatorInfo Create(ProviderPropertyDefinition propertyDefinition, PropertyDefinitionConstraint constraint);
	}
}
