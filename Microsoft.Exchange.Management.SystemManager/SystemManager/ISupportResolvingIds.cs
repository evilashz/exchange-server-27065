using System;
using System.Collections;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000041 RID: 65
	internal interface ISupportResolvingIds
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000296 RID: 662
		// (set) Token: 0x06000297 RID: 663
		PropertyDefinition PropertyForResolving { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000298 RID: 664
		// (set) Token: 0x06000299 RID: 665
		IList IdentitiesToResolve { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600029A RID: 666
		// (set) Token: 0x0600029B RID: 667
		bool IgnoreNonExistingObjects { get; set; }
	}
}
