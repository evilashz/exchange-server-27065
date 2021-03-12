using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.PushNotifications;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000EB6 RID: 3766
	public class RemovePushNotificationSubscriptionCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600DCF5 RID: 56565 RVA: 0x0013928A File Offset: 0x0013748A
		private RemovePushNotificationSubscriptionCommand() : base("Remove-PushNotificationSubscription")
		{
		}

		// Token: 0x0600DCF6 RID: 56566 RVA: 0x00139297 File Offset: 0x00137497
		public RemovePushNotificationSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DCF7 RID: 56567 RVA: 0x001392A6 File Offset: 0x001374A6
		public virtual RemovePushNotificationSubscriptionCommand SetParameters(RemovePushNotificationSubscriptionCommand.RemoveStorageParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DCF8 RID: 56568 RVA: 0x001392B0 File Offset: 0x001374B0
		public virtual RemovePushNotificationSubscriptionCommand SetParameters(RemovePushNotificationSubscriptionCommand.RemoveAllParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DCF9 RID: 56569 RVA: 0x001392BA File Offset: 0x001374BA
		public virtual RemovePushNotificationSubscriptionCommand SetParameters(RemovePushNotificationSubscriptionCommand.IndividualRemoveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000EB7 RID: 3767
		public class RemoveStorageParameters : ParametersBase
		{
			// Token: 0x1700AB42 RID: 43842
			// (set) Token: 0x0600DCFA RID: 56570 RVA: 0x001392C4 File Offset: 0x001374C4
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AB43 RID: 43843
			// (set) Token: 0x0600DCFB RID: 56571 RVA: 0x001392E2 File Offset: 0x001374E2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700AB44 RID: 43844
			// (set) Token: 0x0600DCFC RID: 56572 RVA: 0x001392FA File Offset: 0x001374FA
			public virtual SwitchParameter RemoveStorage
			{
				set
				{
					base.PowerSharpParameters["RemoveStorage"] = value;
				}
			}

			// Token: 0x1700AB45 RID: 43845
			// (set) Token: 0x0600DCFD RID: 56573 RVA: 0x00139312 File Offset: 0x00137512
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB46 RID: 43846
			// (set) Token: 0x0600DCFE RID: 56574 RVA: 0x0013932A File Offset: 0x0013752A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB47 RID: 43847
			// (set) Token: 0x0600DCFF RID: 56575 RVA: 0x00139342 File Offset: 0x00137542
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB48 RID: 43848
			// (set) Token: 0x0600DD00 RID: 56576 RVA: 0x0013935A File Offset: 0x0013755A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB49 RID: 43849
			// (set) Token: 0x0600DD01 RID: 56577 RVA: 0x00139372 File Offset: 0x00137572
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700AB4A RID: 43850
			// (set) Token: 0x0600DD02 RID: 56578 RVA: 0x0013938A File Offset: 0x0013758A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000EB8 RID: 3768
		public class RemoveAllParameters : ParametersBase
		{
			// Token: 0x1700AB4B RID: 43851
			// (set) Token: 0x0600DD04 RID: 56580 RVA: 0x001393AA File Offset: 0x001375AA
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AB4C RID: 43852
			// (set) Token: 0x0600DD05 RID: 56581 RVA: 0x001393C8 File Offset: 0x001375C8
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700AB4D RID: 43853
			// (set) Token: 0x0600DD06 RID: 56582 RVA: 0x001393E0 File Offset: 0x001375E0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB4E RID: 43854
			// (set) Token: 0x0600DD07 RID: 56583 RVA: 0x001393F8 File Offset: 0x001375F8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB4F RID: 43855
			// (set) Token: 0x0600DD08 RID: 56584 RVA: 0x00139410 File Offset: 0x00137610
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB50 RID: 43856
			// (set) Token: 0x0600DD09 RID: 56585 RVA: 0x00139428 File Offset: 0x00137628
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB51 RID: 43857
			// (set) Token: 0x0600DD0A RID: 56586 RVA: 0x00139440 File Offset: 0x00137640
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700AB52 RID: 43858
			// (set) Token: 0x0600DD0B RID: 56587 RVA: 0x00139458 File Offset: 0x00137658
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000EB9 RID: 3769
		public class IndividualRemoveParameters : ParametersBase
		{
			// Token: 0x1700AB53 RID: 43859
			// (set) Token: 0x0600DD0D RID: 56589 RVA: 0x00139478 File Offset: 0x00137678
			public virtual PushNotificationStoreId SubscriptionStoreId
			{
				set
				{
					base.PowerSharpParameters["SubscriptionStoreId"] = value;
				}
			}

			// Token: 0x1700AB54 RID: 43860
			// (set) Token: 0x0600DD0E RID: 56590 RVA: 0x0013948B File Offset: 0x0013768B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB55 RID: 43861
			// (set) Token: 0x0600DD0F RID: 56591 RVA: 0x001394A3 File Offset: 0x001376A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB56 RID: 43862
			// (set) Token: 0x0600DD10 RID: 56592 RVA: 0x001394BB File Offset: 0x001376BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB57 RID: 43863
			// (set) Token: 0x0600DD11 RID: 56593 RVA: 0x001394D3 File Offset: 0x001376D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB58 RID: 43864
			// (set) Token: 0x0600DD12 RID: 56594 RVA: 0x001394EB File Offset: 0x001376EB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700AB59 RID: 43865
			// (set) Token: 0x0600DD13 RID: 56595 RVA: 0x00139503 File Offset: 0x00137703
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
