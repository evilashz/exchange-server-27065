using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000680 RID: 1664
	[XmlType(TypeName = "UMCDRFilterByType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum UMCDRFilterByType
	{
		// Token: 0x04001CD9 RID: 7385
		[XmlEnum("FilterByUser")]
		FilterByUser,
		// Token: 0x04001CDA RID: 7386
		[XmlEnum("FilterByDate")]
		FilterByDate
	}
}
