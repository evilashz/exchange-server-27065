using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002A3 RID: 675
	public class AddServerMonitoringOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06003041 RID: 12353 RVA: 0x000569C9 File Offset: 0x00054BC9
		private AddServerMonitoringOverrideCommand() : base("Add-ServerMonitoringOverride")
		{
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000569D6 File Offset: 0x00054BD6
		public AddServerMonitoringOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000569E5 File Offset: 0x00054BE5
		public virtual AddServerMonitoringOverrideCommand SetParameters(AddServerMonitoringOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000569EF File Offset: 0x00054BEF
		public virtual AddServerMonitoringOverrideCommand SetParameters(AddServerMonitoringOverrideCommand.DurationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000569F9 File Offset: 0x00054BF9
		public virtual AddServerMonitoringOverrideCommand SetParameters(AddServerMonitoringOverrideCommand.ApplyVersionParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002A4 RID: 676
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170016B4 RID: 5812
			// (set) Token: 0x06003046 RID: 12358 RVA: 0x00056A03 File Offset: 0x00054C03
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170016B5 RID: 5813
			// (set) Token: 0x06003047 RID: 12359 RVA: 0x00056A16 File Offset: 0x00054C16
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x170016B6 RID: 5814
			// (set) Token: 0x06003048 RID: 12360 RVA: 0x00056A2E File Offset: 0x00054C2E
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x170016B7 RID: 5815
			// (set) Token: 0x06003049 RID: 12361 RVA: 0x00056A41 File Offset: 0x00054C41
			public virtual string PropertyValue
			{
				set
				{
					base.PowerSharpParameters["PropertyValue"] = value;
				}
			}

			// Token: 0x170016B8 RID: 5816
			// (set) Token: 0x0600304A RID: 12362 RVA: 0x00056A54 File Offset: 0x00054C54
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170016B9 RID: 5817
			// (set) Token: 0x0600304B RID: 12363 RVA: 0x00056A67 File Offset: 0x00054C67
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016BA RID: 5818
			// (set) Token: 0x0600304C RID: 12364 RVA: 0x00056A7F File Offset: 0x00054C7F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016BB RID: 5819
			// (set) Token: 0x0600304D RID: 12365 RVA: 0x00056A97 File Offset: 0x00054C97
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016BC RID: 5820
			// (set) Token: 0x0600304E RID: 12366 RVA: 0x00056AAF File Offset: 0x00054CAF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170016BD RID: 5821
			// (set) Token: 0x0600304F RID: 12367 RVA: 0x00056AC7 File Offset: 0x00054CC7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002A5 RID: 677
		public class DurationParameters : ParametersBase
		{
			// Token: 0x170016BE RID: 5822
			// (set) Token: 0x06003051 RID: 12369 RVA: 0x00056AE7 File Offset: 0x00054CE7
			public virtual EnhancedTimeSpan? Duration
			{
				set
				{
					base.PowerSharpParameters["Duration"] = value;
				}
			}

			// Token: 0x170016BF RID: 5823
			// (set) Token: 0x06003052 RID: 12370 RVA: 0x00056AFF File Offset: 0x00054CFF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170016C0 RID: 5824
			// (set) Token: 0x06003053 RID: 12371 RVA: 0x00056B12 File Offset: 0x00054D12
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x170016C1 RID: 5825
			// (set) Token: 0x06003054 RID: 12372 RVA: 0x00056B2A File Offset: 0x00054D2A
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x170016C2 RID: 5826
			// (set) Token: 0x06003055 RID: 12373 RVA: 0x00056B3D File Offset: 0x00054D3D
			public virtual string PropertyValue
			{
				set
				{
					base.PowerSharpParameters["PropertyValue"] = value;
				}
			}

			// Token: 0x170016C3 RID: 5827
			// (set) Token: 0x06003056 RID: 12374 RVA: 0x00056B50 File Offset: 0x00054D50
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170016C4 RID: 5828
			// (set) Token: 0x06003057 RID: 12375 RVA: 0x00056B63 File Offset: 0x00054D63
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016C5 RID: 5829
			// (set) Token: 0x06003058 RID: 12376 RVA: 0x00056B7B File Offset: 0x00054D7B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016C6 RID: 5830
			// (set) Token: 0x06003059 RID: 12377 RVA: 0x00056B93 File Offset: 0x00054D93
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016C7 RID: 5831
			// (set) Token: 0x0600305A RID: 12378 RVA: 0x00056BAB File Offset: 0x00054DAB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170016C8 RID: 5832
			// (set) Token: 0x0600305B RID: 12379 RVA: 0x00056BC3 File Offset: 0x00054DC3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020002A6 RID: 678
		public class ApplyVersionParameters : ParametersBase
		{
			// Token: 0x170016C9 RID: 5833
			// (set) Token: 0x0600305D RID: 12381 RVA: 0x00056BE3 File Offset: 0x00054DE3
			public virtual Version ApplyVersion
			{
				set
				{
					base.PowerSharpParameters["ApplyVersion"] = value;
				}
			}

			// Token: 0x170016CA RID: 5834
			// (set) Token: 0x0600305E RID: 12382 RVA: 0x00056BF6 File Offset: 0x00054DF6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170016CB RID: 5835
			// (set) Token: 0x0600305F RID: 12383 RVA: 0x00056C09 File Offset: 0x00054E09
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x170016CC RID: 5836
			// (set) Token: 0x06003060 RID: 12384 RVA: 0x00056C21 File Offset: 0x00054E21
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x170016CD RID: 5837
			// (set) Token: 0x06003061 RID: 12385 RVA: 0x00056C34 File Offset: 0x00054E34
			public virtual string PropertyValue
			{
				set
				{
					base.PowerSharpParameters["PropertyValue"] = value;
				}
			}

			// Token: 0x170016CE RID: 5838
			// (set) Token: 0x06003062 RID: 12386 RVA: 0x00056C47 File Offset: 0x00054E47
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170016CF RID: 5839
			// (set) Token: 0x06003063 RID: 12387 RVA: 0x00056C5A File Offset: 0x00054E5A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016D0 RID: 5840
			// (set) Token: 0x06003064 RID: 12388 RVA: 0x00056C72 File Offset: 0x00054E72
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016D1 RID: 5841
			// (set) Token: 0x06003065 RID: 12389 RVA: 0x00056C8A File Offset: 0x00054E8A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016D2 RID: 5842
			// (set) Token: 0x06003066 RID: 12390 RVA: 0x00056CA2 File Offset: 0x00054EA2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170016D3 RID: 5843
			// (set) Token: 0x06003067 RID: 12391 RVA: 0x00056CBA File Offset: 0x00054EBA
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
