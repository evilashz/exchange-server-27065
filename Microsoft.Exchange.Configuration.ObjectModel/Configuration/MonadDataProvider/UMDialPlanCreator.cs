using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001BF RID: 447
	internal class UMDialPlanCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"FaxEnabled",
				"CallAnsweringRulesEnabled",
				"VoIPSecurity",
				"UMServers",
				"URIType",
				"HuntGroups",
				"NumberOfDigitsInExtension",
				"WhenChanged",
				"WelcomeGreetingFilename",
				"WelcomeGreetingEnabled",
				"InfoAnnouncementFilename",
				"InfoAnnouncementEnabled",
				"AccessTelephoneNumbers",
				"OutsideLineAccessCode",
				"InternationalAccessCode",
				"NationalNumberPrefix",
				"CountryOrRegionCode",
				"InCountryOrRegionNumberFormat",
				"InternationalNumberFormat",
				"CallSomeoneEnabled",
				"SendVoiceMsgEnabled",
				"ContactScope",
				"Extension",
				"ContactAddressList",
				"UMAutoAttendant",
				"MatchedNameSelectionMethod",
				"UMAutoAttendants",
				"DialByNamePrimary",
				"DialByNameSecondary",
				"AudioCodec",
				"OperatorExtension",
				"LogonFailuresBeforeDisconnect",
				"MaxCallDuration",
				"MaxRecordingDuration",
				"RecordingIdleTimeout",
				"InputFailuresBeforeDisconnect",
				"AvailableLanguages",
				"DefaultLanguage",
				"ConfiguredInCountryOrRegionGroups",
				"ConfiguredInternationalGroups",
				"AllowDialPlanSubscribers",
				"AllowExtensions",
				"AllowedInCountryOrRegionGroups",
				"AllowedInternationalGroups",
				"PilotIdentifierList"
			};
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003016C File Offset: 0x0002E36C
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "CallAnsweringRulesEnabled")
			{
				PropertyInfo property = configObject.GetType().GetProperty(propertyName);
				property.SetValue(configObject, MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, property.PropertyType), null);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
