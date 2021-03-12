using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000615 RID: 1557
	public class GetRpcClientAccessCommand : SyntheticCommandWithPipelineInput<ExchangeRpcClientAccess, ExchangeRpcClientAccess>
	{
		// Token: 0x06004FDE RID: 20446 RVA: 0x0007ED0C File Offset: 0x0007CF0C
		private GetRpcClientAccessCommand() : base("Get-RpcClientAccess")
		{
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0007ED19 File Offset: 0x0007CF19
		public GetRpcClientAccessCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0007ED28 File Offset: 0x0007CF28
		public virtual GetRpcClientAccessCommand SetParameters(GetRpcClientAccessCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000616 RID: 1558
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F6D RID: 12141
			// (set) Token: 0x06004FE1 RID: 20449 RVA: 0x0007ED32 File Offset: 0x0007CF32
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002F6E RID: 12142
			// (set) Token: 0x06004FE2 RID: 20450 RVA: 0x0007ED45 File Offset: 0x0007CF45
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F6F RID: 12143
			// (set) Token: 0x06004FE3 RID: 20451 RVA: 0x0007ED58 File Offset: 0x0007CF58
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F70 RID: 12144
			// (set) Token: 0x06004FE4 RID: 20452 RVA: 0x0007ED70 File Offset: 0x0007CF70
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F71 RID: 12145
			// (set) Token: 0x06004FE5 RID: 20453 RVA: 0x0007ED88 File Offset: 0x0007CF88
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F72 RID: 12146
			// (set) Token: 0x06004FE6 RID: 20454 RVA: 0x0007EDA0 File Offset: 0x0007CFA0
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
