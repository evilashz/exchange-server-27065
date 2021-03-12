using System;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000026 RID: 38
	public interface IRecipient
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BE RID: 190
		// (set) Token: 0x060000BF RID: 191
		string EmailAddress { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C0 RID: 192
		// (set) Token: 0x060000C1 RID: 193
		string Name { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C2 RID: 194
		// (set) Token: 0x060000C3 RID: 195
		string RoutingType { get; set; }
	}
}
