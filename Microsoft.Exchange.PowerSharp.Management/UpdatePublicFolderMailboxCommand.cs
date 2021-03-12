using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000253 RID: 595
	public class UpdatePublicFolderMailboxCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x06002C6B RID: 11371 RVA: 0x000516A1 File Offset: 0x0004F8A1
		private UpdatePublicFolderMailboxCommand() : base("Update-PublicFolderMailbox")
		{
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000516AE File Offset: 0x0004F8AE
		public UpdatePublicFolderMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000516BD File Offset: 0x0004F8BD
		public virtual UpdatePublicFolderMailboxCommand SetParameters(UpdatePublicFolderMailboxCommand.InvokeMailboxAssistantParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000516C7 File Offset: 0x0004F8C7
		public virtual UpdatePublicFolderMailboxCommand SetParameters(UpdatePublicFolderMailboxCommand.InvokeSynchronizerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000516D1 File Offset: 0x0004F8D1
		public virtual UpdatePublicFolderMailboxCommand SetParameters(UpdatePublicFolderMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000254 RID: 596
		public class InvokeMailboxAssistantParameters : ParametersBase
		{
			// Token: 0x1700137E RID: 4990
			// (set) Token: 0x06002C70 RID: 11376 RVA: 0x000516DB File Offset: 0x0004F8DB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700137F RID: 4991
			// (set) Token: 0x06002C71 RID: 11377 RVA: 0x000516F9 File Offset: 0x0004F8F9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001380 RID: 4992
			// (set) Token: 0x06002C72 RID: 11378 RVA: 0x0005170C File Offset: 0x0004F90C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001381 RID: 4993
			// (set) Token: 0x06002C73 RID: 11379 RVA: 0x00051724 File Offset: 0x0004F924
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001382 RID: 4994
			// (set) Token: 0x06002C74 RID: 11380 RVA: 0x0005173C File Offset: 0x0004F93C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001383 RID: 4995
			// (set) Token: 0x06002C75 RID: 11381 RVA: 0x00051754 File Offset: 0x0004F954
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001384 RID: 4996
			// (set) Token: 0x06002C76 RID: 11382 RVA: 0x0005176C File Offset: 0x0004F96C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000255 RID: 597
		public class InvokeSynchronizerParameters : ParametersBase
		{
			// Token: 0x17001385 RID: 4997
			// (set) Token: 0x06002C78 RID: 11384 RVA: 0x0005178C File Offset: 0x0004F98C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001386 RID: 4998
			// (set) Token: 0x06002C79 RID: 11385 RVA: 0x000517AA File Offset: 0x0004F9AA
			public virtual SwitchParameter InvokeSynchronizer
			{
				set
				{
					base.PowerSharpParameters["InvokeSynchronizer"] = value;
				}
			}

			// Token: 0x17001387 RID: 4999
			// (set) Token: 0x06002C7A RID: 11386 RVA: 0x000517C2 File Offset: 0x0004F9C2
			public virtual SwitchParameter FullSync
			{
				set
				{
					base.PowerSharpParameters["FullSync"] = value;
				}
			}

			// Token: 0x17001388 RID: 5000
			// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000517DA File Offset: 0x0004F9DA
			public virtual SwitchParameter SuppressStatus
			{
				set
				{
					base.PowerSharpParameters["SuppressStatus"] = value;
				}
			}

			// Token: 0x17001389 RID: 5001
			// (set) Token: 0x06002C7C RID: 11388 RVA: 0x000517F2 File Offset: 0x0004F9F2
			public virtual SwitchParameter ReconcileFolders
			{
				set
				{
					base.PowerSharpParameters["ReconcileFolders"] = value;
				}
			}

			// Token: 0x1700138A RID: 5002
			// (set) Token: 0x06002C7D RID: 11389 RVA: 0x0005180A File Offset: 0x0004FA0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700138B RID: 5003
			// (set) Token: 0x06002C7E RID: 11390 RVA: 0x0005181D File Offset: 0x0004FA1D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700138C RID: 5004
			// (set) Token: 0x06002C7F RID: 11391 RVA: 0x00051835 File Offset: 0x0004FA35
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700138D RID: 5005
			// (set) Token: 0x06002C80 RID: 11392 RVA: 0x0005184D File Offset: 0x0004FA4D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700138E RID: 5006
			// (set) Token: 0x06002C81 RID: 11393 RVA: 0x00051865 File Offset: 0x0004FA65
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700138F RID: 5007
			// (set) Token: 0x06002C82 RID: 11394 RVA: 0x0005187D File Offset: 0x0004FA7D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000256 RID: 598
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001390 RID: 5008
			// (set) Token: 0x06002C84 RID: 11396 RVA: 0x0005189D File Offset: 0x0004FA9D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001391 RID: 5009
			// (set) Token: 0x06002C85 RID: 11397 RVA: 0x000518B0 File Offset: 0x0004FAB0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001392 RID: 5010
			// (set) Token: 0x06002C86 RID: 11398 RVA: 0x000518C8 File Offset: 0x0004FAC8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001393 RID: 5011
			// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000518E0 File Offset: 0x0004FAE0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001394 RID: 5012
			// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000518F8 File Offset: 0x0004FAF8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001395 RID: 5013
			// (set) Token: 0x06002C89 RID: 11401 RVA: 0x00051910 File Offset: 0x0004FB10
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
