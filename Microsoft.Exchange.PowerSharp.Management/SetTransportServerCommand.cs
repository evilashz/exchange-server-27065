using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200066F RID: 1647
	public class SetTransportServerCommand : SyntheticCommandWithPipelineInputNoOutput<TransportServer>
	{
		// Token: 0x060055EC RID: 21996 RVA: 0x00086FE2 File Offset: 0x000851E2
		private SetTransportServerCommand() : base("Set-TransportServer")
		{
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x00086FEF File Offset: 0x000851EF
		public SetTransportServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x00086FFE File Offset: 0x000851FE
		public virtual SetTransportServerCommand SetParameters(SetTransportServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x00087008 File Offset: 0x00085208
		public virtual SetTransportServerCommand SetParameters(SetTransportServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000670 RID: 1648
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170034C7 RID: 13511
			// (set) Token: 0x060055F0 RID: 22000 RVA: 0x00087012 File Offset: 0x00085212
			public virtual EnhancedTimeSpan QueueLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxAge"] = value;
				}
			}

			// Token: 0x170034C8 RID: 13512
			// (set) Token: 0x060055F1 RID: 22001 RVA: 0x0008702A File Offset: 0x0008522A
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034C9 RID: 13513
			// (set) Token: 0x060055F2 RID: 22002 RVA: 0x00087042 File Offset: 0x00085242
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034CA RID: 13514
			// (set) Token: 0x060055F3 RID: 22003 RVA: 0x0008705A File Offset: 0x0008525A
			public virtual LocalLongFullPath QueueLogPath
			{
				set
				{
					base.PowerSharpParameters["QueueLogPath"] = value;
				}
			}

			// Token: 0x170034CB RID: 13515
			// (set) Token: 0x060055F4 RID: 22004 RVA: 0x0008706D File Offset: 0x0008526D
			public virtual EnhancedTimeSpan WlmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxAge"] = value;
				}
			}

			// Token: 0x170034CC RID: 13516
			// (set) Token: 0x060055F5 RID: 22005 RVA: 0x00087085 File Offset: 0x00085285
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034CD RID: 13517
			// (set) Token: 0x060055F6 RID: 22006 RVA: 0x0008709D File Offset: 0x0008529D
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034CE RID: 13518
			// (set) Token: 0x060055F7 RID: 22007 RVA: 0x000870B5 File Offset: 0x000852B5
			public virtual LocalLongFullPath WlmLogPath
			{
				set
				{
					base.PowerSharpParameters["WlmLogPath"] = value;
				}
			}

			// Token: 0x170034CF RID: 13519
			// (set) Token: 0x060055F8 RID: 22008 RVA: 0x000870C8 File Offset: 0x000852C8
			public virtual EnhancedTimeSpan AgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxAge"] = value;
				}
			}

			// Token: 0x170034D0 RID: 13520
			// (set) Token: 0x060055F9 RID: 22009 RVA: 0x000870E0 File Offset: 0x000852E0
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034D1 RID: 13521
			// (set) Token: 0x060055FA RID: 22010 RVA: 0x000870F8 File Offset: 0x000852F8
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034D2 RID: 13522
			// (set) Token: 0x060055FB RID: 22011 RVA: 0x00087110 File Offset: 0x00085310
			public virtual LocalLongFullPath AgentLogPath
			{
				set
				{
					base.PowerSharpParameters["AgentLogPath"] = value;
				}
			}

			// Token: 0x170034D3 RID: 13523
			// (set) Token: 0x060055FC RID: 22012 RVA: 0x00087123 File Offset: 0x00085323
			public virtual bool AgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AgentLogEnabled"] = value;
				}
			}

			// Token: 0x170034D4 RID: 13524
			// (set) Token: 0x060055FD RID: 22013 RVA: 0x0008713B File Offset: 0x0008533B
			public virtual EnhancedTimeSpan FlowControlLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxAge"] = value;
				}
			}

			// Token: 0x170034D5 RID: 13525
			// (set) Token: 0x060055FE RID: 22014 RVA: 0x00087153 File Offset: 0x00085353
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034D6 RID: 13526
			// (set) Token: 0x060055FF RID: 22015 RVA: 0x0008716B File Offset: 0x0008536B
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034D7 RID: 13527
			// (set) Token: 0x06005600 RID: 22016 RVA: 0x00087183 File Offset: 0x00085383
			public virtual LocalLongFullPath FlowControlLogPath
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogPath"] = value;
				}
			}

			// Token: 0x170034D8 RID: 13528
			// (set) Token: 0x06005601 RID: 22017 RVA: 0x00087196 File Offset: 0x00085396
			public virtual bool FlowControlLogEnabled
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogEnabled"] = value;
				}
			}

			// Token: 0x170034D9 RID: 13529
			// (set) Token: 0x06005602 RID: 22018 RVA: 0x000871AE File Offset: 0x000853AE
			public virtual EnhancedTimeSpan ProcessingSchedulerLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxAge"] = value;
				}
			}

			// Token: 0x170034DA RID: 13530
			// (set) Token: 0x06005603 RID: 22019 RVA: 0x000871C6 File Offset: 0x000853C6
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034DB RID: 13531
			// (set) Token: 0x06005604 RID: 22020 RVA: 0x000871DE File Offset: 0x000853DE
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034DC RID: 13532
			// (set) Token: 0x06005605 RID: 22021 RVA: 0x000871F6 File Offset: 0x000853F6
			public virtual LocalLongFullPath ProcessingSchedulerLogPath
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogPath"] = value;
				}
			}

			// Token: 0x170034DD RID: 13533
			// (set) Token: 0x06005606 RID: 22022 RVA: 0x00087209 File Offset: 0x00085409
			public virtual bool ProcessingSchedulerLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogEnabled"] = value;
				}
			}

			// Token: 0x170034DE RID: 13534
			// (set) Token: 0x06005607 RID: 22023 RVA: 0x00087221 File Offset: 0x00085421
			public virtual EnhancedTimeSpan ResourceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxAge"] = value;
				}
			}

			// Token: 0x170034DF RID: 13535
			// (set) Token: 0x06005608 RID: 22024 RVA: 0x00087239 File Offset: 0x00085439
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034E0 RID: 13536
			// (set) Token: 0x06005609 RID: 22025 RVA: 0x00087251 File Offset: 0x00085451
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034E1 RID: 13537
			// (set) Token: 0x0600560A RID: 22026 RVA: 0x00087269 File Offset: 0x00085469
			public virtual LocalLongFullPath ResourceLogPath
			{
				set
				{
					base.PowerSharpParameters["ResourceLogPath"] = value;
				}
			}

			// Token: 0x170034E2 RID: 13538
			// (set) Token: 0x0600560B RID: 22027 RVA: 0x0008727C File Offset: 0x0008547C
			public virtual bool ResourceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ResourceLogEnabled"] = value;
				}
			}

			// Token: 0x170034E3 RID: 13539
			// (set) Token: 0x0600560C RID: 22028 RVA: 0x00087294 File Offset: 0x00085494
			public virtual EnhancedTimeSpan DnsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxAge"] = value;
				}
			}

			// Token: 0x170034E4 RID: 13540
			// (set) Token: 0x0600560D RID: 22029 RVA: 0x000872AC File Offset: 0x000854AC
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034E5 RID: 13541
			// (set) Token: 0x0600560E RID: 22030 RVA: 0x000872C4 File Offset: 0x000854C4
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034E6 RID: 13542
			// (set) Token: 0x0600560F RID: 22031 RVA: 0x000872DC File Offset: 0x000854DC
			public virtual LocalLongFullPath DnsLogPath
			{
				set
				{
					base.PowerSharpParameters["DnsLogPath"] = value;
				}
			}

			// Token: 0x170034E7 RID: 13543
			// (set) Token: 0x06005610 RID: 22032 RVA: 0x000872EF File Offset: 0x000854EF
			public virtual bool DnsLogEnabled
			{
				set
				{
					base.PowerSharpParameters["DnsLogEnabled"] = value;
				}
			}

			// Token: 0x170034E8 RID: 13544
			// (set) Token: 0x06005611 RID: 22033 RVA: 0x00087307 File Offset: 0x00085507
			public virtual EnhancedTimeSpan JournalLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxAge"] = value;
				}
			}

			// Token: 0x170034E9 RID: 13545
			// (set) Token: 0x06005612 RID: 22034 RVA: 0x0008731F File Offset: 0x0008551F
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034EA RID: 13546
			// (set) Token: 0x06005613 RID: 22035 RVA: 0x00087337 File Offset: 0x00085537
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034EB RID: 13547
			// (set) Token: 0x06005614 RID: 22036 RVA: 0x0008734F File Offset: 0x0008554F
			public virtual LocalLongFullPath JournalLogPath
			{
				set
				{
					base.PowerSharpParameters["JournalLogPath"] = value;
				}
			}

			// Token: 0x170034EC RID: 13548
			// (set) Token: 0x06005615 RID: 22037 RVA: 0x00087362 File Offset: 0x00085562
			public virtual bool JournalLogEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalLogEnabled"] = value;
				}
			}

			// Token: 0x170034ED RID: 13549
			// (set) Token: 0x06005616 RID: 22038 RVA: 0x0008737A File Offset: 0x0008557A
			public virtual EnhancedTimeSpan TransportMaintenanceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxAge"] = value;
				}
			}

			// Token: 0x170034EE RID: 13550
			// (set) Token: 0x06005617 RID: 22039 RVA: 0x00087392 File Offset: 0x00085592
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034EF RID: 13551
			// (set) Token: 0x06005618 RID: 22040 RVA: 0x000873AA File Offset: 0x000855AA
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034F0 RID: 13552
			// (set) Token: 0x06005619 RID: 22041 RVA: 0x000873C2 File Offset: 0x000855C2
			public virtual LocalLongFullPath TransportMaintenanceLogPath
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogPath"] = value;
				}
			}

			// Token: 0x170034F1 RID: 13553
			// (set) Token: 0x0600561A RID: 22042 RVA: 0x000873D5 File Offset: 0x000855D5
			public virtual bool TransportMaintenanceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogEnabled"] = value;
				}
			}

			// Token: 0x170034F2 RID: 13554
			// (set) Token: 0x0600561B RID: 22043 RVA: 0x000873ED File Offset: 0x000855ED
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170034F3 RID: 13555
			// (set) Token: 0x0600561C RID: 22044 RVA: 0x00087400 File Offset: 0x00085600
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x170034F4 RID: 13556
			// (set) Token: 0x0600561D RID: 22045 RVA: 0x00087418 File Offset: 0x00085618
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x170034F5 RID: 13557
			// (set) Token: 0x0600561E RID: 22046 RVA: 0x00087430 File Offset: 0x00085630
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x170034F6 RID: 13558
			// (set) Token: 0x0600561F RID: 22047 RVA: 0x00087448 File Offset: 0x00085648
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170034F7 RID: 13559
			// (set) Token: 0x06005620 RID: 22048 RVA: 0x00087460 File Offset: 0x00085660
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170034F8 RID: 13560
			// (set) Token: 0x06005621 RID: 22049 RVA: 0x00087478 File Offset: 0x00085678
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x170034F9 RID: 13561
			// (set) Token: 0x06005622 RID: 22050 RVA: 0x0008748B File Offset: 0x0008568B
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x170034FA RID: 13562
			// (set) Token: 0x06005623 RID: 22051 RVA: 0x000874A3 File Offset: 0x000856A3
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x170034FB RID: 13563
			// (set) Token: 0x06005624 RID: 22052 RVA: 0x000874BB File Offset: 0x000856BB
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170034FC RID: 13564
			// (set) Token: 0x06005625 RID: 22053 RVA: 0x000874D3 File Offset: 0x000856D3
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170034FD RID: 13565
			// (set) Token: 0x06005626 RID: 22054 RVA: 0x000874EB File Offset: 0x000856EB
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x170034FE RID: 13566
			// (set) Token: 0x06005627 RID: 22055 RVA: 0x000874FE File Offset: 0x000856FE
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x170034FF RID: 13567
			// (set) Token: 0x06005628 RID: 22056 RVA: 0x00087511 File Offset: 0x00085711
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x17003500 RID: 13568
			// (set) Token: 0x06005629 RID: 22057 RVA: 0x00087529 File Offset: 0x00085729
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x17003501 RID: 13569
			// (set) Token: 0x0600562A RID: 22058 RVA: 0x00087541 File Offset: 0x00085741
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x17003502 RID: 13570
			// (set) Token: 0x0600562B RID: 22059 RVA: 0x00087559 File Offset: 0x00085759
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x17003503 RID: 13571
			// (set) Token: 0x0600562C RID: 22060 RVA: 0x0008756C File Offset: 0x0008576C
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x17003504 RID: 13572
			// (set) Token: 0x0600562D RID: 22061 RVA: 0x00087584 File Offset: 0x00085784
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x17003505 RID: 13573
			// (set) Token: 0x0600562E RID: 22062 RVA: 0x0008759C File Offset: 0x0008579C
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x17003506 RID: 13574
			// (set) Token: 0x0600562F RID: 22063 RVA: 0x000875B4 File Offset: 0x000857B4
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x17003507 RID: 13575
			// (set) Token: 0x06005630 RID: 22064 RVA: 0x000875CC File Offset: 0x000857CC
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x17003508 RID: 13576
			// (set) Token: 0x06005631 RID: 22065 RVA: 0x000875E4 File Offset: 0x000857E4
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x17003509 RID: 13577
			// (set) Token: 0x06005632 RID: 22066 RVA: 0x000875FC File Offset: 0x000857FC
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x1700350A RID: 13578
			// (set) Token: 0x06005633 RID: 22067 RVA: 0x00087614 File Offset: 0x00085814
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x1700350B RID: 13579
			// (set) Token: 0x06005634 RID: 22068 RVA: 0x0008762C File Offset: 0x0008582C
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x1700350C RID: 13580
			// (set) Token: 0x06005635 RID: 22069 RVA: 0x00087644 File Offset: 0x00085844
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700350D RID: 13581
			// (set) Token: 0x06005636 RID: 22070 RVA: 0x0008765C File Offset: 0x0008585C
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700350E RID: 13582
			// (set) Token: 0x06005637 RID: 22071 RVA: 0x00087674 File Offset: 0x00085874
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x1700350F RID: 13583
			// (set) Token: 0x06005638 RID: 22072 RVA: 0x00087687 File Offset: 0x00085887
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x17003510 RID: 13584
			// (set) Token: 0x06005639 RID: 22073 RVA: 0x0008769F File Offset: 0x0008589F
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x17003511 RID: 13585
			// (set) Token: 0x0600563A RID: 22074 RVA: 0x000876B7 File Offset: 0x000858B7
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003512 RID: 13586
			// (set) Token: 0x0600563B RID: 22075 RVA: 0x000876CF File Offset: 0x000858CF
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003513 RID: 13587
			// (set) Token: 0x0600563C RID: 22076 RVA: 0x000876E7 File Offset: 0x000858E7
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x17003514 RID: 13588
			// (set) Token: 0x0600563D RID: 22077 RVA: 0x000876FA File Offset: 0x000858FA
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003515 RID: 13589
			// (set) Token: 0x0600563E RID: 22078 RVA: 0x00087712 File Offset: 0x00085912
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003516 RID: 13590
			// (set) Token: 0x0600563F RID: 22079 RVA: 0x0008772A File Offset: 0x0008592A
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003517 RID: 13591
			// (set) Token: 0x06005640 RID: 22080 RVA: 0x00087742 File Offset: 0x00085942
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x17003518 RID: 13592
			// (set) Token: 0x06005641 RID: 22081 RVA: 0x00087755 File Offset: 0x00085955
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003519 RID: 13593
			// (set) Token: 0x06005642 RID: 22082 RVA: 0x0008776D File Offset: 0x0008596D
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700351A RID: 13594
			// (set) Token: 0x06005643 RID: 22083 RVA: 0x00087785 File Offset: 0x00085985
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700351B RID: 13595
			// (set) Token: 0x06005644 RID: 22084 RVA: 0x0008779D File Offset: 0x0008599D
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x1700351C RID: 13596
			// (set) Token: 0x06005645 RID: 22085 RVA: 0x000877B0 File Offset: 0x000859B0
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x1700351D RID: 13597
			// (set) Token: 0x06005646 RID: 22086 RVA: 0x000877C8 File Offset: 0x000859C8
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x1700351E RID: 13598
			// (set) Token: 0x06005647 RID: 22087 RVA: 0x000877E0 File Offset: 0x000859E0
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700351F RID: 13599
			// (set) Token: 0x06005648 RID: 22088 RVA: 0x000877F8 File Offset: 0x000859F8
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x17003520 RID: 13600
			// (set) Token: 0x06005649 RID: 22089 RVA: 0x00087810 File Offset: 0x00085A10
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x17003521 RID: 13601
			// (set) Token: 0x0600564A RID: 22090 RVA: 0x00087828 File Offset: 0x00085A28
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17003522 RID: 13602
			// (set) Token: 0x0600564B RID: 22091 RVA: 0x00087840 File Offset: 0x00085A40
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x17003523 RID: 13603
			// (set) Token: 0x0600564C RID: 22092 RVA: 0x00087853 File Offset: 0x00085A53
			public virtual bool PipelineTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingEnabled"] = value;
				}
			}

			// Token: 0x17003524 RID: 13604
			// (set) Token: 0x0600564D RID: 22093 RVA: 0x0008786B File Offset: 0x00085A6B
			public virtual bool ContentConversionTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["ContentConversionTracingEnabled"] = value;
				}
			}

			// Token: 0x17003525 RID: 13605
			// (set) Token: 0x0600564E RID: 22094 RVA: 0x00087883 File Offset: 0x00085A83
			public virtual LocalLongFullPath PipelineTracingPath
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingPath"] = value;
				}
			}

			// Token: 0x17003526 RID: 13606
			// (set) Token: 0x0600564F RID: 22095 RVA: 0x00087896 File Offset: 0x00085A96
			public virtual SmtpAddress? PipelineTracingSenderAddress
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingSenderAddress"] = value;
				}
			}

			// Token: 0x17003527 RID: 13607
			// (set) Token: 0x06005650 RID: 22096 RVA: 0x000878AE File Offset: 0x00085AAE
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003528 RID: 13608
			// (set) Token: 0x06005651 RID: 22097 RVA: 0x000878C6 File Offset: 0x00085AC6
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x17003529 RID: 13609
			// (set) Token: 0x06005652 RID: 22098 RVA: 0x000878DE File Offset: 0x00085ADE
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x1700352A RID: 13610
			// (set) Token: 0x06005653 RID: 22099 RVA: 0x000878F6 File Offset: 0x00085AF6
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x1700352B RID: 13611
			// (set) Token: 0x06005654 RID: 22100 RVA: 0x0008790E File Offset: 0x00085B0E
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700352C RID: 13612
			// (set) Token: 0x06005655 RID: 22101 RVA: 0x00087926 File Offset: 0x00085B26
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700352D RID: 13613
			// (set) Token: 0x06005656 RID: 22102 RVA: 0x0008793E File Offset: 0x00085B3E
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x1700352E RID: 13614
			// (set) Token: 0x06005657 RID: 22103 RVA: 0x00087951 File Offset: 0x00085B51
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x1700352F RID: 13615
			// (set) Token: 0x06005658 RID: 22104 RVA: 0x00087969 File Offset: 0x00085B69
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x17003530 RID: 13616
			// (set) Token: 0x06005659 RID: 22105 RVA: 0x0008797C File Offset: 0x00085B7C
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x17003531 RID: 13617
			// (set) Token: 0x0600565A RID: 22106 RVA: 0x0008798F File Offset: 0x00085B8F
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x17003532 RID: 13618
			// (set) Token: 0x0600565B RID: 22107 RVA: 0x000879A7 File Offset: 0x00085BA7
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003533 RID: 13619
			// (set) Token: 0x0600565C RID: 22108 RVA: 0x000879BF File Offset: 0x00085BBF
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x17003534 RID: 13620
			// (set) Token: 0x0600565D RID: 22109 RVA: 0x000879D2 File Offset: 0x00085BD2
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003535 RID: 13621
			// (set) Token: 0x0600565E RID: 22110 RVA: 0x000879EA File Offset: 0x00085BEA
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003536 RID: 13622
			// (set) Token: 0x0600565F RID: 22111 RVA: 0x00087A02 File Offset: 0x00085C02
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003537 RID: 13623
			// (set) Token: 0x06005660 RID: 22112 RVA: 0x00087A1A File Offset: 0x00085C1A
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003538 RID: 13624
			// (set) Token: 0x06005661 RID: 22113 RVA: 0x00087A2D File Offset: 0x00085C2D
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x17003539 RID: 13625
			// (set) Token: 0x06005662 RID: 22114 RVA: 0x00087A45 File Offset: 0x00085C45
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x1700353A RID: 13626
			// (set) Token: 0x06005663 RID: 22115 RVA: 0x00087A5D File Offset: 0x00085C5D
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700353B RID: 13627
			// (set) Token: 0x06005664 RID: 22116 RVA: 0x00087A75 File Offset: 0x00085C75
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x1700353C RID: 13628
			// (set) Token: 0x06005665 RID: 22117 RVA: 0x00087A8D File Offset: 0x00085C8D
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700353D RID: 13629
			// (set) Token: 0x06005666 RID: 22118 RVA: 0x00087AA5 File Offset: 0x00085CA5
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x1700353E RID: 13630
			// (set) Token: 0x06005667 RID: 22119 RVA: 0x00087ABD File Offset: 0x00085CBD
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x1700353F RID: 13631
			// (set) Token: 0x06005668 RID: 22120 RVA: 0x00087AD5 File Offset: 0x00085CD5
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x17003540 RID: 13632
			// (set) Token: 0x06005669 RID: 22121 RVA: 0x00087AED File Offset: 0x00085CED
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x17003541 RID: 13633
			// (set) Token: 0x0600566A RID: 22122 RVA: 0x00087B05 File Offset: 0x00085D05
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x17003542 RID: 13634
			// (set) Token: 0x0600566B RID: 22123 RVA: 0x00087B18 File Offset: 0x00085D18
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x17003543 RID: 13635
			// (set) Token: 0x0600566C RID: 22124 RVA: 0x00087B30 File Offset: 0x00085D30
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x17003544 RID: 13636
			// (set) Token: 0x0600566D RID: 22125 RVA: 0x00087B43 File Offset: 0x00085D43
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003545 RID: 13637
			// (set) Token: 0x0600566E RID: 22126 RVA: 0x00087B5B File Offset: 0x00085D5B
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003546 RID: 13638
			// (set) Token: 0x0600566F RID: 22127 RVA: 0x00087B73 File Offset: 0x00085D73
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003547 RID: 13639
			// (set) Token: 0x06005670 RID: 22128 RVA: 0x00087B8B File Offset: 0x00085D8B
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003548 RID: 13640
			// (set) Token: 0x06005671 RID: 22129 RVA: 0x00087BA3 File Offset: 0x00085DA3
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x17003549 RID: 13641
			// (set) Token: 0x06005672 RID: 22130 RVA: 0x00087BBB File Offset: 0x00085DBB
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x1700354A RID: 13642
			// (set) Token: 0x06005673 RID: 22131 RVA: 0x00087BCE File Offset: 0x00085DCE
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x1700354B RID: 13643
			// (set) Token: 0x06005674 RID: 22132 RVA: 0x00087BE6 File Offset: 0x00085DE6
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x1700354C RID: 13644
			// (set) Token: 0x06005675 RID: 22133 RVA: 0x00087BFE File Offset: 0x00085DFE
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700354D RID: 13645
			// (set) Token: 0x06005676 RID: 22134 RVA: 0x00087C16 File Offset: 0x00085E16
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700354E RID: 13646
			// (set) Token: 0x06005677 RID: 22135 RVA: 0x00087C2E File Offset: 0x00085E2E
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x1700354F RID: 13647
			// (set) Token: 0x06005678 RID: 22136 RVA: 0x00087C46 File Offset: 0x00085E46
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x17003550 RID: 13648
			// (set) Token: 0x06005679 RID: 22137 RVA: 0x00087C59 File Offset: 0x00085E59
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x17003551 RID: 13649
			// (set) Token: 0x0600567A RID: 22138 RVA: 0x00087C71 File Offset: 0x00085E71
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003552 RID: 13650
			// (set) Token: 0x0600567B RID: 22139 RVA: 0x00087C89 File Offset: 0x00085E89
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003553 RID: 13651
			// (set) Token: 0x0600567C RID: 22140 RVA: 0x00087CA1 File Offset: 0x00085EA1
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003554 RID: 13652
			// (set) Token: 0x0600567D RID: 22141 RVA: 0x00087CB9 File Offset: 0x00085EB9
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x17003555 RID: 13653
			// (set) Token: 0x0600567E RID: 22142 RVA: 0x00087CD1 File Offset: 0x00085ED1
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x17003556 RID: 13654
			// (set) Token: 0x0600567F RID: 22143 RVA: 0x00087CE9 File Offset: 0x00085EE9
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x17003557 RID: 13655
			// (set) Token: 0x06005680 RID: 22144 RVA: 0x00087D01 File Offset: 0x00085F01
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x17003558 RID: 13656
			// (set) Token: 0x06005681 RID: 22145 RVA: 0x00087D19 File Offset: 0x00085F19
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x17003559 RID: 13657
			// (set) Token: 0x06005682 RID: 22146 RVA: 0x00087D31 File Offset: 0x00085F31
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x1700355A RID: 13658
			// (set) Token: 0x06005683 RID: 22147 RVA: 0x00087D49 File Offset: 0x00085F49
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x1700355B RID: 13659
			// (set) Token: 0x06005684 RID: 22148 RVA: 0x00087D61 File Offset: 0x00085F61
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x1700355C RID: 13660
			// (set) Token: 0x06005685 RID: 22149 RVA: 0x00087D74 File Offset: 0x00085F74
			public virtual bool UseDowngradedExchangeServerAuth
			{
				set
				{
					base.PowerSharpParameters["UseDowngradedExchangeServerAuth"] = value;
				}
			}

			// Token: 0x1700355D RID: 13661
			// (set) Token: 0x06005686 RID: 22150 RVA: 0x00087D8C File Offset: 0x00085F8C
			public virtual int IntraOrgConnectorSmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorSmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x1700355E RID: 13662
			// (set) Token: 0x06005687 RID: 22151 RVA: 0x00087DA4 File Offset: 0x00085FA4
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x1700355F RID: 13663
			// (set) Token: 0x06005688 RID: 22152 RVA: 0x00087DBC File Offset: 0x00085FBC
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x17003560 RID: 13664
			// (set) Token: 0x06005689 RID: 22153 RVA: 0x00087DD4 File Offset: 0x00085FD4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003561 RID: 13665
			// (set) Token: 0x0600568A RID: 22154 RVA: 0x00087DEC File Offset: 0x00085FEC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003562 RID: 13666
			// (set) Token: 0x0600568B RID: 22155 RVA: 0x00087E04 File Offset: 0x00086004
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003563 RID: 13667
			// (set) Token: 0x0600568C RID: 22156 RVA: 0x00087E1C File Offset: 0x0008601C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003564 RID: 13668
			// (set) Token: 0x0600568D RID: 22157 RVA: 0x00087E34 File Offset: 0x00086034
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000671 RID: 1649
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003565 RID: 13669
			// (set) Token: 0x0600568F RID: 22159 RVA: 0x00087E54 File Offset: 0x00086054
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003566 RID: 13670
			// (set) Token: 0x06005690 RID: 22160 RVA: 0x00087E67 File Offset: 0x00086067
			public virtual EnhancedTimeSpan QueueLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxAge"] = value;
				}
			}

			// Token: 0x17003567 RID: 13671
			// (set) Token: 0x06005691 RID: 22161 RVA: 0x00087E7F File Offset: 0x0008607F
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003568 RID: 13672
			// (set) Token: 0x06005692 RID: 22162 RVA: 0x00087E97 File Offset: 0x00086097
			public virtual Unlimited<ByteQuantifiedSize> QueueLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["QueueLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003569 RID: 13673
			// (set) Token: 0x06005693 RID: 22163 RVA: 0x00087EAF File Offset: 0x000860AF
			public virtual LocalLongFullPath QueueLogPath
			{
				set
				{
					base.PowerSharpParameters["QueueLogPath"] = value;
				}
			}

			// Token: 0x1700356A RID: 13674
			// (set) Token: 0x06005694 RID: 22164 RVA: 0x00087EC2 File Offset: 0x000860C2
			public virtual EnhancedTimeSpan WlmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxAge"] = value;
				}
			}

			// Token: 0x1700356B RID: 13675
			// (set) Token: 0x06005695 RID: 22165 RVA: 0x00087EDA File Offset: 0x000860DA
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700356C RID: 13676
			// (set) Token: 0x06005696 RID: 22166 RVA: 0x00087EF2 File Offset: 0x000860F2
			public virtual Unlimited<ByteQuantifiedSize> WlmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["WlmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700356D RID: 13677
			// (set) Token: 0x06005697 RID: 22167 RVA: 0x00087F0A File Offset: 0x0008610A
			public virtual LocalLongFullPath WlmLogPath
			{
				set
				{
					base.PowerSharpParameters["WlmLogPath"] = value;
				}
			}

			// Token: 0x1700356E RID: 13678
			// (set) Token: 0x06005698 RID: 22168 RVA: 0x00087F1D File Offset: 0x0008611D
			public virtual EnhancedTimeSpan AgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxAge"] = value;
				}
			}

			// Token: 0x1700356F RID: 13679
			// (set) Token: 0x06005699 RID: 22169 RVA: 0x00087F35 File Offset: 0x00086135
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003570 RID: 13680
			// (set) Token: 0x0600569A RID: 22170 RVA: 0x00087F4D File Offset: 0x0008614D
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003571 RID: 13681
			// (set) Token: 0x0600569B RID: 22171 RVA: 0x00087F65 File Offset: 0x00086165
			public virtual LocalLongFullPath AgentLogPath
			{
				set
				{
					base.PowerSharpParameters["AgentLogPath"] = value;
				}
			}

			// Token: 0x17003572 RID: 13682
			// (set) Token: 0x0600569C RID: 22172 RVA: 0x00087F78 File Offset: 0x00086178
			public virtual bool AgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AgentLogEnabled"] = value;
				}
			}

			// Token: 0x17003573 RID: 13683
			// (set) Token: 0x0600569D RID: 22173 RVA: 0x00087F90 File Offset: 0x00086190
			public virtual EnhancedTimeSpan FlowControlLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxAge"] = value;
				}
			}

			// Token: 0x17003574 RID: 13684
			// (set) Token: 0x0600569E RID: 22174 RVA: 0x00087FA8 File Offset: 0x000861A8
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003575 RID: 13685
			// (set) Token: 0x0600569F RID: 22175 RVA: 0x00087FC0 File Offset: 0x000861C0
			public virtual Unlimited<ByteQuantifiedSize> FlowControlLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003576 RID: 13686
			// (set) Token: 0x060056A0 RID: 22176 RVA: 0x00087FD8 File Offset: 0x000861D8
			public virtual LocalLongFullPath FlowControlLogPath
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogPath"] = value;
				}
			}

			// Token: 0x17003577 RID: 13687
			// (set) Token: 0x060056A1 RID: 22177 RVA: 0x00087FEB File Offset: 0x000861EB
			public virtual bool FlowControlLogEnabled
			{
				set
				{
					base.PowerSharpParameters["FlowControlLogEnabled"] = value;
				}
			}

			// Token: 0x17003578 RID: 13688
			// (set) Token: 0x060056A2 RID: 22178 RVA: 0x00088003 File Offset: 0x00086203
			public virtual EnhancedTimeSpan ProcessingSchedulerLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxAge"] = value;
				}
			}

			// Token: 0x17003579 RID: 13689
			// (set) Token: 0x060056A3 RID: 22179 RVA: 0x0008801B File Offset: 0x0008621B
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700357A RID: 13690
			// (set) Token: 0x060056A4 RID: 22180 RVA: 0x00088033 File Offset: 0x00086233
			public virtual Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700357B RID: 13691
			// (set) Token: 0x060056A5 RID: 22181 RVA: 0x0008804B File Offset: 0x0008624B
			public virtual LocalLongFullPath ProcessingSchedulerLogPath
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogPath"] = value;
				}
			}

			// Token: 0x1700357C RID: 13692
			// (set) Token: 0x060056A6 RID: 22182 RVA: 0x0008805E File Offset: 0x0008625E
			public virtual bool ProcessingSchedulerLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ProcessingSchedulerLogEnabled"] = value;
				}
			}

			// Token: 0x1700357D RID: 13693
			// (set) Token: 0x060056A7 RID: 22183 RVA: 0x00088076 File Offset: 0x00086276
			public virtual EnhancedTimeSpan ResourceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxAge"] = value;
				}
			}

			// Token: 0x1700357E RID: 13694
			// (set) Token: 0x060056A8 RID: 22184 RVA: 0x0008808E File Offset: 0x0008628E
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700357F RID: 13695
			// (set) Token: 0x060056A9 RID: 22185 RVA: 0x000880A6 File Offset: 0x000862A6
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003580 RID: 13696
			// (set) Token: 0x060056AA RID: 22186 RVA: 0x000880BE File Offset: 0x000862BE
			public virtual LocalLongFullPath ResourceLogPath
			{
				set
				{
					base.PowerSharpParameters["ResourceLogPath"] = value;
				}
			}

			// Token: 0x17003581 RID: 13697
			// (set) Token: 0x060056AB RID: 22187 RVA: 0x000880D1 File Offset: 0x000862D1
			public virtual bool ResourceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ResourceLogEnabled"] = value;
				}
			}

			// Token: 0x17003582 RID: 13698
			// (set) Token: 0x060056AC RID: 22188 RVA: 0x000880E9 File Offset: 0x000862E9
			public virtual EnhancedTimeSpan DnsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003583 RID: 13699
			// (set) Token: 0x060056AD RID: 22189 RVA: 0x00088101 File Offset: 0x00086301
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003584 RID: 13700
			// (set) Token: 0x060056AE RID: 22190 RVA: 0x00088119 File Offset: 0x00086319
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003585 RID: 13701
			// (set) Token: 0x060056AF RID: 22191 RVA: 0x00088131 File Offset: 0x00086331
			public virtual LocalLongFullPath DnsLogPath
			{
				set
				{
					base.PowerSharpParameters["DnsLogPath"] = value;
				}
			}

			// Token: 0x17003586 RID: 13702
			// (set) Token: 0x060056B0 RID: 22192 RVA: 0x00088144 File Offset: 0x00086344
			public virtual bool DnsLogEnabled
			{
				set
				{
					base.PowerSharpParameters["DnsLogEnabled"] = value;
				}
			}

			// Token: 0x17003587 RID: 13703
			// (set) Token: 0x060056B1 RID: 22193 RVA: 0x0008815C File Offset: 0x0008635C
			public virtual EnhancedTimeSpan JournalLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxAge"] = value;
				}
			}

			// Token: 0x17003588 RID: 13704
			// (set) Token: 0x060056B2 RID: 22194 RVA: 0x00088174 File Offset: 0x00086374
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003589 RID: 13705
			// (set) Token: 0x060056B3 RID: 22195 RVA: 0x0008818C File Offset: 0x0008638C
			public virtual Unlimited<ByteQuantifiedSize> JournalLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["JournalLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700358A RID: 13706
			// (set) Token: 0x060056B4 RID: 22196 RVA: 0x000881A4 File Offset: 0x000863A4
			public virtual LocalLongFullPath JournalLogPath
			{
				set
				{
					base.PowerSharpParameters["JournalLogPath"] = value;
				}
			}

			// Token: 0x1700358B RID: 13707
			// (set) Token: 0x060056B5 RID: 22197 RVA: 0x000881B7 File Offset: 0x000863B7
			public virtual bool JournalLogEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalLogEnabled"] = value;
				}
			}

			// Token: 0x1700358C RID: 13708
			// (set) Token: 0x060056B6 RID: 22198 RVA: 0x000881CF File Offset: 0x000863CF
			public virtual EnhancedTimeSpan TransportMaintenanceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxAge"] = value;
				}
			}

			// Token: 0x1700358D RID: 13709
			// (set) Token: 0x060056B7 RID: 22199 RVA: 0x000881E7 File Offset: 0x000863E7
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700358E RID: 13710
			// (set) Token: 0x060056B8 RID: 22200 RVA: 0x000881FF File Offset: 0x000863FF
			public virtual Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700358F RID: 13711
			// (set) Token: 0x060056B9 RID: 22201 RVA: 0x00088217 File Offset: 0x00086417
			public virtual LocalLongFullPath TransportMaintenanceLogPath
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogPath"] = value;
				}
			}

			// Token: 0x17003590 RID: 13712
			// (set) Token: 0x060056BA RID: 22202 RVA: 0x0008822A File Offset: 0x0008642A
			public virtual bool TransportMaintenanceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportMaintenanceLogEnabled"] = value;
				}
			}

			// Token: 0x17003591 RID: 13713
			// (set) Token: 0x060056BB RID: 22203 RVA: 0x00088242 File Offset: 0x00086442
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003592 RID: 13714
			// (set) Token: 0x060056BC RID: 22204 RVA: 0x00088255 File Offset: 0x00086455
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x17003593 RID: 13715
			// (set) Token: 0x060056BD RID: 22205 RVA: 0x0008826D File Offset: 0x0008646D
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x17003594 RID: 13716
			// (set) Token: 0x060056BE RID: 22206 RVA: 0x00088285 File Offset: 0x00086485
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x17003595 RID: 13717
			// (set) Token: 0x060056BF RID: 22207 RVA: 0x0008829D File Offset: 0x0008649D
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003596 RID: 13718
			// (set) Token: 0x060056C0 RID: 22208 RVA: 0x000882B5 File Offset: 0x000864B5
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003597 RID: 13719
			// (set) Token: 0x060056C1 RID: 22209 RVA: 0x000882CD File Offset: 0x000864CD
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x17003598 RID: 13720
			// (set) Token: 0x060056C2 RID: 22210 RVA: 0x000882E0 File Offset: 0x000864E0
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x17003599 RID: 13721
			// (set) Token: 0x060056C3 RID: 22211 RVA: 0x000882F8 File Offset: 0x000864F8
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700359A RID: 13722
			// (set) Token: 0x060056C4 RID: 22212 RVA: 0x00088310 File Offset: 0x00086510
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x1700359B RID: 13723
			// (set) Token: 0x060056C5 RID: 22213 RVA: 0x00088328 File Offset: 0x00086528
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x1700359C RID: 13724
			// (set) Token: 0x060056C6 RID: 22214 RVA: 0x00088340 File Offset: 0x00086540
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x1700359D RID: 13725
			// (set) Token: 0x060056C7 RID: 22215 RVA: 0x00088353 File Offset: 0x00086553
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x1700359E RID: 13726
			// (set) Token: 0x060056C8 RID: 22216 RVA: 0x00088366 File Offset: 0x00086566
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700359F RID: 13727
			// (set) Token: 0x060056C9 RID: 22217 RVA: 0x0008837E File Offset: 0x0008657E
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170035A0 RID: 13728
			// (set) Token: 0x060056CA RID: 22218 RVA: 0x00088396 File Offset: 0x00086596
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170035A1 RID: 13729
			// (set) Token: 0x060056CB RID: 22219 RVA: 0x000883AE File Offset: 0x000865AE
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x170035A2 RID: 13730
			// (set) Token: 0x060056CC RID: 22220 RVA: 0x000883C1 File Offset: 0x000865C1
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x170035A3 RID: 13731
			// (set) Token: 0x060056CD RID: 22221 RVA: 0x000883D9 File Offset: 0x000865D9
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x170035A4 RID: 13732
			// (set) Token: 0x060056CE RID: 22222 RVA: 0x000883F1 File Offset: 0x000865F1
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x170035A5 RID: 13733
			// (set) Token: 0x060056CF RID: 22223 RVA: 0x00088409 File Offset: 0x00086609
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x170035A6 RID: 13734
			// (set) Token: 0x060056D0 RID: 22224 RVA: 0x00088421 File Offset: 0x00086621
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x170035A7 RID: 13735
			// (set) Token: 0x060056D1 RID: 22225 RVA: 0x00088439 File Offset: 0x00086639
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x170035A8 RID: 13736
			// (set) Token: 0x060056D2 RID: 22226 RVA: 0x00088451 File Offset: 0x00086651
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x170035A9 RID: 13737
			// (set) Token: 0x060056D3 RID: 22227 RVA: 0x00088469 File Offset: 0x00086669
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x170035AA RID: 13738
			// (set) Token: 0x060056D4 RID: 22228 RVA: 0x00088481 File Offset: 0x00086681
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x170035AB RID: 13739
			// (set) Token: 0x060056D5 RID: 22229 RVA: 0x00088499 File Offset: 0x00086699
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035AC RID: 13740
			// (set) Token: 0x060056D6 RID: 22230 RVA: 0x000884B1 File Offset: 0x000866B1
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035AD RID: 13741
			// (set) Token: 0x060056D7 RID: 22231 RVA: 0x000884C9 File Offset: 0x000866C9
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x170035AE RID: 13742
			// (set) Token: 0x060056D8 RID: 22232 RVA: 0x000884DC File Offset: 0x000866DC
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x170035AF RID: 13743
			// (set) Token: 0x060056D9 RID: 22233 RVA: 0x000884F4 File Offset: 0x000866F4
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x170035B0 RID: 13744
			// (set) Token: 0x060056DA RID: 22234 RVA: 0x0008850C File Offset: 0x0008670C
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035B1 RID: 13745
			// (set) Token: 0x060056DB RID: 22235 RVA: 0x00088524 File Offset: 0x00086724
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035B2 RID: 13746
			// (set) Token: 0x060056DC RID: 22236 RVA: 0x0008853C File Offset: 0x0008673C
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x170035B3 RID: 13747
			// (set) Token: 0x060056DD RID: 22237 RVA: 0x0008854F File Offset: 0x0008674F
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x170035B4 RID: 13748
			// (set) Token: 0x060056DE RID: 22238 RVA: 0x00088567 File Offset: 0x00086767
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035B5 RID: 13749
			// (set) Token: 0x060056DF RID: 22239 RVA: 0x0008857F File Offset: 0x0008677F
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035B6 RID: 13750
			// (set) Token: 0x060056E0 RID: 22240 RVA: 0x00088597 File Offset: 0x00086797
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x170035B7 RID: 13751
			// (set) Token: 0x060056E1 RID: 22241 RVA: 0x000885AA File Offset: 0x000867AA
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x170035B8 RID: 13752
			// (set) Token: 0x060056E2 RID: 22242 RVA: 0x000885C2 File Offset: 0x000867C2
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035B9 RID: 13753
			// (set) Token: 0x060056E3 RID: 22243 RVA: 0x000885DA File Offset: 0x000867DA
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035BA RID: 13754
			// (set) Token: 0x060056E4 RID: 22244 RVA: 0x000885F2 File Offset: 0x000867F2
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x170035BB RID: 13755
			// (set) Token: 0x060056E5 RID: 22245 RVA: 0x00088605 File Offset: 0x00086805
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x170035BC RID: 13756
			// (set) Token: 0x060056E6 RID: 22246 RVA: 0x0008861D File Offset: 0x0008681D
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x170035BD RID: 13757
			// (set) Token: 0x060056E7 RID: 22247 RVA: 0x00088635 File Offset: 0x00086835
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170035BE RID: 13758
			// (set) Token: 0x060056E8 RID: 22248 RVA: 0x0008864D File Offset: 0x0008684D
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x170035BF RID: 13759
			// (set) Token: 0x060056E9 RID: 22249 RVA: 0x00088665 File Offset: 0x00086865
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x170035C0 RID: 13760
			// (set) Token: 0x060056EA RID: 22250 RVA: 0x0008867D File Offset: 0x0008687D
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x170035C1 RID: 13761
			// (set) Token: 0x060056EB RID: 22251 RVA: 0x00088695 File Offset: 0x00086895
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x170035C2 RID: 13762
			// (set) Token: 0x060056EC RID: 22252 RVA: 0x000886A8 File Offset: 0x000868A8
			public virtual bool PipelineTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingEnabled"] = value;
				}
			}

			// Token: 0x170035C3 RID: 13763
			// (set) Token: 0x060056ED RID: 22253 RVA: 0x000886C0 File Offset: 0x000868C0
			public virtual bool ContentConversionTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["ContentConversionTracingEnabled"] = value;
				}
			}

			// Token: 0x170035C4 RID: 13764
			// (set) Token: 0x060056EE RID: 22254 RVA: 0x000886D8 File Offset: 0x000868D8
			public virtual LocalLongFullPath PipelineTracingPath
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingPath"] = value;
				}
			}

			// Token: 0x170035C5 RID: 13765
			// (set) Token: 0x060056EF RID: 22255 RVA: 0x000886EB File Offset: 0x000868EB
			public virtual SmtpAddress? PipelineTracingSenderAddress
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingSenderAddress"] = value;
				}
			}

			// Token: 0x170035C6 RID: 13766
			// (set) Token: 0x060056F0 RID: 22256 RVA: 0x00088703 File Offset: 0x00086903
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x170035C7 RID: 13767
			// (set) Token: 0x060056F1 RID: 22257 RVA: 0x0008871B File Offset: 0x0008691B
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x170035C8 RID: 13768
			// (set) Token: 0x060056F2 RID: 22258 RVA: 0x00088733 File Offset: 0x00086933
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x170035C9 RID: 13769
			// (set) Token: 0x060056F3 RID: 22259 RVA: 0x0008874B File Offset: 0x0008694B
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170035CA RID: 13770
			// (set) Token: 0x060056F4 RID: 22260 RVA: 0x00088763 File Offset: 0x00086963
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035CB RID: 13771
			// (set) Token: 0x060056F5 RID: 22261 RVA: 0x0008877B File Offset: 0x0008697B
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035CC RID: 13772
			// (set) Token: 0x060056F6 RID: 22262 RVA: 0x00088793 File Offset: 0x00086993
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x170035CD RID: 13773
			// (set) Token: 0x060056F7 RID: 22263 RVA: 0x000887A6 File Offset: 0x000869A6
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x170035CE RID: 13774
			// (set) Token: 0x060056F8 RID: 22264 RVA: 0x000887BE File Offset: 0x000869BE
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x170035CF RID: 13775
			// (set) Token: 0x060056F9 RID: 22265 RVA: 0x000887D1 File Offset: 0x000869D1
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x170035D0 RID: 13776
			// (set) Token: 0x060056FA RID: 22266 RVA: 0x000887E4 File Offset: 0x000869E4
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x170035D1 RID: 13777
			// (set) Token: 0x060056FB RID: 22267 RVA: 0x000887FC File Offset: 0x000869FC
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035D2 RID: 13778
			// (set) Token: 0x060056FC RID: 22268 RVA: 0x00088814 File Offset: 0x00086A14
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x170035D3 RID: 13779
			// (set) Token: 0x060056FD RID: 22269 RVA: 0x00088827 File Offset: 0x00086A27
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170035D4 RID: 13780
			// (set) Token: 0x060056FE RID: 22270 RVA: 0x0008883F File Offset: 0x00086A3F
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035D5 RID: 13781
			// (set) Token: 0x060056FF RID: 22271 RVA: 0x00088857 File Offset: 0x00086A57
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035D6 RID: 13782
			// (set) Token: 0x06005700 RID: 22272 RVA: 0x0008886F File Offset: 0x00086A6F
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x170035D7 RID: 13783
			// (set) Token: 0x06005701 RID: 22273 RVA: 0x00088882 File Offset: 0x00086A82
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x170035D8 RID: 13784
			// (set) Token: 0x06005702 RID: 22274 RVA: 0x0008889A File Offset: 0x00086A9A
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x170035D9 RID: 13785
			// (set) Token: 0x06005703 RID: 22275 RVA: 0x000888B2 File Offset: 0x00086AB2
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x170035DA RID: 13786
			// (set) Token: 0x06005704 RID: 22276 RVA: 0x000888CA File Offset: 0x00086ACA
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x170035DB RID: 13787
			// (set) Token: 0x06005705 RID: 22277 RVA: 0x000888E2 File Offset: 0x00086AE2
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x170035DC RID: 13788
			// (set) Token: 0x06005706 RID: 22278 RVA: 0x000888FA File Offset: 0x00086AFA
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x170035DD RID: 13789
			// (set) Token: 0x06005707 RID: 22279 RVA: 0x00088912 File Offset: 0x00086B12
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x170035DE RID: 13790
			// (set) Token: 0x06005708 RID: 22280 RVA: 0x0008892A File Offset: 0x00086B2A
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x170035DF RID: 13791
			// (set) Token: 0x06005709 RID: 22281 RVA: 0x00088942 File Offset: 0x00086B42
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x170035E0 RID: 13792
			// (set) Token: 0x0600570A RID: 22282 RVA: 0x0008895A File Offset: 0x00086B5A
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x170035E1 RID: 13793
			// (set) Token: 0x0600570B RID: 22283 RVA: 0x0008896D File Offset: 0x00086B6D
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x170035E2 RID: 13794
			// (set) Token: 0x0600570C RID: 22284 RVA: 0x00088985 File Offset: 0x00086B85
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x170035E3 RID: 13795
			// (set) Token: 0x0600570D RID: 22285 RVA: 0x00088998 File Offset: 0x00086B98
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170035E4 RID: 13796
			// (set) Token: 0x0600570E RID: 22286 RVA: 0x000889B0 File Offset: 0x00086BB0
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035E5 RID: 13797
			// (set) Token: 0x0600570F RID: 22287 RVA: 0x000889C8 File Offset: 0x00086BC8
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035E6 RID: 13798
			// (set) Token: 0x06005710 RID: 22288 RVA: 0x000889E0 File Offset: 0x00086BE0
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170035E7 RID: 13799
			// (set) Token: 0x06005711 RID: 22289 RVA: 0x000889F8 File Offset: 0x00086BF8
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x170035E8 RID: 13800
			// (set) Token: 0x06005712 RID: 22290 RVA: 0x00088A10 File Offset: 0x00086C10
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x170035E9 RID: 13801
			// (set) Token: 0x06005713 RID: 22291 RVA: 0x00088A23 File Offset: 0x00086C23
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170035EA RID: 13802
			// (set) Token: 0x06005714 RID: 22292 RVA: 0x00088A3B File Offset: 0x00086C3B
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x170035EB RID: 13803
			// (set) Token: 0x06005715 RID: 22293 RVA: 0x00088A53 File Offset: 0x00086C53
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035EC RID: 13804
			// (set) Token: 0x06005716 RID: 22294 RVA: 0x00088A6B File Offset: 0x00086C6B
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035ED RID: 13805
			// (set) Token: 0x06005717 RID: 22295 RVA: 0x00088A83 File Offset: 0x00086C83
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x170035EE RID: 13806
			// (set) Token: 0x06005718 RID: 22296 RVA: 0x00088A9B File Offset: 0x00086C9B
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x170035EF RID: 13807
			// (set) Token: 0x06005719 RID: 22297 RVA: 0x00088AAE File Offset: 0x00086CAE
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x170035F0 RID: 13808
			// (set) Token: 0x0600571A RID: 22298 RVA: 0x00088AC6 File Offset: 0x00086CC6
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170035F1 RID: 13809
			// (set) Token: 0x0600571B RID: 22299 RVA: 0x00088ADE File Offset: 0x00086CDE
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170035F2 RID: 13810
			// (set) Token: 0x0600571C RID: 22300 RVA: 0x00088AF6 File Offset: 0x00086CF6
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x170035F3 RID: 13811
			// (set) Token: 0x0600571D RID: 22301 RVA: 0x00088B0E File Offset: 0x00086D0E
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x170035F4 RID: 13812
			// (set) Token: 0x0600571E RID: 22302 RVA: 0x00088B26 File Offset: 0x00086D26
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x170035F5 RID: 13813
			// (set) Token: 0x0600571F RID: 22303 RVA: 0x00088B3E File Offset: 0x00086D3E
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x170035F6 RID: 13814
			// (set) Token: 0x06005720 RID: 22304 RVA: 0x00088B56 File Offset: 0x00086D56
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x170035F7 RID: 13815
			// (set) Token: 0x06005721 RID: 22305 RVA: 0x00088B6E File Offset: 0x00086D6E
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x170035F8 RID: 13816
			// (set) Token: 0x06005722 RID: 22306 RVA: 0x00088B86 File Offset: 0x00086D86
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x170035F9 RID: 13817
			// (set) Token: 0x06005723 RID: 22307 RVA: 0x00088B9E File Offset: 0x00086D9E
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x170035FA RID: 13818
			// (set) Token: 0x06005724 RID: 22308 RVA: 0x00088BB6 File Offset: 0x00086DB6
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x170035FB RID: 13819
			// (set) Token: 0x06005725 RID: 22309 RVA: 0x00088BC9 File Offset: 0x00086DC9
			public virtual bool UseDowngradedExchangeServerAuth
			{
				set
				{
					base.PowerSharpParameters["UseDowngradedExchangeServerAuth"] = value;
				}
			}

			// Token: 0x170035FC RID: 13820
			// (set) Token: 0x06005726 RID: 22310 RVA: 0x00088BE1 File Offset: 0x00086DE1
			public virtual int IntraOrgConnectorSmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorSmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x170035FD RID: 13821
			// (set) Token: 0x06005727 RID: 22311 RVA: 0x00088BF9 File Offset: 0x00086DF9
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x170035FE RID: 13822
			// (set) Token: 0x06005728 RID: 22312 RVA: 0x00088C11 File Offset: 0x00086E11
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x170035FF RID: 13823
			// (set) Token: 0x06005729 RID: 22313 RVA: 0x00088C29 File Offset: 0x00086E29
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003600 RID: 13824
			// (set) Token: 0x0600572A RID: 22314 RVA: 0x00088C41 File Offset: 0x00086E41
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003601 RID: 13825
			// (set) Token: 0x0600572B RID: 22315 RVA: 0x00088C59 File Offset: 0x00086E59
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003602 RID: 13826
			// (set) Token: 0x0600572C RID: 22316 RVA: 0x00088C71 File Offset: 0x00086E71
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003603 RID: 13827
			// (set) Token: 0x0600572D RID: 22317 RVA: 0x00088C89 File Offset: 0x00086E89
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
