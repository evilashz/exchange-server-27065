using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000554 RID: 1364
	public class NewDatabaseAvailabilityGroupNetworkCommand : SyntheticCommandWithPipelineInput<DatabaseAvailabilityGroupNetwork, DatabaseAvailabilityGroupNetwork>
	{
		// Token: 0x06004866 RID: 18534 RVA: 0x00075566 File Offset: 0x00073766
		private NewDatabaseAvailabilityGroupNetworkCommand() : base("New-DatabaseAvailabilityGroupNetwork")
		{
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x00075573 File Offset: 0x00073773
		public NewDatabaseAvailabilityGroupNetworkCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x00075582 File Offset: 0x00073782
		public virtual NewDatabaseAvailabilityGroupNetworkCommand SetParameters(NewDatabaseAvailabilityGroupNetworkCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000555 RID: 1365
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002977 RID: 10615
			// (set) Token: 0x06004869 RID: 18537 RVA: 0x0007558C File Offset: 0x0007378C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002978 RID: 10616
			// (set) Token: 0x0600486A RID: 18538 RVA: 0x0007559F File Offset: 0x0007379F
			public virtual DatabaseAvailabilityGroupIdParameter DatabaseAvailabilityGroup
			{
				set
				{
					base.PowerSharpParameters["DatabaseAvailabilityGroup"] = value;
				}
			}

			// Token: 0x17002979 RID: 10617
			// (set) Token: 0x0600486B RID: 18539 RVA: 0x000755B2 File Offset: 0x000737B2
			public virtual DatabaseAvailabilityGroupSubnetId Subnets
			{
				set
				{
					base.PowerSharpParameters["Subnets"] = value;
				}
			}

			// Token: 0x1700297A RID: 10618
			// (set) Token: 0x0600486C RID: 18540 RVA: 0x000755C5 File Offset: 0x000737C5
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x1700297B RID: 10619
			// (set) Token: 0x0600486D RID: 18541 RVA: 0x000755D8 File Offset: 0x000737D8
			public virtual bool ReplicationEnabled
			{
				set
				{
					base.PowerSharpParameters["ReplicationEnabled"] = value;
				}
			}

			// Token: 0x1700297C RID: 10620
			// (set) Token: 0x0600486E RID: 18542 RVA: 0x000755F0 File Offset: 0x000737F0
			public virtual bool IgnoreNetwork
			{
				set
				{
					base.PowerSharpParameters["IgnoreNetwork"] = value;
				}
			}

			// Token: 0x1700297D RID: 10621
			// (set) Token: 0x0600486F RID: 18543 RVA: 0x00075608 File Offset: 0x00073808
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700297E RID: 10622
			// (set) Token: 0x06004870 RID: 18544 RVA: 0x0007561B File Offset: 0x0007381B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700297F RID: 10623
			// (set) Token: 0x06004871 RID: 18545 RVA: 0x00075633 File Offset: 0x00073833
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002980 RID: 10624
			// (set) Token: 0x06004872 RID: 18546 RVA: 0x0007564B File Offset: 0x0007384B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002981 RID: 10625
			// (set) Token: 0x06004873 RID: 18547 RVA: 0x00075663 File Offset: 0x00073863
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002982 RID: 10626
			// (set) Token: 0x06004874 RID: 18548 RVA: 0x0007567B File Offset: 0x0007387B
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
