using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200008F RID: 143
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMembershipEnumerator : IEnumerable<GroupMailbox>, IEnumerable
	{
		// Token: 0x06000382 RID: 898 RVA: 0x00011624 File Offset: 0x0000F824
		public GroupMembershipEnumerator(IEnumerable<string> aadGroups, IEnumerable<GroupMailbox> mailboxGroups, IQueuedGroupJoinInvoker joinInvoker, IGroupMailboxCollectionBuilder groupMailboxCollectionBuilder, IGroupsLogger logger)
		{
			ArgumentValidator.ThrowIfNull("aadGroups", aadGroups);
			ArgumentValidator.ThrowIfNull("mailboxGroups", mailboxGroups);
			ArgumentValidator.ThrowIfNull("joinInvoker", joinInvoker);
			ArgumentValidator.ThrowIfNull("groupMailboxCollectionBuilder", groupMailboxCollectionBuilder);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.aadGroups = aadGroups;
			this.mailboxGroups = mailboxGroups;
			this.joinInvoker = joinInvoker;
			this.groupMailboxCollectionBuilder = groupMailboxCollectionBuilder;
			this.logger = logger;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000119E4 File Offset: 0x0000FBE4
		public IEnumerator<GroupMailbox> GetEnumerator()
		{
			this.logger.CurrentAction = GroupMembershipAction.EnumerateGroups;
			HashSet<string> aadExternalIds = new HashSet<string>(this.aadGroups);
			GroupMembershipEnumerator.Tracer.TraceDebug<int>((long)this.GetHashCode(), "GroupMembershipEnumerator.GetEnumerator - AAD Group list contains {0} groups.", aadExternalIds.Count);
			foreach (GroupMailbox group in this.mailboxGroups)
			{
				if (aadExternalIds.Contains(group.Locator.ExternalId))
				{
					GroupMembershipEnumerator.Tracer.TraceInformation<string>(this.GetHashCode(), 0L, "GroupMembershipEnumerator.GetEnumerator - Found group in AAD and Mailbox. ExternalId={0}", group.Locator.ExternalId);
					aadExternalIds.Remove(group.Locator.ExternalId);
					yield return group;
				}
				else
				{
					this.logger.LogTrace("GroupMembershipEnumerator.GetEnumerator: Found group in mailbox but not AAD. Omitting from results. ExternalId={0}", new object[]
					{
						group.Locator.ExternalId
					});
				}
			}
			if (aadExternalIds.Count > 0)
			{
				foreach (GroupMailbox group2 in this.groupMailboxCollectionBuilder.BuildGroupMailboxes(aadExternalIds.ToArray<string>()))
				{
					this.logger.LogTrace("GroupMembershipEnumerator.GetEnumerator: Found group in AAD but not in Mailbox. Including in results. ExternalId={0}", new object[]
					{
						group2.Locator.ExternalId
					});
					this.joinInvoker.Enqueue(group2);
					yield return group2;
				}
			}
			yield break;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011A00 File Offset: 0x0000FC00
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generics interface of GetEnumerator.");
		}

		// Token: 0x040005F4 RID: 1524
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x040005F5 RID: 1525
		private readonly IEnumerable<string> aadGroups;

		// Token: 0x040005F6 RID: 1526
		private readonly IEnumerable<GroupMailbox> mailboxGroups;

		// Token: 0x040005F7 RID: 1527
		private readonly IQueuedGroupJoinInvoker joinInvoker;

		// Token: 0x040005F8 RID: 1528
		private readonly IGroupMailboxCollectionBuilder groupMailboxCollectionBuilder;

		// Token: 0x040005F9 RID: 1529
		private readonly IGroupsLogger logger;
	}
}
