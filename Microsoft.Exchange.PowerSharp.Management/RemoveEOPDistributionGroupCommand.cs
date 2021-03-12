using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C25 RID: 3109
	public class RemoveEOPDistributionGroupCommand : SyntheticCommand<object>
	{
		// Token: 0x060097DB RID: 38875 RVA: 0x000DCD35 File Offset: 0x000DAF35
		private RemoveEOPDistributionGroupCommand() : base("Remove-EOPDistributionGroup")
		{
		}

		// Token: 0x060097DC RID: 38876 RVA: 0x000DCD42 File Offset: 0x000DAF42
		public RemoveEOPDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060097DD RID: 38877 RVA: 0x000DCD51 File Offset: 0x000DAF51
		public virtual RemoveEOPDistributionGroupCommand SetParameters(RemoveEOPDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C26 RID: 3110
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B4A RID: 27466
			// (set) Token: 0x060097DE RID: 38878 RVA: 0x000DCD5B File Offset: 0x000DAF5B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006B4B RID: 27467
			// (set) Token: 0x060097DF RID: 38879 RVA: 0x000DCD79 File Offset: 0x000DAF79
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B4C RID: 27468
			// (set) Token: 0x060097E0 RID: 38880 RVA: 0x000DCD8C File Offset: 0x000DAF8C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B4D RID: 27469
			// (set) Token: 0x060097E1 RID: 38881 RVA: 0x000DCDAA File Offset: 0x000DAFAA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B4E RID: 27470
			// (set) Token: 0x060097E2 RID: 38882 RVA: 0x000DCDC2 File Offset: 0x000DAFC2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B4F RID: 27471
			// (set) Token: 0x060097E3 RID: 38883 RVA: 0x000DCDDA File Offset: 0x000DAFDA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B50 RID: 27472
			// (set) Token: 0x060097E4 RID: 38884 RVA: 0x000DCDF2 File Offset: 0x000DAFF2
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
