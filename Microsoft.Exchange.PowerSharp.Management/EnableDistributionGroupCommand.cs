using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BF8 RID: 3064
	public class EnableDistributionGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x060094A2 RID: 38050 RVA: 0x000D8A13 File Offset: 0x000D6C13
		private EnableDistributionGroupCommand() : base("Enable-DistributionGroup")
		{
		}

		// Token: 0x060094A3 RID: 38051 RVA: 0x000D8A20 File Offset: 0x000D6C20
		public EnableDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060094A4 RID: 38052 RVA: 0x000D8A2F File Offset: 0x000D6C2F
		public virtual EnableDistributionGroupCommand SetParameters(EnableDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060094A5 RID: 38053 RVA: 0x000D8A39 File Offset: 0x000D6C39
		public virtual EnableDistributionGroupCommand SetParameters(EnableDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BF9 RID: 3065
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700686B RID: 26731
			// (set) Token: 0x060094A6 RID: 38054 RVA: 0x000D8A43 File Offset: 0x000D6C43
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700686C RID: 26732
			// (set) Token: 0x060094A7 RID: 38055 RVA: 0x000D8A56 File Offset: 0x000D6C56
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700686D RID: 26733
			// (set) Token: 0x060094A8 RID: 38056 RVA: 0x000D8A69 File Offset: 0x000D6C69
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700686E RID: 26734
			// (set) Token: 0x060094A9 RID: 38057 RVA: 0x000D8A81 File Offset: 0x000D6C81
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700686F RID: 26735
			// (set) Token: 0x060094AA RID: 38058 RVA: 0x000D8A99 File Offset: 0x000D6C99
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006870 RID: 26736
			// (set) Token: 0x060094AB RID: 38059 RVA: 0x000D8AAC File Offset: 0x000D6CAC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006871 RID: 26737
			// (set) Token: 0x060094AC RID: 38060 RVA: 0x000D8AC4 File Offset: 0x000D6CC4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006872 RID: 26738
			// (set) Token: 0x060094AD RID: 38061 RVA: 0x000D8ADC File Offset: 0x000D6CDC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006873 RID: 26739
			// (set) Token: 0x060094AE RID: 38062 RVA: 0x000D8AF4 File Offset: 0x000D6CF4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006874 RID: 26740
			// (set) Token: 0x060094AF RID: 38063 RVA: 0x000D8B0C File Offset: 0x000D6D0C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BFA RID: 3066
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006875 RID: 26741
			// (set) Token: 0x060094B1 RID: 38065 RVA: 0x000D8B2C File Offset: 0x000D6D2C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006876 RID: 26742
			// (set) Token: 0x060094B2 RID: 38066 RVA: 0x000D8B4A File Offset: 0x000D6D4A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006877 RID: 26743
			// (set) Token: 0x060094B3 RID: 38067 RVA: 0x000D8B5D File Offset: 0x000D6D5D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006878 RID: 26744
			// (set) Token: 0x060094B4 RID: 38068 RVA: 0x000D8B70 File Offset: 0x000D6D70
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006879 RID: 26745
			// (set) Token: 0x060094B5 RID: 38069 RVA: 0x000D8B88 File Offset: 0x000D6D88
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700687A RID: 26746
			// (set) Token: 0x060094B6 RID: 38070 RVA: 0x000D8BA0 File Offset: 0x000D6DA0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700687B RID: 26747
			// (set) Token: 0x060094B7 RID: 38071 RVA: 0x000D8BB3 File Offset: 0x000D6DB3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700687C RID: 26748
			// (set) Token: 0x060094B8 RID: 38072 RVA: 0x000D8BCB File Offset: 0x000D6DCB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700687D RID: 26749
			// (set) Token: 0x060094B9 RID: 38073 RVA: 0x000D8BE3 File Offset: 0x000D6DE3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700687E RID: 26750
			// (set) Token: 0x060094BA RID: 38074 RVA: 0x000D8BFB File Offset: 0x000D6DFB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700687F RID: 26751
			// (set) Token: 0x060094BB RID: 38075 RVA: 0x000D8C13 File Offset: 0x000D6E13
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
