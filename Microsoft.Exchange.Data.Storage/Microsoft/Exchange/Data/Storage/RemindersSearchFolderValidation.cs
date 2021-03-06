using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000687 RID: 1671
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemindersSearchFolderValidation : SearchFolderValidation
	{
		// Token: 0x0600447F RID: 17535 RVA: 0x001240E4 File Offset: 0x001222E4
		internal RemindersSearchFolderValidation() : base(new IValidator[]
		{
			new MatchContainerClass("Outlook.Reminder"),
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x00124115 File Offset: 0x00122315
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			return base.EnsureIsValid(context, folder) && RemindersSearchFolderValidation.VerifyAndFixRemindersSearchFolder(context, (SearchFolder)folder);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x00124130 File Offset: 0x00122330
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.Save();
			searchFolder.Load(null);
			searchFolder.ApplyContinuousSearch(RemindersSearchFolderValidation.CreateRemindersSearchCriteria(context));
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x00124166 File Offset: 0x00122366
		private static bool IsReminderSearchFolderForO11(DefaultFolderContext context, SearchFolderCriteria currentCriteria)
		{
			ExTraceGlobals.DefaultFoldersTracer.TraceDebug<SearchFolderCriteria>(-1L, "RemindersSearchFolderValidation::IsReminderSearchFolderForO11. We are checking currentCriteria on the folder we found. currentCriteria = {0}.", currentCriteria);
			return SearchFolderValidation.MatchSearchFolderCriteria(currentCriteria, RemindersSearchFolderValidation.CreateRemindersQueryForO11(context));
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x00124188 File Offset: 0x00122388
		private static QueryFilter[] GetO12RemindersSearchFolderExclusionList(SearchFolderCriteria currentCriteria)
		{
			AndFilter andFilter = currentCriteria.SearchQuery as AndFilter;
			AndFilter andFilter2 = (andFilter != null && andFilter.FilterCount > 0) ? (andFilter.Filters[0] as AndFilter) : null;
			if (andFilter2 != null)
			{
				QueryFilter[] array = new QueryFilter[andFilter2.FilterCount];
				andFilter2.Filters.CopyTo(array, 0);
				return array;
			}
			return null;
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x001241E4 File Offset: 0x001223E4
		private static bool IsReminderSearchFolderForO12(DefaultFolderContext context, SearchFolderCriteria currentCriteria, out bool isUpToDate)
		{
			isUpToDate = false;
			ExTraceGlobals.DefaultFoldersTracer.TraceDebug<SearchFolderCriteria>(-1L, "RemindersSearchFolderValidation::IsReminderSearchFolderForO12. We are checking currentCriteria on the folder we found. currentCriteria = {0}.", currentCriteria);
			QueryFilter[] o12RemindersSearchFolderExclusionList = RemindersSearchFolderValidation.GetO12RemindersSearchFolderExclusionList(currentCriteria);
			if (o12RemindersSearchFolderExclusionList == null)
			{
				return false;
			}
			foreach (QueryFilter queryFilter in o12RemindersSearchFolderExclusionList)
			{
				ComparisonFilter comparisonFilter = queryFilter as ComparisonFilter;
				if (comparisonFilter == null || comparisonFilter.ComparisonOperator != ComparisonOperator.NotEqual || !comparisonFilter.Property.Equals(InternalSchema.ParentItemId) || !(comparisonFilter.PropertyValue is StoreId))
				{
					return false;
				}
			}
			AndFilter andFilter = currentCriteria.SearchQuery as AndFilter;
			SearchFolderCriteria searchFolderCriteria = RemindersSearchFolderValidation.CreateRemindersQueryForO12(context, o12RemindersSearchFolderExclusionList);
			AndFilter andFilter2 = (AndFilter)searchFolderCriteria.SearchQuery;
			if (andFilter == null || andFilter.FilterCount != 2 || andFilter.FilterCount != andFilter2.FilterCount || currentCriteria.FolderScope.Length != searchFolderCriteria.FolderScope.Length)
			{
				return false;
			}
			for (int j = 0; j < currentCriteria.FolderScope.Length; j++)
			{
				if (!currentCriteria.FolderScope[j].Equals(searchFolderCriteria.FolderScope[j]))
				{
					return false;
				}
			}
			if (!andFilter.Filters[1].Equals(andFilter2.Filters[1]))
			{
				return false;
			}
			AndFilter andFilter3 = (AndFilter)andFilter2.Filters[0];
			isUpToDate = (o12RemindersSearchFolderExclusionList.Length == andFilter3.FilterCount);
			return true;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x00124338 File Offset: 0x00122538
		private static RemindersSearchFolderValidation.RemindersSearchFolderState GetRemindersSearchFolderState(DefaultFolderContext context, SearchFolder reminders)
		{
			SearchFolderCriteria searchFolderCriteria = SearchFolderValidation.TryGetSearchCriteria(reminders);
			if (searchFolderCriteria == null)
			{
				ExTraceGlobals.DefaultFoldersTracer.TraceDebug(-1L, "RemindersSearchFolderValidation::GetRemindersSearchFolderState. currentCriteria is null.");
				return new RemindersSearchFolderValidation.RemindersSearchFolderState(RemindersSearchFolderValidation.RemindersSearchFolderVersion.NotSet, false);
			}
			if (RemindersSearchFolderValidation.IsReminderSearchFolderForO11(context, searchFolderCriteria))
			{
				ExTraceGlobals.DefaultFoldersTracer.TraceDebug<SearchFolderCriteria>(-1L, "RemindersSearchFolderValidation::GetRemindersSearchFolderState. currentCriteria is O11. current = {0}.", searchFolderCriteria);
				return new RemindersSearchFolderValidation.RemindersSearchFolderState(RemindersSearchFolderValidation.RemindersSearchFolderVersion.O11, false);
			}
			bool isUpToDate;
			if (RemindersSearchFolderValidation.IsReminderSearchFolderForO12(context, searchFolderCriteria, out isUpToDate))
			{
				ExTraceGlobals.DefaultFoldersTracer.TraceDebug<SearchFolderCriteria>(-1L, "DefaultFolderManager::GetRemindersSearchFolderState. currentCriteria is O12. current = {0}.", searchFolderCriteria);
				return new RemindersSearchFolderValidation.RemindersSearchFolderState(RemindersSearchFolderValidation.RemindersSearchFolderVersion.O12, isUpToDate);
			}
			ExTraceGlobals.DefaultFoldersTracer.Information<string>(-1L, "Reminders search folder has an unknown criteria; probably from a newer client: {0}", searchFolderCriteria.SearchQuery.ToString());
			return new RemindersSearchFolderValidation.RemindersSearchFolderState(RemindersSearchFolderValidation.RemindersSearchFolderVersion.Unknown, true);
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x001243D8 File Offset: 0x001225D8
		private static bool VerifyAndFixRemindersSearchFolder(DefaultFolderContext context, SearchFolder reminders)
		{
			RemindersSearchFolderValidation.RemindersSearchFolderState remindersSearchFolderState = RemindersSearchFolderValidation.GetRemindersSearchFolderState(context, reminders);
			if (!remindersSearchFolderState.IsUpToDate)
			{
				ExTraceGlobals.DefaultFoldersTracer.TraceDebug<RemindersSearchFolderValidation.RemindersSearchFolderState>(-1L, "RemindersSearchFolderValidation::VerifyAndFixRemindersSearchFolder. We are updating reminder's state: {0}", remindersSearchFolderState);
				if (context.Session.LogonType != LogonType.Owner)
				{
					return false;
				}
				QueryFilter[] array = (remindersSearchFolderState.Version == RemindersSearchFolderValidation.RemindersSearchFolderVersion.O12) ? RemindersSearchFolderValidation.GetO12RemindersSearchFolderExclusionList(reminders.GetSearchCriteria()) : null;
				if (array != null && array.Length > 30)
				{
					string message = "A maximum allowed number of exclusion entries for a Reminders search folder is reached.Either default folders got re-created numerous times or the folder completeness criteria produced false negatives.";
					ExTraceGlobals.DefaultFoldersTracer.TraceError(-1L, message);
					return true;
				}
				SearchFolderCriteria searchFolderCriteria = RemindersSearchFolderValidation.CreateRemindersQueryForO12(context, array);
				if (array != null)
				{
					ExTraceGlobals.DefaultFoldersTracer.TraceDebug<int, int>(-1L, "RemindersSearchFolderValidation::VerifyAndFixRemindersSearchFolder. Current Reminders search criteria is incomplete: {0} exclusions versus {1} expected", array.Length, RemindersSearchFolderValidation.GetO12RemindersSearchFolderExclusionList(searchFolderCriteria).Length);
				}
				if (remindersSearchFolderState.Version != RemindersSearchFolderValidation.RemindersSearchFolderVersion.O11)
				{
					ExTraceGlobals.DefaultFoldersTracer.TraceDebug<RemindersSearchFolderValidation.RemindersSearchFolderState>(-1L, "RemindersSearchFolderValidation::VerifyAndFixRemindersSearchFolder. Reminder starts to apply new criteria on the folder. folderState = {0}.", remindersSearchFolderState);
					reminders.ApplyContinuousSearch(searchFolderCriteria);
				}
				else
				{
					ExTraceGlobals.DefaultFoldersTracer.TraceDebug<RemindersSearchFolderValidation.RemindersSearchFolderState>(-1L, "RemindersSearchFolderValidation::VerifyAndFixRemindersSearchFolder. Outlook12. folderState = {0}.", remindersSearchFolderState);
					IAsyncResult asyncResult = reminders.BeginApplyContinuousSearch(searchFolderCriteria, null, null);
					if (RemindersSearchFolderValidation.InternalWaitForRemindersIndexing(asyncResult))
					{
						reminders.EndApplyContinuousSearch(asyncResult);
					}
					else
					{
						ExTraceGlobals.DefaultFoldersTracer.TraceDebug(-1L, "RemindersSearchFolderValidation::VerifyAndFixRemindersSearchFolder. Timeout expired waiting for a Reminders search folder to finish population.");
					}
					RemindersSearchFolderValidation.UnsetStaleReminders(context, reminders);
					UserConfiguration userConfiguration = null;
					try
					{
						try
						{
							userConfiguration = context.Session.UserConfigurationManager.GetFolderConfiguration("Calendar", UserConfigurationTypes.Dictionary, context.Session.GetDefaultFolderId(DefaultFolderType.Calendar));
						}
						catch (ObjectNotFoundException)
						{
							ExTraceGlobals.DefaultFoldersTracer.TraceDebug(-1L, "RemindersSearchFolderValidation::VerifyAndFixRemindersSearchFolder. No existing Calendar configuration was found. We are creating new one.");
							userConfiguration = context.Session.UserConfigurationManager.CreateFolderConfiguration("Calendar", UserConfigurationTypes.Dictionary, context.Session.GetDefaultFolderId(DefaultFolderType.Calendar));
						}
						IDictionary dictionary = userConfiguration.GetDictionary();
						dictionary["piReminderUpgradeTime"] = Util.ConvertDateTimeToRTime(ExDateTime.GetNow(context.Session.ExTimeZone));
						userConfiguration.Save();
					}
					finally
					{
						if (userConfiguration != null)
						{
							userConfiguration.Dispose();
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x001245B4 File Offset: 0x001227B4
		private static void UnsetStaleReminders(DefaultFolderContext context, SearchFolder reminders)
		{
			ExTraceGlobals.DefaultFoldersTracer.TraceDebug(-1L, "RemindersSearchFolderValidation::UnsetStaleReminders.");
			using (QueryResult queryResult = reminders.ItemQuery(ItemQueryType.None, null, new SortBy[]
			{
				new SortBy(InternalSchema.ReminderIsSet, SortOrder.Descending),
				new SortBy(InternalSchema.ReminderNextTime, SortOrder.Descending)
			}, new PropertyDefinition[]
			{
				InternalSchema.ItemId,
				InternalSchema.ParentItemId,
				InternalSchema.ReminderIsSet
			}))
			{
				ExDateTime now = ExDateTime.GetNow(context.Session.ExTimeZone);
				StoreId[] folderScope = RemindersSearchFolderValidation.CreateRemindersQueryForO11(context).FolderScope;
				queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.LessThan, InternalSchema.ReminderNextTime, now));
				bool flag = false;
				object[][] rows;
				while (!flag && (rows = queryResult.GetRows(2147483647)).Length > 0)
				{
					foreach (object[] array2 in rows)
					{
						bool flag2;
						if (Util.TryConvertTo<bool>(array2[2], out flag2) && !flag2)
						{
							flag = true;
							break;
						}
						StoreObjectId storeObjectId = PropertyBag.CheckPropertyValue<StoreObjectId>(StoreObjectSchema.ParentItemId, array2[1]);
						bool flag3 = false;
						foreach (StoreId id in folderScope)
						{
							if (storeObjectId.Equals(id))
							{
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							try
							{
								VersionedId storeId = PropertyBag.CheckPropertyValue<VersionedId>(ItemSchema.Id, array2[0]);
								using (Item item = Item.Bind(context.Session, storeId))
								{
									if (item.Reminder != null)
									{
										item.Reminder.IsSet = false;
										item.Save(SaveMode.NoConflictResolution);
									}
								}
							}
							catch (StoragePermanentException arg)
							{
								ExTraceGlobals.DefaultFoldersTracer.TraceDebug<object, StoragePermanentException>(-1L, "RemindersSearchFolderValidation::UnsetStaleReminders. Unable to unset a stale reminder of the item {0}: {1}", array2[0], arg);
							}
							catch (StorageTransientException arg2)
							{
								ExTraceGlobals.DefaultFoldersTracer.TraceDebug<object, StorageTransientException>(-1L, "RemindersSearchFolderValidation::UnsetStaleReminders. Unable to unset a stale reminder of the item {0}: {1}", array2[0], arg2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x001247F8 File Offset: 0x001229F8
		private static SearchFolderCriteria CreateRemindersSearchCriteria(DefaultFolderContext context)
		{
			return RemindersSearchFolderValidation.CreateRemindersQueryForO12(context, null);
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x00124804 File Offset: 0x00122A04
		private static SearchFolderCriteria CreateRemindersQueryForO12(DefaultFolderContext context, IEnumerable<QueryFilter> currentExclusionCriteria)
		{
			QueryFilter searchQuery = new AndFilter(new QueryFilter[]
			{
				SearchFolderValidation.GetSearchExclusionFoldersFilter(context, currentExclusionCriteria, SearchFolderValidation.ExcludeFromRemindersSearchFolder),
				new AndFilter(new QueryFilter[]
				{
					new NotFilter(new AndFilter(new QueryFilter[]
					{
						new ExistsFilter(InternalSchema.ItemClass),
						new TextFilter(InternalSchema.ItemClass, "IPM.Schedule", MatchOptions.Prefix, MatchFlags.Default)
					})),
					new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.HasBeenSubmitted, false),
					RemindersSearchFolderValidation.CreateRemindersQueryForO11(context).SearchQuery
				})
			});
			return new SearchFolderCriteria(searchQuery, new StoreId[]
			{
				context.Session.GetDefaultFolderId(DefaultFolderType.Root)
			})
			{
				DeepTraversal = true
			};
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x001248C4 File Offset: 0x00122AC4
		private static SearchFolderCriteria CreateRemindersQueryForO11(DefaultFolderContext context)
		{
			StoreId[] folderScope = new StoreId[]
			{
				context[DefaultFolderType.Calendar],
				context[DefaultFolderType.Contacts],
				context[DefaultFolderType.Tasks],
				context[DefaultFolderType.Inbox]
			};
			QueryFilter searchQuery = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ReminderIsSetInternal, true),
				new AndFilter(new QueryFilter[]
				{
					new ExistsFilter(InternalSchema.AppointmentRecurring),
					new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.AppointmentRecurring, true)
				})
			});
			return new SearchFolderCriteria(searchQuery, folderScope);
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00124961 File Offset: 0x00122B61
		[Conditional("DEBUG")]
		private static void ThrowInDebug(Exception exception)
		{
			throw exception;
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x00124964 File Offset: 0x00122B64
		private static bool InternalWaitForRemindersIndexing(IAsyncResult initialIndexingDone)
		{
			return initialIndexingDone.AsyncWaitHandle.WaitOne(RemindersSearchFolderValidation.initialRemindersIndexingTimeout, true);
		}

		// Token: 0x04002556 RID: 9558
		private const int MaxRemindersSearchFolderExclusionList = 30;

		// Token: 0x04002557 RID: 9559
		private static readonly TimeSpan initialRemindersIndexingTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x02000688 RID: 1672
		internal static class TestAccess
		{
			// Token: 0x0600448E RID: 17550 RVA: 0x0012498C File Offset: 0x00122B8C
			internal static SearchFolderCriteria CreateRemindersQueryForO12(DefaultFolderContext context, IEnumerable<QueryFilter> currentExclusionCriteria)
			{
				return RemindersSearchFolderValidation.CreateRemindersQueryForO12(context, currentExclusionCriteria);
			}

			// Token: 0x0600448F RID: 17551 RVA: 0x00124995 File Offset: 0x00122B95
			internal static SearchFolderCriteria CreateRemindersQueryForO11(DefaultFolderContext context)
			{
				return RemindersSearchFolderValidation.CreateRemindersQueryForO11(context);
			}

			// Token: 0x06004490 RID: 17552 RVA: 0x001249A0 File Offset: 0x00122BA0
			internal static bool IsReminderSearchFolderForO12(DefaultFolderContext context, SearchFolderCriteria currentCriteria)
			{
				bool flag;
				return RemindersSearchFolderValidation.IsReminderSearchFolderForO12(context, currentCriteria, out flag);
			}
		}

		// Token: 0x02000689 RID: 1673
		private struct RemindersSearchFolderState
		{
			// Token: 0x06004491 RID: 17553 RVA: 0x001249B6 File Offset: 0x00122BB6
			internal RemindersSearchFolderState(RemindersSearchFolderValidation.RemindersSearchFolderVersion version, bool isUpToDate)
			{
				this.Version = version;
				this.IsUpToDate = isUpToDate;
			}

			// Token: 0x06004492 RID: 17554 RVA: 0x001249C6 File Offset: 0x00122BC6
			public override string ToString()
			{
				return string.Format("RemindersSearchFolderState<version = {0}, usUpToDate = {1}>", this.Version, this.IsUpToDate);
			}

			// Token: 0x04002558 RID: 9560
			internal readonly RemindersSearchFolderValidation.RemindersSearchFolderVersion Version;

			// Token: 0x04002559 RID: 9561
			internal readonly bool IsUpToDate;
		}

		// Token: 0x0200068A RID: 1674
		private enum RemindersSearchFolderVersion
		{
			// Token: 0x0400255B RID: 9563
			NotSet,
			// Token: 0x0400255C RID: 9564
			O11,
			// Token: 0x0400255D RID: 9565
			O12,
			// Token: 0x0400255E RID: 9566
			Unknown = 255
		}
	}
}
