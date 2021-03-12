using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000136 RID: 310
	internal interface IBusyStatusProperty : IProperty
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000F81 RID: 3969
		BusyType BusyStatus { get; }
	}
}
