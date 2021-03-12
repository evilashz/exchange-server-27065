using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200009F RID: 159
	public class AddSecondaryDomainCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x000388C1 File Offset: 0x00036AC1
		private AddSecondaryDomainCommand() : base("Add-SecondaryDomain")
		{
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000388CE File Offset: 0x00036ACE
		public AddSecondaryDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000388DD File Offset: 0x00036ADD
		public virtual AddSecondaryDomainCommand SetParameters(AddSecondaryDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000388E7 File Offset: 0x00036AE7
		public virtual AddSecondaryDomainCommand SetParameters(AddSecondaryDomainCommand.DefaultParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000388F1 File Offset: 0x00036AF1
		public virtual AddSecondaryDomainCommand SetParameters(AddSecondaryDomainCommand.OrgScopedParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000A0 RID: 160
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170003E1 RID: 993
			// (set) Token: 0x0600196B RID: 6507 RVA: 0x000388FB File Offset: 0x00036AFB
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170003E2 RID: 994
			// (set) Token: 0x0600196C RID: 6508 RVA: 0x00038913 File Offset: 0x00036B13
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x170003E3 RID: 995
			// (set) Token: 0x0600196D RID: 6509 RVA: 0x0003892B File Offset: 0x00036B2B
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x170003E4 RID: 996
			// (set) Token: 0x0600196E RID: 6510 RVA: 0x00038943 File Offset: 0x00036B43
			public virtual bool MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x170003E5 RID: 997
			// (set) Token: 0x0600196F RID: 6511 RVA: 0x0003895B File Offset: 0x00036B5B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170003E6 RID: 998
			// (set) Token: 0x06001970 RID: 6512 RVA: 0x0003896E File Offset: 0x00036B6E
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170003E7 RID: 999
			// (set) Token: 0x06001971 RID: 6513 RVA: 0x00038986 File Offset: 0x00036B86
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170003E8 RID: 1000
			// (set) Token: 0x06001972 RID: 6514 RVA: 0x0003899E File Offset: 0x00036B9E
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170003E9 RID: 1001
			// (set) Token: 0x06001973 RID: 6515 RVA: 0x000389B6 File Offset: 0x00036BB6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003EA RID: 1002
			// (set) Token: 0x06001974 RID: 6516 RVA: 0x000389CE File Offset: 0x00036BCE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003EB RID: 1003
			// (set) Token: 0x06001975 RID: 6517 RVA: 0x000389E6 File Offset: 0x00036BE6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003EC RID: 1004
			// (set) Token: 0x06001976 RID: 6518 RVA: 0x000389FE File Offset: 0x00036BFE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003ED RID: 1005
			// (set) Token: 0x06001977 RID: 6519 RVA: 0x00038A16 File Offset: 0x00036C16
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000A1 RID: 161
		public class DefaultParameterSetParameters : ParametersBase
		{
			// Token: 0x170003EE RID: 1006
			// (set) Token: 0x06001979 RID: 6521 RVA: 0x00038A36 File Offset: 0x00036C36
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170003EF RID: 1007
			// (set) Token: 0x0600197A RID: 6522 RVA: 0x00038A49 File Offset: 0x00036C49
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170003F0 RID: 1008
			// (set) Token: 0x0600197B RID: 6523 RVA: 0x00038A5C File Offset: 0x00036C5C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170003F1 RID: 1009
			// (set) Token: 0x0600197C RID: 6524 RVA: 0x00038A7A File Offset: 0x00036C7A
			public virtual SwitchParameter DomainOwnershipVerified
			{
				set
				{
					base.PowerSharpParameters["DomainOwnershipVerified"] = value;
				}
			}

			// Token: 0x170003F2 RID: 1010
			// (set) Token: 0x0600197D RID: 6525 RVA: 0x00038A92 File Offset: 0x00036C92
			public virtual AcceptedDomainType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x170003F3 RID: 1011
			// (set) Token: 0x0600197E RID: 6526 RVA: 0x00038AAA File Offset: 0x00036CAA
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x170003F4 RID: 1012
			// (set) Token: 0x0600197F RID: 6527 RVA: 0x00038AC2 File Offset: 0x00036CC2
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x170003F5 RID: 1013
			// (set) Token: 0x06001980 RID: 6528 RVA: 0x00038ADA File Offset: 0x00036CDA
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x170003F6 RID: 1014
			// (set) Token: 0x06001981 RID: 6529 RVA: 0x00038AF2 File Offset: 0x00036CF2
			public virtual bool MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x170003F7 RID: 1015
			// (set) Token: 0x06001982 RID: 6530 RVA: 0x00038B0A File Offset: 0x00036D0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170003F8 RID: 1016
			// (set) Token: 0x06001983 RID: 6531 RVA: 0x00038B1D File Offset: 0x00036D1D
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (set) Token: 0x06001984 RID: 6532 RVA: 0x00038B35 File Offset: 0x00036D35
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170003FA RID: 1018
			// (set) Token: 0x06001985 RID: 6533 RVA: 0x00038B4D File Offset: 0x00036D4D
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (set) Token: 0x06001986 RID: 6534 RVA: 0x00038B65 File Offset: 0x00036D65
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003FC RID: 1020
			// (set) Token: 0x06001987 RID: 6535 RVA: 0x00038B7D File Offset: 0x00036D7D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (set) Token: 0x06001988 RID: 6536 RVA: 0x00038B95 File Offset: 0x00036D95
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003FE RID: 1022
			// (set) Token: 0x06001989 RID: 6537 RVA: 0x00038BAD File Offset: 0x00036DAD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003FF RID: 1023
			// (set) Token: 0x0600198A RID: 6538 RVA: 0x00038BC5 File Offset: 0x00036DC5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000A2 RID: 162
		public class OrgScopedParameterSetParameters : ParametersBase
		{
			// Token: 0x17000400 RID: 1024
			// (set) Token: 0x0600198C RID: 6540 RVA: 0x00038BE5 File Offset: 0x00036DE5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (set) Token: 0x0600198D RID: 6541 RVA: 0x00038BF8 File Offset: 0x00036DF8
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17000402 RID: 1026
			// (set) Token: 0x0600198E RID: 6542 RVA: 0x00038C0B File Offset: 0x00036E0B
			public virtual SwitchParameter DomainOwnershipVerified
			{
				set
				{
					base.PowerSharpParameters["DomainOwnershipVerified"] = value;
				}
			}

			// Token: 0x17000403 RID: 1027
			// (set) Token: 0x0600198F RID: 6543 RVA: 0x00038C23 File Offset: 0x00036E23
			public virtual AcceptedDomainType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x17000404 RID: 1028
			// (set) Token: 0x06001990 RID: 6544 RVA: 0x00038C3B File Offset: 0x00036E3B
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x17000405 RID: 1029
			// (set) Token: 0x06001991 RID: 6545 RVA: 0x00038C53 File Offset: 0x00036E53
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x17000406 RID: 1030
			// (set) Token: 0x06001992 RID: 6546 RVA: 0x00038C6B File Offset: 0x00036E6B
			public virtual bool OutBoundOnly
			{
				set
				{
					base.PowerSharpParameters["OutBoundOnly"] = value;
				}
			}

			// Token: 0x17000407 RID: 1031
			// (set) Token: 0x06001993 RID: 6547 RVA: 0x00038C83 File Offset: 0x00036E83
			public virtual bool MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x17000408 RID: 1032
			// (set) Token: 0x06001994 RID: 6548 RVA: 0x00038C9B File Offset: 0x00036E9B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000409 RID: 1033
			// (set) Token: 0x06001995 RID: 6549 RVA: 0x00038CAE File Offset: 0x00036EAE
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x1700040A RID: 1034
			// (set) Token: 0x06001996 RID: 6550 RVA: 0x00038CC6 File Offset: 0x00036EC6
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x1700040B RID: 1035
			// (set) Token: 0x06001997 RID: 6551 RVA: 0x00038CDE File Offset: 0x00036EDE
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x1700040C RID: 1036
			// (set) Token: 0x06001998 RID: 6552 RVA: 0x00038CF6 File Offset: 0x00036EF6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700040D RID: 1037
			// (set) Token: 0x06001999 RID: 6553 RVA: 0x00038D0E File Offset: 0x00036F0E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700040E RID: 1038
			// (set) Token: 0x0600199A RID: 6554 RVA: 0x00038D26 File Offset: 0x00036F26
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700040F RID: 1039
			// (set) Token: 0x0600199B RID: 6555 RVA: 0x00038D3E File Offset: 0x00036F3E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000410 RID: 1040
			// (set) Token: 0x0600199C RID: 6556 RVA: 0x00038D56 File Offset: 0x00036F56
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
