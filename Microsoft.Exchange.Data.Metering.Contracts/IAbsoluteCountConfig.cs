using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000003 RID: 3
	internal interface IAbsoluteCountConfig : ICountedConfig
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000007 RID: 7
		TimeSpan HistoryLookbackWindow { get; }
	}
}
