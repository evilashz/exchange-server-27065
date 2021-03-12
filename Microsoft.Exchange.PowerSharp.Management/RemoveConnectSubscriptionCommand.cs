using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E76 RID: 3702
	public class RemoveConnectSubscriptionCommand : SyntheticCommandWithPipelineInput<ConnectSubscriptionProxy, ConnectSubscriptionProxy>
	{
		// Token: 0x0600DA84 RID: 55940 RVA: 0x00136071 File Offset: 0x00134271
		private RemoveConnectSubscriptionCommand() : base("Remove-ConnectSubscription")
		{
		}

		// Token: 0x0600DA85 RID: 55941 RVA: 0x0013607E File Offset: 0x0013427E
		public RemoveConnectSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DA86 RID: 55942 RVA: 0x0013608D File Offset: 0x0013428D
		public virtual RemoveConnectSubscriptionCommand SetParameters(RemoveConnectSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DA87 RID: 55943 RVA: 0x00136097 File Offset: 0x00134297
		public virtual RemoveConnectSubscriptionCommand SetParameters(RemoveConnectSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E77 RID: 3703
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A951 RID: 43345
			// (set) Token: 0x0600DA88 RID: 55944 RVA: 0x001360A1 File Offset: 0x001342A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A952 RID: 43346
			// (set) Token: 0x0600DA89 RID: 55945 RVA: 0x001360B4 File Offset: 0x001342B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A953 RID: 43347
			// (set) Token: 0x0600DA8A RID: 55946 RVA: 0x001360CC File Offset: 0x001342CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A954 RID: 43348
			// (set) Token: 0x0600DA8B RID: 55947 RVA: 0x001360E4 File Offset: 0x001342E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A955 RID: 43349
			// (set) Token: 0x0600DA8C RID: 55948 RVA: 0x001360FC File Offset: 0x001342FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A956 RID: 43350
			// (set) Token: 0x0600DA8D RID: 55949 RVA: 0x00136114 File Offset: 0x00134314
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A957 RID: 43351
			// (set) Token: 0x0600DA8E RID: 55950 RVA: 0x0013612C File Offset: 0x0013432C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E78 RID: 3704
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A958 RID: 43352
			// (set) Token: 0x0600DA90 RID: 55952 RVA: 0x0013614C File Offset: 0x0013434C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A959 RID: 43353
			// (set) Token: 0x0600DA91 RID: 55953 RVA: 0x0013616A File Offset: 0x0013436A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A95A RID: 43354
			// (set) Token: 0x0600DA92 RID: 55954 RVA: 0x0013617D File Offset: 0x0013437D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A95B RID: 43355
			// (set) Token: 0x0600DA93 RID: 55955 RVA: 0x00136195 File Offset: 0x00134395
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A95C RID: 43356
			// (set) Token: 0x0600DA94 RID: 55956 RVA: 0x001361AD File Offset: 0x001343AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A95D RID: 43357
			// (set) Token: 0x0600DA95 RID: 55957 RVA: 0x001361C5 File Offset: 0x001343C5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A95E RID: 43358
			// (set) Token: 0x0600DA96 RID: 55958 RVA: 0x001361DD File Offset: 0x001343DD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A95F RID: 43359
			// (set) Token: 0x0600DA97 RID: 55959 RVA: 0x001361F5 File Offset: 0x001343F5
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
