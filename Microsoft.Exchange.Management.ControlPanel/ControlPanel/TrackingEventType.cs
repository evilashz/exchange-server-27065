using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002DC RID: 732
	[DataContract]
	public enum TrackingEventType
	{
		// Token: 0x04002213 RID: 8723
		[EnumMember]
		SmtpReceive,
		// Token: 0x04002214 RID: 8724
		[EnumMember]
		SmtpSend,
		// Token: 0x04002215 RID: 8725
		[EnumMember]
		Fail,
		// Token: 0x04002216 RID: 8726
		[EnumMember]
		Deliver,
		// Token: 0x04002217 RID: 8727
		[EnumMember]
		Resolve,
		// Token: 0x04002218 RID: 8728
		[EnumMember]
		Expand,
		// Token: 0x04002219 RID: 8729
		[EnumMember]
		Redirect,
		// Token: 0x0400221A RID: 8730
		[EnumMember]
		Submit,
		// Token: 0x0400221B RID: 8731
		[EnumMember]
		Defer,
		// Token: 0x0400221C RID: 8732
		[EnumMember]
		InitMessageCreated,
		// Token: 0x0400221D RID: 8733
		[EnumMember]
		ModeratorRejected,
		// Token: 0x0400221E RID: 8734
		[EnumMember]
		ModeratorApprove,
		// Token: 0x0400221F RID: 8735
		[EnumMember]
		Pending,
		// Token: 0x04002220 RID: 8736
		[EnumMember]
		Transferred,
		// Token: 0x04002221 RID: 8737
		[EnumMember]
		None = 99
	}
}
