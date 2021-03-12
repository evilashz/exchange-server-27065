using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CDC RID: 3292
	public class UndoSoftDeletedMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxIdParameter>
	{
		// Token: 0x0600AD07 RID: 44295 RVA: 0x000FA286 File Offset: 0x000F8486
		private UndoSoftDeletedMailboxCommand() : base("Undo-SoftDeletedMailbox")
		{
		}

		// Token: 0x0600AD08 RID: 44296 RVA: 0x000FA293 File Offset: 0x000F8493
		public UndoSoftDeletedMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AD09 RID: 44297 RVA: 0x000FA2A2 File Offset: 0x000F84A2
		public virtual UndoSoftDeletedMailboxCommand SetParameters(UndoSoftDeletedMailboxCommand.SoftDeletedMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD0A RID: 44298 RVA: 0x000FA2AC File Offset: 0x000F84AC
		public virtual UndoSoftDeletedMailboxCommand SetParameters(UndoSoftDeletedMailboxCommand.EnableRoomMailboxAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD0B RID: 44299 RVA: 0x000FA2B6 File Offset: 0x000F84B6
		public virtual UndoSoftDeletedMailboxCommand SetParameters(UndoSoftDeletedMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD0C RID: 44300 RVA: 0x000FA2C0 File Offset: 0x000F84C0
		public virtual UndoSoftDeletedMailboxCommand SetParameters(UndoSoftDeletedMailboxCommand.LinkedRoomMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD0D RID: 44301 RVA: 0x000FA2CA File Offset: 0x000F84CA
		public virtual UndoSoftDeletedMailboxCommand SetParameters(UndoSoftDeletedMailboxCommand.AuxMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD0E RID: 44302 RVA: 0x000FA2D4 File Offset: 0x000F84D4
		public virtual UndoSoftDeletedMailboxCommand SetParameters(UndoSoftDeletedMailboxCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AD0F RID: 44303 RVA: 0x000FA2DE File Offset: 0x000F84DE
		public virtual UndoSoftDeletedMailboxCommand SetParameters(UndoSoftDeletedMailboxCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CDD RID: 3293
		public class SoftDeletedMailboxParameters : ParametersBase
		{
			// Token: 0x17007F08 RID: 32520
			// (set) Token: 0x0600AD10 RID: 44304 RVA: 0x000FA2E8 File Offset: 0x000F84E8
			public virtual string SoftDeletedObject
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedObject"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007F09 RID: 32521
			// (set) Token: 0x0600AD11 RID: 44305 RVA: 0x000FA306 File Offset: 0x000F8506
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007F0A RID: 32522
			// (set) Token: 0x0600AD12 RID: 44306 RVA: 0x000FA319 File Offset: 0x000F8519
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17007F0B RID: 32523
			// (set) Token: 0x0600AD13 RID: 44307 RVA: 0x000FA32C File Offset: 0x000F852C
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007F0C RID: 32524
			// (set) Token: 0x0600AD14 RID: 44308 RVA: 0x000FA33F File Offset: 0x000F853F
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007F0D RID: 32525
			// (set) Token: 0x0600AD15 RID: 44309 RVA: 0x000FA352 File Offset: 0x000F8552
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007F0E RID: 32526
			// (set) Token: 0x0600AD16 RID: 44310 RVA: 0x000FA365 File Offset: 0x000F8565
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007F0F RID: 32527
			// (set) Token: 0x0600AD17 RID: 44311 RVA: 0x000FA378 File Offset: 0x000F8578
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F10 RID: 32528
			// (set) Token: 0x0600AD18 RID: 44312 RVA: 0x000FA38B File Offset: 0x000F858B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F11 RID: 32529
			// (set) Token: 0x0600AD19 RID: 44313 RVA: 0x000FA3A9 File Offset: 0x000F85A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F12 RID: 32530
			// (set) Token: 0x0600AD1A RID: 44314 RVA: 0x000FA3BC File Offset: 0x000F85BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F13 RID: 32531
			// (set) Token: 0x0600AD1B RID: 44315 RVA: 0x000FA3D4 File Offset: 0x000F85D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F14 RID: 32532
			// (set) Token: 0x0600AD1C RID: 44316 RVA: 0x000FA3EC File Offset: 0x000F85EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F15 RID: 32533
			// (set) Token: 0x0600AD1D RID: 44317 RVA: 0x000FA404 File Offset: 0x000F8604
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F16 RID: 32534
			// (set) Token: 0x0600AD1E RID: 44318 RVA: 0x000FA41C File Offset: 0x000F861C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CDE RID: 3294
		public class EnableRoomMailboxAccountParameters : ParametersBase
		{
			// Token: 0x17007F17 RID: 32535
			// (set) Token: 0x0600AD20 RID: 44320 RVA: 0x000FA43C File Offset: 0x000F863C
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x17007F18 RID: 32536
			// (set) Token: 0x0600AD21 RID: 44321 RVA: 0x000FA44F File Offset: 0x000F864F
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x17007F19 RID: 32537
			// (set) Token: 0x0600AD22 RID: 44322 RVA: 0x000FA467 File Offset: 0x000F8667
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007F1A RID: 32538
			// (set) Token: 0x0600AD23 RID: 44323 RVA: 0x000FA47A File Offset: 0x000F867A
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007F1B RID: 32539
			// (set) Token: 0x0600AD24 RID: 44324 RVA: 0x000FA48D File Offset: 0x000F868D
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007F1C RID: 32540
			// (set) Token: 0x0600AD25 RID: 44325 RVA: 0x000FA4A0 File Offset: 0x000F86A0
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F1D RID: 32541
			// (set) Token: 0x0600AD26 RID: 44326 RVA: 0x000FA4B3 File Offset: 0x000F86B3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F1E RID: 32542
			// (set) Token: 0x0600AD27 RID: 44327 RVA: 0x000FA4D1 File Offset: 0x000F86D1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F1F RID: 32543
			// (set) Token: 0x0600AD28 RID: 44328 RVA: 0x000FA4E4 File Offset: 0x000F86E4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F20 RID: 32544
			// (set) Token: 0x0600AD29 RID: 44329 RVA: 0x000FA4FC File Offset: 0x000F86FC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F21 RID: 32545
			// (set) Token: 0x0600AD2A RID: 44330 RVA: 0x000FA514 File Offset: 0x000F8714
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F22 RID: 32546
			// (set) Token: 0x0600AD2B RID: 44331 RVA: 0x000FA52C File Offset: 0x000F872C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F23 RID: 32547
			// (set) Token: 0x0600AD2C RID: 44332 RVA: 0x000FA544 File Offset: 0x000F8744
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CDF RID: 3295
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007F24 RID: 32548
			// (set) Token: 0x0600AD2E RID: 44334 RVA: 0x000FA564 File Offset: 0x000F8764
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007F25 RID: 32549
			// (set) Token: 0x0600AD2F RID: 44335 RVA: 0x000FA577 File Offset: 0x000F8777
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007F26 RID: 32550
			// (set) Token: 0x0600AD30 RID: 44336 RVA: 0x000FA58A File Offset: 0x000F878A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007F27 RID: 32551
			// (set) Token: 0x0600AD31 RID: 44337 RVA: 0x000FA59D File Offset: 0x000F879D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F28 RID: 32552
			// (set) Token: 0x0600AD32 RID: 44338 RVA: 0x000FA5B0 File Offset: 0x000F87B0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F29 RID: 32553
			// (set) Token: 0x0600AD33 RID: 44339 RVA: 0x000FA5CE File Offset: 0x000F87CE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F2A RID: 32554
			// (set) Token: 0x0600AD34 RID: 44340 RVA: 0x000FA5E1 File Offset: 0x000F87E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F2B RID: 32555
			// (set) Token: 0x0600AD35 RID: 44341 RVA: 0x000FA5F9 File Offset: 0x000F87F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F2C RID: 32556
			// (set) Token: 0x0600AD36 RID: 44342 RVA: 0x000FA611 File Offset: 0x000F8811
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F2D RID: 32557
			// (set) Token: 0x0600AD37 RID: 44343 RVA: 0x000FA629 File Offset: 0x000F8829
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F2E RID: 32558
			// (set) Token: 0x0600AD38 RID: 44344 RVA: 0x000FA641 File Offset: 0x000F8841
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CE0 RID: 3296
		public class LinkedRoomMailboxParameters : ParametersBase
		{
			// Token: 0x17007F2F RID: 32559
			// (set) Token: 0x0600AD3A RID: 44346 RVA: 0x000FA661 File Offset: 0x000F8861
			public virtual SwitchParameter LinkedRoom
			{
				set
				{
					base.PowerSharpParameters["LinkedRoom"] = value;
				}
			}

			// Token: 0x17007F30 RID: 32560
			// (set) Token: 0x0600AD3B RID: 44347 RVA: 0x000FA679 File Offset: 0x000F8879
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007F31 RID: 32561
			// (set) Token: 0x0600AD3C RID: 44348 RVA: 0x000FA68C File Offset: 0x000F888C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007F32 RID: 32562
			// (set) Token: 0x0600AD3D RID: 44349 RVA: 0x000FA69F File Offset: 0x000F889F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007F33 RID: 32563
			// (set) Token: 0x0600AD3E RID: 44350 RVA: 0x000FA6B2 File Offset: 0x000F88B2
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F34 RID: 32564
			// (set) Token: 0x0600AD3F RID: 44351 RVA: 0x000FA6C5 File Offset: 0x000F88C5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F35 RID: 32565
			// (set) Token: 0x0600AD40 RID: 44352 RVA: 0x000FA6E3 File Offset: 0x000F88E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F36 RID: 32566
			// (set) Token: 0x0600AD41 RID: 44353 RVA: 0x000FA6F6 File Offset: 0x000F88F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F37 RID: 32567
			// (set) Token: 0x0600AD42 RID: 44354 RVA: 0x000FA70E File Offset: 0x000F890E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F38 RID: 32568
			// (set) Token: 0x0600AD43 RID: 44355 RVA: 0x000FA726 File Offset: 0x000F8926
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F39 RID: 32569
			// (set) Token: 0x0600AD44 RID: 44356 RVA: 0x000FA73E File Offset: 0x000F893E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F3A RID: 32570
			// (set) Token: 0x0600AD45 RID: 44357 RVA: 0x000FA756 File Offset: 0x000F8956
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CE1 RID: 3297
		public class AuxMailboxParameters : ParametersBase
		{
			// Token: 0x17007F3B RID: 32571
			// (set) Token: 0x0600AD47 RID: 44359 RVA: 0x000FA776 File Offset: 0x000F8976
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17007F3C RID: 32572
			// (set) Token: 0x0600AD48 RID: 44360 RVA: 0x000FA78E File Offset: 0x000F898E
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007F3D RID: 32573
			// (set) Token: 0x0600AD49 RID: 44361 RVA: 0x000FA7A1 File Offset: 0x000F89A1
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007F3E RID: 32574
			// (set) Token: 0x0600AD4A RID: 44362 RVA: 0x000FA7B4 File Offset: 0x000F89B4
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007F3F RID: 32575
			// (set) Token: 0x0600AD4B RID: 44363 RVA: 0x000FA7C7 File Offset: 0x000F89C7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F40 RID: 32576
			// (set) Token: 0x0600AD4C RID: 44364 RVA: 0x000FA7DA File Offset: 0x000F89DA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F41 RID: 32577
			// (set) Token: 0x0600AD4D RID: 44365 RVA: 0x000FA7F8 File Offset: 0x000F89F8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F42 RID: 32578
			// (set) Token: 0x0600AD4E RID: 44366 RVA: 0x000FA80B File Offset: 0x000F8A0B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F43 RID: 32579
			// (set) Token: 0x0600AD4F RID: 44367 RVA: 0x000FA823 File Offset: 0x000F8A23
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F44 RID: 32580
			// (set) Token: 0x0600AD50 RID: 44368 RVA: 0x000FA83B File Offset: 0x000F8A3B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F45 RID: 32581
			// (set) Token: 0x0600AD51 RID: 44369 RVA: 0x000FA853 File Offset: 0x000F8A53
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F46 RID: 32582
			// (set) Token: 0x0600AD52 RID: 44370 RVA: 0x000FA86B File Offset: 0x000F8A6B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CE2 RID: 3298
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x17007F47 RID: 32583
			// (set) Token: 0x0600AD54 RID: 44372 RVA: 0x000FA88B File Offset: 0x000F8A8B
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x17007F48 RID: 32584
			// (set) Token: 0x0600AD55 RID: 44373 RVA: 0x000FA8A3 File Offset: 0x000F8AA3
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007F49 RID: 32585
			// (set) Token: 0x0600AD56 RID: 44374 RVA: 0x000FA8B6 File Offset: 0x000F8AB6
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007F4A RID: 32586
			// (set) Token: 0x0600AD57 RID: 44375 RVA: 0x000FA8C9 File Offset: 0x000F8AC9
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007F4B RID: 32587
			// (set) Token: 0x0600AD58 RID: 44376 RVA: 0x000FA8DC File Offset: 0x000F8ADC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F4C RID: 32588
			// (set) Token: 0x0600AD59 RID: 44377 RVA: 0x000FA8EF File Offset: 0x000F8AEF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F4D RID: 32589
			// (set) Token: 0x0600AD5A RID: 44378 RVA: 0x000FA90D File Offset: 0x000F8B0D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F4E RID: 32590
			// (set) Token: 0x0600AD5B RID: 44379 RVA: 0x000FA920 File Offset: 0x000F8B20
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F4F RID: 32591
			// (set) Token: 0x0600AD5C RID: 44380 RVA: 0x000FA938 File Offset: 0x000F8B38
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F50 RID: 32592
			// (set) Token: 0x0600AD5D RID: 44381 RVA: 0x000FA950 File Offset: 0x000F8B50
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F51 RID: 32593
			// (set) Token: 0x0600AD5E RID: 44382 RVA: 0x000FA968 File Offset: 0x000F8B68
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F52 RID: 32594
			// (set) Token: 0x0600AD5F RID: 44383 RVA: 0x000FA980 File Offset: 0x000F8B80
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CE3 RID: 3299
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x17007F53 RID: 32595
			// (set) Token: 0x0600AD61 RID: 44385 RVA: 0x000FA9A0 File Offset: 0x000F8BA0
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17007F54 RID: 32596
			// (set) Token: 0x0600AD62 RID: 44386 RVA: 0x000FA9B8 File Offset: 0x000F8BB8
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007F55 RID: 32597
			// (set) Token: 0x0600AD63 RID: 44387 RVA: 0x000FA9CB File Offset: 0x000F8BCB
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007F56 RID: 32598
			// (set) Token: 0x0600AD64 RID: 44388 RVA: 0x000FA9DE File Offset: 0x000F8BDE
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007F57 RID: 32599
			// (set) Token: 0x0600AD65 RID: 44389 RVA: 0x000FA9F1 File Offset: 0x000F8BF1
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007F58 RID: 32600
			// (set) Token: 0x0600AD66 RID: 44390 RVA: 0x000FAA04 File Offset: 0x000F8C04
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007F59 RID: 32601
			// (set) Token: 0x0600AD67 RID: 44391 RVA: 0x000FAA22 File Offset: 0x000F8C22
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007F5A RID: 32602
			// (set) Token: 0x0600AD68 RID: 44392 RVA: 0x000FAA35 File Offset: 0x000F8C35
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007F5B RID: 32603
			// (set) Token: 0x0600AD69 RID: 44393 RVA: 0x000FAA4D File Offset: 0x000F8C4D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007F5C RID: 32604
			// (set) Token: 0x0600AD6A RID: 44394 RVA: 0x000FAA65 File Offset: 0x000F8C65
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007F5D RID: 32605
			// (set) Token: 0x0600AD6B RID: 44395 RVA: 0x000FAA7D File Offset: 0x000F8C7D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007F5E RID: 32606
			// (set) Token: 0x0600AD6C RID: 44396 RVA: 0x000FAA95 File Offset: 0x000F8C95
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
