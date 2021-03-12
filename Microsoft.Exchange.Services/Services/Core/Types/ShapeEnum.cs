using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200088E RID: 2190
	[XmlType(TypeName = "DefaultShapeNamesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ShapeEnum
	{
		// Token: 0x040023FA RID: 9210
		IdOnly,
		// Token: 0x040023FB RID: 9211
		Default,
		// Token: 0x040023FC RID: 9212
		AllProperties
	}
}
