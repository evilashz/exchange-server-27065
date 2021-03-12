using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E6 RID: 1766
	[XmlType(TypeName = "BodyTypeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum BodyType
	{
		// Token: 0x04001E39 RID: 7737
		[XmlEnum("Text")]
		Text,
		// Token: 0x04001E3A RID: 7738
		[XmlEnum("HTML")]
		HTML
	}
}
