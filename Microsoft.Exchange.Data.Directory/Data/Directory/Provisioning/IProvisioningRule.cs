using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000786 RID: 1926
	internal interface IProvisioningRule
	{
		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x0600605B RID: 24667
		ICollection<Type> TargetObjectTypes { get; }

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x0600605C RID: 24668
		// (set) Token: 0x0600605D RID: 24669
		ProvisioningContext Context { get; set; }
	}
}
