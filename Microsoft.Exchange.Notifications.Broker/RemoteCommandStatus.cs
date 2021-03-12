using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public enum RemoteCommandStatus
	{
		// Token: 0x040000CE RID: 206
		Success = 200,
		// Token: 0x040000CF RID: 207
		UnsupportedCommand = 450,
		// Token: 0x040000D0 RID: 208
		HttpPostFailed = 450,
		// Token: 0x040000D1 RID: 209
		MailboxIsNotHostedLocally,
		// Token: 0x040000D2 RID: 210
		BrokerProtocolIsTooOld,
		// Token: 0x040000D3 RID: 211
		BrokerProtocolIsTooNew
	}
}
