using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000316 RID: 790
	public class NewCountryListCommand : SyntheticCommandWithPipelineInput<CountryList, CountryList>
	{
		// Token: 0x06003409 RID: 13321 RVA: 0x0005B528 File Offset: 0x00059728
		private NewCountryListCommand() : base("New-CountryList")
		{
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x0005B535 File Offset: 0x00059735
		public NewCountryListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x0005B544 File Offset: 0x00059744
		public virtual NewCountryListCommand SetParameters(NewCountryListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000317 RID: 791
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001996 RID: 6550
			// (set) Token: 0x0600340C RID: 13324 RVA: 0x0005B54E File Offset: 0x0005974E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001997 RID: 6551
			// (set) Token: 0x0600340D RID: 13325 RVA: 0x0005B561 File Offset: 0x00059761
			public virtual MultiValuedProperty<CountryInfo> Countries
			{
				set
				{
					base.PowerSharpParameters["Countries"] = value;
				}
			}

			// Token: 0x17001998 RID: 6552
			// (set) Token: 0x0600340E RID: 13326 RVA: 0x0005B574 File Offset: 0x00059774
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001999 RID: 6553
			// (set) Token: 0x0600340F RID: 13327 RVA: 0x0005B587 File Offset: 0x00059787
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700199A RID: 6554
			// (set) Token: 0x06003410 RID: 13328 RVA: 0x0005B59F File Offset: 0x0005979F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700199B RID: 6555
			// (set) Token: 0x06003411 RID: 13329 RVA: 0x0005B5B7 File Offset: 0x000597B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700199C RID: 6556
			// (set) Token: 0x06003412 RID: 13330 RVA: 0x0005B5CF File Offset: 0x000597CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700199D RID: 6557
			// (set) Token: 0x06003413 RID: 13331 RVA: 0x0005B5E7 File Offset: 0x000597E7
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
