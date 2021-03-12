using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200067A RID: 1658
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "RecurringDayTransition")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RecurringDayTransitionType : RecurringTimeTransitionType
	{
		// Token: 0x060032EB RID: 13035 RVA: 0x000B82B1 File Offset: 0x000B64B1
		public RecurringDayTransitionType()
		{
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000B82B9 File Offset: 0x000B64B9
		public RecurringDayTransitionType(TransitionTargetType to, string timeOffset, int month, string dayOfWeek, int occurrence) : base(to, timeOffset, month)
		{
			this.DayOfWeek = dayOfWeek;
			this.Occurrence = occurrence;
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x000B82D4 File Offset: 0x000B64D4
		// (set) Token: 0x060032EE RID: 13038 RVA: 0x000B82DC File Offset: 0x000B64DC
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string DayOfWeek { get; set; }

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x000B82E5 File Offset: 0x000B64E5
		// (set) Token: 0x060032F0 RID: 13040 RVA: 0x000B82ED File Offset: 0x000B64ED
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public int Occurrence { get; set; }
	}
}
