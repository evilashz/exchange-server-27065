using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001AA RID: 426
	public class GetManagedFolderMailboxPolicyCommand : SyntheticCommandWithPipelineInput<ManagedFolderMailboxPolicy, ManagedFolderMailboxPolicy>
	{
		// Token: 0x060024BD RID: 9405 RVA: 0x00047426 File Offset: 0x00045626
		private GetManagedFolderMailboxPolicyCommand() : base("Get-ManagedFolderMailboxPolicy")
		{
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00047433 File Offset: 0x00045633
		public GetManagedFolderMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00047442 File Offset: 0x00045642
		public virtual GetManagedFolderMailboxPolicyCommand SetParameters(GetManagedFolderMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0004744C File Offset: 0x0004564C
		public virtual GetManagedFolderMailboxPolicyCommand SetParameters(GetManagedFolderMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001AB RID: 427
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D22 RID: 3362
			// (set) Token: 0x060024C1 RID: 9409 RVA: 0x00047456 File Offset: 0x00045656
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000D23 RID: 3363
			// (set) Token: 0x060024C2 RID: 9410 RVA: 0x00047474 File Offset: 0x00045674
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D24 RID: 3364
			// (set) Token: 0x060024C3 RID: 9411 RVA: 0x00047487 File Offset: 0x00045687
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D25 RID: 3365
			// (set) Token: 0x060024C4 RID: 9412 RVA: 0x0004749F File Offset: 0x0004569F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D26 RID: 3366
			// (set) Token: 0x060024C5 RID: 9413 RVA: 0x000474B7 File Offset: 0x000456B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D27 RID: 3367
			// (set) Token: 0x060024C6 RID: 9414 RVA: 0x000474CF File Offset: 0x000456CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001AC RID: 428
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000D28 RID: 3368
			// (set) Token: 0x060024C8 RID: 9416 RVA: 0x000474EF File Offset: 0x000456EF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000D29 RID: 3369
			// (set) Token: 0x060024C9 RID: 9417 RVA: 0x0004750D File Offset: 0x0004570D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000D2A RID: 3370
			// (set) Token: 0x060024CA RID: 9418 RVA: 0x0004752B File Offset: 0x0004572B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D2B RID: 3371
			// (set) Token: 0x060024CB RID: 9419 RVA: 0x0004753E File Offset: 0x0004573E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D2C RID: 3372
			// (set) Token: 0x060024CC RID: 9420 RVA: 0x00047556 File Offset: 0x00045756
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D2D RID: 3373
			// (set) Token: 0x060024CD RID: 9421 RVA: 0x0004756E File Offset: 0x0004576E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D2E RID: 3374
			// (set) Token: 0x060024CE RID: 9422 RVA: 0x00047586 File Offset: 0x00045786
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
