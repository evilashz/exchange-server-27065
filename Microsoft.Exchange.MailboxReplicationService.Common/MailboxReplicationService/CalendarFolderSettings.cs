using System;
using System.Collections;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	[DataContract]
	internal class CalendarFolderSettings : ItemPropertiesBase
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022C4 File Offset: 0x000004C4
		public OlcCalendarType OlcCalendarType
		{
			get
			{
				return (OlcCalendarType)this.CalendarType;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022CC File Offset: 0x000004CC
		public override void Apply(CoreFolder folder)
		{
			using (UserConfiguration mailboxConfiguration = this.GetMailboxConfiguration(folder))
			{
				IDictionary dictionary = mailboxConfiguration.GetDictionary();
				dictionary["OlcIsVisible"] = this.IsVisible;
				dictionary["OlcIsHidden"] = this.IsHidden;
				dictionary["OlcColorIndex"] = this.ColorIndex;
				dictionary["OlcIsDailySummaryEnabled"] = this.IsDailySummaryEnabled;
				dictionary["ConsumerTaskPermissionLevel"] = this.ConsumerTaskPermissionLevel;
				dictionary["OlcConsecutiveErrorCount"] = this.ConsecutiveErrorCount;
				dictionary["OlcTotalErrorCount"] = this.TotalErrorCount;
				dictionary["OlcPollingInterval"] = this.PollingInterval;
				dictionary["OlcEntityTag"] = (this.EntityTag ?? string.Empty);
				dictionary["OlcImportedEventCount"] = this.ImportedEventCount;
				dictionary["OlcTotalEventCount"] = this.TotalEventCount;
				dictionary["OlcUpdateStatus"] = this.UpdateStatus;
				dictionary["OlcMissingUidCount"] = this.MissingUidCount;
				dictionary["OlcConsecutiveCriticalErrorCount"] = this.ConsecutiveCriticalErrorCount;
				dictionary["OlcPersonIdMigrated"] = this.PersonIdMigrated;
				ConflictResolutionResult conflictResolutionResult = mailboxConfiguration.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new ConversionFailedException(ConversionFailureReason.ConverterInternalFailure, MrsStrings.ReportCalendarFolderFaiSaveFailed, null);
				}
				MrsTracer.Provider.Debug("Calendar folder user configuration has been updated.", new object[0]);
			}
			folder.PropertyBag.Load(CalendarFolderSchema.Instance.AutoloadProperties);
			if (this.OlcCalendarType != OlcCalendarType.RegularEvents)
			{
				folder.PropertyBag[FolderSchema.DisplayName] = (this.Name ?? string.Empty);
			}
			folder.PropertyBag[CalendarFolderSchema.CharmId] = (this.CharmId ?? string.Empty);
			FolderSaveResult folderSaveResult = folder.Save(SaveMode.NoConflictResolution);
			if (folderSaveResult.OperationResult == OperationResult.Failed)
			{
				throw new ConversionFailedException(ConversionFailureReason.ConverterInternalFailure, MrsStrings.ReportCalendarFolderSaveFailed, null);
			}
			MrsTracer.Provider.Debug("Calendar folder has been updated with settings.", new object[0]);
			folder.PropertyBag.Load(CalendarFolderSchema.Instance.AutoloadProperties);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002534 File Offset: 0x00000734
		protected UserConfiguration GetMailboxConfiguration(CoreFolder folder)
		{
			UserConfigurationManager userConfigurationManager = ((MailboxSession)folder.Session).UserConfigurationManager;
			UserConfiguration result;
			try
			{
				result = userConfigurationManager.GetFolderConfiguration("OlcCalendarFolderSettings", UserConfigurationTypes.Dictionary, folder.Id);
			}
			catch (ObjectNotFoundException)
			{
				result = userConfigurationManager.CreateFolderConfiguration("OlcCalendarFolderSettings", UserConfigurationTypes.Dictionary, folder.Id);
			}
			return result;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002590 File Offset: 0x00000790
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002598 File Offset: 0x00000798
		[DataMember(Name = "CalendarType")]
		public int CalendarType { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025A1 File Offset: 0x000007A1
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000025A9 File Offset: 0x000007A9
		[DataMember(Name = "HolidayLocale")]
		public string HolidayLocale { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025B2 File Offset: 0x000007B2
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025BA File Offset: 0x000007BA
		[DataMember(Name = "ExternalCalendarLocation")]
		public string ExternalCalendarLocation { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025C3 File Offset: 0x000007C3
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000025CB File Offset: 0x000007CB
		[DataMember(Name = "IsVisible")]
		public bool IsVisible { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000025DC File Offset: 0x000007DC
		[DataMember(Name = "IsHidden")]
		public bool IsHidden { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025E5 File Offset: 0x000007E5
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000025ED File Offset: 0x000007ED
		[DataMember(Name = "ColorIndex")]
		public int ColorIndex { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000025F6 File Offset: 0x000007F6
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000025FE File Offset: 0x000007FE
		[DataMember(Name = "Name")]
		public string Name { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002607 File Offset: 0x00000807
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000260F File Offset: 0x0000080F
		[DataMember(Name = "CharmId")]
		public string CharmId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002618 File Offset: 0x00000818
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002620 File Offset: 0x00000820
		[DataMember(Name = "IsDailySummaryEnabled")]
		public bool IsDailySummaryEnabled { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002629 File Offset: 0x00000829
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002631 File Offset: 0x00000831
		[DataMember(Name = "ConsumerTaskPermissionLevel")]
		public int ConsumerTaskPermissionLevel { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000263A File Offset: 0x0000083A
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002642 File Offset: 0x00000842
		[DataMember(Name = "Ordinal")]
		public int Ordinal { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000264B File Offset: 0x0000084B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002653 File Offset: 0x00000853
		[DataMember(Name = "EmailRemindersDisabled")]
		public bool EmailRemindersDisabled { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000265C File Offset: 0x0000085C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002664 File Offset: 0x00000864
		[DataMember(Name = "ConsecutiveErrorCount")]
		public int ConsecutiveErrorCount { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000266D File Offset: 0x0000086D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002675 File Offset: 0x00000875
		[DataMember(Name = "TotalErrorCount")]
		public int TotalErrorCount { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000267E File Offset: 0x0000087E
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002686 File Offset: 0x00000886
		[DataMember(Name = "PollingInterval")]
		public int PollingInterval { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000268F File Offset: 0x0000088F
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002697 File Offset: 0x00000897
		[DataMember(Name = "EntityTag")]
		public string EntityTag { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000026A0 File Offset: 0x000008A0
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000026A8 File Offset: 0x000008A8
		[DataMember(Name = "ImportedEventCount")]
		public int ImportedEventCount { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000026B1 File Offset: 0x000008B1
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000026B9 File Offset: 0x000008B9
		[DataMember(Name = "TotalEventCount")]
		public int TotalEventCount { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000026C2 File Offset: 0x000008C2
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000026CA File Offset: 0x000008CA
		[DataMember(Name = "UpdateStatus")]
		public int UpdateStatus { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000026D3 File Offset: 0x000008D3
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000026DB File Offset: 0x000008DB
		[DataMember(Name = "ExternalIdOfCalendar")]
		public string ExternalIdOfCalendar { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000026E4 File Offset: 0x000008E4
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000026EC File Offset: 0x000008EC
		[DataMember(Name = "MissingUidCount")]
		public int MissingUidCount { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000026F5 File Offset: 0x000008F5
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000026FD File Offset: 0x000008FD
		[DataMember(Name = "ConsecutiveCriticalErrorCount")]
		public int ConsecutiveCriticalErrorCount { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002706 File Offset: 0x00000906
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000270E File Offset: 0x0000090E
		[DataMember(Name = "PersonIdMigrated")]
		public bool PersonIdMigrated { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002717 File Offset: 0x00000917
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000271F File Offset: 0x0000091F
		[DataMember(Name = "ConsumerSharingCalendarSubscriptionCount")]
		public int ConsumerSharingCalendarSubscriptionCount { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002728 File Offset: 0x00000928
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002730 File Offset: 0x00000930
		[DataMember(Name = "ConsumerCalendarGuid")]
		public string ConsumerCalendarGuid { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002739 File Offset: 0x00000939
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002741 File Offset: 0x00000941
		[DataMember(Name = "ConsumerCalendarOwnerId")]
		public long ConsumerCalendarOwnerId { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000274A File Offset: 0x0000094A
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002752 File Offset: 0x00000952
		[DataMember(Name = "ConsumerCalendarPrivateFreeBusyId")]
		public string ConsumerCalendarPrivateFreeBusyId { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000275B File Offset: 0x0000095B
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002763 File Offset: 0x00000963
		[DataMember(Name = "ConsumerCalendarPrivateDetailId")]
		public string ConsumerCalendarPrivateDetailId { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002774 File Offset: 0x00000974
		[DataMember(Name = "ConsumerCalendarPublishVisibility")]
		public int ConsumerCalendarPublishVisibility { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000277D File Offset: 0x0000097D
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002785 File Offset: 0x00000985
		[DataMember(Name = "ConsumerCalendarSharingInvitations")]
		public string ConsumerCalendarSharingInvitations { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000278E File Offset: 0x0000098E
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002796 File Offset: 0x00000996
		[DataMember(Name = "ConsumerCalendarPermissionLevel")]
		public int ConsumerCalendarPermissionLevel { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000279F File Offset: 0x0000099F
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000027A7 File Offset: 0x000009A7
		[DataMember(Name = "ConsumerCalendarSynchronizationState")]
		public string ConsumerCalendarSynchronizationState { get; set; }

		// Token: 0x04000018 RID: 24
		public const string ConfigurationName = "OlcCalendarFolderSettings";

		// Token: 0x04000019 RID: 25
		public const string OlcIsVisible = "OlcIsVisible";

		// Token: 0x0400001A RID: 26
		public const string OlcIsHidden = "OlcIsHidden";

		// Token: 0x0400001B RID: 27
		public const string OlcColorIndex = "OlcColorIndex";

		// Token: 0x0400001C RID: 28
		public const string OlcConsumerTaskPermissionLevel = "ConsumerTaskPermissionLevel";

		// Token: 0x0400001D RID: 29
		public const string OlcIsDailySummaryEnabled = "OlcIsDailySummaryEnabled";

		// Token: 0x0400001E RID: 30
		public const string OlcConsecutiveErrorCount = "OlcConsecutiveErrorCount";

		// Token: 0x0400001F RID: 31
		public const string OlcTotalErrorCount = "OlcTotalErrorCount";

		// Token: 0x04000020 RID: 32
		public const string OlcPollingInterval = "OlcPollingInterval";

		// Token: 0x04000021 RID: 33
		public const string OlcEntityTag = "OlcEntityTag";

		// Token: 0x04000022 RID: 34
		public const string OlcImportedEventCount = "OlcImportedEventCount";

		// Token: 0x04000023 RID: 35
		public const string OlcTotalEventCount = "OlcTotalEventCount";

		// Token: 0x04000024 RID: 36
		public const string OlcUpdateStatus = "OlcUpdateStatus";

		// Token: 0x04000025 RID: 37
		public const string OlcMissingUidCount = "OlcMissingUidCount";

		// Token: 0x04000026 RID: 38
		public const string OlcConsecutiveCriticalErrorCount = "OlcConsecutiveCriticalErrorCount";

		// Token: 0x04000027 RID: 39
		public const string OlcPersonIdMigrated = "OlcPersonIdMigrated";
	}
}
