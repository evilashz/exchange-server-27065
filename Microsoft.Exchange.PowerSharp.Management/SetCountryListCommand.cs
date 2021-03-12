using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200031B RID: 795
	public class SetCountryListCommand : SyntheticCommandWithPipelineInputNoOutput<CountryList>
	{
		// Token: 0x0600342A RID: 13354 RVA: 0x0005B7AB File Offset: 0x000599AB
		private SetCountryListCommand() : base("Set-CountryList")
		{
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x0005B7B8 File Offset: 0x000599B8
		public SetCountryListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x0005B7C7 File Offset: 0x000599C7
		public virtual SetCountryListCommand SetParameters(SetCountryListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x0005B7D1 File Offset: 0x000599D1
		public virtual SetCountryListCommand SetParameters(SetCountryListCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200031C RID: 796
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170019AD RID: 6573
			// (set) Token: 0x0600342E RID: 13358 RVA: 0x0005B7DB File Offset: 0x000599DB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170019AE RID: 6574
			// (set) Token: 0x0600342F RID: 13359 RVA: 0x0005B7EE File Offset: 0x000599EE
			public virtual MultiValuedProperty<CountryInfo> Countries
			{
				set
				{
					base.PowerSharpParameters["Countries"] = value;
				}
			}

			// Token: 0x170019AF RID: 6575
			// (set) Token: 0x06003430 RID: 13360 RVA: 0x0005B801 File Offset: 0x00059A01
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170019B0 RID: 6576
			// (set) Token: 0x06003431 RID: 13361 RVA: 0x0005B814 File Offset: 0x00059A14
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170019B1 RID: 6577
			// (set) Token: 0x06003432 RID: 13362 RVA: 0x0005B82C File Offset: 0x00059A2C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170019B2 RID: 6578
			// (set) Token: 0x06003433 RID: 13363 RVA: 0x0005B844 File Offset: 0x00059A44
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170019B3 RID: 6579
			// (set) Token: 0x06003434 RID: 13364 RVA: 0x0005B85C File Offset: 0x00059A5C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170019B4 RID: 6580
			// (set) Token: 0x06003435 RID: 13365 RVA: 0x0005B874 File Offset: 0x00059A74
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200031D RID: 797
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170019B5 RID: 6581
			// (set) Token: 0x06003437 RID: 13367 RVA: 0x0005B894 File Offset: 0x00059A94
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new CountryListIdParameter(value) : null);
				}
			}

			// Token: 0x170019B6 RID: 6582
			// (set) Token: 0x06003438 RID: 13368 RVA: 0x0005B8B2 File Offset: 0x00059AB2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170019B7 RID: 6583
			// (set) Token: 0x06003439 RID: 13369 RVA: 0x0005B8C5 File Offset: 0x00059AC5
			public virtual MultiValuedProperty<CountryInfo> Countries
			{
				set
				{
					base.PowerSharpParameters["Countries"] = value;
				}
			}

			// Token: 0x170019B8 RID: 6584
			// (set) Token: 0x0600343A RID: 13370 RVA: 0x0005B8D8 File Offset: 0x00059AD8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170019B9 RID: 6585
			// (set) Token: 0x0600343B RID: 13371 RVA: 0x0005B8EB File Offset: 0x00059AEB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170019BA RID: 6586
			// (set) Token: 0x0600343C RID: 13372 RVA: 0x0005B903 File Offset: 0x00059B03
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170019BB RID: 6587
			// (set) Token: 0x0600343D RID: 13373 RVA: 0x0005B91B File Offset: 0x00059B1B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170019BC RID: 6588
			// (set) Token: 0x0600343E RID: 13374 RVA: 0x0005B933 File Offset: 0x00059B33
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170019BD RID: 6589
			// (set) Token: 0x0600343F RID: 13375 RVA: 0x0005B94B File Offset: 0x00059B4B
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
