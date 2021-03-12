using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004F3 RID: 1267
	public class SetAvailabilityConfigCommand : SyntheticCommandWithPipelineInputNoOutput<AvailabilityConfig>
	{
		// Token: 0x06004556 RID: 17750 RVA: 0x00071892 File Offset: 0x0006FA92
		private SetAvailabilityConfigCommand() : base("Set-AvailabilityConfig")
		{
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x0007189F File Offset: 0x0006FA9F
		public SetAvailabilityConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x000718AE File Offset: 0x0006FAAE
		public virtual SetAvailabilityConfigCommand SetParameters(SetAvailabilityConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x000718B8 File Offset: 0x0006FAB8
		public virtual SetAvailabilityConfigCommand SetParameters(SetAvailabilityConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004F4 RID: 1268
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002729 RID: 10025
			// (set) Token: 0x0600455A RID: 17754 RVA: 0x000718C2 File Offset: 0x0006FAC2
			public virtual string OrgWideAccount
			{
				set
				{
					base.PowerSharpParameters["OrgWideAccount"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x1700272A RID: 10026
			// (set) Token: 0x0600455B RID: 17755 RVA: 0x000718E0 File Offset: 0x0006FAE0
			public virtual string PerUserAccount
			{
				set
				{
					base.PowerSharpParameters["PerUserAccount"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x1700272B RID: 10027
			// (set) Token: 0x0600455C RID: 17756 RVA: 0x000718FE File Offset: 0x0006FAFE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700272C RID: 10028
			// (set) Token: 0x0600455D RID: 17757 RVA: 0x00071911 File Offset: 0x0006FB11
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700272D RID: 10029
			// (set) Token: 0x0600455E RID: 17758 RVA: 0x00071929 File Offset: 0x0006FB29
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700272E RID: 10030
			// (set) Token: 0x0600455F RID: 17759 RVA: 0x00071941 File Offset: 0x0006FB41
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700272F RID: 10031
			// (set) Token: 0x06004560 RID: 17760 RVA: 0x00071959 File Offset: 0x0006FB59
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002730 RID: 10032
			// (set) Token: 0x06004561 RID: 17761 RVA: 0x00071971 File Offset: 0x0006FB71
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004F5 RID: 1269
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002731 RID: 10033
			// (set) Token: 0x06004563 RID: 17763 RVA: 0x00071991 File Offset: 0x0006FB91
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002732 RID: 10034
			// (set) Token: 0x06004564 RID: 17764 RVA: 0x000719AF File Offset: 0x0006FBAF
			public virtual string OrgWideAccount
			{
				set
				{
					base.PowerSharpParameters["OrgWideAccount"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17002733 RID: 10035
			// (set) Token: 0x06004565 RID: 17765 RVA: 0x000719CD File Offset: 0x0006FBCD
			public virtual string PerUserAccount
			{
				set
				{
					base.PowerSharpParameters["PerUserAccount"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17002734 RID: 10036
			// (set) Token: 0x06004566 RID: 17766 RVA: 0x000719EB File Offset: 0x0006FBEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002735 RID: 10037
			// (set) Token: 0x06004567 RID: 17767 RVA: 0x000719FE File Offset: 0x0006FBFE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002736 RID: 10038
			// (set) Token: 0x06004568 RID: 17768 RVA: 0x00071A16 File Offset: 0x0006FC16
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002737 RID: 10039
			// (set) Token: 0x06004569 RID: 17769 RVA: 0x00071A2E File Offset: 0x0006FC2E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002738 RID: 10040
			// (set) Token: 0x0600456A RID: 17770 RVA: 0x00071A46 File Offset: 0x0006FC46
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002739 RID: 10041
			// (set) Token: 0x0600456B RID: 17771 RVA: 0x00071A5E File Offset: 0x0006FC5E
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
