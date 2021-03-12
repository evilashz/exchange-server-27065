using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200029F RID: 671
	public class AddGlobalMonitoringOverrideCommand : SyntheticCommandWithPipelineInput<MonitoringOverride, MonitoringOverride>
	{
		// Token: 0x06003019 RID: 12313 RVA: 0x000566B8 File Offset: 0x000548B8
		private AddGlobalMonitoringOverrideCommand() : base("Add-GlobalMonitoringOverride")
		{
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000566C5 File Offset: 0x000548C5
		public AddGlobalMonitoringOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000566D4 File Offset: 0x000548D4
		public virtual AddGlobalMonitoringOverrideCommand SetParameters(AddGlobalMonitoringOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000566DE File Offset: 0x000548DE
		public virtual AddGlobalMonitoringOverrideCommand SetParameters(AddGlobalMonitoringOverrideCommand.DurationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000566E8 File Offset: 0x000548E8
		public virtual AddGlobalMonitoringOverrideCommand SetParameters(AddGlobalMonitoringOverrideCommand.ApplyVersionParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002A0 RID: 672
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001694 RID: 5780
			// (set) Token: 0x0600301E RID: 12318 RVA: 0x000566F2 File Offset: 0x000548F2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17001695 RID: 5781
			// (set) Token: 0x0600301F RID: 12319 RVA: 0x00056705 File Offset: 0x00054905
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x17001696 RID: 5782
			// (set) Token: 0x06003020 RID: 12320 RVA: 0x0005671D File Offset: 0x0005491D
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x17001697 RID: 5783
			// (set) Token: 0x06003021 RID: 12321 RVA: 0x00056730 File Offset: 0x00054930
			public virtual string PropertyValue
			{
				set
				{
					base.PowerSharpParameters["PropertyValue"] = value;
				}
			}

			// Token: 0x17001698 RID: 5784
			// (set) Token: 0x06003022 RID: 12322 RVA: 0x00056743 File Offset: 0x00054943
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001699 RID: 5785
			// (set) Token: 0x06003023 RID: 12323 RVA: 0x00056756 File Offset: 0x00054956
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700169A RID: 5786
			// (set) Token: 0x06003024 RID: 12324 RVA: 0x0005676E File Offset: 0x0005496E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700169B RID: 5787
			// (set) Token: 0x06003025 RID: 12325 RVA: 0x00056786 File Offset: 0x00054986
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700169C RID: 5788
			// (set) Token: 0x06003026 RID: 12326 RVA: 0x0005679E File Offset: 0x0005499E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700169D RID: 5789
			// (set) Token: 0x06003027 RID: 12327 RVA: 0x000567B6 File Offset: 0x000549B6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002A1 RID: 673
		public class DurationParameters : ParametersBase
		{
			// Token: 0x1700169E RID: 5790
			// (set) Token: 0x06003029 RID: 12329 RVA: 0x000567D6 File Offset: 0x000549D6
			public virtual EnhancedTimeSpan? Duration
			{
				set
				{
					base.PowerSharpParameters["Duration"] = value;
				}
			}

			// Token: 0x1700169F RID: 5791
			// (set) Token: 0x0600302A RID: 12330 RVA: 0x000567EE File Offset: 0x000549EE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170016A0 RID: 5792
			// (set) Token: 0x0600302B RID: 12331 RVA: 0x00056801 File Offset: 0x00054A01
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x170016A1 RID: 5793
			// (set) Token: 0x0600302C RID: 12332 RVA: 0x00056819 File Offset: 0x00054A19
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x170016A2 RID: 5794
			// (set) Token: 0x0600302D RID: 12333 RVA: 0x0005682C File Offset: 0x00054A2C
			public virtual string PropertyValue
			{
				set
				{
					base.PowerSharpParameters["PropertyValue"] = value;
				}
			}

			// Token: 0x170016A3 RID: 5795
			// (set) Token: 0x0600302E RID: 12334 RVA: 0x0005683F File Offset: 0x00054A3F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170016A4 RID: 5796
			// (set) Token: 0x0600302F RID: 12335 RVA: 0x00056852 File Offset: 0x00054A52
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016A5 RID: 5797
			// (set) Token: 0x06003030 RID: 12336 RVA: 0x0005686A File Offset: 0x00054A6A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016A6 RID: 5798
			// (set) Token: 0x06003031 RID: 12337 RVA: 0x00056882 File Offset: 0x00054A82
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016A7 RID: 5799
			// (set) Token: 0x06003032 RID: 12338 RVA: 0x0005689A File Offset: 0x00054A9A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170016A8 RID: 5800
			// (set) Token: 0x06003033 RID: 12339 RVA: 0x000568B2 File Offset: 0x00054AB2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002A2 RID: 674
		public class ApplyVersionParameters : ParametersBase
		{
			// Token: 0x170016A9 RID: 5801
			// (set) Token: 0x06003035 RID: 12341 RVA: 0x000568D2 File Offset: 0x00054AD2
			public virtual Version ApplyVersion
			{
				set
				{
					base.PowerSharpParameters["ApplyVersion"] = value;
				}
			}

			// Token: 0x170016AA RID: 5802
			// (set) Token: 0x06003036 RID: 12342 RVA: 0x000568E5 File Offset: 0x00054AE5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170016AB RID: 5803
			// (set) Token: 0x06003037 RID: 12343 RVA: 0x000568F8 File Offset: 0x00054AF8
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x170016AC RID: 5804
			// (set) Token: 0x06003038 RID: 12344 RVA: 0x00056910 File Offset: 0x00054B10
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x170016AD RID: 5805
			// (set) Token: 0x06003039 RID: 12345 RVA: 0x00056923 File Offset: 0x00054B23
			public virtual string PropertyValue
			{
				set
				{
					base.PowerSharpParameters["PropertyValue"] = value;
				}
			}

			// Token: 0x170016AE RID: 5806
			// (set) Token: 0x0600303A RID: 12346 RVA: 0x00056936 File Offset: 0x00054B36
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170016AF RID: 5807
			// (set) Token: 0x0600303B RID: 12347 RVA: 0x00056949 File Offset: 0x00054B49
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016B0 RID: 5808
			// (set) Token: 0x0600303C RID: 12348 RVA: 0x00056961 File Offset: 0x00054B61
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016B1 RID: 5809
			// (set) Token: 0x0600303D RID: 12349 RVA: 0x00056979 File Offset: 0x00054B79
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016B2 RID: 5810
			// (set) Token: 0x0600303E RID: 12350 RVA: 0x00056991 File Offset: 0x00054B91
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170016B3 RID: 5811
			// (set) Token: 0x0600303F RID: 12351 RVA: 0x000569A9 File Offset: 0x00054BA9
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
