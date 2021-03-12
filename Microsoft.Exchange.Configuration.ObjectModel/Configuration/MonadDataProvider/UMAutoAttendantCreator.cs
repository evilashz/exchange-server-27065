using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C2 RID: 450
	internal class UMAutoAttendantCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00030400 File Offset: 0x0002E600
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"SpeechEnabled",
				"DTMFFallbackAutoAttendant",
				"PilotIdentifierList",
				"NameLookupEnabled",
				"BusinessName",
				"BusinessLocation",
				"UMDialPlan",
				"Status",
				"BusinessHoursWelcomeGreetingFilename",
				"BusinessHoursWelcomeGreetingEnabled",
				"AfterHoursWelcomeGreetingFilename",
				"AfterHoursWelcomeGreetingEnabled",
				"InfoAnnouncementFilename",
				"InfoAnnouncementEnabled",
				"BusinessHoursMainMenuCustomPromptFilename",
				"BusinessHoursMainMenuCustomPromptEnabled",
				"AfterHoursMainMenuCustomPromptFilename",
				"AfterHoursMainMenuCustomPromptEnabled",
				"BusinessHoursSchedule",
				"HolidaySchedule",
				"TimeZone",
				"OperatorExtension",
				"BusinessHoursTransferToOperatorEnabled",
				"AfterHoursTransferToOperatorEnabled",
				"CallSomeoneEnabled",
				"SendVoiceMsgEnabled",
				"ContactScope",
				"ContactAddressList",
				"MatchedNameSelectionMethod",
				"Language",
				"BusinessHoursKeyMappingEnabled",
				"BusinessHoursKeyMapping",
				"AfterHoursKeyMappingEnabled",
				"AfterHoursKeyMapping",
				"AllowDialPlanSubscribers",
				"AllowExtensions",
				"AllowedInCountryOrRegionGroups",
				"AllowedInternationalGroups",
				"WhenChanged"
			};
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003057E File Offset: 0x0002E77E
		protected override void FillProperties(Type type, PSObject psObject, object dummyObject, IList<string> properties)
		{
			this.FillProperty(type, psObject, dummyObject as ConfigurableObject, "TimeZoneName");
			base.FillProperties(type, psObject, dummyObject, properties);
		}
	}
}
