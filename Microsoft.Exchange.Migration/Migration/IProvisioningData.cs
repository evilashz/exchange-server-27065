using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IProvisioningData
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600025B RID: 603
		ProvisioningType ProvisioningType { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600025C RID: 604
		ProvisioningComponent Component { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600025D RID: 605
		Dictionary<string, object> Parameters { get; }

		// Token: 0x0600025E RID: 606
		PersistableDictionary ToPersistableDictionary();
	}
}
