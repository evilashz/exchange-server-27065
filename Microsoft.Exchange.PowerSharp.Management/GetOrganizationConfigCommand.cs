using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200080E RID: 2062
	public class GetOrganizationConfigCommand : SyntheticCommandWithPipelineInput<ADOrganizationConfig, ADOrganizationConfig>
	{
		// Token: 0x0600660C RID: 26124 RVA: 0x0009BBBD File Offset: 0x00099DBD
		private GetOrganizationConfigCommand() : base("Get-OrganizationConfig")
		{
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x0009BBCA File Offset: 0x00099DCA
		public GetOrganizationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x0009BBD9 File Offset: 0x00099DD9
		public virtual GetOrganizationConfigCommand SetParameters(GetOrganizationConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x0009BBE3 File Offset: 0x00099DE3
		public virtual GetOrganizationConfigCommand SetParameters(GetOrganizationConfigCommand.PartitionWideParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x0009BBED File Offset: 0x00099DED
		public virtual GetOrganizationConfigCommand SetParameters(GetOrganizationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200080F RID: 2063
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170041A9 RID: 16809
			// (set) Token: 0x06006611 RID: 26129 RVA: 0x0009BBF7 File Offset: 0x00099DF7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170041AA RID: 16810
			// (set) Token: 0x06006612 RID: 26130 RVA: 0x0009BC15 File Offset: 0x00099E15
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041AB RID: 16811
			// (set) Token: 0x06006613 RID: 26131 RVA: 0x0009BC28 File Offset: 0x00099E28
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041AC RID: 16812
			// (set) Token: 0x06006614 RID: 26132 RVA: 0x0009BC40 File Offset: 0x00099E40
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041AD RID: 16813
			// (set) Token: 0x06006615 RID: 26133 RVA: 0x0009BC58 File Offset: 0x00099E58
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041AE RID: 16814
			// (set) Token: 0x06006616 RID: 26134 RVA: 0x0009BC70 File Offset: 0x00099E70
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000810 RID: 2064
		public class PartitionWideParameters : ParametersBase
		{
			// Token: 0x170041AF RID: 16815
			// (set) Token: 0x06006618 RID: 26136 RVA: 0x0009BC90 File Offset: 0x00099E90
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170041B0 RID: 16816
			// (set) Token: 0x06006619 RID: 26137 RVA: 0x0009BCA3 File Offset: 0x00099EA3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041B1 RID: 16817
			// (set) Token: 0x0600661A RID: 26138 RVA: 0x0009BCB6 File Offset: 0x00099EB6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041B2 RID: 16818
			// (set) Token: 0x0600661B RID: 26139 RVA: 0x0009BCCE File Offset: 0x00099ECE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041B3 RID: 16819
			// (set) Token: 0x0600661C RID: 26140 RVA: 0x0009BCE6 File Offset: 0x00099EE6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041B4 RID: 16820
			// (set) Token: 0x0600661D RID: 26141 RVA: 0x0009BCFE File Offset: 0x00099EFE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000811 RID: 2065
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170041B5 RID: 16821
			// (set) Token: 0x0600661F RID: 26143 RVA: 0x0009BD1E File Offset: 0x00099F1E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041B6 RID: 16822
			// (set) Token: 0x06006620 RID: 26144 RVA: 0x0009BD31 File Offset: 0x00099F31
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041B7 RID: 16823
			// (set) Token: 0x06006621 RID: 26145 RVA: 0x0009BD49 File Offset: 0x00099F49
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041B8 RID: 16824
			// (set) Token: 0x06006622 RID: 26146 RVA: 0x0009BD61 File Offset: 0x00099F61
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041B9 RID: 16825
			// (set) Token: 0x06006623 RID: 26147 RVA: 0x0009BD79 File Offset: 0x00099F79
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
