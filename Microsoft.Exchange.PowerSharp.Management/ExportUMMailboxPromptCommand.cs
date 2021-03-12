using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B2F RID: 2863
	public class ExportUMMailboxPromptCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06008BE7 RID: 35815 RVA: 0x000CD594 File Offset: 0x000CB794
		private ExportUMMailboxPromptCommand() : base("Export-UMMailboxPrompt")
		{
		}

		// Token: 0x06008BE8 RID: 35816 RVA: 0x000CD5A1 File Offset: 0x000CB7A1
		public ExportUMMailboxPromptCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008BE9 RID: 35817 RVA: 0x000CD5B0 File Offset: 0x000CB7B0
		public virtual ExportUMMailboxPromptCommand SetParameters(ExportUMMailboxPromptCommand.CustomAwayGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008BEA RID: 35818 RVA: 0x000CD5BA File Offset: 0x000CB7BA
		public virtual ExportUMMailboxPromptCommand SetParameters(ExportUMMailboxPromptCommand.DefaultVoicemailGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008BEB RID: 35819 RVA: 0x000CD5C4 File Offset: 0x000CB7C4
		public virtual ExportUMMailboxPromptCommand SetParameters(ExportUMMailboxPromptCommand.DefaultAwayGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008BEC RID: 35820 RVA: 0x000CD5CE File Offset: 0x000CB7CE
		public virtual ExportUMMailboxPromptCommand SetParameters(ExportUMMailboxPromptCommand.CustomVoicemailGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008BED RID: 35821 RVA: 0x000CD5D8 File Offset: 0x000CB7D8
		public virtual ExportUMMailboxPromptCommand SetParameters(ExportUMMailboxPromptCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B30 RID: 2864
		public class CustomAwayGreetingParameters : ParametersBase
		{
			// Token: 0x17006142 RID: 24898
			// (set) Token: 0x06008BEE RID: 35822 RVA: 0x000CD5E2 File Offset: 0x000CB7E2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006143 RID: 24899
			// (set) Token: 0x06008BEF RID: 35823 RVA: 0x000CD600 File Offset: 0x000CB800
			public virtual SwitchParameter CustomAwayGreeting
			{
				set
				{
					base.PowerSharpParameters["CustomAwayGreeting"] = value;
				}
			}

			// Token: 0x17006144 RID: 24900
			// (set) Token: 0x06008BF0 RID: 35824 RVA: 0x000CD618 File Offset: 0x000CB818
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006145 RID: 24901
			// (set) Token: 0x06008BF1 RID: 35825 RVA: 0x000CD62B File Offset: 0x000CB82B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006146 RID: 24902
			// (set) Token: 0x06008BF2 RID: 35826 RVA: 0x000CD643 File Offset: 0x000CB843
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006147 RID: 24903
			// (set) Token: 0x06008BF3 RID: 35827 RVA: 0x000CD65B File Offset: 0x000CB85B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006148 RID: 24904
			// (set) Token: 0x06008BF4 RID: 35828 RVA: 0x000CD673 File Offset: 0x000CB873
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006149 RID: 24905
			// (set) Token: 0x06008BF5 RID: 35829 RVA: 0x000CD68B File Offset: 0x000CB88B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B31 RID: 2865
		public class DefaultVoicemailGreetingParameters : ParametersBase
		{
			// Token: 0x1700614A RID: 24906
			// (set) Token: 0x06008BF7 RID: 35831 RVA: 0x000CD6AB File Offset: 0x000CB8AB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700614B RID: 24907
			// (set) Token: 0x06008BF8 RID: 35832 RVA: 0x000CD6C9 File Offset: 0x000CB8C9
			public virtual SwitchParameter DefaultVoicemailGreeting
			{
				set
				{
					base.PowerSharpParameters["DefaultVoicemailGreeting"] = value;
				}
			}

			// Token: 0x1700614C RID: 24908
			// (set) Token: 0x06008BF9 RID: 35833 RVA: 0x000CD6E1 File Offset: 0x000CB8E1
			public virtual string TestPhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["TestPhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700614D RID: 24909
			// (set) Token: 0x06008BFA RID: 35834 RVA: 0x000CD6F4 File Offset: 0x000CB8F4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700614E RID: 24910
			// (set) Token: 0x06008BFB RID: 35835 RVA: 0x000CD707 File Offset: 0x000CB907
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700614F RID: 24911
			// (set) Token: 0x06008BFC RID: 35836 RVA: 0x000CD71F File Offset: 0x000CB91F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006150 RID: 24912
			// (set) Token: 0x06008BFD RID: 35837 RVA: 0x000CD737 File Offset: 0x000CB937
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006151 RID: 24913
			// (set) Token: 0x06008BFE RID: 35838 RVA: 0x000CD74F File Offset: 0x000CB94F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006152 RID: 24914
			// (set) Token: 0x06008BFF RID: 35839 RVA: 0x000CD767 File Offset: 0x000CB967
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B32 RID: 2866
		public class DefaultAwayGreetingParameters : ParametersBase
		{
			// Token: 0x17006153 RID: 24915
			// (set) Token: 0x06008C01 RID: 35841 RVA: 0x000CD787 File Offset: 0x000CB987
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006154 RID: 24916
			// (set) Token: 0x06008C02 RID: 35842 RVA: 0x000CD7A5 File Offset: 0x000CB9A5
			public virtual SwitchParameter DefaultAwayGreeting
			{
				set
				{
					base.PowerSharpParameters["DefaultAwayGreeting"] = value;
				}
			}

			// Token: 0x17006155 RID: 24917
			// (set) Token: 0x06008C03 RID: 35843 RVA: 0x000CD7BD File Offset: 0x000CB9BD
			public virtual string TestPhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["TestPhoneticDisplayName"] = value;
				}
			}

			// Token: 0x17006156 RID: 24918
			// (set) Token: 0x06008C04 RID: 35844 RVA: 0x000CD7D0 File Offset: 0x000CB9D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006157 RID: 24919
			// (set) Token: 0x06008C05 RID: 35845 RVA: 0x000CD7E3 File Offset: 0x000CB9E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006158 RID: 24920
			// (set) Token: 0x06008C06 RID: 35846 RVA: 0x000CD7FB File Offset: 0x000CB9FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006159 RID: 24921
			// (set) Token: 0x06008C07 RID: 35847 RVA: 0x000CD813 File Offset: 0x000CBA13
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700615A RID: 24922
			// (set) Token: 0x06008C08 RID: 35848 RVA: 0x000CD82B File Offset: 0x000CBA2B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700615B RID: 24923
			// (set) Token: 0x06008C09 RID: 35849 RVA: 0x000CD843 File Offset: 0x000CBA43
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B33 RID: 2867
		public class CustomVoicemailGreetingParameters : ParametersBase
		{
			// Token: 0x1700615C RID: 24924
			// (set) Token: 0x06008C0B RID: 35851 RVA: 0x000CD863 File Offset: 0x000CBA63
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700615D RID: 24925
			// (set) Token: 0x06008C0C RID: 35852 RVA: 0x000CD881 File Offset: 0x000CBA81
			public virtual SwitchParameter CustomVoicemailGreeting
			{
				set
				{
					base.PowerSharpParameters["CustomVoicemailGreeting"] = value;
				}
			}

			// Token: 0x1700615E RID: 24926
			// (set) Token: 0x06008C0D RID: 35853 RVA: 0x000CD899 File Offset: 0x000CBA99
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700615F RID: 24927
			// (set) Token: 0x06008C0E RID: 35854 RVA: 0x000CD8AC File Offset: 0x000CBAAC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006160 RID: 24928
			// (set) Token: 0x06008C0F RID: 35855 RVA: 0x000CD8C4 File Offset: 0x000CBAC4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006161 RID: 24929
			// (set) Token: 0x06008C10 RID: 35856 RVA: 0x000CD8DC File Offset: 0x000CBADC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006162 RID: 24930
			// (set) Token: 0x06008C11 RID: 35857 RVA: 0x000CD8F4 File Offset: 0x000CBAF4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006163 RID: 24931
			// (set) Token: 0x06008C12 RID: 35858 RVA: 0x000CD90C File Offset: 0x000CBB0C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B34 RID: 2868
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006164 RID: 24932
			// (set) Token: 0x06008C14 RID: 35860 RVA: 0x000CD92C File Offset: 0x000CBB2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006165 RID: 24933
			// (set) Token: 0x06008C15 RID: 35861 RVA: 0x000CD93F File Offset: 0x000CBB3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006166 RID: 24934
			// (set) Token: 0x06008C16 RID: 35862 RVA: 0x000CD957 File Offset: 0x000CBB57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006167 RID: 24935
			// (set) Token: 0x06008C17 RID: 35863 RVA: 0x000CD96F File Offset: 0x000CBB6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006168 RID: 24936
			// (set) Token: 0x06008C18 RID: 35864 RVA: 0x000CD987 File Offset: 0x000CBB87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006169 RID: 24937
			// (set) Token: 0x06008C19 RID: 35865 RVA: 0x000CD99F File Offset: 0x000CBB9F
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
