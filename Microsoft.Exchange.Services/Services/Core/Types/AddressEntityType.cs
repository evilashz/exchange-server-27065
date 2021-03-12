using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200059C RID: 1436
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AddressEntityType : BaseEntityType
	{
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000AC749 File Offset: 0x000AA949
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x000AC751 File Offset: 0x000AA951
		[XmlElement]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Address { get; set; }
	}
}
