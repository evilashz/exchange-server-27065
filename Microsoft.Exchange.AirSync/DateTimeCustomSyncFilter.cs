using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200005C RID: 92
	internal class DateTimeCustomSyncFilter : ISyncFilter
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0001E071 File Offset: 0x0001C271
		internal DateTimeCustomSyncFilter(ExDateTime filterStartTime, FolderSyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			this.filterStartTime = filterStartTime;
			this.syncState = syncState;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
		internal DateTimeCustomSyncFilter(FolderSyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			this.syncState = syncState;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
		private DateTimeCustomSyncFilter()
		{
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001E0DB File Offset: 0x0001C2DB
		public string Id
		{
			get
			{
				if (this.prepopulate)
				{
					return "DateTimeCustomSyncFilter: Prepopulating";
				}
				return "DateTimeCustomSyncFilter: > " + this.filterStartTime.ToString(DateTimeFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001E105 File Offset: 0x0001C305
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0001E118 File Offset: 0x0001C318
		private Dictionary<ISyncItemId, DateTimeCustomSyncFilter.FilterState> CustomFilterState
		{
			get
			{
				return this.syncState.GetData<GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, DateTimeCustomSyncFilter.FilterState>, Dictionary<ISyncItemId, DateTimeCustomSyncFilter.FilterState>>(CustomStateDatumType.CustomCalendarSyncFilter, null);
			}
			set
			{
				this.syncState[CustomStateDatumType.CustomCalendarSyncFilter] = new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, DateTimeCustomSyncFilter.FilterState>(value);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001E130 File Offset: 0x0001C330
		public bool IsItemInFilter(ISyncItemId itemId)
		{
			if (this.prepopulate)
			{
				return false;
			}
			if (this.CustomFilterState == null)
			{
				this.CustomFilterState = new Dictionary<ISyncItemId, DateTimeCustomSyncFilter.FilterState>();
			}
			return this.CustomFilterState.ContainsKey(itemId) && this.IsFilterStateInFilter(this.CustomFilterState[itemId]);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001E17C File Offset: 0x0001C37C
		public void UpdateFilterState(SyncOperation syncOperation)
		{
			if (syncOperation == null)
			{
				throw new ArgumentNullException("syncOperation");
			}
			if (this.prepopulate)
			{
				return;
			}
			if (this.CustomFilterState == null)
			{
				this.CustomFilterState = new Dictionary<ISyncItemId, DateTimeCustomSyncFilter.FilterState>();
			}
			switch (syncOperation.ChangeType)
			{
			case ChangeType.Add:
			case ChangeType.Change:
			case ChangeType.ReadFlagChange:
			{
				CalendarItem calendarItem = null;
				try
				{
					calendarItem = (syncOperation.GetItem(new PropertyDefinition[0]).NativeItem as CalendarItem);
					if (calendarItem == null)
					{
						this.UpdateFilterStateWithAddOrChange(syncOperation.Id, false, false, ExDateTime.MinValue);
					}
					else if (calendarItem.Recurrence == null)
					{
						this.UpdateFilterStateWithAddOrChange(syncOperation.Id, true, false, calendarItem.EndTime);
					}
					else if (calendarItem.Recurrence.Range is NoEndRecurrenceRange)
					{
						this.UpdateFilterStateWithAddOrChange(syncOperation.Id, true, true, ExDateTime.MinValue);
					}
					else
					{
						this.UpdateFilterStateWithAddOrChange(syncOperation.Id, true, true, calendarItem.Recurrence.GetLastOccurrence().EndTime);
					}
				}
				catch (Exception ex)
				{
					if (ex is ObjectNotFoundException)
					{
						if (this.CustomFilterState.ContainsKey(syncOperation.Id))
						{
							this.CustomFilterState.Remove(syncOperation.Id);
						}
					}
					else
					{
						if (!SyncCommand.IsItemSyncTolerableException(ex))
						{
							throw;
						}
						StoreId storeId = null;
						string text = "Unknown";
						ExDateTime exDateTime = ExDateTime.MinValue;
						try
						{
							if (calendarItem != null)
							{
								storeId = calendarItem.Id;
								text = calendarItem.Subject;
								exDateTime = calendarItem.StartTime;
							}
						}
						catch
						{
						}
						AirSyncUtility.ExceptionToStringHelper exceptionToStringHelper = new AirSyncUtility.ExceptionToStringHelper(ex);
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Exception was caught in UpdateFilterState. Item id=\"{0}\", subject=\"{1}\", meetingTime={2}\r\n{3}\r\nIgnoring exception and proceeding to next item.", new object[]
						{
							(storeId != null) ? storeId : "null",
							text,
							exDateTime,
							exceptionToStringHelper
						});
					}
				}
				break;
			}
			case (ChangeType)3:
				break;
			case ChangeType.Delete:
				this.CustomFilterState.Remove(syncOperation.Id);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001E35C File Offset: 0x0001C55C
		internal void Prepopulate(Folder folder)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.prepopulate = true;
			if (this.CustomFilterState == null)
			{
				this.CustomFilterState = new Dictionary<ISyncItemId, DateTimeCustomSyncFilter.FilterState>();
			}
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
			{
				ItemSchema.Id,
				CalendarItemInstanceSchema.EndTime,
				CalendarItemBaseSchema.CalendarItemType,
				StoreObjectSchema.ItemClass,
				ItemSchema.Subject,
				CalendarItemInstanceSchema.StartTime
			}))
			{
				bool flag = false;
				while (!flag)
				{
					object[][] rows = queryResult.GetRows(10000);
					flag = (rows.Length == 0);
					for (int i = 0; i < rows.Length; i++)
					{
						StoreObjectId storeObjectId = null;
						DateTimeCustomSyncFilter.FilterState filterState = null;
						ISyncItemId key = null;
						try
						{
							storeObjectId = ((VersionedId)rows[i][0]).ObjectId;
							string itemClass = rows[i][3] as string;
							key = MailboxSyncItemId.CreateForNewItem(storeObjectId);
							if (!this.CustomFilterState.ContainsKey(key))
							{
								filterState = new DateTimeCustomSyncFilter.FilterState();
								this.CustomFilterState[key] = filterState;
							}
							else
							{
								filterState = this.CustomFilterState[key];
							}
							if (!ObjectClass.IsCalendarItem(itemClass))
							{
								filterState.IsCalendarItem = false;
							}
							else
							{
								filterState.IsCalendarItem = true;
								if (!(rows[i][2] is CalendarItemType))
								{
									filterState.IsCalendarItem = false;
								}
								else
								{
									filterState.IsRecurring = (CalendarItemType.RecurringMaster == (CalendarItemType)rows[i][2]);
									if (filterState.IsRecurring)
									{
										using (CalendarItem calendarItem = CalendarItem.Bind(folder.Session, storeObjectId))
										{
											if (calendarItem.Recurrence != null)
											{
												if (calendarItem.Recurrence.Range is NoEndRecurrenceRange)
												{
													filterState.DoesRecurrenceEnd = false;
												}
												else
												{
													filterState.DoesRecurrenceEnd = true;
													OccurrenceInfo lastOccurrence = calendarItem.Recurrence.GetLastOccurrence();
													filterState.EndTime = lastOccurrence.EndTime;
												}
											}
											else
											{
												filterState.IsCalendarItem = false;
											}
											goto IL_1E6;
										}
									}
									if (!(rows[i][1] is ExDateTime))
									{
										filterState.IsCalendarItem = false;
									}
									else
									{
										filterState.EndTime = (ExDateTime)rows[i][1];
									}
									IL_1E6:;
								}
							}
						}
						catch (Exception ex)
						{
							if (ex is ObjectNotFoundException)
							{
								this.CustomFilterState.Remove(key);
							}
							else
							{
								if (!SyncCommand.IsItemSyncTolerableException(ex))
								{
									throw;
								}
								string text = "Unknown";
								ExDateTime exDateTime = ExDateTime.MinValue;
								try
								{
									text = (rows[i][4] as string);
									exDateTime = (ExDateTime)rows[i][5];
								}
								catch
								{
								}
								AirSyncUtility.ExceptionToStringHelper exceptionToStringHelper = new AirSyncUtility.ExceptionToStringHelper(ex);
								AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Exception was caught in Prepopulate. Item id=\"{0}\", subject=\"{1}\", meetingTime={2}\r\n{3}\r\nIgnoring exception and proceeding to next item.", new object[]
								{
									(storeObjectId != null) ? storeObjectId : "null",
									text,
									exDateTime,
									exceptionToStringHelper
								});
								if (filterState != null)
								{
									filterState.IsCalendarItem = false;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001E690 File Offset: 0x0001C890
		internal void UpdateFilterStateWithAddOrChange(ISyncItemId itemId, bool calendar, bool recurring, ExDateTime endTime)
		{
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			DateTimeCustomSyncFilter.FilterState filterState = null;
			if (this.CustomFilterState == null)
			{
				this.CustomFilterState = new Dictionary<ISyncItemId, DateTimeCustomSyncFilter.FilterState>();
			}
			try
			{
				if (!this.CustomFilterState.ContainsKey(itemId))
				{
					filterState = new DateTimeCustomSyncFilter.FilterState();
					this.CustomFilterState[itemId] = filterState;
				}
				else
				{
					filterState = this.CustomFilterState[itemId];
				}
				filterState.IsCalendarItem = calendar;
				if (calendar)
				{
					filterState.IsRecurring = recurring;
					if (recurring)
					{
						if (endTime.Equals(ExDateTime.MinValue))
						{
							filterState.DoesRecurrenceEnd = false;
						}
						else
						{
							filterState.DoesRecurrenceEnd = true;
							filterState.EndTime = endTime;
						}
					}
					else
					{
						filterState.EndTime = endTime;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is ObjectNotFoundException || !SyncCommand.IsItemSyncTolerableException(ex))
				{
					throw;
				}
				if (filterState != null)
				{
					filterState.IsCalendarItem = false;
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001E76C File Offset: 0x0001C96C
		private bool IsFilterStateInFilter(DateTimeCustomSyncFilter.FilterState itemFilterState)
		{
			return itemFilterState != null && itemFilterState.IsCalendarItem && ((itemFilterState.IsRecurring && !itemFilterState.DoesRecurrenceEnd) || itemFilterState.EndTime > this.filterStartTime);
		}

		// Token: 0x040003AD RID: 941
		private bool prepopulate;

		// Token: 0x040003AE RID: 942
		private SyncState syncState;

		// Token: 0x040003AF RID: 943
		private ExDateTime filterStartTime = ExDateTime.MinValue;

		// Token: 0x0200005D RID: 93
		internal sealed class FilterState : ICustomSerializableBuilder, ICustomSerializable
		{
			// Token: 0x17000210 RID: 528
			// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
			// (set) Token: 0x06000521 RID: 1313 RVA: 0x0001E7BF File Offset: 0x0001C9BF
			public ushort TypeId
			{
				get
				{
					return DateTimeCustomSyncFilter.FilterState.typeId;
				}
				set
				{
					DateTimeCustomSyncFilter.FilterState.typeId = value;
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001E7C7 File Offset: 0x0001C9C7
			// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001E7CF File Offset: 0x0001C9CF
			internal ExDateTime EndTime
			{
				get
				{
					return this.endTime;
				}
				set
				{
					this.endTime = value;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
			// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
			internal bool IsRecurring
			{
				get
				{
					return this.recurring;
				}
				set
				{
					this.recurring = value;
				}
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001E7E9 File Offset: 0x0001C9E9
			// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001E7F1 File Offset: 0x0001C9F1
			internal bool DoesRecurrenceEnd
			{
				get
				{
					return this.doesRecurrenceEnd;
				}
				set
				{
					this.doesRecurrenceEnd = value;
				}
			}

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001E7FA File Offset: 0x0001C9FA
			// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001E802 File Offset: 0x0001CA02
			internal bool IsCalendarItem
			{
				get
				{
					return this.calendarItem;
				}
				set
				{
					this.calendarItem = value;
				}
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x0001E80B File Offset: 0x0001CA0B
			public void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
			{
				writer.Write(this.calendarItem);
				componentDataPool.GetDateTimeDataInstance().Bind(this.endTime).SerializeData(writer, componentDataPool);
				writer.Write(this.recurring);
				writer.Write(this.doesRecurrenceEnd);
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x0001E84C File Offset: 0x0001CA4C
			public void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
			{
				this.calendarItem = reader.ReadBoolean();
				DateTimeData dateTimeDataInstance = componentDataPool.GetDateTimeDataInstance();
				dateTimeDataInstance.DeserializeData(reader, componentDataPool);
				this.endTime = dateTimeDataInstance.Data;
				this.recurring = reader.ReadBoolean();
				this.doesRecurrenceEnd = reader.ReadBoolean();
			}

			// Token: 0x0600052C RID: 1324 RVA: 0x0001E898 File Offset: 0x0001CA98
			public ICustomSerializable BuildObject()
			{
				return new DateTimeCustomSyncFilter.FilterState();
			}

			// Token: 0x040003B0 RID: 944
			private static ushort typeId;

			// Token: 0x040003B1 RID: 945
			private bool calendarItem;

			// Token: 0x040003B2 RID: 946
			private ExDateTime endTime = ExDateTime.MinValue;

			// Token: 0x040003B3 RID: 947
			private bool recurring;

			// Token: 0x040003B4 RID: 948
			private bool doesRecurrenceEnd;
		}
	}
}
