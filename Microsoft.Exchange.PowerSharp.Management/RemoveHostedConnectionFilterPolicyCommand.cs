using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000709 RID: 1801
	public class RemoveHostedConnectionFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedConnectionFilterPolicy, HostedConnectionFilterPolicy>
	{
		// Token: 0x06005CFF RID: 23807 RVA: 0x00090491 File Offset: 0x0008E691
		private RemoveHostedConnectionFilterPolicyCommand() : base("Remove-HostedConnectionFilterPolicy")
		{
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x0009049E File Offset: 0x0008E69E
		public RemoveHostedConnectionFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x000904AD File Offset: 0x0008E6AD
		public virtual RemoveHostedConnectionFilterPolicyCommand SetParameters(RemoveHostedConnectionFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x000904B7 File Offset: 0x0008E6B7
		public virtual RemoveHostedConnectionFilterPolicyCommand SetParameters(RemoveHostedConnectionFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200070A RID: 1802
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003AA6 RID: 15014
			// (set) Token: 0x06005D03 RID: 23811 RVA: 0x000904C1 File Offset: 0x0008E6C1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003AA7 RID: 15015
			// (set) Token: 0x06005D04 RID: 23812 RVA: 0x000904D9 File Offset: 0x0008E6D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003AA8 RID: 15016
			// (set) Token: 0x06005D05 RID: 23813 RVA: 0x000904EC File Offset: 0x0008E6EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003AA9 RID: 15017
			// (set) Token: 0x06005D06 RID: 23814 RVA: 0x00090504 File Offset: 0x0008E704
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003AAA RID: 15018
			// (set) Token: 0x06005D07 RID: 23815 RVA: 0x0009051C File Offset: 0x0008E71C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003AAB RID: 15019
			// (set) Token: 0x06005D08 RID: 23816 RVA: 0x00090534 File Offset: 0x0008E734
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003AAC RID: 15020
			// (set) Token: 0x06005D09 RID: 23817 RVA: 0x0009054C File Offset: 0x0008E74C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003AAD RID: 15021
			// (set) Token: 0x06005D0A RID: 23818 RVA: 0x00090564 File Offset: 0x0008E764
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200070B RID: 1803
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003AAE RID: 15022
			// (set) Token: 0x06005D0C RID: 23820 RVA: 0x00090584 File Offset: 0x0008E784
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedConnectionFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003AAF RID: 15023
			// (set) Token: 0x06005D0D RID: 23821 RVA: 0x000905A2 File Offset: 0x0008E7A2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003AB0 RID: 15024
			// (set) Token: 0x06005D0E RID: 23822 RVA: 0x000905BA File Offset: 0x0008E7BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003AB1 RID: 15025
			// (set) Token: 0x06005D0F RID: 23823 RVA: 0x000905CD File Offset: 0x0008E7CD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003AB2 RID: 15026
			// (set) Token: 0x06005D10 RID: 23824 RVA: 0x000905E5 File Offset: 0x0008E7E5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003AB3 RID: 15027
			// (set) Token: 0x06005D11 RID: 23825 RVA: 0x000905FD File Offset: 0x0008E7FD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003AB4 RID: 15028
			// (set) Token: 0x06005D12 RID: 23826 RVA: 0x00090615 File Offset: 0x0008E815
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003AB5 RID: 15029
			// (set) Token: 0x06005D13 RID: 23827 RVA: 0x0009062D File Offset: 0x0008E82D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003AB6 RID: 15030
			// (set) Token: 0x06005D14 RID: 23828 RVA: 0x00090645 File Offset: 0x0008E845
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
