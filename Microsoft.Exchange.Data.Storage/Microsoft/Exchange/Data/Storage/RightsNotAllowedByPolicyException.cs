using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000775 RID: 1909
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class RightsNotAllowedByPolicyException : StoragePermanentException
	{
		// Token: 0x060048BB RID: 18619 RVA: 0x001316F2 File Offset: 0x0012F8F2
		internal RightsNotAllowedByPolicyException(RightsNotAllowedRecipient[] rightsNotAllowedRecipients, StoreObjectType storeObjectType, string folderName) : base(ServerStrings.RightsNotAllowedByPolicy(storeObjectType.ToString(), folderName))
		{
			if (rightsNotAllowedRecipients == null)
			{
				throw new ArgumentNullException("rightsNotAllowedRecipients");
			}
			if (rightsNotAllowedRecipients.Length == 0)
			{
				throw new ArgumentException("rightsNotAllowedRecipients");
			}
			this.rightsNotAllowedRecipients = rightsNotAllowedRecipients;
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x060048BC RID: 18620 RVA: 0x00131730 File Offset: 0x0012F930
		public RightsNotAllowedRecipient[] RightsNotAllowedRecipients
		{
			get
			{
				return this.rightsNotAllowedRecipients;
			}
		}

		// Token: 0x04002770 RID: 10096
		private RightsNotAllowedRecipient[] rightsNotAllowedRecipients;
	}
}
