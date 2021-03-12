using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200037C RID: 892
	internal sealed class ShadowRedundancyConfig : IShadowRedundancyConfigurationSource
	{
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x00095EAC File Offset: 0x000940AC
		public bool Enabled
		{
			get
			{
				return this.shadowRedundancyConfigData.Enabled;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x00095EB9 File Offset: 0x000940B9
		public ShadowRedundancyCompatibilityVersion CompatibilityVersion
		{
			get
			{
				return this.shadowRedundancyConfigData.CompatibilityVersion;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x00095EC6 File Offset: 0x000940C6
		public TimeSpan ShadowMessageAutoDiscardInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.ShadowMessageAutoDiscardInterval;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x00095ED3 File Offset: 0x000940D3
		public TimeSpan DiscardEventExpireInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.ShadowMessageAutoDiscardInterval;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x00095EE0 File Offset: 0x000940E0
		public TimeSpan QueueMaxIdleTimeInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.QueueMaxIdleTimeInterval;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x00095EED File Offset: 0x000940ED
		public TimeSpan ShadowServerInfoMaxIdleTimeInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.ShadowServerInfoMaxIdleTimeInterval;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x00095EFA File Offset: 0x000940FA
		public TimeSpan ShadowQueueCheckExpiryInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.ShadowQueueCheckExpiryInterval;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00095F07 File Offset: 0x00094107
		public TimeSpan DelayedAckCheckExpiryInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.DelayedAckCheckExpiryInterval;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x00095F14 File Offset: 0x00094114
		public bool DelayedAckSkippingEnabled
		{
			get
			{
				return this.shadowRedundancyConfigData.DelayedAckSkippingEnabled;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x00095F21 File Offset: 0x00094121
		public int DelayedAckSkippingQueueLength
		{
			get
			{
				return this.shadowRedundancyConfigData.DelayedAckSkippingQueueLength;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x00095F2E File Offset: 0x0009412E
		public TimeSpan DiscardEventsCheckExpiryInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.DiscardEventsCheckExpiryInterval;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x00095F3B File Offset: 0x0009413B
		public TimeSpan StringPoolCleanupInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.StringPoolCleanupInterval;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x00095F48 File Offset: 0x00094148
		public int PrimaryServerInfoHardCleanupThreshold
		{
			get
			{
				return this.shadowRedundancyConfigData.PrimaryServerInfoHardCleanupThreshold;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x00095F55 File Offset: 0x00094155
		public TimeSpan PrimaryServerInfoCleanupInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.PrimaryServerInfoCleanupInterval;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x00095F62 File Offset: 0x00094162
		public TimeSpan HeartbeatFrequency
		{
			get
			{
				return this.shadowRedundancyConfigData.HeartbeatFrequency;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x00095F6F File Offset: 0x0009416F
		public int HeartbeatRetryCount
		{
			get
			{
				return this.shadowRedundancyConfigData.HeartbeatRetryCount;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x00095F7C File Offset: 0x0009417C
		public int MaxRemoteShadowAttempts
		{
			get
			{
				return this.shadowRedundancyConfigData.MaxRemoteShadowAttempts;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x00095F89 File Offset: 0x00094189
		public int MaxLocalShadowAttempts
		{
			get
			{
				return this.shadowRedundancyConfigData.MaxLocalShadowAttempts;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x00095F96 File Offset: 0x00094196
		public ShadowMessagePreference ShadowMessagePreference
		{
			get
			{
				return this.shadowRedundancyConfigData.ShadowMessagePreference;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x00095FA3 File Offset: 0x000941A3
		public bool RejectMessageOnShadowFailure
		{
			get
			{
				return this.shadowRedundancyConfigData.RejectMessageOnShadowFailure;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x00095FB0 File Offset: 0x000941B0
		public int MaxDiscardIdsPerSmtpCommand
		{
			get
			{
				return this.shadowRedundancyConfigData.MaxDiscardIdsPerSmtpCommand;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x00095FBD File Offset: 0x000941BD
		public TimeSpan MaxPendingHeartbeatInterval
		{
			get
			{
				return this.shadowRedundancyConfigData.MaxPendingHeartbeatInterval;
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00095FCA File Offset: 0x000941CA
		public void Load()
		{
			this.shadowRedundancyConfigData = ShadowRedundancyConfig.ReadTransportConfig(Components.Configuration.TransportSettings.TransportSettings);
			this.RegisterConfigurationChangeHandlers();
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00095FEC File Offset: 0x000941EC
		public void Unload()
		{
			Components.ConfigChanged -= this.ChangeNotificationConfigUpdate;
			Components.Configuration.TransportSettingsChanged -= this.TransportSettingsConfigUpdate;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00096015 File Offset: 0x00094215
		public void SetShadowRedundancyConfigChangeNotification(ShadowRedundancyConfigChange shadowRedundancyConfigChange)
		{
			if (shadowRedundancyConfigChange == null)
			{
				throw new ArgumentNullException("shadowRedundancyConfigChange");
			}
			this.shadowRedundancyConfigChange = shadowRedundancyConfigChange;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0009602C File Offset: 0x0009422C
		private static ShadowRedundancyConfig.ShadowRedundancyConfigData ReadTransportConfig(TransportConfigContainer transportSettings)
		{
			return new ShadowRedundancyConfig.ShadowRedundancyConfigData
			{
				Enabled = (transportSettings.ShadowRedundancyEnabled && !Components.TransportAppConfig.ShadowRedundancy.ShadowRedundancyLocalDisabled),
				HeartbeatFrequency = transportSettings.ShadowHeartbeatFrequency,
				ShadowMessageAutoDiscardInterval = transportSettings.ShadowMessageAutoDiscardInterval,
				MaxRemoteShadowAttempts = transportSettings.MaxRetriesForRemoteSiteShadow,
				MaxLocalShadowAttempts = transportSettings.MaxRetriesForLocalSiteShadow,
				ShadowMessagePreference = transportSettings.ShadowMessagePreferenceSetting,
				RejectMessageOnShadowFailure = transportSettings.RejectMessageOnShadowFailure,
				HeartbeatRetryCount = (int)Math.Ceiling((double)transportSettings.ShadowResubmitTimeSpan.Ticks / (double)transportSettings.ShadowHeartbeatFrequency.Ticks)
			};
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x00096120 File Offset: 0x00094320
		private void RegisterConfigurationChangeHandlers()
		{
			ADObjectId orgAdObjectId = null;
			IConfigurationSession adSession;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				adSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 311, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\ShadowRedundancy\\ShadowRedundancyConfig.cs");
				orgAdObjectId = adSession.GetOrgContainerId();
			});
			if (!adoperationResult.Succeeded)
			{
				throw new TransportComponentLoadFailedException(Strings.ReadOrgContainerFailed, adoperationResult.Exception);
			}
			Components.ConfigChanged += this.ChangeNotificationConfigUpdate;
			Components.Configuration.TransportSettingsChanged += this.TransportSettingsConfigUpdate;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00096191 File Offset: 0x00094391
		private void ChangeNotificationConfigUpdate(object source, EventArgs args)
		{
			this.TransportSettingsConfigUpdate(Components.Configuration.TransportSettings);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000961A4 File Offset: 0x000943A4
		private void TransportSettingsConfigUpdate(TransportSettingsConfiguration transportSettingsConfig)
		{
			ShadowRedundancyConfig.ShadowRedundancyConfigData shadowRedundancyConfigData = ShadowRedundancyConfig.ReadTransportConfig(transportSettingsConfig.TransportSettings);
			if (shadowRedundancyConfigData != null && !this.shadowRedundancyConfigData.Equals(shadowRedundancyConfigData))
			{
				ShadowRedundancyConfig.ShadowRedundancyConfigData oldConfiguration = this.shadowRedundancyConfigData;
				this.shadowRedundancyConfigData = shadowRedundancyConfigData;
				if (this.shadowRedundancyConfigChange != null)
				{
					this.shadowRedundancyConfigChange(oldConfiguration);
				}
			}
		}

		// Token: 0x040013C5 RID: 5061
		private ShadowRedundancyConfig.ShadowRedundancyConfigData shadowRedundancyConfigData = new ShadowRedundancyConfig.ShadowRedundancyConfigData();

		// Token: 0x040013C6 RID: 5062
		private ShadowRedundancyConfigChange shadowRedundancyConfigChange;

		// Token: 0x0200037D RID: 893
		private sealed class ShadowRedundancyConfigData : IShadowRedundancyConfigurationSource
		{
			// Token: 0x17000BBB RID: 3003
			// (get) Token: 0x060026D2 RID: 9938 RVA: 0x00096203 File Offset: 0x00094403
			// (set) Token: 0x060026D3 RID: 9939 RVA: 0x0009620B File Offset: 0x0009440B
			public bool Enabled
			{
				get
				{
					return this.shadowRedundancyEnabled;
				}
				internal set
				{
					this.shadowRedundancyEnabled = value;
				}
			}

			// Token: 0x17000BBC RID: 3004
			// (get) Token: 0x060026D4 RID: 9940 RVA: 0x00096214 File Offset: 0x00094414
			public ShadowRedundancyCompatibilityVersion CompatibilityVersion
			{
				get
				{
					if (Components.Configuration.ProcessTransportRole != ProcessTransportRole.Edge)
					{
						return Components.TransportAppConfig.ShadowRedundancy.CompatibilityVersion;
					}
					return ShadowRedundancyCompatibilityVersion.E14;
				}
			}

			// Token: 0x17000BBD RID: 3005
			// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00096234 File Offset: 0x00094434
			// (set) Token: 0x060026D6 RID: 9942 RVA: 0x0009623C File Offset: 0x0009443C
			public TimeSpan ShadowMessageAutoDiscardInterval
			{
				get
				{
					return this.shadowMessageAutoDiscardInterval;
				}
				internal set
				{
					this.shadowMessageAutoDiscardInterval = value;
				}
			}

			// Token: 0x17000BBE RID: 3006
			// (get) Token: 0x060026D7 RID: 9943 RVA: 0x00096245 File Offset: 0x00094445
			public TimeSpan DiscardEventExpireInterval
			{
				get
				{
					return this.ShadowMessageAutoDiscardInterval;
				}
			}

			// Token: 0x17000BBF RID: 3007
			// (get) Token: 0x060026D8 RID: 9944 RVA: 0x0009624D File Offset: 0x0009444D
			public TimeSpan QueueMaxIdleTimeInterval
			{
				get
				{
					return Components.Configuration.LocalServer.TransportServer.QueueMaxIdleTime;
				}
			}

			// Token: 0x17000BC0 RID: 3008
			// (get) Token: 0x060026D9 RID: 9945 RVA: 0x00096268 File Offset: 0x00094468
			public TimeSpan ShadowServerInfoMaxIdleTimeInterval
			{
				get
				{
					return this.QueueMaxIdleTimeInterval;
				}
			}

			// Token: 0x17000BC1 RID: 3009
			// (get) Token: 0x060026DA RID: 9946 RVA: 0x00096270 File Offset: 0x00094470
			public TimeSpan ShadowQueueCheckExpiryInterval
			{
				get
				{
					return ShadowRedundancyConfig.ShadowRedundancyConfigData.shadowQueueCheckExpiryInterval;
				}
			}

			// Token: 0x17000BC2 RID: 3010
			// (get) Token: 0x060026DB RID: 9947 RVA: 0x00096277 File Offset: 0x00094477
			public TimeSpan DelayedAckCheckExpiryInterval
			{
				get
				{
					return ShadowRedundancyConfig.ShadowRedundancyConfigData.delayedAckCheckExpiryInterval;
				}
			}

			// Token: 0x17000BC3 RID: 3011
			// (get) Token: 0x060026DC RID: 9948 RVA: 0x0009627E File Offset: 0x0009447E
			public bool DelayedAckSkippingEnabled
			{
				get
				{
					return Components.TransportAppConfig.ShadowRedundancy.DelayedAckSkippingEnabled;
				}
			}

			// Token: 0x17000BC4 RID: 3012
			// (get) Token: 0x060026DD RID: 9949 RVA: 0x0009628F File Offset: 0x0009448F
			public int DelayedAckSkippingQueueLength
			{
				get
				{
					return Components.TransportAppConfig.ShadowRedundancy.DelayedAckSkippingQueueLength;
				}
			}

			// Token: 0x17000BC5 RID: 3013
			// (get) Token: 0x060026DE RID: 9950 RVA: 0x000962A0 File Offset: 0x000944A0
			public TimeSpan DiscardEventsCheckExpiryInterval
			{
				get
				{
					return ShadowRedundancyConfig.ShadowRedundancyConfigData.discardEventsCheckExpiryInterval;
				}
			}

			// Token: 0x17000BC6 RID: 3014
			// (get) Token: 0x060026DF RID: 9951 RVA: 0x000962A7 File Offset: 0x000944A7
			public TimeSpan StringPoolCleanupInterval
			{
				get
				{
					return ShadowRedundancyConfig.ShadowRedundancyConfigData.stringPoolCleanupInterval;
				}
			}

			// Token: 0x17000BC7 RID: 3015
			// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000962AE File Offset: 0x000944AE
			public TimeSpan PrimaryServerInfoCleanupInterval
			{
				get
				{
					return ShadowRedundancyConfig.ShadowRedundancyConfigData.primaryServerInfoCleanupInterval;
				}
			}

			// Token: 0x17000BC8 RID: 3016
			// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000962B5 File Offset: 0x000944B5
			public int PrimaryServerInfoHardCleanupThreshold
			{
				get
				{
					return 10000;
				}
			}

			// Token: 0x17000BC9 RID: 3017
			// (get) Token: 0x060026E2 RID: 9954 RVA: 0x000962BC File Offset: 0x000944BC
			// (set) Token: 0x060026E3 RID: 9955 RVA: 0x000962C4 File Offset: 0x000944C4
			public TimeSpan HeartbeatFrequency
			{
				get
				{
					return this.heartbeatFrequency;
				}
				internal set
				{
					this.heartbeatFrequency = value;
				}
			}

			// Token: 0x17000BCA RID: 3018
			// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000962CD File Offset: 0x000944CD
			// (set) Token: 0x060026E5 RID: 9957 RVA: 0x000962D5 File Offset: 0x000944D5
			public int HeartbeatRetryCount
			{
				get
				{
					return this.heartbeatRetryCount;
				}
				internal set
				{
					this.heartbeatRetryCount = value;
				}
			}

			// Token: 0x17000BCB RID: 3019
			// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000962DE File Offset: 0x000944DE
			// (set) Token: 0x060026E7 RID: 9959 RVA: 0x000962E6 File Offset: 0x000944E6
			public int MaxRemoteShadowAttempts
			{
				get
				{
					return this.maxRemoteAttempts;
				}
				internal set
				{
					this.maxRemoteAttempts = value;
				}
			}

			// Token: 0x17000BCC RID: 3020
			// (get) Token: 0x060026E8 RID: 9960 RVA: 0x000962EF File Offset: 0x000944EF
			// (set) Token: 0x060026E9 RID: 9961 RVA: 0x000962F7 File Offset: 0x000944F7
			public int MaxLocalShadowAttempts
			{
				get
				{
					return this.maxLocalAttempts;
				}
				internal set
				{
					this.maxLocalAttempts = value;
				}
			}

			// Token: 0x17000BCD RID: 3021
			// (get) Token: 0x060026EA RID: 9962 RVA: 0x00096300 File Offset: 0x00094500
			// (set) Token: 0x060026EB RID: 9963 RVA: 0x00096308 File Offset: 0x00094508
			public ShadowMessagePreference ShadowMessagePreference
			{
				get
				{
					return this.shadowMessagePreference;
				}
				internal set
				{
					this.shadowMessagePreference = value;
				}
			}

			// Token: 0x17000BCE RID: 3022
			// (get) Token: 0x060026EC RID: 9964 RVA: 0x00096311 File Offset: 0x00094511
			// (set) Token: 0x060026ED RID: 9965 RVA: 0x00096319 File Offset: 0x00094519
			public bool RejectMessageOnShadowFailure
			{
				get
				{
					return this.rejectMessageOnShadowFailure;
				}
				internal set
				{
					this.rejectMessageOnShadowFailure = value;
				}
			}

			// Token: 0x17000BCF RID: 3023
			// (get) Token: 0x060026EE RID: 9966 RVA: 0x00096322 File Offset: 0x00094522
			public int MaxDiscardIdsPerSmtpCommand
			{
				get
				{
					return Components.TransportAppConfig.ShadowRedundancy.MaxDiscardIdsPerSmtpCommand;
				}
			}

			// Token: 0x17000BD0 RID: 3024
			// (get) Token: 0x060026EF RID: 9967 RVA: 0x00096333 File Offset: 0x00094533
			public TimeSpan MaxPendingHeartbeatInterval
			{
				get
				{
					return Components.TransportAppConfig.ShadowRedundancy.MaxPendingHeartbeatInterval;
				}
			}

			// Token: 0x060026F0 RID: 9968 RVA: 0x00096344 File Offset: 0x00094544
			public void Load()
			{
				throw new NotSupportedException("ShadowRedundancyConfigData.Load() should never be called.");
			}

			// Token: 0x060026F1 RID: 9969 RVA: 0x00096350 File Offset: 0x00094550
			public void Unload()
			{
				throw new NotSupportedException("ShadowRedundancyConfigData.Unload() should never be called.");
			}

			// Token: 0x060026F2 RID: 9970 RVA: 0x0009635C File Offset: 0x0009455C
			public void SetShadowRedundancyConfigChangeNotification(ShadowRedundancyConfigChange shadowRedundancyConfigChange)
			{
				throw new NotSupportedException("ShadowRedundancyConfigData.SetShadowRedundancyConfigChangeNotification() should never be called.");
			}

			// Token: 0x060026F3 RID: 9971 RVA: 0x00096368 File Offset: 0x00094568
			public bool Equals(ShadowRedundancyConfig.ShadowRedundancyConfigData shadowRedundancyConfigData)
			{
				return shadowRedundancyConfigData != null && this.heartbeatRetryCount == shadowRedundancyConfigData.heartbeatRetryCount && this.heartbeatFrequency == shadowRedundancyConfigData.heartbeatFrequency && this.shadowMessageAutoDiscardInterval == shadowRedundancyConfigData.shadowMessageAutoDiscardInterval && this.maxRemoteAttempts == shadowRedundancyConfigData.maxRemoteAttempts && this.maxLocalAttempts == shadowRedundancyConfigData.maxLocalAttempts && this.rejectMessageOnShadowFailure == shadowRedundancyConfigData.rejectMessageOnShadowFailure && this.shadowRedundancyEnabled == shadowRedundancyConfigData.shadowRedundancyEnabled;
			}

			// Token: 0x040013C7 RID: 5063
			private const int DefaultPrimaryServerInfoHardCleanupThreshold = 10000;

			// Token: 0x040013C8 RID: 5064
			private static readonly TimeSpan shadowQueueCheckExpiryInterval = TimeSpan.FromMinutes(1.0);

			// Token: 0x040013C9 RID: 5065
			private static readonly TimeSpan delayedAckCheckExpiryInterval = TimeSpan.FromSeconds(5.0);

			// Token: 0x040013CA RID: 5066
			private static readonly TimeSpan discardEventsCheckExpiryInterval = TimeSpan.FromMinutes(5.0);

			// Token: 0x040013CB RID: 5067
			private static readonly TimeSpan stringPoolCleanupInterval = TimeSpan.FromHours(3.0);

			// Token: 0x040013CC RID: 5068
			private static readonly TimeSpan primaryServerInfoCleanupInterval = TimeSpan.FromMinutes(5.0);

			// Token: 0x040013CD RID: 5069
			private bool shadowRedundancyEnabled;

			// Token: 0x040013CE RID: 5070
			private TimeSpan shadowMessageAutoDiscardInterval;

			// Token: 0x040013CF RID: 5071
			private TimeSpan heartbeatFrequency;

			// Token: 0x040013D0 RID: 5072
			private int heartbeatRetryCount;

			// Token: 0x040013D1 RID: 5073
			private int maxRemoteAttempts;

			// Token: 0x040013D2 RID: 5074
			private int maxLocalAttempts;

			// Token: 0x040013D3 RID: 5075
			private ShadowMessagePreference shadowMessagePreference;

			// Token: 0x040013D4 RID: 5076
			private bool rejectMessageOnShadowFailure;
		}
	}
}
