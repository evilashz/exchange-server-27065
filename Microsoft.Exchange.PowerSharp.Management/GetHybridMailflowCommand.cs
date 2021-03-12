using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200051F RID: 1311
	public class GetHybridMailflowCommand : SyntheticCommand<object>
	{
		// Token: 0x060046A2 RID: 18082 RVA: 0x00073279 File Offset: 0x00071479
		private GetHybridMailflowCommand() : base("Get-HybridMailflow")
		{
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x00073286 File Offset: 0x00071486
		public GetHybridMailflowCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00073295 File Offset: 0x00071495
		public virtual GetHybridMailflowCommand SetParameters(GetHybridMailflowCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000520 RID: 1312
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700281D RID: 10269
			// (set) Token: 0x060046A5 RID: 18085 RVA: 0x0007329F File Offset: 0x0007149F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700281E RID: 10270
			// (set) Token: 0x060046A6 RID: 18086 RVA: 0x000732BD File Offset: 0x000714BD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700281F RID: 10271
			// (set) Token: 0x060046A7 RID: 18087 RVA: 0x000732D5 File Offset: 0x000714D5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002820 RID: 10272
			// (set) Token: 0x060046A8 RID: 18088 RVA: 0x000732ED File Offset: 0x000714ED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002821 RID: 10273
			// (set) Token: 0x060046A9 RID: 18089 RVA: 0x00073305 File Offset: 0x00071505
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
