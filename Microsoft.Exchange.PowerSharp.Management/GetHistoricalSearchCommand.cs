using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000173 RID: 371
	public class GetHistoricalSearchCommand : SyntheticCommandWithPipelineInput<HistoricalSearch, HistoricalSearch>
	{
		// Token: 0x06002285 RID: 8837 RVA: 0x00044532 File Offset: 0x00042732
		private GetHistoricalSearchCommand() : base("Get-HistoricalSearch")
		{
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0004453F File Offset: 0x0004273F
		public GetHistoricalSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x0004454E File Offset: 0x0004274E
		public virtual GetHistoricalSearchCommand SetParameters(GetHistoricalSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000174 RID: 372
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B58 RID: 2904
			// (set) Token: 0x06002288 RID: 8840 RVA: 0x00044558 File Offset: 0x00042758
			public virtual Guid? JobId
			{
				set
				{
					base.PowerSharpParameters["JobId"] = value;
				}
			}

			// Token: 0x17000B59 RID: 2905
			// (set) Token: 0x06002289 RID: 8841 RVA: 0x00044570 File Offset: 0x00042770
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B5A RID: 2906
			// (set) Token: 0x0600228A RID: 8842 RVA: 0x0004458E File Offset: 0x0004278E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B5B RID: 2907
			// (set) Token: 0x0600228B RID: 8843 RVA: 0x000445A6 File Offset: 0x000427A6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B5C RID: 2908
			// (set) Token: 0x0600228C RID: 8844 RVA: 0x000445BE File Offset: 0x000427BE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B5D RID: 2909
			// (set) Token: 0x0600228D RID: 8845 RVA: 0x000445D6 File Offset: 0x000427D6
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
