using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000313 RID: 787
	public class GetCountryListCommand : SyntheticCommandWithPipelineInput<CountryList, CountryList>
	{
		// Token: 0x060033F8 RID: 13304 RVA: 0x0005B3E4 File Offset: 0x000595E4
		private GetCountryListCommand() : base("Get-CountryList")
		{
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x0005B3F1 File Offset: 0x000595F1
		public GetCountryListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x0005B400 File Offset: 0x00059600
		public virtual GetCountryListCommand SetParameters(GetCountryListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x0005B40A File Offset: 0x0005960A
		public virtual GetCountryListCommand SetParameters(GetCountryListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000314 RID: 788
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700198B RID: 6539
			// (set) Token: 0x060033FC RID: 13308 RVA: 0x0005B414 File Offset: 0x00059614
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700198C RID: 6540
			// (set) Token: 0x060033FD RID: 13309 RVA: 0x0005B427 File Offset: 0x00059627
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700198D RID: 6541
			// (set) Token: 0x060033FE RID: 13310 RVA: 0x0005B43F File Offset: 0x0005963F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700198E RID: 6542
			// (set) Token: 0x060033FF RID: 13311 RVA: 0x0005B457 File Offset: 0x00059657
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700198F RID: 6543
			// (set) Token: 0x06003400 RID: 13312 RVA: 0x0005B46F File Offset: 0x0005966F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000315 RID: 789
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001990 RID: 6544
			// (set) Token: 0x06003402 RID: 13314 RVA: 0x0005B48F File Offset: 0x0005968F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CountryListIdParameter(value) : null);
				}
			}

			// Token: 0x17001991 RID: 6545
			// (set) Token: 0x06003403 RID: 13315 RVA: 0x0005B4AD File Offset: 0x000596AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001992 RID: 6546
			// (set) Token: 0x06003404 RID: 13316 RVA: 0x0005B4C0 File Offset: 0x000596C0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001993 RID: 6547
			// (set) Token: 0x06003405 RID: 13317 RVA: 0x0005B4D8 File Offset: 0x000596D8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001994 RID: 6548
			// (set) Token: 0x06003406 RID: 13318 RVA: 0x0005B4F0 File Offset: 0x000596F0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001995 RID: 6549
			// (set) Token: 0x06003407 RID: 13319 RVA: 0x0005B508 File Offset: 0x00059708
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
