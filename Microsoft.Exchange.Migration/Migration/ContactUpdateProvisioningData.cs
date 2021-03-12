using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000040 RID: 64
	internal class ContactUpdateProvisioningData : ProvisioningData
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000B340 File Offset: 0x00009540
		internal ContactUpdateProvisioningData()
		{
			base.Action = ProvisioningAction.UpdateExisting;
			base.ProvisioningType = ProvisioningType.ContactUpdate;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B356 File Offset: 0x00009556
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000B368 File Offset: 0x00009568
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B376 File Offset: 0x00009576
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000B388 File Offset: 0x00009588
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

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B398 File Offset: 0x00009598
		public static ContactUpdateProvisioningData Create(string contactId)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(contactId, "contactId");
			return new ContactUpdateProvisioningData
			{
				Identity = contactId
			};
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B3BE File Offset: 0x000095BE
		public bool IsEmpty()
		{
			return string.IsNullOrEmpty(base.Identity) || (string.IsNullOrEmpty(this.Manager) && (this.GrantSendOnBehalfTo == null || this.GrantSendOnBehalfTo.Length <= 0));
		}
	}
}
