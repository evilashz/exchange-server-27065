using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C27 RID: 3111
	public class RemoveEOPMailUserCommand : SyntheticCommand<object>
	{
		// Token: 0x060097E6 RID: 38886 RVA: 0x000DCE12 File Offset: 0x000DB012
		private RemoveEOPMailUserCommand() : base("Remove-EOPMailUser")
		{
		}

		// Token: 0x060097E7 RID: 38887 RVA: 0x000DCE1F File Offset: 0x000DB01F
		public RemoveEOPMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060097E8 RID: 38888 RVA: 0x000DCE2E File Offset: 0x000DB02E
		public virtual RemoveEOPMailUserCommand SetParameters(RemoveEOPMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C28 RID: 3112
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B51 RID: 27473
			// (set) Token: 0x060097E9 RID: 38889 RVA: 0x000DCE38 File Offset: 0x000DB038
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17006B52 RID: 27474
			// (set) Token: 0x060097EA RID: 38890 RVA: 0x000DCE56 File Offset: 0x000DB056
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B53 RID: 27475
			// (set) Token: 0x060097EB RID: 38891 RVA: 0x000DCE69 File Offset: 0x000DB069
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B54 RID: 27476
			// (set) Token: 0x060097EC RID: 38892 RVA: 0x000DCE87 File Offset: 0x000DB087
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B55 RID: 27477
			// (set) Token: 0x060097ED RID: 38893 RVA: 0x000DCE9F File Offset: 0x000DB09F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B56 RID: 27478
			// (set) Token: 0x060097EE RID: 38894 RVA: 0x000DCEB7 File Offset: 0x000DB0B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B57 RID: 27479
			// (set) Token: 0x060097EF RID: 38895 RVA: 0x000DCECF File Offset: 0x000DB0CF
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
