using System;
using Microsoft.Exchange.Net.AAD;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x0200000A RID: 10
	internal static class AADClientTestHooks
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000024C5 File Offset: 0x000006C5
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024CC File Offset: 0x000006CC
		public static Func<IAadClient> GraphApi_GetAadClient { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024DB File Offset: 0x000006DB
		public static Func<bool> IsUserMemberOfGroup { get; set; }
	}
}
