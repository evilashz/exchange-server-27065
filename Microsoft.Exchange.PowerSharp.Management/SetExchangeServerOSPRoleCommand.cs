using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200065D RID: 1629
	public class SetExchangeServerOSPRoleCommand : SyntheticCommandWithPipelineInputNoOutput<Server>
	{
		// Token: 0x060052D4 RID: 21204 RVA: 0x000829EE File Offset: 0x00080BEE
		private SetExchangeServerOSPRoleCommand() : base("Set-ExchangeServerOSPRole")
		{
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x000829FB File Offset: 0x00080BFB
		public SetExchangeServerOSPRoleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00082A0A File Offset: 0x00080C0A
		public virtual SetExchangeServerOSPRoleCommand SetParameters(SetExchangeServerOSPRoleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x00082A14 File Offset: 0x00080C14
		public virtual SetExchangeServerOSPRoleCommand SetParameters(SetExchangeServerOSPRoleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200065E RID: 1630
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170031D3 RID: 12755
			// (set) Token: 0x060052D8 RID: 21208 RVA: 0x00082A1E File Offset: 0x00080C1E
			public virtual ServerRole ServerRole
			{
				set
				{
					base.PowerSharpParameters["ServerRole"] = value;
				}
			}

			// Token: 0x170031D4 RID: 12756
			// (set) Token: 0x060052D9 RID: 21209 RVA: 0x00082A36 File Offset: 0x00080C36
			public virtual SwitchParameter Remove
			{
				set
				{
					base.PowerSharpParameters["Remove"] = value;
				}
			}

			// Token: 0x170031D5 RID: 12757
			// (set) Token: 0x060052DA RID: 21210 RVA: 0x00082A4E File Offset: 0x00080C4E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170031D6 RID: 12758
			// (set) Token: 0x060052DB RID: 21211 RVA: 0x00082A61 File Offset: 0x00080C61
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x170031D7 RID: 12759
			// (set) Token: 0x060052DC RID: 21212 RVA: 0x00082A79 File Offset: 0x00080C79
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x170031D8 RID: 12760
			// (set) Token: 0x060052DD RID: 21213 RVA: 0x00082A91 File Offset: 0x00080C91
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x170031D9 RID: 12761
			// (set) Token: 0x060052DE RID: 21214 RVA: 0x00082AA9 File Offset: 0x00080CA9
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x170031DA RID: 12762
			// (set) Token: 0x060052DF RID: 21215 RVA: 0x00082AC1 File Offset: 0x00080CC1
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x170031DB RID: 12763
			// (set) Token: 0x060052E0 RID: 21216 RVA: 0x00082AD9 File Offset: 0x00080CD9
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x170031DC RID: 12764
			// (set) Token: 0x060052E1 RID: 21217 RVA: 0x00082AF1 File Offset: 0x00080CF1
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x170031DD RID: 12765
			// (set) Token: 0x060052E2 RID: 21218 RVA: 0x00082B09 File Offset: 0x00080D09
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x170031DE RID: 12766
			// (set) Token: 0x060052E3 RID: 21219 RVA: 0x00082B21 File Offset: 0x00080D21
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x170031DF RID: 12767
			// (set) Token: 0x060052E4 RID: 21220 RVA: 0x00082B39 File Offset: 0x00080D39
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x170031E0 RID: 12768
			// (set) Token: 0x060052E5 RID: 21221 RVA: 0x00082B4C File Offset: 0x00080D4C
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x170031E1 RID: 12769
			// (set) Token: 0x060052E6 RID: 21222 RVA: 0x00082B5F File Offset: 0x00080D5F
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x170031E2 RID: 12770
			// (set) Token: 0x060052E7 RID: 21223 RVA: 0x00082B77 File Offset: 0x00080D77
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170031E3 RID: 12771
			// (set) Token: 0x060052E8 RID: 21224 RVA: 0x00082B8F File Offset: 0x00080D8F
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031E4 RID: 12772
			// (set) Token: 0x060052E9 RID: 21225 RVA: 0x00082BA7 File Offset: 0x00080DA7
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031E5 RID: 12773
			// (set) Token: 0x060052EA RID: 21226 RVA: 0x00082BBF File Offset: 0x00080DBF
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170031E6 RID: 12774
			// (set) Token: 0x060052EB RID: 21227 RVA: 0x00082BD7 File Offset: 0x00080DD7
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031E7 RID: 12775
			// (set) Token: 0x060052EC RID: 21228 RVA: 0x00082BEF File Offset: 0x00080DEF
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031E8 RID: 12776
			// (set) Token: 0x060052ED RID: 21229 RVA: 0x00082C07 File Offset: 0x00080E07
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x170031E9 RID: 12777
			// (set) Token: 0x060052EE RID: 21230 RVA: 0x00082C1F File Offset: 0x00080E1F
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170031EA RID: 12778
			// (set) Token: 0x060052EF RID: 21231 RVA: 0x00082C37 File Offset: 0x00080E37
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x170031EB RID: 12779
			// (set) Token: 0x060052F0 RID: 21232 RVA: 0x00082C4A File Offset: 0x00080E4A
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170031EC RID: 12780
			// (set) Token: 0x060052F1 RID: 21233 RVA: 0x00082C62 File Offset: 0x00080E62
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x170031ED RID: 12781
			// (set) Token: 0x060052F2 RID: 21234 RVA: 0x00082C7A File Offset: 0x00080E7A
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x170031EE RID: 12782
			// (set) Token: 0x060052F3 RID: 21235 RVA: 0x00082C92 File Offset: 0x00080E92
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x170031EF RID: 12783
			// (set) Token: 0x060052F4 RID: 21236 RVA: 0x00082CA5 File Offset: 0x00080EA5
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x170031F0 RID: 12784
			// (set) Token: 0x060052F5 RID: 21237 RVA: 0x00082CB8 File Offset: 0x00080EB8
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x170031F1 RID: 12785
			// (set) Token: 0x060052F6 RID: 21238 RVA: 0x00082CD0 File Offset: 0x00080ED0
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x170031F2 RID: 12786
			// (set) Token: 0x060052F7 RID: 21239 RVA: 0x00082CE8 File Offset: 0x00080EE8
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x170031F3 RID: 12787
			// (set) Token: 0x060052F8 RID: 21240 RVA: 0x00082D00 File Offset: 0x00080F00
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x170031F4 RID: 12788
			// (set) Token: 0x060052F9 RID: 21241 RVA: 0x00082D18 File Offset: 0x00080F18
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x170031F5 RID: 12789
			// (set) Token: 0x060052FA RID: 21242 RVA: 0x00082D2B File Offset: 0x00080F2B
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x170031F6 RID: 12790
			// (set) Token: 0x060052FB RID: 21243 RVA: 0x00082D43 File Offset: 0x00080F43
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031F7 RID: 12791
			// (set) Token: 0x060052FC RID: 21244 RVA: 0x00082D5B File Offset: 0x00080F5B
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031F8 RID: 12792
			// (set) Token: 0x060052FD RID: 21245 RVA: 0x00082D73 File Offset: 0x00080F73
			public virtual string MigrationLogExtensionData
			{
				set
				{
					base.PowerSharpParameters["MigrationLogExtensionData"] = value;
				}
			}

			// Token: 0x170031F9 RID: 12793
			// (set) Token: 0x060052FE RID: 21246 RVA: 0x00082D86 File Offset: 0x00080F86
			public virtual MigrationEventType MigrationLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MigrationLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170031FA RID: 12794
			// (set) Token: 0x060052FF RID: 21247 RVA: 0x00082D9E File Offset: 0x00080F9E
			public virtual LocalLongFullPath MigrationLogFilePath
			{
				set
				{
					base.PowerSharpParameters["MigrationLogFilePath"] = value;
				}
			}

			// Token: 0x170031FB RID: 12795
			// (set) Token: 0x06005300 RID: 21248 RVA: 0x00082DB1 File Offset: 0x00080FB1
			public virtual EnhancedTimeSpan MigrationLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxAge"] = value;
				}
			}

			// Token: 0x170031FC RID: 12796
			// (set) Token: 0x06005301 RID: 21249 RVA: 0x00082DC9 File Offset: 0x00080FC9
			public virtual ByteQuantifiedSize MigrationLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170031FD RID: 12797
			// (set) Token: 0x06005302 RID: 21250 RVA: 0x00082DE1 File Offset: 0x00080FE1
			public virtual ByteQuantifiedSize MigrationLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170031FE RID: 12798
			// (set) Token: 0x06005303 RID: 21251 RVA: 0x00082DF9 File Offset: 0x00080FF9
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x170031FF RID: 12799
			// (set) Token: 0x06005304 RID: 21252 RVA: 0x00082E0C File Offset: 0x0008100C
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x17003200 RID: 12800
			// (set) Token: 0x06005305 RID: 21253 RVA: 0x00082E24 File Offset: 0x00081024
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003201 RID: 12801
			// (set) Token: 0x06005306 RID: 21254 RVA: 0x00082E3C File Offset: 0x0008103C
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003202 RID: 12802
			// (set) Token: 0x06005307 RID: 21255 RVA: 0x00082E54 File Offset: 0x00081054
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003203 RID: 12803
			// (set) Token: 0x06005308 RID: 21256 RVA: 0x00082E6C File Offset: 0x0008106C
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003204 RID: 12804
			// (set) Token: 0x06005309 RID: 21257 RVA: 0x00082E84 File Offset: 0x00081084
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003205 RID: 12805
			// (set) Token: 0x0600530A RID: 21258 RVA: 0x00082E9C File Offset: 0x0008109C
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x17003206 RID: 12806
			// (set) Token: 0x0600530B RID: 21259 RVA: 0x00082EAF File Offset: 0x000810AF
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003207 RID: 12807
			// (set) Token: 0x0600530C RID: 21260 RVA: 0x00082EC7 File Offset: 0x000810C7
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003208 RID: 12808
			// (set) Token: 0x0600530D RID: 21261 RVA: 0x00082EDF File Offset: 0x000810DF
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003209 RID: 12809
			// (set) Token: 0x0600530E RID: 21262 RVA: 0x00082EF7 File Offset: 0x000810F7
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x1700320A RID: 12810
			// (set) Token: 0x0600530F RID: 21263 RVA: 0x00082F0A File Offset: 0x0008110A
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x1700320B RID: 12811
			// (set) Token: 0x06005310 RID: 21264 RVA: 0x00082F22 File Offset: 0x00081122
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x1700320C RID: 12812
			// (set) Token: 0x06005311 RID: 21265 RVA: 0x00082F35 File Offset: 0x00081135
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x1700320D RID: 12813
			// (set) Token: 0x06005312 RID: 21266 RVA: 0x00082F4D File Offset: 0x0008114D
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700320E RID: 12814
			// (set) Token: 0x06005313 RID: 21267 RVA: 0x00082F65 File Offset: 0x00081165
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700320F RID: 12815
			// (set) Token: 0x06005314 RID: 21268 RVA: 0x00082F7D File Offset: 0x0008117D
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x17003210 RID: 12816
			// (set) Token: 0x06005315 RID: 21269 RVA: 0x00082F90 File Offset: 0x00081190
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x17003211 RID: 12817
			// (set) Token: 0x06005316 RID: 21270 RVA: 0x00082FA3 File Offset: 0x000811A3
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x17003212 RID: 12818
			// (set) Token: 0x06005317 RID: 21271 RVA: 0x00082FBB File Offset: 0x000811BB
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x17003213 RID: 12819
			// (set) Token: 0x06005318 RID: 21272 RVA: 0x00082FD3 File Offset: 0x000811D3
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17003214 RID: 12820
			// (set) Token: 0x06005319 RID: 21273 RVA: 0x00082FEB File Offset: 0x000811EB
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x17003215 RID: 12821
			// (set) Token: 0x0600531A RID: 21274 RVA: 0x00083003 File Offset: 0x00081203
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003216 RID: 12822
			// (set) Token: 0x0600531B RID: 21275 RVA: 0x0008301B File Offset: 0x0008121B
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x17003217 RID: 12823
			// (set) Token: 0x0600531C RID: 21276 RVA: 0x0008302E File Offset: 0x0008122E
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17003218 RID: 12824
			// (set) Token: 0x0600531D RID: 21277 RVA: 0x00083046 File Offset: 0x00081246
			public virtual ProtocolLoggingLevel InMemoryReceiveConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17003219 RID: 12825
			// (set) Token: 0x0600531E RID: 21278 RVA: 0x0008305E File Offset: 0x0008125E
			public virtual bool InMemoryReceiveConnectorSmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorSmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x1700321A RID: 12826
			// (set) Token: 0x0600531F RID: 21279 RVA: 0x00083076 File Offset: 0x00081276
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x1700321B RID: 12827
			// (set) Token: 0x06005320 RID: 21280 RVA: 0x0008308E File Offset: 0x0008128E
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x1700321C RID: 12828
			// (set) Token: 0x06005321 RID: 21281 RVA: 0x000830A6 File Offset: 0x000812A6
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x1700321D RID: 12829
			// (set) Token: 0x06005322 RID: 21282 RVA: 0x000830BE File Offset: 0x000812BE
			public virtual bool GatewayEdgeSyncSubscribed
			{
				set
				{
					base.PowerSharpParameters["GatewayEdgeSyncSubscribed"] = value;
				}
			}

			// Token: 0x1700321E RID: 12830
			// (set) Token: 0x06005323 RID: 21283 RVA: 0x000830D6 File Offset: 0x000812D6
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x1700321F RID: 12831
			// (set) Token: 0x06005324 RID: 21284 RVA: 0x000830EE File Offset: 0x000812EE
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x17003220 RID: 12832
			// (set) Token: 0x06005325 RID: 21285 RVA: 0x00083106 File Offset: 0x00081306
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x17003221 RID: 12833
			// (set) Token: 0x06005326 RID: 21286 RVA: 0x0008311E File Offset: 0x0008131E
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x17003222 RID: 12834
			// (set) Token: 0x06005327 RID: 21287 RVA: 0x00083131 File Offset: 0x00081331
			public virtual int? MaxCallsAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxCallsAllowed"] = value;
				}
			}

			// Token: 0x17003223 RID: 12835
			// (set) Token: 0x06005328 RID: 21288 RVA: 0x00083149 File Offset: 0x00081349
			public virtual ServerStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17003224 RID: 12836
			// (set) Token: 0x06005329 RID: 21289 RVA: 0x00083161 File Offset: 0x00081361
			public virtual ScheduleInterval GrammarGenerationSchedule
			{
				set
				{
					base.PowerSharpParameters["GrammarGenerationSchedule"] = value;
				}
			}

			// Token: 0x17003225 RID: 12837
			// (set) Token: 0x0600532A RID: 21290 RVA: 0x00083179 File Offset: 0x00081379
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x17003226 RID: 12838
			// (set) Token: 0x0600532B RID: 21291 RVA: 0x00083191 File Offset: 0x00081391
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x17003227 RID: 12839
			// (set) Token: 0x0600532C RID: 21292 RVA: 0x000831A9 File Offset: 0x000813A9
			public virtual bool DatabaseCopyActivationDisabledAndMoveNow
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyActivationDisabledAndMoveNow"] = value;
				}
			}

			// Token: 0x17003228 RID: 12840
			// (set) Token: 0x0600532D RID: 21293 RVA: 0x000831C1 File Offset: 0x000813C1
			public virtual string FaultZone
			{
				set
				{
					base.PowerSharpParameters["FaultZone"] = value;
				}
			}

			// Token: 0x17003229 RID: 12841
			// (set) Token: 0x0600532E RID: 21294 RVA: 0x000831D4 File Offset: 0x000813D4
			public virtual bool AutoDagServerConfigured
			{
				set
				{
					base.PowerSharpParameters["AutoDagServerConfigured"] = value;
				}
			}

			// Token: 0x1700322A RID: 12842
			// (set) Token: 0x0600532F RID: 21295 RVA: 0x000831EC File Offset: 0x000813EC
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700322B RID: 12843
			// (set) Token: 0x06005330 RID: 21296 RVA: 0x00083204 File Offset: 0x00081404
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x1700322C RID: 12844
			// (set) Token: 0x06005331 RID: 21297 RVA: 0x0008321C File Offset: 0x0008141C
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x1700322D RID: 12845
			// (set) Token: 0x06005332 RID: 21298 RVA: 0x00083234 File Offset: 0x00081434
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x1700322E RID: 12846
			// (set) Token: 0x06005333 RID: 21299 RVA: 0x0008324C File Offset: 0x0008144C
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x1700322F RID: 12847
			// (set) Token: 0x06005334 RID: 21300 RVA: 0x00083264 File Offset: 0x00081464
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x17003230 RID: 12848
			// (set) Token: 0x06005335 RID: 21301 RVA: 0x0008327C File Offset: 0x0008147C
			public virtual bool TransportSyncDispatchEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncDispatchEnabled"] = value;
				}
			}

			// Token: 0x17003231 RID: 12849
			// (set) Token: 0x06005336 RID: 21302 RVA: 0x00083294 File Offset: 0x00081494
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x17003232 RID: 12850
			// (set) Token: 0x06005337 RID: 21303 RVA: 0x000832AC File Offset: 0x000814AC
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x17003233 RID: 12851
			// (set) Token: 0x06005338 RID: 21304 RVA: 0x000832C4 File Offset: 0x000814C4
			public virtual int MaxAcceptedTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxAcceptedTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x17003234 RID: 12852
			// (set) Token: 0x06005339 RID: 21305 RVA: 0x000832DC File Offset: 0x000814DC
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x17003235 RID: 12853
			// (set) Token: 0x0600533A RID: 21306 RVA: 0x000832F4 File Offset: 0x000814F4
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x17003236 RID: 12854
			// (set) Token: 0x0600533B RID: 21307 RVA: 0x00083307 File Offset: 0x00081507
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x17003237 RID: 12855
			// (set) Token: 0x0600533C RID: 21308 RVA: 0x0008331F File Offset: 0x0008151F
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x17003238 RID: 12856
			// (set) Token: 0x0600533D RID: 21309 RVA: 0x00083332 File Offset: 0x00081532
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003239 RID: 12857
			// (set) Token: 0x0600533E RID: 21310 RVA: 0x0008334A File Offset: 0x0008154A
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700323A RID: 12858
			// (set) Token: 0x0600533F RID: 21311 RVA: 0x00083362 File Offset: 0x00081562
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700323B RID: 12859
			// (set) Token: 0x06005340 RID: 21312 RVA: 0x0008337A File Offset: 0x0008157A
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x1700323C RID: 12860
			// (set) Token: 0x06005341 RID: 21313 RVA: 0x00083392 File Offset: 0x00081592
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x1700323D RID: 12861
			// (set) Token: 0x06005342 RID: 21314 RVA: 0x000833AA File Offset: 0x000815AA
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x1700323E RID: 12862
			// (set) Token: 0x06005343 RID: 21315 RVA: 0x000833C2 File Offset: 0x000815C2
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x1700323F RID: 12863
			// (set) Token: 0x06005344 RID: 21316 RVA: 0x000833D5 File Offset: 0x000815D5
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x17003240 RID: 12864
			// (set) Token: 0x06005345 RID: 21317 RVA: 0x000833ED File Offset: 0x000815ED
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003241 RID: 12865
			// (set) Token: 0x06005346 RID: 21318 RVA: 0x00083405 File Offset: 0x00081605
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003242 RID: 12866
			// (set) Token: 0x06005347 RID: 21319 RVA: 0x0008341D File Offset: 0x0008161D
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x17003243 RID: 12867
			// (set) Token: 0x06005348 RID: 21320 RVA: 0x00083435 File Offset: 0x00081635
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x17003244 RID: 12868
			// (set) Token: 0x06005349 RID: 21321 RVA: 0x00083448 File Offset: 0x00081648
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x17003245 RID: 12869
			// (set) Token: 0x0600534A RID: 21322 RVA: 0x00083460 File Offset: 0x00081660
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003246 RID: 12870
			// (set) Token: 0x0600534B RID: 21323 RVA: 0x00083478 File Offset: 0x00081678
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003247 RID: 12871
			// (set) Token: 0x0600534C RID: 21324 RVA: 0x00083490 File Offset: 0x00081690
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x17003248 RID: 12872
			// (set) Token: 0x0600534D RID: 21325 RVA: 0x000834A8 File Offset: 0x000816A8
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x17003249 RID: 12873
			// (set) Token: 0x0600534E RID: 21326 RVA: 0x000834C0 File Offset: 0x000816C0
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x1700324A RID: 12874
			// (set) Token: 0x0600534F RID: 21327 RVA: 0x000834D8 File Offset: 0x000816D8
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x1700324B RID: 12875
			// (set) Token: 0x06005350 RID: 21328 RVA: 0x000834F0 File Offset: 0x000816F0
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700324C RID: 12876
			// (set) Token: 0x06005351 RID: 21329 RVA: 0x00083508 File Offset: 0x00081708
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x1700324D RID: 12877
			// (set) Token: 0x06005352 RID: 21330 RVA: 0x00083520 File Offset: 0x00081720
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x1700324E RID: 12878
			// (set) Token: 0x06005353 RID: 21331 RVA: 0x00083538 File Offset: 0x00081738
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x1700324F RID: 12879
			// (set) Token: 0x06005354 RID: 21332 RVA: 0x00083550 File Offset: 0x00081750
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x17003250 RID: 12880
			// (set) Token: 0x06005355 RID: 21333 RVA: 0x00083563 File Offset: 0x00081763
			public virtual int MaxTransportSyncDispatchers
			{
				set
				{
					base.PowerSharpParameters["MaxTransportSyncDispatchers"] = value;
				}
			}

			// Token: 0x17003251 RID: 12881
			// (set) Token: 0x06005356 RID: 21334 RVA: 0x0008357B File Offset: 0x0008177B
			public virtual bool TransportSyncMailboxLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogEnabled"] = value;
				}
			}

			// Token: 0x17003252 RID: 12882
			// (set) Token: 0x06005357 RID: 21335 RVA: 0x00083593 File Offset: 0x00081793
			public virtual SyncLoggingLevel TransportSyncMailboxLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003253 RID: 12883
			// (set) Token: 0x06005358 RID: 21336 RVA: 0x000835AB File Offset: 0x000817AB
			public virtual LocalLongFullPath TransportSyncMailboxLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogFilePath"] = value;
				}
			}

			// Token: 0x17003254 RID: 12884
			// (set) Token: 0x06005359 RID: 21337 RVA: 0x000835BE File Offset: 0x000817BE
			public virtual EnhancedTimeSpan TransportSyncMailboxLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxAge"] = value;
				}
			}

			// Token: 0x17003255 RID: 12885
			// (set) Token: 0x0600535A RID: 21338 RVA: 0x000835D6 File Offset: 0x000817D6
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003256 RID: 12886
			// (set) Token: 0x0600535B RID: 21339 RVA: 0x000835EE File Offset: 0x000817EE
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003257 RID: 12887
			// (set) Token: 0x0600535C RID: 21340 RVA: 0x00083606 File Offset: 0x00081806
			public virtual bool TransportSyncMailboxHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogEnabled"] = value;
				}
			}

			// Token: 0x17003258 RID: 12888
			// (set) Token: 0x0600535D RID: 21341 RVA: 0x0008361E File Offset: 0x0008181E
			public virtual LocalLongFullPath TransportSyncMailboxHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogFilePath"] = value;
				}
			}

			// Token: 0x17003259 RID: 12889
			// (set) Token: 0x0600535E RID: 21342 RVA: 0x00083631 File Offset: 0x00081831
			public virtual EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x1700325A RID: 12890
			// (set) Token: 0x0600535F RID: 21343 RVA: 0x00083649 File Offset: 0x00081849
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700325B RID: 12891
			// (set) Token: 0x06005360 RID: 21344 RVA: 0x00083661 File Offset: 0x00081861
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700325C RID: 12892
			// (set) Token: 0x06005361 RID: 21345 RVA: 0x00083679 File Offset: 0x00081879
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700325D RID: 12893
			// (set) Token: 0x06005362 RID: 21346 RVA: 0x0008368C File Offset: 0x0008188C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700325E RID: 12894
			// (set) Token: 0x06005363 RID: 21347 RVA: 0x000836A4 File Offset: 0x000818A4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700325F RID: 12895
			// (set) Token: 0x06005364 RID: 21348 RVA: 0x000836BC File Offset: 0x000818BC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003260 RID: 12896
			// (set) Token: 0x06005365 RID: 21349 RVA: 0x000836D4 File Offset: 0x000818D4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200065F RID: 1631
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003261 RID: 12897
			// (set) Token: 0x06005367 RID: 21351 RVA: 0x000836F4 File Offset: 0x000818F4
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003262 RID: 12898
			// (set) Token: 0x06005368 RID: 21352 RVA: 0x00083707 File Offset: 0x00081907
			public virtual ServerRole ServerRole
			{
				set
				{
					base.PowerSharpParameters["ServerRole"] = value;
				}
			}

			// Token: 0x17003263 RID: 12899
			// (set) Token: 0x06005369 RID: 21353 RVA: 0x0008371F File Offset: 0x0008191F
			public virtual SwitchParameter Remove
			{
				set
				{
					base.PowerSharpParameters["Remove"] = value;
				}
			}

			// Token: 0x17003264 RID: 12900
			// (set) Token: 0x0600536A RID: 21354 RVA: 0x00083737 File Offset: 0x00081937
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003265 RID: 12901
			// (set) Token: 0x0600536B RID: 21355 RVA: 0x0008374A File Offset: 0x0008194A
			public virtual EnhancedTimeSpan DelayNotificationTimeout
			{
				set
				{
					base.PowerSharpParameters["DelayNotificationTimeout"] = value;
				}
			}

			// Token: 0x17003266 RID: 12902
			// (set) Token: 0x0600536C RID: 21356 RVA: 0x00083762 File Offset: 0x00081962
			public virtual EnhancedTimeSpan MessageExpirationTimeout
			{
				set
				{
					base.PowerSharpParameters["MessageExpirationTimeout"] = value;
				}
			}

			// Token: 0x17003267 RID: 12903
			// (set) Token: 0x0600536D RID: 21357 RVA: 0x0008377A File Offset: 0x0008197A
			public virtual EnhancedTimeSpan QueueMaxIdleTime
			{
				set
				{
					base.PowerSharpParameters["QueueMaxIdleTime"] = value;
				}
			}

			// Token: 0x17003268 RID: 12904
			// (set) Token: 0x0600536E RID: 21358 RVA: 0x00083792 File Offset: 0x00081992
			public virtual EnhancedTimeSpan MessageRetryInterval
			{
				set
				{
					base.PowerSharpParameters["MessageRetryInterval"] = value;
				}
			}

			// Token: 0x17003269 RID: 12905
			// (set) Token: 0x0600536F RID: 21359 RVA: 0x000837AA File Offset: 0x000819AA
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x1700326A RID: 12906
			// (set) Token: 0x06005370 RID: 21360 RVA: 0x000837C2 File Offset: 0x000819C2
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x1700326B RID: 12907
			// (set) Token: 0x06005371 RID: 21361 RVA: 0x000837DA File Offset: 0x000819DA
			public virtual Unlimited<int> MaxOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxOutboundConnections"] = value;
				}
			}

			// Token: 0x1700326C RID: 12908
			// (set) Token: 0x06005372 RID: 21362 RVA: 0x000837F2 File Offset: 0x000819F2
			public virtual Unlimited<int> MaxPerDomainOutboundConnections
			{
				set
				{
					base.PowerSharpParameters["MaxPerDomainOutboundConnections"] = value;
				}
			}

			// Token: 0x1700326D RID: 12909
			// (set) Token: 0x06005373 RID: 21363 RVA: 0x0008380A File Offset: 0x00081A0A
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x1700326E RID: 12910
			// (set) Token: 0x06005374 RID: 21364 RVA: 0x00083822 File Offset: 0x00081A22
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x1700326F RID: 12911
			// (set) Token: 0x06005375 RID: 21365 RVA: 0x00083835 File Offset: 0x00081A35
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003270 RID: 12912
			// (set) Token: 0x06005376 RID: 21366 RVA: 0x00083848 File Offset: 0x00081A48
			public virtual EnhancedTimeSpan OutboundConnectionFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["OutboundConnectionFailureRetryInterval"] = value;
				}
			}

			// Token: 0x17003271 RID: 12913
			// (set) Token: 0x06005377 RID: 21367 RVA: 0x00083860 File Offset: 0x00081A60
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003272 RID: 12914
			// (set) Token: 0x06005378 RID: 21368 RVA: 0x00083878 File Offset: 0x00081A78
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003273 RID: 12915
			// (set) Token: 0x06005379 RID: 21369 RVA: 0x00083890 File Offset: 0x00081A90
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003274 RID: 12916
			// (set) Token: 0x0600537A RID: 21370 RVA: 0x000838A8 File Offset: 0x00081AA8
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003275 RID: 12917
			// (set) Token: 0x0600537B RID: 21371 RVA: 0x000838C0 File Offset: 0x00081AC0
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003276 RID: 12918
			// (set) Token: 0x0600537C RID: 21372 RVA: 0x000838D8 File Offset: 0x00081AD8
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003277 RID: 12919
			// (set) Token: 0x0600537D RID: 21373 RVA: 0x000838F0 File Offset: 0x00081AF0
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x17003278 RID: 12920
			// (set) Token: 0x0600537E RID: 21374 RVA: 0x00083908 File Offset: 0x00081B08
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x17003279 RID: 12921
			// (set) Token: 0x0600537F RID: 21375 RVA: 0x00083920 File Offset: 0x00081B20
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x1700327A RID: 12922
			// (set) Token: 0x06005380 RID: 21376 RVA: 0x00083933 File Offset: 0x00081B33
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x1700327B RID: 12923
			// (set) Token: 0x06005381 RID: 21377 RVA: 0x0008394B File Offset: 0x00081B4B
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700327C RID: 12924
			// (set) Token: 0x06005382 RID: 21378 RVA: 0x00083963 File Offset: 0x00081B63
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x1700327D RID: 12925
			// (set) Token: 0x06005383 RID: 21379 RVA: 0x0008397B File Offset: 0x00081B7B
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x1700327E RID: 12926
			// (set) Token: 0x06005384 RID: 21380 RVA: 0x0008398E File Offset: 0x00081B8E
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x1700327F RID: 12927
			// (set) Token: 0x06005385 RID: 21381 RVA: 0x000839A1 File Offset: 0x00081BA1
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x17003280 RID: 12928
			// (set) Token: 0x06005386 RID: 21382 RVA: 0x000839B9 File Offset: 0x00081BB9
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x17003281 RID: 12929
			// (set) Token: 0x06005387 RID: 21383 RVA: 0x000839D1 File Offset: 0x00081BD1
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x17003282 RID: 12930
			// (set) Token: 0x06005388 RID: 21384 RVA: 0x000839E9 File Offset: 0x00081BE9
			public virtual int PoisonThreshold
			{
				set
				{
					base.PowerSharpParameters["PoisonThreshold"] = value;
				}
			}

			// Token: 0x17003283 RID: 12931
			// (set) Token: 0x06005389 RID: 21385 RVA: 0x00083A01 File Offset: 0x00081C01
			public virtual LocalLongFullPath MessageTrackingLogPath
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogPath"] = value;
				}
			}

			// Token: 0x17003284 RID: 12932
			// (set) Token: 0x0600538A RID: 21386 RVA: 0x00083A14 File Offset: 0x00081C14
			public virtual EnhancedTimeSpan MessageTrackingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxAge"] = value;
				}
			}

			// Token: 0x17003285 RID: 12933
			// (set) Token: 0x0600538B RID: 21387 RVA: 0x00083A2C File Offset: 0x00081C2C
			public virtual Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003286 RID: 12934
			// (set) Token: 0x0600538C RID: 21388 RVA: 0x00083A44 File Offset: 0x00081C44
			public virtual ByteQuantifiedSize MessageTrackingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003287 RID: 12935
			// (set) Token: 0x0600538D RID: 21389 RVA: 0x00083A5C File Offset: 0x00081C5C
			public virtual string MigrationLogExtensionData
			{
				set
				{
					base.PowerSharpParameters["MigrationLogExtensionData"] = value;
				}
			}

			// Token: 0x17003288 RID: 12936
			// (set) Token: 0x0600538E RID: 21390 RVA: 0x00083A6F File Offset: 0x00081C6F
			public virtual MigrationEventType MigrationLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MigrationLogLoggingLevel"] = value;
				}
			}

			// Token: 0x17003289 RID: 12937
			// (set) Token: 0x0600538F RID: 21391 RVA: 0x00083A87 File Offset: 0x00081C87
			public virtual LocalLongFullPath MigrationLogFilePath
			{
				set
				{
					base.PowerSharpParameters["MigrationLogFilePath"] = value;
				}
			}

			// Token: 0x1700328A RID: 12938
			// (set) Token: 0x06005390 RID: 21392 RVA: 0x00083A9A File Offset: 0x00081C9A
			public virtual EnhancedTimeSpan MigrationLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxAge"] = value;
				}
			}

			// Token: 0x1700328B RID: 12939
			// (set) Token: 0x06005391 RID: 21393 RVA: 0x00083AB2 File Offset: 0x00081CB2
			public virtual ByteQuantifiedSize MigrationLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700328C RID: 12940
			// (set) Token: 0x06005392 RID: 21394 RVA: 0x00083ACA File Offset: 0x00081CCA
			public virtual ByteQuantifiedSize MigrationLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MigrationLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700328D RID: 12941
			// (set) Token: 0x06005393 RID: 21395 RVA: 0x00083AE2 File Offset: 0x00081CE2
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x1700328E RID: 12942
			// (set) Token: 0x06005394 RID: 21396 RVA: 0x00083AF5 File Offset: 0x00081CF5
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x1700328F RID: 12943
			// (set) Token: 0x06005395 RID: 21397 RVA: 0x00083B0D File Offset: 0x00081D0D
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003290 RID: 12944
			// (set) Token: 0x06005396 RID: 21398 RVA: 0x00083B25 File Offset: 0x00081D25
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003291 RID: 12945
			// (set) Token: 0x06005397 RID: 21399 RVA: 0x00083B3D File Offset: 0x00081D3D
			public virtual EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003292 RID: 12946
			// (set) Token: 0x06005398 RID: 21400 RVA: 0x00083B55 File Offset: 0x00081D55
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003293 RID: 12947
			// (set) Token: 0x06005399 RID: 21401 RVA: 0x00083B6D File Offset: 0x00081D6D
			public virtual ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003294 RID: 12948
			// (set) Token: 0x0600539A RID: 21402 RVA: 0x00083B85 File Offset: 0x00081D85
			public virtual LocalLongFullPath ActiveUserStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ActiveUserStatisticsLogPath"] = value;
				}
			}

			// Token: 0x17003295 RID: 12949
			// (set) Token: 0x0600539B RID: 21403 RVA: 0x00083B98 File Offset: 0x00081D98
			public virtual EnhancedTimeSpan ServerStatisticsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003296 RID: 12950
			// (set) Token: 0x0600539C RID: 21404 RVA: 0x00083BB0 File Offset: 0x00081DB0
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003297 RID: 12951
			// (set) Token: 0x0600539D RID: 21405 RVA: 0x00083BC8 File Offset: 0x00081DC8
			public virtual ByteQuantifiedSize ServerStatisticsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003298 RID: 12952
			// (set) Token: 0x0600539E RID: 21406 RVA: 0x00083BE0 File Offset: 0x00081DE0
			public virtual LocalLongFullPath ServerStatisticsLogPath
			{
				set
				{
					base.PowerSharpParameters["ServerStatisticsLogPath"] = value;
				}
			}

			// Token: 0x17003299 RID: 12953
			// (set) Token: 0x0600539F RID: 21407 RVA: 0x00083BF3 File Offset: 0x00081DF3
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x1700329A RID: 12954
			// (set) Token: 0x060053A0 RID: 21408 RVA: 0x00083C0B File Offset: 0x00081E0B
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x1700329B RID: 12955
			// (set) Token: 0x060053A1 RID: 21409 RVA: 0x00083C1E File Offset: 0x00081E1E
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x1700329C RID: 12956
			// (set) Token: 0x060053A2 RID: 21410 RVA: 0x00083C36 File Offset: 0x00081E36
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700329D RID: 12957
			// (set) Token: 0x060053A3 RID: 21411 RVA: 0x00083C4E File Offset: 0x00081E4E
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700329E RID: 12958
			// (set) Token: 0x060053A4 RID: 21412 RVA: 0x00083C66 File Offset: 0x00081E66
			public virtual LocalLongFullPath PickupDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryPath"] = value;
				}
			}

			// Token: 0x1700329F RID: 12959
			// (set) Token: 0x060053A5 RID: 21413 RVA: 0x00083C79 File Offset: 0x00081E79
			public virtual LocalLongFullPath ReplayDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["ReplayDirectoryPath"] = value;
				}
			}

			// Token: 0x170032A0 RID: 12960
			// (set) Token: 0x060053A6 RID: 21414 RVA: 0x00083C8C File Offset: 0x00081E8C
			public virtual int PickupDirectoryMaxMessagesPerMinute
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxMessagesPerMinute"] = value;
				}
			}

			// Token: 0x170032A1 RID: 12961
			// (set) Token: 0x060053A7 RID: 21415 RVA: 0x00083CA4 File Offset: 0x00081EA4
			public virtual ByteQuantifiedSize PickupDirectoryMaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxHeaderSize"] = value;
				}
			}

			// Token: 0x170032A2 RID: 12962
			// (set) Token: 0x060053A8 RID: 21416 RVA: 0x00083CBC File Offset: 0x00081EBC
			public virtual int PickupDirectoryMaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["PickupDirectoryMaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x170032A3 RID: 12963
			// (set) Token: 0x060053A9 RID: 21417 RVA: 0x00083CD4 File Offset: 0x00081ED4
			public virtual EnhancedTimeSpan RoutingTableLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxAge"] = value;
				}
			}

			// Token: 0x170032A4 RID: 12964
			// (set) Token: 0x060053AA RID: 21418 RVA: 0x00083CEC File Offset: 0x00081EEC
			public virtual Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170032A5 RID: 12965
			// (set) Token: 0x060053AB RID: 21419 RVA: 0x00083D04 File Offset: 0x00081F04
			public virtual LocalLongFullPath RoutingTableLogPath
			{
				set
				{
					base.PowerSharpParameters["RoutingTableLogPath"] = value;
				}
			}

			// Token: 0x170032A6 RID: 12966
			// (set) Token: 0x060053AC RID: 21420 RVA: 0x00083D17 File Offset: 0x00081F17
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170032A7 RID: 12967
			// (set) Token: 0x060053AD RID: 21421 RVA: 0x00083D2F File Offset: 0x00081F2F
			public virtual ProtocolLoggingLevel InMemoryReceiveConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170032A8 RID: 12968
			// (set) Token: 0x060053AE RID: 21422 RVA: 0x00083D47 File Offset: 0x00081F47
			public virtual bool InMemoryReceiveConnectorSmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["InMemoryReceiveConnectorSmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x170032A9 RID: 12969
			// (set) Token: 0x060053AF RID: 21423 RVA: 0x00083D5F File Offset: 0x00081F5F
			public virtual bool MessageTrackingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogEnabled"] = value;
				}
			}

			// Token: 0x170032AA RID: 12970
			// (set) Token: 0x060053B0 RID: 21424 RVA: 0x00083D77 File Offset: 0x00081F77
			public virtual bool MessageTrackingLogSubjectLoggingEnabled
			{
				set
				{
					base.PowerSharpParameters["MessageTrackingLogSubjectLoggingEnabled"] = value;
				}
			}

			// Token: 0x170032AB RID: 12971
			// (set) Token: 0x060053B1 RID: 21425 RVA: 0x00083D8F File Offset: 0x00081F8F
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x170032AC RID: 12972
			// (set) Token: 0x060053B2 RID: 21426 RVA: 0x00083DA7 File Offset: 0x00081FA7
			public virtual bool GatewayEdgeSyncSubscribed
			{
				set
				{
					base.PowerSharpParameters["GatewayEdgeSyncSubscribed"] = value;
				}
			}

			// Token: 0x170032AD RID: 12973
			// (set) Token: 0x060053B3 RID: 21427 RVA: 0x00083DBF File Offset: 0x00081FBF
			public virtual bool PoisonMessageDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["PoisonMessageDetectionEnabled"] = value;
				}
			}

			// Token: 0x170032AE RID: 12974
			// (set) Token: 0x060053B4 RID: 21428 RVA: 0x00083DD7 File Offset: 0x00081FD7
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x170032AF RID: 12975
			// (set) Token: 0x060053B5 RID: 21429 RVA: 0x00083DEF File Offset: 0x00081FEF
			public virtual bool RecipientValidationCacheEnabled
			{
				set
				{
					base.PowerSharpParameters["RecipientValidationCacheEnabled"] = value;
				}
			}

			// Token: 0x170032B0 RID: 12976
			// (set) Token: 0x060053B6 RID: 21430 RVA: 0x00083E07 File Offset: 0x00082007
			public virtual string RootDropDirectoryPath
			{
				set
				{
					base.PowerSharpParameters["RootDropDirectoryPath"] = value;
				}
			}

			// Token: 0x170032B1 RID: 12977
			// (set) Token: 0x060053B7 RID: 21431 RVA: 0x00083E1A File Offset: 0x0008201A
			public virtual int? MaxCallsAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxCallsAllowed"] = value;
				}
			}

			// Token: 0x170032B2 RID: 12978
			// (set) Token: 0x060053B8 RID: 21432 RVA: 0x00083E32 File Offset: 0x00082032
			public virtual ServerStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170032B3 RID: 12979
			// (set) Token: 0x060053B9 RID: 21433 RVA: 0x00083E4A File Offset: 0x0008204A
			public virtual ScheduleInterval GrammarGenerationSchedule
			{
				set
				{
					base.PowerSharpParameters["GrammarGenerationSchedule"] = value;
				}
			}

			// Token: 0x170032B4 RID: 12980
			// (set) Token: 0x060053BA RID: 21434 RVA: 0x00083E62 File Offset: 0x00082062
			public virtual AutoDatabaseMountDial AutoDatabaseMountDial
			{
				set
				{
					base.PowerSharpParameters["AutoDatabaseMountDial"] = value;
				}
			}

			// Token: 0x170032B5 RID: 12981
			// (set) Token: 0x060053BB RID: 21435 RVA: 0x00083E7A File Offset: 0x0008207A
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x170032B6 RID: 12982
			// (set) Token: 0x060053BC RID: 21436 RVA: 0x00083E92 File Offset: 0x00082092
			public virtual bool DatabaseCopyActivationDisabledAndMoveNow
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyActivationDisabledAndMoveNow"] = value;
				}
			}

			// Token: 0x170032B7 RID: 12983
			// (set) Token: 0x060053BD RID: 21437 RVA: 0x00083EAA File Offset: 0x000820AA
			public virtual string FaultZone
			{
				set
				{
					base.PowerSharpParameters["FaultZone"] = value;
				}
			}

			// Token: 0x170032B8 RID: 12984
			// (set) Token: 0x060053BE RID: 21438 RVA: 0x00083EBD File Offset: 0x000820BD
			public virtual bool AutoDagServerConfigured
			{
				set
				{
					base.PowerSharpParameters["AutoDagServerConfigured"] = value;
				}
			}

			// Token: 0x170032B9 RID: 12985
			// (set) Token: 0x060053BF RID: 21439 RVA: 0x00083ED5 File Offset: 0x000820D5
			public virtual bool TransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncEnabled"] = value;
				}
			}

			// Token: 0x170032BA RID: 12986
			// (set) Token: 0x060053C0 RID: 21440 RVA: 0x00083EED File Offset: 0x000820ED
			public virtual bool TransportSyncPopEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncPopEnabled"] = value;
				}
			}

			// Token: 0x170032BB RID: 12987
			// (set) Token: 0x060053C1 RID: 21441 RVA: 0x00083F05 File Offset: 0x00082105
			public virtual bool WindowsLiveHotmailTransportSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveHotmailTransportSyncEnabled"] = value;
				}
			}

			// Token: 0x170032BC RID: 12988
			// (set) Token: 0x060053C2 RID: 21442 RVA: 0x00083F1D File Offset: 0x0008211D
			public virtual bool TransportSyncExchangeEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncExchangeEnabled"] = value;
				}
			}

			// Token: 0x170032BD RID: 12989
			// (set) Token: 0x060053C3 RID: 21443 RVA: 0x00083F35 File Offset: 0x00082135
			public virtual bool TransportSyncImapEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncImapEnabled"] = value;
				}
			}

			// Token: 0x170032BE RID: 12990
			// (set) Token: 0x060053C4 RID: 21444 RVA: 0x00083F4D File Offset: 0x0008214D
			public virtual bool TransportSyncFacebookEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncFacebookEnabled"] = value;
				}
			}

			// Token: 0x170032BF RID: 12991
			// (set) Token: 0x060053C5 RID: 21445 RVA: 0x00083F65 File Offset: 0x00082165
			public virtual bool TransportSyncDispatchEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncDispatchEnabled"] = value;
				}
			}

			// Token: 0x170032C0 RID: 12992
			// (set) Token: 0x060053C6 RID: 21446 RVA: 0x00083F7D File Offset: 0x0008217D
			public virtual bool TransportSyncLinkedInEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLinkedInEnabled"] = value;
				}
			}

			// Token: 0x170032C1 RID: 12993
			// (set) Token: 0x060053C7 RID: 21447 RVA: 0x00083F95 File Offset: 0x00082195
			public virtual int MaxNumberOfTransportSyncAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfTransportSyncAttempts"] = value;
				}
			}

			// Token: 0x170032C2 RID: 12994
			// (set) Token: 0x060053C8 RID: 21448 RVA: 0x00083FAD File Offset: 0x000821AD
			public virtual int MaxAcceptedTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxAcceptedTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x170032C3 RID: 12995
			// (set) Token: 0x060053C9 RID: 21449 RVA: 0x00083FC5 File Offset: 0x000821C5
			public virtual int MaxActiveTransportSyncJobsPerProcessor
			{
				set
				{
					base.PowerSharpParameters["MaxActiveTransportSyncJobsPerProcessor"] = value;
				}
			}

			// Token: 0x170032C4 RID: 12996
			// (set) Token: 0x060053CA RID: 21450 RVA: 0x00083FDD File Offset: 0x000821DD
			public virtual string HttpTransportSyncProxyServer
			{
				set
				{
					base.PowerSharpParameters["HttpTransportSyncProxyServer"] = value;
				}
			}

			// Token: 0x170032C5 RID: 12997
			// (set) Token: 0x060053CB RID: 21451 RVA: 0x00083FF0 File Offset: 0x000821F0
			public virtual bool HttpProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x170032C6 RID: 12998
			// (set) Token: 0x060053CC RID: 21452 RVA: 0x00084008 File Offset: 0x00082208
			public virtual LocalLongFullPath HttpProtocolLogFilePath
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogFilePath"] = value;
				}
			}

			// Token: 0x170032C7 RID: 12999
			// (set) Token: 0x060053CD RID: 21453 RVA: 0x0008401B File Offset: 0x0008221B
			public virtual EnhancedTimeSpan HttpProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x170032C8 RID: 13000
			// (set) Token: 0x060053CE RID: 21454 RVA: 0x00084033 File Offset: 0x00082233
			public virtual ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170032C9 RID: 13001
			// (set) Token: 0x060053CF RID: 21455 RVA: 0x0008404B File Offset: 0x0008224B
			public virtual ByteQuantifiedSize HttpProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170032CA RID: 13002
			// (set) Token: 0x060053D0 RID: 21456 RVA: 0x00084063 File Offset: 0x00082263
			public virtual ProtocolLoggingLevel HttpProtocolLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["HttpProtocolLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170032CB RID: 13003
			// (set) Token: 0x060053D1 RID: 21457 RVA: 0x0008407B File Offset: 0x0008227B
			public virtual bool TransportSyncLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogEnabled"] = value;
				}
			}

			// Token: 0x170032CC RID: 13004
			// (set) Token: 0x060053D2 RID: 21458 RVA: 0x00084093 File Offset: 0x00082293
			public virtual SyncLoggingLevel TransportSyncLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170032CD RID: 13005
			// (set) Token: 0x060053D3 RID: 21459 RVA: 0x000840AB File Offset: 0x000822AB
			public virtual LocalLongFullPath TransportSyncLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogFilePath"] = value;
				}
			}

			// Token: 0x170032CE RID: 13006
			// (set) Token: 0x060053D4 RID: 21460 RVA: 0x000840BE File Offset: 0x000822BE
			public virtual EnhancedTimeSpan TransportSyncLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxAge"] = value;
				}
			}

			// Token: 0x170032CF RID: 13007
			// (set) Token: 0x060053D5 RID: 21461 RVA: 0x000840D6 File Offset: 0x000822D6
			public virtual ByteQuantifiedSize TransportSyncLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170032D0 RID: 13008
			// (set) Token: 0x060053D6 RID: 21462 RVA: 0x000840EE File Offset: 0x000822EE
			public virtual ByteQuantifiedSize TransportSyncLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170032D1 RID: 13009
			// (set) Token: 0x060053D7 RID: 21463 RVA: 0x00084106 File Offset: 0x00082306
			public virtual bool TransportSyncHubHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogEnabled"] = value;
				}
			}

			// Token: 0x170032D2 RID: 13010
			// (set) Token: 0x060053D8 RID: 21464 RVA: 0x0008411E File Offset: 0x0008231E
			public virtual LocalLongFullPath TransportSyncHubHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogFilePath"] = value;
				}
			}

			// Token: 0x170032D3 RID: 13011
			// (set) Token: 0x060053D9 RID: 21465 RVA: 0x00084131 File Offset: 0x00082331
			public virtual EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x170032D4 RID: 13012
			// (set) Token: 0x060053DA RID: 21466 RVA: 0x00084149 File Offset: 0x00082349
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170032D5 RID: 13013
			// (set) Token: 0x060053DB RID: 21467 RVA: 0x00084161 File Offset: 0x00082361
			public virtual ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncHubHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170032D6 RID: 13014
			// (set) Token: 0x060053DC RID: 21468 RVA: 0x00084179 File Offset: 0x00082379
			public virtual bool TransportSyncAccountsPoisonDetectionEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonDetectionEnabled"] = value;
				}
			}

			// Token: 0x170032D7 RID: 13015
			// (set) Token: 0x060053DD RID: 21469 RVA: 0x00084191 File Offset: 0x00082391
			public virtual int TransportSyncAccountsPoisonAccountThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonAccountThreshold"] = value;
				}
			}

			// Token: 0x170032D8 RID: 13016
			// (set) Token: 0x060053DE RID: 21470 RVA: 0x000841A9 File Offset: 0x000823A9
			public virtual int TransportSyncAccountsPoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsPoisonItemThreshold"] = value;
				}
			}

			// Token: 0x170032D9 RID: 13017
			// (set) Token: 0x060053DF RID: 21471 RVA: 0x000841C1 File Offset: 0x000823C1
			public virtual int TransportSyncAccountsSuccessivePoisonItemThreshold
			{
				set
				{
					base.PowerSharpParameters["TransportSyncAccountsSuccessivePoisonItemThreshold"] = value;
				}
			}

			// Token: 0x170032DA RID: 13018
			// (set) Token: 0x060053E0 RID: 21472 RVA: 0x000841D9 File Offset: 0x000823D9
			public virtual EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["TransportSyncRemoteConnectionTimeout"] = value;
				}
			}

			// Token: 0x170032DB RID: 13019
			// (set) Token: 0x060053E1 RID: 21473 RVA: 0x000841F1 File Offset: 0x000823F1
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerItem"] = value;
				}
			}

			// Token: 0x170032DC RID: 13020
			// (set) Token: 0x060053E2 RID: 21474 RVA: 0x00084209 File Offset: 0x00082409
			public virtual ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadSizePerConnection"] = value;
				}
			}

			// Token: 0x170032DD RID: 13021
			// (set) Token: 0x060053E3 RID: 21475 RVA: 0x00084221 File Offset: 0x00082421
			public virtual int TransportSyncMaxDownloadItemsPerConnection
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMaxDownloadItemsPerConnection"] = value;
				}
			}

			// Token: 0x170032DE RID: 13022
			// (set) Token: 0x060053E4 RID: 21476 RVA: 0x00084239 File Offset: 0x00082439
			public virtual string DeltaSyncClientCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["DeltaSyncClientCertificateThumbprint"] = value;
				}
			}

			// Token: 0x170032DF RID: 13023
			// (set) Token: 0x060053E5 RID: 21477 RVA: 0x0008424C File Offset: 0x0008244C
			public virtual int MaxTransportSyncDispatchers
			{
				set
				{
					base.PowerSharpParameters["MaxTransportSyncDispatchers"] = value;
				}
			}

			// Token: 0x170032E0 RID: 13024
			// (set) Token: 0x060053E6 RID: 21478 RVA: 0x00084264 File Offset: 0x00082464
			public virtual bool TransportSyncMailboxLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogEnabled"] = value;
				}
			}

			// Token: 0x170032E1 RID: 13025
			// (set) Token: 0x060053E7 RID: 21479 RVA: 0x0008427C File Offset: 0x0008247C
			public virtual SyncLoggingLevel TransportSyncMailboxLogLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogLoggingLevel"] = value;
				}
			}

			// Token: 0x170032E2 RID: 13026
			// (set) Token: 0x060053E8 RID: 21480 RVA: 0x00084294 File Offset: 0x00082494
			public virtual LocalLongFullPath TransportSyncMailboxLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogFilePath"] = value;
				}
			}

			// Token: 0x170032E3 RID: 13027
			// (set) Token: 0x060053E9 RID: 21481 RVA: 0x000842A7 File Offset: 0x000824A7
			public virtual EnhancedTimeSpan TransportSyncMailboxLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxAge"] = value;
				}
			}

			// Token: 0x170032E4 RID: 13028
			// (set) Token: 0x060053EA RID: 21482 RVA: 0x000842BF File Offset: 0x000824BF
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170032E5 RID: 13029
			// (set) Token: 0x060053EB RID: 21483 RVA: 0x000842D7 File Offset: 0x000824D7
			public virtual ByteQuantifiedSize TransportSyncMailboxLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170032E6 RID: 13030
			// (set) Token: 0x060053EC RID: 21484 RVA: 0x000842EF File Offset: 0x000824EF
			public virtual bool TransportSyncMailboxHealthLogEnabled
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogEnabled"] = value;
				}
			}

			// Token: 0x170032E7 RID: 13031
			// (set) Token: 0x060053ED RID: 21485 RVA: 0x00084307 File Offset: 0x00082507
			public virtual LocalLongFullPath TransportSyncMailboxHealthLogFilePath
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogFilePath"] = value;
				}
			}

			// Token: 0x170032E8 RID: 13032
			// (set) Token: 0x060053EE RID: 21486 RVA: 0x0008431A File Offset: 0x0008251A
			public virtual EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxAge"] = value;
				}
			}

			// Token: 0x170032E9 RID: 13033
			// (set) Token: 0x060053EF RID: 21487 RVA: 0x00084332 File Offset: 0x00082532
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170032EA RID: 13034
			// (set) Token: 0x060053F0 RID: 21488 RVA: 0x0008434A File Offset: 0x0008254A
			public virtual ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["TransportSyncMailboxHealthLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170032EB RID: 13035
			// (set) Token: 0x060053F1 RID: 21489 RVA: 0x00084362 File Offset: 0x00082562
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170032EC RID: 13036
			// (set) Token: 0x060053F2 RID: 21490 RVA: 0x00084375 File Offset: 0x00082575
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170032ED RID: 13037
			// (set) Token: 0x060053F3 RID: 21491 RVA: 0x0008438D File Offset: 0x0008258D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170032EE RID: 13038
			// (set) Token: 0x060053F4 RID: 21492 RVA: 0x000843A5 File Offset: 0x000825A5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170032EF RID: 13039
			// (set) Token: 0x060053F5 RID: 21493 RVA: 0x000843BD File Offset: 0x000825BD
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
