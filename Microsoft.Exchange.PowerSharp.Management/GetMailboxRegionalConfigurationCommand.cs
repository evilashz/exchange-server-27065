using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000495 RID: 1173
	public class GetMailboxRegionalConfigurationCommand : SyntheticCommandWithPipelineInput<MailboxRegionalConfiguration, MailboxRegionalConfiguration>
	{
		// Token: 0x0600421B RID: 16923 RVA: 0x0006D898 File Offset: 0x0006BA98
		private GetMailboxRegionalConfigurationCommand() : base("Get-MailboxRegionalConfiguration")
		{
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x0006D8A5 File Offset: 0x0006BAA5
		public GetMailboxRegionalConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x0006D8B4 File Offset: 0x0006BAB4
		public virtual GetMailboxRegionalConfigurationCommand SetParameters(GetMailboxRegionalConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x0006D8BE File Offset: 0x0006BABE
		public virtual GetMailboxRegionalConfigurationCommand SetParameters(GetMailboxRegionalConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000496 RID: 1174
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170024AA RID: 9386
			// (set) Token: 0x0600421F RID: 16927 RVA: 0x0006D8C8 File Offset: 0x0006BAC8
			public virtual SwitchParameter VerifyDefaultFolderNameLanguage
			{
				set
				{
					base.PowerSharpParameters["VerifyDefaultFolderNameLanguage"] = value;
				}
			}

			// Token: 0x170024AB RID: 9387
			// (set) Token: 0x06004220 RID: 16928 RVA: 0x0006D8E0 File Offset: 0x0006BAE0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024AC RID: 9388
			// (set) Token: 0x06004221 RID: 16929 RVA: 0x0006D8F3 File Offset: 0x0006BAF3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024AD RID: 9389
			// (set) Token: 0x06004222 RID: 16930 RVA: 0x0006D90B File Offset: 0x0006BB0B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024AE RID: 9390
			// (set) Token: 0x06004223 RID: 16931 RVA: 0x0006D923 File Offset: 0x0006BB23
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024AF RID: 9391
			// (set) Token: 0x06004224 RID: 16932 RVA: 0x0006D93B File Offset: 0x0006BB3B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000497 RID: 1175
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170024B0 RID: 9392
			// (set) Token: 0x06004226 RID: 16934 RVA: 0x0006D95B File Offset: 0x0006BB5B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170024B1 RID: 9393
			// (set) Token: 0x06004227 RID: 16935 RVA: 0x0006D979 File Offset: 0x0006BB79
			public virtual SwitchParameter VerifyDefaultFolderNameLanguage
			{
				set
				{
					base.PowerSharpParameters["VerifyDefaultFolderNameLanguage"] = value;
				}
			}

			// Token: 0x170024B2 RID: 9394
			// (set) Token: 0x06004228 RID: 16936 RVA: 0x0006D991 File Offset: 0x0006BB91
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024B3 RID: 9395
			// (set) Token: 0x06004229 RID: 16937 RVA: 0x0006D9A4 File Offset: 0x0006BBA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024B4 RID: 9396
			// (set) Token: 0x0600422A RID: 16938 RVA: 0x0006D9BC File Offset: 0x0006BBBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024B5 RID: 9397
			// (set) Token: 0x0600422B RID: 16939 RVA: 0x0006D9D4 File Offset: 0x0006BBD4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024B6 RID: 9398
			// (set) Token: 0x0600422C RID: 16940 RVA: 0x0006D9EC File Offset: 0x0006BBEC
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
