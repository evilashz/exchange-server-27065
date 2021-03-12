using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005C5 RID: 1477
	public class SetSettingOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<SettingOverride>
	{
		// Token: 0x06004CCF RID: 19663 RVA: 0x0007AE8A File Offset: 0x0007908A
		private SetSettingOverrideCommand() : base("Set-SettingOverride")
		{
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0007AE97 File Offset: 0x00079097
		public SetSettingOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x0007AEA6 File Offset: 0x000790A6
		public virtual SetSettingOverrideCommand SetParameters(SetSettingOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0007AEB0 File Offset: 0x000790B0
		public virtual SetSettingOverrideCommand SetParameters(SetSettingOverrideCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005C6 RID: 1478
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002CFE RID: 11518
			// (set) Token: 0x06004CD3 RID: 19667 RVA: 0x0007AEBA File Offset: 0x000790BA
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002CFF RID: 11519
			// (set) Token: 0x06004CD4 RID: 19668 RVA: 0x0007AECD File Offset: 0x000790CD
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002D00 RID: 11520
			// (set) Token: 0x06004CD5 RID: 19669 RVA: 0x0007AEE0 File Offset: 0x000790E0
			public virtual Version FixVersion
			{
				set
				{
					base.PowerSharpParameters["FixVersion"] = value;
				}
			}

			// Token: 0x17002D01 RID: 11521
			// (set) Token: 0x06004CD6 RID: 19670 RVA: 0x0007AEF3 File Offset: 0x000790F3
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002D02 RID: 11522
			// (set) Token: 0x06004CD7 RID: 19671 RVA: 0x0007AF06 File Offset: 0x00079106
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17002D03 RID: 11523
			// (set) Token: 0x06004CD8 RID: 19672 RVA: 0x0007AF19 File Offset: 0x00079119
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002D04 RID: 11524
			// (set) Token: 0x06004CD9 RID: 19673 RVA: 0x0007AF2C File Offset: 0x0007912C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D05 RID: 11525
			// (set) Token: 0x06004CDA RID: 19674 RVA: 0x0007AF3F File Offset: 0x0007913F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002D06 RID: 11526
			// (set) Token: 0x06004CDB RID: 19675 RVA: 0x0007AF52 File Offset: 0x00079152
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D07 RID: 11527
			// (set) Token: 0x06004CDC RID: 19676 RVA: 0x0007AF6A File Offset: 0x0007916A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D08 RID: 11528
			// (set) Token: 0x06004CDD RID: 19677 RVA: 0x0007AF82 File Offset: 0x00079182
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D09 RID: 11529
			// (set) Token: 0x06004CDE RID: 19678 RVA: 0x0007AF9A File Offset: 0x0007919A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D0A RID: 11530
			// (set) Token: 0x06004CDF RID: 19679 RVA: 0x0007AFB2 File Offset: 0x000791B2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002D0B RID: 11531
			// (set) Token: 0x06004CE0 RID: 19680 RVA: 0x0007AFCA File Offset: 0x000791CA
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005C7 RID: 1479
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D0C RID: 11532
			// (set) Token: 0x06004CE2 RID: 19682 RVA: 0x0007AFEA File Offset: 0x000791EA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SettingOverrideIdParameter(value) : null);
				}
			}

			// Token: 0x17002D0D RID: 11533
			// (set) Token: 0x06004CE3 RID: 19683 RVA: 0x0007B008 File Offset: 0x00079208
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002D0E RID: 11534
			// (set) Token: 0x06004CE4 RID: 19684 RVA: 0x0007B01B File Offset: 0x0007921B
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002D0F RID: 11535
			// (set) Token: 0x06004CE5 RID: 19685 RVA: 0x0007B02E File Offset: 0x0007922E
			public virtual Version FixVersion
			{
				set
				{
					base.PowerSharpParameters["FixVersion"] = value;
				}
			}

			// Token: 0x17002D10 RID: 11536
			// (set) Token: 0x06004CE6 RID: 19686 RVA: 0x0007B041 File Offset: 0x00079241
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002D11 RID: 11537
			// (set) Token: 0x06004CE7 RID: 19687 RVA: 0x0007B054 File Offset: 0x00079254
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17002D12 RID: 11538
			// (set) Token: 0x06004CE8 RID: 19688 RVA: 0x0007B067 File Offset: 0x00079267
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002D13 RID: 11539
			// (set) Token: 0x06004CE9 RID: 19689 RVA: 0x0007B07A File Offset: 0x0007927A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D14 RID: 11540
			// (set) Token: 0x06004CEA RID: 19690 RVA: 0x0007B08D File Offset: 0x0007928D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002D15 RID: 11541
			// (set) Token: 0x06004CEB RID: 19691 RVA: 0x0007B0A0 File Offset: 0x000792A0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D16 RID: 11542
			// (set) Token: 0x06004CEC RID: 19692 RVA: 0x0007B0B8 File Offset: 0x000792B8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D17 RID: 11543
			// (set) Token: 0x06004CED RID: 19693 RVA: 0x0007B0D0 File Offset: 0x000792D0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D18 RID: 11544
			// (set) Token: 0x06004CEE RID: 19694 RVA: 0x0007B0E8 File Offset: 0x000792E8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D19 RID: 11545
			// (set) Token: 0x06004CEF RID: 19695 RVA: 0x0007B100 File Offset: 0x00079300
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002D1A RID: 11546
			// (set) Token: 0x06004CF0 RID: 19696 RVA: 0x0007B118 File Offset: 0x00079318
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
