using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000303 RID: 771
	public class NewImapSettingsCommand : SyntheticCommandWithPipelineInput<Imap4AdConfiguration, Imap4AdConfiguration>
	{
		// Token: 0x06003371 RID: 13169 RVA: 0x0005A980 File Offset: 0x00058B80
		private NewImapSettingsCommand() : base("New-ImapSettings")
		{
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0005A98D File Offset: 0x00058B8D
		public NewImapSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0005A99C File Offset: 0x00058B9C
		public virtual NewImapSettingsCommand SetParameters(NewImapSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000304 RID: 772
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001924 RID: 6436
			// (set) Token: 0x06003374 RID: 13172 RVA: 0x0005A9A6 File Offset: 0x00058BA6
			public virtual string ExchangePath
			{
				set
				{
					base.PowerSharpParameters["ExchangePath"] = value;
				}
			}

			// Token: 0x17001925 RID: 6437
			// (set) Token: 0x06003375 RID: 13173 RVA: 0x0005A9B9 File Offset: 0x00058BB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001926 RID: 6438
			// (set) Token: 0x06003376 RID: 13174 RVA: 0x0005A9CC File Offset: 0x00058BCC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001927 RID: 6439
			// (set) Token: 0x06003377 RID: 13175 RVA: 0x0005A9E4 File Offset: 0x00058BE4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001928 RID: 6440
			// (set) Token: 0x06003378 RID: 13176 RVA: 0x0005A9FC File Offset: 0x00058BFC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001929 RID: 6441
			// (set) Token: 0x06003379 RID: 13177 RVA: 0x0005AA14 File Offset: 0x00058C14
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
