using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000171 RID: 369
	internal interface IAirSyncDataObjectGenerator : IDataObjectGenerator
	{
		// Token: 0x06001048 RID: 4168
		AirSyncDataObject GetInnerAirSyncDataObject(IAirSyncMissingPropertyStrategy strategy);
	}
}
