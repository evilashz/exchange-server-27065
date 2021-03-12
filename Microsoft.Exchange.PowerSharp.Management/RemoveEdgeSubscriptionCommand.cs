using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000650 RID: 1616
	public class RemoveEdgeSubscriptionCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x0600514B RID: 20811 RVA: 0x00080813 File Offset: 0x0007EA13
		private RemoveEdgeSubscriptionCommand() : base("Remove-EdgeSubscription")
		{
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x00080820 File Offset: 0x0007EA20
		public RemoveEdgeSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0008082F File Offset: 0x0007EA2F
		public virtual RemoveEdgeSubscriptionCommand SetParameters(RemoveEdgeSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00080839 File Offset: 0x0007EA39
		public virtual RemoveEdgeSubscriptionCommand SetParameters(RemoveEdgeSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000651 RID: 1617
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003064 RID: 12388
			// (set) Token: 0x0600514F RID: 20815 RVA: 0x00080843 File Offset: 0x0007EA43
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003065 RID: 12389
			// (set) Token: 0x06005150 RID: 20816 RVA: 0x0008085B File Offset: 0x0007EA5B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003066 RID: 12390
			// (set) Token: 0x06005151 RID: 20817 RVA: 0x0008086E File Offset: 0x0007EA6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003067 RID: 12391
			// (set) Token: 0x06005152 RID: 20818 RVA: 0x00080886 File Offset: 0x0007EA86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003068 RID: 12392
			// (set) Token: 0x06005153 RID: 20819 RVA: 0x0008089E File Offset: 0x0007EA9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003069 RID: 12393
			// (set) Token: 0x06005154 RID: 20820 RVA: 0x000808B6 File Offset: 0x0007EAB6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700306A RID: 12394
			// (set) Token: 0x06005155 RID: 20821 RVA: 0x000808CE File Offset: 0x0007EACE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700306B RID: 12395
			// (set) Token: 0x06005156 RID: 20822 RVA: 0x000808E6 File Offset: 0x0007EAE6
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000652 RID: 1618
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700306C RID: 12396
			// (set) Token: 0x06005158 RID: 20824 RVA: 0x00080906 File Offset: 0x0007EB06
			public virtual TransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700306D RID: 12397
			// (set) Token: 0x06005159 RID: 20825 RVA: 0x00080919 File Offset: 0x0007EB19
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700306E RID: 12398
			// (set) Token: 0x0600515A RID: 20826 RVA: 0x00080931 File Offset: 0x0007EB31
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700306F RID: 12399
			// (set) Token: 0x0600515B RID: 20827 RVA: 0x00080944 File Offset: 0x0007EB44
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003070 RID: 12400
			// (set) Token: 0x0600515C RID: 20828 RVA: 0x0008095C File Offset: 0x0007EB5C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003071 RID: 12401
			// (set) Token: 0x0600515D RID: 20829 RVA: 0x00080974 File Offset: 0x0007EB74
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003072 RID: 12402
			// (set) Token: 0x0600515E RID: 20830 RVA: 0x0008098C File Offset: 0x0007EB8C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003073 RID: 12403
			// (set) Token: 0x0600515F RID: 20831 RVA: 0x000809A4 File Offset: 0x0007EBA4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003074 RID: 12404
			// (set) Token: 0x06005160 RID: 20832 RVA: 0x000809BC File Offset: 0x0007EBBC
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
