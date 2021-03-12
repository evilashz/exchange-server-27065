using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DA8 RID: 3496
	public class RemoveSyncMailboxCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x0600C927 RID: 51495 RVA: 0x0011F531 File Offset: 0x0011D731
		private RemoveSyncMailboxCommand() : base("Remove-SyncMailbox")
		{
		}

		// Token: 0x0600C928 RID: 51496 RVA: 0x0011F53E File Offset: 0x0011D73E
		public RemoveSyncMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600C929 RID: 51497 RVA: 0x0011F54D File Offset: 0x0011D74D
		public virtual RemoveSyncMailboxCommand SetParameters(RemoveSyncMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C92A RID: 51498 RVA: 0x0011F557 File Offset: 0x0011D757
		public virtual RemoveSyncMailboxCommand SetParameters(RemoveSyncMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600C92B RID: 51499 RVA: 0x0011F561 File Offset: 0x0011D761
		public virtual RemoveSyncMailboxCommand SetParameters(RemoveSyncMailboxCommand.StoreMailboxIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DA9 RID: 3497
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17009990 RID: 39312
			// (set) Token: 0x0600C92C RID: 51500 RVA: 0x0011F56B File Offset: 0x0011D76B
			public virtual SwitchParameter DisableWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["DisableWindowsLiveID"] = value;
				}
			}

			// Token: 0x17009991 RID: 39313
			// (set) Token: 0x0600C92D RID: 51501 RVA: 0x0011F583 File Offset: 0x0011D783
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x17009992 RID: 39314
			// (set) Token: 0x0600C92E RID: 51502 RVA: 0x0011F59B File Offset: 0x0011D79B
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x17009993 RID: 39315
			// (set) Token: 0x0600C92F RID: 51503 RVA: 0x0011F5B3 File Offset: 0x0011D7B3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17009994 RID: 39316
			// (set) Token: 0x0600C930 RID: 51504 RVA: 0x0011F5D1 File Offset: 0x0011D7D1
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x17009995 RID: 39317
			// (set) Token: 0x0600C931 RID: 51505 RVA: 0x0011F5E9 File Offset: 0x0011D7E9
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x17009996 RID: 39318
			// (set) Token: 0x0600C932 RID: 51506 RVA: 0x0011F601 File Offset: 0x0011D801
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x17009997 RID: 39319
			// (set) Token: 0x0600C933 RID: 51507 RVA: 0x0011F619 File Offset: 0x0011D819
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x17009998 RID: 39320
			// (set) Token: 0x0600C934 RID: 51508 RVA: 0x0011F631 File Offset: 0x0011D831
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x17009999 RID: 39321
			// (set) Token: 0x0600C935 RID: 51509 RVA: 0x0011F649 File Offset: 0x0011D849
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x1700999A RID: 39322
			// (set) Token: 0x0600C936 RID: 51510 RVA: 0x0011F661 File Offset: 0x0011D861
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700999B RID: 39323
			// (set) Token: 0x0600C937 RID: 51511 RVA: 0x0011F679 File Offset: 0x0011D879
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x1700999C RID: 39324
			// (set) Token: 0x0600C938 RID: 51512 RVA: 0x0011F691 File Offset: 0x0011D891
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x1700999D RID: 39325
			// (set) Token: 0x0600C939 RID: 51513 RVA: 0x0011F6A9 File Offset: 0x0011D8A9
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700999E RID: 39326
			// (set) Token: 0x0600C93A RID: 51514 RVA: 0x0011F6C1 File Offset: 0x0011D8C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700999F RID: 39327
			// (set) Token: 0x0600C93B RID: 51515 RVA: 0x0011F6D4 File Offset: 0x0011D8D4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170099A0 RID: 39328
			// (set) Token: 0x0600C93C RID: 51516 RVA: 0x0011F6EC File Offset: 0x0011D8EC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170099A1 RID: 39329
			// (set) Token: 0x0600C93D RID: 51517 RVA: 0x0011F704 File Offset: 0x0011D904
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170099A2 RID: 39330
			// (set) Token: 0x0600C93E RID: 51518 RVA: 0x0011F71C File Offset: 0x0011D91C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170099A3 RID: 39331
			// (set) Token: 0x0600C93F RID: 51519 RVA: 0x0011F734 File Offset: 0x0011D934
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170099A4 RID: 39332
			// (set) Token: 0x0600C940 RID: 51520 RVA: 0x0011F74C File Offset: 0x0011D94C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000DAA RID: 3498
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170099A5 RID: 39333
			// (set) Token: 0x0600C942 RID: 51522 RVA: 0x0011F76C File Offset: 0x0011D96C
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x170099A6 RID: 39334
			// (set) Token: 0x0600C943 RID: 51523 RVA: 0x0011F784 File Offset: 0x0011D984
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170099A7 RID: 39335
			// (set) Token: 0x0600C944 RID: 51524 RVA: 0x0011F79C File Offset: 0x0011D99C
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170099A8 RID: 39336
			// (set) Token: 0x0600C945 RID: 51525 RVA: 0x0011F7B4 File Offset: 0x0011D9B4
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x170099A9 RID: 39337
			// (set) Token: 0x0600C946 RID: 51526 RVA: 0x0011F7CC File Offset: 0x0011D9CC
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x170099AA RID: 39338
			// (set) Token: 0x0600C947 RID: 51527 RVA: 0x0011F7E4 File Offset: 0x0011D9E4
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x170099AB RID: 39339
			// (set) Token: 0x0600C948 RID: 51528 RVA: 0x0011F7FC File Offset: 0x0011D9FC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170099AC RID: 39340
			// (set) Token: 0x0600C949 RID: 51529 RVA: 0x0011F814 File Offset: 0x0011DA14
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x170099AD RID: 39341
			// (set) Token: 0x0600C94A RID: 51530 RVA: 0x0011F82C File Offset: 0x0011DA2C
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170099AE RID: 39342
			// (set) Token: 0x0600C94B RID: 51531 RVA: 0x0011F844 File Offset: 0x0011DA44
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170099AF RID: 39343
			// (set) Token: 0x0600C94C RID: 51532 RVA: 0x0011F85C File Offset: 0x0011DA5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170099B0 RID: 39344
			// (set) Token: 0x0600C94D RID: 51533 RVA: 0x0011F86F File Offset: 0x0011DA6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170099B1 RID: 39345
			// (set) Token: 0x0600C94E RID: 51534 RVA: 0x0011F887 File Offset: 0x0011DA87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170099B2 RID: 39346
			// (set) Token: 0x0600C94F RID: 51535 RVA: 0x0011F89F File Offset: 0x0011DA9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170099B3 RID: 39347
			// (set) Token: 0x0600C950 RID: 51536 RVA: 0x0011F8B7 File Offset: 0x0011DAB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170099B4 RID: 39348
			// (set) Token: 0x0600C951 RID: 51537 RVA: 0x0011F8CF File Offset: 0x0011DACF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170099B5 RID: 39349
			// (set) Token: 0x0600C952 RID: 51538 RVA: 0x0011F8E7 File Offset: 0x0011DAE7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000DAB RID: 3499
		public class StoreMailboxIdentityParameters : ParametersBase
		{
			// Token: 0x170099B6 RID: 39350
			// (set) Token: 0x0600C954 RID: 51540 RVA: 0x0011F907 File Offset: 0x0011DB07
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170099B7 RID: 39351
			// (set) Token: 0x0600C955 RID: 51541 RVA: 0x0011F91A File Offset: 0x0011DB1A
			public virtual StoreMailboxIdParameter StoreMailboxIdentity
			{
				set
				{
					base.PowerSharpParameters["StoreMailboxIdentity"] = value;
				}
			}

			// Token: 0x170099B8 RID: 39352
			// (set) Token: 0x0600C956 RID: 51542 RVA: 0x0011F92D File Offset: 0x0011DB2D
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x170099B9 RID: 39353
			// (set) Token: 0x0600C957 RID: 51543 RVA: 0x0011F945 File Offset: 0x0011DB45
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x170099BA RID: 39354
			// (set) Token: 0x0600C958 RID: 51544 RVA: 0x0011F95D File Offset: 0x0011DB5D
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170099BB RID: 39355
			// (set) Token: 0x0600C959 RID: 51545 RVA: 0x0011F975 File Offset: 0x0011DB75
			public virtual SwitchParameter RemoveLastArbitrationMailboxAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveLastArbitrationMailboxAllowed"] = value;
				}
			}

			// Token: 0x170099BC RID: 39356
			// (set) Token: 0x0600C95A RID: 51546 RVA: 0x0011F98D File Offset: 0x0011DB8D
			public virtual SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
			{
				set
				{
					base.PowerSharpParameters["RemoveArbitrationMailboxWithOABsAllowed"] = value;
				}
			}

			// Token: 0x170099BD RID: 39357
			// (set) Token: 0x0600C95B RID: 51547 RVA: 0x0011F9A5 File Offset: 0x0011DBA5
			public virtual SwitchParameter IgnoreLegalHold
			{
				set
				{
					base.PowerSharpParameters["IgnoreLegalHold"] = value;
				}
			}

			// Token: 0x170099BE RID: 39358
			// (set) Token: 0x0600C95C RID: 51548 RVA: 0x0011F9BD File Offset: 0x0011DBBD
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170099BF RID: 39359
			// (set) Token: 0x0600C95D RID: 51549 RVA: 0x0011F9D5 File Offset: 0x0011DBD5
			public virtual SwitchParameter Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x170099C0 RID: 39360
			// (set) Token: 0x0600C95E RID: 51550 RVA: 0x0011F9ED File Offset: 0x0011DBED
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170099C1 RID: 39361
			// (set) Token: 0x0600C95F RID: 51551 RVA: 0x0011FA05 File Offset: 0x0011DC05
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170099C2 RID: 39362
			// (set) Token: 0x0600C960 RID: 51552 RVA: 0x0011FA1D File Offset: 0x0011DC1D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170099C3 RID: 39363
			// (set) Token: 0x0600C961 RID: 51553 RVA: 0x0011FA30 File Offset: 0x0011DC30
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170099C4 RID: 39364
			// (set) Token: 0x0600C962 RID: 51554 RVA: 0x0011FA48 File Offset: 0x0011DC48
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170099C5 RID: 39365
			// (set) Token: 0x0600C963 RID: 51555 RVA: 0x0011FA60 File Offset: 0x0011DC60
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170099C6 RID: 39366
			// (set) Token: 0x0600C964 RID: 51556 RVA: 0x0011FA78 File Offset: 0x0011DC78
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170099C7 RID: 39367
			// (set) Token: 0x0600C965 RID: 51557 RVA: 0x0011FA90 File Offset: 0x0011DC90
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170099C8 RID: 39368
			// (set) Token: 0x0600C966 RID: 51558 RVA: 0x0011FAA8 File Offset: 0x0011DCA8
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
