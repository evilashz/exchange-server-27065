using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000182 RID: 386
	[Serializable]
	public class UserContactIdParameter : RecipientIdParameter
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x0002A1E6 File Offset: 0x000283E6
		public UserContactIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0002A1EF File Offset: 0x000283EF
		public UserContactIdParameter()
		{
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0002A1F7 File Offset: 0x000283F7
		public UserContactIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0002A200 File Offset: 0x00028400
		public UserContactIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0002A20E File Offset: 0x0002840E
		public UserContactIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0002A21C File Offset: 0x0002841C
		public UserContactIdParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0002A22A File Offset: 0x0002842A
		public UserContactIdParameter(Contact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0002A238 File Offset: 0x00028438
		public UserContactIdParameter(MailContact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0002A246 File Offset: 0x00028446
		public UserContactIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0002A24F File Offset: 0x0002844F
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return UserContactIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0002A256 File Offset: 0x00028456
		public new static UserContactIdParameter Parse(string identity)
		{
			return new UserContactIdParameter(identity);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002A25E File Offset: 0x0002845E
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeUserContact(id);
		}

		// Token: 0x04000307 RID: 775
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User,
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.Contact,
			RecipientType.MailContact
		};
	}
}
