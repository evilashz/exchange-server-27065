using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000015 RID: 21
	internal sealed class JournalingDistributionGroupCacheItem
	{
		// Token: 0x0600007B RID: 123 RVA: 0x0000967F File Offset: 0x0000787F
		public JournalingDistributionGroupCacheItem(List<string> members)
		{
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			this.members = members;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000969C File Offset: 0x0000789C
		public bool TryGetNextGroupMember(out string groupMember)
		{
			if (this.members.Count == 0)
			{
				groupMember = null;
				return false;
			}
			groupMember = this.members[this.nextMemberIndex];
			this.nextMemberIndex = (this.nextMemberIndex + 1) % this.members.Count;
			return true;
		}

		// Token: 0x04000086 RID: 134
		private int nextMemberIndex;

		// Token: 0x04000087 RID: 135
		private List<string> members;
	}
}
