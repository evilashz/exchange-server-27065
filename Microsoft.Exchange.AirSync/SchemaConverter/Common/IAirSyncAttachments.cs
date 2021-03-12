using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200012F RID: 303
	internal interface IAirSyncAttachments
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000F61 RID: 3937
		IEnumerable<AirSyncAttachmentInfo> Attachments { get; }
	}
}
