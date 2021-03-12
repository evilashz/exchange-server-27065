using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000481 RID: 1153
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class MailboxTransportServer : ADLegacyVersionableObject
	{
		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000D062B File Offset: 0x000CE82B
		// (set) Token: 0x06003417 RID: 13335 RVA: 0x000D063D File Offset: 0x000CE83D
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[MailboxTransportServerSchema.AdminDisplayVersion];
			}
			internal set
			{
				this[MailboxTransportServerSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000D064B File Offset: 0x000CE84B
		// (set) Token: 0x06003419 RID: 13337 RVA: 0x000D065D File Offset: 0x000CE85D
		public ServerEditionType Edition
		{
			get
			{
				return (ServerEditionType)this[MailboxTransportServerSchema.Edition];
			}
			internal set
			{
				this[MailboxTransportServerSchema.Edition] = value;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000D0670 File Offset: 0x000CE870
		// (set) Token: 0x0600341B RID: 13339 RVA: 0x000D0682 File Offset: 0x000CE882
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[MailboxTransportServerSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[MailboxTransportServerSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000D0690 File Offset: 0x000CE890
		// (set) Token: 0x0600341D RID: 13341 RVA: 0x000D06A2 File Offset: 0x000CE8A2
		public bool IsMailboxServer
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.IsMailboxServer];
			}
			internal set
			{
				this[MailboxTransportServerSchema.IsMailboxServer] = value;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000D06B5 File Offset: 0x000CE8B5
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x000D06C7 File Offset: 0x000CE8C7
		public bool IsProvisionedServer
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.IsProvisionedServer];
			}
			internal set
			{
				this[MailboxTransportServerSchema.IsProvisionedServer] = value;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x000D06DA File Offset: 0x000CE8DA
		// (set) Token: 0x06003421 RID: 13345 RVA: 0x000D06EC File Offset: 0x000CE8EC
		public NetworkAddressCollection NetworkAddress
		{
			get
			{
				return (NetworkAddressCollection)this[MailboxTransportServerSchema.NetworkAddress];
			}
			internal set
			{
				this[MailboxTransportServerSchema.NetworkAddress] = value;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000D06FA File Offset: 0x000CE8FA
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x000D070C File Offset: 0x000CE90C
		public int VersionNumber
		{
			get
			{
				return (int)this[MailboxTransportServerSchema.VersionNumber];
			}
			internal set
			{
				this[MailboxTransportServerSchema.VersionNumber] = value;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000D071F File Offset: 0x000CE91F
		// (set) Token: 0x06003425 RID: 13349 RVA: 0x000D0731 File Offset: 0x000CE931
		[Parameter(Mandatory = false)]
		public bool ConnectivityLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.ConnectivityLogEnabled];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogEnabled] = value;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000D0744 File Offset: 0x000CE944
		// (set) Token: 0x06003427 RID: 13351 RVA: 0x000D0756 File Offset: 0x000CE956
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectivityLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.ConnectivityLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogMaxAge] = value;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000D0769 File Offset: 0x000CE969
		// (set) Token: 0x06003429 RID: 13353 RVA: 0x000D077B File Offset: 0x000CE97B
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ConnectivityLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000D078E File Offset: 0x000CE98E
		// (set) Token: 0x0600342B RID: 13355 RVA: 0x000D07A0 File Offset: 0x000CE9A0
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ConnectivityLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000D07B3 File Offset: 0x000CE9B3
		// (set) Token: 0x0600342D RID: 13357 RVA: 0x000D07C5 File Offset: 0x000CE9C5
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ConnectivityLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.ConnectivityLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogPath] = value;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000D07D3 File Offset: 0x000CE9D3
		// (set) Token: 0x0600342F RID: 13359 RVA: 0x000D07E5 File Offset: 0x000CE9E5
		public bool ContentConversionTracingEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.ContentConversionTracingEnabled];
			}
			internal set
			{
				this[MailboxTransportServerSchema.ContentConversionTracingEnabled] = value;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x000D07F8 File Offset: 0x000CE9F8
		// (set) Token: 0x06003431 RID: 13361 RVA: 0x000D080A File Offset: 0x000CEA0A
		public ServerRole CurrentServerRole
		{
			get
			{
				return (ServerRole)this[MailboxTransportServerSchema.CurrentServerRole];
			}
			internal set
			{
				this[MailboxTransportServerSchema.CurrentServerRole] = value;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000D081D File Offset: 0x000CEA1D
		// (set) Token: 0x06003433 RID: 13363 RVA: 0x000D082F File Offset: 0x000CEA2F
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel InMemoryReceiveConnectorProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[MailboxTransportServerSchema.InMemoryReceiveConnectorProtocolLoggingLevel];
			}
			set
			{
				this[MailboxTransportServerSchema.InMemoryReceiveConnectorProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000D0842 File Offset: 0x000CEA42
		// (set) Token: 0x06003435 RID: 13365 RVA: 0x000D0854 File Offset: 0x000CEA54
		[Parameter(Mandatory = false)]
		public bool InMemoryReceiveConnectorSmtpUtf8Enabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.InMemoryReceiveConnectorSmtpUtf8Enabled];
			}
			set
			{
				this[MailboxTransportServerSchema.InMemoryReceiveConnectorSmtpUtf8Enabled] = value;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000D0867 File Offset: 0x000CEA67
		// (set) Token: 0x06003437 RID: 13367 RVA: 0x000D0879 File Offset: 0x000CEA79
		public EnhancedTimeSpan MailboxDeliveryAgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxAge] = value;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000D088C File Offset: 0x000CEA8C
		// (set) Token: 0x06003439 RID: 13369 RVA: 0x000D089E File Offset: 0x000CEA9E
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000D08B1 File Offset: 0x000CEAB1
		// (set) Token: 0x0600343B RID: 13371 RVA: 0x000D08C3 File Offset: 0x000CEAC3
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000D08D6 File Offset: 0x000CEAD6
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x000D08E8 File Offset: 0x000CEAE8
		public LocalLongFullPath MailboxDeliveryAgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryAgentLogPath] = value;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000D08F6 File Offset: 0x000CEAF6
		// (set) Token: 0x0600343F RID: 13375 RVA: 0x000D0908 File Offset: 0x000CEB08
		public bool MailboxDeliveryAgentLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogEnabled];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryAgentLogEnabled] = value;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000D091B File Offset: 0x000CEB1B
		// (set) Token: 0x06003441 RID: 13377 RVA: 0x000D092D File Offset: 0x000CEB2D
		public EnhancedTimeSpan MailboxSubmissionAgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxAge] = value;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000D0940 File Offset: 0x000CEB40
		// (set) Token: 0x06003443 RID: 13379 RVA: 0x000D0952 File Offset: 0x000CEB52
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x000D0965 File Offset: 0x000CEB65
		// (set) Token: 0x06003445 RID: 13381 RVA: 0x000D0977 File Offset: 0x000CEB77
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003446 RID: 13382 RVA: 0x000D098A File Offset: 0x000CEB8A
		// (set) Token: 0x06003447 RID: 13383 RVA: 0x000D099C File Offset: 0x000CEB9C
		public LocalLongFullPath MailboxSubmissionAgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxSubmissionAgentLogPath] = value;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000D09AA File Offset: 0x000CEBAA
		// (set) Token: 0x06003449 RID: 13385 RVA: 0x000D09BC File Offset: 0x000CEBBC
		public bool MailboxSubmissionAgentLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogEnabled];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxSubmissionAgentLogEnabled] = value;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000D09CF File Offset: 0x000CEBCF
		// (set) Token: 0x0600344B RID: 13387 RVA: 0x000D09E1 File Offset: 0x000CEBE1
		public bool MailboxDeliveryThrottlingLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogEnabled];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogEnabled] = value;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000D09F4 File Offset: 0x000CEBF4
		// (set) Token: 0x0600344D RID: 13389 RVA: 0x000D0A06 File Offset: 0x000CEC06
		public EnhancedTimeSpan MailboxDeliveryThrottlingLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxAge] = value;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000D0A19 File Offset: 0x000CEC19
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x000D0A2B File Offset: 0x000CEC2B
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000D0A3E File Offset: 0x000CEC3E
		// (set) Token: 0x06003451 RID: 13393 RVA: 0x000D0A50 File Offset: 0x000CEC50
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x000D0A63 File Offset: 0x000CEC63
		// (set) Token: 0x06003453 RID: 13395 RVA: 0x000D0A75 File Offset: 0x000CEC75
		public LocalLongFullPath MailboxDeliveryThrottlingLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogPath] = value;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000D0A83 File Offset: 0x000CEC83
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x000D0A95 File Offset: 0x000CEC95
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxDeliveries
		{
			get
			{
				return (int)this[MailboxTransportServerSchema.MaxConcurrentMailboxDeliveries];
			}
			set
			{
				this[MailboxTransportServerSchema.MaxConcurrentMailboxDeliveries] = value;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000D0AA8 File Offset: 0x000CECA8
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x000D0ABA File Offset: 0x000CECBA
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxSubmissions
		{
			get
			{
				return (int)this[MailboxTransportServerSchema.MaxConcurrentMailboxSubmissions];
			}
			set
			{
				this[MailboxTransportServerSchema.MaxConcurrentMailboxSubmissions] = value;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000D0ACD File Offset: 0x000CECCD
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x000D0ADF File Offset: 0x000CECDF
		public bool PipelineTracingEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.PipelineTracingEnabled];
			}
			internal set
			{
				this[MailboxTransportServerSchema.PipelineTracingEnabled] = value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000D0AF2 File Offset: 0x000CECF2
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x000D0B04 File Offset: 0x000CED04
		public LocalLongFullPath PipelineTracingPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.PipelineTracingPath];
			}
			internal set
			{
				this[MailboxTransportServerSchema.PipelineTracingPath] = value;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000D0B12 File Offset: 0x000CED12
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x000D0B24 File Offset: 0x000CED24
		public SmtpAddress? PipelineTracingSenderAddress
		{
			get
			{
				return (SmtpAddress?)this[MailboxTransportServerSchema.PipelineTracingSenderAddress];
			}
			internal set
			{
				this[MailboxTransportServerSchema.PipelineTracingSenderAddress] = value;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000D0B37 File Offset: 0x000CED37
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x000D0B49 File Offset: 0x000CED49
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReceiveProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.ReceiveProtocolLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000D0B5C File Offset: 0x000CED5C
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x000D0B6E File Offset: 0x000CED6E
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ReceiveProtocolLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000D0B81 File Offset: 0x000CED81
		// (set) Token: 0x06003463 RID: 13411 RVA: 0x000D0B93 File Offset: 0x000CED93
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ReceiveProtocolLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000D0BA6 File Offset: 0x000CEDA6
		// (set) Token: 0x06003465 RID: 13413 RVA: 0x000D0BB8 File Offset: 0x000CEDB8
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReceiveProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.ReceiveProtocolLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogPath] = value;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000D0BC6 File Offset: 0x000CEDC6
		// (set) Token: 0x06003467 RID: 13415 RVA: 0x000D0BD8 File Offset: 0x000CEDD8
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SendProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.SendProtocolLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000D0BEB File Offset: 0x000CEDEB
		// (set) Token: 0x06003469 RID: 13417 RVA: 0x000D0BFD File Offset: 0x000CEDFD
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.SendProtocolLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000D0C10 File Offset: 0x000CEE10
		// (set) Token: 0x0600346B RID: 13419 RVA: 0x000D0C22 File Offset: 0x000CEE22
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.SendProtocolLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000D0C35 File Offset: 0x000CEE35
		// (set) Token: 0x0600346D RID: 13421 RVA: 0x000D0C47 File Offset: 0x000CEE47
		[Parameter(Mandatory = false)]
		public LocalLongFullPath SendProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.SendProtocolLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogPath] = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000D0C55 File Offset: 0x000CEE55
		internal override ADObjectSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					this.schema = ObjectSchema.GetInstance<MailboxTransportServerADSchema>();
				}
				return this.schema;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000D0C70 File Offset: 0x000CEE70
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MailboxTransportServer.mostDerivedClass;
			}
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000D0C77 File Offset: 0x000CEE77
		internal Container GetParentContainer()
		{
			return base.Session.Read<Container>(base.Id.Parent);
		}

		// Token: 0x040023C6 RID: 9158
		internal const string MailboxTransportServerADObjectName = "Mailbox";

		// Token: 0x040023C7 RID: 9159
		private static string mostDerivedClass = "msExchExchangeTransportServer";

		// Token: 0x040023C8 RID: 9160
		[NonSerialized]
		private ADObjectSchema schema;
	}
}
