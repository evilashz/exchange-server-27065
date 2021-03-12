using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200000D RID: 13
	public interface ICountableObject
	{
		// Token: 0x0600003D RID: 61
		void IncrementObjectCounter(MapiObjectTrackingScope scope, MapiObjectTrackedType trackedType);

		// Token: 0x0600003E RID: 62
		void DecrementObjectCounter(MapiObjectTrackingScope scope);

		// Token: 0x0600003F RID: 63
		IMapiObjectCounter GetObjectCounter(MapiObjectTrackingScope scope);
	}
}
