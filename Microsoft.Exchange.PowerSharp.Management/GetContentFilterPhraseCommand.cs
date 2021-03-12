using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006FA RID: 1786
	public class GetContentFilterPhraseCommand : SyntheticCommandWithPipelineInput<ContentFilterPhrase, ContentFilterPhrase>
	{
		// Token: 0x06005C87 RID: 23687 RVA: 0x0008FB0B File Offset: 0x0008DD0B
		private GetContentFilterPhraseCommand() : base("Get-ContentFilterPhrase")
		{
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x0008FB18 File Offset: 0x0008DD18
		public GetContentFilterPhraseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x0008FB27 File Offset: 0x0008DD27
		public virtual GetContentFilterPhraseCommand SetParameters(GetContentFilterPhraseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x0008FB31 File Offset: 0x0008DD31
		public virtual GetContentFilterPhraseCommand SetParameters(GetContentFilterPhraseCommand.PhraseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x0008FB3B File Offset: 0x0008DD3B
		public virtual GetContentFilterPhraseCommand SetParameters(GetContentFilterPhraseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006FB RID: 1787
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A4C RID: 14924
			// (set) Token: 0x06005C8C RID: 23692 RVA: 0x0008FB45 File Offset: 0x0008DD45
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A4D RID: 14925
			// (set) Token: 0x06005C8D RID: 23693 RVA: 0x0008FB58 File Offset: 0x0008DD58
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A4E RID: 14926
			// (set) Token: 0x06005C8E RID: 23694 RVA: 0x0008FB70 File Offset: 0x0008DD70
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A4F RID: 14927
			// (set) Token: 0x06005C8F RID: 23695 RVA: 0x0008FB88 File Offset: 0x0008DD88
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A50 RID: 14928
			// (set) Token: 0x06005C90 RID: 23696 RVA: 0x0008FBA0 File Offset: 0x0008DDA0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020006FC RID: 1788
		public class PhraseParameters : ParametersBase
		{
			// Token: 0x17003A51 RID: 14929
			// (set) Token: 0x06005C92 RID: 23698 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
			public virtual string Phrase
			{
				set
				{
					base.PowerSharpParameters["Phrase"] = ((value != null) ? new ContentFilterPhraseIdParameter(value) : null);
				}
			}

			// Token: 0x17003A52 RID: 14930
			// (set) Token: 0x06005C93 RID: 23699 RVA: 0x0008FBDE File Offset: 0x0008DDDE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A53 RID: 14931
			// (set) Token: 0x06005C94 RID: 23700 RVA: 0x0008FBF1 File Offset: 0x0008DDF1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A54 RID: 14932
			// (set) Token: 0x06005C95 RID: 23701 RVA: 0x0008FC09 File Offset: 0x0008DE09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A55 RID: 14933
			// (set) Token: 0x06005C96 RID: 23702 RVA: 0x0008FC21 File Offset: 0x0008DE21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A56 RID: 14934
			// (set) Token: 0x06005C97 RID: 23703 RVA: 0x0008FC39 File Offset: 0x0008DE39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020006FD RID: 1789
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003A57 RID: 14935
			// (set) Token: 0x06005C99 RID: 23705 RVA: 0x0008FC59 File Offset: 0x0008DE59
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ContentFilterPhraseIdParameter(value) : null);
				}
			}

			// Token: 0x17003A58 RID: 14936
			// (set) Token: 0x06005C9A RID: 23706 RVA: 0x0008FC77 File Offset: 0x0008DE77
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A59 RID: 14937
			// (set) Token: 0x06005C9B RID: 23707 RVA: 0x0008FC8A File Offset: 0x0008DE8A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A5A RID: 14938
			// (set) Token: 0x06005C9C RID: 23708 RVA: 0x0008FCA2 File Offset: 0x0008DEA2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A5B RID: 14939
			// (set) Token: 0x06005C9D RID: 23709 RVA: 0x0008FCBA File Offset: 0x0008DEBA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A5C RID: 14940
			// (set) Token: 0x06005C9E RID: 23710 RVA: 0x0008FCD2 File Offset: 0x0008DED2
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
