using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000729 RID: 1833
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ConflictResultsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ConflictResults
	{
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000C59CA File Offset: 0x000C3BCA
		// (set) Token: 0x06003790 RID: 14224 RVA: 0x000C59D2 File Offset: 0x000C3BD2
		[XmlElement("Count")]
		[DataMember(Name = "Count", EmitDefaultValue = true, Order = 0)]
		public int Count { get; set; }
	}
}
