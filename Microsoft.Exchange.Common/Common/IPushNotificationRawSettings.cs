using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200006F RID: 111
	public interface IPushNotificationRawSettings : IEquatable<IPushNotificationRawSettings>
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000242 RID: 578
		string Name { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000243 RID: 579
		PushNotificationPlatform Platform { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000244 RID: 580
		bool? Enabled { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000245 RID: 581
		Version ExchangeMinimumVersion { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000246 RID: 582
		Version ExchangeMaximumVersion { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000247 RID: 583
		int? QueueSize { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000248 RID: 584
		int? NumberOfChannels { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000249 RID: 585
		int? AddTimeout { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600024A RID: 586
		string AuthenticationId { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600024B RID: 587
		string AuthenticationKey { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600024C RID: 588
		string AuthenticationKeyFallback { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600024D RID: 589
		bool? IsAuthenticationKeyEncrypted { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600024E RID: 590
		string Url { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600024F RID: 591
		int? Port { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000250 RID: 592
		string SecondaryUrl { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000251 RID: 593
		int? SecondaryPort { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000252 RID: 594
		int? ConnectStepTimeout { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000253 RID: 595
		int? ConnectTotalTimeout { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000254 RID: 596
		int? ConnectRetryDelay { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000255 RID: 597
		int? ConnectRetryMax { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000256 RID: 598
		int? AuthenticateRetryMax { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000257 RID: 599
		int? ReadTimeout { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000258 RID: 600
		int? WriteTimeout { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000259 RID: 601
		bool? IgnoreCertificateErrors { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600025A RID: 602
		int? BackOffTimeInSeconds { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600025B RID: 603
		string UriTemplate { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600025C RID: 604
		int? MaximumCacheSize { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600025D RID: 605
		string RegistrationTemplate { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600025E RID: 606
		bool? RegistrationEnabled { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600025F RID: 607
		bool? MultifactorRegistrationEnabled { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000260 RID: 608
		string PartitionName { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000261 RID: 609
		bool? IsDefaultPartitionName { get; }

		// Token: 0x06000262 RID: 610
		string ToFullString();
	}
}
