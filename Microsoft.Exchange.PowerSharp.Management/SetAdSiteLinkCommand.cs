using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000016 RID: 22
	public class SetAdSiteLinkCommand : SyntheticCommandWithPipelineInputNoOutput<ADSiteLink>
	{
		// Token: 0x060014E0 RID: 5344 RVA: 0x00032D76 File Offset: 0x00030F76
		private SetAdSiteLinkCommand() : base("Set-AdSiteLink")
		{
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00032D83 File Offset: 0x00030F83
		public SetAdSiteLinkCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00032D92 File Offset: 0x00030F92
		public virtual SetAdSiteLinkCommand SetParameters(SetAdSiteLinkCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00032D9C File Offset: 0x00030F9C
		public virtual SetAdSiteLinkCommand SetParameters(SetAdSiteLinkCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000017 RID: 23
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700006D RID: 109
			// (set) Token: 0x060014E4 RID: 5348 RVA: 0x00032DA6 File Offset: 0x00030FA6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700006E RID: 110
			// (set) Token: 0x060014E5 RID: 5349 RVA: 0x00032DB9 File Offset: 0x00030FB9
			public virtual int? ExchangeCost
			{
				set
				{
					base.PowerSharpParameters["ExchangeCost"] = value;
				}
			}

			// Token: 0x1700006F RID: 111
			// (set) Token: 0x060014E6 RID: 5350 RVA: 0x00032DD1 File Offset: 0x00030FD1
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17000070 RID: 112
			// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00032DE9 File Offset: 0x00030FE9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000071 RID: 113
			// (set) Token: 0x060014E8 RID: 5352 RVA: 0x00032DFC File Offset: 0x00030FFC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000072 RID: 114
			// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00032E14 File Offset: 0x00031014
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000073 RID: 115
			// (set) Token: 0x060014EA RID: 5354 RVA: 0x00032E2C File Offset: 0x0003102C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000074 RID: 116
			// (set) Token: 0x060014EB RID: 5355 RVA: 0x00032E44 File Offset: 0x00031044
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000075 RID: 117
			// (set) Token: 0x060014EC RID: 5356 RVA: 0x00032E5C File Offset: 0x0003105C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000018 RID: 24
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000076 RID: 118
			// (set) Token: 0x060014EE RID: 5358 RVA: 0x00032E7C File Offset: 0x0003107C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AdSiteLinkIdParameter(value) : null);
				}
			}

			// Token: 0x17000077 RID: 119
			// (set) Token: 0x060014EF RID: 5359 RVA: 0x00032E9A File Offset: 0x0003109A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000078 RID: 120
			// (set) Token: 0x060014F0 RID: 5360 RVA: 0x00032EAD File Offset: 0x000310AD
			public virtual int? ExchangeCost
			{
				set
				{
					base.PowerSharpParameters["ExchangeCost"] = value;
				}
			}

			// Token: 0x17000079 RID: 121
			// (set) Token: 0x060014F1 RID: 5361 RVA: 0x00032EC5 File Offset: 0x000310C5
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x1700007A RID: 122
			// (set) Token: 0x060014F2 RID: 5362 RVA: 0x00032EDD File Offset: 0x000310DD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700007B RID: 123
			// (set) Token: 0x060014F3 RID: 5363 RVA: 0x00032EF0 File Offset: 0x000310F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700007C RID: 124
			// (set) Token: 0x060014F4 RID: 5364 RVA: 0x00032F08 File Offset: 0x00031108
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700007D RID: 125
			// (set) Token: 0x060014F5 RID: 5365 RVA: 0x00032F20 File Offset: 0x00031120
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700007E RID: 126
			// (set) Token: 0x060014F6 RID: 5366 RVA: 0x00032F38 File Offset: 0x00031138
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700007F RID: 127
			// (set) Token: 0x060014F7 RID: 5367 RVA: 0x00032F50 File Offset: 0x00031150
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
