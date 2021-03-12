using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004ED RID: 1261
	public class RemoveAvailabilityConfigCommand : SyntheticCommandWithPipelineInput<AvailabilityConfig, AvailabilityConfig>
	{
		// Token: 0x0600452C RID: 17708 RVA: 0x00071555 File Offset: 0x0006F755
		private RemoveAvailabilityConfigCommand() : base("Remove-AvailabilityConfig")
		{
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x00071562 File Offset: 0x0006F762
		public RemoveAvailabilityConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x00071571 File Offset: 0x0006F771
		public virtual RemoveAvailabilityConfigCommand SetParameters(RemoveAvailabilityConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x0007157B File Offset: 0x0006F77B
		public virtual RemoveAvailabilityConfigCommand SetParameters(RemoveAvailabilityConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004EE RID: 1262
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700270B RID: 9995
			// (set) Token: 0x06004530 RID: 17712 RVA: 0x00071585 File Offset: 0x0006F785
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AvailabilityConfigIdParameter(value) : null);
				}
			}

			// Token: 0x1700270C RID: 9996
			// (set) Token: 0x06004531 RID: 17713 RVA: 0x000715A3 File Offset: 0x0006F7A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700270D RID: 9997
			// (set) Token: 0x06004532 RID: 17714 RVA: 0x000715B6 File Offset: 0x0006F7B6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700270E RID: 9998
			// (set) Token: 0x06004533 RID: 17715 RVA: 0x000715CE File Offset: 0x0006F7CE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700270F RID: 9999
			// (set) Token: 0x06004534 RID: 17716 RVA: 0x000715E6 File Offset: 0x0006F7E6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002710 RID: 10000
			// (set) Token: 0x06004535 RID: 17717 RVA: 0x000715FE File Offset: 0x0006F7FE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002711 RID: 10001
			// (set) Token: 0x06004536 RID: 17718 RVA: 0x00071616 File Offset: 0x0006F816
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002712 RID: 10002
			// (set) Token: 0x06004537 RID: 17719 RVA: 0x0007162E File Offset: 0x0006F82E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020004EF RID: 1263
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002713 RID: 10003
			// (set) Token: 0x06004539 RID: 17721 RVA: 0x0007164E File Offset: 0x0006F84E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002714 RID: 10004
			// (set) Token: 0x0600453A RID: 17722 RVA: 0x00071661 File Offset: 0x0006F861
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002715 RID: 10005
			// (set) Token: 0x0600453B RID: 17723 RVA: 0x00071679 File Offset: 0x0006F879
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002716 RID: 10006
			// (set) Token: 0x0600453C RID: 17724 RVA: 0x00071691 File Offset: 0x0006F891
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002717 RID: 10007
			// (set) Token: 0x0600453D RID: 17725 RVA: 0x000716A9 File Offset: 0x0006F8A9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002718 RID: 10008
			// (set) Token: 0x0600453E RID: 17726 RVA: 0x000716C1 File Offset: 0x0006F8C1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002719 RID: 10009
			// (set) Token: 0x0600453F RID: 17727 RVA: 0x000716D9 File Offset: 0x0006F8D9
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
