using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005BD RID: 1469
	public class GetSettingOverrideCommand : SyntheticCommand<object>
	{
		// Token: 0x06004C95 RID: 19605 RVA: 0x0007AA26 File Offset: 0x00078C26
		private GetSettingOverrideCommand() : base("Get-SettingOverride")
		{
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0007AA33 File Offset: 0x00078C33
		public GetSettingOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0007AA42 File Offset: 0x00078C42
		public virtual GetSettingOverrideCommand SetParameters(GetSettingOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0007AA4C File Offset: 0x00078C4C
		public virtual GetSettingOverrideCommand SetParameters(GetSettingOverrideCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005BE RID: 1470
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002CD4 RID: 11476
			// (set) Token: 0x06004C99 RID: 19609 RVA: 0x0007AA56 File Offset: 0x00078C56
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CD5 RID: 11477
			// (set) Token: 0x06004C9A RID: 19610 RVA: 0x0007AA69 File Offset: 0x00078C69
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CD6 RID: 11478
			// (set) Token: 0x06004C9B RID: 19611 RVA: 0x0007AA81 File Offset: 0x00078C81
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CD7 RID: 11479
			// (set) Token: 0x06004C9C RID: 19612 RVA: 0x0007AA99 File Offset: 0x00078C99
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CD8 RID: 11480
			// (set) Token: 0x06004C9D RID: 19613 RVA: 0x0007AAB1 File Offset: 0x00078CB1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020005BF RID: 1471
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002CD9 RID: 11481
			// (set) Token: 0x06004C9F RID: 19615 RVA: 0x0007AAD1 File Offset: 0x00078CD1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SettingOverrideIdParameter(value) : null);
				}
			}

			// Token: 0x17002CDA RID: 11482
			// (set) Token: 0x06004CA0 RID: 19616 RVA: 0x0007AAEF File Offset: 0x00078CEF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002CDB RID: 11483
			// (set) Token: 0x06004CA1 RID: 19617 RVA: 0x0007AB02 File Offset: 0x00078D02
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002CDC RID: 11484
			// (set) Token: 0x06004CA2 RID: 19618 RVA: 0x0007AB1A File Offset: 0x00078D1A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002CDD RID: 11485
			// (set) Token: 0x06004CA3 RID: 19619 RVA: 0x0007AB32 File Offset: 0x00078D32
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002CDE RID: 11486
			// (set) Token: 0x06004CA4 RID: 19620 RVA: 0x0007AB4A File Offset: 0x00078D4A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
