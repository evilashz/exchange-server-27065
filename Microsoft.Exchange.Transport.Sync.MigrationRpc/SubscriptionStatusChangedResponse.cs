using System;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200001F RID: 31
	[Flags]
	internal enum SubscriptionStatusChangedResponse
	{
		// Token: 0x040000AC RID: 172
		OK = 1,
		// Token: 0x040000AD RID: 173
		ActionRequired = 40960,
		// Token: 0x040000AE RID: 174
		Delete = 40961,
		// Token: 0x040000AF RID: 175
		Disable = 40962
	}
}
