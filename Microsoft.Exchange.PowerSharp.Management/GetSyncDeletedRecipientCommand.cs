using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DDA RID: 3546
	public class GetSyncDeletedRecipientCommand : SyntheticCommandWithPipelineInput<ADRawEntry, ADRawEntry>
	{
		// Token: 0x0600D353 RID: 54099 RVA: 0x0012C9E3 File Offset: 0x0012ABE3
		private GetSyncDeletedRecipientCommand() : base("Get-SyncDeletedRecipient")
		{
		}

		// Token: 0x0600D354 RID: 54100 RVA: 0x0012C9F0 File Offset: 0x0012ABF0
		public GetSyncDeletedRecipientCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D355 RID: 54101 RVA: 0x0012C9FF File Offset: 0x0012ABFF
		public virtual GetSyncDeletedRecipientCommand SetParameters(GetSyncDeletedRecipientCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DDB RID: 3547
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A358 RID: 41816
			// (set) Token: 0x0600D356 RID: 54102 RVA: 0x0012CA09 File Offset: 0x0012AC09
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x1700A359 RID: 41817
			// (set) Token: 0x0600D357 RID: 54103 RVA: 0x0012CA21 File Offset: 0x0012AC21
			public virtual int Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x1700A35A RID: 41818
			// (set) Token: 0x0600D358 RID: 54104 RVA: 0x0012CA39 File Offset: 0x0012AC39
			public virtual SyncRecipientType RecipientType
			{
				set
				{
					base.PowerSharpParameters["RecipientType"] = value;
				}
			}

			// Token: 0x1700A35B RID: 41819
			// (set) Token: 0x0600D359 RID: 54105 RVA: 0x0012CA51 File Offset: 0x0012AC51
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A35C RID: 41820
			// (set) Token: 0x0600D35A RID: 54106 RVA: 0x0012CA64 File Offset: 0x0012AC64
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A35D RID: 41821
			// (set) Token: 0x0600D35B RID: 54107 RVA: 0x0012CA7C File Offset: 0x0012AC7C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A35E RID: 41822
			// (set) Token: 0x0600D35C RID: 54108 RVA: 0x0012CA94 File Offset: 0x0012AC94
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A35F RID: 41823
			// (set) Token: 0x0600D35D RID: 54109 RVA: 0x0012CAAC File Offset: 0x0012ACAC
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
