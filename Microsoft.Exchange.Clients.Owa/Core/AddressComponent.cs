using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002A6 RID: 678
	public sealed class AddressComponent
	{
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000974F9 File Offset: 0x000956F9
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x00097501 File Offset: 0x00095701
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x0009750A File Offset: 0x0009570A
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x00097512 File Offset: 0x00095712
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				this.label = value;
			}
		}

		// Token: 0x040012D9 RID: 4825
		private string value;

		// Token: 0x040012DA RID: 4826
		private string label;
	}
}
