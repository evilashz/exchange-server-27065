using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000929 RID: 2345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class StoreParticipantOrigin : ParticipantOrigin
	{
		// Token: 0x060057A0 RID: 22432 RVA: 0x001683FA File Offset: 0x001665FA
		public StoreParticipantOrigin(StoreId originItemId) : this(originItemId, EmailAddressIndex.None)
		{
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x00168404 File Offset: 0x00166604
		public StoreParticipantOrigin(StoreId originItemId, EmailAddressIndex emailAddressIndex)
		{
			EnumValidator.ThrowIfInvalid<EmailAddressIndex>(emailAddressIndex);
			if (originItemId == null)
			{
				throw new ArgumentNullException("originItemId");
			}
			this.originItemId = StoreId.GetStoreObjectId(originItemId);
			this.emailAddressIndex = emailAddressIndex;
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x00168433 File Offset: 0x00166633
		public EmailAddressIndex EmailAddressIndex
		{
			get
			{
				return this.emailAddressIndex;
			}
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x0016843B File Offset: 0x0016663B
		public StoreObjectId OriginItemId
		{
			get
			{
				return this.originItemId;
			}
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x00168443 File Offset: 0x00166643
		public override string ToString()
		{
			return string.Format("Store ({0}, {1})", this.originItemId, this.emailAddressIndex);
		}

		// Token: 0x060057A5 RID: 22437 RVA: 0x00168460 File Offset: 0x00166660
		internal override IEnumerable<PropValue> GetProperties()
		{
			List<PropValue> list = new List<PropValue>();
			if (this.emailAddressIndex == EmailAddressIndex.None)
			{
				list.Add(new PropValue(ParticipantSchema.OriginItemId, this.originItemId));
			}
			list.Add(new PropValue(ParticipantSchema.DisplayType, (this.emailAddressIndex == EmailAddressIndex.None) ? LegacyRecipientDisplayType.PersonalDistributionList : LegacyRecipientDisplayType.MailUser));
			return list;
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x001684B3 File Offset: 0x001666B3
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x04002EBE RID: 11966
		private readonly StoreObjectId originItemId;

		// Token: 0x04002EBF RID: 11967
		private readonly EmailAddressIndex emailAddressIndex;
	}
}
