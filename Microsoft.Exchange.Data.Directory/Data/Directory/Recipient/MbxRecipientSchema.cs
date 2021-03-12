using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000151 RID: 337
	internal class MbxRecipientSchema : ObjectSchema
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x00043C38 File Offset: 0x00041E38
		public MbxRecipientSchema()
		{
			this.InitializePropertyTagMap();
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00043C48 File Offset: 0x00041E48
		private void InitializePropertyTagMap()
		{
			this.mbxPropertyDefinitionsDictionary = new Dictionary<PropTag, MbxPropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in base.AllProperties)
			{
				MbxPropertyDefinition mbxPropertyDefinition = propertyDefinition as MbxPropertyDefinition;
				if (mbxPropertyDefinition != null && mbxPropertyDefinition.PropTag != PropTag.Null)
				{
					this.mbxPropertyDefinitionsDictionary[mbxPropertyDefinition.PropTag] = mbxPropertyDefinition;
				}
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00043CC4 File Offset: 0x00041EC4
		internal MbxPropertyDefinition FindPropertyDefinitionByPropTag(PropTag propTag)
		{
			return this.mbxPropertyDefinitionsDictionary[propTag];
		}

		// Token: 0x04000754 RID: 1876
		public static readonly MbxPropertyDefinition DisplayName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.DisplayName, "DisplayName", false);

		// Token: 0x04000755 RID: 1877
		public static readonly MbxPropertyDefinition ActiveSyncAllowedDeviceIDs = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationActiveSyncAllowedDeviceIDs, "ActiveSyncAllowedDeviceIDs", true);

		// Token: 0x04000756 RID: 1878
		public static readonly MbxPropertyDefinition ActiveSyncBlockedDeviceIDs = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationActiveSyncBlockedDeviceIDs, "ActiveSyncBlockedDeviceIDs", true);

		// Token: 0x04000757 RID: 1879
		public static readonly MbxPropertyDefinition ActiveSyncDebugLogging = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationActiveSyncDebugLogging, "ActiveSyncDebugLogging", false);

		// Token: 0x04000758 RID: 1880
		public static readonly MbxPropertyDefinition UserInformationActiveSyncEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationActiveSyncEnabled, null, false);

		// Token: 0x04000759 RID: 1881
		public static readonly MbxPropertyDefinition ActiveSyncEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ActiveSyncEnabled", MbxRecipientSchema.UserInformationActiveSyncEnabled, false);

		// Token: 0x0400075A RID: 1882
		public static readonly MbxPropertyDefinition AdminDisplayName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.ProviderDllName, "AdminDisplayName", false);

		// Token: 0x0400075B RID: 1883
		public static readonly MbxPropertyDefinition AggregationSubscriptionCredential = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationAggregationSubscriptionCredential, "AggregationSubscriptionCredential", true);

		// Token: 0x0400075C RID: 1884
		public static readonly MbxPropertyDefinition UserInformationAllowArchiveAddressSync = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationAllowArchiveAddressSync, null, false);

		// Token: 0x0400075D RID: 1885
		public static readonly MbxPropertyDefinition AllowArchiveAddressSync = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("AllowArchiveAddressSync", MbxRecipientSchema.UserInformationAllowArchiveAddressSync, false);

		// Token: 0x0400075E RID: 1886
		public static readonly MbxPropertyDefinition Altitude = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.ProviderOrdinal, "Altitude", false);

		// Token: 0x0400075F RID: 1887
		public static readonly MbxPropertyDefinition UserInformationAntispamBypassEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationAntispamBypassEnabled, null, false);

		// Token: 0x04000760 RID: 1888
		public static readonly MbxPropertyDefinition AntispamBypassEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("AntispamBypassEnabled", MbxRecipientSchema.UserInformationAntispamBypassEnabled, false);

		// Token: 0x04000761 RID: 1889
		public static readonly MbxPropertyDefinition UserInformationArchiveDomain = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationArchiveDomain, null, false);

		// Token: 0x04000762 RID: 1890
		public static readonly MbxPropertyDefinition ArchiveDomain = MbxPropertyDefinition.SmtpDomainFromStringPropertyDefinition("ArchiveDomain", MbxRecipientSchema.UserInformationArchiveDomain, false);

		// Token: 0x04000763 RID: 1891
		public static readonly MbxPropertyDefinition UserInformationArchiveGuid = MbxPropertyDefinition.NullableGuidPropertyDefinition(PropTag.UserInformationArchiveGuid, null, false);

		// Token: 0x04000764 RID: 1892
		public static readonly MbxPropertyDefinition ArchiveGuid = MbxPropertyDefinition.GuidFromNullableGuidPropertyDefinition("ArchiveGuid", MbxRecipientSchema.UserInformationArchiveGuid, false);

		// Token: 0x04000765 RID: 1893
		public static readonly MbxPropertyDefinition ArchiveName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationArchiveName, "ArchiveName", true);

		// Token: 0x04000766 RID: 1894
		public static readonly MbxPropertyDefinition UserInformationArchiveQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.ConversationIdObsolete, "UserInformationArchiveQuota", false);

		// Token: 0x04000767 RID: 1895
		public static readonly MbxPropertyDefinition ArchiveQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("ArchiveQuota", MbxRecipientSchema.UserInformationArchiveQuota, false);

		// Token: 0x04000768 RID: 1896
		public static readonly MbxPropertyDefinition ArchiveRelease = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationArchiveRelease, "ArchiveRelease", false);

		// Token: 0x04000769 RID: 1897
		public static readonly MbxPropertyDefinition UserInformationArchiveStatus = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationArchiveStatus, null, false);

		// Token: 0x0400076A RID: 1898
		public static readonly MbxPropertyDefinition ArchiveStatus = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<ArchiveStatusFlags>("ArchiveStatus", MbxRecipientSchema.UserInformationArchiveStatus, false);

		// Token: 0x0400076B RID: 1899
		public static readonly MbxPropertyDefinition UserInformationArchiveWarningQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationArchiveWarningQuota, null, false);

		// Token: 0x0400076C RID: 1900
		public static readonly MbxPropertyDefinition ArchiveWarningQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("ArchiveWarningQuota", MbxRecipientSchema.UserInformationArchiveWarningQuota, false);

		// Token: 0x0400076D RID: 1901
		public static readonly MbxPropertyDefinition AssistantName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationAssistantName, "AssistantName", false);

		// Token: 0x0400076E RID: 1902
		public static readonly MbxPropertyDefinition Birthdate = MbxPropertyDefinition.NullableDateTimePropertyDefinition(PropTag.UserInformationBirthdate, "Birthdate", false);

		// Token: 0x0400076F RID: 1903
		public static readonly MbxPropertyDefinition UserInformationBypassNestedModerationEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationBypassNestedModerationEnabled, null, false);

		// Token: 0x04000770 RID: 1904
		public static readonly MbxPropertyDefinition BypassNestedModerationEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("BypassNestedModerationEnabled", MbxRecipientSchema.UserInformationBypassNestedModerationEnabled, false);

		// Token: 0x04000771 RID: 1905
		public static readonly MbxPropertyDefinition C = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationC, "C", false);

		// Token: 0x04000772 RID: 1906
		public static readonly MbxPropertyDefinition UserInformationCalendarLoggingQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationCalendarLoggingQuota, null, false);

		// Token: 0x04000773 RID: 1907
		public static readonly MbxPropertyDefinition CalendarLoggingQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("CalendarLoggingQuota", MbxRecipientSchema.UserInformationCalendarLoggingQuota, false);

		// Token: 0x04000774 RID: 1908
		public static readonly MbxPropertyDefinition UserInformationCalendarRepairDisabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationCalendarRepairDisabled, null, false);

		// Token: 0x04000775 RID: 1909
		public static readonly MbxPropertyDefinition CalendarRepairDisabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("CalendarRepairDisabled", MbxRecipientSchema.UserInformationCalendarRepairDisabled, false);

		// Token: 0x04000776 RID: 1910
		public static readonly MbxPropertyDefinition UserInformationCalendarVersionStoreDisabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationCalendarVersionStoreDisabled, null, false);

		// Token: 0x04000777 RID: 1911
		public static readonly MbxPropertyDefinition CalendarVersionStoreDisabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("CalendarVersionStoreDisabled", MbxRecipientSchema.UserInformationCalendarVersionStoreDisabled, false);

		// Token: 0x04000778 RID: 1912
		public static readonly MbxPropertyDefinition City = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationCity, "City", false);

		// Token: 0x04000779 RID: 1913
		public static readonly MbxPropertyDefinition Country = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationCountry, "Country", false);

		// Token: 0x0400077A RID: 1914
		public static readonly MbxPropertyDefinition UserInformationCountryCode = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationCountryCode, null, false);

		// Token: 0x0400077B RID: 1915
		public static readonly MbxPropertyDefinition CountryCode = MbxPropertyDefinition.Int32FromNullableInt32PropertyDefinition("CountryCode", MbxRecipientSchema.UserInformationCountryCode, false);

		// Token: 0x0400077C RID: 1916
		public static readonly MbxPropertyDefinition UserInformationCountryOrRegion = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationCountryOrRegion, null, false);

		// Token: 0x0400077D RID: 1917
		public static readonly MbxPropertyDefinition CountryOrRegion = MbxPropertyDefinition.CountryInfoFromStringPropertyDefinition("CountryOrRegion", MbxRecipientSchema.UserInformationCountryOrRegion, false);

		// Token: 0x0400077E RID: 1918
		public static readonly MbxPropertyDefinition DefaultMailTip = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationDefaultMailTip, "DefaultMailTip", false);

		// Token: 0x0400077F RID: 1919
		public static readonly MbxPropertyDefinition UserInformationDeliverToMailboxAndForward = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationDeliverToMailboxAndForward, null, false);

		// Token: 0x04000780 RID: 1920
		public static readonly MbxPropertyDefinition DeliverToMailboxAndForward = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("DeliverToMailboxAndForward", MbxRecipientSchema.UserInformationDeliverToMailboxAndForward, false);

		// Token: 0x04000781 RID: 1921
		public static readonly MbxPropertyDefinition Description = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationDescription, "Description", true);

		// Token: 0x04000782 RID: 1922
		public static readonly MbxPropertyDefinition UserInformationDisabledArchiveGuid = MbxPropertyDefinition.NullableGuidPropertyDefinition(PropTag.UserInformationDisabledArchiveGuid, null, false);

		// Token: 0x04000783 RID: 1923
		public static readonly MbxPropertyDefinition DisabledArchiveGuid = MbxPropertyDefinition.GuidFromNullableGuidPropertyDefinition("DisabledArchiveGuid", MbxRecipientSchema.UserInformationDisabledArchiveGuid, false);

		// Token: 0x04000784 RID: 1924
		public static readonly MbxPropertyDefinition UserInformationDowngradeHighPriorityMessagesEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationDowngradeHighPriorityMessagesEnabled, null, false);

		// Token: 0x04000785 RID: 1925
		public static readonly MbxPropertyDefinition DowngradeHighPriorityMessagesEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("DowngradeHighPriorityMessagesEnabled", MbxRecipientSchema.UserInformationDowngradeHighPriorityMessagesEnabled, false);

		// Token: 0x04000786 RID: 1926
		public static readonly MbxPropertyDefinition UserInformationECPEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationECPEnabled, null, false);

		// Token: 0x04000787 RID: 1927
		public static readonly MbxPropertyDefinition ECPEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ECPEnabled", MbxRecipientSchema.UserInformationECPEnabled, false);

		// Token: 0x04000788 RID: 1928
		public static readonly MbxPropertyDefinition UserInformationEmailAddressPolicyEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationEmailAddressPolicyEnabled, null, false);

		// Token: 0x04000789 RID: 1929
		public static readonly MbxPropertyDefinition EmailAddressPolicyEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("EmailAddressPolicyEnabled", MbxRecipientSchema.UserInformationEmailAddressPolicyEnabled, false);

		// Token: 0x0400078A RID: 1930
		public static readonly MbxPropertyDefinition EwsAllowEntourage = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationEwsAllowEntourage, "EwsAllowEntourage", false);

		// Token: 0x0400078B RID: 1931
		public static readonly MbxPropertyDefinition EwsAllowMacOutlook = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationEwsAllowMacOutlook, "EwsAllowMacOutlook", false);

		// Token: 0x0400078C RID: 1932
		public static readonly MbxPropertyDefinition EwsAllowOutlook = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationEwsAllowOutlook, "EwsAllowOutlook", false);

		// Token: 0x0400078D RID: 1933
		public static readonly MbxPropertyDefinition UserInformationEwsApplicationAccessPolicy = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationEwsApplicationAccessPolicy, null, false);

		// Token: 0x0400078E RID: 1934
		public static readonly MbxPropertyDefinition EwsApplicationAccessPolicy = MbxPropertyDefinition.NullableEnumFromNullableInt32PropertyDefinition<EwsApplicationAccessPolicy>("EwsApplicationAccessPolicy", MbxRecipientSchema.UserInformationEwsApplicationAccessPolicy, false);

		// Token: 0x0400078F RID: 1935
		public static readonly MbxPropertyDefinition EwsEnabled = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationEwsEnabled, "EwsEnabled", false);

		// Token: 0x04000790 RID: 1936
		public static readonly MbxPropertyDefinition EwsExceptions = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationEwsExceptions, "EwsExceptions", true);

		// Token: 0x04000791 RID: 1937
		public static readonly MbxPropertyDefinition EwsWellKnownApplicationAccessPolicies = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationEwsWellKnownApplicationAccessPolicies, "EwsWellKnownApplicationAccessPolicies", true);

		// Token: 0x04000792 RID: 1938
		public static readonly MbxPropertyDefinition UserInformationExchangeGuid = MbxPropertyDefinition.NullableGuidPropertyDefinition(PropTag.UserInformationExchangeGuid, null, false);

		// Token: 0x04000793 RID: 1939
		public static readonly MbxPropertyDefinition ExchangeGuid = MbxPropertyDefinition.GuidFromNullableGuidPropertyDefinition("ExchangeGuid", MbxRecipientSchema.UserInformationExchangeGuid, false);

		// Token: 0x04000794 RID: 1940
		public static readonly MbxPropertyDefinition UserInformationExternalOofOptions = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationExternalOofOptions, null, false);

		// Token: 0x04000795 RID: 1941
		public static readonly MbxPropertyDefinition ExternalOofOptions = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<ExternalOofOptions>("ExternalOofOptions", MbxRecipientSchema.UserInformationExternalOofOptions, false);

		// Token: 0x04000796 RID: 1942
		public static readonly MbxPropertyDefinition FirstName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationFirstName, "FirstName", false);

		// Token: 0x04000797 RID: 1943
		public static readonly MbxPropertyDefinition UserInformationForwardingSmtpAddress = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationForwardingSmtpAddress, null, false);

		// Token: 0x04000798 RID: 1944
		public static readonly MbxPropertyDefinition ForwardingSmtpAddress = MbxPropertyDefinition.ProxyAddressFromStringPropertyDefinition("ForwardingSmtpAddress", MbxRecipientSchema.UserInformationForwardingSmtpAddress, false);

		// Token: 0x04000799 RID: 1945
		public static readonly MbxPropertyDefinition Gender = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationGender, "Gender", false);

		// Token: 0x0400079A RID: 1946
		public static readonly MbxPropertyDefinition UserInformationGenericForwardingAddress = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationGenericForwardingAddress, null, false);

		// Token: 0x0400079B RID: 1947
		public static readonly MbxPropertyDefinition GenericForwardingAddress = MbxPropertyDefinition.ProxyAddressFromStringPropertyDefinition("GenericForwardingAddress", MbxRecipientSchema.UserInformationGenericForwardingAddress, false);

		// Token: 0x0400079C RID: 1948
		public static readonly MbxPropertyDefinition UserInformationGeoCoordinates = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationGeoCoordinates, null, false);

		// Token: 0x0400079D RID: 1949
		public static readonly MbxPropertyDefinition GeoCoordinates = MbxPropertyDefinition.GeoCoordinatesFromStringPropertyDefinition("GeoCoordinates", MbxRecipientSchema.UserInformationGeoCoordinates, false);

		// Token: 0x0400079E RID: 1950
		public static readonly MbxPropertyDefinition HABSeniorityIndex = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationHABSeniorityIndex, "HABSeniorityIndex", false);

		// Token: 0x0400079F RID: 1951
		public static readonly MbxPropertyDefinition UserInformationHasActiveSyncDevicePartnership = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationHasActiveSyncDevicePartnership, null, false);

		// Token: 0x040007A0 RID: 1952
		public static readonly MbxPropertyDefinition HasActiveSyncDevicePartnership = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("HasActiveSyncDevicePartnership", MbxRecipientSchema.UserInformationHasActiveSyncDevicePartnership, false);

		// Token: 0x040007A1 RID: 1953
		public static readonly MbxPropertyDefinition UserInformationHiddenFromAddressListsEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationHiddenFromAddressListsEnabled, null, false);

		// Token: 0x040007A2 RID: 1954
		public static readonly MbxPropertyDefinition HiddenFromAddressListsEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("HiddenFromAddressListsEnabled", MbxRecipientSchema.UserInformationHiddenFromAddressListsEnabled, false);

		// Token: 0x040007A3 RID: 1955
		public static readonly MbxPropertyDefinition UserInformationHiddenFromAddressListsValue = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationHiddenFromAddressListsValue, null, false);

		// Token: 0x040007A4 RID: 1956
		public static readonly MbxPropertyDefinition HiddenFromAddressListsValue = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("HiddenFromAddressListsValue", MbxRecipientSchema.UserInformationHiddenFromAddressListsValue, false);

		// Token: 0x040007A5 RID: 1957
		public static readonly MbxPropertyDefinition HomePhone = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationHomePhone, "HomePhone", false);

		// Token: 0x040007A6 RID: 1958
		public static readonly MbxPropertyDefinition UserInformationImapEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationImapEnabled, null, false);

		// Token: 0x040007A7 RID: 1959
		public static readonly MbxPropertyDefinition ImapEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ImapEnabled", MbxRecipientSchema.UserInformationImapEnabled, false);

		// Token: 0x040007A8 RID: 1960
		public static readonly MbxPropertyDefinition UserInformationImapEnableExactRFC822Size = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationImapEnableExactRFC822Size, null, false);

		// Token: 0x040007A9 RID: 1961
		public static readonly MbxPropertyDefinition ImapEnableExactRFC822Size = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ImapEnableExactRFC822Size", MbxRecipientSchema.UserInformationImapEnableExactRFC822Size, false);

		// Token: 0x040007AA RID: 1962
		public static readonly MbxPropertyDefinition UserInformationImapForceICalForCalendarRetrievalOption = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationImapForceICalForCalendarRetrievalOption, null, false);

		// Token: 0x040007AB RID: 1963
		public static readonly MbxPropertyDefinition ImapForceICalForCalendarRetrievalOption = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ImapForceICalForCalendarRetrievalOption", MbxRecipientSchema.UserInformationImapForceICalForCalendarRetrievalOption, false);

		// Token: 0x040007AC RID: 1964
		public static readonly MbxPropertyDefinition UserInformationImapMessagesRetrievalMimeFormat = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationImapMessagesRetrievalMimeFormat, null, false);

		// Token: 0x040007AD RID: 1965
		public static readonly MbxPropertyDefinition ImapMessagesRetrievalMimeFormat = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<MimeTextFormat>("ImapMessagesRetrievalMimeFormat", MbxRecipientSchema.UserInformationImapMessagesRetrievalMimeFormat, false);

		// Token: 0x040007AE RID: 1966
		public static readonly MbxPropertyDefinition ImapProtocolLoggingEnabled = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationImapProtocolLoggingEnabled, "ImapProtocolLoggingEnabled", false);

		// Token: 0x040007AF RID: 1967
		public static readonly MbxPropertyDefinition UserInformationImapSuppressReadReceipt = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationImapSuppressReadReceipt, null, false);

		// Token: 0x040007B0 RID: 1968
		public static readonly MbxPropertyDefinition ImapSuppressReadReceipt = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ImapSuppressReadReceipt", MbxRecipientSchema.UserInformationImapSuppressReadReceipt, false);

		// Token: 0x040007B1 RID: 1969
		public static readonly MbxPropertyDefinition UserInformationImapUseProtocolDefaults = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationImapUseProtocolDefaults, null, false);

		// Token: 0x040007B2 RID: 1970
		public static readonly MbxPropertyDefinition ImapUseProtocolDefaults = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ImapUseProtocolDefaults", MbxRecipientSchema.UserInformationImapUseProtocolDefaults, false);

		// Token: 0x040007B3 RID: 1971
		public static readonly MbxPropertyDefinition UserInformationIncludeInGarbageCollection = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIncludeInGarbageCollection, null, false);

		// Token: 0x040007B4 RID: 1972
		public static readonly MbxPropertyDefinition IncludeInGarbageCollection = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IncludeInGarbageCollection", MbxRecipientSchema.UserInformationIncludeInGarbageCollection, false);

		// Token: 0x040007B5 RID: 1973
		public static readonly MbxPropertyDefinition Initials = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationInitials, "Initials", false);

		// Token: 0x040007B6 RID: 1974
		public static readonly MbxPropertyDefinition InPlaceHolds = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationInPlaceHolds, "InPlaceHolds", true);

		// Token: 0x040007B7 RID: 1975
		public static readonly MbxPropertyDefinition UserInformationInternalOnly = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationInternalOnly, null, false);

		// Token: 0x040007B8 RID: 1976
		public static readonly MbxPropertyDefinition InternalOnly = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("InternalOnly", MbxRecipientSchema.UserInformationInternalOnly, false);

		// Token: 0x040007B9 RID: 1977
		public static readonly MbxPropertyDefinition InternalUsageLocation = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationInternalUsageLocation, "InternalUsageLocation", false);

		// Token: 0x040007BA RID: 1978
		public static readonly MbxPropertyDefinition UserInformationInternetEncoding = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationInternetEncoding, null, false);

		// Token: 0x040007BB RID: 1979
		public static readonly MbxPropertyDefinition InternetEncoding = MbxPropertyDefinition.Int32FromNullableInt32PropertyDefinition("InternetEncoding", MbxRecipientSchema.UserInformationInternetEncoding, false);

		// Token: 0x040007BC RID: 1980
		public static readonly MbxPropertyDefinition UserInformationIsCalculatedTargetAddress = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsCalculatedTargetAddress, null, false);

		// Token: 0x040007BD RID: 1981
		public static readonly MbxPropertyDefinition IsCalculatedTargetAddress = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsCalculatedTargetAddress", MbxRecipientSchema.UserInformationIsCalculatedTargetAddress, false);

		// Token: 0x040007BE RID: 1982
		public static readonly MbxPropertyDefinition UserInformationIsExcludedFromServingHierarchy = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsExcludedFromServingHierarchy, null, false);

		// Token: 0x040007BF RID: 1983
		public static readonly MbxPropertyDefinition IsExcludedFromServingHierarchy = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsExcludedFromServingHierarchy", MbxRecipientSchema.UserInformationIsExcludedFromServingHierarchy, false);

		// Token: 0x040007C0 RID: 1984
		public static readonly MbxPropertyDefinition UserInformationIsHierarchyReady = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsHierarchyReady, null, false);

		// Token: 0x040007C1 RID: 1985
		public static readonly MbxPropertyDefinition IsHierarchyReady = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsHierarchyReady", MbxRecipientSchema.UserInformationIsHierarchyReady, false);

		// Token: 0x040007C2 RID: 1986
		public static readonly MbxPropertyDefinition UserInformationIsInactiveMailbox = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsInactiveMailbox, null, false);

		// Token: 0x040007C3 RID: 1987
		public static readonly MbxPropertyDefinition IsInactiveMailbox = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsInactiveMailbox", MbxRecipientSchema.UserInformationIsInactiveMailbox, false);

		// Token: 0x040007C4 RID: 1988
		public static readonly MbxPropertyDefinition UserInformationIsSoftDeletedByDisable = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsSoftDeletedByDisable, null, false);

		// Token: 0x040007C5 RID: 1989
		public static readonly MbxPropertyDefinition IsSoftDeletedByDisable = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsSoftDeletedByDisable", MbxRecipientSchema.UserInformationIsSoftDeletedByDisable, false);

		// Token: 0x040007C6 RID: 1990
		public static readonly MbxPropertyDefinition UserInformationIsSoftDeletedByRemove = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsSoftDeletedByRemove, null, false);

		// Token: 0x040007C7 RID: 1991
		public static readonly MbxPropertyDefinition IsSoftDeletedByRemove = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsSoftDeletedByRemove", MbxRecipientSchema.UserInformationIsSoftDeletedByRemove, false);

		// Token: 0x040007C8 RID: 1992
		public static readonly MbxPropertyDefinition UserInformationIssueWarningQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationIssueWarningQuota, null, false);

		// Token: 0x040007C9 RID: 1993
		public static readonly MbxPropertyDefinition IssueWarningQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("IssueWarningQuota", MbxRecipientSchema.UserInformationIssueWarningQuota, false);

		// Token: 0x040007CA RID: 1994
		public static readonly MbxPropertyDefinition UserInformationJournalArchiveAddress = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationJournalArchiveAddress, null, false);

		// Token: 0x040007CB RID: 1995
		public static readonly MbxPropertyDefinition JournalArchiveAddress = MbxPropertyDefinition.SmtpAddressFromStringPropertyDefinition("JournalArchiveAddress", MbxRecipientSchema.UserInformationJournalArchiveAddress, false);

		// Token: 0x040007CC RID: 1996
		public static readonly MbxPropertyDefinition UserInformationLanguages = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationLanguages, null, true);

		// Token: 0x040007CD RID: 1997
		public static readonly MbxPropertyDefinition Languages = MbxPropertyDefinition.CultureInfoFromStringPropertyDefinition("Languages", MbxRecipientSchema.UserInformationLanguages, true);

		// Token: 0x040007CE RID: 1998
		public static readonly MbxPropertyDefinition LastExchangeChangedTime = MbxPropertyDefinition.NullableDateTimePropertyDefinition(PropTag.UserInformationLastExchangeChangedTime, "LastExchangeChangedTime", false);

		// Token: 0x040007CF RID: 1999
		public static readonly MbxPropertyDefinition LastName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationLastName, "LastName", false);

		// Token: 0x040007D0 RID: 2000
		public static readonly MbxPropertyDefinition Latitude = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationLatitude, "Latitude", false);

		// Token: 0x040007D1 RID: 2001
		public static readonly MbxPropertyDefinition UserInformationLEOEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationLEOEnabled, null, false);

		// Token: 0x040007D2 RID: 2002
		public static readonly MbxPropertyDefinition LEOEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("LEOEnabled", MbxRecipientSchema.UserInformationLEOEnabled, false);

		// Token: 0x040007D3 RID: 2003
		public static readonly MbxPropertyDefinition UserInformationLocaleID = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationLocaleID, null, true);

		// Token: 0x040007D4 RID: 2004
		public static readonly MbxPropertyDefinition LocaleID = MbxPropertyDefinition.Int32FromNullableInt32PropertyDefinition("LocaleID", MbxRecipientSchema.UserInformationLocaleID, true);

		// Token: 0x040007D5 RID: 2005
		public static readonly MbxPropertyDefinition Longitude = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationLongitude, "Longitude", false);

		// Token: 0x040007D6 RID: 2006
		public static readonly MbxPropertyDefinition UserInformationMacAttachmentFormat = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationMacAttachmentFormat, null, false);

		// Token: 0x040007D7 RID: 2007
		public static readonly MbxPropertyDefinition MacAttachmentFormat = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<MacAttachmentFormat>("MacAttachmentFormat", MbxRecipientSchema.UserInformationMacAttachmentFormat, false);

		// Token: 0x040007D8 RID: 2008
		public static readonly MbxPropertyDefinition MailboxContainerGuid = MbxPropertyDefinition.NullableGuidPropertyDefinition(PropTag.UserInformationMailboxContainerGuid, "MailboxContainerGuid", false);

		// Token: 0x040007D9 RID: 2009
		public static readonly MbxPropertyDefinition MailboxMoveBatchName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMailboxMoveBatchName, "MailboxMoveBatchName", false);

		// Token: 0x040007DA RID: 2010
		public static readonly MbxPropertyDefinition MailboxMoveRemoteHostName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMailboxMoveRemoteHostName, "MailboxMoveRemoteHostName", false);

		// Token: 0x040007DB RID: 2011
		public static readonly MbxPropertyDefinition UserInformationMailboxMoveStatus = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationMailboxMoveStatus, null, false);

		// Token: 0x040007DC RID: 2012
		public static readonly MbxPropertyDefinition MailboxMoveStatus = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<RequestStatus>("MailboxMoveStatus", MbxRecipientSchema.UserInformationMailboxMoveStatus, false);

		// Token: 0x040007DD RID: 2013
		public static readonly MbxPropertyDefinition MailboxRelease = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMailboxRelease, "MailboxRelease", false);

		// Token: 0x040007DE RID: 2014
		public static readonly MbxPropertyDefinition MailTipTranslations = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMailTipTranslations, "MailTipTranslations", true);

		// Token: 0x040007DF RID: 2015
		public static readonly MbxPropertyDefinition UserInformationMAPIBlockOutlookNonCachedMode = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMAPIBlockOutlookNonCachedMode, null, false);

		// Token: 0x040007E0 RID: 2016
		public static readonly MbxPropertyDefinition MAPIBlockOutlookNonCachedMode = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("MAPIBlockOutlookNonCachedMode", MbxRecipientSchema.UserInformationMAPIBlockOutlookNonCachedMode, false);

		// Token: 0x040007E1 RID: 2017
		public static readonly MbxPropertyDefinition UserInformationMAPIBlockOutlookRpcHttp = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMAPIBlockOutlookRpcHttp, null, false);

		// Token: 0x040007E2 RID: 2018
		public static readonly MbxPropertyDefinition MAPIBlockOutlookRpcHttp = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("MAPIBlockOutlookRpcHttp", MbxRecipientSchema.UserInformationMAPIBlockOutlookRpcHttp, false);

		// Token: 0x040007E3 RID: 2019
		public static readonly MbxPropertyDefinition MAPIBlockOutlookExternalConnectivity = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMAPIBlockOutlookExternalConnectivity, "MAPIBlockOutlookExternalConnectivity", false);

		// Token: 0x040007E4 RID: 2020
		public static readonly MbxPropertyDefinition MAPIBlockOutlookVersions = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMAPIBlockOutlookVersions, "MAPIBlockOutlookVersions", false);

		// Token: 0x040007E5 RID: 2021
		public static readonly MbxPropertyDefinition UserInformationMAPIEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMAPIEnabled, null, false);

		// Token: 0x040007E6 RID: 2022
		public static readonly MbxPropertyDefinition MAPIEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("MAPIEnabled", MbxRecipientSchema.UserInformationMAPIEnabled, false);

		// Token: 0x040007E7 RID: 2023
		public static readonly MbxPropertyDefinition MapiHttpEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMapiHttpEnabled, "MapiHttpEnabled", false);

		// Token: 0x040007E8 RID: 2024
		public static readonly MbxPropertyDefinition MapiRecipient = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMapiRecipient, "MapiRecipient", false);

		// Token: 0x040007E9 RID: 2025
		public static readonly MbxPropertyDefinition MaxBlockedSenders = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationMaxBlockedSenders, "MaxBlockedSenders", false);

		// Token: 0x040007EA RID: 2026
		public static readonly MbxPropertyDefinition UserInformationMaxReceiveSize = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMaxReceiveSize, null, false);

		// Token: 0x040007EB RID: 2027
		public static readonly MbxPropertyDefinition MaxReceiveSize = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("MaxReceiveSize", MbxRecipientSchema.UserInformationMaxReceiveSize, false);

		// Token: 0x040007EC RID: 2028
		public static readonly MbxPropertyDefinition MaxSafeSenders = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationMaxSafeSenders, "MaxSafeSenders", false);

		// Token: 0x040007ED RID: 2029
		public static readonly MbxPropertyDefinition UserInformationMaxSendSize = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMaxSendSize, null, false);

		// Token: 0x040007EE RID: 2030
		public static readonly MbxPropertyDefinition MaxSendSize = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("MaxSendSize", MbxRecipientSchema.UserInformationMaxSendSize, false);

		// Token: 0x040007EF RID: 2031
		public static readonly MbxPropertyDefinition MemberName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMemberName, "MemberName", false);

		// Token: 0x040007F0 RID: 2032
		public static readonly MbxPropertyDefinition UserInformationMessageBodyFormat = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationMessageBodyFormat, null, false);

		// Token: 0x040007F1 RID: 2033
		public static readonly MbxPropertyDefinition MessageBodyFormat = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<MessageBodyFormat>("MessageBodyFormat", MbxRecipientSchema.UserInformationMessageBodyFormat, false);

		// Token: 0x040007F2 RID: 2034
		public static readonly MbxPropertyDefinition UserInformationMessageFormat = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationMessageFormat, null, false);

		// Token: 0x040007F3 RID: 2035
		public static readonly MbxPropertyDefinition MessageFormat = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<MessageFormat>("MessageFormat", MbxRecipientSchema.UserInformationMessageFormat, false);

		// Token: 0x040007F4 RID: 2036
		public static readonly MbxPropertyDefinition UserInformationMessageTrackingReadStatusDisabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMessageTrackingReadStatusDisabled, null, false);

		// Token: 0x040007F5 RID: 2037
		public static readonly MbxPropertyDefinition MessageTrackingReadStatusDisabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("MessageTrackingReadStatusDisabled", MbxRecipientSchema.UserInformationMessageTrackingReadStatusDisabled, false);

		// Token: 0x040007F6 RID: 2038
		public static readonly MbxPropertyDefinition UserInformationMobileFeaturesEnabled = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationMobileFeaturesEnabled, null, false);

		// Token: 0x040007F7 RID: 2039
		public static readonly MbxPropertyDefinition MobileFeaturesEnabled = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<MobileFeaturesEnabled>("MobileFeaturesEnabled", MbxRecipientSchema.UserInformationMobileFeaturesEnabled, false);

		// Token: 0x040007F8 RID: 2040
		public static readonly MbxPropertyDefinition MobilePhone = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationMobilePhone, "MobilePhone", false);

		// Token: 0x040007F9 RID: 2041
		public static readonly MbxPropertyDefinition UserInformationModerationFlags = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationModerationFlags, null, false);

		// Token: 0x040007FA RID: 2042
		public static readonly MbxPropertyDefinition ModerationFlags = MbxPropertyDefinition.Int32FromNullableInt32PropertyDefinition("ModerationFlags", MbxRecipientSchema.UserInformationModerationFlags, false);

		// Token: 0x040007FB RID: 2043
		public static readonly MbxPropertyDefinition Notes = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationNotes, "Notes", false);

		// Token: 0x040007FC RID: 2044
		public static readonly MbxPropertyDefinition Occupation = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationOccupation, "Occupation", false);

		// Token: 0x040007FD RID: 2045
		public static readonly MbxPropertyDefinition UserInformationOpenDomainRoutingDisabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationOpenDomainRoutingDisabled, null, false);

		// Token: 0x040007FE RID: 2046
		public static readonly MbxPropertyDefinition OpenDomainRoutingDisabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("OpenDomainRoutingDisabled", MbxRecipientSchema.UserInformationOpenDomainRoutingDisabled, false);

		// Token: 0x040007FF RID: 2047
		public static readonly MbxPropertyDefinition OtherHomePhone = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationOtherHomePhone, "OtherHomePhone", true);

		// Token: 0x04000800 RID: 2048
		public static readonly MbxPropertyDefinition OtherMobile = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationOtherMobile, "OtherMobile", true);

		// Token: 0x04000801 RID: 2049
		public static readonly MbxPropertyDefinition OtherTelephone = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationOtherTelephone, "OtherTelephone", true);

		// Token: 0x04000802 RID: 2050
		public static readonly MbxPropertyDefinition UserInformationOWAEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationOWAEnabled, null, false);

		// Token: 0x04000803 RID: 2051
		public static readonly MbxPropertyDefinition OWAEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("OWAEnabled", MbxRecipientSchema.UserInformationOWAEnabled, false);

		// Token: 0x04000804 RID: 2052
		public static readonly MbxPropertyDefinition UserInformationOWAforDevicesEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationOWAforDevicesEnabled, null, false);

		// Token: 0x04000805 RID: 2053
		public static readonly MbxPropertyDefinition OWAforDevicesEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("OWAforDevicesEnabled", MbxRecipientSchema.UserInformationOWAforDevicesEnabled, false);

		// Token: 0x04000806 RID: 2054
		public static readonly MbxPropertyDefinition Pager = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationPager, "Pager", false);

		// Token: 0x04000807 RID: 2055
		public static readonly MbxPropertyDefinition UserInformationPersistedCapabilities = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationPersistedCapabilities, null, true);

		// Token: 0x04000808 RID: 2056
		public static readonly MbxPropertyDefinition PersistedCapabilities = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<Capability>("PersistedCapabilities", MbxRecipientSchema.UserInformationPersistedCapabilities, true);

		// Token: 0x04000809 RID: 2057
		public static readonly MbxPropertyDefinition Phone = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationPhone, "Phone", false);

		// Token: 0x0400080A RID: 2058
		public static readonly MbxPropertyDefinition PhoneProviderId = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationPhoneProviderId, "PhoneProviderId", false);

		// Token: 0x0400080B RID: 2059
		public static readonly MbxPropertyDefinition UserInformationPopEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationPopEnabled, null, false);

		// Token: 0x0400080C RID: 2060
		public static readonly MbxPropertyDefinition PopEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("PopEnabled", MbxRecipientSchema.UserInformationPopEnabled, false);

		// Token: 0x0400080D RID: 2061
		public static readonly MbxPropertyDefinition UserInformationPopEnableExactRFC822Size = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationPopEnableExactRFC822Size, null, false);

		// Token: 0x0400080E RID: 2062
		public static readonly MbxPropertyDefinition PopEnableExactRFC822Size = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("PopEnableExactRFC822Size", MbxRecipientSchema.UserInformationPopEnableExactRFC822Size, false);

		// Token: 0x0400080F RID: 2063
		public static readonly MbxPropertyDefinition UserInformationPopForceICalForCalendarRetrievalOption = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationPopForceICalForCalendarRetrievalOption, null, false);

		// Token: 0x04000810 RID: 2064
		public static readonly MbxPropertyDefinition PopForceICalForCalendarRetrievalOption = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("PopForceICalForCalendarRetrievalOption", MbxRecipientSchema.UserInformationPopForceICalForCalendarRetrievalOption, false);

		// Token: 0x04000811 RID: 2065
		public static readonly MbxPropertyDefinition UserInformationPopMessagesRetrievalMimeFormat = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationPopMessagesRetrievalMimeFormat, null, false);

		// Token: 0x04000812 RID: 2066
		public static readonly MbxPropertyDefinition PopMessagesRetrievalMimeFormat = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<MimeTextFormat>("PopMessagesRetrievalMimeFormat", MbxRecipientSchema.UserInformationPopMessagesRetrievalMimeFormat, false);

		// Token: 0x04000813 RID: 2067
		public static readonly MbxPropertyDefinition PopProtocolLoggingEnabled = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationPopProtocolLoggingEnabled, "PopProtocolLoggingEnabled", false);

		// Token: 0x04000814 RID: 2068
		public static readonly MbxPropertyDefinition UserInformationPopSuppressReadReceipt = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationPopSuppressReadReceipt, null, false);

		// Token: 0x04000815 RID: 2069
		public static readonly MbxPropertyDefinition PopSuppressReadReceipt = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("PopSuppressReadReceipt", MbxRecipientSchema.UserInformationPopSuppressReadReceipt, false);

		// Token: 0x04000816 RID: 2070
		public static readonly MbxPropertyDefinition UserInformationPopUseProtocolDefaults = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationPopUseProtocolDefaults, null, false);

		// Token: 0x04000817 RID: 2071
		public static readonly MbxPropertyDefinition PopUseProtocolDefaults = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("PopUseProtocolDefaults", MbxRecipientSchema.UserInformationPopUseProtocolDefaults, false);

		// Token: 0x04000818 RID: 2072
		public static readonly MbxPropertyDefinition PostalCode = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationPostalCode, "PostalCode", false);

		// Token: 0x04000819 RID: 2073
		public static readonly MbxPropertyDefinition PostOfficeBox = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationPostOfficeBox, "PostOfficeBox", true);

		// Token: 0x0400081A RID: 2074
		public static readonly MbxPropertyDefinition UserInformationPreviousExchangeGuid = MbxPropertyDefinition.NullableGuidPropertyDefinition(PropTag.UserInformationPreviousExchangeGuid, null, false);

		// Token: 0x0400081B RID: 2075
		public static readonly MbxPropertyDefinition PreviousExchangeGuid = MbxPropertyDefinition.GuidFromNullableGuidPropertyDefinition("PreviousExchangeGuid", MbxRecipientSchema.UserInformationPreviousExchangeGuid, false);

		// Token: 0x0400081C RID: 2076
		public static readonly MbxPropertyDefinition UserInformationPreviousRecipientTypeDetails = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationPreviousRecipientTypeDetails, null, false);

		// Token: 0x0400081D RID: 2077
		public static readonly MbxPropertyDefinition PreviousRecipientTypeDetails = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<RecipientTypeDetails>("PreviousRecipientTypeDetails", MbxRecipientSchema.UserInformationPreviousRecipientTypeDetails, false);

		// Token: 0x0400081E RID: 2078
		public static readonly MbxPropertyDefinition UserInformationProhibitSendQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationProhibitSendQuota, null, false);

		// Token: 0x0400081F RID: 2079
		public static readonly MbxPropertyDefinition ProhibitSendQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("ProhibitSendQuota", MbxRecipientSchema.UserInformationProhibitSendQuota, false);

		// Token: 0x04000820 RID: 2080
		public static readonly MbxPropertyDefinition UserInformationProhibitSendReceiveQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationProhibitSendReceiveQuota, null, false);

		// Token: 0x04000821 RID: 2081
		public static readonly MbxPropertyDefinition ProhibitSendReceiveQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("ProhibitSendReceiveQuota", MbxRecipientSchema.UserInformationProhibitSendReceiveQuota, false);

		// Token: 0x04000822 RID: 2082
		public static readonly MbxPropertyDefinition UserInformationQueryBaseDNRestrictionEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationQueryBaseDNRestrictionEnabled, null, false);

		// Token: 0x04000823 RID: 2083
		public static readonly MbxPropertyDefinition QueryBaseDNRestrictionEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("QueryBaseDNRestrictionEnabled", MbxRecipientSchema.UserInformationQueryBaseDNRestrictionEnabled, false);

		// Token: 0x04000824 RID: 2084
		public static readonly MbxPropertyDefinition UserInformationRecipientDisplayType = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationRecipientDisplayType, null, false);

		// Token: 0x04000825 RID: 2085
		public static readonly MbxPropertyDefinition RecipientDisplayType = MbxPropertyDefinition.NullableEnumFromNullableInt32PropertyDefinition<RecipientDisplayType>("RecipientDisplayType", MbxRecipientSchema.UserInformationRecipientDisplayType, false);

		// Token: 0x04000826 RID: 2086
		public static readonly MbxPropertyDefinition UserInformationRecipientLimits = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationRecipientLimits, null, false);

		// Token: 0x04000827 RID: 2087
		public static readonly MbxPropertyDefinition RecipientLimits = MbxPropertyDefinition.UnlimitedInt32FromStringPropertyDefinition("RecipientLimits", MbxRecipientSchema.UserInformationRecipientLimits, false);

		// Token: 0x04000828 RID: 2088
		public static readonly MbxPropertyDefinition UserInformationRecipientSoftDeletedStatus = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationRecipientSoftDeletedStatus, null, false);

		// Token: 0x04000829 RID: 2089
		public static readonly MbxPropertyDefinition RecipientSoftDeletedStatus = MbxPropertyDefinition.Int32FromNullableInt32PropertyDefinition("RecipientSoftDeletedStatus", MbxRecipientSchema.UserInformationRecipientSoftDeletedStatus, false);

		// Token: 0x0400082A RID: 2090
		public static readonly MbxPropertyDefinition UserInformationRecoverableItemsQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationRecoverableItemsQuota, null, false);

		// Token: 0x0400082B RID: 2091
		public static readonly MbxPropertyDefinition RecoverableItemsQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("RecoverableItemsQuota", MbxRecipientSchema.UserInformationRecoverableItemsQuota, false);

		// Token: 0x0400082C RID: 2092
		public static readonly MbxPropertyDefinition UserInformationRecoverableItemsWarningQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationRecoverableItemsWarningQuota, null, false);

		// Token: 0x0400082D RID: 2093
		public static readonly MbxPropertyDefinition RecoverableItemsWarningQuota = MbxPropertyDefinition.UnlimitedByteQuantifiedSizeFromStringPropertyDefinition("RecoverableItemsWarningQuota", MbxRecipientSchema.UserInformationRecoverableItemsWarningQuota, false);

		// Token: 0x0400082E RID: 2094
		public static readonly MbxPropertyDefinition Region = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationRegion, "Region", false);

		// Token: 0x0400082F RID: 2095
		public static readonly MbxPropertyDefinition UserInformationRemotePowerShellEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationRemotePowerShellEnabled, null, false);

		// Token: 0x04000830 RID: 2096
		public static readonly MbxPropertyDefinition RemotePowerShellEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("RemotePowerShellEnabled", MbxRecipientSchema.UserInformationRemotePowerShellEnabled, false);

		// Token: 0x04000831 RID: 2097
		public static readonly MbxPropertyDefinition UserInformationRemoteRecipientType = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationRemoteRecipientType, null, false);

		// Token: 0x04000832 RID: 2098
		public static readonly MbxPropertyDefinition RemoteRecipientType = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<RemoteRecipientType>("RemoteRecipientType", MbxRecipientSchema.UserInformationRemoteRecipientType, false);

		// Token: 0x04000833 RID: 2099
		public static readonly MbxPropertyDefinition UserInformationRequireAllSendersAreAuthenticated = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationRequireAllSendersAreAuthenticated, null, false);

		// Token: 0x04000834 RID: 2100
		public static readonly MbxPropertyDefinition RequireAllSendersAreAuthenticated = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("RequireAllSendersAreAuthenticated", MbxRecipientSchema.UserInformationRequireAllSendersAreAuthenticated, false);

		// Token: 0x04000835 RID: 2101
		public static readonly MbxPropertyDefinition UserInformationResetPasswordOnNextLogon = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationResetPasswordOnNextLogon, null, false);

		// Token: 0x04000836 RID: 2102
		public static readonly MbxPropertyDefinition ResetPasswordOnNextLogon = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ResetPasswordOnNextLogon", MbxRecipientSchema.UserInformationResetPasswordOnNextLogon, false);

		// Token: 0x04000837 RID: 2103
		public static readonly MbxPropertyDefinition UserInformationRetainDeletedItemsFor = MbxPropertyDefinition.NullableInt64PropertyDefinition(PropTag.UserInformationRetainDeletedItemsFor, null, false);

		// Token: 0x04000838 RID: 2104
		public static readonly MbxPropertyDefinition RetainDeletedItemsFor = MbxPropertyDefinition.EnhancedTimeSpanFromNullableInt64PropertyDefinition("RetainDeletedItemsFor", MbxRecipientSchema.UserInformationRetainDeletedItemsFor, false);

		// Token: 0x04000839 RID: 2105
		public static readonly MbxPropertyDefinition UserInformationRetainDeletedItemsUntilBackup = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationRetainDeletedItemsUntilBackup, null, false);

		// Token: 0x0400083A RID: 2106
		public static readonly MbxPropertyDefinition RetainDeletedItemsUntilBackup = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("RetainDeletedItemsUntilBackup", MbxRecipientSchema.UserInformationRetainDeletedItemsUntilBackup, false);

		// Token: 0x0400083B RID: 2107
		public static readonly MbxPropertyDefinition UserInformationRulesQuota = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationRulesQuota, null, false);

		// Token: 0x0400083C RID: 2108
		public static readonly MbxPropertyDefinition RulesQuota = MbxPropertyDefinition.ByteQuantifiedSizeFromStringPropertyDefinition("RulesQuota", MbxRecipientSchema.UserInformationRulesQuota, false);

		// Token: 0x0400083D RID: 2109
		public static readonly MbxPropertyDefinition UserInformationShouldUseDefaultRetentionPolicy = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationShouldUseDefaultRetentionPolicy, null, false);

		// Token: 0x0400083E RID: 2110
		public static readonly MbxPropertyDefinition ShouldUseDefaultRetentionPolicy = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("ShouldUseDefaultRetentionPolicy", MbxRecipientSchema.UserInformationShouldUseDefaultRetentionPolicy, false);

		// Token: 0x0400083F RID: 2111
		public static readonly MbxPropertyDefinition SimpleDisplayName = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationSimpleDisplayName, "SimpleDisplayName", false);

		// Token: 0x04000840 RID: 2112
		public static readonly MbxPropertyDefinition UserInformationSingleItemRecoveryEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationSingleItemRecoveryEnabled, null, false);

		// Token: 0x04000841 RID: 2113
		public static readonly MbxPropertyDefinition SingleItemRecoveryEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("SingleItemRecoveryEnabled", MbxRecipientSchema.UserInformationSingleItemRecoveryEnabled, false);

		// Token: 0x04000842 RID: 2114
		public static readonly MbxPropertyDefinition StateOrProvince = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationStateOrProvince, "StateOrProvince", false);

		// Token: 0x04000843 RID: 2115
		public static readonly MbxPropertyDefinition StreetAddress = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationStreetAddress, "StreetAddress", false);

		// Token: 0x04000844 RID: 2116
		public static readonly MbxPropertyDefinition UserInformationSubscriberAccessEnabled = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationSubscriberAccessEnabled, null, false);

		// Token: 0x04000845 RID: 2117
		public static readonly MbxPropertyDefinition SubscriberAccessEnabled = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("SubscriberAccessEnabled", MbxRecipientSchema.UserInformationSubscriberAccessEnabled, false);

		// Token: 0x04000846 RID: 2118
		public static readonly MbxPropertyDefinition TextEncodedORAddress = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationTextEncodedORAddress, "TextEncodedORAddress", false);

		// Token: 0x04000847 RID: 2119
		public static readonly MbxPropertyDefinition UserInformationTextMessagingState = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationTextMessagingState, null, true);

		// Token: 0x04000848 RID: 2120
		public static readonly MbxPropertyDefinition TextMessagingState = MbxPropertyDefinition.TextMessagingStateBaseFromStringPropertyDefinition("TextMessagingState", MbxRecipientSchema.UserInformationTextMessagingState, true);

		// Token: 0x04000849 RID: 2121
		public static readonly MbxPropertyDefinition Timezone = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationTimezone, "Timezone", false);

		// Token: 0x0400084A RID: 2122
		public static readonly MbxPropertyDefinition UserInformationUCSImListMigrationCompleted = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationUCSImListMigrationCompleted, null, false);

		// Token: 0x0400084B RID: 2123
		public static readonly MbxPropertyDefinition UCSImListMigrationCompleted = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("UCSImListMigrationCompleted", MbxRecipientSchema.UserInformationUCSImListMigrationCompleted, false);

		// Token: 0x0400084C RID: 2124
		public static readonly MbxPropertyDefinition UpgradeDetails = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationUpgradeDetails, "UpgradeDetails", false);

		// Token: 0x0400084D RID: 2125
		public static readonly MbxPropertyDefinition UpgradeMessage = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationUpgradeMessage, "UpgradeMessage", false);

		// Token: 0x0400084E RID: 2126
		public static readonly MbxPropertyDefinition UserInformationUpgradeRequest = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationUpgradeRequest, null, false);

		// Token: 0x0400084F RID: 2127
		public static readonly MbxPropertyDefinition UpgradeRequest = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<UpgradeRequestTypes>("UpgradeRequest", MbxRecipientSchema.UserInformationUpgradeRequest, false);

		// Token: 0x04000850 RID: 2128
		public static readonly MbxPropertyDefinition UserInformationUpgradeStage = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationUpgradeStage, null, false);

		// Token: 0x04000851 RID: 2129
		public static readonly MbxPropertyDefinition UpgradeStage = MbxPropertyDefinition.NullableEnumFromNullableInt32PropertyDefinition<UpgradeStage>("UpgradeStage", MbxRecipientSchema.UserInformationUpgradeStage, false);

		// Token: 0x04000852 RID: 2130
		public static readonly MbxPropertyDefinition UpgradeStageTimeStamp = MbxPropertyDefinition.NullableDateTimePropertyDefinition(PropTag.UserInformationUpgradeStageTimeStamp, "UpgradeStageTimeStamp", false);

		// Token: 0x04000853 RID: 2131
		public static readonly MbxPropertyDefinition UserInformationUpgradeStatus = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationUpgradeStatus, null, false);

		// Token: 0x04000854 RID: 2132
		public static readonly MbxPropertyDefinition UpgradeStatus = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<UpgradeStatusTypes>("UpgradeStatus", MbxRecipientSchema.UserInformationUpgradeStatus, false);

		// Token: 0x04000855 RID: 2133
		public static readonly MbxPropertyDefinition UserInformationUsageLocation = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationUsageLocation, null, false);

		// Token: 0x04000856 RID: 2134
		public static readonly MbxPropertyDefinition UsageLocation = MbxPropertyDefinition.CountryInfoFromStringPropertyDefinition("UsageLocation", MbxRecipientSchema.UserInformationUsageLocation, false);

		// Token: 0x04000857 RID: 2135
		public static readonly MbxPropertyDefinition UserInformationUseMapiRichTextFormat = MbxPropertyDefinition.NullableInt32PropertyDefinition(PropTag.UserInformationUseMapiRichTextFormat, null, false);

		// Token: 0x04000858 RID: 2136
		public static readonly MbxPropertyDefinition UseMapiRichTextFormat = MbxPropertyDefinition.EnumFromNullableInt32PropertyDefinition<UseMapiRichTextFormat>("UseMapiRichTextFormat", MbxRecipientSchema.UserInformationUseMapiRichTextFormat, false);

		// Token: 0x04000859 RID: 2137
		public static readonly MbxPropertyDefinition UserInformationUsePreferMessageFormat = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationUsePreferMessageFormat, null, false);

		// Token: 0x0400085A RID: 2138
		public static readonly MbxPropertyDefinition UsePreferMessageFormat = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("UsePreferMessageFormat", MbxRecipientSchema.UserInformationUsePreferMessageFormat, false);

		// Token: 0x0400085B RID: 2139
		public static readonly MbxPropertyDefinition UserInformationUseUCCAuditConfig = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationUseUCCAuditConfig, null, false);

		// Token: 0x0400085C RID: 2140
		public static readonly MbxPropertyDefinition UseUCCAuditConfig = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("UseUCCAuditConfig", MbxRecipientSchema.UserInformationUseUCCAuditConfig, false);

		// Token: 0x0400085D RID: 2141
		public static readonly MbxPropertyDefinition WebPage = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationWebPage, "WebPage", false);

		// Token: 0x0400085E RID: 2142
		public static readonly MbxPropertyDefinition WhenMailboxCreated = MbxPropertyDefinition.NullableDateTimePropertyDefinition(PropTag.UserInformationWhenMailboxCreated, "WhenMailboxCreated", false);

		// Token: 0x0400085F RID: 2143
		public static readonly MbxPropertyDefinition WhenSoftDeleted = MbxPropertyDefinition.NullableDateTimePropertyDefinition(PropTag.UserInformationWhenSoftDeleted, "WhenSoftDeleted", false);

		// Token: 0x04000860 RID: 2144
		public static readonly MbxPropertyDefinition BirthdayPrecision = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationBirthdayPrecision, "BirthdayPrecision", false);

		// Token: 0x04000861 RID: 2145
		public static readonly MbxPropertyDefinition NameVersion = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationNameVersion, "NameVersion", false);

		// Token: 0x04000862 RID: 2146
		public static readonly MbxPropertyDefinition UserInformationOptInUser = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationOptInUser, null, false);

		// Token: 0x04000863 RID: 2147
		public static readonly MbxPropertyDefinition OptInUser = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("OptInUser", MbxRecipientSchema.UserInformationOptInUser, false);

		// Token: 0x04000864 RID: 2148
		public static readonly MbxPropertyDefinition UserInformationIsMigratedConsumerMailbox = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsMigratedConsumerMailbox, null, false);

		// Token: 0x04000865 RID: 2149
		public static readonly MbxPropertyDefinition IsMigratedConsumerMailbox = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsMigratedConsumerMailbox", MbxRecipientSchema.UserInformationIsMigratedConsumerMailbox, false);

		// Token: 0x04000866 RID: 2150
		public static readonly MbxPropertyDefinition UserInformationMigrationDryRun = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationMigrationDryRun, null, false);

		// Token: 0x04000867 RID: 2151
		public static readonly MbxPropertyDefinition MigrationDryRun = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("MigrationDryRun", MbxRecipientSchema.UserInformationMigrationDryRun, false);

		// Token: 0x04000868 RID: 2152
		public static readonly MbxPropertyDefinition UserInformationIsPremiumConsumerMailbox = MbxPropertyDefinition.NullableBoolPropertyDefinition(PropTag.UserInformationIsPremiumConsumerMailbox, null, false);

		// Token: 0x04000869 RID: 2153
		public static readonly MbxPropertyDefinition IsPremiumConsumerMailbox = MbxPropertyDefinition.BoolFromNullableBoolPropertyDefinition("IsPremiumConsumerMailbox", MbxRecipientSchema.UserInformationIsPremiumConsumerMailbox, false);

		// Token: 0x0400086A RID: 2154
		public static readonly MbxPropertyDefinition AlternateSupportEmailAddresses = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationAlternateSupportEmailAddresses, "AlternateSupportEmailAddresses", false);

		// Token: 0x0400086B RID: 2155
		public static readonly MbxPropertyDefinition UserInformationEmailAddresses = MbxPropertyDefinition.StringPropertyDefinition(PropTag.UserInformationEmailAddresses, null, true);

		// Token: 0x0400086C RID: 2156
		public static readonly MbxPropertyDefinition EmailAddresses = MbxPropertyDefinition.ProxyAddressFromStringPropertyDefinition("EmailAddresses", MbxRecipientSchema.UserInformationEmailAddresses, true);

		// Token: 0x0400086D RID: 2157
		private Dictionary<PropTag, MbxPropertyDefinition> mbxPropertyDefinitionsDictionary;
	}
}
