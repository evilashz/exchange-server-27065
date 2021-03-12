using System;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x02000012 RID: 18
	public enum HttpStatus
	{
		// Token: 0x040000A2 RID: 162
		OK = 200,
		// Token: 0x040000A3 RID: 163
		BadRequest = 400,
		// Token: 0x040000A4 RID: 164
		Unauthorized,
		// Token: 0x040000A5 RID: 165
		Forbidden = 403,
		// Token: 0x040000A6 RID: 166
		NotFound,
		// Token: 0x040000A7 RID: 167
		NeedProvisioning = 449,
		// Token: 0x040000A8 RID: 168
		ActiveSyncRedirect = 451,
		// Token: 0x040000A9 RID: 169
		InternalServerError = 500,
		// Token: 0x040000AA RID: 170
		NotImplemented,
		// Token: 0x040000AB RID: 171
		ProxyError,
		// Token: 0x040000AC RID: 172
		ServiceUnavailable,
		// Token: 0x040000AD RID: 173
		VersionNotSupported = 505,
		// Token: 0x040000AE RID: 174
		InsufficientDiskSapce = 507
	}
}
