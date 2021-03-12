using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	internal class DeviceFilterInvalidException : AirSyncPermanentException
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00022287 File Offset: 0x00020487
		public DeviceFilterInvalidException(LocalizedString message) : this(message, null)
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00022291 File Offset: 0x00020491
		public DeviceFilterInvalidException(LocalizedString message, Exception innerException) : base(message, innerException, true)
		{
		}
	}
}
