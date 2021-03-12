using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200062A RID: 1578
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "EndDateRecurrence")]
	[Serializable]
	public class EndDateRecurrenceRangeType : RecurrenceRangeBaseType
	{
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000B6EF9 File Offset: 0x000B50F9
		// (set) Token: 0x06003145 RID: 12613 RVA: 0x000B6F01 File Offset: 0x000B5101
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		[XmlElement]
		[DateTimeString]
		public string EndDate { get; set; }
	}
}
