using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006F6 RID: 1782
	public class AddContentFilterPhraseCommand : SyntheticCommandWithPipelineInput<ContentFilterPhrase, ContentFilterPhrase>
	{
		// Token: 0x06005C72 RID: 23666 RVA: 0x0008F986 File Offset: 0x0008DB86
		private AddContentFilterPhraseCommand() : base("Add-ContentFilterPhrase")
		{
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x0008F993 File Offset: 0x0008DB93
		public AddContentFilterPhraseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x0008F9A2 File Offset: 0x0008DBA2
		public virtual AddContentFilterPhraseCommand SetParameters(AddContentFilterPhraseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006F7 RID: 1783
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A3F RID: 14911
			// (set) Token: 0x06005C75 RID: 23669 RVA: 0x0008F9AC File Offset: 0x0008DBAC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A40 RID: 14912
			// (set) Token: 0x06005C76 RID: 23670 RVA: 0x0008F9BF File Offset: 0x0008DBBF
			public virtual string Phrase
			{
				set
				{
					base.PowerSharpParameters["Phrase"] = value;
				}
			}

			// Token: 0x17003A41 RID: 14913
			// (set) Token: 0x06005C77 RID: 23671 RVA: 0x0008F9D2 File Offset: 0x0008DBD2
			public virtual Influence Influence
			{
				set
				{
					base.PowerSharpParameters["Influence"] = value;
				}
			}

			// Token: 0x17003A42 RID: 14914
			// (set) Token: 0x06005C78 RID: 23672 RVA: 0x0008F9EA File Offset: 0x0008DBEA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A43 RID: 14915
			// (set) Token: 0x06005C79 RID: 23673 RVA: 0x0008FA02 File Offset: 0x0008DC02
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A44 RID: 14916
			// (set) Token: 0x06005C7A RID: 23674 RVA: 0x0008FA1A File Offset: 0x0008DC1A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A45 RID: 14917
			// (set) Token: 0x06005C7B RID: 23675 RVA: 0x0008FA32 File Offset: 0x0008DC32
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A46 RID: 14918
			// (set) Token: 0x06005C7C RID: 23676 RVA: 0x0008FA4A File Offset: 0x0008DC4A
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
