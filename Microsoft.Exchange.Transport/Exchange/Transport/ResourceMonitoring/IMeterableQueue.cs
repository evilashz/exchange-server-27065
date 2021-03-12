using System;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x02000312 RID: 786
	internal interface IMeterableQueue
	{
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002220 RID: 8736
		string Name { get; }

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002221 RID: 8737
		long Length { get; }
	}
}
