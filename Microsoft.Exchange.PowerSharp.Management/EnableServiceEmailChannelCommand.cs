using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000429 RID: 1065
	public class EnableServiceEmailChannelCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x06003E3D RID: 15933 RVA: 0x000688B3 File Offset: 0x00066AB3
		private EnableServiceEmailChannelCommand() : base("Enable-ServiceEmailChannel")
		{
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000688C0 File Offset: 0x00066AC0
		public EnableServiceEmailChannelCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x000688CF File Offset: 0x00066ACF
		public virtual EnableServiceEmailChannelCommand SetParameters(EnableServiceEmailChannelCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000688D9 File Offset: 0x00066AD9
		public virtual EnableServiceEmailChannelCommand SetParameters(EnableServiceEmailChannelCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200042A RID: 1066
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170021A4 RID: 8612
			// (set) Token: 0x06003E41 RID: 15937 RVA: 0x000688E3 File Offset: 0x00066AE3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021A5 RID: 8613
			// (set) Token: 0x06003E42 RID: 15938 RVA: 0x000688F6 File Offset: 0x00066AF6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021A6 RID: 8614
			// (set) Token: 0x06003E43 RID: 15939 RVA: 0x0006890E File Offset: 0x00066B0E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021A7 RID: 8615
			// (set) Token: 0x06003E44 RID: 15940 RVA: 0x00068926 File Offset: 0x00066B26
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021A8 RID: 8616
			// (set) Token: 0x06003E45 RID: 15941 RVA: 0x0006893E File Offset: 0x00066B3E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021A9 RID: 8617
			// (set) Token: 0x06003E46 RID: 15942 RVA: 0x00068956 File Offset: 0x00066B56
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200042B RID: 1067
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170021AA RID: 8618
			// (set) Token: 0x06003E48 RID: 15944 RVA: 0x00068976 File Offset: 0x00066B76
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170021AB RID: 8619
			// (set) Token: 0x06003E49 RID: 15945 RVA: 0x00068994 File Offset: 0x00066B94
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021AC RID: 8620
			// (set) Token: 0x06003E4A RID: 15946 RVA: 0x000689A7 File Offset: 0x00066BA7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021AD RID: 8621
			// (set) Token: 0x06003E4B RID: 15947 RVA: 0x000689BF File Offset: 0x00066BBF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021AE RID: 8622
			// (set) Token: 0x06003E4C RID: 15948 RVA: 0x000689D7 File Offset: 0x00066BD7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021AF RID: 8623
			// (set) Token: 0x06003E4D RID: 15949 RVA: 0x000689EF File Offset: 0x00066BEF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021B0 RID: 8624
			// (set) Token: 0x06003E4E RID: 15950 RVA: 0x00068A07 File Offset: 0x00066C07
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
