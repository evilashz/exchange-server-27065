using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005CA RID: 1482
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum LocationSourceType
	{
		// Token: 0x04001AD7 RID: 6871
		None,
		// Token: 0x04001AD8 RID: 6872
		LocationServices,
		// Token: 0x04001AD9 RID: 6873
		PhonebookServices,
		// Token: 0x04001ADA RID: 6874
		Device,
		// Token: 0x04001ADB RID: 6875
		Contact,
		// Token: 0x04001ADC RID: 6876
		Resource
	}
}
