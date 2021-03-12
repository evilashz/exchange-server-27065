using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000AD RID: 173
	public class GetOrganizationCommand : SyntheticCommandWithPipelineInput<ExchangeConfigurationUnit, ExchangeConfigurationUnit>
	{
		// Token: 0x06001A21 RID: 6689 RVA: 0x000397F4 File Offset: 0x000379F4
		private GetOrganizationCommand() : base("Get-Organization")
		{
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00039801 File Offset: 0x00037A01
		public GetOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00039810 File Offset: 0x00037A10
		public virtual GetOrganizationCommand SetParameters(GetOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0003981A File Offset: 0x00037A1A
		public virtual GetOrganizationCommand SetParameters(GetOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000AE RID: 174
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000480 RID: 1152
			// (set) Token: 0x06001A25 RID: 6693 RVA: 0x00039824 File Offset: 0x00037A24
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000481 RID: 1153
			// (set) Token: 0x06001A26 RID: 6694 RVA: 0x00039842 File Offset: 0x00037A42
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17000482 RID: 1154
			// (set) Token: 0x06001A27 RID: 6695 RVA: 0x0003985A File Offset: 0x00037A5A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17000483 RID: 1155
			// (set) Token: 0x06001A28 RID: 6696 RVA: 0x0003986D File Offset: 0x00037A6D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000484 RID: 1156
			// (set) Token: 0x06001A29 RID: 6697 RVA: 0x00039885 File Offset: 0x00037A85
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17000485 RID: 1157
			// (set) Token: 0x06001A2A RID: 6698 RVA: 0x00039898 File Offset: 0x00037A98
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000486 RID: 1158
			// (set) Token: 0x06001A2B RID: 6699 RVA: 0x000398AB File Offset: 0x00037AAB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000487 RID: 1159
			// (set) Token: 0x06001A2C RID: 6700 RVA: 0x000398C3 File Offset: 0x00037AC3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000488 RID: 1160
			// (set) Token: 0x06001A2D RID: 6701 RVA: 0x000398DB File Offset: 0x00037ADB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000489 RID: 1161
			// (set) Token: 0x06001A2E RID: 6702 RVA: 0x000398F3 File Offset: 0x00037AF3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020000AF RID: 175
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700048A RID: 1162
			// (set) Token: 0x06001A30 RID: 6704 RVA: 0x00039913 File Offset: 0x00037B13
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x1700048B RID: 1163
			// (set) Token: 0x06001A31 RID: 6705 RVA: 0x0003992B File Offset: 0x00037B2B
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700048C RID: 1164
			// (set) Token: 0x06001A32 RID: 6706 RVA: 0x0003993E File Offset: 0x00037B3E
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700048D RID: 1165
			// (set) Token: 0x06001A33 RID: 6707 RVA: 0x00039956 File Offset: 0x00037B56
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700048E RID: 1166
			// (set) Token: 0x06001A34 RID: 6708 RVA: 0x00039969 File Offset: 0x00037B69
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700048F RID: 1167
			// (set) Token: 0x06001A35 RID: 6709 RVA: 0x0003997C File Offset: 0x00037B7C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000490 RID: 1168
			// (set) Token: 0x06001A36 RID: 6710 RVA: 0x00039994 File Offset: 0x00037B94
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000491 RID: 1169
			// (set) Token: 0x06001A37 RID: 6711 RVA: 0x000399AC File Offset: 0x00037BAC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000492 RID: 1170
			// (set) Token: 0x06001A38 RID: 6712 RVA: 0x000399C4 File Offset: 0x00037BC4
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
