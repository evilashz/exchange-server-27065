using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000152 RID: 338
	[DataContract]
	public enum ItemIsRuleItemType
	{
		// Token: 0x040006E2 RID: 1762
		[EnumMember]
		Message = 1,
		// Token: 0x040006E3 RID: 1763
		[EnumMember]
		Appointment
	}
}
