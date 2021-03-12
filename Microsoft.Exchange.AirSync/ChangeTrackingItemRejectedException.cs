using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	internal class ChangeTrackingItemRejectedException : AirSyncPermanentException
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x0001D386 File Offset: 0x0001B586
		internal ChangeTrackingItemRejectedException() : base(false)
		{
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001D38F File Offset: 0x0001B58F
		protected ChangeTrackingItemRejectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
