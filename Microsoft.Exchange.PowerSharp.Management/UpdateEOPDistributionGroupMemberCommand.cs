using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C31 RID: 3121
	public class UpdateEOPDistributionGroupMemberCommand : SyntheticCommand<object>
	{
		// Token: 0x0600983B RID: 38971 RVA: 0x000DD4A7 File Offset: 0x000DB6A7
		private UpdateEOPDistributionGroupMemberCommand() : base("Update-EOPDistributionGroupMember")
		{
		}

		// Token: 0x0600983C RID: 38972 RVA: 0x000DD4B4 File Offset: 0x000DB6B4
		public UpdateEOPDistributionGroupMemberCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600983D RID: 38973 RVA: 0x000DD4C3 File Offset: 0x000DB6C3
		public virtual UpdateEOPDistributionGroupMemberCommand SetParameters(UpdateEOPDistributionGroupMemberCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C32 RID: 3122
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B92 RID: 27538
			// (set) Token: 0x0600983E RID: 38974 RVA: 0x000DD4CD File Offset: 0x000DB6CD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006B93 RID: 27539
			// (set) Token: 0x0600983F RID: 38975 RVA: 0x000DD4EB File Offset: 0x000DB6EB
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B94 RID: 27540
			// (set) Token: 0x06009840 RID: 38976 RVA: 0x000DD4FE File Offset: 0x000DB6FE
			public virtual string Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17006B95 RID: 27541
			// (set) Token: 0x06009841 RID: 38977 RVA: 0x000DD511 File Offset: 0x000DB711
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B96 RID: 27542
			// (set) Token: 0x06009842 RID: 38978 RVA: 0x000DD52F File Offset: 0x000DB72F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B97 RID: 27543
			// (set) Token: 0x06009843 RID: 38979 RVA: 0x000DD547 File Offset: 0x000DB747
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B98 RID: 27544
			// (set) Token: 0x06009844 RID: 38980 RVA: 0x000DD55F File Offset: 0x000DB75F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B99 RID: 27545
			// (set) Token: 0x06009845 RID: 38981 RVA: 0x000DD577 File Offset: 0x000DB777
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
