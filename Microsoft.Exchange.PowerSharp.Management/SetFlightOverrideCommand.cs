using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005BA RID: 1466
	public class SetFlightOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<SettingOverride>
	{
		// Token: 0x06004C72 RID: 19570 RVA: 0x0007A778 File Offset: 0x00078978
		private SetFlightOverrideCommand() : base("Set-FlightOverride")
		{
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0007A785 File Offset: 0x00078985
		public SetFlightOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0007A794 File Offset: 0x00078994
		public virtual SetFlightOverrideCommand SetParameters(SetFlightOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0007A79E File Offset: 0x0007899E
		public virtual SetFlightOverrideCommand SetParameters(SetFlightOverrideCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005BB RID: 1467
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002CB7 RID: 11447
			// (set) Token: 0x06004C76 RID: 19574 RVA: 0x0007A7A8 File Offset: 0x000789A8
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002CB8 RID: 11448
			// (set) Token: 0x06004C77 RID: 19575 RVA: 0x0007A7BB File Offset: 0x000789BB
			public virtual Version FixVersion
			{
				set
				{
					base.PowerSharpParameters["FixVersion"] = value;
				}
			}

			// Token: 0x17002CB9 RID: 11449
			// (set) Token: 0x06004C78 RID: 19576 RVA: 0x0007A7CE File Offset: 0x000789CE
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002CBA RID: 11450
			// (set) Token: 0x06004C79 RID: 19577 RVA: 0x0007A7E1 File Offset: 0x000789E1
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002CBB RID: 11451
			// (set) Token: 0x06004C7A RID: 19578 RVA: 0x0007A7F4 File Offset: 0x000789F4
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17002CBC RID: 11452
			// (set) Token: 0x06004C7B RID: 19579 RVA: 0x0007A807 File Offset: 0x00078A07
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002CBD RID: 11453
			// (set) Token: 0x06004C7C RID: 19580 RVA: 0x0007A81A File Offset: 0x00078A1A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CBE RID: 11454
			// (set) Token: 0x06004C7D RID: 19581 RVA: 0x0007A82D File Offset: 0x00078A2D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002CBF RID: 11455
			// (set) Token: 0x06004C7E RID: 19582 RVA: 0x0007A840 File Offset: 0x00078A40
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CC0 RID: 11456
			// (set) Token: 0x06004C7F RID: 19583 RVA: 0x0007A858 File Offset: 0x00078A58
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CC1 RID: 11457
			// (set) Token: 0x06004C80 RID: 19584 RVA: 0x0007A870 File Offset: 0x00078A70
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CC2 RID: 11458
			// (set) Token: 0x06004C81 RID: 19585 RVA: 0x0007A888 File Offset: 0x00078A88
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CC3 RID: 11459
			// (set) Token: 0x06004C82 RID: 19586 RVA: 0x0007A8A0 File Offset: 0x00078AA0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002CC4 RID: 11460
			// (set) Token: 0x06004C83 RID: 19587 RVA: 0x0007A8B8 File Offset: 0x00078AB8
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005BC RID: 1468
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002CC5 RID: 11461
			// (set) Token: 0x06004C85 RID: 19589 RVA: 0x0007A8D8 File Offset: 0x00078AD8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SettingOverrideIdParameter(value) : null);
				}
			}

			// Token: 0x17002CC6 RID: 11462
			// (set) Token: 0x06004C86 RID: 19590 RVA: 0x0007A8F6 File Offset: 0x00078AF6
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x17002CC7 RID: 11463
			// (set) Token: 0x06004C87 RID: 19591 RVA: 0x0007A909 File Offset: 0x00078B09
			public virtual Version FixVersion
			{
				set
				{
					base.PowerSharpParameters["FixVersion"] = value;
				}
			}

			// Token: 0x17002CC8 RID: 11464
			// (set) Token: 0x06004C88 RID: 19592 RVA: 0x0007A91C File Offset: 0x00078B1C
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x17002CC9 RID: 11465
			// (set) Token: 0x06004C89 RID: 19593 RVA: 0x0007A92F File Offset: 0x00078B2F
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002CCA RID: 11466
			// (set) Token: 0x06004C8A RID: 19594 RVA: 0x0007A942 File Offset: 0x00078B42
			public virtual MultiValuedProperty<string> Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17002CCB RID: 11467
			// (set) Token: 0x06004C8B RID: 19595 RVA: 0x0007A955 File Offset: 0x00078B55
			public virtual string Reason
			{
				set
				{
					base.PowerSharpParameters["Reason"] = value;
				}
			}

			// Token: 0x17002CCC RID: 11468
			// (set) Token: 0x06004C8C RID: 19596 RVA: 0x0007A968 File Offset: 0x00078B68
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CCD RID: 11469
			// (set) Token: 0x06004C8D RID: 19597 RVA: 0x0007A97B File Offset: 0x00078B7B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002CCE RID: 11470
			// (set) Token: 0x06004C8E RID: 19598 RVA: 0x0007A98E File Offset: 0x00078B8E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CCF RID: 11471
			// (set) Token: 0x06004C8F RID: 19599 RVA: 0x0007A9A6 File Offset: 0x00078BA6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CD0 RID: 11472
			// (set) Token: 0x06004C90 RID: 19600 RVA: 0x0007A9BE File Offset: 0x00078BBE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CD1 RID: 11473
			// (set) Token: 0x06004C91 RID: 19601 RVA: 0x0007A9D6 File Offset: 0x00078BD6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002CD2 RID: 11474
			// (set) Token: 0x06004C92 RID: 19602 RVA: 0x0007A9EE File Offset: 0x00078BEE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002CD3 RID: 11475
			// (set) Token: 0x06004C93 RID: 19603 RVA: 0x0007AA06 File Offset: 0x00078C06
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
