using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	public class MailContactIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x0002867E File Offset: 0x0002687E
		public MailContactIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00028687 File Offset: 0x00026887
		public MailContactIdParameter()
		{
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002868F File Offset: 0x0002688F
		public MailContactIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00028698 File Offset: 0x00026898
		public MailContactIdParameter(MailContact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000286A6 File Offset: 0x000268A6
		public MailContactIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x000286AF File Offset: 0x000268AF
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailContactIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000286B6 File Offset: 0x000268B6
		public new static MailContactIdParameter Parse(string identity)
		{
			return new MailContactIdParameter(identity);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000286BE File Offset: 0x000268BE
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailContact(id);
		}

		// Token: 0x040002F2 RID: 754
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.MailContact
		};
	}
}
