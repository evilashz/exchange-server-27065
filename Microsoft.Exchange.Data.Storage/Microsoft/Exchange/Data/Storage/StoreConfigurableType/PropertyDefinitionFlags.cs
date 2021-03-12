using System;

namespace Microsoft.Exchange.Data.Storage.StoreConfigurableType
{
	// Token: 0x02000A13 RID: 2579
	[Flags]
	internal enum PropertyDefinitionFlags
	{
		// Token: 0x040034CB RID: 13515
		None = 0,
		// Token: 0x040034CC RID: 13516
		ReadOnly = 1,
		// Token: 0x040034CD RID: 13517
		MultiValued = 2,
		// Token: 0x040034CE RID: 13518
		Calculated = 4,
		// Token: 0x040034CF RID: 13519
		FilterOnly = 8,
		// Token: 0x040034D0 RID: 13520
		Mandatory = 16,
		// Token: 0x040034D1 RID: 13521
		PersistDefaultValue = 32,
		// Token: 0x040034D2 RID: 13522
		WriteOnce = 64,
		// Token: 0x040034D3 RID: 13523
		ReturnOnBind = 128
	}
}
