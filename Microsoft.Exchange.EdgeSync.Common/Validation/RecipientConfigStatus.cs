using System;
using System.Text;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class RecipientConfigStatus : EdgeConfigStatus
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x000091E3 File Offset: 0x000073E3
		public RecipientConfigStatus(SyncStatus status, string detail)
		{
			base.SyncStatus = status;
			this.detail = detail;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000091FC File Offset: 0x000073FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.SyncStatus.ToString());
			if (!string.IsNullOrEmpty(this.detail))
			{
				stringBuilder.Append(" - ");
				stringBuilder.Append(this.detail);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400013F RID: 319
		public const string RecipientDoesNotExistInAD = "Recipient doesn't exist in source Active Directory";

		// Token: 0x04000140 RID: 320
		public const string MoreThanOneRecipientsInAD = "More than one recipient found in source Active Directory and may cause NDR on Edge server. RecipientStatus.ConflictObjects contains relevant entries.";

		// Token: 0x04000141 RID: 321
		public const string MoreThanOneRecipientsInADAM = "More than one recipient found in target Edge Server and may cause NDR on Edge server. RecipientStatus.ConflictObjects contains relevant entries.";

		// Token: 0x04000142 RID: 322
		public const string RecipientDoesNotExistInEdge = "Recipient doesn't exist in target Edge Server and may cause NDR on Edge server";

		// Token: 0x04000143 RID: 323
		public const string RecipientAttributesNotSynced = "Recipient exists in target Edge Server but attributes are not synchronized";

		// Token: 0x04000144 RID: 324
		public const string RecipientRequiresAuthToSendTo = "Recipient requires sender authentication and this may cause NDR on Edge server. RecipientStatus.ConflictObjects contains the recipient object in source Active Directory";

		// Token: 0x04000145 RID: 325
		private readonly string detail;
	}
}
