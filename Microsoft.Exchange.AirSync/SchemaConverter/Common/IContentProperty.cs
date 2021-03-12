using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000140 RID: 320
	internal interface IContentProperty : IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000F9B RID: 3995
		Stream Body { get; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000F9C RID: 3996
		long Size { get; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000F9D RID: 3997
		bool IsIrmErrorMessage { get; }

		// Token: 0x06000F9E RID: 3998
		Stream GetData(BodyType type, long truncationSize, out long estimatedDataSize, out IEnumerable<AirSyncAttachmentInfo> attachments);

		// Token: 0x06000F9F RID: 3999
		BodyType GetNativeType();

		// Token: 0x06000FA0 RID: 4000
		void PreProcessProperty();

		// Token: 0x06000FA1 RID: 4001
		void PostProcessProperty();
	}
}
