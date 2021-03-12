using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200000E RID: 14
	internal enum ErrorCode
	{
		// Token: 0x04000035 RID: 53
		Unknown,
		// Token: 0x04000036 RID: 54
		BadRequest = 400,
		// Token: 0x04000037 RID: 55
		Forbidden = 403,
		// Token: 0x04000038 RID: 56
		NotFound,
		// Token: 0x04000039 RID: 57
		MethodNotAllowed,
		// Token: 0x0400003A RID: 58
		ClientTimeout = 408,
		// Token: 0x0400003B RID: 59
		Conflict,
		// Token: 0x0400003C RID: 60
		Gone,
		// Token: 0x0400003D RID: 61
		PreconditionFailed = 412,
		// Token: 0x0400003E RID: 62
		EntityTooLarge,
		// Token: 0x0400003F RID: 63
		UnsupportedMediaType = 415,
		// Token: 0x04000040 RID: 64
		PreconditionRequired = 428,
		// Token: 0x04000041 RID: 65
		TooManyRequests,
		// Token: 0x04000042 RID: 66
		ServiceFailure = 500,
		// Token: 0x04000043 RID: 67
		BadGateway = 502,
		// Token: 0x04000044 RID: 68
		ServiceUnavailable,
		// Token: 0x04000045 RID: 69
		Timeout,
		// Token: 0x04000046 RID: 70
		ExpectedFailure = 700,
		// Token: 0x04000047 RID: 71
		LocalFailure = 710,
		// Token: 0x04000048 RID: 72
		RemoteFailure = 720,
		// Token: 0x04000049 RID: 73
		Informational = 730
	}
}
