using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004EB RID: 1259
	public class NewAvailabilityConfigCommand : SyntheticCommandWithPipelineInput<AvailabilityConfig, AvailabilityConfig>
	{
		// Token: 0x06004520 RID: 17696 RVA: 0x00071460 File Offset: 0x0006F660
		private NewAvailabilityConfigCommand() : base("New-AvailabilityConfig")
		{
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x0007146D File Offset: 0x0006F66D
		public NewAvailabilityConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x0007147C File Offset: 0x0006F67C
		public virtual NewAvailabilityConfigCommand SetParameters(NewAvailabilityConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004EC RID: 1260
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002703 RID: 9987
			// (set) Token: 0x06004523 RID: 17699 RVA: 0x00071486 File Offset: 0x0006F686
			public virtual string OrgWideAccount
			{
				set
				{
					base.PowerSharpParameters["OrgWideAccount"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17002704 RID: 9988
			// (set) Token: 0x06004524 RID: 17700 RVA: 0x000714A4 File Offset: 0x0006F6A4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002705 RID: 9989
			// (set) Token: 0x06004525 RID: 17701 RVA: 0x000714C2 File Offset: 0x0006F6C2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002706 RID: 9990
			// (set) Token: 0x06004526 RID: 17702 RVA: 0x000714D5 File Offset: 0x0006F6D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002707 RID: 9991
			// (set) Token: 0x06004527 RID: 17703 RVA: 0x000714ED File Offset: 0x0006F6ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002708 RID: 9992
			// (set) Token: 0x06004528 RID: 17704 RVA: 0x00071505 File Offset: 0x0006F705
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002709 RID: 9993
			// (set) Token: 0x06004529 RID: 17705 RVA: 0x0007151D File Offset: 0x0006F71D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700270A RID: 9994
			// (set) Token: 0x0600452A RID: 17706 RVA: 0x00071535 File Offset: 0x0006F735
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
