using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200037B RID: 891
	internal interface IShadowRedundancyConfigurationSource
	{
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600269B RID: 9883
		bool Enabled { get; }

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600269C RID: 9884
		ShadowRedundancyCompatibilityVersion CompatibilityVersion { get; }

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600269D RID: 9885
		TimeSpan ShadowMessageAutoDiscardInterval { get; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600269E RID: 9886
		TimeSpan DiscardEventExpireInterval { get; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600269F RID: 9887
		TimeSpan QueueMaxIdleTimeInterval { get; }

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060026A0 RID: 9888
		TimeSpan ShadowServerInfoMaxIdleTimeInterval { get; }

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060026A1 RID: 9889
		TimeSpan ShadowQueueCheckExpiryInterval { get; }

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060026A2 RID: 9890
		TimeSpan DelayedAckCheckExpiryInterval { get; }

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060026A3 RID: 9891
		bool DelayedAckSkippingEnabled { get; }

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060026A4 RID: 9892
		int DelayedAckSkippingQueueLength { get; }

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060026A5 RID: 9893
		TimeSpan DiscardEventsCheckExpiryInterval { get; }

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060026A6 RID: 9894
		TimeSpan StringPoolCleanupInterval { get; }

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060026A7 RID: 9895
		TimeSpan PrimaryServerInfoCleanupInterval { get; }

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060026A8 RID: 9896
		int PrimaryServerInfoHardCleanupThreshold { get; }

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060026A9 RID: 9897
		TimeSpan HeartbeatFrequency { get; }

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060026AA RID: 9898
		int HeartbeatRetryCount { get; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060026AB RID: 9899
		int MaxRemoteShadowAttempts { get; }

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060026AC RID: 9900
		int MaxLocalShadowAttempts { get; }

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060026AD RID: 9901
		ShadowMessagePreference ShadowMessagePreference { get; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060026AE RID: 9902
		bool RejectMessageOnShadowFailure { get; }

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060026AF RID: 9903
		int MaxDiscardIdsPerSmtpCommand { get; }

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060026B0 RID: 9904
		TimeSpan MaxPendingHeartbeatInterval { get; }

		// Token: 0x060026B1 RID: 9905
		void Load();

		// Token: 0x060026B2 RID: 9906
		void Unload();

		// Token: 0x060026B3 RID: 9907
		void SetShadowRedundancyConfigChangeNotification(ShadowRedundancyConfigChange shadowRedundancyConfigChange);
	}
}
