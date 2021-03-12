using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000153 RID: 339
	[DataContract]
	public enum ItemIsRuleFormType
	{
		// Token: 0x040006E5 RID: 1765
		[EnumMember]
		Edit = 1,
		// Token: 0x040006E6 RID: 1766
		[EnumMember]
		Read,
		// Token: 0x040006E7 RID: 1767
		[EnumMember]
		ReadOrEdit
	}
}
