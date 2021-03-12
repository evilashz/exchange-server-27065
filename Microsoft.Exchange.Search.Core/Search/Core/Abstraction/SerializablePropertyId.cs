using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000045 RID: 69
	internal enum SerializablePropertyId : byte
	{
		// Token: 0x04000079 RID: 121
		None,
		// Token: 0x0400007A RID: 122
		AnnotationToken,
		// Token: 0x0400007B RID: 123
		Tasks,
		// Token: 0x0400007C RID: 124
		Meetings = 4,
		// Token: 0x0400007D RID: 125
		Addresses,
		// Token: 0x0400007E RID: 126
		Keywords,
		// Token: 0x0400007F RID: 127
		Phones = 9,
		// Token: 0x04000080 RID: 128
		Emails,
		// Token: 0x04000081 RID: 129
		Urls,
		// Token: 0x04000082 RID: 130
		Contacts,
		// Token: 0x04000083 RID: 131
		Language,
		// Token: 0x04000084 RID: 132
		OperatorTiming,
		// Token: 0x04000085 RID: 133
		Mdm,
		// Token: 0x04000086 RID: 134
		Max
	}
}
