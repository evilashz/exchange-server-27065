using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007A9 RID: 1961
	public class NewAcceptedDomainCommand : SyntheticCommandWithPipelineInput<AcceptedDomain, AcceptedDomain>
	{
		// Token: 0x0600625E RID: 25182 RVA: 0x0009712E File Offset: 0x0009532E
		private NewAcceptedDomainCommand() : base("New-AcceptedDomain")
		{
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x0009713B File Offset: 0x0009533B
		public NewAcceptedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x0009714A File Offset: 0x0009534A
		public virtual NewAcceptedDomainCommand SetParameters(NewAcceptedDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007AA RID: 1962
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003EC5 RID: 16069
			// (set) Token: 0x06006261 RID: 25185 RVA: 0x00097154 File Offset: 0x00095354
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003EC6 RID: 16070
			// (set) Token: 0x06006262 RID: 25186 RVA: 0x00097167 File Offset: 0x00095367
			public virtual SmtpDomainWithSubdomains DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003EC7 RID: 16071
			// (set) Token: 0x06006263 RID: 25187 RVA: 0x0009717A File Offset: 0x0009537A
			public virtual AcceptedDomainType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x17003EC8 RID: 16072
			// (set) Token: 0x06006264 RID: 25188 RVA: 0x00097192 File Offset: 0x00095392
			public virtual AuthenticationType AuthenticationType
			{
				set
				{
					base.PowerSharpParameters["AuthenticationType"] = value;
				}
			}

			// Token: 0x17003EC9 RID: 16073
			// (set) Token: 0x06006265 RID: 25189 RVA: 0x000971AA File Offset: 0x000953AA
			public virtual LiveIdInstanceType LiveIdInstanceType
			{
				set
				{
					base.PowerSharpParameters["LiveIdInstanceType"] = value;
				}
			}

			// Token: 0x17003ECA RID: 16074
			// (set) Token: 0x06006266 RID: 25190 RVA: 0x000971C2 File Offset: 0x000953C2
			public virtual string CatchAllRecipient
			{
				set
				{
					base.PowerSharpParameters["CatchAllRecipient"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17003ECB RID: 16075
			// (set) Token: 0x06006267 RID: 25191 RVA: 0x000971E0 File Offset: 0x000953E0
			public virtual bool MatchSubDomains
			{
				set
				{
					base.PowerSharpParameters["MatchSubDomains"] = value;
				}
			}

			// Token: 0x17003ECC RID: 16076
			// (set) Token: 0x06006268 RID: 25192 RVA: 0x000971F8 File Offset: 0x000953F8
			public virtual string MailFlowPartner
			{
				set
				{
					base.PowerSharpParameters["MailFlowPartner"] = ((value != null) ? new MailFlowPartnerIdParameter(value) : null);
				}
			}

			// Token: 0x17003ECD RID: 16077
			// (set) Token: 0x06006269 RID: 25193 RVA: 0x00097216 File Offset: 0x00095416
			public virtual bool OutboundOnly
			{
				set
				{
					base.PowerSharpParameters["OutboundOnly"] = value;
				}
			}

			// Token: 0x17003ECE RID: 16078
			// (set) Token: 0x0600626A RID: 25194 RVA: 0x0009722E File Offset: 0x0009542E
			public virtual bool InitialDomain
			{
				set
				{
					base.PowerSharpParameters["InitialDomain"] = value;
				}
			}

			// Token: 0x17003ECF RID: 16079
			// (set) Token: 0x0600626B RID: 25195 RVA: 0x00097246 File Offset: 0x00095446
			public virtual SwitchParameter SkipDnsProvisioning
			{
				set
				{
					base.PowerSharpParameters["SkipDnsProvisioning"] = value;
				}
			}

			// Token: 0x17003ED0 RID: 16080
			// (set) Token: 0x0600626C RID: 25196 RVA: 0x0009725E File Offset: 0x0009545E
			public virtual SwitchParameter SkipDomainNameValidation
			{
				set
				{
					base.PowerSharpParameters["SkipDomainNameValidation"] = value;
				}
			}

			// Token: 0x17003ED1 RID: 16081
			// (set) Token: 0x0600626D RID: 25197 RVA: 0x00097276 File Offset: 0x00095476
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003ED2 RID: 16082
			// (set) Token: 0x0600626E RID: 25198 RVA: 0x00097294 File Offset: 0x00095494
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003ED3 RID: 16083
			// (set) Token: 0x0600626F RID: 25199 RVA: 0x000972A7 File Offset: 0x000954A7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003ED4 RID: 16084
			// (set) Token: 0x06006270 RID: 25200 RVA: 0x000972BF File Offset: 0x000954BF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003ED5 RID: 16085
			// (set) Token: 0x06006271 RID: 25201 RVA: 0x000972D7 File Offset: 0x000954D7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003ED6 RID: 16086
			// (set) Token: 0x06006272 RID: 25202 RVA: 0x000972EF File Offset: 0x000954EF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003ED7 RID: 16087
			// (set) Token: 0x06006273 RID: 25203 RVA: 0x00097307 File Offset: 0x00095507
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
