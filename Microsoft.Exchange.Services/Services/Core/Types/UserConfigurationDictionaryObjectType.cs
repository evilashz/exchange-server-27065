using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000693 RID: 1683
	[XmlType("UserConfigurationDictionaryObjectTypesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UserConfigurationDictionaryObjectType
	{
		// Token: 0x04001D28 RID: 7464
		DateTime,
		// Token: 0x04001D29 RID: 7465
		Boolean,
		// Token: 0x04001D2A RID: 7466
		Byte,
		// Token: 0x04001D2B RID: 7467
		String,
		// Token: 0x04001D2C RID: 7468
		Integer32,
		// Token: 0x04001D2D RID: 7469
		UnsignedInteger32,
		// Token: 0x04001D2E RID: 7470
		Integer64,
		// Token: 0x04001D2F RID: 7471
		UnsignedInteger64,
		// Token: 0x04001D30 RID: 7472
		StringArray,
		// Token: 0x04001D31 RID: 7473
		ByteArray
	}
}
