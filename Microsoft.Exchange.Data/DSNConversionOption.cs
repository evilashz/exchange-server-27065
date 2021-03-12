using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A3 RID: 163
	[Flags]
	public enum DSNConversionOption
	{
		// Token: 0x0400025C RID: 604
		[LocDescription(DataStrings.IDs.UseExchangeDSNs)]
		UseExchangeDSNs = 0,
		// Token: 0x0400025D RID: 605
		[LocDescription(DataStrings.IDs.PreserveDSNBody)]
		PreserveDSNBody = 1,
		// Token: 0x0400025E RID: 606
		[LocDescription(DataStrings.IDs.DoNotConvert)]
		DoNotConvert = 2
	}
}
