using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200008C RID: 140
	internal class UserUpdateProvisioningData : ProvisioningData
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00023BDF File Offset: 0x00021DDF
		internal UserUpdateProvisioningData()
		{
			base.Action = ProvisioningAction.UpdateExisting;
			base.ProvisioningType = ProvisioningType.UserUpdate;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00023BF5 File Offset: 0x00021DF5
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00023C07 File Offset: 0x00021E07
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00023C15 File Offset: 0x00021E15
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00023C27 File Offset: 0x00021E27
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

		// Token: 0x06000810 RID: 2064 RVA: 0x00023C38 File Offset: 0x00021E38
		public static UserUpdateProvisioningData Create(string userId)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(userId, "userId");
			return new UserUpdateProvisioningData
			{
				Identity = userId
			};
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00023C5E File Offset: 0x00021E5E
		public bool IsEmpty()
		{
			return string.IsNullOrEmpty(base.Identity) || (string.IsNullOrEmpty(this.Manager) && (this.GrantSendOnBehalfTo == null || this.GrantSendOnBehalfTo.Length <= 0));
		}
	}
}
