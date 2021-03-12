using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000779 RID: 1913
	[Serializable]
	public sealed class TransportServer : ADPresentationObject
	{
		// Token: 0x06005E09 RID: 24073 RVA: 0x00143D65 File Offset: 0x00141F65
		public TransportServer()
		{
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x00143D6D File Offset: 0x00141F6D
		public TransportServer(Server dataObject) : base(dataObject)
		{
		}

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x00143D76 File Offset: 0x00141F76
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				if (TopologyProvider.IsAdamTopology())
				{
					return TransportServer.schema;
				}
				return TransportServer.adSchema;
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x06005E0C RID: 24076 RVA: 0x00143D8A File Offset: 0x00141F8A
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x00143D9C File Offset: 0x00141F9C
		// (set) Token: 0x06005E0E RID: 24078 RVA: 0x00143DAE File Offset: 0x00141FAE
		[Parameter(Mandatory = false)]
		public bool AntispamAgentsEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.AntispamAgentsEnabled];
			}
			set
			{
				this[TransportServerSchema.AntispamAgentsEnabled] = value;
			}
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x00143DC1 File Offset: 0x00141FC1
		// (set) Token: 0x06005E10 RID: 24080 RVA: 0x00143DD3 File Offset: 0x00141FD3
		[Parameter(Mandatory = false)]
		public bool ConnectivityLogEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.ConnectivityLogEnabled];
			}
			set
			{
				this[TransportServerSchema.ConnectivityLogEnabled] = value;
			}
		}

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x00143DE6 File Offset: 0x00141FE6
		// (set) Token: 0x06005E12 RID: 24082 RVA: 0x00143DF8 File Offset: 0x00141FF8
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectivityLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.ConnectivityLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.ConnectivityLogMaxAge] = value;
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x00143E0B File Offset: 0x0014200B
		// (set) Token: 0x06005E14 RID: 24084 RVA: 0x00143E1D File Offset: 0x0014201D
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.ConnectivityLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.ConnectivityLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x00143E30 File Offset: 0x00142030
		// (set) Token: 0x06005E16 RID: 24086 RVA: 0x00143E42 File Offset: 0x00142042
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.ConnectivityLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.ConnectivityLogMaxFileSize] = value;
			}
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x00143E55 File Offset: 0x00142055
		// (set) Token: 0x06005E18 RID: 24088 RVA: 0x00143E67 File Offset: 0x00142067
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ConnectivityLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.ConnectivityLogPath];
			}
			set
			{
				this[TransportServerSchema.ConnectivityLogPath] = value;
			}
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x06005E19 RID: 24089 RVA: 0x00143E75 File Offset: 0x00142075
		// (set) Token: 0x06005E1A RID: 24090 RVA: 0x00143E87 File Offset: 0x00142087
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan DelayNotificationTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.DelayNotificationTimeout];
			}
			set
			{
				this[TransportServerSchema.DelayNotificationTimeout] = value;
			}
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x06005E1B RID: 24091 RVA: 0x00143E9A File Offset: 0x0014209A
		// (set) Token: 0x06005E1C RID: 24092 RVA: 0x00143EAF File Offset: 0x001420AF
		[Parameter(Mandatory = false)]
		public bool ExternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[TransportServerSchema.ExternalDNSAdapterDisabled];
			}
			set
			{
				this[TransportServerSchema.ExternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x00143EC5 File Offset: 0x001420C5
		// (set) Token: 0x06005E1E RID: 24094 RVA: 0x00143ED7 File Offset: 0x001420D7
		[Parameter(Mandatory = false)]
		public Guid ExternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[TransportServerSchema.ExternalDNSAdapterGuid];
			}
			set
			{
				this[TransportServerSchema.ExternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x00143EEA File Offset: 0x001420EA
		// (set) Token: 0x06005E20 RID: 24096 RVA: 0x00143EFC File Offset: 0x001420FC
		[Parameter(Mandatory = false)]
		public ProtocolOption ExternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[TransportServerSchema.ExternalDNSProtocolOption];
			}
			set
			{
				this[TransportServerSchema.ExternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x00143F0F File Offset: 0x0014210F
		// (set) Token: 0x06005E22 RID: 24098 RVA: 0x00143F21 File Offset: 0x00142121
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> ExternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[TransportServerSchema.ExternalDNSServers];
			}
			set
			{
				this[TransportServerSchema.ExternalDNSServers] = value;
			}
		}

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06005E23 RID: 24099 RVA: 0x00143F2F File Offset: 0x0014212F
		// (set) Token: 0x06005E24 RID: 24100 RVA: 0x00143F41 File Offset: 0x00142141
		[Parameter(Mandatory = false)]
		public IPAddress ExternalIPAddress
		{
			get
			{
				return (IPAddress)this[TransportServerSchema.ExternalIPAddress];
			}
			set
			{
				this[TransportServerSchema.ExternalIPAddress] = value;
			}
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x00143F4F File Offset: 0x0014214F
		// (set) Token: 0x06005E26 RID: 24102 RVA: 0x00143F64 File Offset: 0x00142164
		[Parameter(Mandatory = false)]
		public bool InternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[TransportServerSchema.InternalDNSAdapterDisabled];
			}
			set
			{
				this[TransportServerSchema.InternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x00143F7A File Offset: 0x0014217A
		// (set) Token: 0x06005E28 RID: 24104 RVA: 0x00143F8C File Offset: 0x0014218C
		[Parameter(Mandatory = false)]
		public Guid InternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[TransportServerSchema.InternalDNSAdapterGuid];
			}
			set
			{
				this[TransportServerSchema.InternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x00143F9F File Offset: 0x0014219F
		// (set) Token: 0x06005E2A RID: 24106 RVA: 0x00143FB1 File Offset: 0x001421B1
		[Parameter(Mandatory = false)]
		public ProtocolOption InternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[TransportServerSchema.InternalDNSProtocolOption];
			}
			set
			{
				this[TransportServerSchema.InternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x00143FC4 File Offset: 0x001421C4
		// (set) Token: 0x06005E2C RID: 24108 RVA: 0x00143FD6 File Offset: 0x001421D6
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> InternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[TransportServerSchema.InternalDNSServers];
			}
			set
			{
				this[TransportServerSchema.InternalDNSServers] = value;
			}
		}

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x00143FE4 File Offset: 0x001421E4
		// (set) Token: 0x06005E2E RID: 24110 RVA: 0x00143FF6 File Offset: 0x001421F6
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxDeliveries
		{
			get
			{
				return (int)this[TransportServerSchema.MaxConcurrentMailboxDeliveries];
			}
			set
			{
				this[TransportServerSchema.MaxConcurrentMailboxDeliveries] = value;
			}
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x00144009 File Offset: 0x00142209
		// (set) Token: 0x06005E30 RID: 24112 RVA: 0x0014401B File Offset: 0x0014221B
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxSubmissions
		{
			get
			{
				return (int)this[ADTransportServerSchema.MaxConcurrentMailboxSubmissions];
			}
			set
			{
				this[ADTransportServerSchema.MaxConcurrentMailboxSubmissions] = value;
			}
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x0014402E File Offset: 0x0014222E
		// (set) Token: 0x06005E32 RID: 24114 RVA: 0x00144040 File Offset: 0x00142240
		[Parameter(Mandatory = false)]
		public int MaxConnectionRatePerMinute
		{
			get
			{
				return (int)this[TransportServerSchema.MaxConnectionRatePerMinute];
			}
			set
			{
				this[TransportServerSchema.MaxConnectionRatePerMinute] = value;
			}
		}

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x00144053 File Offset: 0x00142253
		// (set) Token: 0x06005E34 RID: 24116 RVA: 0x00144065 File Offset: 0x00142265
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxOutboundConnections
		{
			get
			{
				return (Unlimited<int>)this[TransportServerSchema.MaxOutboundConnections];
			}
			set
			{
				this[TransportServerSchema.MaxOutboundConnections] = value;
			}
		}

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x00144078 File Offset: 0x00142278
		// (set) Token: 0x06005E36 RID: 24118 RVA: 0x0014408A File Offset: 0x0014228A
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxPerDomainOutboundConnections
		{
			get
			{
				return (Unlimited<int>)this[TransportServerSchema.MaxPerDomainOutboundConnections];
			}
			set
			{
				this[TransportServerSchema.MaxPerDomainOutboundConnections] = value;
			}
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x0014409D File Offset: 0x0014229D
		// (set) Token: 0x06005E38 RID: 24120 RVA: 0x001440AF File Offset: 0x001422AF
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MessageExpirationTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.MessageExpirationTimeout];
			}
			set
			{
				this[TransportServerSchema.MessageExpirationTimeout] = value;
			}
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06005E39 RID: 24121 RVA: 0x001440C2 File Offset: 0x001422C2
		// (set) Token: 0x06005E3A RID: 24122 RVA: 0x001440D4 File Offset: 0x001422D4
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MessageRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.MessageRetryInterval];
			}
			set
			{
				this[TransportServerSchema.MessageRetryInterval] = value;
			}
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x06005E3B RID: 24123 RVA: 0x001440E7 File Offset: 0x001422E7
		// (set) Token: 0x06005E3C RID: 24124 RVA: 0x001440F9 File Offset: 0x001422F9
		[Parameter(Mandatory = false)]
		public bool MessageTrackingLogEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.MessageTrackingLogEnabled];
			}
			set
			{
				this[TransportServerSchema.MessageTrackingLogEnabled] = value;
			}
		}

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x0014410C File Offset: 0x0014230C
		// (set) Token: 0x06005E3E RID: 24126 RVA: 0x0014411E File Offset: 0x0014231E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MessageTrackingLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.MessageTrackingLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.MessageTrackingLogMaxAge] = value;
			}
		}

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x00144131 File Offset: 0x00142331
		// (set) Token: 0x06005E40 RID: 24128 RVA: 0x00144143 File Offset: 0x00142343
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.MessageTrackingLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.MessageTrackingLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x00144156 File Offset: 0x00142356
		// (set) Token: 0x06005E42 RID: 24130 RVA: 0x00144168 File Offset: 0x00142368
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MessageTrackingLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.MessageTrackingLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.MessageTrackingLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x0014417B File Offset: 0x0014237B
		// (set) Token: 0x06005E44 RID: 24132 RVA: 0x0014418D File Offset: 0x0014238D
		[Parameter(Mandatory = false)]
		public LocalLongFullPath MessageTrackingLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.MessageTrackingLogPath];
			}
			set
			{
				this[TransportServerSchema.MessageTrackingLogPath] = value;
			}
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x0014419B File Offset: 0x0014239B
		// (set) Token: 0x06005E46 RID: 24134 RVA: 0x001441AD File Offset: 0x001423AD
		[Parameter(Mandatory = false)]
		public bool IrmLogEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.IrmLogEnabled];
			}
			set
			{
				this[TransportServerSchema.IrmLogEnabled] = value;
			}
		}

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x001441C0 File Offset: 0x001423C0
		// (set) Token: 0x06005E48 RID: 24136 RVA: 0x001441D2 File Offset: 0x001423D2
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan IrmLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.IrmLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.IrmLogMaxAge] = value;
			}
		}

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x001441E5 File Offset: 0x001423E5
		// (set) Token: 0x06005E4A RID: 24138 RVA: 0x001441F7 File Offset: 0x001423F7
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.IrmLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.IrmLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x0014420A File Offset: 0x0014240A
		// (set) Token: 0x06005E4C RID: 24140 RVA: 0x0014421C File Offset: 0x0014241C
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize IrmLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.IrmLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.IrmLogMaxFileSize] = value;
			}
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x0014422F File Offset: 0x0014242F
		// (set) Token: 0x06005E4E RID: 24142 RVA: 0x00144241 File Offset: 0x00142441
		[Parameter(Mandatory = false)]
		public LocalLongFullPath IrmLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.IrmLogPath];
			}
			set
			{
				this[TransportServerSchema.IrmLogPath] = value;
			}
		}

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x06005E4F RID: 24143 RVA: 0x0014424F File Offset: 0x0014244F
		// (set) Token: 0x06005E50 RID: 24144 RVA: 0x00144261 File Offset: 0x00142461
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.ActiveUserStatisticsLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.ActiveUserStatisticsLogMaxAge] = value;
			}
		}

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x00144274 File Offset: 0x00142474
		// (set) Token: 0x06005E52 RID: 24146 RVA: 0x00144286 File Offset: 0x00142486
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.ActiveUserStatisticsLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.ActiveUserStatisticsLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x06005E53 RID: 24147 RVA: 0x00144299 File Offset: 0x00142499
		// (set) Token: 0x06005E54 RID: 24148 RVA: 0x001442AB File Offset: 0x001424AB
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.ActiveUserStatisticsLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.ActiveUserStatisticsLogMaxFileSize] = value;
			}
		}

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x06005E55 RID: 24149 RVA: 0x001442BE File Offset: 0x001424BE
		// (set) Token: 0x06005E56 RID: 24150 RVA: 0x001442D0 File Offset: 0x001424D0
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ActiveUserStatisticsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.ActiveUserStatisticsLogPath];
			}
			set
			{
				this[TransportServerSchema.ActiveUserStatisticsLogPath] = value;
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x001442DE File Offset: 0x001424DE
		// (set) Token: 0x06005E58 RID: 24152 RVA: 0x001442F0 File Offset: 0x001424F0
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ServerStatisticsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.ServerStatisticsLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.ServerStatisticsLogMaxAge] = value;
			}
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x00144303 File Offset: 0x00142503
		// (set) Token: 0x06005E5A RID: 24154 RVA: 0x00144315 File Offset: 0x00142515
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.ServerStatisticsLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.ServerStatisticsLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x06005E5B RID: 24155 RVA: 0x00144328 File Offset: 0x00142528
		// (set) Token: 0x06005E5C RID: 24156 RVA: 0x0014433A File Offset: 0x0014253A
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ServerStatisticsLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.ServerStatisticsLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.ServerStatisticsLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x06005E5D RID: 24157 RVA: 0x0014434D File Offset: 0x0014254D
		// (set) Token: 0x06005E5E RID: 24158 RVA: 0x0014435F File Offset: 0x0014255F
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ServerStatisticsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.ServerStatisticsLogPath];
			}
			set
			{
				this[TransportServerSchema.ServerStatisticsLogPath] = value;
			}
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x06005E5F RID: 24159 RVA: 0x0014436D File Offset: 0x0014256D
		// (set) Token: 0x06005E60 RID: 24160 RVA: 0x0014437F File Offset: 0x0014257F
		[Parameter(Mandatory = false)]
		public bool MessageTrackingLogSubjectLoggingEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.MessageTrackingLogSubjectLoggingEnabled];
			}
			set
			{
				this[TransportServerSchema.MessageTrackingLogSubjectLoggingEnabled] = value;
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x06005E61 RID: 24161 RVA: 0x00144392 File Offset: 0x00142592
		// (set) Token: 0x06005E62 RID: 24162 RVA: 0x001443A4 File Offset: 0x001425A4
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan OutboundConnectionFailureRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.OutboundConnectionFailureRetryInterval];
			}
			set
			{
				this[TransportServerSchema.OutboundConnectionFailureRetryInterval] = value;
			}
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x06005E63 RID: 24163 RVA: 0x001443B7 File Offset: 0x001425B7
		// (set) Token: 0x06005E64 RID: 24164 RVA: 0x001443C9 File Offset: 0x001425C9
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[TransportServerSchema.IntraOrgConnectorProtocolLoggingLevel];
			}
			set
			{
				this[TransportServerSchema.IntraOrgConnectorProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x06005E65 RID: 24165 RVA: 0x001443DC File Offset: 0x001425DC
		// (set) Token: 0x06005E66 RID: 24166 RVA: 0x001443EE File Offset: 0x001425EE
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize PickupDirectoryMaxHeaderSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.PickupDirectoryMaxHeaderSize];
			}
			set
			{
				this[TransportServerSchema.PickupDirectoryMaxHeaderSize] = value;
			}
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x06005E67 RID: 24167 RVA: 0x00144401 File Offset: 0x00142601
		// (set) Token: 0x06005E68 RID: 24168 RVA: 0x00144413 File Offset: 0x00142613
		[Parameter(Mandatory = false)]
		public int PickupDirectoryMaxMessagesPerMinute
		{
			get
			{
				return (int)this[TransportServerSchema.PickupDirectoryMaxMessagesPerMinute];
			}
			set
			{
				this[TransportServerSchema.PickupDirectoryMaxMessagesPerMinute] = value;
			}
		}

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x06005E69 RID: 24169 RVA: 0x00144426 File Offset: 0x00142626
		// (set) Token: 0x06005E6A RID: 24170 RVA: 0x00144438 File Offset: 0x00142638
		[Parameter(Mandatory = false)]
		public int PickupDirectoryMaxRecipientsPerMessage
		{
			get
			{
				return (int)this[TransportServerSchema.PickupDirectoryMaxRecipientsPerMessage];
			}
			set
			{
				this[TransportServerSchema.PickupDirectoryMaxRecipientsPerMessage] = value;
			}
		}

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x06005E6B RID: 24171 RVA: 0x0014444B File Offset: 0x0014264B
		// (set) Token: 0x06005E6C RID: 24172 RVA: 0x0014445D File Offset: 0x0014265D
		[Parameter(Mandatory = false)]
		public LocalLongFullPath PickupDirectoryPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.PickupDirectoryPath];
			}
			set
			{
				this[TransportServerSchema.PickupDirectoryPath] = value;
			}
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x06005E6D RID: 24173 RVA: 0x0014446B File Offset: 0x0014266B
		// (set) Token: 0x06005E6E RID: 24174 RVA: 0x0014447D File Offset: 0x0014267D
		[Parameter(Mandatory = false)]
		public bool PipelineTracingEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.PipelineTracingEnabled];
			}
			set
			{
				this[TransportServerSchema.PipelineTracingEnabled] = value;
			}
		}

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x06005E6F RID: 24175 RVA: 0x00144490 File Offset: 0x00142690
		// (set) Token: 0x06005E70 RID: 24176 RVA: 0x001444A2 File Offset: 0x001426A2
		[Parameter(Mandatory = false)]
		public bool ContentConversionTracingEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.ContentConversionTracingEnabled];
			}
			set
			{
				this[TransportServerSchema.ContentConversionTracingEnabled] = value;
			}
		}

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x06005E71 RID: 24177 RVA: 0x001444B5 File Offset: 0x001426B5
		// (set) Token: 0x06005E72 RID: 24178 RVA: 0x001444C7 File Offset: 0x001426C7
		[Parameter(Mandatory = false)]
		public LocalLongFullPath PipelineTracingPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.PipelineTracingPath];
			}
			set
			{
				this[TransportServerSchema.PipelineTracingPath] = value;
			}
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x06005E73 RID: 24179 RVA: 0x001444D5 File Offset: 0x001426D5
		// (set) Token: 0x06005E74 RID: 24180 RVA: 0x001444E7 File Offset: 0x001426E7
		[Parameter(Mandatory = false)]
		public SmtpAddress? PipelineTracingSenderAddress
		{
			get
			{
				return (SmtpAddress?)this[TransportServerSchema.PipelineTracingSenderAddress];
			}
			set
			{
				this[TransportServerSchema.PipelineTracingSenderAddress] = value;
			}
		}

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x06005E75 RID: 24181 RVA: 0x001444FA File Offset: 0x001426FA
		// (set) Token: 0x06005E76 RID: 24182 RVA: 0x0014450C File Offset: 0x0014270C
		[Parameter(Mandatory = false)]
		public bool PoisonMessageDetectionEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.PoisonMessageDetectionEnabled];
			}
			set
			{
				this[TransportServerSchema.PoisonMessageDetectionEnabled] = value;
			}
		}

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x06005E77 RID: 24183 RVA: 0x0014451F File Offset: 0x0014271F
		// (set) Token: 0x06005E78 RID: 24184 RVA: 0x00144531 File Offset: 0x00142731
		[Parameter(Mandatory = false)]
		public int PoisonThreshold
		{
			get
			{
				return (int)this[TransportServerSchema.PoisonThreshold];
			}
			set
			{
				this[TransportServerSchema.PoisonThreshold] = value;
			}
		}

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x00144544 File Offset: 0x00142744
		// (set) Token: 0x06005E7A RID: 24186 RVA: 0x00144556 File Offset: 0x00142756
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan QueueMaxIdleTime
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.QueueMaxIdleTime];
			}
			set
			{
				this[TransportServerSchema.QueueMaxIdleTime] = value;
			}
		}

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06005E7B RID: 24187 RVA: 0x00144569 File Offset: 0x00142769
		// (set) Token: 0x06005E7C RID: 24188 RVA: 0x0014457B File Offset: 0x0014277B
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReceiveProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.ReceiveProtocolLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.ReceiveProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x06005E7D RID: 24189 RVA: 0x0014458E File Offset: 0x0014278E
		// (set) Token: 0x06005E7E RID: 24190 RVA: 0x001445A0 File Offset: 0x001427A0
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.ReceiveProtocolLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.ReceiveProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x06005E7F RID: 24191 RVA: 0x001445B3 File Offset: 0x001427B3
		// (set) Token: 0x06005E80 RID: 24192 RVA: 0x001445C5 File Offset: 0x001427C5
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.ReceiveProtocolLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.ReceiveProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x001445D8 File Offset: 0x001427D8
		// (set) Token: 0x06005E82 RID: 24194 RVA: 0x001445EA File Offset: 0x001427EA
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReceiveProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.ReceiveProtocolLogPath];
			}
			set
			{
				this[TransportServerSchema.ReceiveProtocolLogPath] = value;
			}
		}

		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x001445F8 File Offset: 0x001427F8
		// (set) Token: 0x06005E84 RID: 24196 RVA: 0x0014460A File Offset: 0x0014280A
		[Parameter(Mandatory = false)]
		public bool RecipientValidationCacheEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.RecipientValidationCacheEnabled];
			}
			set
			{
				this[TransportServerSchema.RecipientValidationCacheEnabled] = value;
			}
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x0014461D File Offset: 0x0014281D
		// (set) Token: 0x06005E86 RID: 24198 RVA: 0x0014462F File Offset: 0x0014282F
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReplayDirectoryPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.ReplayDirectoryPath];
			}
			set
			{
				this[TransportServerSchema.ReplayDirectoryPath] = value;
			}
		}

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x06005E87 RID: 24199 RVA: 0x0014463D File Offset: 0x0014283D
		// (set) Token: 0x06005E88 RID: 24200 RVA: 0x0014464F File Offset: 0x0014284F
		[Parameter(Mandatory = false)]
		public string RootDropDirectoryPath
		{
			get
			{
				return (string)this[TransportServerSchema.RootDropDirectoryPath];
			}
			set
			{
				this[TransportServerSchema.RootDropDirectoryPath] = value;
			}
		}

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x06005E89 RID: 24201 RVA: 0x0014465D File Offset: 0x0014285D
		// (set) Token: 0x06005E8A RID: 24202 RVA: 0x0014466F File Offset: 0x0014286F
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan RoutingTableLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.RoutingTableLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.RoutingTableLogMaxAge] = value;
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x06005E8B RID: 24203 RVA: 0x00144682 File Offset: 0x00142882
		// (set) Token: 0x06005E8C RID: 24204 RVA: 0x00144694 File Offset: 0x00142894
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.RoutingTableLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.RoutingTableLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x06005E8D RID: 24205 RVA: 0x001446A7 File Offset: 0x001428A7
		// (set) Token: 0x06005E8E RID: 24206 RVA: 0x001446B9 File Offset: 0x001428B9
		[Parameter(Mandatory = false)]
		public LocalLongFullPath RoutingTableLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.RoutingTableLogPath];
			}
			set
			{
				this[TransportServerSchema.RoutingTableLogPath] = value;
			}
		}

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x06005E8F RID: 24207 RVA: 0x001446C7 File Offset: 0x001428C7
		// (set) Token: 0x06005E90 RID: 24208 RVA: 0x001446D9 File Offset: 0x001428D9
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SendProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.SendProtocolLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.SendProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x06005E91 RID: 24209 RVA: 0x001446EC File Offset: 0x001428EC
		// (set) Token: 0x06005E92 RID: 24210 RVA: 0x001446FE File Offset: 0x001428FE
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.SendProtocolLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.SendProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x06005E93 RID: 24211 RVA: 0x00144711 File Offset: 0x00142911
		// (set) Token: 0x06005E94 RID: 24212 RVA: 0x00144723 File Offset: 0x00142923
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TransportServerSchema.SendProtocolLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.SendProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x06005E95 RID: 24213 RVA: 0x00144736 File Offset: 0x00142936
		// (set) Token: 0x06005E96 RID: 24214 RVA: 0x00144748 File Offset: 0x00142948
		[Parameter(Mandatory = false)]
		public LocalLongFullPath SendProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.SendProtocolLogPath];
			}
			set
			{
				this[TransportServerSchema.SendProtocolLogPath] = value;
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x06005E97 RID: 24215 RVA: 0x00144756 File Offset: 0x00142956
		// (set) Token: 0x06005E98 RID: 24216 RVA: 0x00144768 File Offset: 0x00142968
		[Parameter(Mandatory = false)]
		public int TransientFailureRetryCount
		{
			get
			{
				return (int)this[TransportServerSchema.TransientFailureRetryCount];
			}
			set
			{
				this[TransportServerSchema.TransientFailureRetryCount] = value;
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06005E99 RID: 24217 RVA: 0x0014477B File Offset: 0x0014297B
		// (set) Token: 0x06005E9A RID: 24218 RVA: 0x0014478D File Offset: 0x0014298D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransientFailureRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.TransientFailureRetryInterval];
			}
			set
			{
				this[TransportServerSchema.TransientFailureRetryInterval] = value;
			}
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x001447A0 File Offset: 0x001429A0
		public bool AntispamUpdatesEnabled
		{
			get
			{
				return (bool)this[ServerSchema.AntispamUpdatesEnabled];
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06005E9C RID: 24220 RVA: 0x001447B2 File Offset: 0x001429B2
		public string InternalTransportCertificateThumbprint
		{
			get
			{
				return (string)this[TransportServerSchema.InternalTransportCertificateThumbprint];
			}
		}

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06005E9D RID: 24221 RVA: 0x001447C4 File Offset: 0x001429C4
		// (set) Token: 0x06005E9E RID: 24222 RVA: 0x001447D6 File Offset: 0x001429D6
		[Parameter(Mandatory = false)]
		public bool TransportSyncEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncEnabled] = value;
			}
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06005E9F RID: 24223 RVA: 0x001447E9 File Offset: 0x001429E9
		// (set) Token: 0x06005EA0 RID: 24224 RVA: 0x001447FB File Offset: 0x001429FB
		[Parameter(Mandatory = false)]
		public bool TransportSyncPopEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncPopEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncPopEnabled] = value;
			}
		}

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06005EA1 RID: 24225 RVA: 0x0014480E File Offset: 0x00142A0E
		// (set) Token: 0x06005EA2 RID: 24226 RVA: 0x00144820 File Offset: 0x00142A20
		[Parameter(Mandatory = false)]
		public bool WindowsLiveHotmailTransportSyncEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.WindowsLiveHotmailTransportSyncEnabled];
			}
			set
			{
				this[TransportServerSchema.WindowsLiveHotmailTransportSyncEnabled] = value;
			}
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06005EA3 RID: 24227 RVA: 0x00144833 File Offset: 0x00142A33
		// (set) Token: 0x06005EA4 RID: 24228 RVA: 0x00144845 File Offset: 0x00142A45
		[Parameter(Mandatory = false)]
		public bool TransportSyncExchangeEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncExchangeEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncExchangeEnabled] = value;
			}
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x06005EA5 RID: 24229 RVA: 0x00144858 File Offset: 0x00142A58
		// (set) Token: 0x06005EA6 RID: 24230 RVA: 0x0014486A File Offset: 0x00142A6A
		[Parameter(Mandatory = false)]
		public bool TransportSyncImapEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncImapEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncImapEnabled] = value;
			}
		}

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x06005EA7 RID: 24231 RVA: 0x0014487D File Offset: 0x00142A7D
		// (set) Token: 0x06005EA8 RID: 24232 RVA: 0x0014488F File Offset: 0x00142A8F
		[Parameter(Mandatory = false)]
		public int MaxNumberOfTransportSyncAttempts
		{
			get
			{
				return (int)this[TransportServerSchema.MaxNumberOfTransportSyncAttempts];
			}
			set
			{
				this[TransportServerSchema.MaxNumberOfTransportSyncAttempts] = value;
			}
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x06005EA9 RID: 24233 RVA: 0x001448A2 File Offset: 0x00142AA2
		// (set) Token: 0x06005EAA RID: 24234 RVA: 0x001448B4 File Offset: 0x00142AB4
		[Parameter(Mandatory = false)]
		public int MaxActiveTransportSyncJobsPerProcessor
		{
			get
			{
				return (int)this[TransportServerSchema.MaxActiveTransportSyncJobsPerProcessor];
			}
			set
			{
				this[TransportServerSchema.MaxActiveTransportSyncJobsPerProcessor] = value;
			}
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06005EAB RID: 24235 RVA: 0x001448C7 File Offset: 0x00142AC7
		// (set) Token: 0x06005EAC RID: 24236 RVA: 0x001448D9 File Offset: 0x00142AD9
		[Parameter(Mandatory = false)]
		public string HttpTransportSyncProxyServer
		{
			get
			{
				return (string)this[TransportServerSchema.HttpTransportSyncProxyServer];
			}
			set
			{
				this[TransportServerSchema.HttpTransportSyncProxyServer] = value;
			}
		}

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x001448E7 File Offset: 0x00142AE7
		// (set) Token: 0x06005EAE RID: 24238 RVA: 0x001448F9 File Offset: 0x00142AF9
		[Parameter(Mandatory = false)]
		public bool HttpProtocolLogEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.HttpProtocolLogEnabled];
			}
			set
			{
				this[TransportServerSchema.HttpProtocolLogEnabled] = value;
			}
		}

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x0014490C File Offset: 0x00142B0C
		// (set) Token: 0x06005EB0 RID: 24240 RVA: 0x0014491E File Offset: 0x00142B1E
		[Parameter(Mandatory = false)]
		public LocalLongFullPath HttpProtocolLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.HttpProtocolLogFilePath];
			}
			set
			{
				this[TransportServerSchema.HttpProtocolLogFilePath] = value;
			}
		}

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x0014492C File Offset: 0x00142B2C
		// (set) Token: 0x06005EB2 RID: 24242 RVA: 0x0014493E File Offset: 0x00142B3E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan HttpProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.HttpProtocolLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.HttpProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x06005EB3 RID: 24243 RVA: 0x00144951 File Offset: 0x00142B51
		// (set) Token: 0x06005EB4 RID: 24244 RVA: 0x00144963 File Offset: 0x00142B63
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.HttpProtocolLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.HttpProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x00144976 File Offset: 0x00142B76
		// (set) Token: 0x06005EB6 RID: 24246 RVA: 0x00144988 File Offset: 0x00142B88
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize HttpProtocolLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.HttpProtocolLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.HttpProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x0014499B File Offset: 0x00142B9B
		// (set) Token: 0x06005EB8 RID: 24248 RVA: 0x001449AD File Offset: 0x00142BAD
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel HttpProtocolLogLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[TransportServerSchema.HttpProtocolLogLoggingLevel];
			}
			set
			{
				this[TransportServerSchema.HttpProtocolLogLoggingLevel] = value;
			}
		}

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x06005EB9 RID: 24249 RVA: 0x001449C0 File Offset: 0x00142BC0
		// (set) Token: 0x06005EBA RID: 24250 RVA: 0x001449D2 File Offset: 0x00142BD2
		[Parameter(Mandatory = false)]
		public bool TransportSyncLogEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncLogEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncLogEnabled] = value;
			}
		}

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x001449E5 File Offset: 0x00142BE5
		// (set) Token: 0x06005EBC RID: 24252 RVA: 0x001449F7 File Offset: 0x00142BF7
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.TransportSyncLogFilePath];
			}
			set
			{
				this[TransportServerSchema.TransportSyncLogFilePath] = value;
			}
		}

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x00144A05 File Offset: 0x00142C05
		// (set) Token: 0x06005EBE RID: 24254 RVA: 0x00144A17 File Offset: 0x00142C17
		[Parameter(Mandatory = false)]
		public SyncLoggingLevel TransportSyncLogLoggingLevel
		{
			get
			{
				return (SyncLoggingLevel)this[TransportServerSchema.TransportSyncLogLoggingLevel];
			}
			set
			{
				this[TransportServerSchema.TransportSyncLogLoggingLevel] = value;
			}
		}

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x00144A2A File Offset: 0x00142C2A
		// (set) Token: 0x06005EC0 RID: 24256 RVA: 0x00144A3C File Offset: 0x00142C3C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.TransportSyncLogMaxAge];
			}
			set
			{
				this[TransportServerSchema.TransportSyncLogMaxAge] = value;
			}
		}

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x00144A4F File Offset: 0x00142C4F
		// (set) Token: 0x06005EC2 RID: 24258 RVA: 0x00144A61 File Offset: 0x00142C61
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.TransportSyncLogMaxDirectorySize];
			}
			set
			{
				this[TransportServerSchema.TransportSyncLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x00144A74 File Offset: 0x00142C74
		// (set) Token: 0x06005EC4 RID: 24260 RVA: 0x00144A86 File Offset: 0x00142C86
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.TransportSyncLogMaxFileSize];
			}
			set
			{
				this[TransportServerSchema.TransportSyncLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x06005EC5 RID: 24261 RVA: 0x00144A99 File Offset: 0x00142C99
		// (set) Token: 0x06005EC6 RID: 24262 RVA: 0x00144AAB File Offset: 0x00142CAB
		[Parameter(Mandatory = false)]
		public bool TransportSyncHubHealthLogEnabled
		{
			get
			{
				return (bool)this[ADTransportServerSchema.TransportSyncHubHealthLogEnabled];
			}
			set
			{
				this[ADTransportServerSchema.TransportSyncHubHealthLogEnabled] = value;
			}
		}

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x00144ABE File Offset: 0x00142CBE
		// (set) Token: 0x06005EC8 RID: 24264 RVA: 0x00144AD0 File Offset: 0x00142CD0
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncHubHealthLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[ADTransportServerSchema.TransportSyncHubHealthLogFilePath];
			}
			set
			{
				this[ADTransportServerSchema.TransportSyncHubHealthLogFilePath] = value;
			}
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x00144ADE File Offset: 0x00142CDE
		// (set) Token: 0x06005ECA RID: 24266 RVA: 0x00144AF0 File Offset: 0x00142CF0
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ADTransportServerSchema.TransportSyncHubHealthLogMaxAge];
			}
			set
			{
				this[ADTransportServerSchema.TransportSyncHubHealthLogMaxAge] = value;
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x00144B03 File Offset: 0x00142D03
		// (set) Token: 0x06005ECC RID: 24268 RVA: 0x00144B15 File Offset: 0x00142D15
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ADTransportServerSchema.TransportSyncHubHealthLogMaxDirectorySize];
			}
			set
			{
				this[ADTransportServerSchema.TransportSyncHubHealthLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x00144B28 File Offset: 0x00142D28
		// (set) Token: 0x06005ECE RID: 24270 RVA: 0x00144B3A File Offset: 0x00142D3A
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ADTransportServerSchema.TransportSyncHubHealthLogMaxFileSize];
			}
			set
			{
				this[ADTransportServerSchema.TransportSyncHubHealthLogMaxFileSize] = value;
			}
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x00144B4D File Offset: 0x00142D4D
		// (set) Token: 0x06005ED0 RID: 24272 RVA: 0x00144B5F File Offset: 0x00142D5F
		[Parameter(Mandatory = false)]
		public bool TransportSyncAccountsPoisonDetectionEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncAccountsPoisonDetectionEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncAccountsPoisonDetectionEnabled] = value;
			}
		}

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x00144B72 File Offset: 0x00142D72
		// (set) Token: 0x06005ED2 RID: 24274 RVA: 0x00144B84 File Offset: 0x00142D84
		[Parameter(Mandatory = false)]
		public int TransportSyncAccountsPoisonAccountThreshold
		{
			get
			{
				return (int)this[TransportServerSchema.TransportSyncAccountsPoisonAccountThreshold];
			}
			set
			{
				this[TransportServerSchema.TransportSyncAccountsPoisonAccountThreshold] = value;
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x00144B97 File Offset: 0x00142D97
		// (set) Token: 0x06005ED4 RID: 24276 RVA: 0x00144BA9 File Offset: 0x00142DA9
		[Parameter(Mandatory = false)]
		public int TransportSyncAccountsPoisonItemThreshold
		{
			get
			{
				return (int)this[TransportServerSchema.TransportSyncAccountsPoisonItemThreshold];
			}
			set
			{
				this[TransportServerSchema.TransportSyncAccountsPoisonItemThreshold] = value;
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x06005ED5 RID: 24277 RVA: 0x00144BBC File Offset: 0x00142DBC
		// (set) Token: 0x06005ED6 RID: 24278 RVA: 0x00144BCE File Offset: 0x00142DCE
		[Parameter(Mandatory = false)]
		public int TransportSyncAccountsSuccessivePoisonItemThreshold
		{
			get
			{
				return (int)this[ADTransportServerSchema.TransportSyncAccountsSuccessivePoisonItemThreshold];
			}
			set
			{
				this[ADTransportServerSchema.TransportSyncAccountsSuccessivePoisonItemThreshold] = value;
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x06005ED7 RID: 24279 RVA: 0x00144BE1 File Offset: 0x00142DE1
		// (set) Token: 0x06005ED8 RID: 24280 RVA: 0x00144BF3 File Offset: 0x00142DF3
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportServerSchema.TransportSyncRemoteConnectionTimeout];
			}
			set
			{
				this[TransportServerSchema.TransportSyncRemoteConnectionTimeout] = value;
			}
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x06005ED9 RID: 24281 RVA: 0x00144C06 File Offset: 0x00142E06
		// (set) Token: 0x06005EDA RID: 24282 RVA: 0x00144C18 File Offset: 0x00142E18
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.TransportSyncMaxDownloadSizePerItem];
			}
			set
			{
				this[TransportServerSchema.TransportSyncMaxDownloadSizePerItem] = value;
			}
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x00144C2B File Offset: 0x00142E2B
		// (set) Token: 0x06005EDC RID: 24284 RVA: 0x00144C3D File Offset: 0x00142E3D
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportServerSchema.TransportSyncMaxDownloadSizePerConnection];
			}
			set
			{
				this[TransportServerSchema.TransportSyncMaxDownloadSizePerConnection] = value;
			}
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06005EDD RID: 24285 RVA: 0x00144C50 File Offset: 0x00142E50
		// (set) Token: 0x06005EDE RID: 24286 RVA: 0x00144C62 File Offset: 0x00142E62
		[Parameter(Mandatory = false)]
		public int TransportSyncMaxDownloadItemsPerConnection
		{
			get
			{
				return (int)this[TransportServerSchema.TransportSyncMaxDownloadItemsPerConnection];
			}
			set
			{
				this[TransportServerSchema.TransportSyncMaxDownloadItemsPerConnection] = value;
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06005EDF RID: 24287 RVA: 0x00144C75 File Offset: 0x00142E75
		// (set) Token: 0x06005EE0 RID: 24288 RVA: 0x00144C87 File Offset: 0x00142E87
		[Parameter(Mandatory = false)]
		public string DeltaSyncClientCertificateThumbprint
		{
			get
			{
				return (string)this[TransportServerSchema.DeltaSyncClientCertificateThumbprint];
			}
			set
			{
				this[TransportServerSchema.DeltaSyncClientCertificateThumbprint] = value;
			}
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x06005EE1 RID: 24289 RVA: 0x00144C95 File Offset: 0x00142E95
		// (set) Token: 0x06005EE2 RID: 24290 RVA: 0x00144CA7 File Offset: 0x00142EA7
		[Parameter(Mandatory = false)]
		public bool UseDowngradedExchangeServerAuth
		{
			get
			{
				return (bool)this[ADTransportServerSchema.UseDowngradedExchangeServerAuth];
			}
			set
			{
				this[ADTransportServerSchema.UseDowngradedExchangeServerAuth] = value;
			}
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06005EE3 RID: 24291 RVA: 0x00144CBA File Offset: 0x00142EBA
		// (set) Token: 0x06005EE4 RID: 24292 RVA: 0x00144CCC File Offset: 0x00142ECC
		[Parameter(Mandatory = false)]
		public int IntraOrgConnectorSmtpMaxMessagesPerConnection
		{
			get
			{
				return (int)this[TransportServerSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection];
			}
			set
			{
				this[TransportServerSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection] = value;
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06005EE5 RID: 24293 RVA: 0x00144CDF File Offset: 0x00142EDF
		// (set) Token: 0x06005EE6 RID: 24294 RVA: 0x00144CF1 File Offset: 0x00142EF1
		[Parameter(Mandatory = false)]
		public bool TransportSyncLinkedInEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncLinkedInEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncLinkedInEnabled] = value;
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x06005EE7 RID: 24295 RVA: 0x00144D04 File Offset: 0x00142F04
		// (set) Token: 0x06005EE8 RID: 24296 RVA: 0x00144D16 File Offset: 0x00142F16
		[Parameter(Mandatory = false)]
		public bool TransportSyncFacebookEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.TransportSyncFacebookEnabled];
			}
			set
			{
				this[TransportServerSchema.TransportSyncFacebookEnabled] = value;
			}
		}

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x06005EE9 RID: 24297 RVA: 0x00144D29 File Offset: 0x00142F29
		public EnhancedTimeSpan QueueLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.QueueLogMaxAge];
			}
		}

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x06005EEA RID: 24298 RVA: 0x00144D3B File Offset: 0x00142F3B
		public Unlimited<ByteQuantifiedSize> QueueLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.QueueLogMaxDirectorySize];
			}
		}

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x06005EEB RID: 24299 RVA: 0x00144D4D File Offset: 0x00142F4D
		public Unlimited<ByteQuantifiedSize> QueueLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.QueueLogMaxFileSize];
			}
		}

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x00144D5F File Offset: 0x00142F5F
		public LocalLongFullPath QueueLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.QueueLogPath];
			}
		}

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06005EED RID: 24301 RVA: 0x00144D71 File Offset: 0x00142F71
		public EnhancedTimeSpan WlmLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.WlmLogMaxAge];
			}
		}

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x00144D83 File Offset: 0x00142F83
		public Unlimited<ByteQuantifiedSize> WlmLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.WlmLogMaxDirectorySize];
			}
		}

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06005EEF RID: 24303 RVA: 0x00144D95 File Offset: 0x00142F95
		public Unlimited<ByteQuantifiedSize> WlmLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.WlmLogMaxFileSize];
			}
		}

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x00144DA7 File Offset: 0x00142FA7
		public LocalLongFullPath WlmLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.WlmLogPath];
			}
		}

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x06005EF1 RID: 24305 RVA: 0x00144DB9 File Offset: 0x00142FB9
		public EnhancedTimeSpan AgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.AgentLogMaxAge];
			}
		}

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x06005EF2 RID: 24306 RVA: 0x00144DCB File Offset: 0x00142FCB
		public Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.AgentLogMaxDirectorySize];
			}
		}

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x06005EF3 RID: 24307 RVA: 0x00144DDD File Offset: 0x00142FDD
		public Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.AgentLogMaxFileSize];
			}
		}

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x00144DEF File Offset: 0x00142FEF
		public LocalLongFullPath AgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.AgentLogPath];
			}
		}

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06005EF5 RID: 24309 RVA: 0x00144E01 File Offset: 0x00143001
		public bool AgentLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.AgentLogEnabled];
			}
		}

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06005EF6 RID: 24310 RVA: 0x00144E13 File Offset: 0x00143013
		public EnhancedTimeSpan FlowControlLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.FlowControlLogMaxAge];
			}
		}

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06005EF7 RID: 24311 RVA: 0x00144E25 File Offset: 0x00143025
		public Unlimited<ByteQuantifiedSize> FlowControlLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.FlowControlLogMaxDirectorySize];
			}
		}

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06005EF8 RID: 24312 RVA: 0x00144E37 File Offset: 0x00143037
		public Unlimited<ByteQuantifiedSize> FlowControlLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.FlowControlLogMaxFileSize];
			}
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06005EF9 RID: 24313 RVA: 0x00144E49 File Offset: 0x00143049
		public LocalLongFullPath FlowControlLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.FlowControlLogPath];
			}
		}

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06005EFA RID: 24314 RVA: 0x00144E5B File Offset: 0x0014305B
		public bool FlowControlLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.FlowControlLogEnabled];
			}
		}

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06005EFB RID: 24315 RVA: 0x00144E6D File Offset: 0x0014306D
		public EnhancedTimeSpan ProcessingSchedulerLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ProcessingSchedulerLogMaxAge];
			}
		}

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06005EFC RID: 24316 RVA: 0x00144E7F File Offset: 0x0014307F
		public Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ProcessingSchedulerLogMaxDirectorySize];
			}
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06005EFD RID: 24317 RVA: 0x00144E91 File Offset: 0x00143091
		public Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ProcessingSchedulerLogMaxFileSize];
			}
		}

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06005EFE RID: 24318 RVA: 0x00144EA3 File Offset: 0x001430A3
		public LocalLongFullPath ProcessingSchedulerLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ProcessingSchedulerLogPath];
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x00144EB5 File Offset: 0x001430B5
		public bool ProcessingSchedulerLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.ProcessingSchedulerLogEnabled];
			}
		}

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x00144EC7 File Offset: 0x001430C7
		public EnhancedTimeSpan ResourceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ResourceLogMaxAge];
			}
		}

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x06005F01 RID: 24321 RVA: 0x00144ED9 File Offset: 0x001430D9
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ResourceLogMaxDirectorySize];
			}
		}

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x06005F02 RID: 24322 RVA: 0x00144EEB File Offset: 0x001430EB
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ResourceLogMaxFileSize];
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x06005F03 RID: 24323 RVA: 0x00144EFD File Offset: 0x001430FD
		public LocalLongFullPath ResourceLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ResourceLogPath];
			}
		}

		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x06005F04 RID: 24324 RVA: 0x00144F0F File Offset: 0x0014310F
		public bool ResourceLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.ResourceLogEnabled];
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06005F05 RID: 24325 RVA: 0x00144F21 File Offset: 0x00143121
		public EnhancedTimeSpan DnsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.DnsLogMaxAge];
			}
		}

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x06005F06 RID: 24326 RVA: 0x00144F33 File Offset: 0x00143133
		public Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.DnsLogMaxDirectorySize];
			}
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x06005F07 RID: 24327 RVA: 0x00144F45 File Offset: 0x00143145
		public Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.DnsLogMaxFileSize];
			}
		}

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06005F08 RID: 24328 RVA: 0x00144F57 File Offset: 0x00143157
		public LocalLongFullPath DnsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.DnsLogPath];
			}
		}

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x00144F69 File Offset: 0x00143169
		public bool DnsLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.DnsLogEnabled];
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x06005F0A RID: 24330 RVA: 0x00144F7B File Offset: 0x0014317B
		public EnhancedTimeSpan JournalLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.JournalLogMaxAge];
			}
		}

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x06005F0B RID: 24331 RVA: 0x00144F8D File Offset: 0x0014318D
		public Unlimited<ByteQuantifiedSize> JournalLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.JournalLogMaxDirectorySize];
			}
		}

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x00144F9F File Offset: 0x0014319F
		public Unlimited<ByteQuantifiedSize> JournalLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.JournalLogMaxFileSize];
			}
		}

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06005F0D RID: 24333 RVA: 0x00144FB1 File Offset: 0x001431B1
		public LocalLongFullPath JournalLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.JournalLogPath];
			}
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06005F0E RID: 24334 RVA: 0x00144FC3 File Offset: 0x001431C3
		public bool JournalLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.JournalLogEnabled];
			}
		}

		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06005F0F RID: 24335 RVA: 0x00144FD5 File Offset: 0x001431D5
		public EnhancedTimeSpan TransportMaintenanceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.TransportMaintenanceLogMaxAge];
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x06005F10 RID: 24336 RVA: 0x00144FE7 File Offset: 0x001431E7
		public Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.TransportMaintenanceLogMaxDirectorySize];
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x00144FF9 File Offset: 0x001431F9
		public Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.TransportMaintenanceLogMaxFileSize];
			}
		}

		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x06005F12 RID: 24338 RVA: 0x0014500B File Offset: 0x0014320B
		public LocalLongFullPath TransportMaintenanceLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.TransportMaintenanceLogPath];
			}
		}

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x06005F13 RID: 24339 RVA: 0x0014501D File Offset: 0x0014321D
		public bool TransportMaintenanceLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportMaintenanceLogEnabled];
			}
		}

		// Token: 0x04003FFA RID: 16378
		private static TransportServerSchema schema = ObjectSchema.GetInstance<TransportServerSchema>();

		// Token: 0x04003FFB RID: 16379
		private static ADTransportServerSchema adSchema = ObjectSchema.GetInstance<ADTransportServerSchema>();
	}
}
