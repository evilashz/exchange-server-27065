using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000694 RID: 1684
	[XmlType("UserConfigurationPropertyType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[Serializable]
	public enum UserConfigurationProperties
	{
		// Token: 0x04001D33 RID: 7475
		Id = 1,
		// Token: 0x04001D34 RID: 7476
		Dictionary = 2,
		// Token: 0x04001D35 RID: 7477
		XmlData = 4,
		// Token: 0x04001D36 RID: 7478
		BinaryData = 8,
		// Token: 0x04001D37 RID: 7479
		All = 15
	}
}
