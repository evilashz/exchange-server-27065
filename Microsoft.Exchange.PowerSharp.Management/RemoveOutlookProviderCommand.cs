using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000501 RID: 1281
	public class RemoveOutlookProviderCommand : SyntheticCommandWithPipelineInput<OutlookProvider, OutlookProvider>
	{
		// Token: 0x060045BD RID: 17853 RVA: 0x000720A5 File Offset: 0x000702A5
		private RemoveOutlookProviderCommand() : base("Remove-OutlookProvider")
		{
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x000720B2 File Offset: 0x000702B2
		public RemoveOutlookProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x000720C1 File Offset: 0x000702C1
		public virtual RemoveOutlookProviderCommand SetParameters(RemoveOutlookProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x000720CB File Offset: 0x000702CB
		public virtual RemoveOutlookProviderCommand SetParameters(RemoveOutlookProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000502 RID: 1282
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002774 RID: 10100
			// (set) Token: 0x060045C1 RID: 17857 RVA: 0x000720D5 File Offset: 0x000702D5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002775 RID: 10101
			// (set) Token: 0x060045C2 RID: 17858 RVA: 0x000720E8 File Offset: 0x000702E8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002776 RID: 10102
			// (set) Token: 0x060045C3 RID: 17859 RVA: 0x00072100 File Offset: 0x00070300
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002777 RID: 10103
			// (set) Token: 0x060045C4 RID: 17860 RVA: 0x00072118 File Offset: 0x00070318
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002778 RID: 10104
			// (set) Token: 0x060045C5 RID: 17861 RVA: 0x00072130 File Offset: 0x00070330
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002779 RID: 10105
			// (set) Token: 0x060045C6 RID: 17862 RVA: 0x00072148 File Offset: 0x00070348
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700277A RID: 10106
			// (set) Token: 0x060045C7 RID: 17863 RVA: 0x00072160 File Offset: 0x00070360
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000503 RID: 1283
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700277B RID: 10107
			// (set) Token: 0x060045C9 RID: 17865 RVA: 0x00072180 File Offset: 0x00070380
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OutlookProviderIdParameter(value) : null);
				}
			}

			// Token: 0x1700277C RID: 10108
			// (set) Token: 0x060045CA RID: 17866 RVA: 0x0007219E File Offset: 0x0007039E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700277D RID: 10109
			// (set) Token: 0x060045CB RID: 17867 RVA: 0x000721B1 File Offset: 0x000703B1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700277E RID: 10110
			// (set) Token: 0x060045CC RID: 17868 RVA: 0x000721C9 File Offset: 0x000703C9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700277F RID: 10111
			// (set) Token: 0x060045CD RID: 17869 RVA: 0x000721E1 File Offset: 0x000703E1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002780 RID: 10112
			// (set) Token: 0x060045CE RID: 17870 RVA: 0x000721F9 File Offset: 0x000703F9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002781 RID: 10113
			// (set) Token: 0x060045CF RID: 17871 RVA: 0x00072211 File Offset: 0x00070411
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002782 RID: 10114
			// (set) Token: 0x060045D0 RID: 17872 RVA: 0x00072229 File Offset: 0x00070429
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
