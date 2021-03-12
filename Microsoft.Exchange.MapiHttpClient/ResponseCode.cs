using System;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002D RID: 45
	public enum ResponseCode : uint
	{
		// Token: 0x040000DC RID: 220
		Success,
		// Token: 0x040000DD RID: 221
		UnknownFailure,
		// Token: 0x040000DE RID: 222
		InvalidVerb,
		// Token: 0x040000DF RID: 223
		InvalidPath,
		// Token: 0x040000E0 RID: 224
		InvalidHeader,
		// Token: 0x040000E1 RID: 225
		InvalidRequestType,
		// Token: 0x040000E2 RID: 226
		InvalidContextCookie,
		// Token: 0x040000E3 RID: 227
		MissingHeader,
		// Token: 0x040000E4 RID: 228
		AnonymousNotAllowed,
		// Token: 0x040000E5 RID: 229
		TooLarge,
		// Token: 0x040000E6 RID: 230
		ContextNotFound,
		// Token: 0x040000E7 RID: 231
		NotContextOwner,
		// Token: 0x040000E8 RID: 232
		InvalidPayload,
		// Token: 0x040000E9 RID: 233
		MissingCookie,
		// Token: 0x040000EA RID: 234
		NoClient,
		// Token: 0x040000EB RID: 235
		InvalidSequence,
		// Token: 0x040000EC RID: 236
		EndpointDisabled,
		// Token: 0x040000ED RID: 237
		InvalidResponse,
		// Token: 0x040000EE RID: 238
		EndpointShuttingDown
	}
}
