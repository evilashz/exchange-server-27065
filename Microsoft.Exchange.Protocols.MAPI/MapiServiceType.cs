using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000071 RID: 113
	public enum MapiServiceType : uint
	{
		// Token: 0x04000234 RID: 564
		Availability,
		// Token: 0x04000235 RID: 565
		Assistants,
		// Token: 0x04000236 RID: 566
		ContentIndex,
		// Token: 0x04000237 RID: 567
		Transport,
		// Token: 0x04000238 RID: 568
		Admin,
		// Token: 0x04000239 RID: 569
		Inference,
		// Token: 0x0400023A RID: 570
		ELC,
		// Token: 0x0400023B RID: 571
		SMS,
		// Token: 0x0400023C RID: 572
		UnknownServiceType,
		// Token: 0x0400023D RID: 573
		MaxServiceType = 8U
	}
}
