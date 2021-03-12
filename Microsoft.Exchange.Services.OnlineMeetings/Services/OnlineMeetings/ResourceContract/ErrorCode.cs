using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000086 RID: 134
	[DataContract(Name = "code")]
	internal enum ErrorCode
	{
		// Token: 0x04000247 RID: 583
		[EnumMember]
		BadRequest = 400,
		// Token: 0x04000248 RID: 584
		[EnumMember]
		Forbidden = 403,
		// Token: 0x04000249 RID: 585
		[EnumMember]
		ResourceNotFound,
		// Token: 0x0400024A RID: 586
		[EnumMember]
		MethodNotAllowed,
		// Token: 0x0400024B RID: 587
		[EnumMember]
		Conflict = 409,
		// Token: 0x0400024C RID: 588
		[EnumMember]
		InvalidOperation = 422,
		// Token: 0x0400024D RID: 589
		[EnumMember]
		TooManyRequests = 429,
		// Token: 0x0400024E RID: 590
		[EnumMember]
		ServiceFailure = 500
	}
}
