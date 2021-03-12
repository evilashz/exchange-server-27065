using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200070C RID: 1804
	public class SetHostedConnectionFilterPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<HostedConnectionFilterPolicy>
	{
		// Token: 0x06005D16 RID: 23830 RVA: 0x00090665 File Offset: 0x0008E865
		private SetHostedConnectionFilterPolicyCommand() : base("Set-HostedConnectionFilterPolicy")
		{
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x00090672 File Offset: 0x0008E872
		public SetHostedConnectionFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x00090681 File Offset: 0x0008E881
		public virtual SetHostedConnectionFilterPolicyCommand SetParameters(SetHostedConnectionFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x0009068B File Offset: 0x0008E88B
		public virtual SetHostedConnectionFilterPolicyCommand SetParameters(SetHostedConnectionFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200070D RID: 1805
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003AB7 RID: 15031
			// (set) Token: 0x06005D1A RID: 23834 RVA: 0x00090695 File Offset: 0x0008E895
			public virtual SwitchParameter MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x17003AB8 RID: 15032
			// (set) Token: 0x06005D1B RID: 23835 RVA: 0x000906AD File Offset: 0x0008E8AD
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003AB9 RID: 15033
			// (set) Token: 0x06005D1C RID: 23836 RVA: 0x000906C5 File Offset: 0x0008E8C5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003ABA RID: 15034
			// (set) Token: 0x06005D1D RID: 23837 RVA: 0x000906D8 File Offset: 0x0008E8D8
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003ABB RID: 15035
			// (set) Token: 0x06005D1E RID: 23838 RVA: 0x000906EB File Offset: 0x0008E8EB
			public virtual MultiValuedProperty<IPRange> IPAllowList
			{
				set
				{
					base.PowerSharpParameters["IPAllowList"] = value;
				}
			}

			// Token: 0x17003ABC RID: 15036
			// (set) Token: 0x06005D1F RID: 23839 RVA: 0x000906FE File Offset: 0x0008E8FE
			public virtual MultiValuedProperty<IPRange> IPBlockList
			{
				set
				{
					base.PowerSharpParameters["IPBlockList"] = value;
				}
			}

			// Token: 0x17003ABD RID: 15037
			// (set) Token: 0x06005D20 RID: 23840 RVA: 0x00090711 File Offset: 0x0008E911
			public virtual bool EnableSafeList
			{
				set
				{
					base.PowerSharpParameters["EnableSafeList"] = value;
				}
			}

			// Token: 0x17003ABE RID: 15038
			// (set) Token: 0x06005D21 RID: 23841 RVA: 0x00090729 File Offset: 0x0008E929
			public virtual DirectoryBasedEdgeBlockMode DirectoryBasedEdgeBlockMode
			{
				set
				{
					base.PowerSharpParameters["DirectoryBasedEdgeBlockMode"] = value;
				}
			}

			// Token: 0x17003ABF RID: 15039
			// (set) Token: 0x06005D22 RID: 23842 RVA: 0x00090741 File Offset: 0x0008E941
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003AC0 RID: 15040
			// (set) Token: 0x06005D23 RID: 23843 RVA: 0x00090754 File Offset: 0x0008E954
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003AC1 RID: 15041
			// (set) Token: 0x06005D24 RID: 23844 RVA: 0x0009076C File Offset: 0x0008E96C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003AC2 RID: 15042
			// (set) Token: 0x06005D25 RID: 23845 RVA: 0x00090784 File Offset: 0x0008E984
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003AC3 RID: 15043
			// (set) Token: 0x06005D26 RID: 23846 RVA: 0x0009079C File Offset: 0x0008E99C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003AC4 RID: 15044
			// (set) Token: 0x06005D27 RID: 23847 RVA: 0x000907B4 File Offset: 0x0008E9B4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200070E RID: 1806
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003AC5 RID: 15045
			// (set) Token: 0x06005D29 RID: 23849 RVA: 0x000907D4 File Offset: 0x0008E9D4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedConnectionFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003AC6 RID: 15046
			// (set) Token: 0x06005D2A RID: 23850 RVA: 0x000907F2 File Offset: 0x0008E9F2
			public virtual SwitchParameter MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x17003AC7 RID: 15047
			// (set) Token: 0x06005D2B RID: 23851 RVA: 0x0009080A File Offset: 0x0008EA0A
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003AC8 RID: 15048
			// (set) Token: 0x06005D2C RID: 23852 RVA: 0x00090822 File Offset: 0x0008EA22
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003AC9 RID: 15049
			// (set) Token: 0x06005D2D RID: 23853 RVA: 0x00090835 File Offset: 0x0008EA35
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003ACA RID: 15050
			// (set) Token: 0x06005D2E RID: 23854 RVA: 0x00090848 File Offset: 0x0008EA48
			public virtual MultiValuedProperty<IPRange> IPAllowList
			{
				set
				{
					base.PowerSharpParameters["IPAllowList"] = value;
				}
			}

			// Token: 0x17003ACB RID: 15051
			// (set) Token: 0x06005D2F RID: 23855 RVA: 0x0009085B File Offset: 0x0008EA5B
			public virtual MultiValuedProperty<IPRange> IPBlockList
			{
				set
				{
					base.PowerSharpParameters["IPBlockList"] = value;
				}
			}

			// Token: 0x17003ACC RID: 15052
			// (set) Token: 0x06005D30 RID: 23856 RVA: 0x0009086E File Offset: 0x0008EA6E
			public virtual bool EnableSafeList
			{
				set
				{
					base.PowerSharpParameters["EnableSafeList"] = value;
				}
			}

			// Token: 0x17003ACD RID: 15053
			// (set) Token: 0x06005D31 RID: 23857 RVA: 0x00090886 File Offset: 0x0008EA86
			public virtual DirectoryBasedEdgeBlockMode DirectoryBasedEdgeBlockMode
			{
				set
				{
					base.PowerSharpParameters["DirectoryBasedEdgeBlockMode"] = value;
				}
			}

			// Token: 0x17003ACE RID: 15054
			// (set) Token: 0x06005D32 RID: 23858 RVA: 0x0009089E File Offset: 0x0008EA9E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003ACF RID: 15055
			// (set) Token: 0x06005D33 RID: 23859 RVA: 0x000908B1 File Offset: 0x0008EAB1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003AD0 RID: 15056
			// (set) Token: 0x06005D34 RID: 23860 RVA: 0x000908C9 File Offset: 0x0008EAC9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003AD1 RID: 15057
			// (set) Token: 0x06005D35 RID: 23861 RVA: 0x000908E1 File Offset: 0x0008EAE1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003AD2 RID: 15058
			// (set) Token: 0x06005D36 RID: 23862 RVA: 0x000908F9 File Offset: 0x0008EAF9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003AD3 RID: 15059
			// (set) Token: 0x06005D37 RID: 23863 RVA: 0x00090911 File Offset: 0x0008EB11
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
