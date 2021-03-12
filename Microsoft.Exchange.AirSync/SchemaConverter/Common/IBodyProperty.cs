using System;
using System.IO;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000132 RID: 306
	internal interface IBodyProperty : IMIMERelatedProperty, IProperty
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000F69 RID: 3945
		Stream RtfData { get; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000F6A RID: 3946
		bool RtfPresent { get; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000F6B RID: 3947
		int RtfSize { get; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000F6C RID: 3948
		Stream TextData { get; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000F6D RID: 3949
		bool TextPresent { get; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000F6E RID: 3950
		int TextSize { get; }

		// Token: 0x06000F6F RID: 3951
		Stream GetTextData(int length);
	}
}
