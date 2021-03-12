using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000021 RID: 33
	[Flags]
	public enum Step
	{
		// Token: 0x040000EA RID: 234
		None = 0,
		// Token: 0x040000EB RID: 235
		TryExistingEndpoint = 1,
		// Token: 0x040000EC RID: 236
		TrySmtpAddress = 2,
		// Token: 0x040000ED RID: 237
		TryRemovingDomainPrefix = 4,
		// Token: 0x040000EE RID: 238
		TryAddingAutodiscoverPrefix = 8,
		// Token: 0x040000EF RID: 239
		TryUnauthenticatedGet = 16,
		// Token: 0x040000F0 RID: 240
		TryDnsLookupOfSrvRecord = 32,
		// Token: 0x040000F1 RID: 241
		Succeeded = 64,
		// Token: 0x040000F2 RID: 242
		Failed = 128,
		// Token: 0x040000F3 RID: 243
		Done = 192
	}
}
