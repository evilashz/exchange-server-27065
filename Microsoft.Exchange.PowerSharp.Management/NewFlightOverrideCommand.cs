using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005B5 RID: 1461
	public class NewFlightOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06004C4A RID: 19530 RVA: 0x0007A46B File Offset: 0x0007866B
		private NewFlightOverrideCommand() : base("New-FlightOverride")
		{
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0007A478 File Offset: 0x00078678
		public NewFlightOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0007A487 File Offset: 0x00078687
		public virtual NewFlightOverrideCommand SetParameters(NewFlightOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005B6 RID: 1462
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002C99 RID: 11417
			// (set) Token: 0x06004C4D RID: 19533 RVA: 0x0007A491 File Offset: 0x00078691
			public virtual string Flight
			{
				set
				{
					base.PowerSharpParameters["Flight"] = value;
				}
			}

			// Token: 0x17002C9A RID: 11418
			// (set) Token: 0x06004C4E RID: 19534 RVA: 0x0007A4A4 File Offset: 0x000786A4
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002C9B RID: 11419
			// (set) Token: 0x06004C4F RID: 19535 RVA: 0x0007A4B7 File Offset: 0x000786B7
			public virtual Version FixVersion
			{
				set
				{
					base.PowerSharpParameters["FixVersion"] = value;
				}
			}

			// Token: 0x17002C9C RID: 11420
			// (set) Token: 0x06004C50 RID: 19536 RVA: 0x0007A4CA File Offset: 0x000786CA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002C9D RID: 11421
			// (set) Token: 0x06004C51 RID: 19537 RVA: 0x0007A4DD File Offset: 0x000786DD
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002C9E RID: 11422
			// (set) Token: 0x06004C52 RID: 19538 RVA: 0x0007A4F0 File Offset: 0x000786F0
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002C9F RID: 11423
			// (set) Token: 0x06004C53 RID: 19539 RVA: 0x0007A503 File Offset: 0x00078703
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17002CA0 RID: 11424
			// (set) Token: 0x06004C54 RID: 19540 RVA: 0x0007A516 File Offset: 0x00078716
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002CA1 RID: 11425
			// (set) Token: 0x06004C55 RID: 19541 RVA: 0x0007A529 File Offset: 0x00078729
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002CA2 RID: 11426
			// (set) Token: 0x06004C56 RID: 19542 RVA: 0x0007A541 File Offset: 0x00078741
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CA3 RID: 11427
			// (set) Token: 0x06004C57 RID: 19543 RVA: 0x0007A554 File Offset: 0x00078754
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CA4 RID: 11428
			// (set) Token: 0x06004C58 RID: 19544 RVA: 0x0007A56C File Offset: 0x0007876C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CA5 RID: 11429
			// (set) Token: 0x06004C59 RID: 19545 RVA: 0x0007A584 File Offset: 0x00078784
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CA6 RID: 11430
			// (set) Token: 0x06004C5A RID: 19546 RVA: 0x0007A59C File Offset: 0x0007879C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CA7 RID: 11431
			// (set) Token: 0x06004C5B RID: 19547 RVA: 0x0007A5B4 File Offset: 0x000787B4
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
