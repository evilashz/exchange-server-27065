using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000423 RID: 1059
	public class GetSenderReputationConfigCommand : SyntheticCommandWithPipelineInput<SenderReputationConfig, SenderReputationConfig>
	{
		// Token: 0x06003E15 RID: 15893 RVA: 0x0006859C File Offset: 0x0006679C
		private GetSenderReputationConfigCommand() : base("Get-SenderReputationConfig")
		{
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000685A9 File Offset: 0x000667A9
		public GetSenderReputationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x000685B8 File Offset: 0x000667B8
		public virtual GetSenderReputationConfigCommand SetParameters(GetSenderReputationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000424 RID: 1060
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002188 RID: 8584
			// (set) Token: 0x06003E18 RID: 15896 RVA: 0x000685C2 File Offset: 0x000667C2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002189 RID: 8585
			// (set) Token: 0x06003E19 RID: 15897 RVA: 0x000685D5 File Offset: 0x000667D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700218A RID: 8586
			// (set) Token: 0x06003E1A RID: 15898 RVA: 0x000685ED File Offset: 0x000667ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700218B RID: 8587
			// (set) Token: 0x06003E1B RID: 15899 RVA: 0x00068605 File Offset: 0x00066805
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700218C RID: 8588
			// (set) Token: 0x06003E1C RID: 15900 RVA: 0x0006861D File Offset: 0x0006681D
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
