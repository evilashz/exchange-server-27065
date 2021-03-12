using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E69 RID: 3689
	public class NewConnectSubscriptionCommand : SyntheticCommandWithPipelineInput<ConnectSubscriptionProxy, ConnectSubscriptionProxy>
	{
		// Token: 0x0600DA09 RID: 55817 RVA: 0x001356A3 File Offset: 0x001338A3
		private NewConnectSubscriptionCommand() : base("New-ConnectSubscription")
		{
		}

		// Token: 0x0600DA0A RID: 55818 RVA: 0x001356B0 File Offset: 0x001338B0
		public NewConnectSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DA0B RID: 55819 RVA: 0x001356BF File Offset: 0x001338BF
		public virtual NewConnectSubscriptionCommand SetParameters(NewConnectSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DA0C RID: 55820 RVA: 0x001356C9 File Offset: 0x001338C9
		public virtual NewConnectSubscriptionCommand SetParameters(NewConnectSubscriptionCommand.FacebookParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DA0D RID: 55821 RVA: 0x001356D3 File Offset: 0x001338D3
		public virtual NewConnectSubscriptionCommand SetParameters(NewConnectSubscriptionCommand.LinkedInParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E6A RID: 3690
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A8F0 RID: 43248
			// (set) Token: 0x0600DA0E RID: 55822 RVA: 0x001356DD File Offset: 0x001338DD
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8F1 RID: 43249
			// (set) Token: 0x0600DA0F RID: 55823 RVA: 0x001356FB File Offset: 0x001338FB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8F2 RID: 43250
			// (set) Token: 0x0600DA10 RID: 55824 RVA: 0x0013570E File Offset: 0x0013390E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8F3 RID: 43251
			// (set) Token: 0x0600DA11 RID: 55825 RVA: 0x00135726 File Offset: 0x00133926
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8F4 RID: 43252
			// (set) Token: 0x0600DA12 RID: 55826 RVA: 0x0013573E File Offset: 0x0013393E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8F5 RID: 43253
			// (set) Token: 0x0600DA13 RID: 55827 RVA: 0x00135756 File Offset: 0x00133956
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8F6 RID: 43254
			// (set) Token: 0x0600DA14 RID: 55828 RVA: 0x0013576E File Offset: 0x0013396E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E6B RID: 3691
		public class FacebookParameterSetParameters : ParametersBase
		{
			// Token: 0x1700A8F7 RID: 43255
			// (set) Token: 0x0600DA16 RID: 55830 RVA: 0x0013578E File Offset: 0x0013398E
			public virtual SwitchParameter Facebook
			{
				set
				{
					base.PowerSharpParameters["Facebook"] = value;
				}
			}

			// Token: 0x1700A8F8 RID: 43256
			// (set) Token: 0x0600DA17 RID: 55831 RVA: 0x001357A6 File Offset: 0x001339A6
			public virtual string AppAuthorizationCode
			{
				set
				{
					base.PowerSharpParameters["AppAuthorizationCode"] = value;
				}
			}

			// Token: 0x1700A8F9 RID: 43257
			// (set) Token: 0x0600DA18 RID: 55832 RVA: 0x001357B9 File Offset: 0x001339B9
			public virtual string RedirectUri
			{
				set
				{
					base.PowerSharpParameters["RedirectUri"] = value;
				}
			}

			// Token: 0x1700A8FA RID: 43258
			// (set) Token: 0x0600DA19 RID: 55833 RVA: 0x001357CC File Offset: 0x001339CC
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8FB RID: 43259
			// (set) Token: 0x0600DA1A RID: 55834 RVA: 0x001357EA File Offset: 0x001339EA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8FC RID: 43260
			// (set) Token: 0x0600DA1B RID: 55835 RVA: 0x001357FD File Offset: 0x001339FD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8FD RID: 43261
			// (set) Token: 0x0600DA1C RID: 55836 RVA: 0x00135815 File Offset: 0x00133A15
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8FE RID: 43262
			// (set) Token: 0x0600DA1D RID: 55837 RVA: 0x0013582D File Offset: 0x00133A2D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8FF RID: 43263
			// (set) Token: 0x0600DA1E RID: 55838 RVA: 0x00135845 File Offset: 0x00133A45
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A900 RID: 43264
			// (set) Token: 0x0600DA1F RID: 55839 RVA: 0x0013585D File Offset: 0x00133A5D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E6C RID: 3692
		public class LinkedInParameterSetParameters : ParametersBase
		{
			// Token: 0x1700A901 RID: 43265
			// (set) Token: 0x0600DA21 RID: 55841 RVA: 0x0013587D File Offset: 0x00133A7D
			public virtual SwitchParameter LinkedIn
			{
				set
				{
					base.PowerSharpParameters["LinkedIn"] = value;
				}
			}

			// Token: 0x1700A902 RID: 43266
			// (set) Token: 0x0600DA22 RID: 55842 RVA: 0x00135895 File Offset: 0x00133A95
			public virtual string RequestToken
			{
				set
				{
					base.PowerSharpParameters["RequestToken"] = value;
				}
			}

			// Token: 0x1700A903 RID: 43267
			// (set) Token: 0x0600DA23 RID: 55843 RVA: 0x001358A8 File Offset: 0x00133AA8
			public virtual string RequestSecret
			{
				set
				{
					base.PowerSharpParameters["RequestSecret"] = value;
				}
			}

			// Token: 0x1700A904 RID: 43268
			// (set) Token: 0x0600DA24 RID: 55844 RVA: 0x001358BB File Offset: 0x00133ABB
			public virtual string OAuthVerifier
			{
				set
				{
					base.PowerSharpParameters["OAuthVerifier"] = value;
				}
			}

			// Token: 0x1700A905 RID: 43269
			// (set) Token: 0x0600DA25 RID: 55845 RVA: 0x001358CE File Offset: 0x00133ACE
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A906 RID: 43270
			// (set) Token: 0x0600DA26 RID: 55846 RVA: 0x001358EC File Offset: 0x00133AEC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A907 RID: 43271
			// (set) Token: 0x0600DA27 RID: 55847 RVA: 0x001358FF File Offset: 0x00133AFF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A908 RID: 43272
			// (set) Token: 0x0600DA28 RID: 55848 RVA: 0x00135917 File Offset: 0x00133B17
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A909 RID: 43273
			// (set) Token: 0x0600DA29 RID: 55849 RVA: 0x0013592F File Offset: 0x00133B2F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A90A RID: 43274
			// (set) Token: 0x0600DA2A RID: 55850 RVA: 0x00135947 File Offset: 0x00133B47
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A90B RID: 43275
			// (set) Token: 0x0600DA2B RID: 55851 RVA: 0x0013595F File Offset: 0x00133B5F
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
