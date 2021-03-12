using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001FE RID: 510
	public class RemoveMailboxAssociationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x060028F7 RID: 10487 RVA: 0x0004CF41 File Offset: 0x0004B141
		private RemoveMailboxAssociationCommand() : base("Remove-MailboxAssociation")
		{
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0004CF4E File Offset: 0x0004B14E
		public RemoveMailboxAssociationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x0004CF5D File Offset: 0x0004B15D
		public virtual RemoveMailboxAssociationCommand SetParameters(RemoveMailboxAssociationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x0004CF67 File Offset: 0x0004B167
		public virtual RemoveMailboxAssociationCommand SetParameters(RemoveMailboxAssociationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001FF RID: 511
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170010B4 RID: 4276
			// (set) Token: 0x060028FB RID: 10491 RVA: 0x0004CF71 File Offset: 0x0004B171
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxAssociationIdParameter(value) : null);
				}
			}

			// Token: 0x170010B5 RID: 4277
			// (set) Token: 0x060028FC RID: 10492 RVA: 0x0004CF8F File Offset: 0x0004B18F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170010B6 RID: 4278
			// (set) Token: 0x060028FD RID: 10493 RVA: 0x0004CFA7 File Offset: 0x0004B1A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170010B7 RID: 4279
			// (set) Token: 0x060028FE RID: 10494 RVA: 0x0004CFBA File Offset: 0x0004B1BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010B8 RID: 4280
			// (set) Token: 0x060028FF RID: 10495 RVA: 0x0004CFD2 File Offset: 0x0004B1D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010B9 RID: 4281
			// (set) Token: 0x06002900 RID: 10496 RVA: 0x0004CFEA File Offset: 0x0004B1EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010BA RID: 4282
			// (set) Token: 0x06002901 RID: 10497 RVA: 0x0004D002 File Offset: 0x0004B202
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010BB RID: 4283
			// (set) Token: 0x06002902 RID: 10498 RVA: 0x0004D01A File Offset: 0x0004B21A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170010BC RID: 4284
			// (set) Token: 0x06002903 RID: 10499 RVA: 0x0004D032 File Offset: 0x0004B232
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000200 RID: 512
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170010BD RID: 4285
			// (set) Token: 0x06002905 RID: 10501 RVA: 0x0004D052 File Offset: 0x0004B252
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170010BE RID: 4286
			// (set) Token: 0x06002906 RID: 10502 RVA: 0x0004D06A File Offset: 0x0004B26A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170010BF RID: 4287
			// (set) Token: 0x06002907 RID: 10503 RVA: 0x0004D07D File Offset: 0x0004B27D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170010C0 RID: 4288
			// (set) Token: 0x06002908 RID: 10504 RVA: 0x0004D095 File Offset: 0x0004B295
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170010C1 RID: 4289
			// (set) Token: 0x06002909 RID: 10505 RVA: 0x0004D0AD File Offset: 0x0004B2AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170010C2 RID: 4290
			// (set) Token: 0x0600290A RID: 10506 RVA: 0x0004D0C5 File Offset: 0x0004B2C5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170010C3 RID: 4291
			// (set) Token: 0x0600290B RID: 10507 RVA: 0x0004D0DD File Offset: 0x0004B2DD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170010C4 RID: 4292
			// (set) Token: 0x0600290C RID: 10508 RVA: 0x0004D0F5 File Offset: 0x0004B2F5
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
