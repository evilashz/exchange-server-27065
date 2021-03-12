using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000476 RID: 1142
	[XmlType(TypeName = "SearchPageDirectionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SearchPageDirectionType
	{
		// Token: 0x040014AC RID: 5292
		Next,
		// Token: 0x040014AD RID: 5293
		Previous
	}
}
