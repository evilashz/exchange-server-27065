using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002CB RID: 715
	public class RemoveServerMonitoringOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06003181 RID: 12673 RVA: 0x00058276 File Offset: 0x00056476
		private RemoveServerMonitoringOverrideCommand() : base("Remove-ServerMonitoringOverride")
		{
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x00058283 File Offset: 0x00056483
		public RemoveServerMonitoringOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00058292 File Offset: 0x00056492
		public virtual RemoveServerMonitoringOverrideCommand SetParameters(RemoveServerMonitoringOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002CC RID: 716
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170017A4 RID: 6052
			// (set) Token: 0x06003184 RID: 12676 RVA: 0x0005829C File Offset: 0x0005649C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170017A5 RID: 6053
			// (set) Token: 0x06003185 RID: 12677 RVA: 0x000582AF File Offset: 0x000564AF
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x170017A6 RID: 6054
			// (set) Token: 0x06003186 RID: 12678 RVA: 0x000582C7 File Offset: 0x000564C7
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x170017A7 RID: 6055
			// (set) Token: 0x06003187 RID: 12679 RVA: 0x000582DA File Offset: 0x000564DA
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170017A8 RID: 6056
			// (set) Token: 0x06003188 RID: 12680 RVA: 0x000582ED File Offset: 0x000564ED
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017A9 RID: 6057
			// (set) Token: 0x06003189 RID: 12681 RVA: 0x00058305 File Offset: 0x00056505
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017AA RID: 6058
			// (set) Token: 0x0600318A RID: 12682 RVA: 0x0005831D File Offset: 0x0005651D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017AB RID: 6059
			// (set) Token: 0x0600318B RID: 12683 RVA: 0x00058335 File Offset: 0x00056535
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017AC RID: 6060
			// (set) Token: 0x0600318C RID: 12684 RVA: 0x0005834D File Offset: 0x0005654D
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
