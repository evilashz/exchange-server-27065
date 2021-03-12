using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001C3 RID: 451
	internal class ExpandedGroup : CachableItem
	{
		// Token: 0x06001272 RID: 4722 RVA: 0x0005913D File Offset: 0x0005733D
		public ExpandedGroup() : this(new List<Guid>(), new List<Guid>())
		{
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0005914F File Offset: 0x0005734F
		public ExpandedGroup(List<Guid> memberGroups, List<Guid> memberRecipients)
		{
			this.memberGroups = memberGroups;
			this.memberRecipients = memberRecipients;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00059165 File Offset: 0x00057365
		public IEnumerable<Guid> MemberGroups
		{
			get
			{
				return this.memberGroups;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x0005916D File Offset: 0x0005736D
		public override long ItemSize
		{
			get
			{
				return (long)((this.memberGroups.Count + this.memberRecipients.Count) * 16);
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0005918A File Offset: 0x0005738A
		public bool ContainsRecipient(Guid recipientGuid)
		{
			return this.memberRecipients.Contains(recipientGuid);
		}

		// Token: 0x04000AAB RID: 2731
		private const int GuidLength = 16;

		// Token: 0x04000AAC RID: 2732
		private List<Guid> memberGroups;

		// Token: 0x04000AAD RID: 2733
		private List<Guid> memberRecipients;
	}
}
