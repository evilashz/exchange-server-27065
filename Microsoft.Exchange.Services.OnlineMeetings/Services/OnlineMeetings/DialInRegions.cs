using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000007 RID: 7
	internal class DialInRegions : Collection<DialInRegion>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000224C File Offset: 0x0000044C
		public DialInRegions()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002254 File Offset: 0x00000454
		public DialInRegions(IList<DialInRegion> list) : base(list)
		{
		}
	}
}
