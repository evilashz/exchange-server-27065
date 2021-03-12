using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000455 RID: 1109
	public class DisableMailboxQuarantineCommand : SyntheticCommandWithPipelineInputNoOutput<GeneralMailboxIdParameter>
	{
		// Token: 0x06003FF0 RID: 16368 RVA: 0x0006AC15 File Offset: 0x00068E15
		private DisableMailboxQuarantineCommand() : base("Disable-MailboxQuarantine")
		{
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x0006AC22 File Offset: 0x00068E22
		public DisableMailboxQuarantineCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x0006AC31 File Offset: 0x00068E31
		public virtual DisableMailboxQuarantineCommand SetParameters(DisableMailboxQuarantineCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000456 RID: 1110
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170022FF RID: 8959
			// (set) Token: 0x06003FF3 RID: 16371 RVA: 0x0006AC3B File Offset: 0x00068E3B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GeneralMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17002300 RID: 8960
			// (set) Token: 0x06003FF4 RID: 16372 RVA: 0x0006AC59 File Offset: 0x00068E59
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002301 RID: 8961
			// (set) Token: 0x06003FF5 RID: 16373 RVA: 0x0006AC71 File Offset: 0x00068E71
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002302 RID: 8962
			// (set) Token: 0x06003FF6 RID: 16374 RVA: 0x0006AC89 File Offset: 0x00068E89
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002303 RID: 8963
			// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x0006ACA1 File Offset: 0x00068EA1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002304 RID: 8964
			// (set) Token: 0x06003FF8 RID: 16376 RVA: 0x0006ACB9 File Offset: 0x00068EB9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002305 RID: 8965
			// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x0006ACD1 File Offset: 0x00068ED1
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
