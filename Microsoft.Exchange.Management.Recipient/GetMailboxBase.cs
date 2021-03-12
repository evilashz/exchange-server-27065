using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200004B RID: 75
	public abstract class GetMailboxBase<TIdentity> : GetRecipientWithAddressListBase<TIdentity, ADUser> where TIdentity : RecipientIdParameter, new()
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x00014551 File Offset: 0x00012751
		public GetMailboxBase()
		{
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00014559 File Offset: 0x00012759
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetMailboxBase<TIdentity>.SortPropertiesArray;
			}
		}

		// Token: 0x0400011B RID: 283
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			MailEnabledRecipientSchema.Alias,
			MailEnabledRecipientSchema.DisplayName,
			ADObjectSchema.Name,
			MailboxSchema.Office,
			MailboxSchema.ServerLegacyDN
		};
	}
}
