using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Search;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000099 RID: 153
	public class NewSearchDocumentFormatCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x000385F4 File Offset: 0x000367F4
		private NewSearchDocumentFormatCommand() : base("New-SearchDocumentFormat")
		{
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00038601 File Offset: 0x00036801
		public NewSearchDocumentFormatCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00038610 File Offset: 0x00036810
		public virtual NewSearchDocumentFormatCommand SetParameters(NewSearchDocumentFormatCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200009A RID: 154
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170003C7 RID: 967
			// (set) Token: 0x06001943 RID: 6467 RVA: 0x0003861A File Offset: 0x0003681A
			public virtual SearchDocumentFormatId Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170003C8 RID: 968
			// (set) Token: 0x06001944 RID: 6468 RVA: 0x0003862D File Offset: 0x0003682D
			public virtual string Extension
			{
				set
				{
					base.PowerSharpParameters["Extension"] = value;
				}
			}

			// Token: 0x170003C9 RID: 969
			// (set) Token: 0x06001945 RID: 6469 RVA: 0x00038640 File Offset: 0x00036840
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170003CA RID: 970
			// (set) Token: 0x06001946 RID: 6470 RVA: 0x00038653 File Offset: 0x00036853
			public virtual string MimeType
			{
				set
				{
					base.PowerSharpParameters["MimeType"] = value;
				}
			}

			// Token: 0x170003CB RID: 971
			// (set) Token: 0x06001947 RID: 6471 RVA: 0x00038666 File Offset: 0x00036866
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170003CC RID: 972
			// (set) Token: 0x06001948 RID: 6472 RVA: 0x00038679 File Offset: 0x00036879
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170003CD RID: 973
			// (set) Token: 0x06001949 RID: 6473 RVA: 0x00038691 File Offset: 0x00036891
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170003CE RID: 974
			// (set) Token: 0x0600194A RID: 6474 RVA: 0x000386A9 File Offset: 0x000368A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170003CF RID: 975
			// (set) Token: 0x0600194B RID: 6475 RVA: 0x000386C1 File Offset: 0x000368C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170003D0 RID: 976
			// (set) Token: 0x0600194C RID: 6476 RVA: 0x000386D9 File Offset: 0x000368D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170003D1 RID: 977
			// (set) Token: 0x0600194D RID: 6477 RVA: 0x000386F1 File Offset: 0x000368F1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
