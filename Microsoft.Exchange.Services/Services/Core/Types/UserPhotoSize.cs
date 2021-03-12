using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008BE RID: 2238
	[XmlType(TypeName = "UserPhotoSizeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UserPhotoSize
	{
		// Token: 0x0400244F RID: 9295
		HR48x48,
		// Token: 0x04002450 RID: 9296
		HR64x64,
		// Token: 0x04002451 RID: 9297
		HR96x96,
		// Token: 0x04002452 RID: 9298
		HR120x120,
		// Token: 0x04002453 RID: 9299
		HR240x240,
		// Token: 0x04002454 RID: 9300
		HR360x360,
		// Token: 0x04002455 RID: 9301
		HR432x432,
		// Token: 0x04002456 RID: 9302
		HR504x504,
		// Token: 0x04002457 RID: 9303
		HR648x648
	}
}
