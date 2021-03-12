using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200015A RID: 346
	internal class MservChange
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x000482AE File Offset: 0x000464AE
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x000482B6 File Offset: 0x000464B6
		public MservRecord NewValue { get; private set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x000482BF File Offset: 0x000464BF
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x000482C7 File Offset: 0x000464C7
		public MservRecord OldValue { get; private set; }

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000482D0 File Offset: 0x000464D0
		public MservChange(MservRecord newValue, MservRecord oldValue)
		{
			this.NewValue = newValue;
			this.OldValue = oldValue;
		}
	}
}
