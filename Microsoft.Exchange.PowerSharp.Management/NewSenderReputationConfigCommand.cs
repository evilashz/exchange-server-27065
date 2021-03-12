using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000425 RID: 1061
	public class NewSenderReputationConfigCommand : SyntheticCommandWithPipelineInput<SenderReputationConfig, SenderReputationConfig>
	{
		// Token: 0x06003E1E RID: 15902 RVA: 0x0006863D File Offset: 0x0006683D
		private NewSenderReputationConfigCommand() : base("New-SenderReputationConfig")
		{
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x0006864A File Offset: 0x0006684A
		public NewSenderReputationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00068659 File Offset: 0x00066859
		public virtual NewSenderReputationConfigCommand SetParameters(NewSenderReputationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000426 RID: 1062
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700218D RID: 8589
			// (set) Token: 0x06003E21 RID: 15905 RVA: 0x00068663 File Offset: 0x00066863
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700218E RID: 8590
			// (set) Token: 0x06003E22 RID: 15906 RVA: 0x00068676 File Offset: 0x00066876
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700218F RID: 8591
			// (set) Token: 0x06003E23 RID: 15907 RVA: 0x00068694 File Offset: 0x00066894
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002190 RID: 8592
			// (set) Token: 0x06003E24 RID: 15908 RVA: 0x000686A7 File Offset: 0x000668A7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002191 RID: 8593
			// (set) Token: 0x06003E25 RID: 15909 RVA: 0x000686BF File Offset: 0x000668BF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002192 RID: 8594
			// (set) Token: 0x06003E26 RID: 15910 RVA: 0x000686D7 File Offset: 0x000668D7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002193 RID: 8595
			// (set) Token: 0x06003E27 RID: 15911 RVA: 0x000686EF File Offset: 0x000668EF
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
