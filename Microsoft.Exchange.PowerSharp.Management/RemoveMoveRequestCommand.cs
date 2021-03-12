using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009DA RID: 2522
	public class RemoveMoveRequestCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseIdParameter>
	{
		// Token: 0x06007EB6 RID: 32438 RVA: 0x000BC4A9 File Offset: 0x000BA6A9
		private RemoveMoveRequestCommand() : base("Remove-MoveRequest")
		{
		}

		// Token: 0x06007EB7 RID: 32439 RVA: 0x000BC4B6 File Offset: 0x000BA6B6
		public RemoveMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x000BC4C5 File Offset: 0x000BA6C5
		public virtual RemoveMoveRequestCommand SetParameters(RemoveMoveRequestCommand.MigrationMoveRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x000BC4CF File Offset: 0x000BA6CF
		public virtual RemoveMoveRequestCommand SetParameters(RemoveMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x000BC4D9 File Offset: 0x000BA6D9
		public virtual RemoveMoveRequestCommand SetParameters(RemoveMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009DB RID: 2523
		public class MigrationMoveRequestQueueParameters : ParametersBase
		{
			// Token: 0x170056BB RID: 22203
			// (set) Token: 0x06007EBB RID: 32443 RVA: 0x000BC4E3 File Offset: 0x000BA6E3
			public virtual DatabaseIdParameter MoveRequestQueue
			{
				set
				{
					base.PowerSharpParameters["MoveRequestQueue"] = value;
				}
			}

			// Token: 0x170056BC RID: 22204
			// (set) Token: 0x06007EBC RID: 32444 RVA: 0x000BC4F6 File Offset: 0x000BA6F6
			public virtual Guid MailboxGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxGuid"] = value;
				}
			}

			// Token: 0x170056BD RID: 22205
			// (set) Token: 0x06007EBD RID: 32445 RVA: 0x000BC50E File Offset: 0x000BA70E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170056BE RID: 22206
			// (set) Token: 0x06007EBE RID: 32446 RVA: 0x000BC521 File Offset: 0x000BA721
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170056BF RID: 22207
			// (set) Token: 0x06007EBF RID: 32447 RVA: 0x000BC539 File Offset: 0x000BA739
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170056C0 RID: 22208
			// (set) Token: 0x06007EC0 RID: 32448 RVA: 0x000BC551 File Offset: 0x000BA751
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170056C1 RID: 22209
			// (set) Token: 0x06007EC1 RID: 32449 RVA: 0x000BC569 File Offset: 0x000BA769
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170056C2 RID: 22210
			// (set) Token: 0x06007EC2 RID: 32450 RVA: 0x000BC581 File Offset: 0x000BA781
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170056C3 RID: 22211
			// (set) Token: 0x06007EC3 RID: 32451 RVA: 0x000BC599 File Offset: 0x000BA799
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009DC RID: 2524
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170056C4 RID: 22212
			// (set) Token: 0x06007EC5 RID: 32453 RVA: 0x000BC5B9 File Offset: 0x000BA7B9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170056C5 RID: 22213
			// (set) Token: 0x06007EC6 RID: 32454 RVA: 0x000BC5D7 File Offset: 0x000BA7D7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170056C6 RID: 22214
			// (set) Token: 0x06007EC7 RID: 32455 RVA: 0x000BC5EA File Offset: 0x000BA7EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170056C7 RID: 22215
			// (set) Token: 0x06007EC8 RID: 32456 RVA: 0x000BC602 File Offset: 0x000BA802
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170056C8 RID: 22216
			// (set) Token: 0x06007EC9 RID: 32457 RVA: 0x000BC61A File Offset: 0x000BA81A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170056C9 RID: 22217
			// (set) Token: 0x06007ECA RID: 32458 RVA: 0x000BC632 File Offset: 0x000BA832
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170056CA RID: 22218
			// (set) Token: 0x06007ECB RID: 32459 RVA: 0x000BC64A File Offset: 0x000BA84A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170056CB RID: 22219
			// (set) Token: 0x06007ECC RID: 32460 RVA: 0x000BC662 File Offset: 0x000BA862
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009DD RID: 2525
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170056CC RID: 22220
			// (set) Token: 0x06007ECE RID: 32462 RVA: 0x000BC682 File Offset: 0x000BA882
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170056CD RID: 22221
			// (set) Token: 0x06007ECF RID: 32463 RVA: 0x000BC695 File Offset: 0x000BA895
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170056CE RID: 22222
			// (set) Token: 0x06007ED0 RID: 32464 RVA: 0x000BC6AD File Offset: 0x000BA8AD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170056CF RID: 22223
			// (set) Token: 0x06007ED1 RID: 32465 RVA: 0x000BC6C5 File Offset: 0x000BA8C5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170056D0 RID: 22224
			// (set) Token: 0x06007ED2 RID: 32466 RVA: 0x000BC6DD File Offset: 0x000BA8DD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170056D1 RID: 22225
			// (set) Token: 0x06007ED3 RID: 32467 RVA: 0x000BC6F5 File Offset: 0x000BA8F5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170056D2 RID: 22226
			// (set) Token: 0x06007ED4 RID: 32468 RVA: 0x000BC70D File Offset: 0x000BA90D
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
