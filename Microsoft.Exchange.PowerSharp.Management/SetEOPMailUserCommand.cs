using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C2D RID: 3117
	public class SetEOPMailUserCommand : SyntheticCommand<object>
	{
		// Token: 0x0600980D RID: 38925 RVA: 0x000DD120 File Offset: 0x000DB320
		private SetEOPMailUserCommand() : base("Set-EOPMailUser")
		{
		}

		// Token: 0x0600980E RID: 38926 RVA: 0x000DD12D File Offset: 0x000DB32D
		public SetEOPMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600980F RID: 38927 RVA: 0x000DD13C File Offset: 0x000DB33C
		public virtual SetEOPMailUserCommand SetParameters(SetEOPMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C2E RID: 3118
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B6C RID: 27500
			// (set) Token: 0x06009810 RID: 38928 RVA: 0x000DD146 File Offset: 0x000DB346
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17006B6D RID: 27501
			// (set) Token: 0x06009811 RID: 38929 RVA: 0x000DD164 File Offset: 0x000DB364
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B6E RID: 27502
			// (set) Token: 0x06009812 RID: 38930 RVA: 0x000DD177 File Offset: 0x000DB377
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006B6F RID: 27503
			// (set) Token: 0x06009813 RID: 38931 RVA: 0x000DD18A File Offset: 0x000DB38A
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006B70 RID: 27504
			// (set) Token: 0x06009814 RID: 38932 RVA: 0x000DD19D File Offset: 0x000DB39D
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006B71 RID: 27505
			// (set) Token: 0x06009815 RID: 38933 RVA: 0x000DD1B0 File Offset: 0x000DB3B0
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17006B72 RID: 27506
			// (set) Token: 0x06009816 RID: 38934 RVA: 0x000DD1C8 File Offset: 0x000DB3C8
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17006B73 RID: 27507
			// (set) Token: 0x06009817 RID: 38935 RVA: 0x000DD1DB File Offset: 0x000DB3DB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B74 RID: 27508
			// (set) Token: 0x06009818 RID: 38936 RVA: 0x000DD1F9 File Offset: 0x000DB3F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B75 RID: 27509
			// (set) Token: 0x06009819 RID: 38937 RVA: 0x000DD211 File Offset: 0x000DB411
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B76 RID: 27510
			// (set) Token: 0x0600981A RID: 38938 RVA: 0x000DD229 File Offset: 0x000DB429
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B77 RID: 27511
			// (set) Token: 0x0600981B RID: 38939 RVA: 0x000DD241 File Offset: 0x000DB441
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
