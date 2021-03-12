using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000629 RID: 1577
	[XmlInclude(typeof(NumberedRecurrenceRangeType))]
	[XmlInclude(typeof(EndDateRecurrenceRangeType))]
	[XmlInclude(typeof(NoEndRecurrenceRangeType))]
	[KnownType(typeof(NoEndRecurrenceRangeType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(NumberedRecurrenceRangeType))]
	[KnownType(typeof(EndDateRecurrenceRangeType))]
	[Serializable]
	public abstract class RecurrenceRangeBaseType
	{
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000B6EE0 File Offset: 0x000B50E0
		// (set) Token: 0x06003142 RID: 12610 RVA: 0x000B6EE8 File Offset: 0x000B50E8
		[DateTimeString]
		[XmlElement]
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public string StartDate { get; set; }
	}
}
