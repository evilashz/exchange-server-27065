using System;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000102 RID: 258
	internal sealed class ViewStatePropertySchema : UserConfigurationPropertySchemaBase
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x000205F6 File Offset: 0x0001E7F6
		private ViewStatePropertySchema()
		{
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000205FE File Offset: 0x0001E7FE
		internal static ViewStatePropertySchema Instance
		{
			get
			{
				ViewStatePropertySchema result;
				if ((result = ViewStatePropertySchema.instance) == null)
				{
					result = (ViewStatePropertySchema.instance = new ViewStatePropertySchema());
				}
				return result;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00020614 File Offset: 0x0001E814
		internal override UserConfigurationPropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return ViewStatePropertySchema.propertyDefinitions;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002061B File Offset: 0x0001E81B
		internal override UserConfigurationPropertyId PropertyDefinitionsBaseId
		{
			get
			{
				return UserConfigurationPropertyId.CalendarViewTypeNarrow;
			}
		}

		// Token: 0x0400067F RID: 1663
		private static readonly UserConfigurationPropertyDefinition[] propertyDefinitions = new UserConfigurationPropertyDefinition[]
		{
			new UserConfigurationPropertyDefinition("CalendarViewTypeNarrow", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCalendarViewTypeNarrow)),
			new UserConfigurationPropertyDefinition("CalendarViewTypeWide", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCalendarViewType)),
			new UserConfigurationPropertyDefinition("CalendarViewTypeDesktop", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCalendarViewType)),
			new UserConfigurationPropertyDefinition("CalendarSidePanelIsExpanded", typeof(bool), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCalendarSidePanelIsExpanded)),
			new UserConfigurationPropertyDefinition("FolderViewState", typeof(string[])),
			new UserConfigurationPropertyDefinition("SchedulingViewType", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateSchedulingViewType)),
			new UserConfigurationPropertyDefinition("SchedulingLastUsedRoomListName", typeof(string)),
			new UserConfigurationPropertyDefinition("SchedulingLastUsedRoomListEmailAddress", typeof(string)),
			new UserConfigurationPropertyDefinition("SearchHistory", typeof(string[])),
			new UserConfigurationPropertyDefinition("PeopleHubDisplayOption", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidatePeopleHubDisplayOptionType)),
			new UserConfigurationPropertyDefinition("PeopleHubSortOption", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidatePeopleHubSortOptionType)),
			new UserConfigurationPropertyDefinition("CalendarSidePanelMonthPickerCount", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCalendarSidePanelMonthPickerCount)),
			new UserConfigurationPropertyDefinition("SelectedCalendarsDesktop", typeof(string[])),
			new UserConfigurationPropertyDefinition("SelectedCalendarsTWide", typeof(string[])),
			new UserConfigurationPropertyDefinition("SelectedCalendarsTNarrow", typeof(string[])),
			new UserConfigurationPropertyDefinition("AttachmentsFilePickerViewTypeForMouse", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateAttachmentsFilePickerViewType)),
			new UserConfigurationPropertyDefinition("AttachmentsFilePickerViewTypeForTouch", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateAttachmentsFilePickerViewType)),
			new UserConfigurationPropertyDefinition("BookmarkedWeatherLocations", typeof(string[])),
			new UserConfigurationPropertyDefinition("CurrentWeatherLocationBookmarkIndex", typeof(int), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCurrentWeatherLocationBookmarkIndex)),
			new UserConfigurationPropertyDefinition("TemperatureUnit", typeof(TemperatureUnit), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateTemperatureUnit)),
			new UserConfigurationPropertyDefinition("GlobalFolderViewState", typeof(string)),
			new UserConfigurationPropertyDefinition("CalendarAgendaViewIsExpandedMouse", typeof(bool), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCalendarAgendaViewIsExpandedMouse)),
			new UserConfigurationPropertyDefinition("CalendarAgendaViewIsExpandedTWide", typeof(bool), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateCalendarAgendaViewIsExpandedTWide)),
			new UserConfigurationPropertyDefinition("AttachmentsFilePickerHideBanner", typeof(bool), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateAttachmentsFilePickerHideBanner))
		};

		// Token: 0x04000680 RID: 1664
		private static ViewStatePropertySchema instance;
	}
}
