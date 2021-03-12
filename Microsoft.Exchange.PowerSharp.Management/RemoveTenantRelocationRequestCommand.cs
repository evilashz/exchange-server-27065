using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200038B RID: 907
	public class RemoveTenantRelocationRequestCommand : SyntheticCommandWithPipelineInput<TenantRelocationRequest, TenantRelocationRequest>
	{
		// Token: 0x060038EF RID: 14575 RVA: 0x00061B75 File Offset: 0x0005FD75
		private RemoveTenantRelocationRequestCommand() : base("Remove-TenantRelocationRequest")
		{
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x00061B82 File Offset: 0x0005FD82
		public RemoveTenantRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x00061B91 File Offset: 0x0005FD91
		public virtual RemoveTenantRelocationRequestCommand SetParameters(RemoveTenantRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x00061B9B File Offset: 0x0005FD9B
		public virtual RemoveTenantRelocationRequestCommand SetParameters(RemoveTenantRelocationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200038C RID: 908
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001D92 RID: 7570
			// (set) Token: 0x060038F3 RID: 14579 RVA: 0x00061BA5 File Offset: 0x0005FDA5
			public virtual SwitchParameter Complete
			{
				set
				{
					base.PowerSharpParameters["Complete"] = value;
				}
			}

			// Token: 0x17001D93 RID: 7571
			// (set) Token: 0x060038F4 RID: 14580 RVA: 0x00061BBD File Offset: 0x0005FDBD
			public virtual SwitchParameter DeprovisionedTarget
			{
				set
				{
					base.PowerSharpParameters["DeprovisionedTarget"] = value;
				}
			}

			// Token: 0x17001D94 RID: 7572
			// (set) Token: 0x060038F5 RID: 14581 RVA: 0x00061BD5 File Offset: 0x0005FDD5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D95 RID: 7573
			// (set) Token: 0x060038F6 RID: 14582 RVA: 0x00061BE8 File Offset: 0x0005FDE8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D96 RID: 7574
			// (set) Token: 0x060038F7 RID: 14583 RVA: 0x00061C00 File Offset: 0x0005FE00
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D97 RID: 7575
			// (set) Token: 0x060038F8 RID: 14584 RVA: 0x00061C18 File Offset: 0x0005FE18
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D98 RID: 7576
			// (set) Token: 0x060038F9 RID: 14585 RVA: 0x00061C30 File Offset: 0x0005FE30
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D99 RID: 7577
			// (set) Token: 0x060038FA RID: 14586 RVA: 0x00061C48 File Offset: 0x0005FE48
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001D9A RID: 7578
			// (set) Token: 0x060038FB RID: 14587 RVA: 0x00061C60 File Offset: 0x0005FE60
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200038D RID: 909
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001D9B RID: 7579
			// (set) Token: 0x060038FD RID: 14589 RVA: 0x00061C80 File Offset: 0x0005FE80
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001D9C RID: 7580
			// (set) Token: 0x060038FE RID: 14590 RVA: 0x00061C9E File Offset: 0x0005FE9E
			public virtual SwitchParameter Complete
			{
				set
				{
					base.PowerSharpParameters["Complete"] = value;
				}
			}

			// Token: 0x17001D9D RID: 7581
			// (set) Token: 0x060038FF RID: 14591 RVA: 0x00061CB6 File Offset: 0x0005FEB6
			public virtual SwitchParameter DeprovisionedTarget
			{
				set
				{
					base.PowerSharpParameters["DeprovisionedTarget"] = value;
				}
			}

			// Token: 0x17001D9E RID: 7582
			// (set) Token: 0x06003900 RID: 14592 RVA: 0x00061CCE File Offset: 0x0005FECE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D9F RID: 7583
			// (set) Token: 0x06003901 RID: 14593 RVA: 0x00061CE1 File Offset: 0x0005FEE1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DA0 RID: 7584
			// (set) Token: 0x06003902 RID: 14594 RVA: 0x00061CF9 File Offset: 0x0005FEF9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DA1 RID: 7585
			// (set) Token: 0x06003903 RID: 14595 RVA: 0x00061D11 File Offset: 0x0005FF11
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DA2 RID: 7586
			// (set) Token: 0x06003904 RID: 14596 RVA: 0x00061D29 File Offset: 0x0005FF29
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001DA3 RID: 7587
			// (set) Token: 0x06003905 RID: 14597 RVA: 0x00061D41 File Offset: 0x0005FF41
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001DA4 RID: 7588
			// (set) Token: 0x06003906 RID: 14598 RVA: 0x00061D59 File Offset: 0x0005FF59
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
