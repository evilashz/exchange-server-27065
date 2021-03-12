using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000215 RID: 533
	internal class RunspaceCacheValue
	{
		// Token: 0x0600128F RID: 4751 RVA: 0x0003BF4B File Offset: 0x0003A14B
		internal RunspaceCacheValue()
		{
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0003BF53 File Offset: 0x0003A153
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x0003BF5B File Offset: 0x0003A15B
		internal CostHandle CostHandle { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0003BF64 File Offset: 0x0003A164
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x0003BF6C File Offset: 0x0003A16C
		internal PswsAuthZUserToken UserToken { get; set; }
	}
}
