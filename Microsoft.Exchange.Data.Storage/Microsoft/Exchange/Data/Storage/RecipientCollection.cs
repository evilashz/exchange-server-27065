using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200085D RID: 2141
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecipientCollection : RecipientBaseCollection<Recipient>, IRecipientBaseCollection<Recipient>, IList<Recipient>, ICollection<Recipient>, IEnumerable<Recipient>, IEnumerable
	{
		// Token: 0x06005080 RID: 20608 RVA: 0x0014E5D9 File Offset: 0x0014C7D9
		internal RecipientCollection(CoreRecipientCollection coreRecipientCollection) : base(coreRecipientCollection)
		{
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0014E5E4 File Offset: 0x0014C7E4
		public Recipient Add(Participant participant, RecipientItemType recipItemType)
		{
			CoreRecipient coreRecipient = base.CreateCoreRecipient(new CoreRecipient.SetDefaultPropertiesDelegate(Recipient.SetDefaultRecipientProperties), participant);
			Recipient recipient = this.ConstructStronglyTypedRecipient(coreRecipient);
			recipient.RecipientItemType = recipItemType;
			return recipient;
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x0014E615 File Offset: 0x0014C815
		public override Recipient Add(Participant participant)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			return this.Add(participant, RecipientItemType.To);
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x0014E633 File Offset: 0x0014C833
		public override void Add(Recipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("item");
			}
			this.Add(recipient.Participant, recipient.RecipientItemType);
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0014E656 File Offset: 0x0014C856
		internal void CopyRecipientsFrom(RecipientCollection recipientCollection)
		{
			base.CoreItem.Recipients.CopyRecipientsFrom(recipientCollection.CoreItem.Recipients);
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x0014E673 File Offset: 0x0014C873
		protected override Recipient ConstructStronglyTypedRecipient(CoreRecipient coreRecipient)
		{
			return new Recipient(coreRecipient);
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x0014E67B File Offset: 0x0014C87B
		public bool Contains(Participant participant)
		{
			return this.Contains(participant, false);
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x0014E688 File Offset: 0x0014C888
		public bool Contains(Participant participant, bool canLookup)
		{
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			foreach (Recipient recipient in this)
			{
				if (Participant.HasSameEmail(recipient.Participant, participant, canLookup))
				{
					return true;
				}
			}
			return false;
		}
	}
}
