using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A1 RID: 1953
	[XmlType(TypeName = "ImportanceChoicesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ImportanceType
	{
		// Token: 0x040020A3 RID: 8355
		Low,
		// Token: 0x040020A4 RID: 8356
		Normal,
		// Token: 0x040020A5 RID: 8357
		High
	}
}
