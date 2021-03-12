using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E5 RID: 1765
	[XmlType(TypeName = "BodyTypeResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum BodyResponseType
	{
		// Token: 0x04001E35 RID: 7733
		[XmlEnum("Best")]
		Best,
		// Token: 0x04001E36 RID: 7734
		[XmlEnum("Text")]
		Text,
		// Token: 0x04001E37 RID: 7735
		[XmlEnum("HTML")]
		HTML
	}
}
