using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000A6 RID: 166
	public class EnableOrganizationCustomizationCommand : SyntheticCommand<object>
	{
		// Token: 0x060019BB RID: 6587 RVA: 0x00038FDA File Offset: 0x000371DA
		private EnableOrganizationCustomizationCommand() : base("Enable-OrganizationCustomization")
		{
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00038FE7 File Offset: 0x000371E7
		public EnableOrganizationCustomizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00038FF6 File Offset: 0x000371F6
		public virtual EnableOrganizationCustomizationCommand SetParameters(EnableOrganizationCustomizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000A7 RID: 167
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000428 RID: 1064
			// (set) Token: 0x060019BE RID: 6590 RVA: 0x00039000 File Offset: 0x00037200
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000429 RID: 1065
			// (set) Token: 0x060019BF RID: 6591 RVA: 0x0003901E File Offset: 0x0003721E
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x1700042A RID: 1066
			// (set) Token: 0x060019C0 RID: 6592 RVA: 0x00039036 File Offset: 0x00037236
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700042B RID: 1067
			// (set) Token: 0x060019C1 RID: 6593 RVA: 0x00039049 File Offset: 0x00037249
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x1700042C RID: 1068
			// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00039061 File Offset: 0x00037261
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x1700042D RID: 1069
			// (set) Token: 0x060019C3 RID: 6595 RVA: 0x00039079 File Offset: 0x00037279
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x1700042E RID: 1070
			// (set) Token: 0x060019C4 RID: 6596 RVA: 0x00039091 File Offset: 0x00037291
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700042F RID: 1071
			// (set) Token: 0x060019C5 RID: 6597 RVA: 0x000390A9 File Offset: 0x000372A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000430 RID: 1072
			// (set) Token: 0x060019C6 RID: 6598 RVA: 0x000390C1 File Offset: 0x000372C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000431 RID: 1073
			// (set) Token: 0x060019C7 RID: 6599 RVA: 0x000390D9 File Offset: 0x000372D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000432 RID: 1074
			// (set) Token: 0x060019C8 RID: 6600 RVA: 0x000390F1 File Offset: 0x000372F1
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
