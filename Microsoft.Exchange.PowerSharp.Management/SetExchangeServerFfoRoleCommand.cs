using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200065A RID: 1626
	public class SetExchangeServerFfoRoleCommand : SyntheticCommandWithPipelineInputNoOutput<Server>
	{
		// Token: 0x060051B1 RID: 20913 RVA: 0x00080FFF File Offset: 0x0007F1FF
		private SetExchangeServerFfoRoleCommand() : base("Set-ExchangeServerFfoRole")
		{
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x0008100C File Offset: 0x0007F20C
		public SetExchangeServerFfoRoleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0008101B File Offset: 0x0007F21B
		public virtual SetExchangeServerFfoRoleCommand SetParameters(SetExchangeServerFfoRoleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x00081025 File Offset: 0x0007F225
		public virtual SetExchangeServerFfoRoleCommand SetParameters(SetExchangeServerFfoRoleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200065B RID: 1627
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170030B6 RID: 12470
			// (set) Token: 0x060051B5 RID: 20917 RVA: 0x0008102F File Offset: 0x0007F22F
			public virtual ServerRole ServerRole
			{
				set
				{
					base.PowerSharpParameters["ServerRole"] = value;
				}
			}

			// Token: 0x170030B7 RID: 12471
			// (set) Token: 0x060051B6 RID: 20918 RVA: 0x00081047 File Offset: 0x0007F247
			public virtual SwitchParameter Remove
			{
				set
				{
					base.PowerSharpParameters["Remove"] = value;
				}
			}

			// Token: 0x170030B8 RID: 12472
			// (set) Token: 0x060051B7 RID: 20919 RVA: 0x0008105F File Offset: 0x0007F25F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170030B9 RID: 12473
			// (set) Token: 0x060051B8 RID: 20920 RVA: 0x00081072 File Offset: 0x0007F272
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x170030BA RID: 12474
			// (set) Token: 0x060051B9 RID: 20921 RVA: 0x0008108A File Offset: 0x0007F28A
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x170030BB RID: 12475
			// (set) Token: 0x060051BA RID: 20922 RVA: 0x000810A2 File Offset: 0x0007F2A2
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x170030BC RID: 12476
			// (set) Token: 0x060051BB RID: 20923 RVA: 0x000810BA File Offset: 0x0007F2BA
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x170030BD RID: 12477
			// (set) Token: 0x060051BC RID: 20924 RVA: 0x000810D2 File Offset: 0x0007F2D2
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x170030BE RID: 12478
			// (set) Token: 0x060051BD RID: 20925 RVA: 0x000810EA File Offset: 0x0007F2EA
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x170030BF RID: 12479
			// (set) Token: 0x060051BE RID: 20926 RVA: 0x00081102 File Offset: 0x0007F302
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x170030C0 RID: 12480
			// (set) Token: 0x060051BF RID: 20927 RVA: 0x0008111A File Offset: 0x0007F31A
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x170030C1 RID: 12481
			// (set) Token: 0x060051C0 RID: 20928 RVA: 0x00081132 File Offset: 0x0007F332
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x170030C2 RID: 12482
			// (set) Token: 0x060051C1 RID: 20929 RVA: 0x0008114A File Offset: 0x0007F34A
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x170030C3 RID: 12483
			// (set) Token: 0x060051C2 RID: 20930 RVA: 0x0008115D File Offset: 0x0007F35D
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x170030C4 RID: 12484
			// (set) Token: 0x060051C3 RID: 20931 RVA: 0x00081170 File Offset: 0x0007F370
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x170030C5 RID: 12485
			// (set) Token: 0x060051C4 RID: 20932 RVA: 0x00081188 File Offset: 0x0007F388
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170030C6 RID: 12486
			// (set) Token: 0x060051C5 RID: 20933 RVA: 0x000811A0 File Offset: 0x0007F3A0
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030C7 RID: 12487
			// (set) Token: 0x060051C6 RID: 20934 RVA: 0x000811B8 File Offset: 0x0007F3B8
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030C8 RID: 12488
			// (set) Token: 0x060051C7 RID: 20935 RVA: 0x000811D0 File Offset: 0x0007F3D0
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170030C9 RID: 12489
			// (set) Token: 0x060051C8 RID: 20936 RVA: 0x000811E8 File Offset: 0x0007F3E8
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030CA RID: 12490
			// (set) Token: 0x060051C9 RID: 20937 RVA: 0x00081200 File Offset: 0x0007F400
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030CB RID: 12491
			// (set) Token: 0x060051CA RID: 20938 RVA: 0x00081218 File Offset: 0x0007F418
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x170030CC RID: 12492
			// (set) Token: 0x060051CB RID: 20939 RVA: 0x00081230 File Offset: 0x0007F430
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170030CD RID: 12493
			// (set) Token: 0x060051CC RID: 20940 RVA: 0x00081248 File Offset: 0x0007F448
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x170030CE RID: 12494
			// (set) Token: 0x060051CD RID: 20941 RVA: 0x0008125B File Offset: 0x0007F45B
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170030CF RID: 12495
			// (set) Token: 0x060051CE RID: 20942 RVA: 0x00081273 File Offset: 0x0007F473
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x170030D0 RID: 12496
			// (set) Token: 0x060051CF RID: 20943 RVA: 0x0008128B File Offset: 0x0007F48B
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170030D1 RID: 12497
			// (set) Token: 0x060051D0 RID: 20944 RVA: 0x000812A3 File Offset: 0x0007F4A3
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x170030D2 RID: 12498
			// (set) Token: 0x060051D1 RID: 20945 RVA: 0x000812B6 File Offset: 0x0007F4B6
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x170030D3 RID: 12499
			// (set) Token: 0x060051D2 RID: 20946 RVA: 0x000812C9 File Offset: 0x0007F4C9
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170030D4 RID: 12500
			// (set) Token: 0x060051D3 RID: 20947 RVA: 0x000812E1 File Offset: 0x0007F4E1
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x170030D5 RID: 12501
			// (set) Token: 0x060051D4 RID: 20948 RVA: 0x000812F9 File Offset: 0x0007F4F9
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x170030D6 RID: 12502
			// (set) Token: 0x060051D5 RID: 20949 RVA: 0x00081311 File Offset: 0x0007F511
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x170030D7 RID: 12503
			// (set) Token: 0x060051D6 RID: 20950 RVA: 0x00081329 File Offset: 0x0007F529
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x170030D8 RID: 12504
			// (set) Token: 0x060051D7 RID: 20951 RVA: 0x0008133C File Offset: 0x0007F53C
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x170030D9 RID: 12505
			// (set) Token: 0x060051D8 RID: 20952 RVA: 0x00081354 File Offset: 0x0007F554
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030DA RID: 12506
			// (set) Token: 0x060051D9 RID: 20953 RVA: 0x0008136C File Offset: 0x0007F56C
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030DB RID: 12507
			// (set) Token: 0x060051DA RID: 20954 RVA: 0x00081384 File Offset: 0x0007F584
			public virtual string MigrationLogExtensionData
			{
				set
				{
					base.PowerSharpParameters["MigrationLogExtensionData"] = value;
				}
			}

			// Token: 0x170030DC RID: 12508
			// (set) Token: 0x060051DB RID: 20955 RVA: 0x00081397 File Offset: 0x0007F597
			public virtual MigrationEventType MigrationLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MigrationLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170030DD RID: 12509
			// (set) Token: 0x060051DC RID: 20956 RVA: 0x000813AF File Offset: 0x0007F5AF
			public virtual LocalLongFullPath MigrationLogFilePath
			{
				set
				{
					base.PowerSharpParameters["MigrationLogFilePath"] = value;
				}
			}

			// Token: 0x170030DE RID: 12510
			// (set) Token: 0x060051DD RID: 20957 RVA: 0x000813C2 File Offset: 0x0007F5C2
			public virtual EnhancedTimeSpan MigrationLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxAge"] = value;
				}
			}

			// Token: 0x170030DF RID: 12511
			// (set) Token: 0x060051DE RID: 20958 RVA: 0x000813DA File Offset: 0x0007F5DA
			public virtual ByteQuantifiedSize MigrationLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030E0 RID: 12512
			// (set) Token: 0x060051DF RID: 20959 RVA: 0x000813F2 File Offset: 0x0007F5F2
			public virtual ByteQuantifiedSize MigrationLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030E1 RID: 12513
			// (set) Token: 0x060051E0 RID: 20960 RVA: 0x0008140A File Offset: 0x0007F60A
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x170030E2 RID: 12514
			// (set) Token: 0x060051E1 RID: 20961 RVA: 0x0008141D File Offset: 0x0007F61D
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x170030E3 RID: 12515
			// (set) Token: 0x060051E2 RID: 20962 RVA: 0x00081435 File Offset: 0x0007F635
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030E4 RID: 12516
			// (set) Token: 0x060051E3 RID: 20963 RVA: 0x0008144D File Offset: 0x0007F64D
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030E5 RID: 12517
			// (set) Token: 0x060051E4 RID: 20964 RVA: 0x00081465 File Offset: 0x0007F665
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x170030E6 RID: 12518
			// (set) Token: 0x060051E5 RID: 20965 RVA: 0x0008147D File Offset: 0x0007F67D
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030E7 RID: 12519
			// (set) Token: 0x060051E6 RID: 20966 RVA: 0x00081495 File Offset: 0x0007F695
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030E8 RID: 12520
			// (set) Token: 0x060051E7 RID: 20967 RVA: 0x000814AD File Offset: 0x0007F6AD
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x170030E9 RID: 12521
			// (set) Token: 0x060051E8 RID: 20968 RVA: 0x000814C0 File Offset: 0x0007F6C0
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x170030EA RID: 12522
			// (set) Token: 0x060051E9 RID: 20969 RVA: 0x000814D8 File Offset: 0x0007F6D8
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030EB RID: 12523
			// (set) Token: 0x060051EA RID: 20970 RVA: 0x000814F0 File Offset: 0x0007F6F0
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030EC RID: 12524
			// (set) Token: 0x060051EB RID: 20971 RVA: 0x00081508 File Offset: 0x0007F708
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x170030ED RID: 12525
			// (set) Token: 0x060051EC RID: 20972 RVA: 0x0008151B File Offset: 0x0007F71B
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x170030EE RID: 12526
			// (set) Token: 0x060051ED RID: 20973 RVA: 0x00081533 File Offset: 0x0007F733
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x170030EF RID: 12527
			// (set) Token: 0x060051EE RID: 20974 RVA: 0x00081546 File Offset: 0x0007F746
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x170030F0 RID: 12528
			// (set) Token: 0x060051EF RID: 20975 RVA: 0x0008155E File Offset: 0x0007F75E
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030F1 RID: 12529
			// (set) Token: 0x060051F0 RID: 20976 RVA: 0x00081576 File Offset: 0x0007F776
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170030F2 RID: 12530
			// (set) Token: 0x060051F1 RID: 20977 RVA: 0x0008158E File Offset: 0x0007F78E
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x170030F3 RID: 12531
			// (set) Token: 0x060051F2 RID: 20978 RVA: 0x000815A1 File Offset: 0x0007F7A1
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x170030F4 RID: 12532
			// (set) Token: 0x060051F3 RID: 20979 RVA: 0x000815B4 File Offset: 0x0007F7B4
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x170030F5 RID: 12533
			// (set) Token: 0x060051F4 RID: 20980 RVA: 0x000815CC File Offset: 0x0007F7CC
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x170030F6 RID: 12534
			// (set) Token: 0x060051F5 RID: 20981 RVA: 0x000815E4 File Offset: 0x0007F7E4
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x170030F7 RID: 12535
			// (set) Token: 0x060051F6 RID: 20982 RVA: 0x000815FC File Offset: 0x0007F7FC
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x170030F8 RID: 12536
			// (set) Token: 0x060051F7 RID: 20983 RVA: 0x00081614 File Offset: 0x0007F814
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170030F9 RID: 12537
			// (set) Token: 0x060051F8 RID: 20984 RVA: 0x0008162C File Offset: 0x0007F82C
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x170030FA RID: 12538
			// (set) Token: 0x060051F9 RID: 20985 RVA: 0x0008163F File Offset: 0x0007F83F
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170030FB RID: 12539
			// (set) Token: 0x060051FA RID: 20986 RVA: 0x00081657 File Offset: 0x0007F857
			public virtual ProtocolLoggingLevel InMemoryReceiveConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170030FC RID: 12540
			// (set) Token: 0x060051FB RID: 20987 RVA: 0x0008166F File Offset: 0x0007F86F
			public virtual bool InMemoryReceiveConnectorSmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorSmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x170030FD RID: 12541
			// (set) Token: 0x060051FC RID: 20988 RVA: 0x00081687 File Offset: 0x0007F887
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x170030FE RID: 12542
			// (set) Token: 0x060051FD RID: 20989 RVA: 0x0008169F File Offset: 0x0007F89F
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x170030FF RID: 12543
			// (set) Token: 0x060051FE RID: 20990 RVA: 0x000816B7 File Offset: 0x0007F8B7
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x17003100 RID: 12544
			// (set) Token: 0x060051FF RID: 20991 RVA: 0x000816CF File Offset: 0x0007F8CF
			public virtual bool GatewayEdgeSyncSubscribed
			{
				set
				{
					base.PowerSharpParameters["GatewayEdgeSyncSubscribed"] = value;
				}
			}

			// Token: 0x17003101 RID: 12545
			// (set) Token: 0x06005200 RID: 20992 RVA: 0x000816E7 File Offset: 0x0007F8E7
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003102 RID: 12546
			// (set) Token: 0x06005201 RID: 20993 RVA: 0x000816FF File Offset: 0x0007F8FF
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x17003103 RID: 12547
			// (set) Token: 0x06005202 RID: 20994 RVA: 0x00081717 File Offset: 0x0007F917
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x17003104 RID: 12548
			// (set) Token: 0x06005203 RID: 20995 RVA: 0x0008172F File Offset: 0x0007F92F
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x17003105 RID: 12549
			// (set) Token: 0x06005204 RID: 20996 RVA: 0x00081742 File Offset: 0x0007F942
			public virtual int? MaxCallsAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxCallsAllowed"] = value;
				}
			}

			// Token: 0x17003106 RID: 12550
			// (set) Token: 0x06005205 RID: 20997 RVA: 0x0008175A File Offset: 0x0007F95A
			public virtual ServerStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17003107 RID: 12551
			// (set) Token: 0x06005206 RID: 20998 RVA: 0x00081772 File Offset: 0x0007F972
			public virtual ScheduleInterval GrammarGenerationSchedule
			{
				set
				{
					base.PowerSharpParameters["GrammarGenerationSchedule"] = value;
				}
			}

			// Token: 0x17003108 RID: 12552
			// (set) Token: 0x06005207 RID: 20999 RVA: 0x0008178A File Offset: 0x0007F98A
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17003109 RID: 12553
			// (set) Token: 0x06005208 RID: 21000 RVA: 0x000817A2 File Offset: 0x0007F9A2
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x1700310A RID: 12554
			// (set) Token: 0x06005209 RID: 21001 RVA: 0x000817BA File Offset: 0x0007F9BA
			public virtual bool DatabaseCopyActivationDisabledAndMoveNow
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyActivationDisabledAndMoveNow"] = value;
				}
			}

			// Token: 0x1700310B RID: 12555
			// (set) Token: 0x0600520A RID: 21002 RVA: 0x000817D2 File Offset: 0x0007F9D2
			public virtual string FaultZone
			{
				set
				{
					base.PowerSharpParameters["FaultZone"] = value;
				}
			}

			// Token: 0x1700310C RID: 12556
			// (set) Token: 0x0600520B RID: 21003 RVA: 0x000817E5 File Offset: 0x0007F9E5
			public virtual bool AutoDagServerConfigured
			{
				set
				{
					base.PowerSharpParameters["AutoDagServerConfigured"] = value;
				}
			}

			// Token: 0x1700310D RID: 12557
			// (set) Token: 0x0600520C RID: 21004 RVA: 0x000817FD File Offset: 0x0007F9FD
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700310E RID: 12558
			// (set) Token: 0x0600520D RID: 21005 RVA: 0x00081815 File Offset: 0x0007FA15
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x1700310F RID: 12559
			// (set) Token: 0x0600520E RID: 21006 RVA: 0x0008182D File Offset: 0x0007FA2D
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x17003110 RID: 12560
			// (set) Token: 0x0600520F RID: 21007 RVA: 0x00081845 File Offset: 0x0007FA45
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x17003111 RID: 12561
			// (set) Token: 0x06005210 RID: 21008 RVA: 0x0008185D File Offset: 0x0007FA5D
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x17003112 RID: 12562
			// (set) Token: 0x06005211 RID: 21009 RVA: 0x00081875 File Offset: 0x0007FA75
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x17003113 RID: 12563
			// (set) Token: 0x06005212 RID: 21010 RVA: 0x0008188D File Offset: 0x0007FA8D
			public virtual bool TransportSyncDispatchEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncDispatchEnabled"] = value;
				}
			}

			// Token: 0x17003114 RID: 12564
			// (set) Token: 0x06005213 RID: 21011 RVA: 0x000818A5 File Offset: 0x0007FAA5
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x17003115 RID: 12565
			// (set) Token: 0x06005214 RID: 21012 RVA: 0x000818BD File Offset: 0x0007FABD
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x17003116 RID: 12566
			// (set) Token: 0x06005215 RID: 21013 RVA: 0x000818D5 File Offset: 0x0007FAD5
			public virtual int MaxAcceptedTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxAcceptedTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x17003117 RID: 12567
			// (set) Token: 0x06005216 RID: 21014 RVA: 0x000818ED File Offset: 0x0007FAED
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x17003118 RID: 12568
			// (set) Token: 0x06005217 RID: 21015 RVA: 0x00081905 File Offset: 0x0007FB05
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x17003119 RID: 12569
			// (set) Token: 0x06005218 RID: 21016 RVA: 0x00081918 File Offset: 0x0007FB18
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x1700311A RID: 12570
			// (set) Token: 0x06005219 RID: 21017 RVA: 0x00081930 File Offset: 0x0007FB30
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x1700311B RID: 12571
			// (set) Token: 0x0600521A RID: 21018 RVA: 0x00081943 File Offset: 0x0007FB43
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x1700311C RID: 12572
			// (set) Token: 0x0600521B RID: 21019 RVA: 0x0008195B File Offset: 0x0007FB5B
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700311D RID: 12573
			// (set) Token: 0x0600521C RID: 21020 RVA: 0x00081973 File Offset: 0x0007FB73
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700311E RID: 12574
			// (set) Token: 0x0600521D RID: 21021 RVA: 0x0008198B File Offset: 0x0007FB8B
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x1700311F RID: 12575
			// (set) Token: 0x0600521E RID: 21022 RVA: 0x000819A3 File Offset: 0x0007FBA3
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x17003120 RID: 12576
			// (set) Token: 0x0600521F RID: 21023 RVA: 0x000819BB File Offset: 0x0007FBBB
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003121 RID: 12577
			// (set) Token: 0x06005220 RID: 21024 RVA: 0x000819D3 File Offset: 0x0007FBD3
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x17003122 RID: 12578
			// (set) Token: 0x06005221 RID: 21025 RVA: 0x000819E6 File Offset: 0x0007FBE6
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x17003123 RID: 12579
			// (set) Token: 0x06005222 RID: 21026 RVA: 0x000819FE File Offset: 0x0007FBFE
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003124 RID: 12580
			// (set) Token: 0x06005223 RID: 21027 RVA: 0x00081A16 File Offset: 0x0007FC16
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003125 RID: 12581
			// (set) Token: 0x06005224 RID: 21028 RVA: 0x00081A2E File Offset: 0x0007FC2E
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x17003126 RID: 12582
			// (set) Token: 0x06005225 RID: 21029 RVA: 0x00081A46 File Offset: 0x0007FC46
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x17003127 RID: 12583
			// (set) Token: 0x06005226 RID: 21030 RVA: 0x00081A59 File Offset: 0x0007FC59
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x17003128 RID: 12584
			// (set) Token: 0x06005227 RID: 21031 RVA: 0x00081A71 File Offset: 0x0007FC71
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003129 RID: 12585
			// (set) Token: 0x06005228 RID: 21032 RVA: 0x00081A89 File Offset: 0x0007FC89
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700312A RID: 12586
			// (set) Token: 0x06005229 RID: 21033 RVA: 0x00081AA1 File Offset: 0x0007FCA1
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x1700312B RID: 12587
			// (set) Token: 0x0600522A RID: 21034 RVA: 0x00081AB9 File Offset: 0x0007FCB9
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x1700312C RID: 12588
			// (set) Token: 0x0600522B RID: 21035 RVA: 0x00081AD1 File Offset: 0x0007FCD1
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x1700312D RID: 12589
			// (set) Token: 0x0600522C RID: 21036 RVA: 0x00081AE9 File Offset: 0x0007FCE9
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x1700312E RID: 12590
			// (set) Token: 0x0600522D RID: 21037 RVA: 0x00081B01 File Offset: 0x0007FD01
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700312F RID: 12591
			// (set) Token: 0x0600522E RID: 21038 RVA: 0x00081B19 File Offset: 0x0007FD19
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x17003130 RID: 12592
			// (set) Token: 0x0600522F RID: 21039 RVA: 0x00081B31 File Offset: 0x0007FD31
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x17003131 RID: 12593
			// (set) Token: 0x06005230 RID: 21040 RVA: 0x00081B49 File Offset: 0x0007FD49
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x17003132 RID: 12594
			// (set) Token: 0x06005231 RID: 21041 RVA: 0x00081B61 File Offset: 0x0007FD61
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x17003133 RID: 12595
			// (set) Token: 0x06005232 RID: 21042 RVA: 0x00081B74 File Offset: 0x0007FD74
			public virtual int MaxTransportSyncDispatchers
			{
				set
				{
					base.PowerSharpParameters["MaxTransportSyncDispatchers"] = value;
				}
			}

			// Token: 0x17003134 RID: 12596
			// (set) Token: 0x06005233 RID: 21043 RVA: 0x00081B8C File Offset: 0x0007FD8C
			public virtual bool TransportSyncMailboxLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogEnabled"] = value;
				}
			}

			// Token: 0x17003135 RID: 12597
			// (set) Token: 0x06005234 RID: 21044 RVA: 0x00081BA4 File Offset: 0x0007FDA4
			public virtual SyncLoggingLevel TransportSyncMailboxLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003136 RID: 12598
			// (set) Token: 0x06005235 RID: 21045 RVA: 0x00081BBC File Offset: 0x0007FDBC
			public virtual LocalLongFullPath TransportSyncMailboxLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogFilePath"] = value;
				}
			}

			// Token: 0x17003137 RID: 12599
			// (set) Token: 0x06005236 RID: 21046 RVA: 0x00081BCF File Offset: 0x0007FDCF
			public virtual EnhancedTimeSpan TransportSyncMailboxLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxAge"] = value;
				}
			}

			// Token: 0x17003138 RID: 12600
			// (set) Token: 0x06005237 RID: 21047 RVA: 0x00081BE7 File Offset: 0x0007FDE7
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003139 RID: 12601
			// (set) Token: 0x06005238 RID: 21048 RVA: 0x00081BFF File Offset: 0x0007FDFF
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700313A RID: 12602
			// (set) Token: 0x06005239 RID: 21049 RVA: 0x00081C17 File Offset: 0x0007FE17
			public virtual bool TransportSyncMailboxHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogEnabled"] = value;
				}
			}

			// Token: 0x1700313B RID: 12603
			// (set) Token: 0x0600523A RID: 21050 RVA: 0x00081C2F File Offset: 0x0007FE2F
			public virtual LocalLongFullPath TransportSyncMailboxHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogFilePath"] = value;
				}
			}

			// Token: 0x1700313C RID: 12604
			// (set) Token: 0x0600523B RID: 21051 RVA: 0x00081C42 File Offset: 0x0007FE42
			public virtual EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x1700313D RID: 12605
			// (set) Token: 0x0600523C RID: 21052 RVA: 0x00081C5A File Offset: 0x0007FE5A
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700313E RID: 12606
			// (set) Token: 0x0600523D RID: 21053 RVA: 0x00081C72 File Offset: 0x0007FE72
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700313F RID: 12607
			// (set) Token: 0x0600523E RID: 21054 RVA: 0x00081C8A File Offset: 0x0007FE8A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003140 RID: 12608
			// (set) Token: 0x0600523F RID: 21055 RVA: 0x00081C9D File Offset: 0x0007FE9D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003141 RID: 12609
			// (set) Token: 0x06005240 RID: 21056 RVA: 0x00081CB5 File Offset: 0x0007FEB5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003142 RID: 12610
			// (set) Token: 0x06005241 RID: 21057 RVA: 0x00081CCD File Offset: 0x0007FECD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003143 RID: 12611
			// (set) Token: 0x06005242 RID: 21058 RVA: 0x00081CE5 File Offset: 0x0007FEE5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200065C RID: 1628
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003144 RID: 12612
			// (set) Token: 0x06005244 RID: 21060 RVA: 0x00081D05 File Offset: 0x0007FF05
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003145 RID: 12613
			// (set) Token: 0x06005245 RID: 21061 RVA: 0x00081D18 File Offset: 0x0007FF18
			public virtual ServerRole ServerRole
			{
				set
				{
					base.PowerSharpParameters["ServerRole"] = value;
				}
			}

			// Token: 0x17003146 RID: 12614
			// (set) Token: 0x06005246 RID: 21062 RVA: 0x00081D30 File Offset: 0x0007FF30
			public virtual SwitchParameter Remove
			{
				set
				{
					base.PowerSharpParameters["Remove"] = value;
				}
			}

			// Token: 0x17003147 RID: 12615
			// (set) Token: 0x06005247 RID: 21063 RVA: 0x00081D48 File Offset: 0x0007FF48
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003148 RID: 12616
			// (set) Token: 0x06005248 RID: 21064 RVA: 0x00081D5B File Offset: 0x0007FF5B
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x17003149 RID: 12617
			// (set) Token: 0x06005249 RID: 21065 RVA: 0x00081D73 File Offset: 0x0007FF73
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x1700314A RID: 12618
			// (set) Token: 0x0600524A RID: 21066 RVA: 0x00081D8B File Offset: 0x0007FF8B
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x1700314B RID: 12619
			// (set) Token: 0x0600524B RID: 21067 RVA: 0x00081DA3 File Offset: 0x0007FFA3
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x1700314C RID: 12620
			// (set) Token: 0x0600524C RID: 21068 RVA: 0x00081DBB File Offset: 0x0007FFBB
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x1700314D RID: 12621
			// (set) Token: 0x0600524D RID: 21069 RVA: 0x00081DD3 File Offset: 0x0007FFD3
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x1700314E RID: 12622
			// (set) Token: 0x0600524E RID: 21070 RVA: 0x00081DEB File Offset: 0x0007FFEB
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x1700314F RID: 12623
			// (set) Token: 0x0600524F RID: 21071 RVA: 0x00081E03 File Offset: 0x00080003
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x17003150 RID: 12624
			// (set) Token: 0x06005250 RID: 21072 RVA: 0x00081E1B File Offset: 0x0008001B
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x17003151 RID: 12625
			// (set) Token: 0x06005251 RID: 21073 RVA: 0x00081E33 File Offset: 0x00080033
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003152 RID: 12626
			// (set) Token: 0x06005252 RID: 21074 RVA: 0x00081E46 File Offset: 0x00080046
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003153 RID: 12627
			// (set) Token: 0x06005253 RID: 21075 RVA: 0x00081E59 File Offset: 0x00080059
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x17003154 RID: 12628
			// (set) Token: 0x06005254 RID: 21076 RVA: 0x00081E71 File Offset: 0x00080071
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003155 RID: 12629
			// (set) Token: 0x06005255 RID: 21077 RVA: 0x00081E89 File Offset: 0x00080089
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003156 RID: 12630
			// (set) Token: 0x06005256 RID: 21078 RVA: 0x00081EA1 File Offset: 0x000800A1
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003157 RID: 12631
			// (set) Token: 0x06005257 RID: 21079 RVA: 0x00081EB9 File Offset: 0x000800B9
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003158 RID: 12632
			// (set) Token: 0x06005258 RID: 21080 RVA: 0x00081ED1 File Offset: 0x000800D1
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003159 RID: 12633
			// (set) Token: 0x06005259 RID: 21081 RVA: 0x00081EE9 File Offset: 0x000800E9
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700315A RID: 12634
			// (set) Token: 0x0600525A RID: 21082 RVA: 0x00081F01 File Offset: 0x00080101
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700315B RID: 12635
			// (set) Token: 0x0600525B RID: 21083 RVA: 0x00081F19 File Offset: 0x00080119
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x1700315C RID: 12636
			// (set) Token: 0x0600525C RID: 21084 RVA: 0x00081F31 File Offset: 0x00080131
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x1700315D RID: 12637
			// (set) Token: 0x0600525D RID: 21085 RVA: 0x00081F44 File Offset: 0x00080144
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x1700315E RID: 12638
			// (set) Token: 0x0600525E RID: 21086 RVA: 0x00081F5C File Offset: 0x0008015C
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700315F RID: 12639
			// (set) Token: 0x0600525F RID: 21087 RVA: 0x00081F74 File Offset: 0x00080174
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x17003160 RID: 12640
			// (set) Token: 0x06005260 RID: 21088 RVA: 0x00081F8C File Offset: 0x0008018C
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x17003161 RID: 12641
			// (set) Token: 0x06005261 RID: 21089 RVA: 0x00081F9F File Offset: 0x0008019F
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x17003162 RID: 12642
			// (set) Token: 0x06005262 RID: 21090 RVA: 0x00081FB2 File Offset: 0x000801B2
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x17003163 RID: 12643
			// (set) Token: 0x06005263 RID: 21091 RVA: 0x00081FCA File Offset: 0x000801CA
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x17003164 RID: 12644
			// (set) Token: 0x06005264 RID: 21092 RVA: 0x00081FE2 File Offset: 0x000801E2
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x17003165 RID: 12645
			// (set) Token: 0x06005265 RID: 21093 RVA: 0x00081FFA File Offset: 0x000801FA
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x17003166 RID: 12646
			// (set) Token: 0x06005266 RID: 21094 RVA: 0x00082012 File Offset: 0x00080212
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x17003167 RID: 12647
			// (set) Token: 0x06005267 RID: 21095 RVA: 0x00082025 File Offset: 0x00080225
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x17003168 RID: 12648
			// (set) Token: 0x06005268 RID: 21096 RVA: 0x0008203D File Offset: 0x0008023D
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003169 RID: 12649
			// (set) Token: 0x06005269 RID: 21097 RVA: 0x00082055 File Offset: 0x00080255
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700316A RID: 12650
			// (set) Token: 0x0600526A RID: 21098 RVA: 0x0008206D File Offset: 0x0008026D
			public virtual string MigrationLogExtensionData
			{
				set
				{
					base.PowerSharpParameters["MigrationLogExtensionData"] = value;
				}
			}

			// Token: 0x1700316B RID: 12651
			// (set) Token: 0x0600526B RID: 21099 RVA: 0x00082080 File Offset: 0x00080280
			public virtual MigrationEventType MigrationLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MigrationLogLoggingLevel"] = value;
				}
			}

			// Token: 0x1700316C RID: 12652
			// (set) Token: 0x0600526C RID: 21100 RVA: 0x00082098 File Offset: 0x00080298
			public virtual LocalLongFullPath MigrationLogFilePath
			{
				set
				{
					base.PowerSharpParameters["MigrationLogFilePath"] = value;
				}
			}

			// Token: 0x1700316D RID: 12653
			// (set) Token: 0x0600526D RID: 21101 RVA: 0x000820AB File Offset: 0x000802AB
			public virtual EnhancedTimeSpan MigrationLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxAge"] = value;
				}
			}

			// Token: 0x1700316E RID: 12654
			// (set) Token: 0x0600526E RID: 21102 RVA: 0x000820C3 File Offset: 0x000802C3
			public virtual ByteQuantifiedSize MigrationLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700316F RID: 12655
			// (set) Token: 0x0600526F RID: 21103 RVA: 0x000820DB File Offset: 0x000802DB
			public virtual ByteQuantifiedSize MigrationLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003170 RID: 12656
			// (set) Token: 0x06005270 RID: 21104 RVA: 0x000820F3 File Offset: 0x000802F3
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x17003171 RID: 12657
			// (set) Token: 0x06005271 RID: 21105 RVA: 0x00082106 File Offset: 0x00080306
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x17003172 RID: 12658
			// (set) Token: 0x06005272 RID: 21106 RVA: 0x0008211E File Offset: 0x0008031E
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003173 RID: 12659
			// (set) Token: 0x06005273 RID: 21107 RVA: 0x00082136 File Offset: 0x00080336
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003174 RID: 12660
			// (set) Token: 0x06005274 RID: 21108 RVA: 0x0008214E File Offset: 0x0008034E
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003175 RID: 12661
			// (set) Token: 0x06005275 RID: 21109 RVA: 0x00082166 File Offset: 0x00080366
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003176 RID: 12662
			// (set) Token: 0x06005276 RID: 21110 RVA: 0x0008217E File Offset: 0x0008037E
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003177 RID: 12663
			// (set) Token: 0x06005277 RID: 21111 RVA: 0x00082196 File Offset: 0x00080396
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x17003178 RID: 12664
			// (set) Token: 0x06005278 RID: 21112 RVA: 0x000821A9 File Offset: 0x000803A9
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003179 RID: 12665
			// (set) Token: 0x06005279 RID: 21113 RVA: 0x000821C1 File Offset: 0x000803C1
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700317A RID: 12666
			// (set) Token: 0x0600527A RID: 21114 RVA: 0x000821D9 File Offset: 0x000803D9
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700317B RID: 12667
			// (set) Token: 0x0600527B RID: 21115 RVA: 0x000821F1 File Offset: 0x000803F1
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x1700317C RID: 12668
			// (set) Token: 0x0600527C RID: 21116 RVA: 0x00082204 File Offset: 0x00080404
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x1700317D RID: 12669
			// (set) Token: 0x0600527D RID: 21117 RVA: 0x0008221C File Offset: 0x0008041C
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x1700317E RID: 12670
			// (set) Token: 0x0600527E RID: 21118 RVA: 0x0008222F File Offset: 0x0008042F
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x1700317F RID: 12671
			// (set) Token: 0x0600527F RID: 21119 RVA: 0x00082247 File Offset: 0x00080447
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003180 RID: 12672
			// (set) Token: 0x06005280 RID: 21120 RVA: 0x0008225F File Offset: 0x0008045F
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003181 RID: 12673
			// (set) Token: 0x06005281 RID: 21121 RVA: 0x00082277 File Offset: 0x00080477
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x17003182 RID: 12674
			// (set) Token: 0x06005282 RID: 21122 RVA: 0x0008228A File Offset: 0x0008048A
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x17003183 RID: 12675
			// (set) Token: 0x06005283 RID: 21123 RVA: 0x0008229D File Offset: 0x0008049D
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x17003184 RID: 12676
			// (set) Token: 0x06005284 RID: 21124 RVA: 0x000822B5 File Offset: 0x000804B5
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x17003185 RID: 12677
			// (set) Token: 0x06005285 RID: 21125 RVA: 0x000822CD File Offset: 0x000804CD
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17003186 RID: 12678
			// (set) Token: 0x06005286 RID: 21126 RVA: 0x000822E5 File Offset: 0x000804E5
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x17003187 RID: 12679
			// (set) Token: 0x06005287 RID: 21127 RVA: 0x000822FD File Offset: 0x000804FD
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003188 RID: 12680
			// (set) Token: 0x06005288 RID: 21128 RVA: 0x00082315 File Offset: 0x00080515
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x17003189 RID: 12681
			// (set) Token: 0x06005289 RID: 21129 RVA: 0x00082328 File Offset: 0x00080528
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700318A RID: 12682
			// (set) Token: 0x0600528A RID: 21130 RVA: 0x00082340 File Offset: 0x00080540
			public virtual ProtocolLoggingLevel InMemoryReceiveConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700318B RID: 12683
			// (set) Token: 0x0600528B RID: 21131 RVA: 0x00082358 File Offset: 0x00080558
			public virtual bool InMemoryReceiveConnectorSmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorSmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x1700318C RID: 12684
			// (set) Token: 0x0600528C RID: 21132 RVA: 0x00082370 File Offset: 0x00080570
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x1700318D RID: 12685
			// (set) Token: 0x0600528D RID: 21133 RVA: 0x00082388 File Offset: 0x00080588
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x1700318E RID: 12686
			// (set) Token: 0x0600528E RID: 21134 RVA: 0x000823A0 File Offset: 0x000805A0
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x1700318F RID: 12687
			// (set) Token: 0x0600528F RID: 21135 RVA: 0x000823B8 File Offset: 0x000805B8
			public virtual bool GatewayEdgeSyncSubscribed
			{
				set
				{
					base.PowerSharpParameters["GatewayEdgeSyncSubscribed"] = value;
				}
			}

			// Token: 0x17003190 RID: 12688
			// (set) Token: 0x06005290 RID: 21136 RVA: 0x000823D0 File Offset: 0x000805D0
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003191 RID: 12689
			// (set) Token: 0x06005291 RID: 21137 RVA: 0x000823E8 File Offset: 0x000805E8
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x17003192 RID: 12690
			// (set) Token: 0x06005292 RID: 21138 RVA: 0x00082400 File Offset: 0x00080600
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x17003193 RID: 12691
			// (set) Token: 0x06005293 RID: 21139 RVA: 0x00082418 File Offset: 0x00080618
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x17003194 RID: 12692
			// (set) Token: 0x06005294 RID: 21140 RVA: 0x0008242B File Offset: 0x0008062B
			public virtual int? MaxCallsAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxCallsAllowed"] = value;
				}
			}

			// Token: 0x17003195 RID: 12693
			// (set) Token: 0x06005295 RID: 21141 RVA: 0x00082443 File Offset: 0x00080643
			public virtual ServerStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17003196 RID: 12694
			// (set) Token: 0x06005296 RID: 21142 RVA: 0x0008245B File Offset: 0x0008065B
			public virtual ScheduleInterval GrammarGenerationSchedule
			{
				set
				{
					base.PowerSharpParameters["GrammarGenerationSchedule"] = value;
				}
			}

			// Token: 0x17003197 RID: 12695
			// (set) Token: 0x06005297 RID: 21143 RVA: 0x00082473 File Offset: 0x00080673
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17003198 RID: 12696
			// (set) Token: 0x06005298 RID: 21144 RVA: 0x0008248B File Offset: 0x0008068B
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x17003199 RID: 12697
			// (set) Token: 0x06005299 RID: 21145 RVA: 0x000824A3 File Offset: 0x000806A3
			public virtual bool DatabaseCopyActivationDisabledAndMoveNow
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyActivationDisabledAndMoveNow"] = value;
				}
			}

			// Token: 0x1700319A RID: 12698
			// (set) Token: 0x0600529A RID: 21146 RVA: 0x000824BB File Offset: 0x000806BB
			public virtual string FaultZone
			{
				set
				{
					base.PowerSharpParameters["FaultZone"] = value;
				}
			}

			// Token: 0x1700319B RID: 12699
			// (set) Token: 0x0600529B RID: 21147 RVA: 0x000824CE File Offset: 0x000806CE
			public virtual bool AutoDagServerConfigured
			{
				set
				{
					base.PowerSharpParameters["AutoDagServerConfigured"] = value;
				}
			}

			// Token: 0x1700319C RID: 12700
			// (set) Token: 0x0600529C RID: 21148 RVA: 0x000824E6 File Offset: 0x000806E6
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700319D RID: 12701
			// (set) Token: 0x0600529D RID: 21149 RVA: 0x000824FE File Offset: 0x000806FE
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x1700319E RID: 12702
			// (set) Token: 0x0600529E RID: 21150 RVA: 0x00082516 File Offset: 0x00080716
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700319F RID: 12703
			// (set) Token: 0x0600529F RID: 21151 RVA: 0x0008252E File Offset: 0x0008072E
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x170031A0 RID: 12704
			// (set) Token: 0x060052A0 RID: 21152 RVA: 0x00082546 File Offset: 0x00080746
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x170031A1 RID: 12705
			// (set) Token: 0x060052A1 RID: 21153 RVA: 0x0008255E File Offset: 0x0008075E
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x170031A2 RID: 12706
			// (set) Token: 0x060052A2 RID: 21154 RVA: 0x00082576 File Offset: 0x00080776
			public virtual bool TransportSyncDispatchEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncDispatchEnabled"] = value;
				}
			}

			// Token: 0x170031A3 RID: 12707
			// (set) Token: 0x060052A3 RID: 21155 RVA: 0x0008258E File Offset: 0x0008078E
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x170031A4 RID: 12708
			// (set) Token: 0x060052A4 RID: 21156 RVA: 0x000825A6 File Offset: 0x000807A6
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x170031A5 RID: 12709
			// (set) Token: 0x060052A5 RID: 21157 RVA: 0x000825BE File Offset: 0x000807BE
			public virtual int MaxAcceptedTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxAcceptedTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x170031A6 RID: 12710
			// (set) Token: 0x060052A6 RID: 21158 RVA: 0x000825D6 File Offset: 0x000807D6
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x170031A7 RID: 12711
			// (set) Token: 0x060052A7 RID: 21159 RVA: 0x000825EE File Offset: 0x000807EE
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x170031A8 RID: 12712
			// (set) Token: 0x060052A8 RID: 21160 RVA: 0x00082601 File Offset: 0x00080801
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x170031A9 RID: 12713
			// (set) Token: 0x060052A9 RID: 21161 RVA: 0x00082619 File Offset: 0x00080819
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x170031AA RID: 12714
			// (set) Token: 0x060052AA RID: 21162 RVA: 0x0008262C File Offset: 0x0008082C
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170031AB RID: 12715
			// (set) Token: 0x060052AB RID: 21163 RVA: 0x00082644 File Offset: 0x00080844
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031AC RID: 12716
			// (set) Token: 0x060052AC RID: 21164 RVA: 0x0008265C File Offset: 0x0008085C
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031AD RID: 12717
			// (set) Token: 0x060052AD RID: 21165 RVA: 0x00082674 File Offset: 0x00080874
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170031AE RID: 12718
			// (set) Token: 0x060052AE RID: 21166 RVA: 0x0008268C File Offset: 0x0008088C
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x170031AF RID: 12719
			// (set) Token: 0x060052AF RID: 21167 RVA: 0x000826A4 File Offset: 0x000808A4
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170031B0 RID: 12720
			// (set) Token: 0x060052B0 RID: 21168 RVA: 0x000826BC File Offset: 0x000808BC
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x170031B1 RID: 12721
			// (set) Token: 0x060052B1 RID: 21169 RVA: 0x000826CF File Offset: 0x000808CF
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x170031B2 RID: 12722
			// (set) Token: 0x060052B2 RID: 21170 RVA: 0x000826E7 File Offset: 0x000808E7
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031B3 RID: 12723
			// (set) Token: 0x060052B3 RID: 21171 RVA: 0x000826FF File Offset: 0x000808FF
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031B4 RID: 12724
			// (set) Token: 0x060052B4 RID: 21172 RVA: 0x00082717 File Offset: 0x00080917
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x170031B5 RID: 12725
			// (set) Token: 0x060052B5 RID: 21173 RVA: 0x0008272F File Offset: 0x0008092F
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x170031B6 RID: 12726
			// (set) Token: 0x060052B6 RID: 21174 RVA: 0x00082742 File Offset: 0x00080942
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x170031B7 RID: 12727
			// (set) Token: 0x060052B7 RID: 21175 RVA: 0x0008275A File Offset: 0x0008095A
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031B8 RID: 12728
			// (set) Token: 0x060052B8 RID: 21176 RVA: 0x00082772 File Offset: 0x00080972
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031B9 RID: 12729
			// (set) Token: 0x060052B9 RID: 21177 RVA: 0x0008278A File Offset: 0x0008098A
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x170031BA RID: 12730
			// (set) Token: 0x060052BA RID: 21178 RVA: 0x000827A2 File Offset: 0x000809A2
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x170031BB RID: 12731
			// (set) Token: 0x060052BB RID: 21179 RVA: 0x000827BA File Offset: 0x000809BA
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x170031BC RID: 12732
			// (set) Token: 0x060052BC RID: 21180 RVA: 0x000827D2 File Offset: 0x000809D2
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x170031BD RID: 12733
			// (set) Token: 0x060052BD RID: 21181 RVA: 0x000827EA File Offset: 0x000809EA
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x170031BE RID: 12734
			// (set) Token: 0x060052BE RID: 21182 RVA: 0x00082802 File Offset: 0x00080A02
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x170031BF RID: 12735
			// (set) Token: 0x060052BF RID: 21183 RVA: 0x0008281A File Offset: 0x00080A1A
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x170031C0 RID: 12736
			// (set) Token: 0x060052C0 RID: 21184 RVA: 0x00082832 File Offset: 0x00080A32
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x170031C1 RID: 12737
			// (set) Token: 0x060052C1 RID: 21185 RVA: 0x0008284A File Offset: 0x00080A4A
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x170031C2 RID: 12738
			// (set) Token: 0x060052C2 RID: 21186 RVA: 0x0008285D File Offset: 0x00080A5D
			public virtual int MaxTransportSyncDispatchers
			{
				set
				{
					base.PowerSharpParameters["MaxTransportSyncDispatchers"] = value;
				}
			}

			// Token: 0x170031C3 RID: 12739
			// (set) Token: 0x060052C3 RID: 21187 RVA: 0x00082875 File Offset: 0x00080A75
			public virtual bool TransportSyncMailboxLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogEnabled"] = value;
				}
			}

			// Token: 0x170031C4 RID: 12740
			// (set) Token: 0x060052C4 RID: 21188 RVA: 0x0008288D File Offset: 0x00080A8D
			public virtual SyncLoggingLevel TransportSyncMailboxLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170031C5 RID: 12741
			// (set) Token: 0x060052C5 RID: 21189 RVA: 0x000828A5 File Offset: 0x00080AA5
			public virtual LocalLongFullPath TransportSyncMailboxLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogFilePath"] = value;
				}
			}

			// Token: 0x170031C6 RID: 12742
			// (set) Token: 0x060052C6 RID: 21190 RVA: 0x000828B8 File Offset: 0x00080AB8
			public virtual EnhancedTimeSpan TransportSyncMailboxLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxAge"] = value;
				}
			}

			// Token: 0x170031C7 RID: 12743
			// (set) Token: 0x060052C7 RID: 21191 RVA: 0x000828D0 File Offset: 0x00080AD0
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031C8 RID: 12744
			// (set) Token: 0x060052C8 RID: 21192 RVA: 0x000828E8 File Offset: 0x00080AE8
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031C9 RID: 12745
			// (set) Token: 0x060052C9 RID: 21193 RVA: 0x00082900 File Offset: 0x00080B00
			public virtual bool TransportSyncMailboxHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogEnabled"] = value;
				}
			}

			// Token: 0x170031CA RID: 12746
			// (set) Token: 0x060052CA RID: 21194 RVA: 0x00082918 File Offset: 0x00080B18
			public virtual LocalLongFullPath TransportSyncMailboxHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogFilePath"] = value;
				}
			}

			// Token: 0x170031CB RID: 12747
			// (set) Token: 0x060052CB RID: 21195 RVA: 0x0008292B File Offset: 0x00080B2B
			public virtual EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x170031CC RID: 12748
			// (set) Token: 0x060052CC RID: 21196 RVA: 0x00082943 File Offset: 0x00080B43
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031CD RID: 12749
			// (set) Token: 0x060052CD RID: 21197 RVA: 0x0008295B File Offset: 0x00080B5B
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031CE RID: 12750
			// (set) Token: 0x060052CE RID: 21198 RVA: 0x00082973 File Offset: 0x00080B73
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170031CF RID: 12751
			// (set) Token: 0x060052CF RID: 21199 RVA: 0x00082986 File Offset: 0x00080B86
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170031D0 RID: 12752
			// (set) Token: 0x060052D0 RID: 21200 RVA: 0x0008299E File Offset: 0x00080B9E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170031D1 RID: 12753
			// (set) Token: 0x060052D1 RID: 21201 RVA: 0x000829B6 File Offset: 0x00080BB6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170031D2 RID: 12754
			// (set) Token: 0x060052D2 RID: 21202 RVA: 0x000829CE File Offset: 0x00080BCE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
