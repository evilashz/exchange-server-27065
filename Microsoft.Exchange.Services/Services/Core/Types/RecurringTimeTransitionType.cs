using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000679 RID: 1657
	[XmlInclude(typeof(RecurringDayTransitionType))]
	[XmlInclude(typeof(RecurringDateTransitionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(RecurringDateTransitionType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(RecurringDayTransitionType))]
	[Serializable]
	public abstract class RecurringTimeTransitionType : TransitionType
	{
		// Token: 0x060032E5 RID: 13029 RVA: 0x000B8270 File Offset: 0x000B6470
		public RecurringTimeTransitionType()
		{
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000B8278 File Offset: 0x000B6478
		public RecurringTimeTransitionType(TransitionTargetType to, string timeOffset, int month) : base(to)
		{
			this.TimeOffset = timeOffset;
			this.Month = month;
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x000B828F File Offset: 0x000B648F
		// (set) Token: 0x060032E8 RID: 13032 RVA: 0x000B8297 File Offset: 0x000B6497
		[XmlElement(DataType = "duration")]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string TimeOffset { get; set; }

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000B82A0 File Offset: 0x000B64A0
		// (set) Token: 0x060032EA RID: 13034 RVA: 0x000B82A8 File Offset: 0x000B64A8
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public int Month { get; set; }
	}
}
