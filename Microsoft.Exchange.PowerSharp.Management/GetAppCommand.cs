using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Extension;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E40 RID: 3648
	public class GetAppCommand : SyntheticCommandWithPipelineInput<App, App>
	{
		// Token: 0x0600D892 RID: 55442 RVA: 0x001337ED File Offset: 0x001319ED
		private GetAppCommand() : base("Get-App")
		{
		}

		// Token: 0x0600D893 RID: 55443 RVA: 0x001337FA File Offset: 0x001319FA
		public GetAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D894 RID: 55444 RVA: 0x00133809 File Offset: 0x00131A09
		public virtual GetAppCommand SetParameters(GetAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D895 RID: 55445 RVA: 0x00133813 File Offset: 0x00131A13
		public virtual GetAppCommand SetParameters(GetAppCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E41 RID: 3649
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A7CB RID: 42955
			// (set) Token: 0x0600D896 RID: 55446 RVA: 0x0013381D File Offset: 0x00131A1D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7CC RID: 42956
			// (set) Token: 0x0600D897 RID: 55447 RVA: 0x0013383B File Offset: 0x00131A3B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7CD RID: 42957
			// (set) Token: 0x0600D898 RID: 55448 RVA: 0x00133859 File Offset: 0x00131A59
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A7CE RID: 42958
			// (set) Token: 0x0600D899 RID: 55449 RVA: 0x00133871 File Offset: 0x00131A71
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7CF RID: 42959
			// (set) Token: 0x0600D89A RID: 55450 RVA: 0x00133884 File Offset: 0x00131A84
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7D0 RID: 42960
			// (set) Token: 0x0600D89B RID: 55451 RVA: 0x0013389C File Offset: 0x00131A9C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7D1 RID: 42961
			// (set) Token: 0x0600D89C RID: 55452 RVA: 0x001338B4 File Offset: 0x00131AB4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7D2 RID: 42962
			// (set) Token: 0x0600D89D RID: 55453 RVA: 0x001338CC File Offset: 0x00131ACC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E42 RID: 3650
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A7D3 RID: 42963
			// (set) Token: 0x0600D89F RID: 55455 RVA: 0x001338EC File Offset: 0x00131AEC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AppIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7D4 RID: 42964
			// (set) Token: 0x0600D8A0 RID: 55456 RVA: 0x0013390A File Offset: 0x00131B0A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7D5 RID: 42965
			// (set) Token: 0x0600D8A1 RID: 55457 RVA: 0x00133928 File Offset: 0x00131B28
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7D6 RID: 42966
			// (set) Token: 0x0600D8A2 RID: 55458 RVA: 0x00133946 File Offset: 0x00131B46
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A7D7 RID: 42967
			// (set) Token: 0x0600D8A3 RID: 55459 RVA: 0x0013395E File Offset: 0x00131B5E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7D8 RID: 42968
			// (set) Token: 0x0600D8A4 RID: 55460 RVA: 0x00133971 File Offset: 0x00131B71
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7D9 RID: 42969
			// (set) Token: 0x0600D8A5 RID: 55461 RVA: 0x00133989 File Offset: 0x00131B89
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7DA RID: 42970
			// (set) Token: 0x0600D8A6 RID: 55462 RVA: 0x001339A1 File Offset: 0x00131BA1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7DB RID: 42971
			// (set) Token: 0x0600D8A7 RID: 55463 RVA: 0x001339B9 File Offset: 0x00131BB9
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
