using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C29 RID: 3113
	public class SetEOPDistributionGroupCommand : SyntheticCommand<object>
	{
		// Token: 0x060097F1 RID: 38897 RVA: 0x000DCEEF File Offset: 0x000DB0EF
		private SetEOPDistributionGroupCommand() : base("Set-EOPDistributionGroup")
		{
		}

		// Token: 0x060097F2 RID: 38898 RVA: 0x000DCEFC File Offset: 0x000DB0FC
		public SetEOPDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060097F3 RID: 38899 RVA: 0x000DCF0B File Offset: 0x000DB10B
		public virtual SetEOPDistributionGroupCommand SetParameters(SetEOPDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C2A RID: 3114
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B58 RID: 27480
			// (set) Token: 0x060097F4 RID: 38900 RVA: 0x000DCF15 File Offset: 0x000DB115
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006B59 RID: 27481
			// (set) Token: 0x060097F5 RID: 38901 RVA: 0x000DCF33 File Offset: 0x000DB133
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B5A RID: 27482
			// (set) Token: 0x060097F6 RID: 38902 RVA: 0x000DCF46 File Offset: 0x000DB146
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17006B5B RID: 27483
			// (set) Token: 0x060097F7 RID: 38903 RVA: 0x000DCF59 File Offset: 0x000DB159
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006B5C RID: 27484
			// (set) Token: 0x060097F8 RID: 38904 RVA: 0x000DCF6C File Offset: 0x000DB16C
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006B5D RID: 27485
			// (set) Token: 0x060097F9 RID: 38905 RVA: 0x000DCF84 File Offset: 0x000DB184
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006B5E RID: 27486
			// (set) Token: 0x060097FA RID: 38906 RVA: 0x000DCF97 File Offset: 0x000DB197
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B5F RID: 27487
			// (set) Token: 0x060097FB RID: 38907 RVA: 0x000DCFB5 File Offset: 0x000DB1B5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B60 RID: 27488
			// (set) Token: 0x060097FC RID: 38908 RVA: 0x000DCFCD File Offset: 0x000DB1CD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B61 RID: 27489
			// (set) Token: 0x060097FD RID: 38909 RVA: 0x000DCFE5 File Offset: 0x000DB1E5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B62 RID: 27490
			// (set) Token: 0x060097FE RID: 38910 RVA: 0x000DCFFD File Offset: 0x000DB1FD
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
