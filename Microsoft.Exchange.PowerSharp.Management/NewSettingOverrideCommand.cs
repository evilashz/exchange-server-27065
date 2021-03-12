using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005C0 RID: 1472
	public class NewSettingOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06004CA6 RID: 19622 RVA: 0x0007AB6A File Offset: 0x00078D6A
		private NewSettingOverrideCommand() : base("New-SettingOverride")
		{
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x0007AB77 File Offset: 0x00078D77
		public NewSettingOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0007AB86 File Offset: 0x00078D86
		public virtual NewSettingOverrideCommand SetParameters(NewSettingOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005C1 RID: 1473
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002CDF RID: 11487
			// (set) Token: 0x06004CA9 RID: 19625 RVA: 0x0007AB90 File Offset: 0x00078D90
			public virtual string Component
			{
				set
				{
					base.PowerSharpParameters["Component"] = value;
				}
			}

			// Token: 0x17002CE0 RID: 11488
			// (set) Token: 0x06004CAA RID: 19626 RVA: 0x0007ABA3 File Offset: 0x00078DA3
			public virtual string Section
			{
				set
				{
					base.PowerSharpParameters["Section"] = value;
				}
			}

			// Token: 0x17002CE1 RID: 11489
			// (set) Token: 0x06004CAB RID: 19627 RVA: 0x0007ABB6 File Offset: 0x00078DB6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002CE2 RID: 11490
			// (set) Token: 0x06004CAC RID: 19628 RVA: 0x0007ABC9 File Offset: 0x00078DC9
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002CE3 RID: 11491
			// (set) Token: 0x06004CAD RID: 19629 RVA: 0x0007ABDC File Offset: 0x00078DDC
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002CE4 RID: 11492
			// (set) Token: 0x06004CAE RID: 19630 RVA: 0x0007ABEF File Offset: 0x00078DEF
			public virtual Version FixVersion
			{
				set
				{
					base.PowerSharpParameters["FixVersion"] = value;
				}
			}

			// Token: 0x17002CE5 RID: 11493
			// (set) Token: 0x06004CAF RID: 19631 RVA: 0x0007AC02 File Offset: 0x00078E02
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002CE6 RID: 11494
			// (set) Token: 0x06004CB0 RID: 19632 RVA: 0x0007AC15 File Offset: 0x00078E15
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17002CE7 RID: 11495
			// (set) Token: 0x06004CB1 RID: 19633 RVA: 0x0007AC28 File Offset: 0x00078E28
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002CE8 RID: 11496
			// (set) Token: 0x06004CB2 RID: 19634 RVA: 0x0007AC3B File Offset: 0x00078E3B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002CE9 RID: 11497
			// (set) Token: 0x06004CB3 RID: 19635 RVA: 0x0007AC53 File Offset: 0x00078E53
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CEA RID: 11498
			// (set) Token: 0x06004CB4 RID: 19636 RVA: 0x0007AC66 File Offset: 0x00078E66
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CEB RID: 11499
			// (set) Token: 0x06004CB5 RID: 19637 RVA: 0x0007AC7E File Offset: 0x00078E7E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CEC RID: 11500
			// (set) Token: 0x06004CB6 RID: 19638 RVA: 0x0007AC96 File Offset: 0x00078E96
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CED RID: 11501
			// (set) Token: 0x06004CB7 RID: 19639 RVA: 0x0007ACAE File Offset: 0x00078EAE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CEE RID: 11502
			// (set) Token: 0x06004CB8 RID: 19640 RVA: 0x0007ACC6 File Offset: 0x00078EC6
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
