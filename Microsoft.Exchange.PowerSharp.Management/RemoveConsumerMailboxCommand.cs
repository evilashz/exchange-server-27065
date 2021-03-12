using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BE5 RID: 3045
	public class RemoveConsumerMailboxCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009362 RID: 37730 RVA: 0x000D70EF File Offset: 0x000D52EF
		private RemoveConsumerMailboxCommand() : base("Remove-ConsumerMailbox")
		{
		}

		// Token: 0x06009363 RID: 37731 RVA: 0x000D70FC File Offset: 0x000D52FC
		public RemoveConsumerMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009364 RID: 37732 RVA: 0x000D710B File Offset: 0x000D530B
		public virtual RemoveConsumerMailboxCommand SetParameters(RemoveConsumerMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009365 RID: 37733 RVA: 0x000D7115 File Offset: 0x000D5315
		public virtual RemoveConsumerMailboxCommand SetParameters(RemoveConsumerMailboxCommand.ConsumerSecondaryMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009366 RID: 37734 RVA: 0x000D711F File Offset: 0x000D531F
		public virtual RemoveConsumerMailboxCommand SetParameters(RemoveConsumerMailboxCommand.ConsumerPrimaryMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BE6 RID: 3046
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006751 RID: 26449
			// (set) Token: 0x06009367 RID: 37735 RVA: 0x000D7129 File Offset: 0x000D5329
			public virtual ConsumerMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006752 RID: 26450
			// (set) Token: 0x06009368 RID: 37736 RVA: 0x000D713C File Offset: 0x000D533C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006753 RID: 26451
			// (set) Token: 0x06009369 RID: 37737 RVA: 0x000D7154 File Offset: 0x000D5354
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006754 RID: 26452
			// (set) Token: 0x0600936A RID: 37738 RVA: 0x000D716C File Offset: 0x000D536C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006755 RID: 26453
			// (set) Token: 0x0600936B RID: 37739 RVA: 0x000D7184 File Offset: 0x000D5384
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006756 RID: 26454
			// (set) Token: 0x0600936C RID: 37740 RVA: 0x000D719C File Offset: 0x000D539C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006757 RID: 26455
			// (set) Token: 0x0600936D RID: 37741 RVA: 0x000D71B4 File Offset: 0x000D53B4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BE7 RID: 3047
		public class ConsumerSecondaryMailboxParameters : ParametersBase
		{
			// Token: 0x17006758 RID: 26456
			// (set) Token: 0x0600936F RID: 37743 RVA: 0x000D71D4 File Offset: 0x000D53D4
			public virtual ConsumerMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006759 RID: 26457
			// (set) Token: 0x06009370 RID: 37744 RVA: 0x000D71E7 File Offset: 0x000D53E7
			public virtual SwitchParameter RemoveExoSecondary
			{
				set
				{
					base.PowerSharpParameters["RemoveExoSecondary"] = value;
				}
			}

			// Token: 0x1700675A RID: 26458
			// (set) Token: 0x06009371 RID: 37745 RVA: 0x000D71FF File Offset: 0x000D53FF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700675B RID: 26459
			// (set) Token: 0x06009372 RID: 37746 RVA: 0x000D7217 File Offset: 0x000D5417
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700675C RID: 26460
			// (set) Token: 0x06009373 RID: 37747 RVA: 0x000D722F File Offset: 0x000D542F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700675D RID: 26461
			// (set) Token: 0x06009374 RID: 37748 RVA: 0x000D7247 File Offset: 0x000D5447
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700675E RID: 26462
			// (set) Token: 0x06009375 RID: 37749 RVA: 0x000D725F File Offset: 0x000D545F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700675F RID: 26463
			// (set) Token: 0x06009376 RID: 37750 RVA: 0x000D7277 File Offset: 0x000D5477
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BE8 RID: 3048
		public class ConsumerPrimaryMailboxParameters : ParametersBase
		{
			// Token: 0x17006760 RID: 26464
			// (set) Token: 0x06009378 RID: 37752 RVA: 0x000D7297 File Offset: 0x000D5497
			public virtual ConsumerMailboxIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006761 RID: 26465
			// (set) Token: 0x06009379 RID: 37753 RVA: 0x000D72AA File Offset: 0x000D54AA
			public virtual SwitchParameter RemoveExoPrimary
			{
				set
				{
					base.PowerSharpParameters["RemoveExoPrimary"] = value;
				}
			}

			// Token: 0x17006762 RID: 26466
			// (set) Token: 0x0600937A RID: 37754 RVA: 0x000D72C2 File Offset: 0x000D54C2
			public virtual SwitchParameter SwitchToSecondary
			{
				set
				{
					base.PowerSharpParameters["SwitchToSecondary"] = value;
				}
			}

			// Token: 0x17006763 RID: 26467
			// (set) Token: 0x0600937B RID: 37755 RVA: 0x000D72DA File Offset: 0x000D54DA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006764 RID: 26468
			// (set) Token: 0x0600937C RID: 37756 RVA: 0x000D72F2 File Offset: 0x000D54F2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006765 RID: 26469
			// (set) Token: 0x0600937D RID: 37757 RVA: 0x000D730A File Offset: 0x000D550A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006766 RID: 26470
			// (set) Token: 0x0600937E RID: 37758 RVA: 0x000D7322 File Offset: 0x000D5522
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006767 RID: 26471
			// (set) Token: 0x0600937F RID: 37759 RVA: 0x000D733A File Offset: 0x000D553A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006768 RID: 26472
			// (set) Token: 0x06009380 RID: 37760 RVA: 0x000D7352 File Offset: 0x000D5552
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
