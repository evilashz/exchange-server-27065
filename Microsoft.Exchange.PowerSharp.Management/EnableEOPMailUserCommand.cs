using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C1F RID: 3103
	public class EnableEOPMailUserCommand : SyntheticCommand<object>
	{
		// Token: 0x060097AC RID: 38828 RVA: 0x000DC99B File Offset: 0x000DAB9B
		private EnableEOPMailUserCommand() : base("Enable-EOPMailUser")
		{
		}

		// Token: 0x060097AD RID: 38829 RVA: 0x000DC9A8 File Offset: 0x000DABA8
		public EnableEOPMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060097AE RID: 38830 RVA: 0x000DC9B7 File Offset: 0x000DABB7
		public virtual EnableEOPMailUserCommand SetParameters(EnableEOPMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C20 RID: 3104
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B27 RID: 27431
			// (set) Token: 0x060097AF RID: 38831 RVA: 0x000DC9C1 File Offset: 0x000DABC1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006B28 RID: 27432
			// (set) Token: 0x060097B0 RID: 38832 RVA: 0x000DC9DF File Offset: 0x000DABDF
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B29 RID: 27433
			// (set) Token: 0x060097B1 RID: 38833 RVA: 0x000DC9F2 File Offset: 0x000DABF2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B2A RID: 27434
			// (set) Token: 0x060097B2 RID: 38834 RVA: 0x000DCA10 File Offset: 0x000DAC10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B2B RID: 27435
			// (set) Token: 0x060097B3 RID: 38835 RVA: 0x000DCA28 File Offset: 0x000DAC28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B2C RID: 27436
			// (set) Token: 0x060097B4 RID: 38836 RVA: 0x000DCA40 File Offset: 0x000DAC40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B2D RID: 27437
			// (set) Token: 0x060097B5 RID: 38837 RVA: 0x000DCA58 File Offset: 0x000DAC58
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
