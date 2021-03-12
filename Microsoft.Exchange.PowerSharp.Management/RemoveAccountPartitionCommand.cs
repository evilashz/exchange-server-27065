using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000868 RID: 2152
	public class RemoveAccountPartitionCommand : SyntheticCommandWithPipelineInput<AccountPartition, AccountPartition>
	{
		// Token: 0x06006AB0 RID: 27312 RVA: 0x000A1D4E File Offset: 0x0009FF4E
		private RemoveAccountPartitionCommand() : base("Remove-AccountPartition")
		{
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x000A1D5B File Offset: 0x0009FF5B
		public RemoveAccountPartitionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x000A1D6A File Offset: 0x0009FF6A
		public virtual RemoveAccountPartitionCommand SetParameters(RemoveAccountPartitionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x000A1D74 File Offset: 0x0009FF74
		public virtual RemoveAccountPartitionCommand SetParameters(RemoveAccountPartitionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000869 RID: 2153
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004599 RID: 17817
			// (set) Token: 0x06006AB4 RID: 27316 RVA: 0x000A1D7E File Offset: 0x0009FF7E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700459A RID: 17818
			// (set) Token: 0x06006AB5 RID: 27317 RVA: 0x000A1D96 File Offset: 0x0009FF96
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700459B RID: 17819
			// (set) Token: 0x06006AB6 RID: 27318 RVA: 0x000A1DA9 File Offset: 0x0009FFA9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700459C RID: 17820
			// (set) Token: 0x06006AB7 RID: 27319 RVA: 0x000A1DC1 File Offset: 0x0009FFC1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700459D RID: 17821
			// (set) Token: 0x06006AB8 RID: 27320 RVA: 0x000A1DD9 File Offset: 0x0009FFD9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700459E RID: 17822
			// (set) Token: 0x06006AB9 RID: 27321 RVA: 0x000A1DF1 File Offset: 0x0009FFF1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700459F RID: 17823
			// (set) Token: 0x06006ABA RID: 27322 RVA: 0x000A1E09 File Offset: 0x000A0009
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170045A0 RID: 17824
			// (set) Token: 0x06006ABB RID: 27323 RVA: 0x000A1E21 File Offset: 0x000A0021
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200086A RID: 2154
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170045A1 RID: 17825
			// (set) Token: 0x06006ABD RID: 27325 RVA: 0x000A1E41 File Offset: 0x000A0041
			public virtual AccountPartitionIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170045A2 RID: 17826
			// (set) Token: 0x06006ABE RID: 27326 RVA: 0x000A1E54 File Offset: 0x000A0054
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170045A3 RID: 17827
			// (set) Token: 0x06006ABF RID: 27327 RVA: 0x000A1E6C File Offset: 0x000A006C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045A4 RID: 17828
			// (set) Token: 0x06006AC0 RID: 27328 RVA: 0x000A1E7F File Offset: 0x000A007F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045A5 RID: 17829
			// (set) Token: 0x06006AC1 RID: 27329 RVA: 0x000A1E97 File Offset: 0x000A0097
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045A6 RID: 17830
			// (set) Token: 0x06006AC2 RID: 27330 RVA: 0x000A1EAF File Offset: 0x000A00AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045A7 RID: 17831
			// (set) Token: 0x06006AC3 RID: 27331 RVA: 0x000A1EC7 File Offset: 0x000A00C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170045A8 RID: 17832
			// (set) Token: 0x06006AC4 RID: 27332 RVA: 0x000A1EDF File Offset: 0x000A00DF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170045A9 RID: 17833
			// (set) Token: 0x06006AC5 RID: 27333 RVA: 0x000A1EF7 File Offset: 0x000A00F7
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
