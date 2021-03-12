using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002BF RID: 703
	public class GetServerMonitoringOverrideCommand : SyntheticCommandWithPipelineInputNoOutput<ServerIdParameter>
	{
		// Token: 0x06003126 RID: 12582 RVA: 0x00057B93 File Offset: 0x00055D93
		private GetServerMonitoringOverrideCommand() : base("Get-ServerMonitoringOverride")
		{
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x00057BA0 File Offset: 0x00055DA0
		public GetServerMonitoringOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x00057BAF File Offset: 0x00055DAF
		public virtual GetServerMonitoringOverrideCommand SetParameters(GetServerMonitoringOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002C0 RID: 704
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001761 RID: 5985
			// (set) Token: 0x06003129 RID: 12585 RVA: 0x00057BB9 File Offset: 0x00055DB9
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17001762 RID: 5986
			// (set) Token: 0x0600312A RID: 12586 RVA: 0x00057BCC File Offset: 0x00055DCC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001763 RID: 5987
			// (set) Token: 0x0600312B RID: 12587 RVA: 0x00057BE4 File Offset: 0x00055DE4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001764 RID: 5988
			// (set) Token: 0x0600312C RID: 12588 RVA: 0x00057BFC File Offset: 0x00055DFC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001765 RID: 5989
			// (set) Token: 0x0600312D RID: 12589 RVA: 0x00057C14 File Offset: 0x00055E14
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
