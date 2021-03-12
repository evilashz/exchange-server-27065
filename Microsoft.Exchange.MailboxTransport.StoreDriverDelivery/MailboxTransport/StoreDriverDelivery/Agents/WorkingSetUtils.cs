using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000D8 RID: 216
	internal static class WorkingSetUtils
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x000242EC File Offset: 0x000224EC
		internal static StoreObjectId FindWorkingSetPartitionFolder(MailboxSession mailboxSession, string partitionName)
		{
			StoreObjectId result;
			using (Folder folder = WorkingSetUtils.SafeBindToWorkingSetFolder(mailboxSession))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id
				}))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.WorkingSetSourcePartitionInternal, partitionName));
					object[][] rows = queryResult.GetRows(1);
					result = ((rows.Length > 0) ? ((VersionedId)rows[0][0]).ObjectId : null);
				}
			}
			return result;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00024388 File Offset: 0x00022588
		internal static StoreObjectId GetWorkingSetPartitionFolderId(MailboxSession mailboxSession, string workingSetSourcePartition, string workingSetSourcePartitionInternal)
		{
			StoreObjectId result;
			using (Folder folder = WorkingSetUtils.SafeBindToWorkingSetFolder(mailboxSession))
			{
				StoreObjectId storeObjectId = WorkingSetUtils.FindWorkingSetPartitionFolder(mailboxSession, workingSetSourcePartitionInternal);
				if (storeObjectId == null)
				{
					using (Folder folder2 = Folder.Create(mailboxSession, folder.StoreObjectId, StoreObjectType.Folder, workingSetSourcePartition, CreateMode.CreateNew))
					{
						folder2[FolderSchema.WorkingSetSourcePartitionInternal] = workingSetSourcePartitionInternal;
						folder2.Save();
						folder2.Load();
						storeObjectId = folder2.StoreObjectId;
						WorkingSet.PartitionsCreated.Increment();
					}
				}
				result = storeObjectId;
			}
			return result;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0002441C File Offset: 0x0002261C
		public static void DeleteWorkingSetPartition(MailboxSession mailboxSession, string workingSetSourcePartitionInternal)
		{
			StoreObjectId storeObjectId = WorkingSetUtils.FindWorkingSetPartitionFolder(mailboxSession, workingSetSourcePartitionInternal);
			if (storeObjectId != null)
			{
				using (Folder folder = WorkingSetUtils.SafeBindToWorkingSetFolder(mailboxSession))
				{
					AggregateOperationResult aggregateOperationResult = folder.DeleteObjects(DeleteItemFlags.HardDelete, new StoreId[]
					{
						storeObjectId
					});
					if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
					{
						throw new ObjectNotFoundException(new LocalizedString(string.Format("Delete partition failed, partition {0} is not found", workingSetSourcePartitionInternal)));
					}
					WorkingSet.PartitionsDeleted.Increment();
					return;
				}
			}
			throw new ObjectNotFoundException(new LocalizedString(string.Format("Delete partition failed, partition {0} is not found", workingSetSourcePartitionInternal)));
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000244AC File Offset: 0x000226AC
		internal static bool IsWorkingSetAgentFeatureEnabled(MiniRecipient miniRecipient)
		{
			if (miniRecipient != null)
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(miniRecipient.GetContext(null), null, null);
				return snapshot.WorkingSet.WorkingSetAgent.Enabled;
			}
			return false;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000244E0 File Offset: 0x000226E0
		internal static string GetWorkingSetSourcePartition(Item item)
		{
			object property = item.GetProperty("WorkingSetSourcePartition");
			if (property != null)
			{
				return property.ToString();
			}
			return "Default";
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00024508 File Offset: 0x00022708
		internal static string GetWorkingSetSourcePartitionInternal(Item item)
		{
			object property = item.GetProperty("WorkingSetSourcePartitionInternal");
			if (property != null)
			{
				return property.ToString();
			}
			return "Default";
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00024530 File Offset: 0x00022730
		internal static WorkingSetFlags GetWorkingSetFlags(WorkingSetSource workingSetSource)
		{
			WorkingSetFlags result;
			switch (workingSetSource)
			{
			case WorkingSetSource.None:
				result = WorkingSetFlags.Exchange;
				break;
			case WorkingSetSource.SPO:
				result = (WorkingSetFlags.WorkingSet | WorkingSetFlags.SPO);
				break;
			case WorkingSetSource.Yammer:
				result = (WorkingSetFlags.WorkingSet | WorkingSetFlags.Yammer);
				break;
			case WorkingSetSource.O365:
				result = WorkingSetFlags.WorkingSet;
				break;
			case WorkingSetSource.SubscribedGroups:
				result = (WorkingSetFlags.Exchange | WorkingSetFlags.WorkingSet | WorkingSetFlags.Subscribed | WorkingSetFlags.Groups);
				break;
			case WorkingSetSource.PinnedGroups:
				result = (WorkingSetFlags.WorkingSet | WorkingSetFlags.Pinned | WorkingSetFlags.Groups);
				break;
			default:
				throw new FormatException(string.Format("Invalid Working Set source : {0}", workingSetSource));
			}
			return result;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00024594 File Offset: 0x00022794
		private static Folder SafeBindToWorkingSetFolder(StoreSession session)
		{
			Folder result = null;
			try
			{
				result = Folder.Bind(session, DefaultFolderType.WorkingSet);
			}
			catch (ObjectNotFoundException)
			{
				MailboxSession mailboxSession = session as MailboxSession;
				if (mailboxSession == null)
				{
					throw;
				}
				mailboxSession.CreateDefaultFolder(DefaultFolderType.WorkingSet);
				result = Folder.Bind(session, DefaultFolderType.WorkingSet);
			}
			return result;
		}

		// Token: 0x04000389 RID: 905
		internal const int DefaultRetentionPeriod = 30;

		// Token: 0x0400038A RID: 906
		private const string DefaultWorkingSetSourcePartition = "Default";

		// Token: 0x0400038B RID: 907
		private const string DefaultWorkingSetSourcePartitionInternal = "Default";
	}
}
