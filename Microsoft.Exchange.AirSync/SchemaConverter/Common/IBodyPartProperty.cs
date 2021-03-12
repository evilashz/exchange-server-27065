using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200012E RID: 302
	internal interface IBodyPartProperty : IProperty
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000F5F RID: 3935
		string Preview { get; }

		// Token: 0x06000F60 RID: 3936
		Stream GetData(BodyType type, long truncationSize, out long estimatedDataSize, out IEnumerable<AirSyncAttachmentInfo> attachments);
	}
}
