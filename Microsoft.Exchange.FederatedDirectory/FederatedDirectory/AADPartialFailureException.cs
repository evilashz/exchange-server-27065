using System;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000003 RID: 3
	internal sealed class AADPartialFailureException : LocalizedException
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public AADPartialFailureException(string group) : base(Strings.PartiallyFailedToUpdateGroup(group))
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E6 File Offset: 0x000002E6
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020EE File Offset: 0x000002EE
		public AADPartialFailureException.FailedLink[] FailedAddedMembers { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F7 File Offset: 0x000002F7
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020FF File Offset: 0x000002FF
		public AADPartialFailureException.FailedLink[] FailedRemovedMembers { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002108 File Offset: 0x00000308
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public AADPartialFailureException.FailedLink[] FailedAddedOwners { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002119 File Offset: 0x00000319
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002121 File Offset: 0x00000321
		public AADPartialFailureException.FailedLink[] FailedRemovedOwners { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000212A File Offset: 0x0000032A
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002132 File Offset: 0x00000332
		public AADPartialFailureException.FailedLink[] FailedAddedPendingMembers { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000213B File Offset: 0x0000033B
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002143 File Offset: 0x00000343
		public AADPartialFailureException.FailedLink[] FailedRemovedPendingMembers { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000214C File Offset: 0x0000034C
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(base.Message);
				if (this.FailedAddedMembers != null)
				{
					foreach (AADPartialFailureException.FailedLink failedLink in this.FailedAddedMembers)
					{
						stringBuilder.AppendLine(Strings.FailedToAddMember(failedLink.Link, failedLink.Exception.ToString()));
					}
				}
				if (this.FailedRemovedMembers != null)
				{
					foreach (AADPartialFailureException.FailedLink failedLink2 in this.FailedRemovedMembers)
					{
						stringBuilder.AppendLine(Strings.FailedToRemoveMember(failedLink2.Link, failedLink2.Exception.ToString()));
					}
				}
				if (this.FailedAddedOwners != null)
				{
					foreach (AADPartialFailureException.FailedLink failedLink3 in this.FailedAddedOwners)
					{
						stringBuilder.AppendLine(Strings.FailedToAddOwner(failedLink3.Link, failedLink3.Exception.ToString()));
					}
				}
				if (this.FailedRemovedOwners != null)
				{
					foreach (AADPartialFailureException.FailedLink failedLink4 in this.FailedRemovedOwners)
					{
						stringBuilder.AppendLine(Strings.FailedToRemoveOwner(failedLink4.Link, failedLink4.Exception.ToString()));
					}
				}
				if (this.FailedAddedPendingMembers != null)
				{
					foreach (AADPartialFailureException.FailedLink failedLink5 in this.FailedAddedPendingMembers)
					{
						stringBuilder.AppendLine(Strings.FailedToAddPendingMember(failedLink5.Link, failedLink5.Exception.ToString()));
					}
				}
				if (this.FailedRemovedPendingMembers != null)
				{
					foreach (AADPartialFailureException.FailedLink failedLink6 in this.FailedRemovedPendingMembers)
					{
						stringBuilder.AppendLine(Strings.FailedToRemovePendingMember(failedLink6.Link, failedLink6.Exception.ToString()));
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000004 RID: 4
		public struct FailedLink
		{
			// Token: 0x0400000F RID: 15
			public string Link;

			// Token: 0x04000010 RID: 16
			public Exception Exception;
		}
	}
}
