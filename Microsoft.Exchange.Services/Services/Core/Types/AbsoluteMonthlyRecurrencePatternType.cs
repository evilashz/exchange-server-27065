using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200061E RID: 1566
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "AbsoluteMonthlyRecurrence")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AbsoluteMonthlyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000B6DA8 File Offset: 0x000B4FA8
		// (set) Token: 0x0600311F RID: 12575 RVA: 0x000B6DB0 File Offset: 0x000B4FB0
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public int DayOfMonth { get; set; }
	}
}
