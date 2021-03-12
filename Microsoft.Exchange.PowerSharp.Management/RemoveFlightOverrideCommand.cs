using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005B7 RID: 1463
	public class RemoveFlightOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<SettingOverrideIdParameter>
	{
		// Token: 0x06004C5D RID: 19549 RVA: 0x0007A5D4 File Offset: 0x000787D4
		private RemoveFlightOverrideCommand() : base("Remove-FlightOverride")
		{
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0007A5E1 File Offset: 0x000787E1
		public RemoveFlightOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0007A5F0 File Offset: 0x000787F0
		public virtual RemoveFlightOverrideCommand SetParameters(RemoveFlightOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0007A5FA File Offset: 0x000787FA
		public virtual RemoveFlightOverrideCommand SetParameters(RemoveFlightOverrideCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005B8 RID: 1464
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002CA8 RID: 11432
			// (set) Token: 0x06004C61 RID: 19553 RVA: 0x0007A604 File Offset: 0x00078804
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CA9 RID: 11433
			// (set) Token: 0x06004C62 RID: 19554 RVA: 0x0007A617 File Offset: 0x00078817
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CAA RID: 11434
			// (set) Token: 0x06004C63 RID: 19555 RVA: 0x0007A62F File Offset: 0x0007882F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CAB RID: 11435
			// (set) Token: 0x06004C64 RID: 19556 RVA: 0x0007A647 File Offset: 0x00078847
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CAC RID: 11436
			// (set) Token: 0x06004C65 RID: 19557 RVA: 0x0007A65F File Offset: 0x0007885F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CAD RID: 11437
			// (set) Token: 0x06004C66 RID: 19558 RVA: 0x0007A677 File Offset: 0x00078877
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002CAE RID: 11438
			// (set) Token: 0x06004C67 RID: 19559 RVA: 0x0007A68F File Offset: 0x0007888F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005B9 RID: 1465
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002CAF RID: 11439
			// (set) Token: 0x06004C69 RID: 19561 RVA: 0x0007A6AF File Offset: 0x000788AF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SettingOverrideIdParameter(value) : null);
				}
			}

			// Token: 0x17002CB0 RID: 11440
			// (set) Token: 0x06004C6A RID: 19562 RVA: 0x0007A6CD File Offset: 0x000788CD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CB1 RID: 11441
			// (set) Token: 0x06004C6B RID: 19563 RVA: 0x0007A6E0 File Offset: 0x000788E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CB2 RID: 11442
			// (set) Token: 0x06004C6C RID: 19564 RVA: 0x0007A6F8 File Offset: 0x000788F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CB3 RID: 11443
			// (set) Token: 0x06004C6D RID: 19565 RVA: 0x0007A710 File Offset: 0x00078910
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CB4 RID: 11444
			// (set) Token: 0x06004C6E RID: 19566 RVA: 0x0007A728 File Offset: 0x00078928
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CB5 RID: 11445
			// (set) Token: 0x06004C6F RID: 19567 RVA: 0x0007A740 File Offset: 0x00078940
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002CB6 RID: 11446
			// (set) Token: 0x06004C70 RID: 19568 RVA: 0x0007A758 File Offset: 0x00078958
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
