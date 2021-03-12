using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000597 RID: 1431
	public class SetServerComponentStateCommand : SyntheticCommandWithPipelineInputNoOutput<ServerIdParameter>
	{
		// Token: 0x06004AC7 RID: 19143 RVA: 0x00078527 File Offset: 0x00076727
		private SetServerComponentStateCommand() : base("Set-ServerComponentState")
		{
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00078534 File Offset: 0x00076734
		public SetServerComponentStateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x00078543 File Offset: 0x00076743
		public virtual SetServerComponentStateCommand SetParameters(SetServerComponentStateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000598 RID: 1432
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B52 RID: 11090
			// (set) Token: 0x06004ACA RID: 19146 RVA: 0x0007854D File Offset: 0x0007674D
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002B53 RID: 11091
			// (set) Token: 0x06004ACB RID: 19147 RVA: 0x00078560 File Offset: 0x00076760
			public virtual string Component
			{
				set
				{
					base.PowerSharpParameters["Component"] = value;
				}
			}

			// Token: 0x17002B54 RID: 11092
			// (set) Token: 0x06004ACC RID: 19148 RVA: 0x00078573 File Offset: 0x00076773
			public virtual ServiceState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17002B55 RID: 11093
			// (set) Token: 0x06004ACD RID: 19149 RVA: 0x0007858B File Offset: 0x0007678B
			public virtual string Requester
			{
				set
				{
					base.PowerSharpParameters["Requester"] = value;
				}
			}

			// Token: 0x17002B56 RID: 11094
			// (set) Token: 0x06004ACE RID: 19150 RVA: 0x0007859E File Offset: 0x0007679E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B57 RID: 11095
			// (set) Token: 0x06004ACF RID: 19151 RVA: 0x000785B1 File Offset: 0x000767B1
			public virtual SwitchParameter RemoteOnly
			{
				set
				{
					base.PowerSharpParameters["RemoteOnly"] = value;
				}
			}

			// Token: 0x17002B58 RID: 11096
			// (set) Token: 0x06004AD0 RID: 19152 RVA: 0x000785C9 File Offset: 0x000767C9
			public virtual SwitchParameter LocalOnly
			{
				set
				{
					base.PowerSharpParameters["LocalOnly"] = value;
				}
			}

			// Token: 0x17002B59 RID: 11097
			// (set) Token: 0x06004AD1 RID: 19153 RVA: 0x000785E1 File Offset: 0x000767E1
			public virtual int TimeoutInSeconds
			{
				set
				{
					base.PowerSharpParameters["TimeoutInSeconds"] = value;
				}
			}

			// Token: 0x17002B5A RID: 11098
			// (set) Token: 0x06004AD2 RID: 19154 RVA: 0x000785F9 File Offset: 0x000767F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B5B RID: 11099
			// (set) Token: 0x06004AD3 RID: 19155 RVA: 0x00078611 File Offset: 0x00076811
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B5C RID: 11100
			// (set) Token: 0x06004AD4 RID: 19156 RVA: 0x00078629 File Offset: 0x00076829
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B5D RID: 11101
			// (set) Token: 0x06004AD5 RID: 19157 RVA: 0x00078641 File Offset: 0x00076841
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B5E RID: 11102
			// (set) Token: 0x06004AD6 RID: 19158 RVA: 0x00078659 File Offset: 0x00076859
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
