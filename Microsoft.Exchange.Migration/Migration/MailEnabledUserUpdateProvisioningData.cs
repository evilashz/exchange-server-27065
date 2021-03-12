using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000049 RID: 73
	internal class MailEnabledUserUpdateProvisioningData : ProvisioningData
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0000BEE6 File Offset: 0x0000A0E6
		internal MailEnabledUserUpdateProvisioningData()
		{
			base.Action = ProvisioningAction.UpdateExisting;
			base.ProvisioningType = ProvisioningType.MailEnabledUserUpdate;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000BF0E File Offset: 0x0000A10E
		public string Manager
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Manager];
			}
			set
			{
				base[ADOrgPersonSchema.Manager] = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000BF1C File Offset: 0x0000A11C
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000BF2E File Offset: 0x0000A12E
		public string[] GrantSendOnBehalfTo
		{
			get
			{
				return (string[])base[ADRecipientSchema.GrantSendOnBehalfTo];
			}
			set
			{
				base[ADRecipientSchema.GrantSendOnBehalfTo] = value;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BF3C File Offset: 0x0000A13C
		public static MailEnabledUserUpdateProvisioningData Create(string meuId)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(meuId, "meuId");
			return new MailEnabledUserUpdateProvisioningData
			{
				Identity = meuId
			};
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BF62 File Offset: 0x0000A162
		public bool IsEmpty()
		{
			return string.IsNullOrEmpty(base.Identity) || (string.IsNullOrEmpty(this.Manager) && (this.GrantSendOnBehalfTo == null || this.GrantSendOnBehalfTo.Length <= 0));
		}
	}
}
