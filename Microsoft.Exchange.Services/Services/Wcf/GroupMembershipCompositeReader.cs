using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMembershipCompositeReader : IGroupMembershipReader<GroupMailbox>
	{
		// Token: 0x0600037D RID: 893 RVA: 0x00011490 File Offset: 0x0000F690
		public GroupMembershipCompositeReader(IGroupMembershipReader<string> aadReader, IGroupMembershipReader<GroupMailbox> mailboxReader, IQueuedGroupJoinInvoker joinInvoker, IGroupsLogger logger, IGroupMailboxCollectionBuilder groupMailboxCollectionBuilder) : this(aadReader, mailboxReader, logger, (IEnumerable<string> aadGroups, IEnumerable<GroupMailbox> mailboxGroups) => new GroupMembershipEnumerator(aadGroups, mailboxGroups, joinInvoker, groupMailboxCollectionBuilder, logger))
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000114DC File Offset: 0x0000F6DC
		public GroupMembershipCompositeReader(IGroupMembershipReader<string> aadReader, IGroupMembershipReader<GroupMailbox> mailboxReader, IGroupsLogger logger, Func<IEnumerable<string>, IEnumerable<GroupMailbox>, IEnumerable<GroupMailbox>> groupMembershipEnumeratorCreator)
		{
			ArgumentValidator.ThrowIfNull("aadReader", aadReader);
			ArgumentValidator.ThrowIfNull("mailboxReader", mailboxReader);
			ArgumentValidator.ThrowIfNull("logger", logger);
			ArgumentValidator.ThrowIfNull("groupMembershipEnumeratorCreator", groupMembershipEnumeratorCreator);
			this.aadReader = aadReader;
			this.mailboxReader = mailboxReader;
			this.logger = logger;
			this.groupMembershipEnumeratorCreator = groupMembershipEnumeratorCreator;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00011548 File Offset: 0x0000F748
		public IEnumerable<GroupMailbox> GetJoinedGroups()
		{
			this.logger.CurrentAction = GroupMembershipAction.EnumerateGroups;
			IEnumerable<string> enumerable = null;
			Task<IEnumerable<string>> task = Task.Run<IEnumerable<string>>(() => this.aadReader.GetJoinedGroups());
			IEnumerable<GroupMailbox> joinedGroups = this.mailboxReader.GetJoinedGroups();
			try
			{
				if (task.Wait(GroupMembershipCompositeReader.MaxWaitTimeForAADQuery))
				{
					enumerable = task.Result;
				}
			}
			catch (AggregateException exception)
			{
				this.logger.LogException(exception, "GroupMembershipCompositeReader.GetJoinedGroups - Unable to retrieve group membership from AAD.", new object[0]);
			}
			if (enumerable == null)
			{
				GroupMembershipCompositeReader.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMembershipCompositeReader.GetJoinedGroups - AAD lookup failed. Returning unfiltered list from mailbox.");
				return joinedGroups;
			}
			GroupMembershipCompositeReader.Tracer.TraceDebug((long)this.GetHashCode(), "GroupMembershipCompositeReader.GetJoinedGroups - Found groups in AAD. Building filtered / augmented list.");
			return this.groupMembershipEnumeratorCreator(enumerable, joinedGroups);
		}

		// Token: 0x040005EE RID: 1518
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x040005EF RID: 1519
		private static readonly TimeSpan MaxWaitTimeForAADQuery = TimeSpan.FromSeconds(20.0);

		// Token: 0x040005F0 RID: 1520
		private readonly IGroupMembershipReader<string> aadReader;

		// Token: 0x040005F1 RID: 1521
		private readonly IGroupMembershipReader<GroupMailbox> mailboxReader;

		// Token: 0x040005F2 RID: 1522
		private readonly IGroupsLogger logger;

		// Token: 0x040005F3 RID: 1523
		private readonly Func<IEnumerable<string>, IEnumerable<GroupMailbox>, IEnumerable<GroupMailbox>> groupMembershipEnumeratorCreator;
	}
}
