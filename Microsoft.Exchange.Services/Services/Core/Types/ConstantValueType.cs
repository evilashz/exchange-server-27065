using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200063F RID: 1599
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Constant")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ConstantValueType
	{
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000B7555 File Offset: 0x000B5755
		// (set) Token: 0x060031D3 RID: 12755 RVA: 0x000B755D File Offset: 0x000B575D
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false)]
		public string Value { get; set; }
	}
}
