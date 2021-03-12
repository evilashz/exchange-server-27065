using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B94 RID: 2964
	public class SetUMServerLanguagesCommand : SyntheticCommandWithPipelineInputNoOutput<Server>
	{
		// Token: 0x06008F8A RID: 36746 RVA: 0x000D2060 File Offset: 0x000D0260
		private SetUMServerLanguagesCommand() : base("Set-UMServerLanguages")
		{
		}

		// Token: 0x06008F8B RID: 36747 RVA: 0x000D206D File Offset: 0x000D026D
		public SetUMServerLanguagesCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008F8C RID: 36748 RVA: 0x000D207C File Offset: 0x000D027C
		public virtual SetUMServerLanguagesCommand SetParameters(SetUMServerLanguagesCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008F8D RID: 36749 RVA: 0x000D2086 File Offset: 0x000D0286
		public virtual SetUMServerLanguagesCommand SetParameters(SetUMServerLanguagesCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B95 RID: 2965
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700641B RID: 25627
			// (set) Token: 0x06008F8E RID: 36750 RVA: 0x000D2090 File Offset: 0x000D0290
			public virtual MultiValuedProperty<UMLanguage> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700641C RID: 25628
			// (set) Token: 0x06008F8F RID: 36751 RVA: 0x000D20A3 File Offset: 0x000D02A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700641D RID: 25629
			// (set) Token: 0x06008F90 RID: 36752 RVA: 0x000D20B6 File Offset: 0x000D02B6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700641E RID: 25630
			// (set) Token: 0x06008F91 RID: 36753 RVA: 0x000D20CE File Offset: 0x000D02CE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700641F RID: 25631
			// (set) Token: 0x06008F92 RID: 36754 RVA: 0x000D20E6 File Offset: 0x000D02E6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006420 RID: 25632
			// (set) Token: 0x06008F93 RID: 36755 RVA: 0x000D20FE File Offset: 0x000D02FE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006421 RID: 25633
			// (set) Token: 0x06008F94 RID: 36756 RVA: 0x000D2116 File Offset: 0x000D0316
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B96 RID: 2966
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006422 RID: 25634
			// (set) Token: 0x06008F96 RID: 36758 RVA: 0x000D2136 File Offset: 0x000D0336
			public virtual UMServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006423 RID: 25635
			// (set) Token: 0x06008F97 RID: 36759 RVA: 0x000D2149 File Offset: 0x000D0349
			public virtual MultiValuedProperty<UMLanguage> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17006424 RID: 25636
			// (set) Token: 0x06008F98 RID: 36760 RVA: 0x000D215C File Offset: 0x000D035C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006425 RID: 25637
			// (set) Token: 0x06008F99 RID: 36761 RVA: 0x000D216F File Offset: 0x000D036F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006426 RID: 25638
			// (set) Token: 0x06008F9A RID: 36762 RVA: 0x000D2187 File Offset: 0x000D0387
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006427 RID: 25639
			// (set) Token: 0x06008F9B RID: 36763 RVA: 0x000D219F File Offset: 0x000D039F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006428 RID: 25640
			// (set) Token: 0x06008F9C RID: 36764 RVA: 0x000D21B7 File Offset: 0x000D03B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006429 RID: 25641
			// (set) Token: 0x06008F9D RID: 36765 RVA: 0x000D21CF File Offset: 0x000D03CF
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
