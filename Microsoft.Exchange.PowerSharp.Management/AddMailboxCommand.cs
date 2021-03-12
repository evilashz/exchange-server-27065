using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C68 RID: 3176
	public class AddMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009C0D RID: 39949 RVA: 0x000E25E4 File Offset: 0x000E07E4
		private AddMailboxCommand() : base("Add-Mailbox")
		{
		}

		// Token: 0x06009C0E RID: 39950 RVA: 0x000E25F1 File Offset: 0x000E07F1
		public AddMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009C0F RID: 39951 RVA: 0x000E2600 File Offset: 0x000E0800
		public virtual AddMailboxCommand SetParameters(AddMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009C10 RID: 39952 RVA: 0x000E260A File Offset: 0x000E080A
		public virtual AddMailboxCommand SetParameters(AddMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C69 RID: 3177
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006EF6 RID: 28406
			// (set) Token: 0x06009C11 RID: 39953 RVA: 0x000E2614 File Offset: 0x000E0814
			public virtual SwitchParameter AuxArchive
			{
				set
				{
					base.PowerSharpParameters["AuxArchive"] = value;
				}
			}

			// Token: 0x17006EF7 RID: 28407
			// (set) Token: 0x06009C12 RID: 39954 RVA: 0x000E262C File Offset: 0x000E082C
			public virtual SwitchParameter AuxPrimary
			{
				set
				{
					base.PowerSharpParameters["AuxPrimary"] = value;
				}
			}

			// Token: 0x17006EF8 RID: 28408
			// (set) Token: 0x06009C13 RID: 39955 RVA: 0x000E2644 File Offset: 0x000E0844
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006EF9 RID: 28409
			// (set) Token: 0x06009C14 RID: 39956 RVA: 0x000E2657 File Offset: 0x000E0857
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006EFA RID: 28410
			// (set) Token: 0x06009C15 RID: 39957 RVA: 0x000E266F File Offset: 0x000E086F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006EFB RID: 28411
			// (set) Token: 0x06009C16 RID: 39958 RVA: 0x000E2687 File Offset: 0x000E0887
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006EFC RID: 28412
			// (set) Token: 0x06009C17 RID: 39959 RVA: 0x000E269F File Offset: 0x000E089F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006EFD RID: 28413
			// (set) Token: 0x06009C18 RID: 39960 RVA: 0x000E26B7 File Offset: 0x000E08B7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C6A RID: 3178
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006EFE RID: 28414
			// (set) Token: 0x06009C1A RID: 39962 RVA: 0x000E26D7 File Offset: 0x000E08D7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006EFF RID: 28415
			// (set) Token: 0x06009C1B RID: 39963 RVA: 0x000E26F5 File Offset: 0x000E08F5
			public virtual SwitchParameter AuxArchive
			{
				set
				{
					base.PowerSharpParameters["AuxArchive"] = value;
				}
			}

			// Token: 0x17006F00 RID: 28416
			// (set) Token: 0x06009C1C RID: 39964 RVA: 0x000E270D File Offset: 0x000E090D
			public virtual SwitchParameter AuxPrimary
			{
				set
				{
					base.PowerSharpParameters["AuxPrimary"] = value;
				}
			}

			// Token: 0x17006F01 RID: 28417
			// (set) Token: 0x06009C1D RID: 39965 RVA: 0x000E2725 File Offset: 0x000E0925
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006F02 RID: 28418
			// (set) Token: 0x06009C1E RID: 39966 RVA: 0x000E2738 File Offset: 0x000E0938
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006F03 RID: 28419
			// (set) Token: 0x06009C1F RID: 39967 RVA: 0x000E2750 File Offset: 0x000E0950
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006F04 RID: 28420
			// (set) Token: 0x06009C20 RID: 39968 RVA: 0x000E2768 File Offset: 0x000E0968
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006F05 RID: 28421
			// (set) Token: 0x06009C21 RID: 39969 RVA: 0x000E2780 File Offset: 0x000E0980
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006F06 RID: 28422
			// (set) Token: 0x06009C22 RID: 39970 RVA: 0x000E2798 File Offset: 0x000E0998
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
