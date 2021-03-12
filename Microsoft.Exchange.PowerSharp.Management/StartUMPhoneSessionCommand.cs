using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMPhoneSession;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B9A RID: 2970
	public class StartUMPhoneSessionCommand : SyntheticCommandWithPipelineInput<UMPhoneSession, UMPhoneSession>
	{
		// Token: 0x0600900A RID: 36874 RVA: 0x000D2B09 File Offset: 0x000D0D09
		private StartUMPhoneSessionCommand() : base("Start-UMPhoneSession")
		{
		}

		// Token: 0x0600900B RID: 36875 RVA: 0x000D2B16 File Offset: 0x000D0D16
		public StartUMPhoneSessionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600900C RID: 36876 RVA: 0x000D2B25 File Offset: 0x000D0D25
		public virtual StartUMPhoneSessionCommand SetParameters(StartUMPhoneSessionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600900D RID: 36877 RVA: 0x000D2B2F File Offset: 0x000D0D2F
		public virtual StartUMPhoneSessionCommand SetParameters(StartUMPhoneSessionCommand.DefaultVoicemailGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600900E RID: 36878 RVA: 0x000D2B39 File Offset: 0x000D0D39
		public virtual StartUMPhoneSessionCommand SetParameters(StartUMPhoneSessionCommand.AwayVoicemailGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600900F RID: 36879 RVA: 0x000D2B43 File Offset: 0x000D0D43
		public virtual StartUMPhoneSessionCommand SetParameters(StartUMPhoneSessionCommand.PlayOnPhoneGreetingParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B9B RID: 2971
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700648F RID: 25743
			// (set) Token: 0x06009010 RID: 36880 RVA: 0x000D2B4D File Offset: 0x000D0D4D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006490 RID: 25744
			// (set) Token: 0x06009011 RID: 36881 RVA: 0x000D2B6B File Offset: 0x000D0D6B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006491 RID: 25745
			// (set) Token: 0x06009012 RID: 36882 RVA: 0x000D2B7E File Offset: 0x000D0D7E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006492 RID: 25746
			// (set) Token: 0x06009013 RID: 36883 RVA: 0x000D2B96 File Offset: 0x000D0D96
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006493 RID: 25747
			// (set) Token: 0x06009014 RID: 36884 RVA: 0x000D2BAE File Offset: 0x000D0DAE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006494 RID: 25748
			// (set) Token: 0x06009015 RID: 36885 RVA: 0x000D2BC6 File Offset: 0x000D0DC6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006495 RID: 25749
			// (set) Token: 0x06009016 RID: 36886 RVA: 0x000D2BDE File Offset: 0x000D0DDE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B9C RID: 2972
		public class DefaultVoicemailGreetingParameters : ParametersBase
		{
			// Token: 0x17006496 RID: 25750
			// (set) Token: 0x06009018 RID: 36888 RVA: 0x000D2BFE File Offset: 0x000D0DFE
			public virtual string UMMailbox
			{
				set
				{
					base.PowerSharpParameters["UMMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006497 RID: 25751
			// (set) Token: 0x06009019 RID: 36889 RVA: 0x000D2C1C File Offset: 0x000D0E1C
			public virtual string PhoneNumber
			{
				set
				{
					base.PowerSharpParameters["PhoneNumber"] = value;
				}
			}

			// Token: 0x17006498 RID: 25752
			// (set) Token: 0x0600901A RID: 36890 RVA: 0x000D2C2F File Offset: 0x000D0E2F
			public virtual SwitchParameter DefaultVoicemailGreeting
			{
				set
				{
					base.PowerSharpParameters["DefaultVoicemailGreeting"] = value;
				}
			}

			// Token: 0x17006499 RID: 25753
			// (set) Token: 0x0600901B RID: 36891 RVA: 0x000D2C47 File Offset: 0x000D0E47
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700649A RID: 25754
			// (set) Token: 0x0600901C RID: 36892 RVA: 0x000D2C65 File Offset: 0x000D0E65
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700649B RID: 25755
			// (set) Token: 0x0600901D RID: 36893 RVA: 0x000D2C78 File Offset: 0x000D0E78
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700649C RID: 25756
			// (set) Token: 0x0600901E RID: 36894 RVA: 0x000D2C90 File Offset: 0x000D0E90
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700649D RID: 25757
			// (set) Token: 0x0600901F RID: 36895 RVA: 0x000D2CA8 File Offset: 0x000D0EA8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700649E RID: 25758
			// (set) Token: 0x06009020 RID: 36896 RVA: 0x000D2CC0 File Offset: 0x000D0EC0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700649F RID: 25759
			// (set) Token: 0x06009021 RID: 36897 RVA: 0x000D2CD8 File Offset: 0x000D0ED8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B9D RID: 2973
		public class AwayVoicemailGreetingParameters : ParametersBase
		{
			// Token: 0x170064A0 RID: 25760
			// (set) Token: 0x06009023 RID: 36899 RVA: 0x000D2CF8 File Offset: 0x000D0EF8
			public virtual string UMMailbox
			{
				set
				{
					base.PowerSharpParameters["UMMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170064A1 RID: 25761
			// (set) Token: 0x06009024 RID: 36900 RVA: 0x000D2D16 File Offset: 0x000D0F16
			public virtual string PhoneNumber
			{
				set
				{
					base.PowerSharpParameters["PhoneNumber"] = value;
				}
			}

			// Token: 0x170064A2 RID: 25762
			// (set) Token: 0x06009025 RID: 36901 RVA: 0x000D2D29 File Offset: 0x000D0F29
			public virtual SwitchParameter AwayVoicemailGreeting
			{
				set
				{
					base.PowerSharpParameters["AwayVoicemailGreeting"] = value;
				}
			}

			// Token: 0x170064A3 RID: 25763
			// (set) Token: 0x06009026 RID: 36902 RVA: 0x000D2D41 File Offset: 0x000D0F41
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170064A4 RID: 25764
			// (set) Token: 0x06009027 RID: 36903 RVA: 0x000D2D5F File Offset: 0x000D0F5F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064A5 RID: 25765
			// (set) Token: 0x06009028 RID: 36904 RVA: 0x000D2D72 File Offset: 0x000D0F72
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064A6 RID: 25766
			// (set) Token: 0x06009029 RID: 36905 RVA: 0x000D2D8A File Offset: 0x000D0F8A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064A7 RID: 25767
			// (set) Token: 0x0600902A RID: 36906 RVA: 0x000D2DA2 File Offset: 0x000D0FA2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064A8 RID: 25768
			// (set) Token: 0x0600902B RID: 36907 RVA: 0x000D2DBA File Offset: 0x000D0FBA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170064A9 RID: 25769
			// (set) Token: 0x0600902C RID: 36908 RVA: 0x000D2DD2 File Offset: 0x000D0FD2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B9E RID: 2974
		public class PlayOnPhoneGreetingParameters : ParametersBase
		{
			// Token: 0x170064AA RID: 25770
			// (set) Token: 0x0600902E RID: 36910 RVA: 0x000D2DF2 File Offset: 0x000D0FF2
			public virtual string PhoneNumber
			{
				set
				{
					base.PowerSharpParameters["PhoneNumber"] = value;
				}
			}

			// Token: 0x170064AB RID: 25771
			// (set) Token: 0x0600902F RID: 36911 RVA: 0x000D2E05 File Offset: 0x000D1005
			public virtual string CallAnsweringRuleId
			{
				set
				{
					base.PowerSharpParameters["CallAnsweringRuleId"] = ((value != null) ? new UMCallAnsweringRuleIdParameter(value) : null);
				}
			}

			// Token: 0x170064AC RID: 25772
			// (set) Token: 0x06009030 RID: 36912 RVA: 0x000D2E23 File Offset: 0x000D1023
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170064AD RID: 25773
			// (set) Token: 0x06009031 RID: 36913 RVA: 0x000D2E41 File Offset: 0x000D1041
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064AE RID: 25774
			// (set) Token: 0x06009032 RID: 36914 RVA: 0x000D2E54 File Offset: 0x000D1054
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064AF RID: 25775
			// (set) Token: 0x06009033 RID: 36915 RVA: 0x000D2E6C File Offset: 0x000D106C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064B0 RID: 25776
			// (set) Token: 0x06009034 RID: 36916 RVA: 0x000D2E84 File Offset: 0x000D1084
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064B1 RID: 25777
			// (set) Token: 0x06009035 RID: 36917 RVA: 0x000D2E9C File Offset: 0x000D109C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170064B2 RID: 25778
			// (set) Token: 0x06009036 RID: 36918 RVA: 0x000D2EB4 File Offset: 0x000D10B4
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
