using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000676 RID: 1654
	[XmlInclude(typeof(AbsoluteDateTransitionType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Transition")]
	[XmlInclude(typeof(RecurringDateTransitionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(AbsoluteDateTransitionType))]
	[XmlInclude(typeof(RecurringTimeTransitionType))]
	[XmlInclude(typeof(RecurringDayTransitionType))]
	[KnownType(typeof(RecurringTimeTransitionType))]
	[KnownType(typeof(RecurringDayTransitionType))]
	[KnownType(typeof(RecurringDateTransitionType))]
	[Serializable]
	public class TransitionType
	{
		// Token: 0x060032D3 RID: 13011 RVA: 0x000B8177 File Offset: 0x000B6377
		public TransitionType()
		{
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000B817F File Offset: 0x000B637F
		public TransitionType(TransitionTargetType to)
		{
			this.To = to;
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000B818E File Offset: 0x000B638E
		// (set) Token: 0x060032D6 RID: 13014 RVA: 0x000B8196 File Offset: 0x000B6396
		[DataMember(EmitDefaultValue = false)]
		public TransitionTargetType To { get; set; }
	}
}
