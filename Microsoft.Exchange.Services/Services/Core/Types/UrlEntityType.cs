using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000687 RID: 1671
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UrlEntityType : BaseEntityType
	{
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000B84B7 File Offset: 0x000B66B7
		// (set) Token: 0x06003326 RID: 13094 RVA: 0x000B84BF File Offset: 0x000B66BF
		[XmlElement]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Url { get; set; }
	}
}
