using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F2 RID: 1010
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OwaViewStateConfiguration : UserConfigurationBaseType
	{
		// Token: 0x06002099 RID: 8345 RVA: 0x00078EBC File Offset: 0x000770BC
		public OwaViewStateConfiguration() : base(OwaViewStateConfiguration.configurationName)
		{
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00078EC9 File Offset: 0x000770C9
		// (set) Token: 0x0600209B RID: 8347 RVA: 0x00078EDB File Offset: 0x000770DB
		[DataMember]
		public TemperatureUnit TemperatureUnit
		{
			get
			{
				return (TemperatureUnit)base[UserConfigurationPropertyId.TemperatureUnit];
			}
			set
			{
				base[UserConfigurationPropertyId.TemperatureUnit] = value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x00078EEE File Offset: 0x000770EE
		// (set) Token: 0x0600209D RID: 8349 RVA: 0x00078F00 File Offset: 0x00077100
		[DataMember]
		public string[] BookmarkedWeatherLocations
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.BookmarkedWeatherLocations];
			}
			set
			{
				base[UserConfigurationPropertyId.BookmarkedWeatherLocations] = value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00078F0E File Offset: 0x0007710E
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x00078F1D File Offset: 0x0007711D
		[DataMember]
		public int CalendarViewTypeNarrow
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.CalendarViewTypeNarrow];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarViewTypeNarrow] = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00078F2D File Offset: 0x0007712D
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x00078F3C File Offset: 0x0007713C
		[DataMember]
		public int CalendarViewTypeWide
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.CalendarViewTypeWide];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarViewTypeWide] = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00078F4C File Offset: 0x0007714C
		// (set) Token: 0x060020A3 RID: 8355 RVA: 0x00078F5B File Offset: 0x0007715B
		[DataMember]
		public int CalendarViewTypeDesktop
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.CalendarViewTypeDesktop];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarViewTypeDesktop] = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00078F6B File Offset: 0x0007716B
		// (set) Token: 0x060020A5 RID: 8357 RVA: 0x00078F7A File Offset: 0x0007717A
		[DataMember]
		public bool CalendarSidePanelIsExpanded
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.CalendarSidePanelIsExpanded];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarSidePanelIsExpanded] = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x00078F8A File Offset: 0x0007718A
		// (set) Token: 0x060020A7 RID: 8359 RVA: 0x00078F99 File Offset: 0x00077199
		[DataMember]
		public int CalendarSidePanelMonthPickerCount
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.CalendarSidePanelMonthPickerCount];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarSidePanelMonthPickerCount] = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00078FA9 File Offset: 0x000771A9
		// (set) Token: 0x060020A9 RID: 8361 RVA: 0x00078FBB File Offset: 0x000771BB
		[DataMember]
		public int CurrentWeatherLocationBookmarkIndex
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.CurrentWeatherLocationBookmarkIndex];
			}
			set
			{
				base[UserConfigurationPropertyId.CurrentWeatherLocationBookmarkIndex] = value;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00078FCE File Offset: 0x000771CE
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x00078FDD File Offset: 0x000771DD
		[DataMember]
		public string[] SelectedCalendarsDesktop
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.SelectedCalendarsDesktop];
			}
			set
			{
				base[UserConfigurationPropertyId.SelectedCalendarsDesktop] = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x00078FE8 File Offset: 0x000771E8
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x00078FF7 File Offset: 0x000771F7
		[DataMember]
		public string[] SelectedCalendarsTWide
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.SelectedCalendarsTWide];
			}
			set
			{
				base[UserConfigurationPropertyId.SelectedCalendarsTWide] = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x00079002 File Offset: 0x00077202
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x00079011 File Offset: 0x00077211
		[DataMember]
		public string[] SelectedCalendarsTNarrow
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.SelectedCalendarsTNarrow];
			}
			set
			{
				base[UserConfigurationPropertyId.SelectedCalendarsTNarrow] = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0007901C File Offset: 0x0007721C
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x0007902B File Offset: 0x0007722B
		[DataMember]
		public string[] FolderViewState
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.FolderViewState];
			}
			set
			{
				base[UserConfigurationPropertyId.FolderViewState] = value;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00079036 File Offset: 0x00077236
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x00079048 File Offset: 0x00077248
		[DataMember]
		public string GlobalFolderViewState
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.GlobalFolderViewState];
			}
			set
			{
				base[UserConfigurationPropertyId.GlobalFolderViewState] = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x00079056 File Offset: 0x00077256
		// (set) Token: 0x060020B5 RID: 8373 RVA: 0x00079065 File Offset: 0x00077265
		[DataMember]
		public int SchedulingViewType
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.SchedulingViewType];
			}
			set
			{
				base[UserConfigurationPropertyId.SchedulingViewType] = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x00079075 File Offset: 0x00077275
		// (set) Token: 0x060020B7 RID: 8375 RVA: 0x00079084 File Offset: 0x00077284
		[DataMember]
		public string SchedulingLastUsedRoomListName
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SchedulingLastUsedRoomListName];
			}
			set
			{
				base[UserConfigurationPropertyId.SchedulingLastUsedRoomListName] = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x0007908F File Offset: 0x0007728F
		// (set) Token: 0x060020B9 RID: 8377 RVA: 0x0007909E File Offset: 0x0007729E
		[DataMember]
		public string SchedulingLastUsedRoomListEmailAddress
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SchedulingLastUsedRoomListEmailAddress];
			}
			set
			{
				base[UserConfigurationPropertyId.SchedulingLastUsedRoomListEmailAddress] = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x000790A9 File Offset: 0x000772A9
		// (set) Token: 0x060020BB RID: 8379 RVA: 0x000790B8 File Offset: 0x000772B8
		[DataMember]
		public string[] SearchHistory
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.SearchHistory];
			}
			set
			{
				base[UserConfigurationPropertyId.SearchHistory] = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x000790C3 File Offset: 0x000772C3
		// (set) Token: 0x060020BD RID: 8381 RVA: 0x000790D2 File Offset: 0x000772D2
		[DataMember]
		public int PeopleHubDisplayOption
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.PeopleHubDisplayOption];
			}
			set
			{
				base[UserConfigurationPropertyId.PeopleHubDisplayOption] = value;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x000790E2 File Offset: 0x000772E2
		// (set) Token: 0x060020BF RID: 8383 RVA: 0x000790F1 File Offset: 0x000772F1
		[DataMember]
		public int PeopleHubSortOption
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.PeopleHubSortOption];
			}
			set
			{
				base[UserConfigurationPropertyId.PeopleHubSortOption] = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x00079101 File Offset: 0x00077301
		// (set) Token: 0x060020C1 RID: 8385 RVA: 0x00079110 File Offset: 0x00077310
		[DataMember]
		public int AttachmentsFilePickerViewTypeForMouse
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.AttachmentsFilePickerViewTypeForMouse];
			}
			set
			{
				base[UserConfigurationPropertyId.AttachmentsFilePickerViewTypeForMouse] = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x00079120 File Offset: 0x00077320
		// (set) Token: 0x060020C3 RID: 8387 RVA: 0x00079132 File Offset: 0x00077332
		[DataMember]
		public int AttachmentsFilePickerViewTypeForTouch
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.AttachmentsFilePickerViewTypeForTouch];
			}
			set
			{
				base[UserConfigurationPropertyId.AttachmentsFilePickerViewTypeForTouch] = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x00079145 File Offset: 0x00077345
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x00079157 File Offset: 0x00077357
		[DataMember]
		public bool AttachmentsFilePickerHideBanner
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.AttachmentsFilePickerHideBanner];
			}
			set
			{
				base[UserConfigurationPropertyId.AttachmentsFilePickerHideBanner] = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0007916A File Offset: 0x0007736A
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x0007917C File Offset: 0x0007737C
		[DataMember]
		public bool CalendarAgendaViewIsExpandedMouse
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.CalendarAgendaViewIsExpandedMouse];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarAgendaViewIsExpandedMouse] = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0007918F File Offset: 0x0007738F
		// (set) Token: 0x060020C9 RID: 8393 RVA: 0x000791A1 File Offset: 0x000773A1
		[DataMember]
		public bool CalendarAgendaViewIsExpandedTWide
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.CalendarAgendaViewIsExpandedTWide];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarAgendaViewIsExpandedTWide] = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x000791B4 File Offset: 0x000773B4
		internal override UserConfigurationPropertySchemaBase Schema
		{
			get
			{
				return ViewStatePropertySchema.Instance;
			}
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000791BC File Offset: 0x000773BC
		internal void LoadAll(MailboxSession session)
		{
			IList<UserConfigurationPropertyDefinition> properties = new List<UserConfigurationPropertyDefinition>(base.OptionProperties.Keys);
			base.Load(session, properties, true);
		}

		// Token: 0x04001272 RID: 4722
		private static string configurationName = "OWA.ViewStateConfiguration";
	}
}
