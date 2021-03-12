using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000155 RID: 341
	[DataContract]
	public enum RegExPropertyName
	{
		// Token: 0x040006F1 RID: 1777
		[EnumMember]
		Subject = 1,
		// Token: 0x040006F2 RID: 1778
		[EnumMember]
		BodyAsPlaintext,
		// Token: 0x040006F3 RID: 1779
		[EnumMember]
		BodyAsHTML,
		// Token: 0x040006F4 RID: 1780
		[EnumMember]
		SenderSMTPAddress
	}
}
