using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000556 RID: 1366
	public class NewStampGroupCommand : SyntheticCommandWithPipelineInput<StampGroup, StampGroup>
	{
		// Token: 0x06004876 RID: 18550 RVA: 0x0007569B File Offset: 0x0007389B
		private NewStampGroupCommand() : base("New-StampGroup")
		{
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x000756A8 File Offset: 0x000738A8
		public NewStampGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x000756B7 File Offset: 0x000738B7
		public virtual NewStampGroupCommand SetParameters(NewStampGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000557 RID: 1367
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002983 RID: 10627
			// (set) Token: 0x06004879 RID: 18553 RVA: 0x000756C1 File Offset: 0x000738C1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002984 RID: 10628
			// (set) Token: 0x0600487A RID: 18554 RVA: 0x000756D4 File Offset: 0x000738D4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002985 RID: 10629
			// (set) Token: 0x0600487B RID: 18555 RVA: 0x000756E7 File Offset: 0x000738E7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002986 RID: 10630
			// (set) Token: 0x0600487C RID: 18556 RVA: 0x000756FF File Offset: 0x000738FF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002987 RID: 10631
			// (set) Token: 0x0600487D RID: 18557 RVA: 0x00075717 File Offset: 0x00073917
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002988 RID: 10632
			// (set) Token: 0x0600487E RID: 18558 RVA: 0x0007572F File Offset: 0x0007392F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002989 RID: 10633
			// (set) Token: 0x0600487F RID: 18559 RVA: 0x00075747 File Offset: 0x00073947
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
