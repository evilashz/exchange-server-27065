using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	public class MailboxOrMailUserIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x0002856C File Offset: 0x0002676C
		public MailboxOrMailUserIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00028575 File Offset: 0x00026775
		public MailboxOrMailUserIdParameter()
		{
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002857D File Offset: 0x0002677D
		public MailboxOrMailUserIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00028586 File Offset: 0x00026786
		public MailboxOrMailUserIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00028594 File Offset: 0x00026794
		public MailboxOrMailUserIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000285A2 File Offset: 0x000267A2
		public MailboxOrMailUserIdParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000285B0 File Offset: 0x000267B0
		public MailboxOrMailUserIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000285B9 File Offset: 0x000267B9
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return MailboxOrMailUserIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000285C0 File Offset: 0x000267C0
		public new static MailboxOrMailUserIdParameter Parse(string identity)
		{
			return new MailboxOrMailUserIdParameter(identity);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000285C8 File Offset: 0x000267C8
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeMailboxOrMailUser(id);
		}

		// Token: 0x040002F0 RID: 752
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox,
			RecipientType.MailUser
		};
	}
}
