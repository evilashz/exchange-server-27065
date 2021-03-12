using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000663 RID: 1635
	public class SetFrontendTransportServiceCommand : SyntheticCommandWithPipelineInputNoOutput<FrontendTransportServerPresentationObject>
	{
		// Token: 0x0600541A RID: 21530 RVA: 0x000846BC File Offset: 0x000828BC
		private SetFrontendTransportServiceCommand() : base("Set-FrontendTransportService")
		{
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x000846C9 File Offset: 0x000828C9
		public SetFrontendTransportServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x000846D8 File Offset: 0x000828D8
		public virtual SetFrontendTransportServiceCommand SetParameters(SetFrontendTransportServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x000846E2 File Offset: 0x000828E2
		public virtual SetFrontendTransportServiceCommand SetParameters(SetFrontendTransportServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000664 RID: 1636
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700330D RID: 13069
			// (set) Token: 0x0600541E RID: 21534 RVA: 0x000846EC File Offset: 0x000828EC
			public virtual EnhancedTimeSpan AgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxAge"] = value;
				}
			}

			// Token: 0x1700330E RID: 13070
			// (set) Token: 0x0600541F RID: 21535 RVA: 0x00084704 File Offset: 0x00082904
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700330F RID: 13071
			// (set) Token: 0x06005420 RID: 21536 RVA: 0x0008471C File Offset: 0x0008291C
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003310 RID: 13072
			// (set) Token: 0x06005421 RID: 21537 RVA: 0x00084734 File Offset: 0x00082934
			public virtual LocalLongFullPath AgentLogPath
			{
				set
				{
					base.PowerSharpParameters["AgentLogPath"] = value;
				}
			}

			// Token: 0x17003311 RID: 13073
			// (set) Token: 0x06005422 RID: 21538 RVA: 0x00084747 File Offset: 0x00082947
			public virtual bool AgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AgentLogEnabled"] = value;
				}
			}

			// Token: 0x17003312 RID: 13074
			// (set) Token: 0x06005423 RID: 21539 RVA: 0x0008475F File Offset: 0x0008295F
			public virtual EnhancedTimeSpan DnsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxAge"] = value;
				}
			}

			// Token: 0x17003313 RID: 13075
			// (set) Token: 0x06005424 RID: 21540 RVA: 0x00084777 File Offset: 0x00082977
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003314 RID: 13076
			// (set) Token: 0x06005425 RID: 21541 RVA: 0x0008478F File Offset: 0x0008298F
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003315 RID: 13077
			// (set) Token: 0x06005426 RID: 21542 RVA: 0x000847A7 File Offset: 0x000829A7
			public virtual LocalLongFullPath DnsLogPath
			{
				set
				{
					base.PowerSharpParameters["DnsLogPath"] = value;
				}
			}

			// Token: 0x17003316 RID: 13078
			// (set) Token: 0x06005427 RID: 21543 RVA: 0x000847BA File Offset: 0x000829BA
			public virtual bool DnsLogEnabled
			{
				set
				{
					base.PowerSharpParameters["DnsLogEnabled"] = value;
				}
			}

			// Token: 0x17003317 RID: 13079
			// (set) Token: 0x06005428 RID: 21544 RVA: 0x000847D2 File Offset: 0x000829D2
			public virtual EnhancedTimeSpan ResourceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxAge"] = value;
				}
			}

			// Token: 0x17003318 RID: 13080
			// (set) Token: 0x06005429 RID: 21545 RVA: 0x000847EA File Offset: 0x000829EA
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003319 RID: 13081
			// (set) Token: 0x0600542A RID: 21546 RVA: 0x00084802 File Offset: 0x00082A02
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700331A RID: 13082
			// (set) Token: 0x0600542B RID: 21547 RVA: 0x0008481A File Offset: 0x00082A1A
			public virtual LocalLongFullPath ResourceLogPath
			{
				set
				{
					base.PowerSharpParameters["ResourceLogPath"] = value;
				}
			}

			// Token: 0x1700331B RID: 13083
			// (set) Token: 0x0600542C RID: 21548 RVA: 0x0008482D File Offset: 0x00082A2D
			public virtual bool ResourceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ResourceLogEnabled"] = value;
				}
			}

			// Token: 0x1700331C RID: 13084
			// (set) Token: 0x0600542D RID: 21549 RVA: 0x00084845 File Offset: 0x00082A45
			public virtual EnhancedTimeSpan AttributionLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AttributionLogMaxAge"] = value;
				}
			}

			// Token: 0x1700331D RID: 13085
			// (set) Token: 0x0600542E RID: 21550 RVA: 0x0008485D File Offset: 0x00082A5D
			public virtual Unlimited<ByteQuantifiedSize> AttributionLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AttributionLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700331E RID: 13086
			// (set) Token: 0x0600542F RID: 21551 RVA: 0x00084875 File Offset: 0x00082A75
			public virtual Unlimited<ByteQuantifiedSize> AttributionLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AttributionLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700331F RID: 13087
			// (set) Token: 0x06005430 RID: 21552 RVA: 0x0008488D File Offset: 0x00082A8D
			public virtual LocalLongFullPath AttributionLogPath
			{
				set
				{
					base.PowerSharpParameters["AttributionLogPath"] = value;
				}
			}

			// Token: 0x17003320 RID: 13088
			// (set) Token: 0x06005431 RID: 21553 RVA: 0x000848A0 File Offset: 0x00082AA0
			public virtual bool AttributionLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AttributionLogEnabled"] = value;
				}
			}

			// Token: 0x17003321 RID: 13089
			// (set) Token: 0x06005432 RID: 21554 RVA: 0x000848B8 File Offset: 0x00082AB8
			public virtual int MaxReceiveTlsRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveTlsRatePerMinute"] = value;
				}
			}

			// Token: 0x17003322 RID: 13090
			// (set) Token: 0x06005433 RID: 21555 RVA: 0x000848D0 File Offset: 0x00082AD0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003323 RID: 13091
			// (set) Token: 0x06005434 RID: 21556 RVA: 0x000848E3 File Offset: 0x00082AE3
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x17003324 RID: 13092
			// (set) Token: 0x06005435 RID: 21557 RVA: 0x000848FB File Offset: 0x00082AFB
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x17003325 RID: 13093
			// (set) Token: 0x06005436 RID: 21558 RVA: 0x00084913 File Offset: 0x00082B13
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x17003326 RID: 13094
			// (set) Token: 0x06005437 RID: 21559 RVA: 0x0008492B File Offset: 0x00082B2B
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003327 RID: 13095
			// (set) Token: 0x06005438 RID: 21560 RVA: 0x00084943 File Offset: 0x00082B43
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003328 RID: 13096
			// (set) Token: 0x06005439 RID: 21561 RVA: 0x0008495B File Offset: 0x00082B5B
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x17003329 RID: 13097
			// (set) Token: 0x0600543A RID: 21562 RVA: 0x0008496E File Offset: 0x00082B6E
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700332A RID: 13098
			// (set) Token: 0x0600543B RID: 21563 RVA: 0x00084986 File Offset: 0x00082B86
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x1700332B RID: 13099
			// (set) Token: 0x0600543C RID: 21564 RVA: 0x0008499E File Offset: 0x00082B9E
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x1700332C RID: 13100
			// (set) Token: 0x0600543D RID: 21565 RVA: 0x000849B6 File Offset: 0x00082BB6
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x1700332D RID: 13101
			// (set) Token: 0x0600543E RID: 21566 RVA: 0x000849C9 File Offset: 0x00082BC9
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x1700332E RID: 13102
			// (set) Token: 0x0600543F RID: 21567 RVA: 0x000849DC File Offset: 0x00082BDC
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x1700332F RID: 13103
			// (set) Token: 0x06005440 RID: 21568 RVA: 0x000849F4 File Offset: 0x00082BF4
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x17003330 RID: 13104
			// (set) Token: 0x06005441 RID: 21569 RVA: 0x00084A0C File Offset: 0x00082C0C
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x17003331 RID: 13105
			// (set) Token: 0x06005442 RID: 21570 RVA: 0x00084A24 File Offset: 0x00082C24
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x17003332 RID: 13106
			// (set) Token: 0x06005443 RID: 21571 RVA: 0x00084A37 File Offset: 0x00082C37
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17003333 RID: 13107
			// (set) Token: 0x06005444 RID: 21572 RVA: 0x00084A4F File Offset: 0x00082C4F
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003334 RID: 13108
			// (set) Token: 0x06005445 RID: 21573 RVA: 0x00084A67 File Offset: 0x00082C67
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003335 RID: 13109
			// (set) Token: 0x06005446 RID: 21574 RVA: 0x00084A7F File Offset: 0x00082C7F
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003336 RID: 13110
			// (set) Token: 0x06005447 RID: 21575 RVA: 0x00084A97 File Offset: 0x00082C97
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003337 RID: 13111
			// (set) Token: 0x06005448 RID: 21576 RVA: 0x00084AAA File Offset: 0x00082CAA
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003338 RID: 13112
			// (set) Token: 0x06005449 RID: 21577 RVA: 0x00084AC2 File Offset: 0x00082CC2
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003339 RID: 13113
			// (set) Token: 0x0600544A RID: 21578 RVA: 0x00084ADA File Offset: 0x00082CDA
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700333A RID: 13114
			// (set) Token: 0x0600544B RID: 21579 RVA: 0x00084AF2 File Offset: 0x00082CF2
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x1700333B RID: 13115
			// (set) Token: 0x0600544C RID: 21580 RVA: 0x00084B05 File Offset: 0x00082D05
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x1700333C RID: 13116
			// (set) Token: 0x0600544D RID: 21581 RVA: 0x00084B1D File Offset: 0x00082D1D
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x1700333D RID: 13117
			// (set) Token: 0x0600544E RID: 21582 RVA: 0x00084B35 File Offset: 0x00082D35
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x1700333E RID: 13118
			// (set) Token: 0x0600544F RID: 21583 RVA: 0x00084B4D File Offset: 0x00082D4D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700333F RID: 13119
			// (set) Token: 0x06005450 RID: 21584 RVA: 0x00084B65 File Offset: 0x00082D65
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003340 RID: 13120
			// (set) Token: 0x06005451 RID: 21585 RVA: 0x00084B7D File Offset: 0x00082D7D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003341 RID: 13121
			// (set) Token: 0x06005452 RID: 21586 RVA: 0x00084B95 File Offset: 0x00082D95
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003342 RID: 13122
			// (set) Token: 0x06005453 RID: 21587 RVA: 0x00084BAD File Offset: 0x00082DAD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000665 RID: 1637
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003343 RID: 13123
			// (set) Token: 0x06005455 RID: 21589 RVA: 0x00084BCD File Offset: 0x00082DCD
			public virtual FrontendTransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003344 RID: 13124
			// (set) Token: 0x06005456 RID: 21590 RVA: 0x00084BE0 File Offset: 0x00082DE0
			public virtual EnhancedTimeSpan AgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxAge"] = value;
				}
			}

			// Token: 0x17003345 RID: 13125
			// (set) Token: 0x06005457 RID: 21591 RVA: 0x00084BF8 File Offset: 0x00082DF8
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003346 RID: 13126
			// (set) Token: 0x06005458 RID: 21592 RVA: 0x00084C10 File Offset: 0x00082E10
			public virtual Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003347 RID: 13127
			// (set) Token: 0x06005459 RID: 21593 RVA: 0x00084C28 File Offset: 0x00082E28
			public virtual LocalLongFullPath AgentLogPath
			{
				set
				{
					base.PowerSharpParameters["AgentLogPath"] = value;
				}
			}

			// Token: 0x17003348 RID: 13128
			// (set) Token: 0x0600545A RID: 21594 RVA: 0x00084C3B File Offset: 0x00082E3B
			public virtual bool AgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AgentLogEnabled"] = value;
				}
			}

			// Token: 0x17003349 RID: 13129
			// (set) Token: 0x0600545B RID: 21595 RVA: 0x00084C53 File Offset: 0x00082E53
			public virtual EnhancedTimeSpan DnsLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxAge"] = value;
				}
			}

			// Token: 0x1700334A RID: 13130
			// (set) Token: 0x0600545C RID: 21596 RVA: 0x00084C6B File Offset: 0x00082E6B
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700334B RID: 13131
			// (set) Token: 0x0600545D RID: 21597 RVA: 0x00084C83 File Offset: 0x00082E83
			public virtual Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["DnsLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700334C RID: 13132
			// (set) Token: 0x0600545E RID: 21598 RVA: 0x00084C9B File Offset: 0x00082E9B
			public virtual LocalLongFullPath DnsLogPath
			{
				set
				{
					base.PowerSharpParameters["DnsLogPath"] = value;
				}
			}

			// Token: 0x1700334D RID: 13133
			// (set) Token: 0x0600545F RID: 21599 RVA: 0x00084CAE File Offset: 0x00082EAE
			public virtual bool DnsLogEnabled
			{
				set
				{
					base.PowerSharpParameters["DnsLogEnabled"] = value;
				}
			}

			// Token: 0x1700334E RID: 13134
			// (set) Token: 0x06005460 RID: 21600 RVA: 0x00084CC6 File Offset: 0x00082EC6
			public virtual EnhancedTimeSpan ResourceLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxAge"] = value;
				}
			}

			// Token: 0x1700334F RID: 13135
			// (set) Token: 0x06005461 RID: 21601 RVA: 0x00084CDE File Offset: 0x00082EDE
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003350 RID: 13136
			// (set) Token: 0x06005462 RID: 21602 RVA: 0x00084CF6 File Offset: 0x00082EF6
			public virtual Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ResourceLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003351 RID: 13137
			// (set) Token: 0x06005463 RID: 21603 RVA: 0x00084D0E File Offset: 0x00082F0E
			public virtual LocalLongFullPath ResourceLogPath
			{
				set
				{
					base.PowerSharpParameters["ResourceLogPath"] = value;
				}
			}

			// Token: 0x17003352 RID: 13138
			// (set) Token: 0x06005464 RID: 21604 RVA: 0x00084D21 File Offset: 0x00082F21
			public virtual bool ResourceLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ResourceLogEnabled"] = value;
				}
			}

			// Token: 0x17003353 RID: 13139
			// (set) Token: 0x06005465 RID: 21605 RVA: 0x00084D39 File Offset: 0x00082F39
			public virtual EnhancedTimeSpan AttributionLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["AttributionLogMaxAge"] = value;
				}
			}

			// Token: 0x17003354 RID: 13140
			// (set) Token: 0x06005466 RID: 21606 RVA: 0x00084D51 File Offset: 0x00082F51
			public virtual Unlimited<ByteQuantifiedSize> AttributionLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["AttributionLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003355 RID: 13141
			// (set) Token: 0x06005467 RID: 21607 RVA: 0x00084D69 File Offset: 0x00082F69
			public virtual Unlimited<ByteQuantifiedSize> AttributionLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["AttributionLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003356 RID: 13142
			// (set) Token: 0x06005468 RID: 21608 RVA: 0x00084D81 File Offset: 0x00082F81
			public virtual LocalLongFullPath AttributionLogPath
			{
				set
				{
					base.PowerSharpParameters["AttributionLogPath"] = value;
				}
			}

			// Token: 0x17003357 RID: 13143
			// (set) Token: 0x06005469 RID: 21609 RVA: 0x00084D94 File Offset: 0x00082F94
			public virtual bool AttributionLogEnabled
			{
				set
				{
					base.PowerSharpParameters["AttributionLogEnabled"] = value;
				}
			}

			// Token: 0x17003358 RID: 13144
			// (set) Token: 0x0600546A RID: 21610 RVA: 0x00084DAC File Offset: 0x00082FAC
			public virtual int MaxReceiveTlsRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveTlsRatePerMinute"] = value;
				}
			}

			// Token: 0x17003359 RID: 13145
			// (set) Token: 0x0600546B RID: 21611 RVA: 0x00084DC4 File Offset: 0x00082FC4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700335A RID: 13146
			// (set) Token: 0x0600546C RID: 21612 RVA: 0x00084DD7 File Offset: 0x00082FD7
			public virtual bool AntispamAgentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AntispamAgentsEnabled"] = value;
				}
			}

			// Token: 0x1700335B RID: 13147
			// (set) Token: 0x0600546D RID: 21613 RVA: 0x00084DEF File Offset: 0x00082FEF
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x1700335C RID: 13148
			// (set) Token: 0x0600546E RID: 21614 RVA: 0x00084E07 File Offset: 0x00083007
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x1700335D RID: 13149
			// (set) Token: 0x0600546F RID: 21615 RVA: 0x00084E1F File Offset: 0x0008301F
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700335E RID: 13150
			// (set) Token: 0x06005470 RID: 21616 RVA: 0x00084E37 File Offset: 0x00083037
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700335F RID: 13151
			// (set) Token: 0x06005471 RID: 21617 RVA: 0x00084E4F File Offset: 0x0008304F
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x17003360 RID: 13152
			// (set) Token: 0x06005472 RID: 21618 RVA: 0x00084E62 File Offset: 0x00083062
			public virtual bool ExternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x17003361 RID: 13153
			// (set) Token: 0x06005473 RID: 21619 RVA: 0x00084E7A File Offset: 0x0008307A
			public virtual Guid ExternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x17003362 RID: 13154
			// (set) Token: 0x06005474 RID: 21620 RVA: 0x00084E92 File Offset: 0x00083092
			public virtual ProtocolOption ExternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x17003363 RID: 13155
			// (set) Token: 0x06005475 RID: 21621 RVA: 0x00084EAA File Offset: 0x000830AA
			public virtual MultiValuedProperty<IPAddress> ExternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["ExternalDNSServers"] = value;
				}
			}

			// Token: 0x17003364 RID: 13156
			// (set) Token: 0x06005476 RID: 21622 RVA: 0x00084EBD File Offset: 0x000830BD
			public virtual IPAddress ExternalIPAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddress"] = value;
				}
			}

			// Token: 0x17003365 RID: 13157
			// (set) Token: 0x06005477 RID: 21623 RVA: 0x00084ED0 File Offset: 0x000830D0
			public virtual bool InternalDNSAdapterEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterEnabled"] = value;
				}
			}

			// Token: 0x17003366 RID: 13158
			// (set) Token: 0x06005478 RID: 21624 RVA: 0x00084EE8 File Offset: 0x000830E8
			public virtual Guid InternalDNSAdapterGuid
			{
				set
				{
					base.PowerSharpParameters["InternalDNSAdapterGuid"] = value;
				}
			}

			// Token: 0x17003367 RID: 13159
			// (set) Token: 0x06005479 RID: 21625 RVA: 0x00084F00 File Offset: 0x00083100
			public virtual ProtocolOption InternalDNSProtocolOption
			{
				set
				{
					base.PowerSharpParameters["InternalDNSProtocolOption"] = value;
				}
			}

			// Token: 0x17003368 RID: 13160
			// (set) Token: 0x0600547A RID: 21626 RVA: 0x00084F18 File Offset: 0x00083118
			public virtual MultiValuedProperty<IPAddress> InternalDNSServers
			{
				set
				{
					base.PowerSharpParameters["InternalDNSServers"] = value;
				}
			}

			// Token: 0x17003369 RID: 13161
			// (set) Token: 0x0600547B RID: 21627 RVA: 0x00084F2B File Offset: 0x0008312B
			public virtual ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["IntraOrgConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700336A RID: 13162
			// (set) Token: 0x0600547C RID: 21628 RVA: 0x00084F43 File Offset: 0x00083143
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x1700336B RID: 13163
			// (set) Token: 0x0600547D RID: 21629 RVA: 0x00084F5B File Offset: 0x0008315B
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700336C RID: 13164
			// (set) Token: 0x0600547E RID: 21630 RVA: 0x00084F73 File Offset: 0x00083173
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700336D RID: 13165
			// (set) Token: 0x0600547F RID: 21631 RVA: 0x00084F8B File Offset: 0x0008318B
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x1700336E RID: 13166
			// (set) Token: 0x06005480 RID: 21632 RVA: 0x00084F9E File Offset: 0x0008319E
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x1700336F RID: 13167
			// (set) Token: 0x06005481 RID: 21633 RVA: 0x00084FB6 File Offset: 0x000831B6
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003370 RID: 13168
			// (set) Token: 0x06005482 RID: 21634 RVA: 0x00084FCE File Offset: 0x000831CE
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003371 RID: 13169
			// (set) Token: 0x06005483 RID: 21635 RVA: 0x00084FE6 File Offset: 0x000831E6
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003372 RID: 13170
			// (set) Token: 0x06005484 RID: 21636 RVA: 0x00084FF9 File Offset: 0x000831F9
			public virtual int TransientFailureRetryCount
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryCount"] = value;
				}
			}

			// Token: 0x17003373 RID: 13171
			// (set) Token: 0x06005485 RID: 21637 RVA: 0x00085011 File Offset: 0x00083211
			public virtual EnhancedTimeSpan TransientFailureRetryInterval
			{
				set
				{
					base.PowerSharpParameters["TransientFailureRetryInterval"] = value;
				}
			}

			// Token: 0x17003374 RID: 13172
			// (set) Token: 0x06005486 RID: 21638 RVA: 0x00085029 File Offset: 0x00083229
			public virtual int MaxConnectionRatePerMinute
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionRatePerMinute"] = value;
				}
			}

			// Token: 0x17003375 RID: 13173
			// (set) Token: 0x06005487 RID: 21639 RVA: 0x00085041 File Offset: 0x00083241
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003376 RID: 13174
			// (set) Token: 0x06005488 RID: 21640 RVA: 0x00085059 File Offset: 0x00083259
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003377 RID: 13175
			// (set) Token: 0x06005489 RID: 21641 RVA: 0x00085071 File Offset: 0x00083271
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003378 RID: 13176
			// (set) Token: 0x0600548A RID: 21642 RVA: 0x00085089 File Offset: 0x00083289
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003379 RID: 13177
			// (set) Token: 0x0600548B RID: 21643 RVA: 0x000850A1 File Offset: 0x000832A1
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
