using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200074E RID: 1870
	public class SetIPAllowListConfigCommand : SyntheticCommandWithPipelineInputNoOutput<IPAllowListConfig>
	{
		// Token: 0x06005F90 RID: 24464 RVA: 0x00093930 File Offset: 0x00091B30
		private SetIPAllowListConfigCommand() : base("Set-IPAllowListConfig")
		{
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x0009393D File Offset: 0x00091B3D
		public SetIPAllowListConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x0009394C File Offset: 0x00091B4C
		public virtual SetIPAllowListConfigCommand SetParameters(SetIPAllowListConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200074F RID: 1871
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CAD RID: 15533
			// (set) Token: 0x06005F93 RID: 24467 RVA: 0x00093956 File Offset: 0x00091B56
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CAE RID: 15534
			// (set) Token: 0x06005F94 RID: 24468 RVA: 0x00093969 File Offset: 0x00091B69
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003CAF RID: 15535
			// (set) Token: 0x06005F95 RID: 24469 RVA: 0x00093981 File Offset: 0x00091B81
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003CB0 RID: 15536
			// (set) Token: 0x06005F96 RID: 24470 RVA: 0x00093999 File Offset: 0x00091B99
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003CB1 RID: 15537
			// (set) Token: 0x06005F97 RID: 24471 RVA: 0x000939B1 File Offset: 0x00091BB1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CB2 RID: 15538
			// (set) Token: 0x06005F98 RID: 24472 RVA: 0x000939C9 File Offset: 0x00091BC9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CB3 RID: 15539
			// (set) Token: 0x06005F99 RID: 24473 RVA: 0x000939E1 File Offset: 0x00091BE1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CB4 RID: 15540
			// (set) Token: 0x06005F9A RID: 24474 RVA: 0x000939F9 File Offset: 0x00091BF9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003CB5 RID: 15541
			// (set) Token: 0x06005F9B RID: 24475 RVA: 0x00093A11 File Offset: 0x00091C11
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
