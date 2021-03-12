using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A17 RID: 2583
	public class SuspendMailboxExportRequestCommand : SyntheticCommandWithPipelineInput<MailboxExportRequestIdParameter, MailboxExportRequestIdParameter>
	{
		// Token: 0x06008138 RID: 33080 RVA: 0x000BF8B0 File Offset: 0x000BDAB0
		private SuspendMailboxExportRequestCommand() : base("Suspend-MailboxExportRequest")
		{
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x000BF8BD File Offset: 0x000BDABD
		public SuspendMailboxExportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x000BF8CC File Offset: 0x000BDACC
		public virtual SuspendMailboxExportRequestCommand SetParameters(SuspendMailboxExportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x000BF8D6 File Offset: 0x000BDAD6
		public virtual SuspendMailboxExportRequestCommand SetParameters(SuspendMailboxExportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A18 RID: 2584
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170058C3 RID: 22723
			// (set) Token: 0x0600813C RID: 33084 RVA: 0x000BF8E0 File Offset: 0x000BDAE0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170058C4 RID: 22724
			// (set) Token: 0x0600813D RID: 33085 RVA: 0x000BF8FE File Offset: 0x000BDAFE
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170058C5 RID: 22725
			// (set) Token: 0x0600813E RID: 33086 RVA: 0x000BF911 File Offset: 0x000BDB11
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058C6 RID: 22726
			// (set) Token: 0x0600813F RID: 33087 RVA: 0x000BF924 File Offset: 0x000BDB24
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058C7 RID: 22727
			// (set) Token: 0x06008140 RID: 33088 RVA: 0x000BF93C File Offset: 0x000BDB3C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058C8 RID: 22728
			// (set) Token: 0x06008141 RID: 33089 RVA: 0x000BF954 File Offset: 0x000BDB54
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058C9 RID: 22729
			// (set) Token: 0x06008142 RID: 33090 RVA: 0x000BF96C File Offset: 0x000BDB6C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170058CA RID: 22730
			// (set) Token: 0x06008143 RID: 33091 RVA: 0x000BF984 File Offset: 0x000BDB84
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170058CB RID: 22731
			// (set) Token: 0x06008144 RID: 33092 RVA: 0x000BF99C File Offset: 0x000BDB9C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A19 RID: 2585
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170058CC RID: 22732
			// (set) Token: 0x06008146 RID: 33094 RVA: 0x000BF9BC File Offset: 0x000BDBBC
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170058CD RID: 22733
			// (set) Token: 0x06008147 RID: 33095 RVA: 0x000BF9CF File Offset: 0x000BDBCF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058CE RID: 22734
			// (set) Token: 0x06008148 RID: 33096 RVA: 0x000BF9E2 File Offset: 0x000BDBE2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058CF RID: 22735
			// (set) Token: 0x06008149 RID: 33097 RVA: 0x000BF9FA File Offset: 0x000BDBFA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058D0 RID: 22736
			// (set) Token: 0x0600814A RID: 33098 RVA: 0x000BFA12 File Offset: 0x000BDC12
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058D1 RID: 22737
			// (set) Token: 0x0600814B RID: 33099 RVA: 0x000BFA2A File Offset: 0x000BDC2A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170058D2 RID: 22738
			// (set) Token: 0x0600814C RID: 33100 RVA: 0x000BFA42 File Offset: 0x000BDC42
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170058D3 RID: 22739
			// (set) Token: 0x0600814D RID: 33101 RVA: 0x000BFA5A File Offset: 0x000BDC5A
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
