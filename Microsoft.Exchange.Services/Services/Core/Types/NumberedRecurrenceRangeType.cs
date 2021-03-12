using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200062B RID: 1579
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "NumberedRecurrence")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NumberedRecurrenceRangeType : RecurrenceRangeBaseType
	{
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000B6F12 File Offset: 0x000B5112
		// (set) Token: 0x06003148 RID: 12616 RVA: 0x000B6F1A File Offset: 0x000B511A
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public int NumberOfOccurrences { get; set; }
	}
}
