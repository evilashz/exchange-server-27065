using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000662 RID: 1634
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarVersionStoreValidation : SearchFolderValidation
	{
		// Token: 0x060043B3 RID: 17331 RVA: 0x0011EFAC File Offset: 0x0011D1AC
		internal CalendarVersionStoreValidation() : base(new IValidator[]
		{
			new MatchMapiFolderType(FolderType.Search)
		})
		{
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x0011EFD0 File Offset: 0x0011D1D0
		private static QueryFilter[] GetItemSubClassQueryFilter(IList<string> itemClasses)
		{
			QueryFilter[] array = new QueryFilter[itemClasses.Count * 2];
			for (int i = 0; i < itemClasses.Count; i++)
			{
				string text = itemClasses[i];
				array[i * 2] = new TextFilter(InternalSchema.ItemClass, text, MatchOptions.ExactPhrase, MatchFlags.IgnoreCase);
				array[i * 2 + 1] = new TextFilter(InternalSchema.ItemClass, string.Format("{0}.", text), MatchOptions.Prefix, MatchFlags.IgnoreCase);
			}
			return array;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x0011F038 File Offset: 0x0011D238
		internal static SearchFolderCriteria CreateCalendarVersionSearchCriteria(DefaultFolderContext context)
		{
			List<StoreId> list = new List<StoreId>();
			list.Add(context[DefaultFolderType.Root]);
			if (context[DefaultFolderType.RecoverableItemsRoot] != null)
			{
				list.Add(context[DefaultFolderType.RecoverableItemsRoot]);
			}
			CalendarVersionStoreValidation.AddAdditionalFoldersForCalendarVersionSearch(list, context);
			return new SearchFolderCriteria(CalendarVersionStoreValidation.GetCalendarVersionQueryFilter(context), list.ToArray())
			{
				DeepTraversal = true
			};
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x0011F094 File Offset: 0x0011D294
		internal override bool EnsureIsValid(DefaultFolderContext context, Folder folder)
		{
			if (!base.EnsureIsValid(context, folder) || !(folder is SearchFolder))
			{
				return false;
			}
			SearchFolder searchFolder = (SearchFolder)folder;
			SearchFolderCriteria searchFolderCriteria = CalendarVersionStoreValidation.CreateCalendarVersionSearchCriteria(context);
			SearchFolderCriteria searchCriteria = searchFolder.GetSearchCriteria();
			if (!SearchFolderValidation.MatchSearchFolderCriteria(searchCriteria, searchFolderCriteria))
			{
				searchFolder.ApplyContinuousSearch(searchFolderCriteria);
			}
			return true;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x0011F0DC File Offset: 0x0011D2DC
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			SearchFolder searchFolder = (SearchFolder)folder;
			searchFolder.Save();
			searchFolder.ApplyContinuousSearch(CalendarVersionStoreValidation.CreateCalendarVersionSearchCriteria(context));
			searchFolder.Load();
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x0011F114 File Offset: 0x0011D314
		private static QueryFilter GetCalendarVersionQueryFilter(DefaultFolderContext context)
		{
			List<string> list = new List<string>
			{
				"IPM.Appointment",
				"IPM.Schedule.Meeting",
				"IPM.Schedule.Inquiry",
				"IPM.Notification.Meeting",
				"IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}"
			};
			if (CalendarVersionStoreValidation.IsIncludeSeriesMeetingMessagesInCVSEnabled(context))
			{
				list.Add("IPM.AppointmentSeries");
				list.Add("IPM.MeetingMessageSeries");
				list.Add("IPM.Parked.MeetingMessage");
			}
			return new OrFilter(CalendarVersionStoreValidation.GetItemSubClassQueryFilter(list));
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x0011F194 File Offset: 0x0011D394
		private static void AddAdditionalFoldersForCalendarVersionSearch(List<StoreId> folderScope, DefaultFolderContext context)
		{
			if (CalendarVersionStoreValidation.IsIncludeSeriesMeetingMessagesInCVSEnabled(context) && context[DefaultFolderType.ParkedMessages] != null)
			{
				folderScope.Add(context[DefaultFolderType.ParkedMessages]);
			}
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x0011F1B8 File Offset: 0x0011D3B8
		private static bool IsIncludeSeriesMeetingMessagesInCVSEnabled(DefaultFolderContext context)
		{
			return context.Session.MailboxOwner.GetConfiguration().CalendarLogging.CalendarLoggingIncludeSeriesMeetingMessagesInCVS.Enabled;
		}
	}
}
