using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200067B RID: 1659
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "RecurringDateTransition")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RecurringDateTransitionType : RecurringTimeTransitionType
	{
		// Token: 0x060032F1 RID: 13041 RVA: 0x000B82F6 File Offset: 0x000B64F6
		public RecurringDateTransitionType()
		{
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000B82FE File Offset: 0x000B64FE
		public RecurringDateTransitionType(TransitionTargetType to, string timeOffset, int month, int day) : base(to, timeOffset, month)
		{
			this.Day = day;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000B8311 File Offset: 0x000B6511
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x000B8319 File Offset: 0x000B6519
		[DataMember(EmitDefaultValue = false)]
		public int Day { get; set; }
	}
}
