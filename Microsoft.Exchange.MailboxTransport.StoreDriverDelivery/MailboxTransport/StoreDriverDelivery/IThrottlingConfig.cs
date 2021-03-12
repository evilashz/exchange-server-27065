using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200003B RID: 59
	internal interface IThrottlingConfig
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000298 RID: 664
		int RecipientThreadLimit { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000299 RID: 665
		int MaxConcurrentMailboxDeliveries { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600029A RID: 666
		int MailboxServerThreadLimit { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600029B RID: 667
		int MaxMailboxDeliveryPerMdbConnections { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600029C RID: 668
		bool MailboxDeliveryThrottlingEnabled { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600029D RID: 669
		int MdbHealthMediumToHighThreshold { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600029E RID: 670
		int MaxMailboxDeliveryPerMdbConnectionsHighHealthPercent { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600029F RID: 671
		int MdbHealthLowToMediumThreshold { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002A0 RID: 672
		int MaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A1 RID: 673
		int MaxMailboxDeliveryPerMdbConnectionsLowHealthPercent { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002A2 RID: 674
		int MaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002A3 RID: 675
		bool DynamicMailboxDatabaseThrottlingEnabled { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002A4 RID: 676
		TimeSpan AcquireConnectionTimeout { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002A5 RID: 677
		ulong MaxConcurrentMessageSizeLimit { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002A6 RID: 678
		bool ThrottlingLogEnabled { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002A7 RID: 679
		EnhancedTimeSpan ThrottlingLogMaxAge { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002A8 RID: 680
		Unlimited<ByteQuantifiedSize> ThrottlingLogMaxDirectorySize { get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002A9 RID: 681
		Unlimited<ByteQuantifiedSize> ThrottlingLogMaxFileSize { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002AA RID: 682
		LocalLongFullPath ThrottlingLogPath { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002AB RID: 683
		int ThrottlingLogBufferSize { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002AC RID: 684
		TimeSpan ThrottlingLogFlushInterval { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002AD RID: 685
		TimeSpan AsyncLogInterval { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002AE RID: 686
		TimeSpan ThrottlingSummaryLoggingInterval { get; }
	}
}
