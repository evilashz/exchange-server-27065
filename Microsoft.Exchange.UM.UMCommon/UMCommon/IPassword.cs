using System;
using System.Collections;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000AF RID: 175
	internal interface IPassword
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000651 RID: 1617
		// (set) Token: 0x06000652 RID: 1618
		int LockoutCount { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000653 RID: 1619
		// (set) Token: 0x06000654 RID: 1620
		PasswordBlob CurrentPassword { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000655 RID: 1621
		// (set) Token: 0x06000656 RID: 1622
		ExDateTime TimeSet { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000657 RID: 1623
		// (set) Token: 0x06000658 RID: 1624
		ArrayList OldPasswords { get; set; }

		// Token: 0x06000659 RID: 1625
		void Commit();
	}
}
