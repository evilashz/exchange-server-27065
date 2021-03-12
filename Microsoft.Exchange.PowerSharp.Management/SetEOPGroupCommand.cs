using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C2B RID: 3115
	public class SetEOPGroupCommand : SyntheticCommand<object>
	{
		// Token: 0x06009800 RID: 38912 RVA: 0x000DD01D File Offset: 0x000DB21D
		private SetEOPGroupCommand() : base("Set-EOPGroup")
		{
		}

		// Token: 0x06009801 RID: 38913 RVA: 0x000DD02A File Offset: 0x000DB22A
		public SetEOPGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009802 RID: 38914 RVA: 0x000DD039 File Offset: 0x000DB239
		public virtual SetEOPGroupCommand SetParameters(SetEOPGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C2C RID: 3116
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B63 RID: 27491
			// (set) Token: 0x06009803 RID: 38915 RVA: 0x000DD043 File Offset: 0x000DB243
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x17006B64 RID: 27492
			// (set) Token: 0x06009804 RID: 38916 RVA: 0x000DD061 File Offset: 0x000DB261
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B65 RID: 27493
			// (set) Token: 0x06009805 RID: 38917 RVA: 0x000DD074 File Offset: 0x000DB274
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = value;
				}
			}

			// Token: 0x17006B66 RID: 27494
			// (set) Token: 0x06009806 RID: 38918 RVA: 0x000DD087 File Offset: 0x000DB287
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006B67 RID: 27495
			// (set) Token: 0x06009807 RID: 38919 RVA: 0x000DD09A File Offset: 0x000DB29A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B68 RID: 27496
			// (set) Token: 0x06009808 RID: 38920 RVA: 0x000DD0B8 File Offset: 0x000DB2B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B69 RID: 27497
			// (set) Token: 0x06009809 RID: 38921 RVA: 0x000DD0D0 File Offset: 0x000DB2D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B6A RID: 27498
			// (set) Token: 0x0600980A RID: 38922 RVA: 0x000DD0E8 File Offset: 0x000DB2E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B6B RID: 27499
			// (set) Token: 0x0600980B RID: 38923 RVA: 0x000DD100 File Offset: 0x000DB300
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
