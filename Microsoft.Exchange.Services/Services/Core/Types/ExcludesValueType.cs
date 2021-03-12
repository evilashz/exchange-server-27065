using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000642 RID: 1602
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExcludesValueType
	{
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000B75E9 File Offset: 0x000B57E9
		// (set) Token: 0x060031E2 RID: 12770 RVA: 0x000B75F1 File Offset: 0x000B57F1
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false)]
		public string Value { get; set; }
	}
}
