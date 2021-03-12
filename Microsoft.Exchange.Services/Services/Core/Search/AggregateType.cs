using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000254 RID: 596
	[XmlType(TypeName = "AggregateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum AggregateType
	{
		// Token: 0x04000BD4 RID: 3028
		[XmlEnum(Name = "Minimum")]
		Minimum,
		// Token: 0x04000BD5 RID: 3029
		[XmlEnum(Name = "Maximum")]
		Maximum
	}
}
