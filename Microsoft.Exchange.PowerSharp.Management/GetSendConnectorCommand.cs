using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200089A RID: 2202
	public class GetSendConnectorCommand : SyntheticCommandWithPipelineInput<SmtpSendConnectorConfig, SmtpSendConnectorConfig>
	{
		// Token: 0x06006D42 RID: 27970 RVA: 0x000A555B File Offset: 0x000A375B
		private GetSendConnectorCommand() : base("Get-SendConnector")
		{
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x000A5568 File Offset: 0x000A3768
		public GetSendConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x000A5577 File Offset: 0x000A3777
		public virtual GetSendConnectorCommand SetParameters(GetSendConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x000A5581 File Offset: 0x000A3781
		public virtual GetSendConnectorCommand SetParameters(GetSendConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200089B RID: 2203
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170047C7 RID: 18375
			// (set) Token: 0x06006D46 RID: 27974 RVA: 0x000A558B File Offset: 0x000A378B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047C8 RID: 18376
			// (set) Token: 0x06006D47 RID: 27975 RVA: 0x000A559E File Offset: 0x000A379E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047C9 RID: 18377
			// (set) Token: 0x06006D48 RID: 27976 RVA: 0x000A55B6 File Offset: 0x000A37B6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047CA RID: 18378
			// (set) Token: 0x06006D49 RID: 27977 RVA: 0x000A55CE File Offset: 0x000A37CE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047CB RID: 18379
			// (set) Token: 0x06006D4A RID: 27978 RVA: 0x000A55E6 File Offset: 0x000A37E6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200089C RID: 2204
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170047CC RID: 18380
			// (set) Token: 0x06006D4C RID: 27980 RVA: 0x000A5606 File Offset: 0x000A3806
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SendConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x170047CD RID: 18381
			// (set) Token: 0x06006D4D RID: 27981 RVA: 0x000A5624 File Offset: 0x000A3824
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047CE RID: 18382
			// (set) Token: 0x06006D4E RID: 27982 RVA: 0x000A5637 File Offset: 0x000A3837
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047CF RID: 18383
			// (set) Token: 0x06006D4F RID: 27983 RVA: 0x000A564F File Offset: 0x000A384F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047D0 RID: 18384
			// (set) Token: 0x06006D50 RID: 27984 RVA: 0x000A5667 File Offset: 0x000A3867
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047D1 RID: 18385
			// (set) Token: 0x06006D51 RID: 27985 RVA: 0x000A567F File Offset: 0x000A387F
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
