using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200011F RID: 287
	internal interface IProperty
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000F04 RID: 3844
		// (set) Token: 0x06000F05 RID: 3845
		int SchemaLinkId { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000F06 RID: 3846
		// (set) Token: 0x06000F07 RID: 3847
		PropertyState State { get; set; }

		// Token: 0x06000F08 RID: 3848
		void CopyFrom(IProperty srcProperty);

		// Token: 0x06000F09 RID: 3849
		string ToString();
	}
}
