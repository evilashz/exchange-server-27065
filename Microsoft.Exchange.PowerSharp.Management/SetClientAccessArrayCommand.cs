using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000620 RID: 1568
	public class SetClientAccessArrayCommand : SyntheticCommandWithPipelineInputNoOutput<ClientAccessArray>
	{
		// Token: 0x06005025 RID: 20517 RVA: 0x0007F261 File Offset: 0x0007D461
		private SetClientAccessArrayCommand() : base("Set-ClientAccessArray")
		{
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0007F26E File Offset: 0x0007D46E
		public SetClientAccessArrayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0007F27D File Offset: 0x0007D47D
		public virtual SetClientAccessArrayCommand SetParameters(SetClientAccessArrayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x0007F287 File Offset: 0x0007D487
		public virtual SetClientAccessArrayCommand SetParameters(SetClientAccessArrayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000621 RID: 1569
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F9E RID: 12190
			// (set) Token: 0x06005029 RID: 20521 RVA: 0x0007F291 File Offset: 0x0007D491
			public virtual string ArrayDefinition
			{
				set
				{
					base.PowerSharpParameters["ArrayDefinition"] = value;
				}
			}

			// Token: 0x17002F9F RID: 12191
			// (set) Token: 0x0600502A RID: 20522 RVA: 0x0007F2A4 File Offset: 0x0007D4A4
			public virtual int ServerCount
			{
				set
				{
					base.PowerSharpParameters["ServerCount"] = value;
				}
			}

			// Token: 0x17002FA0 RID: 12192
			// (set) Token: 0x0600502B RID: 20523 RVA: 0x0007F2BC File Offset: 0x0007D4BC
			public virtual string Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17002FA1 RID: 12193
			// (set) Token: 0x0600502C RID: 20524 RVA: 0x0007F2DA File Offset: 0x0007D4DA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FA2 RID: 12194
			// (set) Token: 0x0600502D RID: 20525 RVA: 0x0007F2ED File Offset: 0x0007D4ED
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002FA3 RID: 12195
			// (set) Token: 0x0600502E RID: 20526 RVA: 0x0007F300 File Offset: 0x0007D500
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FA4 RID: 12196
			// (set) Token: 0x0600502F RID: 20527 RVA: 0x0007F318 File Offset: 0x0007D518
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FA5 RID: 12197
			// (set) Token: 0x06005030 RID: 20528 RVA: 0x0007F330 File Offset: 0x0007D530
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FA6 RID: 12198
			// (set) Token: 0x06005031 RID: 20529 RVA: 0x0007F348 File Offset: 0x0007D548
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002FA7 RID: 12199
			// (set) Token: 0x06005032 RID: 20530 RVA: 0x0007F360 File Offset: 0x0007D560
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000622 RID: 1570
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002FA8 RID: 12200
			// (set) Token: 0x06005034 RID: 20532 RVA: 0x0007F380 File Offset: 0x0007D580
			public virtual ClientAccessArrayIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002FA9 RID: 12201
			// (set) Token: 0x06005035 RID: 20533 RVA: 0x0007F393 File Offset: 0x0007D593
			public virtual string ArrayDefinition
			{
				set
				{
					base.PowerSharpParameters["ArrayDefinition"] = value;
				}
			}

			// Token: 0x17002FAA RID: 12202
			// (set) Token: 0x06005036 RID: 20534 RVA: 0x0007F3A6 File Offset: 0x0007D5A6
			public virtual int ServerCount
			{
				set
				{
					base.PowerSharpParameters["ServerCount"] = value;
				}
			}

			// Token: 0x17002FAB RID: 12203
			// (set) Token: 0x06005037 RID: 20535 RVA: 0x0007F3BE File Offset: 0x0007D5BE
			public virtual string Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17002FAC RID: 12204
			// (set) Token: 0x06005038 RID: 20536 RVA: 0x0007F3DC File Offset: 0x0007D5DC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FAD RID: 12205
			// (set) Token: 0x06005039 RID: 20537 RVA: 0x0007F3EF File Offset: 0x0007D5EF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002FAE RID: 12206
			// (set) Token: 0x0600503A RID: 20538 RVA: 0x0007F402 File Offset: 0x0007D602
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FAF RID: 12207
			// (set) Token: 0x0600503B RID: 20539 RVA: 0x0007F41A File Offset: 0x0007D61A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FB0 RID: 12208
			// (set) Token: 0x0600503C RID: 20540 RVA: 0x0007F432 File Offset: 0x0007D632
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FB1 RID: 12209
			// (set) Token: 0x0600503D RID: 20541 RVA: 0x0007F44A File Offset: 0x0007D64A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002FB2 RID: 12210
			// (set) Token: 0x0600503E RID: 20542 RVA: 0x0007F462 File Offset: 0x0007D662
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
