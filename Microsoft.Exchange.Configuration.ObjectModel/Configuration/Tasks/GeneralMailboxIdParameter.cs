using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000121 RID: 289
	[Serializable]
	public class GeneralMailboxIdParameter : MailboxIdParameter
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x0002299A File Offset: 0x00020B9A
		public GeneralMailboxIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000229A3 File Offset: 0x00020BA3
		public GeneralMailboxIdParameter()
		{
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000229AB File Offset: 0x00020BAB
		public GeneralMailboxIdParameter(MailboxEntry storeMailboxEntry) : base(storeMailboxEntry)
		{
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000229B4 File Offset: 0x00020BB4
		public GeneralMailboxIdParameter(MailboxId storeMailboxId) : base(storeMailboxId)
		{
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000229BD File Offset: 0x00020BBD
		public GeneralMailboxIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000229C6 File Offset: 0x00020BC6
		public GeneralMailboxIdParameter(ReducedRecipient reducedRecipient) : this(reducedRecipient.Id)
		{
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000229D4 File Offset: 0x00020BD4
		public GeneralMailboxIdParameter(ADUser user) : this(user.Id)
		{
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000229E2 File Offset: 0x00020BE2
		public GeneralMailboxIdParameter(ADSystemAttendantMailbox systemAttendant) : this(systemAttendant.Id)
		{
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000229F0 File Offset: 0x00020BF0
		public GeneralMailboxIdParameter(Mailbox mailbox) : base(mailbox)
		{
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000229F9 File Offset: 0x00020BF9
		public GeneralMailboxIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00022A02 File Offset: 0x00020C02
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return GeneralMailboxIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00022A09 File Offset: 0x00020C09
		public new static GeneralMailboxIdParameter Parse(string identity)
		{
			return new GeneralMailboxIdParameter(identity);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00022A11 File Offset: 0x00020C11
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeGeneralMailboxIdParameter(id);
		}

		// Token: 0x04000283 RID: 643
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox,
			RecipientType.SystemAttendantMailbox,
			RecipientType.SystemMailbox
		};
	}
}
