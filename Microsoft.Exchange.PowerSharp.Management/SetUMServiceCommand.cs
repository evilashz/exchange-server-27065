using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B91 RID: 2961
	public class SetUMServiceCommand : SyntheticCommandWithPipelineInputNoOutput<UMServer>
	{
		// Token: 0x06008F51 RID: 36689 RVA: 0x000D1BAD File Offset: 0x000CFDAD
		private SetUMServiceCommand() : base("Set-UMService")
		{
		}

		// Token: 0x06008F52 RID: 36690 RVA: 0x000D1BBA File Offset: 0x000CFDBA
		public SetUMServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008F53 RID: 36691 RVA: 0x000D1BC9 File Offset: 0x000CFDC9
		public virtual SetUMServiceCommand SetParameters(SetUMServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008F54 RID: 36692 RVA: 0x000D1BD3 File Offset: 0x000CFDD3
		public virtual SetUMServiceCommand SetParameters(SetUMServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B92 RID: 2962
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170063E8 RID: 25576
			// (set) Token: 0x06008F55 RID: 36693 RVA: 0x000D1BDD File Offset: 0x000CFDDD
			public virtual MultiValuedProperty<UMDialPlanIdParameter> DialPlans
			{
				set
				{
					base.PowerSharpParameters["DialPlans"] = value;
				}
			}

			// Token: 0x170063E9 RID: 25577
			// (set) Token: 0x06008F56 RID: 36694 RVA: 0x000D1BF0 File Offset: 0x000CFDF0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170063EA RID: 25578
			// (set) Token: 0x06008F57 RID: 36695 RVA: 0x000D1C03 File Offset: 0x000CFE03
			public virtual int? MaxCallsAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxCallsAllowed"] = value;
				}
			}

			// Token: 0x170063EB RID: 25579
			// (set) Token: 0x06008F58 RID: 36696 RVA: 0x000D1C1B File Offset: 0x000CFE1B
			public virtual ServerStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170063EC RID: 25580
			// (set) Token: 0x06008F59 RID: 36697 RVA: 0x000D1C33 File Offset: 0x000CFE33
			public virtual int SipTcpListeningPort
			{
				set
				{
					base.PowerSharpParameters["SipTcpListeningPort"] = value;
				}
			}

			// Token: 0x170063ED RID: 25581
			// (set) Token: 0x06008F5A RID: 36698 RVA: 0x000D1C4B File Offset: 0x000CFE4B
			public virtual int SipTlsListeningPort
			{
				set
				{
					base.PowerSharpParameters["SipTlsListeningPort"] = value;
				}
			}

			// Token: 0x170063EE RID: 25582
			// (set) Token: 0x06008F5B RID: 36699 RVA: 0x000D1C63 File Offset: 0x000CFE63
			public virtual ScheduleInterval GrammarGenerationSchedule
			{
				set
				{
					base.PowerSharpParameters["GrammarGenerationSchedule"] = value;
				}
			}

			// Token: 0x170063EF RID: 25583
			// (set) Token: 0x06008F5C RID: 36700 RVA: 0x000D1C7B File Offset: 0x000CFE7B
			public virtual UMSmartHost ExternalHostFqdn
			{
				set
				{
					base.PowerSharpParameters["ExternalHostFqdn"] = value;
				}
			}

			// Token: 0x170063F0 RID: 25584
			// (set) Token: 0x06008F5D RID: 36701 RVA: 0x000D1C8E File Offset: 0x000CFE8E
			public virtual UMSmartHost ExternalServiceFqdn
			{
				set
				{
					base.PowerSharpParameters["ExternalServiceFqdn"] = value;
				}
			}

			// Token: 0x170063F1 RID: 25585
			// (set) Token: 0x06008F5E RID: 36702 RVA: 0x000D1CA1 File Offset: 0x000CFEA1
			public virtual string UMPodRedirectTemplate
			{
				set
				{
					base.PowerSharpParameters["UMPodRedirectTemplate"] = value;
				}
			}

			// Token: 0x170063F2 RID: 25586
			// (set) Token: 0x06008F5F RID: 36703 RVA: 0x000D1CB4 File Offset: 0x000CFEB4
			public virtual string UMForwardingAddressTemplate
			{
				set
				{
					base.PowerSharpParameters["UMForwardingAddressTemplate"] = value;
				}
			}

			// Token: 0x170063F3 RID: 25587
			// (set) Token: 0x06008F60 RID: 36704 RVA: 0x000D1CC7 File Offset: 0x000CFEC7
			public virtual ProtocolConnectionSettings SIPAccessService
			{
				set
				{
					base.PowerSharpParameters["SIPAccessService"] = value;
				}
			}

			// Token: 0x170063F4 RID: 25588
			// (set) Token: 0x06008F61 RID: 36705 RVA: 0x000D1CDA File Offset: 0x000CFEDA
			public virtual UMStartupMode UMStartupMode
			{
				set
				{
					base.PowerSharpParameters["UMStartupMode"] = value;
				}
			}

			// Token: 0x170063F5 RID: 25589
			// (set) Token: 0x06008F62 RID: 36706 RVA: 0x000D1CF2 File Offset: 0x000CFEF2
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x170063F6 RID: 25590
			// (set) Token: 0x06008F63 RID: 36707 RVA: 0x000D1D0A File Offset: 0x000CFF0A
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x170063F7 RID: 25591
			// (set) Token: 0x06008F64 RID: 36708 RVA: 0x000D1D22 File Offset: 0x000CFF22
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x170063F8 RID: 25592
			// (set) Token: 0x06008F65 RID: 36709 RVA: 0x000D1D3A File Offset: 0x000CFF3A
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x170063F9 RID: 25593
			// (set) Token: 0x06008F66 RID: 36710 RVA: 0x000D1D52 File Offset: 0x000CFF52
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x170063FA RID: 25594
			// (set) Token: 0x06008F67 RID: 36711 RVA: 0x000D1D65 File Offset: 0x000CFF65
			public virtual bool IPAddressFamilyConfigurable
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamilyConfigurable"] = value;
				}
			}

			// Token: 0x170063FB RID: 25595
			// (set) Token: 0x06008F68 RID: 36712 RVA: 0x000D1D7D File Offset: 0x000CFF7D
			public virtual IPAddressFamily IPAddressFamily
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamily"] = value;
				}
			}

			// Token: 0x170063FC RID: 25596
			// (set) Token: 0x06008F69 RID: 36713 RVA: 0x000D1D95 File Offset: 0x000CFF95
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170063FD RID: 25597
			// (set) Token: 0x06008F6A RID: 36714 RVA: 0x000D1DAD File Offset: 0x000CFFAD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170063FE RID: 25598
			// (set) Token: 0x06008F6B RID: 36715 RVA: 0x000D1DC5 File Offset: 0x000CFFC5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170063FF RID: 25599
			// (set) Token: 0x06008F6C RID: 36716 RVA: 0x000D1DDD File Offset: 0x000CFFDD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006400 RID: 25600
			// (set) Token: 0x06008F6D RID: 36717 RVA: 0x000D1DF5 File Offset: 0x000CFFF5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B93 RID: 2963
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006401 RID: 25601
			// (set) Token: 0x06008F6F RID: 36719 RVA: 0x000D1E15 File Offset: 0x000D0015
			public virtual UMServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006402 RID: 25602
			// (set) Token: 0x06008F70 RID: 36720 RVA: 0x000D1E28 File Offset: 0x000D0028
			public virtual MultiValuedProperty<UMDialPlanIdParameter> DialPlans
			{
				set
				{
					base.PowerSharpParameters["DialPlans"] = value;
				}
			}

			// Token: 0x17006403 RID: 25603
			// (set) Token: 0x06008F71 RID: 36721 RVA: 0x000D1E3B File Offset: 0x000D003B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006404 RID: 25604
			// (set) Token: 0x06008F72 RID: 36722 RVA: 0x000D1E4E File Offset: 0x000D004E
			public virtual int? MaxCallsAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxCallsAllowed"] = value;
				}
			}

			// Token: 0x17006405 RID: 25605
			// (set) Token: 0x06008F73 RID: 36723 RVA: 0x000D1E66 File Offset: 0x000D0066
			public virtual ServerStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17006406 RID: 25606
			// (set) Token: 0x06008F74 RID: 36724 RVA: 0x000D1E7E File Offset: 0x000D007E
			public virtual int SipTcpListeningPort
			{
				set
				{
					base.PowerSharpParameters["SipTcpListeningPort"] = value;
				}
			}

			// Token: 0x17006407 RID: 25607
			// (set) Token: 0x06008F75 RID: 36725 RVA: 0x000D1E96 File Offset: 0x000D0096
			public virtual int SipTlsListeningPort
			{
				set
				{
					base.PowerSharpParameters["SipTlsListeningPort"] = value;
				}
			}

			// Token: 0x17006408 RID: 25608
			// (set) Token: 0x06008F76 RID: 36726 RVA: 0x000D1EAE File Offset: 0x000D00AE
			public virtual ScheduleInterval GrammarGenerationSchedule
			{
				set
				{
					base.PowerSharpParameters["GrammarGenerationSchedule"] = value;
				}
			}

			// Token: 0x17006409 RID: 25609
			// (set) Token: 0x06008F77 RID: 36727 RVA: 0x000D1EC6 File Offset: 0x000D00C6
			public virtual UMSmartHost ExternalHostFqdn
			{
				set
				{
					base.PowerSharpParameters["ExternalHostFqdn"] = value;
				}
			}

			// Token: 0x1700640A RID: 25610
			// (set) Token: 0x06008F78 RID: 36728 RVA: 0x000D1ED9 File Offset: 0x000D00D9
			public virtual UMSmartHost ExternalServiceFqdn
			{
				set
				{
					base.PowerSharpParameters["ExternalServiceFqdn"] = value;
				}
			}

			// Token: 0x1700640B RID: 25611
			// (set) Token: 0x06008F79 RID: 36729 RVA: 0x000D1EEC File Offset: 0x000D00EC
			public virtual string UMPodRedirectTemplate
			{
				set
				{
					base.PowerSharpParameters["UMPodRedirectTemplate"] = value;
				}
			}

			// Token: 0x1700640C RID: 25612
			// (set) Token: 0x06008F7A RID: 36730 RVA: 0x000D1EFF File Offset: 0x000D00FF
			public virtual string UMForwardingAddressTemplate
			{
				set
				{
					base.PowerSharpParameters["UMForwardingAddressTemplate"] = value;
				}
			}

			// Token: 0x1700640D RID: 25613
			// (set) Token: 0x06008F7B RID: 36731 RVA: 0x000D1F12 File Offset: 0x000D0112
			public virtual ProtocolConnectionSettings SIPAccessService
			{
				set
				{
					base.PowerSharpParameters["SIPAccessService"] = value;
				}
			}

			// Token: 0x1700640E RID: 25614
			// (set) Token: 0x06008F7C RID: 36732 RVA: 0x000D1F25 File Offset: 0x000D0125
			public virtual UMStartupMode UMStartupMode
			{
				set
				{
					base.PowerSharpParameters["UMStartupMode"] = value;
				}
			}

			// Token: 0x1700640F RID: 25615
			// (set) Token: 0x06008F7D RID: 36733 RVA: 0x000D1F3D File Offset: 0x000D013D
			public virtual bool IrmLogEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmLogEnabled"] = value;
				}
			}

			// Token: 0x17006410 RID: 25616
			// (set) Token: 0x06008F7E RID: 36734 RVA: 0x000D1F55 File Offset: 0x000D0155
			public virtual EnhancedTimeSpan IrmLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxAge"] = value;
				}
			}

			// Token: 0x17006411 RID: 25617
			// (set) Token: 0x06008F7F RID: 36735 RVA: 0x000D1F6D File Offset: 0x000D016D
			public virtual Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17006412 RID: 25618
			// (set) Token: 0x06008F80 RID: 36736 RVA: 0x000D1F85 File Offset: 0x000D0185
			public virtual ByteQuantifiedSize IrmLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["IrmLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17006413 RID: 25619
			// (set) Token: 0x06008F81 RID: 36737 RVA: 0x000D1F9D File Offset: 0x000D019D
			public virtual LocalLongFullPath IrmLogPath
			{
				set
				{
					base.PowerSharpParameters["IrmLogPath"] = value;
				}
			}

			// Token: 0x17006414 RID: 25620
			// (set) Token: 0x06008F82 RID: 36738 RVA: 0x000D1FB0 File Offset: 0x000D01B0
			public virtual bool IPAddressFamilyConfigurable
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamilyConfigurable"] = value;
				}
			}

			// Token: 0x17006415 RID: 25621
			// (set) Token: 0x06008F83 RID: 36739 RVA: 0x000D1FC8 File Offset: 0x000D01C8
			public virtual IPAddressFamily IPAddressFamily
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamily"] = value;
				}
			}

			// Token: 0x17006416 RID: 25622
			// (set) Token: 0x06008F84 RID: 36740 RVA: 0x000D1FE0 File Offset: 0x000D01E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006417 RID: 25623
			// (set) Token: 0x06008F85 RID: 36741 RVA: 0x000D1FF8 File Offset: 0x000D01F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006418 RID: 25624
			// (set) Token: 0x06008F86 RID: 36742 RVA: 0x000D2010 File Offset: 0x000D0210
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006419 RID: 25625
			// (set) Token: 0x06008F87 RID: 36743 RVA: 0x000D2028 File Offset: 0x000D0228
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700641A RID: 25626
			// (set) Token: 0x06008F88 RID: 36744 RVA: 0x000D2040 File Offset: 0x000D0240
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
