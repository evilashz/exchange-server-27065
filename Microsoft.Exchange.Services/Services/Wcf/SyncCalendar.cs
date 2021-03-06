using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200093A RID: 2362
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncCalendar : ServiceCommand<SyncCalendarResponse>
	{
		// Token: 0x06004466 RID: 17510 RVA: 0x000EAE8C File Offset: 0x000E908C
		public SyncCalendar(CallContext context, SyncCalendarParameters request) : base(context)
		{
			this.idConverter = new IdConverter(context);
			this.syncState = SyncCalendar.CalendarSyncState.Parse(request.SyncState);
			this.folderId = request.FolderId;
			this.windowStart = ExDateTime.Parse(request.WindowStart);
			this.windowEnd = ExDateTime.Parse(request.WindowEnd);
			this.includeAdditionalDataInResponse = request.RespondWithAdditionalData;
			OwsLogRegistry.Register(base.GetType().Name, typeof(SyncCalendarMetadata), new Type[0]);
			if (request.MaxChangesReturned == 0 || request.MaxChangesReturned > 200)
			{
				this.maxChangesReturned = 200;
				return;
			}
			this.maxChangesReturned = request.MaxChangesReturned;
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x000EAF44 File Offset: 0x000E9144
		private static ItemResponseShape ResponseShape
		{
			get
			{
				if (SyncCalendar.responseShape == null)
				{
					SyncCalendar.responseShape = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, new PropertyPath[]
					{
						CalendarItemSchema.ICalendarUid.PropertyPath,
						CalendarItemSchema.CalendarItemType.PropertyPath,
						CalendarItemSchema.Start.PropertyPath,
						CalendarItemSchema.StartWallClock.PropertyPath,
						CalendarItemSchema.End.PropertyPath,
						CalendarItemSchema.EndWallClock.PropertyPath
					});
				}
				return SyncCalendar.responseShape;
			}
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x000EAFF4 File Offset: 0x000E91F4
		protected override SyncCalendarResponse InternalExecute()
		{
			ExTraceGlobals.SyncCalendarCallTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendar.InternalExecute: Start");
			Stopwatch stopwatch = Stopwatch.StartNew();
			IdAndSession idAndSession = this.idConverter.ConvertFolderIdToIdAndSessionReadOnly(this.folderId.BaseFolderId);
			PropertyListForViewRowDeterminer determiner = null;
			SyncCalendar syncCalendar = new SyncCalendar(this.syncState, idAndSession.Session, idAndSession.GetAsStoreObjectId(), delegate(CalendarFolder folder)
			{
				determiner = PropertyListForViewRowDeterminer.BuildForItems(SyncCalendar.ResponseShape, folder);
				return determiner.GetPropertiesToFetch();
			}, this.windowStart, this.windowEnd, this.includeAdditionalDataInResponse, this.maxChangesReturned);
			IFolderSyncState folderSyncState;
			IList<KeyValuePair<StoreId, LocalizedException>> list;
			SyncCalendarResponse syncCalendarResponse = syncCalendar.Execute(out folderSyncState, out list);
			foreach (KeyValuePair<StoreId, LocalizedException> keyValuePair in list)
			{
				base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.ExceptionStoreId, keyValuePair.Key.ToBase64String());
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, keyValuePair.Value, "SyncCalendar_Exception");
			}
			SyncCalendar.CalendarSyncState calendarSyncState = new SyncCalendar.CalendarSyncState(((ServicesFolderSyncState)folderSyncState).SerializeAsBase64String(), syncCalendarResponse.QueryResumptionPoint, syncCalendarResponse.OldWindowEnd);
			SyncCalendarResponse syncCalendarResponse2 = new SyncCalendarResponse
			{
				SyncState = calendarSyncState.ToString(),
				DeletedItems = SyncCalendar.CopyDeletedItemsList(idAndSession, syncCalendarResponse.DeletedItems),
				UpdatedItems = SyncCalendar.CopyItemsList(determiner, idAndSession, syncCalendarResponse.UpdatedItems),
				RecurrenceMastersWithInstances = SyncCalendar.CopyItemsList(determiner, idAndSession, syncCalendarResponse.RecurrenceMastersWithInstances),
				RecurrenceMastersWithoutInstances = SyncCalendar.CopyItemsList(determiner, idAndSession, syncCalendarResponse.RecurrenceMastersWithoutInstances),
				UnchangedRecurrenceMastersWithInstances = SyncCalendar.CopyItemsList(determiner, idAndSession, syncCalendarResponse.UnchangedRecurrenceMastersWithInstances),
				IncludesLastItemInRange = syncCalendarResponse.IncludesLastItemInRange
			};
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.SyncStateSize, syncCalendarResponse2.SyncState.Length);
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.SyncStateHash, syncCalendarResponse2.SyncState.GetHashCode());
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.DeletedItemsCount, syncCalendarResponse2.DeletedItems.Length);
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.UpdatedItemsCount, syncCalendarResponse2.UpdatedItems.Length);
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.RecurrenceMastersWithInstancesCount, syncCalendarResponse2.RecurrenceMastersWithInstances.Length);
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.RecurrenceMastersWithoutInstancesCount, syncCalendarResponse2.RecurrenceMastersWithoutInstances.Length);
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.UnchangedRecurrenceMastersWithInstancesCount, syncCalendarResponse2.UnchangedRecurrenceMastersWithInstances.Length);
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.IncludesLastItemInRange, syncCalendarResponse2.IncludesLastItemInRange);
			stopwatch.Stop();
			base.CallContext.ProtocolLog.Set(SyncCalendarMetadata.TotalTime, stopwatch.ElapsedMilliseconds);
			ExTraceGlobals.SyncCalendarCallTracer.TraceDebug((long)this.GetHashCode(), "SyncCalendar.InternalExecute: End");
			return syncCalendarResponse2;
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x000EB33C File Offset: 0x000E953C
		private static Microsoft.Exchange.Services.Core.Types.ItemId GetEwsItemId(StoreId manifestNativeId, IdAndSession folderIdAndSession)
		{
			IdAndSession idAndSession = new IdAndSession(manifestNativeId, folderIdAndSession.Session);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(manifestNativeId, idAndSession, null);
			return new Microsoft.Exchange.Services.Core.Types.ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x000EB374 File Offset: 0x000E9574
		private static Microsoft.Exchange.Services.Core.Types.ItemId[] CopyDeletedItemsList(IdAndSession folderIdAndSession, IList<StoreId> deletedItemsList)
		{
			Microsoft.Exchange.Services.Core.Types.ItemId[] array = new Microsoft.Exchange.Services.Core.Types.ItemId[deletedItemsList.Count];
			for (int i = 0; i < deletedItemsList.Count; i++)
			{
				array[i] = SyncCalendar.GetEwsItemId(deletedItemsList[i], folderIdAndSession);
			}
			return array;
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x000EB3B0 File Offset: 0x000E95B0
		private static EwsCalendarItemType[] CopyItemsList(PropertyListForViewRowDeterminer determiner, IdAndSession folderIdAndSession, IList<SyncCalendarItemType> itemsList)
		{
			EwsCalendarItemType[] array = new EwsCalendarItemType[itemsList.Count];
			for (int i = 0; i < itemsList.Count; i++)
			{
				if (itemsList[i].RowData == null)
				{
					array[i] = new EwsCalendarItemType
					{
						UID = itemsList[i].UID,
						ItemId = SyncCalendar.GetEwsItemId(itemsList[i].ItemId, folderIdAndSession),
						CalendarItemTypeString = itemsList[i].CalendarItemType.ToString(),
						Start = StartEndProperty.ConvertDateTimeToString(itemsList[i].Start, false),
						StartWallClock = StartEndProperty.ConvertDateTimeToString(itemsList[i].StartWallClock, true),
						End = StartEndProperty.ConvertDateTimeToString(itemsList[i].End, false),
						EndWallClock = StartEndProperty.ConvertDateTimeToString(itemsList[i].EndWallClock, true)
					};
				}
				else
				{
					StoreObjectType storeObjectType;
					ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyList = determiner.GetToServiceObjectPropertyList(itemsList[i].RowData, out storeObjectType);
					array[i] = new EwsCalendarItemType();
					toServiceObjectPropertyList.ConvertPropertiesToServiceObject(array[i], itemsList[i].RowData, folderIdAndSession);
				}
			}
			return array;
		}

		// Token: 0x040027DA RID: 10202
		private const int MaxChangesReturnedCap = 200;

		// Token: 0x040027DB RID: 10203
		private const int MaximumAllowedIcsBatchSize = 100;

		// Token: 0x040027DC RID: 10204
		private const double IcsCutOffPercentage = 0.8;

		// Token: 0x040027DD RID: 10205
		private readonly int maxChangesReturned;

		// Token: 0x040027DE RID: 10206
		private readonly bool includeAdditionalDataInResponse;

		// Token: 0x040027DF RID: 10207
		private static ItemResponseShape responseShape;

		// Token: 0x040027E0 RID: 10208
		private SyncCalendar.CalendarSyncState syncState;

		// Token: 0x040027E1 RID: 10209
		private TargetFolderId folderId;

		// Token: 0x040027E2 RID: 10210
		private ExDateTime windowStart;

		// Token: 0x040027E3 RID: 10211
		private ExDateTime windowEnd;

		// Token: 0x040027E4 RID: 10212
		private IdConverter idConverter;

		// Token: 0x0200093B RID: 2363
		[ClassAccessLevel(AccessLevel.Implementation)]
		private class CalendarSyncState : Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar.CalendarSyncState
		{
			// Token: 0x0600446C RID: 17516 RVA: 0x000EB4DA File Offset: 0x000E96DA
			public CalendarSyncState(string base64IcsSyncState, CalendarViewQueryResumptionPoint queryResumptionPoint, ExDateTime? oldWindowEndTime) : base(base64IcsSyncState, queryResumptionPoint, oldWindowEndTime)
			{
			}

			// Token: 0x0600446D RID: 17517 RVA: 0x000EB4E8 File Offset: 0x000E96E8
			public static SyncCalendar.CalendarSyncState Parse(string value)
			{
				if (!string.IsNullOrEmpty(value))
				{
					string[] array = value.Split(new char[]
					{
						','
					});
					if (array.Length == 7 && array[0] == "v2")
					{
						CalendarViewQueryResumptionPoint queryResumptionPoint = Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar.CalendarSyncState.SafeGetResumptionPoint(array[2], array[3], array[4], array[5]);
						return new SyncCalendar.CalendarSyncState(array[1], queryResumptionPoint, Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar.CalendarSyncState.SafeGetDateTimeValue(array[6]));
					}
				}
				return SyncCalendar.CalendarSyncState.Empty;
			}

			// Token: 0x0600446E RID: 17518 RVA: 0x000EB54F File Offset: 0x000E974F
			public override IFolderSyncState CreateFolderSyncState(StoreObjectId folderObjectId, ISyncProvider syncProvider)
			{
				return new ServicesFolderSyncState(folderObjectId, syncProvider, base.IcsSyncState);
			}

			// Token: 0x040027E5 RID: 10213
			public static readonly SyncCalendar.CalendarSyncState Empty = new SyncCalendar.CalendarSyncState(null, null, null);
		}
	}
}
