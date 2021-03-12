using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AC4 RID: 2756
	public class ResumeSyncRequestCommand : SyntheticCommandWithPipelineInput<SyncRequestIdParameter, SyncRequestIdParameter>
	{
		// Token: 0x06008884 RID: 34948 RVA: 0x000C9067 File Offset: 0x000C7267
		private ResumeSyncRequestCommand() : base("Resume-SyncRequest")
		{
		}

		// Token: 0x06008885 RID: 34949 RVA: 0x000C9074 File Offset: 0x000C7274
		public ResumeSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008886 RID: 34950 RVA: 0x000C9083 File Offset: 0x000C7283
		public virtual ResumeSyncRequestCommand SetParameters(ResumeSyncRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008887 RID: 34951 RVA: 0x000C908D File Offset: 0x000C728D
		public virtual ResumeSyncRequestCommand SetParameters(ResumeSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AC5 RID: 2757
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005EB5 RID: 24245
			// (set) Token: 0x06008888 RID: 34952 RVA: 0x000C9097 File Offset: 0x000C7297
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005EB6 RID: 24246
			// (set) Token: 0x06008889 RID: 34953 RVA: 0x000C90B5 File Offset: 0x000C72B5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EB7 RID: 24247
			// (set) Token: 0x0600888A RID: 34954 RVA: 0x000C90C8 File Offset: 0x000C72C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EB8 RID: 24248
			// (set) Token: 0x0600888B RID: 34955 RVA: 0x000C90E0 File Offset: 0x000C72E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EB9 RID: 24249
			// (set) Token: 0x0600888C RID: 34956 RVA: 0x000C90F8 File Offset: 0x000C72F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EBA RID: 24250
			// (set) Token: 0x0600888D RID: 34957 RVA: 0x000C9110 File Offset: 0x000C7310
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EBB RID: 24251
			// (set) Token: 0x0600888E RID: 34958 RVA: 0x000C9128 File Offset: 0x000C7328
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AC6 RID: 2758
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005EBC RID: 24252
			// (set) Token: 0x06008890 RID: 34960 RVA: 0x000C9148 File Offset: 0x000C7348
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EBD RID: 24253
			// (set) Token: 0x06008891 RID: 34961 RVA: 0x000C915B File Offset: 0x000C735B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EBE RID: 24254
			// (set) Token: 0x06008892 RID: 34962 RVA: 0x000C9173 File Offset: 0x000C7373
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EBF RID: 24255
			// (set) Token: 0x06008893 RID: 34963 RVA: 0x000C918B File Offset: 0x000C738B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EC0 RID: 24256
			// (set) Token: 0x06008894 RID: 34964 RVA: 0x000C91A3 File Offset: 0x000C73A3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EC1 RID: 24257
			// (set) Token: 0x06008895 RID: 34965 RVA: 0x000C91BB File Offset: 0x000C73BB
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
