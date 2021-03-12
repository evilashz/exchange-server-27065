using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000143 RID: 323
	internal interface IOriginatingChangeTimestamp
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000DB4 RID: 3508
		// (set) Token: 0x06000DB5 RID: 3509
		DateTime? LastExchangeChangedTime { get; set; }
	}
}
