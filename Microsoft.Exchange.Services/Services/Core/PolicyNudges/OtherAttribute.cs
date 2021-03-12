using System;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003B6 RID: 950
	public class OtherAttribute
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x0009865B File Offset: 0x0009685B
		public OtherAttribute(string name, string val)
		{
			this.Name = name;
			this.Value = val;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00098671 File Offset: 0x00096871
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x00098679 File Offset: 0x00096879
		public string Name { get; private set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00098682 File Offset: 0x00096882
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0009868A File Offset: 0x0009688A
		public string Value { get; private set; }
	}
}
