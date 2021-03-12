using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000318 RID: 792
	public class RemoveCountryListCommand : SyntheticCommandWithPipelineInput<CountryList, CountryList>
	{
		// Token: 0x06003415 RID: 13333 RVA: 0x0005B607 File Offset: 0x00059807
		private RemoveCountryListCommand() : base("Remove-CountryList")
		{
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x0005B614 File Offset: 0x00059814
		public RemoveCountryListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x0005B623 File Offset: 0x00059823
		public virtual RemoveCountryListCommand SetParameters(RemoveCountryListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x0005B62D File Offset: 0x0005982D
		public virtual RemoveCountryListCommand SetParameters(RemoveCountryListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000319 RID: 793
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700199E RID: 6558
			// (set) Token: 0x06003419 RID: 13337 RVA: 0x0005B637 File Offset: 0x00059837
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700199F RID: 6559
			// (set) Token: 0x0600341A RID: 13338 RVA: 0x0005B64A File Offset: 0x0005984A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170019A0 RID: 6560
			// (set) Token: 0x0600341B RID: 13339 RVA: 0x0005B662 File Offset: 0x00059862
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170019A1 RID: 6561
			// (set) Token: 0x0600341C RID: 13340 RVA: 0x0005B67A File Offset: 0x0005987A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170019A2 RID: 6562
			// (set) Token: 0x0600341D RID: 13341 RVA: 0x0005B692 File Offset: 0x00059892
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170019A3 RID: 6563
			// (set) Token: 0x0600341E RID: 13342 RVA: 0x0005B6AA File Offset: 0x000598AA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170019A4 RID: 6564
			// (set) Token: 0x0600341F RID: 13343 RVA: 0x0005B6C2 File Offset: 0x000598C2
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200031A RID: 794
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170019A5 RID: 6565
			// (set) Token: 0x06003421 RID: 13345 RVA: 0x0005B6E2 File Offset: 0x000598E2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CountryListIdParameter(value) : null);
				}
			}

			// Token: 0x170019A6 RID: 6566
			// (set) Token: 0x06003422 RID: 13346 RVA: 0x0005B700 File Offset: 0x00059900
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170019A7 RID: 6567
			// (set) Token: 0x06003423 RID: 13347 RVA: 0x0005B713 File Offset: 0x00059913
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170019A8 RID: 6568
			// (set) Token: 0x06003424 RID: 13348 RVA: 0x0005B72B File Offset: 0x0005992B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170019A9 RID: 6569
			// (set) Token: 0x06003425 RID: 13349 RVA: 0x0005B743 File Offset: 0x00059943
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170019AA RID: 6570
			// (set) Token: 0x06003426 RID: 13350 RVA: 0x0005B75B File Offset: 0x0005995B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170019AB RID: 6571
			// (set) Token: 0x06003427 RID: 13351 RVA: 0x0005B773 File Offset: 0x00059973
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170019AC RID: 6572
			// (set) Token: 0x06003428 RID: 13352 RVA: 0x0005B78B File Offset: 0x0005998B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
