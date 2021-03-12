using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002FF RID: 767
	public class GetImapSettingsCommand : SyntheticCommandWithPipelineInput<Imap4AdConfiguration, Imap4AdConfiguration>
	{
		// Token: 0x06003340 RID: 13120 RVA: 0x0005A588 File Offset: 0x00058788
		private GetImapSettingsCommand() : base("Get-ImapSettings")
		{
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x0005A595 File Offset: 0x00058795
		public GetImapSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x0005A5A4 File Offset: 0x000587A4
		public virtual GetImapSettingsCommand SetParameters(GetImapSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000300 RID: 768
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170018FB RID: 6395
			// (set) Token: 0x06003343 RID: 13123 RVA: 0x0005A5AE File Offset: 0x000587AE
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170018FC RID: 6396
			// (set) Token: 0x06003344 RID: 13124 RVA: 0x0005A5C1 File Offset: 0x000587C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018FD RID: 6397
			// (set) Token: 0x06003345 RID: 13125 RVA: 0x0005A5D4 File Offset: 0x000587D4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018FE RID: 6398
			// (set) Token: 0x06003346 RID: 13126 RVA: 0x0005A5EC File Offset: 0x000587EC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018FF RID: 6399
			// (set) Token: 0x06003347 RID: 13127 RVA: 0x0005A604 File Offset: 0x00058804
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001900 RID: 6400
			// (set) Token: 0x06003348 RID: 13128 RVA: 0x0005A61C File Offset: 0x0005881C
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
