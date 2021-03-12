using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000715 RID: 1813
	[Serializable]
	public sealed class FrontendTransportServerPresentationObject : ADPresentationObject
	{
		// Token: 0x0600554E RID: 21838 RVA: 0x001351C7 File Offset: 0x001333C7
		public FrontendTransportServerPresentationObject()
		{
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x001351CF File Offset: 0x001333CF
		public FrontendTransportServerPresentationObject(FrontendTransportServer dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x06005550 RID: 21840 RVA: 0x001351D8 File Offset: 0x001333D8
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return FrontendTransportServerPresentationObject.schema;
			}
		}

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x06005551 RID: 21841 RVA: 0x001351DF File Offset: 0x001333DF
		public new string Name
		{
			get
			{
				return ((ADObjectId)this[ADObjectSchema.Id]).Parent.Parent.Name;
			}
		}

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x00135200 File Offset: 0x00133400
		// (set) Token: 0x06005553 RID: 21843 RVA: 0x00135212 File Offset: 0x00133412
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[FrontendTransportServerSchema.AdminDisplayVersion];
			}
			internal set
			{
				this[FrontendTransportServerSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06005554 RID: 21844 RVA: 0x00135220 File Offset: 0x00133420
		// (set) Token: 0x06005555 RID: 21845 RVA: 0x00135232 File Offset: 0x00133432
		public ServerEditionType Edition
		{
			get
			{
				return (ServerEditionType)this[FrontendTransportServerSchema.Edition];
			}
			internal set
			{
				this[FrontendTransportServerSchema.Edition] = value;
			}
		}

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x06005556 RID: 21846 RVA: 0x00135245 File Offset: 0x00133445
		// (set) Token: 0x06005557 RID: 21847 RVA: 0x00135257 File Offset: 0x00133457
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[FrontendTransportServerSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[FrontendTransportServerSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x06005558 RID: 21848 RVA: 0x00135265 File Offset: 0x00133465
		// (set) Token: 0x06005559 RID: 21849 RVA: 0x00135277 File Offset: 0x00133477
		public bool IsFrontendTransportServer
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.IsFrontendTransportServer];
			}
			internal set
			{
				this[FrontendTransportServerSchema.IsFrontendTransportServer] = value;
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x0600555A RID: 21850 RVA: 0x0013528A File Offset: 0x0013348A
		// (set) Token: 0x0600555B RID: 21851 RVA: 0x0013529C File Offset: 0x0013349C
		public bool IsProvisionedServer
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.IsProvisionedServer];
			}
			internal set
			{
				this[FrontendTransportServerSchema.IsProvisionedServer] = value;
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x0600555C RID: 21852 RVA: 0x001352AF File Offset: 0x001334AF
		// (set) Token: 0x0600555D RID: 21853 RVA: 0x001352C1 File Offset: 0x001334C1
		public NetworkAddressCollection NetworkAddress
		{
			get
			{
				return (NetworkAddressCollection)this[FrontendTransportServerSchema.NetworkAddress];
			}
			internal set
			{
				this[FrontendTransportServerSchema.NetworkAddress] = value;
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x0600555E RID: 21854 RVA: 0x001352CF File Offset: 0x001334CF
		// (set) Token: 0x0600555F RID: 21855 RVA: 0x001352E1 File Offset: 0x001334E1
		public int VersionNumber
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.VersionNumber];
			}
			internal set
			{
				this[FrontendTransportServerSchema.VersionNumber] = value;
			}
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x06005560 RID: 21856 RVA: 0x001352F4 File Offset: 0x001334F4
		// (set) Token: 0x06005561 RID: 21857 RVA: 0x00135306 File Offset: 0x00133506
		[Parameter(Mandatory = false)]
		public bool AntispamAgentsEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.AntispamAgentsEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.AntispamAgentsEnabled] = value;
			}
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x06005562 RID: 21858 RVA: 0x00135319 File Offset: 0x00133519
		// (set) Token: 0x06005563 RID: 21859 RVA: 0x0013532B File Offset: 0x0013352B
		[Parameter(Mandatory = false)]
		public bool ConnectivityLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.ConnectivityLogEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogEnabled] = value;
			}
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x06005564 RID: 21860 RVA: 0x0013533E File Offset: 0x0013353E
		// (set) Token: 0x06005565 RID: 21861 RVA: 0x00135350 File Offset: 0x00133550
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectivityLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.ConnectivityLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogMaxAge] = value;
			}
		}

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x06005566 RID: 21862 RVA: 0x00135363 File Offset: 0x00133563
		// (set) Token: 0x06005567 RID: 21863 RVA: 0x00135375 File Offset: 0x00133575
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ConnectivityLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x06005568 RID: 21864 RVA: 0x00135388 File Offset: 0x00133588
		// (set) Token: 0x06005569 RID: 21865 RVA: 0x0013539A File Offset: 0x0013359A
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ConnectivityLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x001353AD File Offset: 0x001335AD
		// (set) Token: 0x0600556B RID: 21867 RVA: 0x001353BF File Offset: 0x001335BF
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ConnectivityLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.ConnectivityLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogPath] = value;
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x0600556C RID: 21868 RVA: 0x001353CD File Offset: 0x001335CD
		// (set) Token: 0x0600556D RID: 21869 RVA: 0x001353E2 File Offset: 0x001335E2
		[Parameter(Mandatory = false)]
		public bool ExternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[FrontendTransportServerSchema.ExternalDNSAdapterDisabled];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x0600556E RID: 21870 RVA: 0x001353F8 File Offset: 0x001335F8
		// (set) Token: 0x0600556F RID: 21871 RVA: 0x0013540A File Offset: 0x0013360A
		[Parameter(Mandatory = false)]
		public Guid ExternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[FrontendTransportServerSchema.ExternalDNSAdapterGuid];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0013541D File Offset: 0x0013361D
		// (set) Token: 0x06005571 RID: 21873 RVA: 0x0013542F File Offset: 0x0013362F
		[Parameter(Mandatory = false)]
		public ProtocolOption ExternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[FrontendTransportServerSchema.ExternalDNSProtocolOption];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06005572 RID: 21874 RVA: 0x00135442 File Offset: 0x00133642
		// (set) Token: 0x06005573 RID: 21875 RVA: 0x00135454 File Offset: 0x00133654
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> ExternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[FrontendTransportServerSchema.ExternalDNSServers];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSServers] = value;
			}
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x00135462 File Offset: 0x00133662
		// (set) Token: 0x06005575 RID: 21877 RVA: 0x00135474 File Offset: 0x00133674
		[Parameter(Mandatory = false)]
		public IPAddress ExternalIPAddress
		{
			get
			{
				return (IPAddress)this[FrontendTransportServerSchema.ExternalIPAddress];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalIPAddress] = value;
			}
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x00135482 File Offset: 0x00133682
		// (set) Token: 0x06005577 RID: 21879 RVA: 0x00135497 File Offset: 0x00133697
		[Parameter(Mandatory = false)]
		public bool InternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[FrontendTransportServerSchema.InternalDNSAdapterDisabled];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x001354AD File Offset: 0x001336AD
		// (set) Token: 0x06005579 RID: 21881 RVA: 0x001354BF File Offset: 0x001336BF
		[Parameter(Mandatory = false)]
		public Guid InternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[FrontendTransportServerSchema.InternalDNSAdapterGuid];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x0600557A RID: 21882 RVA: 0x001354D2 File Offset: 0x001336D2
		// (set) Token: 0x0600557B RID: 21883 RVA: 0x001354E4 File Offset: 0x001336E4
		[Parameter(Mandatory = false)]
		public ProtocolOption InternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[FrontendTransportServerSchema.InternalDNSProtocolOption];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x0600557C RID: 21884 RVA: 0x001354F7 File Offset: 0x001336F7
		// (set) Token: 0x0600557D RID: 21885 RVA: 0x00135509 File Offset: 0x00133709
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> InternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[FrontendTransportServerSchema.InternalDNSServers];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSServers] = value;
			}
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x0600557E RID: 21886 RVA: 0x00135517 File Offset: 0x00133717
		// (set) Token: 0x0600557F RID: 21887 RVA: 0x00135529 File Offset: 0x00133729
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[FrontendTransportServerSchema.IntraOrgConnectorProtocolLoggingLevel];
			}
			set
			{
				this[FrontendTransportServerSchema.IntraOrgConnectorProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06005580 RID: 21888 RVA: 0x0013553C File Offset: 0x0013373C
		// (set) Token: 0x06005581 RID: 21889 RVA: 0x0013554E File Offset: 0x0013374E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReceiveProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.ReceiveProtocolLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06005582 RID: 21890 RVA: 0x00135561 File Offset: 0x00133761
		// (set) Token: 0x06005583 RID: 21891 RVA: 0x00135573 File Offset: 0x00133773
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ReceiveProtocolLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x06005584 RID: 21892 RVA: 0x00135586 File Offset: 0x00133786
		// (set) Token: 0x06005585 RID: 21893 RVA: 0x00135598 File Offset: 0x00133798
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ReceiveProtocolLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x06005586 RID: 21894 RVA: 0x001355AB File Offset: 0x001337AB
		// (set) Token: 0x06005587 RID: 21895 RVA: 0x001355BD File Offset: 0x001337BD
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReceiveProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.ReceiveProtocolLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogPath] = value;
			}
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06005588 RID: 21896 RVA: 0x001355CB File Offset: 0x001337CB
		// (set) Token: 0x06005589 RID: 21897 RVA: 0x001355DD File Offset: 0x001337DD
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SendProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.SendProtocolLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x0600558A RID: 21898 RVA: 0x001355F0 File Offset: 0x001337F0
		// (set) Token: 0x0600558B RID: 21899 RVA: 0x00135602 File Offset: 0x00133802
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.SendProtocolLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x0600558C RID: 21900 RVA: 0x00135615 File Offset: 0x00133815
		// (set) Token: 0x0600558D RID: 21901 RVA: 0x00135627 File Offset: 0x00133827
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.SendProtocolLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x0600558E RID: 21902 RVA: 0x0013563A File Offset: 0x0013383A
		// (set) Token: 0x0600558F RID: 21903 RVA: 0x0013564C File Offset: 0x0013384C
		[Parameter(Mandatory = false)]
		public LocalLongFullPath SendProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.SendProtocolLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogPath] = value;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06005590 RID: 21904 RVA: 0x0013565A File Offset: 0x0013385A
		// (set) Token: 0x06005591 RID: 21905 RVA: 0x0013566C File Offset: 0x0013386C
		[Parameter(Mandatory = false)]
		public int TransientFailureRetryCount
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.TransientFailureRetryCount];
			}
			set
			{
				this[FrontendTransportServerSchema.TransientFailureRetryCount] = value;
			}
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06005592 RID: 21906 RVA: 0x0013567F File Offset: 0x0013387F
		// (set) Token: 0x06005593 RID: 21907 RVA: 0x00135691 File Offset: 0x00133891
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransientFailureRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.TransientFailureRetryInterval];
			}
			set
			{
				this[FrontendTransportServerSchema.TransientFailureRetryInterval] = value;
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x06005594 RID: 21908 RVA: 0x001356A4 File Offset: 0x001338A4
		// (set) Token: 0x06005595 RID: 21909 RVA: 0x001356B6 File Offset: 0x001338B6
		[Parameter(Mandatory = false)]
		public int MaxConnectionRatePerMinute
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.MaxConnectionRatePerMinute];
			}
			set
			{
				this[FrontendTransportServerSchema.MaxConnectionRatePerMinute] = value;
			}
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06005596 RID: 21910 RVA: 0x001356C9 File Offset: 0x001338C9
		public EnhancedTimeSpan AgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.AgentLogMaxAge];
			}
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x06005597 RID: 21911 RVA: 0x001356DB File Offset: 0x001338DB
		public Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AgentLogMaxDirectorySize];
			}
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06005598 RID: 21912 RVA: 0x001356ED File Offset: 0x001338ED
		public Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AgentLogMaxFileSize];
			}
		}

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06005599 RID: 21913 RVA: 0x001356FF File Offset: 0x001338FF
		public LocalLongFullPath AgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.AgentLogPath];
			}
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x00135711 File Offset: 0x00133911
		public bool AgentLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.AgentLogEnabled];
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x0600559B RID: 21915 RVA: 0x00135723 File Offset: 0x00133923
		public EnhancedTimeSpan DnsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.DnsLogMaxAge];
			}
		}

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x0600559C RID: 21916 RVA: 0x00135735 File Offset: 0x00133935
		public Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.DnsLogMaxDirectorySize];
			}
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x0600559D RID: 21917 RVA: 0x00135747 File Offset: 0x00133947
		public Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.DnsLogMaxFileSize];
			}
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x0600559E RID: 21918 RVA: 0x00135759 File Offset: 0x00133959
		public LocalLongFullPath DnsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.DnsLogPath];
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x0600559F RID: 21919 RVA: 0x0013576B File Offset: 0x0013396B
		public bool DnsLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.DnsLogEnabled];
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x0013577D File Offset: 0x0013397D
		public EnhancedTimeSpan AttributionLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.AttributionLogMaxAge];
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x060055A1 RID: 21921 RVA: 0x0013578F File Offset: 0x0013398F
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AttributionLogMaxDirectorySize];
			}
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x060055A2 RID: 21922 RVA: 0x001357A1 File Offset: 0x001339A1
		public EnhancedTimeSpan ResourceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.ResourceLogMaxAge];
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x001357B3 File Offset: 0x001339B3
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ResourceLogMaxDirectorySize];
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x060055A4 RID: 21924 RVA: 0x001357C5 File Offset: 0x001339C5
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ResourceLogMaxFileSize];
			}
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x060055A5 RID: 21925 RVA: 0x001357D7 File Offset: 0x001339D7
		public LocalLongFullPath ResourceLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.ResourceLogPath];
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x060055A6 RID: 21926 RVA: 0x001357E9 File Offset: 0x001339E9
		public bool ResourceLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.ResourceLogEnabled];
			}
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x060055A7 RID: 21927 RVA: 0x001357FB File Offset: 0x001339FB
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AttributionLogMaxFileSize];
			}
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x060055A8 RID: 21928 RVA: 0x0013580D File Offset: 0x00133A0D
		public LocalLongFullPath AttributionLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.AttributionLogPath];
			}
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x060055A9 RID: 21929 RVA: 0x0013581F File Offset: 0x00133A1F
		public bool AttributionLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.AttributionLogEnabled];
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x060055AA RID: 21930 RVA: 0x00135831 File Offset: 0x00133A31
		public ServerRole ServerRole
		{
			get
			{
				return (ServerRole)this[FrontendTransportServerSchema.CurrentServerRole];
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x00135843 File Offset: 0x00133A43
		public int MaxReceiveTlsRatePerMinute
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.MaxReceiveTlsRatePerMinute];
			}
		}

		// Token: 0x040039C3 RID: 14787
		private static FrontendTransportServerSchema schema = ObjectSchema.GetInstance<FrontendTransportServerSchema>();
	}
}
