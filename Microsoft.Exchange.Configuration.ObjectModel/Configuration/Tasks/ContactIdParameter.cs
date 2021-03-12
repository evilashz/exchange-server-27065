using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000165 RID: 357
	[Serializable]
	public class ContactIdParameter : RecipientIdParameter
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x00027F05 File Offset: 0x00026105
		public ContactIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00027F0E File Offset: 0x0002610E
		public ContactIdParameter()
		{
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00027F16 File Offset: 0x00026116
		public ContactIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00027F1F File Offset: 0x0002611F
		public ContactIdParameter(Contact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00027F2D File Offset: 0x0002612D
		public ContactIdParameter(MailContact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00027F3B File Offset: 0x0002613B
		public ContactIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00027F44 File Offset: 0x00026144
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return ContactIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00027F4B File Offset: 0x0002614B
		public new static ContactIdParameter Parse(string identity)
		{
			return new ContactIdParameter(identity);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00027F53 File Offset: 0x00026153
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailEnabledContact(id);
		}

		// Token: 0x040002E5 RID: 741
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.Contact,
			RecipientType.MailContact
		};
	}
}
