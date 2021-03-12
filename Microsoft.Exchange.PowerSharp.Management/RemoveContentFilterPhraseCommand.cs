using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006FE RID: 1790
	public class RemoveContentFilterPhraseCommand : SyntheticCommandWithPipelineInput<ContentFilterPhrase, ContentFilterPhrase>
	{
		// Token: 0x06005CA0 RID: 23712 RVA: 0x0008FCF2 File Offset: 0x0008DEF2
		private RemoveContentFilterPhraseCommand() : base("Remove-ContentFilterPhrase")
		{
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x0008FCFF File Offset: 0x0008DEFF
		public RemoveContentFilterPhraseCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x0008FD0E File Offset: 0x0008DF0E
		public virtual RemoveContentFilterPhraseCommand SetParameters(RemoveContentFilterPhraseCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x0008FD18 File Offset: 0x0008DF18
		public virtual RemoveContentFilterPhraseCommand SetParameters(RemoveContentFilterPhraseCommand.PhraseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x0008FD22 File Offset: 0x0008DF22
		public virtual RemoveContentFilterPhraseCommand SetParameters(RemoveContentFilterPhraseCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006FF RID: 1791
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003A5D RID: 14941
			// (set) Token: 0x06005CA5 RID: 23717 RVA: 0x0008FD2C File Offset: 0x0008DF2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A5E RID: 14942
			// (set) Token: 0x06005CA6 RID: 23718 RVA: 0x0008FD3F File Offset: 0x0008DF3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A5F RID: 14943
			// (set) Token: 0x06005CA7 RID: 23719 RVA: 0x0008FD57 File Offset: 0x0008DF57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A60 RID: 14944
			// (set) Token: 0x06005CA8 RID: 23720 RVA: 0x0008FD6F File Offset: 0x0008DF6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A61 RID: 14945
			// (set) Token: 0x06005CA9 RID: 23721 RVA: 0x0008FD87 File Offset: 0x0008DF87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A62 RID: 14946
			// (set) Token: 0x06005CAA RID: 23722 RVA: 0x0008FD9F File Offset: 0x0008DF9F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003A63 RID: 14947
			// (set) Token: 0x06005CAB RID: 23723 RVA: 0x0008FDB7 File Offset: 0x0008DFB7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000700 RID: 1792
		public class PhraseParameters : ParametersBase
		{
			// Token: 0x17003A64 RID: 14948
			// (set) Token: 0x06005CAD RID: 23725 RVA: 0x0008FDD7 File Offset: 0x0008DFD7
			public virtual string Phrase
			{
				set
				{
					base.PowerSharpParameters["Phrase"] = ((value != null) ? new ContentFilterPhraseIdParameter(value) : null);
				}
			}

			// Token: 0x17003A65 RID: 14949
			// (set) Token: 0x06005CAE RID: 23726 RVA: 0x0008FDF5 File Offset: 0x0008DFF5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A66 RID: 14950
			// (set) Token: 0x06005CAF RID: 23727 RVA: 0x0008FE08 File Offset: 0x0008E008
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A67 RID: 14951
			// (set) Token: 0x06005CB0 RID: 23728 RVA: 0x0008FE20 File Offset: 0x0008E020
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A68 RID: 14952
			// (set) Token: 0x06005CB1 RID: 23729 RVA: 0x0008FE38 File Offset: 0x0008E038
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A69 RID: 14953
			// (set) Token: 0x06005CB2 RID: 23730 RVA: 0x0008FE50 File Offset: 0x0008E050
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A6A RID: 14954
			// (set) Token: 0x06005CB3 RID: 23731 RVA: 0x0008FE68 File Offset: 0x0008E068
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003A6B RID: 14955
			// (set) Token: 0x06005CB4 RID: 23732 RVA: 0x0008FE80 File Offset: 0x0008E080
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000701 RID: 1793
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003A6C RID: 14956
			// (set) Token: 0x06005CB6 RID: 23734 RVA: 0x0008FEA0 File Offset: 0x0008E0A0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ContentFilterPhraseIdParameter(value) : null);
				}
			}

			// Token: 0x17003A6D RID: 14957
			// (set) Token: 0x06005CB7 RID: 23735 RVA: 0x0008FEBE File Offset: 0x0008E0BE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003A6E RID: 14958
			// (set) Token: 0x06005CB8 RID: 23736 RVA: 0x0008FED1 File Offset: 0x0008E0D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003A6F RID: 14959
			// (set) Token: 0x06005CB9 RID: 23737 RVA: 0x0008FEE9 File Offset: 0x0008E0E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003A70 RID: 14960
			// (set) Token: 0x06005CBA RID: 23738 RVA: 0x0008FF01 File Offset: 0x0008E101
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003A71 RID: 14961
			// (set) Token: 0x06005CBB RID: 23739 RVA: 0x0008FF19 File Offset: 0x0008E119
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003A72 RID: 14962
			// (set) Token: 0x06005CBC RID: 23740 RVA: 0x0008FF31 File Offset: 0x0008E131
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003A73 RID: 14963
			// (set) Token: 0x06005CBD RID: 23741 RVA: 0x0008FF49 File Offset: 0x0008E149
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
