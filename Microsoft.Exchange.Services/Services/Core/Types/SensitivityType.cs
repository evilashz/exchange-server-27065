using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000878 RID: 2168
	[XmlType(TypeName = "SensitivityChoicesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SensitivityType
	{
		// Token: 0x040023C4 RID: 9156
		Normal,
		// Token: 0x040023C5 RID: 9157
		Personal,
		// Token: 0x040023C6 RID: 9158
		Private,
		// Token: 0x040023C7 RID: 9159
		Confidential
	}
}
