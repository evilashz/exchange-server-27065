using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D62 RID: 3426
	public class RemoveRemoteMailboxCommand : SyntheticCommandWithPipelineInput<RemoteMailboxIdParameter, RemoteMailboxIdParameter>
	{
		// Token: 0x0600B584 RID: 46468 RVA: 0x001054B2 File Offset: 0x001036B2
		private RemoveRemoteMailboxCommand() : base("Remove-RemoteMailbox")
		{
		}

		// Token: 0x0600B585 RID: 46469 RVA: 0x001054BF File Offset: 0x001036BF
		public RemoveRemoteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B586 RID: 46470 RVA: 0x001054CE File Offset: 0x001036CE
		public virtual RemoveRemoteMailboxCommand SetParameters(RemoveRemoteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B587 RID: 46471 RVA: 0x001054D8 File Offset: 0x001036D8
		public virtual RemoveRemoteMailboxCommand SetParameters(RemoveRemoteMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D63 RID: 3427
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008679 RID: 34425
			// (set) Token: 0x0600B588 RID: 46472 RVA: 0x001054E2 File Offset: 0x001036E2
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x1700867A RID: 34426
			// (set) Token: 0x0600B589 RID: 46473 RVA: 0x001054FA File Offset: 0x001036FA
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700867B RID: 34427
			// (set) Token: 0x0600B58A RID: 46474 RVA: 0x00105512 File Offset: 0x00103712
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700867C RID: 34428
			// (set) Token: 0x0600B58B RID: 46475 RVA: 0x00105525 File Offset: 0x00103725
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700867D RID: 34429
			// (set) Token: 0x0600B58C RID: 46476 RVA: 0x0010553D File Offset: 0x0010373D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700867E RID: 34430
			// (set) Token: 0x0600B58D RID: 46477 RVA: 0x00105555 File Offset: 0x00103755
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700867F RID: 34431
			// (set) Token: 0x0600B58E RID: 46478 RVA: 0x0010556D File Offset: 0x0010376D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008680 RID: 34432
			// (set) Token: 0x0600B58F RID: 46479 RVA: 0x00105585 File Offset: 0x00103785
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17008681 RID: 34433
			// (set) Token: 0x0600B590 RID: 46480 RVA: 0x0010559D File Offset: 0x0010379D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000D64 RID: 3428
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17008682 RID: 34434
			// (set) Token: 0x0600B592 RID: 46482 RVA: 0x001055BD File Offset: 0x001037BD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RemoteMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008683 RID: 34435
			// (set) Token: 0x0600B593 RID: 46483 RVA: 0x001055DB File Offset: 0x001037DB
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x17008684 RID: 34436
			// (set) Token: 0x0600B594 RID: 46484 RVA: 0x001055F3 File Offset: 0x001037F3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008685 RID: 34437
			// (set) Token: 0x0600B595 RID: 46485 RVA: 0x0010560B File Offset: 0x0010380B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008686 RID: 34438
			// (set) Token: 0x0600B596 RID: 46486 RVA: 0x0010561E File Offset: 0x0010381E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008687 RID: 34439
			// (set) Token: 0x0600B597 RID: 46487 RVA: 0x00105636 File Offset: 0x00103836
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008688 RID: 34440
			// (set) Token: 0x0600B598 RID: 46488 RVA: 0x0010564E File Offset: 0x0010384E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008689 RID: 34441
			// (set) Token: 0x0600B599 RID: 46489 RVA: 0x00105666 File Offset: 0x00103866
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700868A RID: 34442
			// (set) Token: 0x0600B59A RID: 46490 RVA: 0x0010567E File Offset: 0x0010387E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700868B RID: 34443
			// (set) Token: 0x0600B59B RID: 46491 RVA: 0x00105696 File Offset: 0x00103896
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
