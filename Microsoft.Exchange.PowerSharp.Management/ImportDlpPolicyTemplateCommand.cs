using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005CE RID: 1486
	public class ImportDlpPolicyTemplateCommand : SyntheticCommandWithPipelineInput<ADComplianceProgramCollection, ADComplianceProgramCollection>
	{
		// Token: 0x06004D16 RID: 19734 RVA: 0x0007B3FC File Offset: 0x000795FC
		private ImportDlpPolicyTemplateCommand() : base("Import-DlpPolicyTemplate")
		{
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x0007B409 File Offset: 0x00079609
		public ImportDlpPolicyTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x0007B418 File Offset: 0x00079618
		public virtual ImportDlpPolicyTemplateCommand SetParameters(ImportDlpPolicyTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005CF RID: 1487
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D33 RID: 11571
			// (set) Token: 0x06004D19 RID: 19737 RVA: 0x0007B422 File Offset: 0x00079622
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17002D34 RID: 11572
			// (set) Token: 0x06004D1A RID: 19738 RVA: 0x0007B43A File Offset: 0x0007963A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D35 RID: 11573
			// (set) Token: 0x06004D1B RID: 19739 RVA: 0x0007B452 File Offset: 0x00079652
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D36 RID: 11574
			// (set) Token: 0x06004D1C RID: 19740 RVA: 0x0007B46A File Offset: 0x0007966A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D37 RID: 11575
			// (set) Token: 0x06004D1D RID: 19741 RVA: 0x0007B482 File Offset: 0x00079682
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D38 RID: 11576
			// (set) Token: 0x06004D1E RID: 19742 RVA: 0x0007B49A File Offset: 0x0007969A
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
