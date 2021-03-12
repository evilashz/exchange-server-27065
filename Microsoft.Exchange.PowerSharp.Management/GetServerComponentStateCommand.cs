using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000595 RID: 1429
	public class GetServerComponentStateCommand : SyntheticCommandWithPipelineInputNoOutput<ServerIdParameter>
	{
		// Token: 0x06004ABC RID: 19132 RVA: 0x00078460 File Offset: 0x00076660
		private GetServerComponentStateCommand() : base("Get-ServerComponentState")
		{
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0007846D File Offset: 0x0007666D
		public GetServerComponentStateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0007847C File Offset: 0x0007667C
		public virtual GetServerComponentStateCommand SetParameters(GetServerComponentStateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000596 RID: 1430
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B4B RID: 11083
			// (set) Token: 0x06004ABF RID: 19135 RVA: 0x00078486 File Offset: 0x00076686
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002B4C RID: 11084
			// (set) Token: 0x06004AC0 RID: 19136 RVA: 0x00078499 File Offset: 0x00076699
			public virtual string Component
			{
				set
				{
					base.PowerSharpParameters["Component"] = value;
				}
			}

			// Token: 0x17002B4D RID: 11085
			// (set) Token: 0x06004AC1 RID: 19137 RVA: 0x000784AC File Offset: 0x000766AC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B4E RID: 11086
			// (set) Token: 0x06004AC2 RID: 19138 RVA: 0x000784BF File Offset: 0x000766BF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B4F RID: 11087
			// (set) Token: 0x06004AC3 RID: 19139 RVA: 0x000784D7 File Offset: 0x000766D7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B50 RID: 11088
			// (set) Token: 0x06004AC4 RID: 19140 RVA: 0x000784EF File Offset: 0x000766EF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B51 RID: 11089
			// (set) Token: 0x06004AC5 RID: 19141 RVA: 0x00078507 File Offset: 0x00076707
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
