using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200096C RID: 2412
	internal class SetCalendarGroupOrder : CalendarActionBase<CalendarActionResponse>
	{
		// Token: 0x06004545 RID: 17733 RVA: 0x000F26ED File Offset: 0x000F08ED
		public SetCalendarGroupOrder(MailboxSession session, string groupToPosition, string beforeGroup) : base(session)
		{
			this.groupToPosition = groupToPosition;
			this.beforeGroup = beforeGroup;
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x000F2704 File Offset: 0x000F0904
		public override CalendarActionResponse Execute()
		{
			Guid empty = Guid.Empty;
			byte[] nodeBefore = null;
			byte[] nodeAfter = null;
			Guid guid;
			if (!Guid.TryParse(this.groupToPosition, out guid) || (this.beforeGroup != null && !Guid.TryParse(this.beforeGroup, out empty)))
			{
				ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceError<string, string>((long)this.GetHashCode(), "Provided parent group id is not valid. GroupId: '{0}'. BeforeGroupId: '{1}'", (this.groupToPosition == null) ? "is null" : this.groupToPosition, (this.beforeGroup == null) ? "is null" : this.beforeGroup);
				return new CalendarActionResponse(CalendarActionError.CalendarActionInvalidGroupId);
			}
			VersionedId versionedId = this.FindCalendarGroupId(guid, empty, out nodeBefore, out nodeAfter);
			if (versionedId == null)
			{
				ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceError<Guid>((long)this.GetHashCode(), "Unable to retrieve group with class id: '{0}'", guid);
				return new CalendarActionResponse(CalendarActionError.CalendarActionUnableToFindGroupWithId);
			}
			using (CalendarGroup calendarGroup = CalendarGroup.Bind(base.MailboxSession, versionedId, null))
			{
				string groupName = calendarGroup.GroupName;
				ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceDebug<VersionedId, string, Guid>((long)this.GetHashCode(), "Successfully bound to group. Id: '{0}', Name: '{1}', GroupClassId: '{2}'", versionedId, (groupName == null) ? "is null" : groupName, guid);
				calendarGroup.SetNodeOrdinal(nodeBefore, nodeAfter);
				ConflictResolutionResult conflictResolutionResult = calendarGroup.Save(SaveMode.NoConflictResolution);
				if (conflictResolutionResult.SaveStatus != SaveResult.Success)
				{
					ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceError<VersionedId, string, Guid>((long)this.GetHashCode(), "Unable to update node ordinal of calendar group. Id: '{0}', Name: '{1}', GroupClassId: '{2}'", versionedId, (groupName == null) ? "is null" : groupName, guid);
					return new CalendarActionResponse(CalendarActionError.CalendarActionCannotSaveGroup);
				}
			}
			return new CalendarActionResponse();
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x000F286C File Offset: 0x000F0A6C
		private static bool IsCalendarGroupNode(IStorePropertyBag row)
		{
			string valueOrDefault = row.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			FolderTreeDataSection valueOrDefault2 = row.GetValueOrDefault<FolderTreeDataSection>(FolderTreeDataSchema.GroupSection, FolderTreeDataSection.None);
			FolderTreeDataType valueOrDefault3 = row.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.Undefined);
			return ObjectClass.IsFolderTreeData(valueOrDefault) && valueOrDefault2 == FolderTreeDataSection.Calendar && valueOrDefault3 == FolderTreeDataType.Header;
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x000F28B8 File Offset: 0x000F0AB8
		private VersionedId FindCalendarGroupId(Guid groupToPositionId, Guid groupBeforeId, out byte[] groupBeforeOrdinal, out byte[] groupAfterOrdinal)
		{
			groupBeforeOrdinal = null;
			groupAfterOrdinal = null;
			VersionedId versionedId = null;
			bool flag = this.beforeGroup == null;
			using (Folder folder = Folder.Bind(base.MailboxSession, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, SetCalendarGroupOrder.CommonViewsSortOrder, SetCalendarGroupOrder.AttributesToFetch))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
					foreach (IStorePropertyBag storePropertyBag in propertyBags)
					{
						ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceDebug((long)this.GetHashCode(), "Processing CommonViews result.");
						if (SetCalendarGroupOrder.IsCalendarGroupNode(storePropertyBag))
						{
							if (versionedId != null && groupAfterOrdinal != null)
							{
								break;
							}
							byte[] valueOrDefault = storePropertyBag.GetValueOrDefault<byte[]>(CalendarGroupSchema.GroupClassId, null);
							if (valueOrDefault != null && valueOrDefault.Length == 16)
							{
								Guid guid = new Guid(valueOrDefault);
								if (guid.Equals(groupToPositionId))
								{
									ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceDebug((long)this.GetHashCode(), "Calendar group to move was found.");
									versionedId = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
								}
								else if (this.beforeGroup != null && guid.Equals(groupBeforeId))
								{
									ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceDebug((long)this.GetHashCode(), "Reference calendar group (before) was found.");
									groupBeforeOrdinal = storePropertyBag.GetValueOrDefault<byte[]>(FolderTreeDataSchema.Ordinal, null);
									flag = true;
								}
								else if (flag)
								{
									ExTraceGlobals.SetCalendarGroupOrderCallTracer.TraceDebug((long)this.GetHashCode(), "Reference calendar group (after) was found.");
									groupAfterOrdinal = storePropertyBag.GetValueOrDefault<byte[]>(FolderTreeDataSchema.Ordinal, null);
									flag = false;
								}
							}
						}
					}
				}
			}
			return versionedId;
		}

		// Token: 0x04002853 RID: 10323
		private static readonly SortBy[] CommonViewsSortOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.Type, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Ascending)
		};

		// Token: 0x04002854 RID: 10324
		private static readonly PropertyDefinition[] AttributesToFetch = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			FolderTreeDataSchema.GroupSection,
			CalendarGroupSchema.GroupClassId,
			FolderTreeDataSchema.Type,
			FolderTreeDataSchema.Ordinal
		};

		// Token: 0x04002855 RID: 10325
		private readonly string groupToPosition;

		// Token: 0x04002856 RID: 10326
		private readonly string beforeGroup;
	}
}
