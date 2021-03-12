using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200008D RID: 141
	internal interface ISubscriptionSettings : IMigrationSerializable
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000812 RID: 2066
		ExDateTime LastModifiedTime { get; }
	}
}
