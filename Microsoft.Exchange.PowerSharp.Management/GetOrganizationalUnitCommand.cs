using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009AB RID: 2475
	public class GetOrganizationalUnitCommand : SyntheticCommandWithPipelineInput<ExtendedOrganizationalUnit, ExtendedOrganizationalUnit>
	{
		// Token: 0x06007C77 RID: 31863 RVA: 0x000B950A File Offset: 0x000B770A
		private GetOrganizationalUnitCommand() : base("Get-OrganizationalUnit")
		{
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x000B9517 File Offset: 0x000B7717
		public GetOrganizationalUnitCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x000B9526 File Offset: 0x000B7726
		public virtual GetOrganizationalUnitCommand SetParameters(GetOrganizationalUnitCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007C7A RID: 31866 RVA: 0x000B9530 File Offset: 0x000B7730
		public virtual GetOrganizationalUnitCommand SetParameters(GetOrganizationalUnitCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007C7B RID: 31867 RVA: 0x000B953A File Offset: 0x000B773A
		public virtual GetOrganizationalUnitCommand SetParameters(GetOrganizationalUnitCommand.SearchSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009AC RID: 2476
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170054DA RID: 21722
			// (set) Token: 0x06007C7C RID: 31868 RVA: 0x000B9544 File Offset: 0x000B7744
			public virtual SwitchParameter SingleNodeOnly
			{
				set
				{
					base.PowerSharpParameters["SingleNodeOnly"] = value;
				}
			}

			// Token: 0x170054DB RID: 21723
			// (set) Token: 0x06007C7D RID: 31869 RVA: 0x000B955C File Offset: 0x000B775C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExtendedOrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170054DC RID: 21724
			// (set) Token: 0x06007C7E RID: 31870 RVA: 0x000B957A File Offset: 0x000B777A
			public virtual SwitchParameter IncludeContainers
			{
				set
				{
					base.PowerSharpParameters["IncludeContainers"] = value;
				}
			}

			// Token: 0x170054DD RID: 21725
			// (set) Token: 0x06007C7F RID: 31871 RVA: 0x000B9592 File Offset: 0x000B7792
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170054DE RID: 21726
			// (set) Token: 0x06007C80 RID: 31872 RVA: 0x000B95AA File Offset: 0x000B77AA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170054DF RID: 21727
			// (set) Token: 0x06007C81 RID: 31873 RVA: 0x000B95C8 File Offset: 0x000B77C8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054E0 RID: 21728
			// (set) Token: 0x06007C82 RID: 31874 RVA: 0x000B95DB File Offset: 0x000B77DB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054E1 RID: 21729
			// (set) Token: 0x06007C83 RID: 31875 RVA: 0x000B95F3 File Offset: 0x000B77F3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054E2 RID: 21730
			// (set) Token: 0x06007C84 RID: 31876 RVA: 0x000B960B File Offset: 0x000B780B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054E3 RID: 21731
			// (set) Token: 0x06007C85 RID: 31877 RVA: 0x000B9623 File Offset: 0x000B7823
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009AD RID: 2477
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170054E4 RID: 21732
			// (set) Token: 0x06007C87 RID: 31879 RVA: 0x000B9643 File Offset: 0x000B7843
			public virtual SwitchParameter IncludeContainers
			{
				set
				{
					base.PowerSharpParameters["IncludeContainers"] = value;
				}
			}

			// Token: 0x170054E5 RID: 21733
			// (set) Token: 0x06007C88 RID: 31880 RVA: 0x000B965B File Offset: 0x000B785B
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170054E6 RID: 21734
			// (set) Token: 0x06007C89 RID: 31881 RVA: 0x000B9673 File Offset: 0x000B7873
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170054E7 RID: 21735
			// (set) Token: 0x06007C8A RID: 31882 RVA: 0x000B9691 File Offset: 0x000B7891
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054E8 RID: 21736
			// (set) Token: 0x06007C8B RID: 31883 RVA: 0x000B96A4 File Offset: 0x000B78A4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054E9 RID: 21737
			// (set) Token: 0x06007C8C RID: 31884 RVA: 0x000B96BC File Offset: 0x000B78BC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054EA RID: 21738
			// (set) Token: 0x06007C8D RID: 31885 RVA: 0x000B96D4 File Offset: 0x000B78D4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054EB RID: 21739
			// (set) Token: 0x06007C8E RID: 31886 RVA: 0x000B96EC File Offset: 0x000B78EC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009AE RID: 2478
		public class SearchSetParameters : ParametersBase
		{
			// Token: 0x170054EC RID: 21740
			// (set) Token: 0x06007C90 RID: 31888 RVA: 0x000B970C File Offset: 0x000B790C
			public virtual string SearchText
			{
				set
				{
					base.PowerSharpParameters["SearchText"] = value;
				}
			}

			// Token: 0x170054ED RID: 21741
			// (set) Token: 0x06007C91 RID: 31889 RVA: 0x000B971F File Offset: 0x000B791F
			public virtual SwitchParameter IncludeContainers
			{
				set
				{
					base.PowerSharpParameters["IncludeContainers"] = value;
				}
			}

			// Token: 0x170054EE RID: 21742
			// (set) Token: 0x06007C92 RID: 31890 RVA: 0x000B9737 File Offset: 0x000B7937
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170054EF RID: 21743
			// (set) Token: 0x06007C93 RID: 31891 RVA: 0x000B974F File Offset: 0x000B794F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170054F0 RID: 21744
			// (set) Token: 0x06007C94 RID: 31892 RVA: 0x000B976D File Offset: 0x000B796D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170054F1 RID: 21745
			// (set) Token: 0x06007C95 RID: 31893 RVA: 0x000B9780 File Offset: 0x000B7980
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170054F2 RID: 21746
			// (set) Token: 0x06007C96 RID: 31894 RVA: 0x000B9798 File Offset: 0x000B7998
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170054F3 RID: 21747
			// (set) Token: 0x06007C97 RID: 31895 RVA: 0x000B97B0 File Offset: 0x000B79B0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170054F4 RID: 21748
			// (set) Token: 0x06007C98 RID: 31896 RVA: 0x000B97C8 File Offset: 0x000B79C8
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
