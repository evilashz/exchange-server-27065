using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000870 RID: 2160
	[Flags]
	[XmlType(TypeName = "SearchResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SearchResultType
	{
		// Token: 0x04002399 RID: 9113
		StatisticsOnly = 1,
		// Token: 0x0400239A RID: 9114
		PreviewOnly = 2
	}
}
