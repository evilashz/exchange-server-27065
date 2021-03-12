using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000013 RID: 19
	public class GetAdSiteLinkCommand : SyntheticCommandWithPipelineInput<ADSiteLink, ADSiteLink>
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x00032C32 File Offset: 0x00030E32
		private GetAdSiteLinkCommand() : base("Get-AdSiteLink")
		{
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00032C3F File Offset: 0x00030E3F
		public GetAdSiteLinkCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00032C4E File Offset: 0x00030E4E
		public virtual GetAdSiteLinkCommand SetParameters(GetAdSiteLinkCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00032C58 File Offset: 0x00030E58
		public virtual GetAdSiteLinkCommand SetParameters(GetAdSiteLinkCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000014 RID: 20
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000062 RID: 98
			// (set) Token: 0x060014D3 RID: 5331 RVA: 0x00032C62 File Offset: 0x00030E62
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000063 RID: 99
			// (set) Token: 0x060014D4 RID: 5332 RVA: 0x00032C75 File Offset: 0x00030E75
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000064 RID: 100
			// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00032C8D File Offset: 0x00030E8D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000065 RID: 101
			// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00032CA5 File Offset: 0x00030EA5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000066 RID: 102
			// (set) Token: 0x060014D7 RID: 5335 RVA: 0x00032CBD File Offset: 0x00030EBD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000015 RID: 21
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000067 RID: 103
			// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00032CDD File Offset: 0x00030EDD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AdSiteLinkIdParameter(value) : null);
				}
			}

			// Token: 0x17000068 RID: 104
			// (set) Token: 0x060014DA RID: 5338 RVA: 0x00032CFB File Offset: 0x00030EFB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000069 RID: 105
			// (set) Token: 0x060014DB RID: 5339 RVA: 0x00032D0E File Offset: 0x00030F0E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700006A RID: 106
			// (set) Token: 0x060014DC RID: 5340 RVA: 0x00032D26 File Offset: 0x00030F26
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700006B RID: 107
			// (set) Token: 0x060014DD RID: 5341 RVA: 0x00032D3E File Offset: 0x00030F3E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700006C RID: 108
			// (set) Token: 0x060014DE RID: 5342 RVA: 0x00032D56 File Offset: 0x00030F56
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
