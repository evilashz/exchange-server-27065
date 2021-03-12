using System;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x0200001A RID: 26
	internal enum AutodiscoverHttpStatus
	{
		// Token: 0x040000C9 RID: 201
		OK = 200,
		// Token: 0x040000CA RID: 202
		MovedPermanently = 301,
		// Token: 0x040000CB RID: 203
		Redirect,
		// Token: 0x040000CC RID: 204
		TemporaryRedirect = 307,
		// Token: 0x040000CD RID: 205
		Unauthorized = 401,
		// Token: 0x040000CE RID: 206
		NotFound = 404,
		// Token: 0x040000CF RID: 207
		InternalServerError = 500,
		// Token: 0x040000D0 RID: 208
		ProxyError = 502,
		// Token: 0x040000D1 RID: 209
		ServiceUnavailable
	}
}
