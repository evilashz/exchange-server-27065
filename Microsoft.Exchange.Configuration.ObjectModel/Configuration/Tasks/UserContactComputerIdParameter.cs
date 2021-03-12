using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000166 RID: 358
	[Serializable]
	public class UserContactComputerIdParameter : RecipientIdParameter
	{
		// Token: 0x06000CDC RID: 3292 RVA: 0x00027F7E File Offset: 0x0002617E
		public UserContactComputerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00027F87 File Offset: 0x00026187
		public UserContactComputerIdParameter()
		{
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00027F8F File Offset: 0x0002618F
		public UserContactComputerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00027F98 File Offset: 0x00026198
		public UserContactComputerIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00027FA6 File Offset: 0x000261A6
		public UserContactComputerIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00027FB4 File Offset: 0x000261B4
		public UserContactComputerIdParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00027FC2 File Offset: 0x000261C2
		public UserContactComputerIdParameter(Contact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00027FD0 File Offset: 0x000261D0
		public UserContactComputerIdParameter(MailContact contact) : base(contact.Id)
		{
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00027FDE File Offset: 0x000261DE
		public UserContactComputerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00027FE7 File Offset: 0x000261E7
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return UserContactIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00027FEE File Offset: 0x000261EE
		public new static UserContactIdParameter Parse(string identity)
		{
			return new UserContactIdParameter(identity);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00027FF6 File Offset: 0x000261F6
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeUserContactComputer(id);
		}

		// Token: 0x040002E6 RID: 742
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User,
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.Contact,
			RecipientType.MailContact,
			RecipientType.Computer
		};
	}
}
