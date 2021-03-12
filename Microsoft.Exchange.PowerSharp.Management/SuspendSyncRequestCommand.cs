using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AC7 RID: 2759
	public class SuspendSyncRequestCommand : SyntheticCommandWithPipelineInput<SyncRequestIdParameter, SyncRequestIdParameter>
	{
		// Token: 0x06008897 RID: 34967 RVA: 0x000C91DB File Offset: 0x000C73DB
		private SuspendSyncRequestCommand() : base("Suspend-SyncRequest")
		{
		}

		// Token: 0x06008898 RID: 34968 RVA: 0x000C91E8 File Offset: 0x000C73E8
		public SuspendSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008899 RID: 34969 RVA: 0x000C91F7 File Offset: 0x000C73F7
		public virtual SuspendSyncRequestCommand SetParameters(SuspendSyncRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600889A RID: 34970 RVA: 0x000C9201 File Offset: 0x000C7401
		public virtual SuspendSyncRequestCommand SetParameters(SuspendSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AC8 RID: 2760
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005EC2 RID: 24258
			// (set) Token: 0x0600889B RID: 34971 RVA: 0x000C920B File Offset: 0x000C740B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005EC3 RID: 24259
			// (set) Token: 0x0600889C RID: 34972 RVA: 0x000C9229 File Offset: 0x000C7429
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005EC4 RID: 24260
			// (set) Token: 0x0600889D RID: 34973 RVA: 0x000C923C File Offset: 0x000C743C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EC5 RID: 24261
			// (set) Token: 0x0600889E RID: 34974 RVA: 0x000C924F File Offset: 0x000C744F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EC6 RID: 24262
			// (set) Token: 0x0600889F RID: 34975 RVA: 0x000C9267 File Offset: 0x000C7467
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EC7 RID: 24263
			// (set) Token: 0x060088A0 RID: 34976 RVA: 0x000C927F File Offset: 0x000C747F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EC8 RID: 24264
			// (set) Token: 0x060088A1 RID: 34977 RVA: 0x000C9297 File Offset: 0x000C7497
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EC9 RID: 24265
			// (set) Token: 0x060088A2 RID: 34978 RVA: 0x000C92AF File Offset: 0x000C74AF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005ECA RID: 24266
			// (set) Token: 0x060088A3 RID: 34979 RVA: 0x000C92C7 File Offset: 0x000C74C7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000AC9 RID: 2761
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005ECB RID: 24267
			// (set) Token: 0x060088A5 RID: 34981 RVA: 0x000C92E7 File Offset: 0x000C74E7
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005ECC RID: 24268
			// (set) Token: 0x060088A6 RID: 34982 RVA: 0x000C92FA File Offset: 0x000C74FA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005ECD RID: 24269
			// (set) Token: 0x060088A7 RID: 34983 RVA: 0x000C930D File Offset: 0x000C750D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005ECE RID: 24270
			// (set) Token: 0x060088A8 RID: 34984 RVA: 0x000C9325 File Offset: 0x000C7525
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005ECF RID: 24271
			// (set) Token: 0x060088A9 RID: 34985 RVA: 0x000C933D File Offset: 0x000C753D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005ED0 RID: 24272
			// (set) Token: 0x060088AA RID: 34986 RVA: 0x000C9355 File Offset: 0x000C7555
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005ED1 RID: 24273
			// (set) Token: 0x060088AB RID: 34987 RVA: 0x000C936D File Offset: 0x000C756D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005ED2 RID: 24274
			// (set) Token: 0x060088AC RID: 34988 RVA: 0x000C9385 File Offset: 0x000C7585
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
