using System;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000592 RID: 1426
	[XmlType(TypeName = "RulePredicateDateRangeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RulePredicateDateRange
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x000AAB0D File Offset: 0x000A8D0D
		// (set) Token: 0x060027CD RID: 10189 RVA: 0x000AAB15 File Offset: 0x000A8D15
		[XmlElement(Order = 0)]
		public string StartDateTime { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x000AAB1E File Offset: 0x000A8D1E
		// (set) Token: 0x060027CF RID: 10191 RVA: 0x000AAB26 File Offset: 0x000A8D26
		[XmlIgnore]
		public bool StartDateTimeSpecified { get; set; }

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x000AAB2F File Offset: 0x000A8D2F
		// (set) Token: 0x060027D1 RID: 10193 RVA: 0x000AAB37 File Offset: 0x000A8D37
		[XmlElement(Order = 1)]
		public string EndDateTime { get; set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x000AAB40 File Offset: 0x000A8D40
		// (set) Token: 0x060027D3 RID: 10195 RVA: 0x000AAB48 File Offset: 0x000A8D48
		[XmlIgnore]
		public bool EndDateTimeSpecified { get; set; }

		// Token: 0x060027D4 RID: 10196 RVA: 0x000AAB51 File Offset: 0x000A8D51
		public RulePredicateDateRange()
		{
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000AAB5C File Offset: 0x000A8D5C
		public RulePredicateDateRange(ExDateTime? startTime, ExDateTime? endTime)
		{
			if (startTime != null)
			{
				this.StartDateTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(startTime.Value);
				this.StartDateTimeSpecified = true;
			}
			if (endTime != null)
			{
				this.EndDateTime = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(endTime.Value);
				this.EndDateTimeSpecified = true;
			}
		}
	}
}
