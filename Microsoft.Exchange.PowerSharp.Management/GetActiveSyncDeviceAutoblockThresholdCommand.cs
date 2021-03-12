using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000031 RID: 49
	public class GetActiveSyncDeviceAutoblockThresholdCommand : SyntheticCommandWithPipelineInput<ActiveSyncDeviceAutoblockThreshold, ActiveSyncDeviceAutoblockThreshold>
	{
		// Token: 0x060015C8 RID: 5576 RVA: 0x0003400B File Offset: 0x0003220B
		private GetActiveSyncDeviceAutoblockThresholdCommand() : base("Get-ActiveSyncDeviceAutoblockThreshold")
		{
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00034018 File Offset: 0x00032218
		public GetActiveSyncDeviceAutoblockThresholdCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00034027 File Offset: 0x00032227
		public virtual GetActiveSyncDeviceAutoblockThresholdCommand SetParameters(GetActiveSyncDeviceAutoblockThresholdCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00034031 File Offset: 0x00032231
		public virtual GetActiveSyncDeviceAutoblockThresholdCommand SetParameters(GetActiveSyncDeviceAutoblockThresholdCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000032 RID: 50
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700011F RID: 287
			// (set) Token: 0x060015CC RID: 5580 RVA: 0x0003403B File Offset: 0x0003223B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000120 RID: 288
			// (set) Token: 0x060015CD RID: 5581 RVA: 0x0003404E File Offset: 0x0003224E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000121 RID: 289
			// (set) Token: 0x060015CE RID: 5582 RVA: 0x00034066 File Offset: 0x00032266
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000122 RID: 290
			// (set) Token: 0x060015CF RID: 5583 RVA: 0x0003407E File Offset: 0x0003227E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000123 RID: 291
			// (set) Token: 0x060015D0 RID: 5584 RVA: 0x00034096 File Offset: 0x00032296
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000033 RID: 51
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000124 RID: 292
			// (set) Token: 0x060015D2 RID: 5586 RVA: 0x000340B6 File Offset: 0x000322B6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceAutoblockThresholdIdParameter(value) : null);
				}
			}

			// Token: 0x17000125 RID: 293
			// (set) Token: 0x060015D3 RID: 5587 RVA: 0x000340D4 File Offset: 0x000322D4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000126 RID: 294
			// (set) Token: 0x060015D4 RID: 5588 RVA: 0x000340E7 File Offset: 0x000322E7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000127 RID: 295
			// (set) Token: 0x060015D5 RID: 5589 RVA: 0x000340FF File Offset: 0x000322FF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000128 RID: 296
			// (set) Token: 0x060015D6 RID: 5590 RVA: 0x00034117 File Offset: 0x00032317
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000129 RID: 297
			// (set) Token: 0x060015D7 RID: 5591 RVA: 0x0003412F File Offset: 0x0003232F
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
