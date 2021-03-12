using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DFC RID: 3580
	public class GetSiteMailboxDiagnosticsCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientIdParameter>
	{
		// Token: 0x0600D545 RID: 54597 RVA: 0x0012F26D File Offset: 0x0012D46D
		private GetSiteMailboxDiagnosticsCommand() : base("Get-SiteMailboxDiagnostics")
		{
		}

		// Token: 0x0600D546 RID: 54598 RVA: 0x0012F27A File Offset: 0x0012D47A
		public GetSiteMailboxDiagnosticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D547 RID: 54599 RVA: 0x0012F289 File Offset: 0x0012D489
		public virtual GetSiteMailboxDiagnosticsCommand SetParameters(GetSiteMailboxDiagnosticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D548 RID: 54600 RVA: 0x0012F293 File Offset: 0x0012D493
		public virtual GetSiteMailboxDiagnosticsCommand SetParameters(GetSiteMailboxDiagnosticsCommand.TeamMailboxITProParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DFD RID: 3581
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A506 RID: 42246
			// (set) Token: 0x0600D549 RID: 54601 RVA: 0x0012F29D File Offset: 0x0012D49D
			public virtual SwitchParameter SendMeEmail
			{
				set
				{
					base.PowerSharpParameters["SendMeEmail"] = value;
				}
			}

			// Token: 0x1700A507 RID: 42247
			// (set) Token: 0x0600D54A RID: 54602 RVA: 0x0012F2B5 File Offset: 0x0012D4B5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A508 RID: 42248
			// (set) Token: 0x0600D54B RID: 54603 RVA: 0x0012F2D3 File Offset: 0x0012D4D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A509 RID: 42249
			// (set) Token: 0x0600D54C RID: 54604 RVA: 0x0012F2EB File Offset: 0x0012D4EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A50A RID: 42250
			// (set) Token: 0x0600D54D RID: 54605 RVA: 0x0012F303 File Offset: 0x0012D503
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A50B RID: 42251
			// (set) Token: 0x0600D54E RID: 54606 RVA: 0x0012F31B File Offset: 0x0012D51B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A50C RID: 42252
			// (set) Token: 0x0600D54F RID: 54607 RVA: 0x0012F333 File Offset: 0x0012D533
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DFE RID: 3582
		public class TeamMailboxITProParameters : ParametersBase
		{
			// Token: 0x1700A50D RID: 42253
			// (set) Token: 0x0600D551 RID: 54609 RVA: 0x0012F353 File Offset: 0x0012D553
			public virtual SwitchParameter BypassOwnerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassOwnerCheck"] = value;
				}
			}

			// Token: 0x1700A50E RID: 42254
			// (set) Token: 0x0600D552 RID: 54610 RVA: 0x0012F36B File Offset: 0x0012D56B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A50F RID: 42255
			// (set) Token: 0x0600D553 RID: 54611 RVA: 0x0012F389 File Offset: 0x0012D589
			public virtual SwitchParameter SendMeEmail
			{
				set
				{
					base.PowerSharpParameters["SendMeEmail"] = value;
				}
			}

			// Token: 0x1700A510 RID: 42256
			// (set) Token: 0x0600D554 RID: 54612 RVA: 0x0012F3A1 File Offset: 0x0012D5A1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A511 RID: 42257
			// (set) Token: 0x0600D555 RID: 54613 RVA: 0x0012F3BF File Offset: 0x0012D5BF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A512 RID: 42258
			// (set) Token: 0x0600D556 RID: 54614 RVA: 0x0012F3D7 File Offset: 0x0012D5D7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A513 RID: 42259
			// (set) Token: 0x0600D557 RID: 54615 RVA: 0x0012F3EF File Offset: 0x0012D5EF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A514 RID: 42260
			// (set) Token: 0x0600D558 RID: 54616 RVA: 0x0012F407 File Offset: 0x0012D607
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A515 RID: 42261
			// (set) Token: 0x0600D559 RID: 54617 RVA: 0x0012F41F File Offset: 0x0012D61F
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
