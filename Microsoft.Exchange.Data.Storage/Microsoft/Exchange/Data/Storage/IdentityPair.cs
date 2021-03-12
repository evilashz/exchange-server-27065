using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000629 RID: 1577
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct IdentityPair
	{
		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x00111351 File Offset: 0x0010F551
		// (set) Token: 0x060040FE RID: 16638 RVA: 0x00111359 File Offset: 0x0010F559
		public string LogonUserSid { get; set; }

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x00111362 File Offset: 0x0010F562
		// (set) Token: 0x06004100 RID: 16640 RVA: 0x0011136A File Offset: 0x0010F56A
		public string LogonUserDisplayName { get; set; }
	}
}
