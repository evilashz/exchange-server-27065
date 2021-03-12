using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A6D RID: 2669
	public class RemovePublicFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMoveRequestIdParameter, PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x06008492 RID: 33938 RVA: 0x000C3DC1 File Offset: 0x000C1FC1
		private RemovePublicFolderMoveRequestCommand() : base("Remove-PublicFolderMoveRequest")
		{
		}

		// Token: 0x06008493 RID: 33939 RVA: 0x000C3DCE File Offset: 0x000C1FCE
		public RemovePublicFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008494 RID: 33940 RVA: 0x000C3DDD File Offset: 0x000C1FDD
		public virtual RemovePublicFolderMoveRequestCommand SetParameters(RemovePublicFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008495 RID: 33941 RVA: 0x000C3DE7 File Offset: 0x000C1FE7
		public virtual RemovePublicFolderMoveRequestCommand SetParameters(RemovePublicFolderMoveRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008496 RID: 33942 RVA: 0x000C3DF1 File Offset: 0x000C1FF1
		public virtual RemovePublicFolderMoveRequestCommand SetParameters(RemovePublicFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A6E RID: 2670
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005B71 RID: 23409
			// (set) Token: 0x06008497 RID: 33943 RVA: 0x000C3DFB File Offset: 0x000C1FFB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B72 RID: 23410
			// (set) Token: 0x06008498 RID: 33944 RVA: 0x000C3E19 File Offset: 0x000C2019
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B73 RID: 23411
			// (set) Token: 0x06008499 RID: 33945 RVA: 0x000C3E2C File Offset: 0x000C202C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B74 RID: 23412
			// (set) Token: 0x0600849A RID: 33946 RVA: 0x000C3E44 File Offset: 0x000C2044
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B75 RID: 23413
			// (set) Token: 0x0600849B RID: 33947 RVA: 0x000C3E5C File Offset: 0x000C205C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B76 RID: 23414
			// (set) Token: 0x0600849C RID: 33948 RVA: 0x000C3E74 File Offset: 0x000C2074
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B77 RID: 23415
			// (set) Token: 0x0600849D RID: 33949 RVA: 0x000C3E8C File Offset: 0x000C208C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005B78 RID: 23416
			// (set) Token: 0x0600849E RID: 33950 RVA: 0x000C3EA4 File Offset: 0x000C20A4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A6F RID: 2671
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005B79 RID: 23417
			// (set) Token: 0x060084A0 RID: 33952 RVA: 0x000C3EC4 File Offset: 0x000C20C4
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005B7A RID: 23418
			// (set) Token: 0x060084A1 RID: 33953 RVA: 0x000C3ED7 File Offset: 0x000C20D7
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005B7B RID: 23419
			// (set) Token: 0x060084A2 RID: 33954 RVA: 0x000C3EEF File Offset: 0x000C20EF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B7C RID: 23420
			// (set) Token: 0x060084A3 RID: 33955 RVA: 0x000C3F02 File Offset: 0x000C2102
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B7D RID: 23421
			// (set) Token: 0x060084A4 RID: 33956 RVA: 0x000C3F1A File Offset: 0x000C211A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B7E RID: 23422
			// (set) Token: 0x060084A5 RID: 33957 RVA: 0x000C3F32 File Offset: 0x000C2132
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B7F RID: 23423
			// (set) Token: 0x060084A6 RID: 33958 RVA: 0x000C3F4A File Offset: 0x000C214A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B80 RID: 23424
			// (set) Token: 0x060084A7 RID: 33959 RVA: 0x000C3F62 File Offset: 0x000C2162
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005B81 RID: 23425
			// (set) Token: 0x060084A8 RID: 33960 RVA: 0x000C3F7A File Offset: 0x000C217A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A70 RID: 2672
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005B82 RID: 23426
			// (set) Token: 0x060084AA RID: 33962 RVA: 0x000C3F9A File Offset: 0x000C219A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B83 RID: 23427
			// (set) Token: 0x060084AB RID: 33963 RVA: 0x000C3FAD File Offset: 0x000C21AD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B84 RID: 23428
			// (set) Token: 0x060084AC RID: 33964 RVA: 0x000C3FC5 File Offset: 0x000C21C5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B85 RID: 23429
			// (set) Token: 0x060084AD RID: 33965 RVA: 0x000C3FDD File Offset: 0x000C21DD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B86 RID: 23430
			// (set) Token: 0x060084AE RID: 33966 RVA: 0x000C3FF5 File Offset: 0x000C21F5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B87 RID: 23431
			// (set) Token: 0x060084AF RID: 33967 RVA: 0x000C400D File Offset: 0x000C220D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005B88 RID: 23432
			// (set) Token: 0x060084B0 RID: 33968 RVA: 0x000C4025 File Offset: 0x000C2225
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
