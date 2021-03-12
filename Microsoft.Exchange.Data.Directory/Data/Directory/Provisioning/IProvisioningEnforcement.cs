using System;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000788 RID: 1928
	internal interface IProvisioningEnforcement : IProvisioningRule
	{
		// Token: 0x0600605F RID: 24671
		bool IsApplicable(IConfigurable readOnlyPresentationObject);

		// Token: 0x06006060 RID: 24672
		ProvisioningValidationError[] Validate(ADProvisioningPolicy enforcementPolicy, IConfigurable readOnlyPresentationObject);
	}
}
