using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000163 RID: 355
	internal interface IAirSyncMissingPropertyStrategy
	{
		// Token: 0x0600101C RID: 4124
		void ExecuteCopyProperty(IProperty srcProperty, AirSyncProperty dstAirSyncProperty);

		// Token: 0x0600101D RID: 4125
		void PostProcessPropertyBag(AirSyncDataObject propertyBag);

		// Token: 0x0600101E RID: 4126
		void Validate(AirSyncDataObject airSyncDataObject);
	}
}
