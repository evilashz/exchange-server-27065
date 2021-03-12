using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C1 RID: 449
	internal class UMMailboxPolicyCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x00030228 File Offset: 0x0002E428
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"MaxGreetingDuration",
				"FaxServerURI",
				"AllowMissedCallNotifications",
				"AllowMessageWaitingIndicator",
				"AllowFax",
				"AllowVoiceMailPreview",
				"AllowSubscriberAccess",
				"AllowPlayOnPhone",
				"AllowCallAnsweringRules",
				"UMDialPlan",
				"WhenChanged",
				"UMEnabledText",
				"ResetPINText",
				"VoiceMailText",
				"FaxMessageText",
				"MinPINLength",
				"PINLifetime",
				"PINHistoryCount",
				"AllowCommonPatterns",
				"LogonFailuresBeforePINReset",
				"MaxLogonAttempts",
				"AllowDialPlanSubscribers",
				"AllowExtensions",
				"AllowedInCountryOrRegionGroups",
				"AllowedInternationalGroups",
				"UMDialPlan",
				"ProtectUnauthenticatedVoiceMail",
				"ProtectAuthenticatedVoiceMail",
				"RequireProtectedPlayOnPhone",
				"AllowVoiceResponseToOtherMessageTypes",
				"ProtectedVoiceMailText"
			};
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00030360 File Offset: 0x0002E560
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "AllowCallAnsweringRules")
			{
				configObject.propertyBag[UMMailboxPolicySchema.AllowPersonalAutoAttendant] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, UMMailboxPolicySchema.AllowPersonalAutoAttendant.Type);
				return;
			}
			if (propertyName == "AllowCommonPatterns")
			{
				configObject.propertyBag[UMMailboxPolicySchema.AllowCommonPatterns] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, typeof(bool));
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
