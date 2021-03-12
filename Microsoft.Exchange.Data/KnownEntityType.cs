using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000154 RID: 340
	[DataContract]
	public enum KnownEntityType
	{
		// Token: 0x040006E9 RID: 1769
		[EnumMember]
		MeetingSuggestion = 1,
		// Token: 0x040006EA RID: 1770
		[EnumMember]
		TaskSuggestion,
		// Token: 0x040006EB RID: 1771
		[EnumMember]
		Address,
		// Token: 0x040006EC RID: 1772
		[EnumMember]
		Url,
		// Token: 0x040006ED RID: 1773
		[EnumMember]
		PhoneNumber,
		// Token: 0x040006EE RID: 1774
		[EnumMember]
		EmailAddress,
		// Token: 0x040006EF RID: 1775
		[EnumMember]
		Contact
	}
}
