using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A30 RID: 2608
	public class SuspendMailboxImportRequestCommand : SyntheticCommandWithPipelineInput<MailboxImportRequestIdParameter, MailboxImportRequestIdParameter>
	{
		// Token: 0x0600822A RID: 33322 RVA: 0x000C0C1A File Offset: 0x000BEE1A
		private SuspendMailboxImportRequestCommand() : base("Suspend-MailboxImportRequest")
		{
		}

		// Token: 0x0600822B RID: 33323 RVA: 0x000C0C27 File Offset: 0x000BEE27
		public SuspendMailboxImportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600822C RID: 33324 RVA: 0x000C0C36 File Offset: 0x000BEE36
		public virtual SuspendMailboxImportRequestCommand SetParameters(SuspendMailboxImportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600822D RID: 33325 RVA: 0x000C0C40 File Offset: 0x000BEE40
		public virtual SuspendMailboxImportRequestCommand SetParameters(SuspendMailboxImportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A31 RID: 2609
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005983 RID: 22915
			// (set) Token: 0x0600822E RID: 33326 RVA: 0x000C0C4A File Offset: 0x000BEE4A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005984 RID: 22916
			// (set) Token: 0x0600822F RID: 33327 RVA: 0x000C0C68 File Offset: 0x000BEE68
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005985 RID: 22917
			// (set) Token: 0x06008230 RID: 33328 RVA: 0x000C0C7B File Offset: 0x000BEE7B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005986 RID: 22918
			// (set) Token: 0x06008231 RID: 33329 RVA: 0x000C0C8E File Offset: 0x000BEE8E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005987 RID: 22919
			// (set) Token: 0x06008232 RID: 33330 RVA: 0x000C0CA6 File Offset: 0x000BEEA6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005988 RID: 22920
			// (set) Token: 0x06008233 RID: 33331 RVA: 0x000C0CBE File Offset: 0x000BEEBE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005989 RID: 22921
			// (set) Token: 0x06008234 RID: 33332 RVA: 0x000C0CD6 File Offset: 0x000BEED6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700598A RID: 22922
			// (set) Token: 0x06008235 RID: 33333 RVA: 0x000C0CEE File Offset: 0x000BEEEE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700598B RID: 22923
			// (set) Token: 0x06008236 RID: 33334 RVA: 0x000C0D06 File Offset: 0x000BEF06
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A32 RID: 2610
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700598C RID: 22924
			// (set) Token: 0x06008238 RID: 33336 RVA: 0x000C0D26 File Offset: 0x000BEF26
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x1700598D RID: 22925
			// (set) Token: 0x06008239 RID: 33337 RVA: 0x000C0D39 File Offset: 0x000BEF39
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700598E RID: 22926
			// (set) Token: 0x0600823A RID: 33338 RVA: 0x000C0D4C File Offset: 0x000BEF4C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700598F RID: 22927
			// (set) Token: 0x0600823B RID: 33339 RVA: 0x000C0D64 File Offset: 0x000BEF64
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005990 RID: 22928
			// (set) Token: 0x0600823C RID: 33340 RVA: 0x000C0D7C File Offset: 0x000BEF7C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005991 RID: 22929
			// (set) Token: 0x0600823D RID: 33341 RVA: 0x000C0D94 File Offset: 0x000BEF94
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005992 RID: 22930
			// (set) Token: 0x0600823E RID: 33342 RVA: 0x000C0DAC File Offset: 0x000BEFAC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005993 RID: 22931
			// (set) Token: 0x0600823F RID: 33343 RVA: 0x000C0DC4 File Offset: 0x000BEFC4
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
