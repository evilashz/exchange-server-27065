using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200084B RID: 2123
	[XmlType(TypeName = "PreviewItemBaseShapeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PreviewItemBaseShape
	{
		// Token: 0x040021C9 RID: 8649
		Default,
		// Token: 0x040021CA RID: 8650
		Compact
	}
}
