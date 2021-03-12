using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007AE RID: 1966
	public class SetAcceptedDomainCommand : SyntheticCommandWithPipelineInputNoOutput<AcceptedDomain>
	{
		// Token: 0x0600628C RID: 25228 RVA: 0x000974F0 File Offset: 0x000956F0
		private SetAcceptedDomainCommand() : base("Set-AcceptedDomain")
		{
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x000974FD File Offset: 0x000956FD
		public SetAcceptedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x0009750C File Offset: 0x0009570C
		public virtual SetAcceptedDomainCommand SetParameters(SetAcceptedDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x00097516 File Offset: 0x00095716
		public virtual SetAcceptedDomainCommand SetParameters(SetAcceptedDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007AF RID: 1967
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003EE9 RID: 16105
			// (set) Token: 0x06006290 RID: 25232 RVA: 0x00097520 File Offset: 0x00095720
			public virtual bool MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x17003EEA RID: 16106
			// (set) Token: 0x06006291 RID: 25233 RVA: 0x00097538 File Offset: 0x00095738
			public virtual bool IsCoexistenceDomain
			{
				set
				{
					base.PowerSharpParameters["IsCoexistenceDomain"] = value;
				}
			}

			// Token: 0x17003EEB RID: 16107
			// (set) Token: 0x06006292 RID: 25234 RVA: 0x00097550 File Offset: 0x00095750
			public virtual string CatchAllRecipient
			{
				set
				{
					base.PowerSharpParameters["CatchAllRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17003EEC RID: 16108
			// (set) Token: 0x06006293 RID: 25235 RVA: 0x0009756E File Offset: 0x0009576E
			public virtual bool MatchSubDomains
			{
				set
				{
					base.PowerSharpParameters["MatchSubDomains"] = value;
				}
			}

			// Token: 0x17003EED RID: 16109
			// (set) Token: 0x06006294 RID: 25236 RVA: 0x00097586 File Offset: 0x00095786
			public virtual string MailFlowPartner
			{
				set
				{
					base.PowerSharpParameters["MailFlowPartner"] = ((value != null) ? new MailFlowPartnerIdParameter(value) : null);
				}
			}

			// Token: 0x17003EEE RID: 16110
			// (set) Token: 0x06006295 RID: 25237 RVA: 0x000975A4 File Offset: 0x000957A4
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x17003EEF RID: 16111
			// (set) Token: 0x06006296 RID: 25238 RVA: 0x000975BC File Offset: 0x000957BC
			public virtual bool InitialDomain
			{
				set
				{
					base.PowerSharpParameters["InitialDomain"] = value;
				}
			}

			// Token: 0x17003EF0 RID: 16112
			// (set) Token: 0x06006297 RID: 25239 RVA: 0x000975D4 File Offset: 0x000957D4
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x17003EF1 RID: 16113
			// (set) Token: 0x06006298 RID: 25240 RVA: 0x000975EC File Offset: 0x000957EC
			public virtual bool EnableNego2Authentication
			{
				set
				{
					base.PowerSharpParameters["EnableNego2Authentication"] = value;
				}
			}

			// Token: 0x17003EF2 RID: 16114
			// (set) Token: 0x06006299 RID: 25241 RVA: 0x00097604 File Offset: 0x00095804
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003EF3 RID: 16115
			// (set) Token: 0x0600629A RID: 25242 RVA: 0x00097617 File Offset: 0x00095817
			public virtual AcceptedDomainType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x17003EF4 RID: 16116
			// (set) Token: 0x0600629B RID: 25243 RVA: 0x0009762F File Offset: 0x0009582F
			public virtual bool AddressBookEnabled
			{
				set
				{
					base.PowerSharpParameters["AddressBookEnabled"] = value;
				}
			}

			// Token: 0x17003EF5 RID: 16117
			// (set) Token: 0x0600629C RID: 25244 RVA: 0x00097647 File Offset: 0x00095847
			public virtual bool PendingRemoval
			{
				set
				{
					base.PowerSharpParameters["PendingRemoval"] = value;
				}
			}

			// Token: 0x17003EF6 RID: 16118
			// (set) Token: 0x0600629D RID: 25245 RVA: 0x0009765F File Offset: 0x0009585F
			public virtual bool PendingCompletion
			{
				set
				{
					base.PowerSharpParameters["PendingCompletion"] = value;
				}
			}

			// Token: 0x17003EF7 RID: 16119
			// (set) Token: 0x0600629E RID: 25246 RVA: 0x00097677 File Offset: 0x00095877
			public virtual bool DualProvisioningEnabled
			{
				set
				{
					base.PowerSharpParameters["DualProvisioningEnabled"] = value;
				}
			}

			// Token: 0x17003EF8 RID: 16120
			// (set) Token: 0x0600629F RID: 25247 RVA: 0x0009768F File Offset: 0x0009588F
			public virtual bool OutboundOnly
			{
				set
				{
					base.PowerSharpParameters["OutboundOnly"] = value;
				}
			}

			// Token: 0x17003EF9 RID: 16121
			// (set) Token: 0x060062A0 RID: 25248 RVA: 0x000976A7 File Offset: 0x000958A7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003EFA RID: 16122
			// (set) Token: 0x060062A1 RID: 25249 RVA: 0x000976BA File Offset: 0x000958BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EFB RID: 16123
			// (set) Token: 0x060062A2 RID: 25250 RVA: 0x000976D2 File Offset: 0x000958D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EFC RID: 16124
			// (set) Token: 0x060062A3 RID: 25251 RVA: 0x000976EA File Offset: 0x000958EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EFD RID: 16125
			// (set) Token: 0x060062A4 RID: 25252 RVA: 0x00097702 File Offset: 0x00095902
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003EFE RID: 16126
			// (set) Token: 0x060062A5 RID: 25253 RVA: 0x0009771A File Offset: 0x0009591A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007B0 RID: 1968
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003EFF RID: 16127
			// (set) Token: 0x060062A7 RID: 25255 RVA: 0x0009773A File Offset: 0x0009593A
			public virtual AcceptedDomainIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003F00 RID: 16128
			// (set) Token: 0x060062A8 RID: 25256 RVA: 0x0009774D File Offset: 0x0009594D
			public virtual bool MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x17003F01 RID: 16129
			// (set) Token: 0x060062A9 RID: 25257 RVA: 0x00097765 File Offset: 0x00095965
			public virtual bool IsCoexistenceDomain
			{
				set
				{
					base.PowerSharpParameters["IsCoexistenceDomain"] = value;
				}
			}

			// Token: 0x17003F02 RID: 16130
			// (set) Token: 0x060062AA RID: 25258 RVA: 0x0009777D File Offset: 0x0009597D
			public virtual string CatchAllRecipient
			{
				set
				{
					base.PowerSharpParameters["CatchAllRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17003F03 RID: 16131
			// (set) Token: 0x060062AB RID: 25259 RVA: 0x0009779B File Offset: 0x0009599B
			public virtual bool MatchSubDomains
			{
				set
				{
					base.PowerSharpParameters["MatchSubDomains"] = value;
				}
			}

			// Token: 0x17003F04 RID: 16132
			// (set) Token: 0x060062AC RID: 25260 RVA: 0x000977B3 File Offset: 0x000959B3
			public virtual string MailFlowPartner
			{
				set
				{
					base.PowerSharpParameters["MailFlowPartner"] = ((value != null) ? new MailFlowPartnerIdParameter(value) : null);
				}
			}

			// Token: 0x17003F05 RID: 16133
			// (set) Token: 0x060062AD RID: 25261 RVA: 0x000977D1 File Offset: 0x000959D1
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x17003F06 RID: 16134
			// (set) Token: 0x060062AE RID: 25262 RVA: 0x000977E9 File Offset: 0x000959E9
			public virtual bool InitialDomain
			{
				set
				{
					base.PowerSharpParameters["InitialDomain"] = value;
				}
			}

			// Token: 0x17003F07 RID: 16135
			// (set) Token: 0x060062AF RID: 25263 RVA: 0x00097801 File Offset: 0x00095A01
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x17003F08 RID: 16136
			// (set) Token: 0x060062B0 RID: 25264 RVA: 0x00097819 File Offset: 0x00095A19
			public virtual bool EnableNego2Authentication
			{
				set
				{
					base.PowerSharpParameters["EnableNego2Authentication"] = value;
				}
			}

			// Token: 0x17003F09 RID: 16137
			// (set) Token: 0x060062B1 RID: 25265 RVA: 0x00097831 File Offset: 0x00095A31
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003F0A RID: 16138
			// (set) Token: 0x060062B2 RID: 25266 RVA: 0x00097844 File Offset: 0x00095A44
			public virtual AcceptedDomainType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x17003F0B RID: 16139
			// (set) Token: 0x060062B3 RID: 25267 RVA: 0x0009785C File Offset: 0x00095A5C
			public virtual bool AddressBookEnabled
			{
				set
				{
					base.PowerSharpParameters["AddressBookEnabled"] = value;
				}
			}

			// Token: 0x17003F0C RID: 16140
			// (set) Token: 0x060062B4 RID: 25268 RVA: 0x00097874 File Offset: 0x00095A74
			public virtual bool PendingRemoval
			{
				set
				{
					base.PowerSharpParameters["PendingRemoval"] = value;
				}
			}

			// Token: 0x17003F0D RID: 16141
			// (set) Token: 0x060062B5 RID: 25269 RVA: 0x0009788C File Offset: 0x00095A8C
			public virtual bool PendingCompletion
			{
				set
				{
					base.PowerSharpParameters["PendingCompletion"] = value;
				}
			}

			// Token: 0x17003F0E RID: 16142
			// (set) Token: 0x060062B6 RID: 25270 RVA: 0x000978A4 File Offset: 0x00095AA4
			public virtual bool DualProvisioningEnabled
			{
				set
				{
					base.PowerSharpParameters["DualProvisioningEnabled"] = value;
				}
			}

			// Token: 0x17003F0F RID: 16143
			// (set) Token: 0x060062B7 RID: 25271 RVA: 0x000978BC File Offset: 0x00095ABC
			public virtual bool OutboundOnly
			{
				set
				{
					base.PowerSharpParameters["OutboundOnly"] = value;
				}
			}

			// Token: 0x17003F10 RID: 16144
			// (set) Token: 0x060062B8 RID: 25272 RVA: 0x000978D4 File Offset: 0x00095AD4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003F11 RID: 16145
			// (set) Token: 0x060062B9 RID: 25273 RVA: 0x000978E7 File Offset: 0x00095AE7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003F12 RID: 16146
			// (set) Token: 0x060062BA RID: 25274 RVA: 0x000978FF File Offset: 0x00095AFF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003F13 RID: 16147
			// (set) Token: 0x060062BB RID: 25275 RVA: 0x00097917 File Offset: 0x00095B17
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003F14 RID: 16148
			// (set) Token: 0x060062BC RID: 25276 RVA: 0x0009792F File Offset: 0x00095B2F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003F15 RID: 16149
			// (set) Token: 0x060062BD RID: 25277 RVA: 0x00097947 File Offset: 0x00095B47
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
