using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001FC RID: 508
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ChangeListSorter
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x0003835B File Offset: 0x0003655B
		public ChangeListSorter(Func<SyncChangeEntry, bool> parentFolderAlreadySeenByCaller)
		{
			SyncUtilities.ThrowIfArgumentNull("parentFolderAlreadySeenByCaller", parentFolderAlreadySeenByCaller);
			this.parentFolderAlreadySeenByCaller = parentFolderAlreadySeenByCaller;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00038378 File Offset: 0x00036578
		public void Run(IList<SyncChangeEntry> incomingChangList, out List<SyncChangeEntry> inOrderChangeList, out List<SyncChangeEntry> orphanChangeList)
		{
			SyncUtilities.ThrowIfArgumentNull("incomingChangList", incomingChangList);
			inOrderChangeList = new List<SyncChangeEntry>(incomingChangList.Count);
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, List<SyncChangeEntry>> dictionary = new Dictionary<string, List<SyncChangeEntry>>(incomingChangList.Count / 2);
			foreach (SyncChangeEntry syncChangeEntry in incomingChangList)
			{
				this.AddSyncChangeEntryInOrder(syncChangeEntry, inOrderChangeList, hashSet, dictionary);
			}
			orphanChangeList = new List<SyncChangeEntry>(dictionary.Count);
			foreach (List<SyncChangeEntry> collection in dictionary.Values)
			{
				orphanChangeList.AddRange(collection);
			}
			dictionary.Clear();
			dictionary = null;
			hashSet.Clear();
			hashSet = null;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00038458 File Offset: 0x00036658
		private void AddSyncChangeEntryInOrder(SyncChangeEntry syncChangeEntry, List<SyncChangeEntry> inOrderChangeList, HashSet<string> inOrderFolders, Dictionary<string, List<SyncChangeEntry>> outOfOrderChangeList)
		{
			if (syncChangeEntry.CloudFolderId == null || this.parentFolderAlreadySeenByCaller(syncChangeEntry) || inOrderFolders.Contains(syncChangeEntry.CloudFolderId))
			{
				this.AddSyncChangeEntryAndItsChildrenToInOrderList(syncChangeEntry, inOrderChangeList, inOrderFolders, outOfOrderChangeList);
				return;
			}
			List<SyncChangeEntry> list = null;
			if (!outOfOrderChangeList.TryGetValue(syncChangeEntry.CloudFolderId, out list))
			{
				list = new List<SyncChangeEntry>(1);
				outOfOrderChangeList.Add(syncChangeEntry.CloudFolderId, list);
			}
			list.Add(syncChangeEntry);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000384C4 File Offset: 0x000366C4
		private void AddSyncChangeEntryAndItsChildrenToInOrderList(SyncChangeEntry syncChangeEntry, List<SyncChangeEntry> inOrderChangeList, HashSet<string> inOrderFolders, Dictionary<string, List<SyncChangeEntry>> outOfOrderChangeList)
		{
			inOrderChangeList.Add(syncChangeEntry);
			inOrderFolders.Add(syncChangeEntry.CloudId);
			if (outOfOrderChangeList.ContainsKey(syncChangeEntry.CloudId))
			{
				foreach (SyncChangeEntry syncChangeEntry2 in outOfOrderChangeList[syncChangeEntry.CloudId])
				{
					this.AddSyncChangeEntryAndItsChildrenToInOrderList(syncChangeEntry2, inOrderChangeList, inOrderFolders, outOfOrderChangeList);
				}
				outOfOrderChangeList.Remove(syncChangeEntry.CloudId);
			}
		}

		// Token: 0x0400098F RID: 2447
		private readonly Func<SyncChangeEntry, bool> parentFolderAlreadySeenByCaller;
	}
}
