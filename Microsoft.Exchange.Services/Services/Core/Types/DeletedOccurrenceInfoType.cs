using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B6 RID: 1462
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DeletedOccurrenceInfoType
	{
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000B1113 File Offset: 0x000AF313
		// (set) Token: 0x06002C80 RID: 11392 RVA: 0x000B111B File Offset: 0x000AF31B
		[DataMember(IsRequired = true, EmitDefaultValue = true)]
		[XmlElement]
		public string Start { get; set; }
	}
}
