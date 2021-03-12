using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000512 RID: 1298
	internal sealed class OfflineAddressBookSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002727 RID: 10023
		public static readonly ADPropertyDefinition Server = new ADPropertyDefinition("Server", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "offLineABServer", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002728 RID: 10024
		public static readonly ADPropertyDefinition AddressLists = new ADPropertyDefinition("AddressLists", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "offLineABContainers", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002729 RID: 10025
		public static readonly ADPropertyDefinition RawVersion = new ADPropertyDefinition("RawVersion", ExchangeObjectVersion.Exchange2003, typeof(int), "doOABVersion", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400272A RID: 10026
		public static readonly ADPropertyDefinition OfflineAddressBookFolder = new ADPropertyDefinition("OfflineAddressBookFolder", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchOABFolder", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400272B RID: 10027
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchOABDefault", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400272C RID: 10028
		public static readonly ADPropertyDefinition PublicFolderDatabase = SharedPropertyDefinitions.SitePublicFolderDatabase;

		// Token: 0x0400272D RID: 10029
		public static readonly ADPropertyDefinition ScheduleBitmaps = new ADPropertyDefinition("ScheduleBitmaps", ExchangeObjectVersion.Exchange2003, typeof(Schedule), "offLineABSchedule", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400272E RID: 10030
		public static readonly ADPropertyDefinition ScheduleMode = new ADPropertyDefinition("ScheduleMode", ExchangeObjectVersion.Exchange2003, typeof(ScheduleMode), "offlineABStyle", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, Microsoft.Exchange.Data.ScheduleMode.Never, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400272F RID: 10031
		public static readonly ADPropertyDefinition SiteFolderGuid = new ADPropertyDefinition("SiteFolderGuid", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "siteFolderGUID", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002730 RID: 10032
		public static readonly ADPropertyDefinition OabFlags = new ADPropertyDefinition("OabFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOABFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002731 RID: 10033
		public static readonly ADPropertyDefinition ExchangeLegacyDN = new ADPropertyDefinition("ExchangeLegacyDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002732 RID: 10034
		public new static readonly ADPropertyDefinition SystemFlags = new ADPropertyDefinition("SystemFlags", ExchangeObjectVersion.Exchange2003, typeof(SystemFlagsEnum), "systemFlags", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.WriteOnce, SystemFlagsEnum.Movable | SystemFlagsEnum.Renamable, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002733 RID: 10035
		public static readonly ADPropertyDefinition VirtualDirectories = new ADPropertyDefinition("VirtualDirectories", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchOABVirtualDirectoriesLink", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002734 RID: 10036
		public static readonly ADPropertyDefinition RawDiffRetentionPeriod = new ADPropertyDefinition("RawDiffRetentionPeriod", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchOABTTL", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002735 RID: 10037
		public static readonly ADPropertyDefinition MaxBinaryPropertySize = new ADPropertyDefinition("MaxBinaryPropertySize", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOABMaxBinarySize", ADPropertyDefinitionFlags.PersistDefaultValue, 32768, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002736 RID: 10038
		public static readonly ADPropertyDefinition MaxMultivaluedBinaryPropertySize = new ADPropertyDefinition("MaxMultivaluedBinaryPropertySize", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOABMaxMVBinarySize", ADPropertyDefinitionFlags.PersistDefaultValue, 65536, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002737 RID: 10039
		public static readonly ADPropertyDefinition MaxStringPropertySize = new ADPropertyDefinition("MaxStringPropertySize", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOABMaxStringSize", ADPropertyDefinitionFlags.PersistDefaultValue, 3400, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002738 RID: 10040
		public static readonly ADPropertyDefinition MaxMultivaluedStringPropertySize = new ADPropertyDefinition("MaxMultivaluedStringPropertySize", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOABMaxMVStringSize", ADPropertyDefinitionFlags.PersistDefaultValue, 65536, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002739 RID: 10041
		public static readonly ADPropertyDefinition ANRProperties = new ADPropertyDefinition("ANRProperties", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOABANRProperties", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400273A RID: 10042
		public static readonly ADPropertyDefinition DetailsProperties = new ADPropertyDefinition("DetailsProperties", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOABDetailsProperties", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400273B RID: 10043
		public static readonly ADPropertyDefinition TruncatedProperties = new ADPropertyDefinition("TruncatedProperties", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOABTruncatedProperties", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400273C RID: 10044
		public static readonly ADPropertyDefinition LastTouchedTime = new ADPropertyDefinition("LastTouchedTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchOABLastTouchedTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400273D RID: 10045
		public static readonly ADPropertyDefinition LastRequestedTime = new ADPropertyDefinition("LastRequestedTime", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), "msExchLastUpdateTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400273E RID: 10046
		public static readonly ADPropertyDefinition LastNumberOfRecords = new ADPropertyDefinition("LastNumberOfRecords", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchOABLastNumberOfRecords", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x0400273F RID: 10047
		public static readonly ADPropertyDefinition DiffRetentionPeriod = new ADPropertyDefinition("DiffRetentionPeriod", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>?), null, ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(7, 1825)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OfflineAddressBookSchema.RawDiffRetentionPeriod,
			ADObjectSchema.ExchangeVersion
		}, null, new GetterDelegate(OfflineAddressBook.DiffRetentionPeriodGetter), new SetterDelegate(OfflineAddressBook.DiffRetentionPeriodSetter), null, null);

		// Token: 0x04002740 RID: 10048
		public static readonly ADPropertyDefinition Versions = new ADPropertyDefinition("Versions", ExchangeObjectVersion.Exchange2003, typeof(OfflineAddressBookVersion), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(OfflineAddressBookVersion))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OfflineAddressBookSchema.RawVersion
		}, null, new GetterDelegate(OfflineAddressBook.VersionsGetter), new SetterDelegate(OfflineAddressBook.VersionsSetter), null, null);

		// Token: 0x04002741 RID: 10049
		public static readonly ADPropertyDefinition Schedule = new ADPropertyDefinition("Schedule", ExchangeObjectVersion.Exchange2003, typeof(Schedule), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OfflineAddressBookSchema.ScheduleBitmaps,
			OfflineAddressBookSchema.ScheduleMode
		}, null, (IPropertyBag pb) => (Schedule)pb[OfflineAddressBookSchema.ScheduleBitmaps], new SetterDelegate(OfflineAddressBook.ScheduleSetter), null, null);

		// Token: 0x04002742 RID: 10050
		public static readonly ADPropertyDefinition PublicFolderDistributionEnabled = new ADPropertyDefinition("PublicFolderDistributionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OfflineAddressBookSchema.OabFlags
		}, null, ADObject.FlagGetterDelegate(OfflineAddressBookSchema.OabFlags, 1), ADObject.FlagSetterDelegate(OfflineAddressBookSchema.OabFlags, 1), null, null);

		// Token: 0x04002743 RID: 10051
		public static readonly ADPropertyDefinition GlobalWebDistributionEnabled = new ADPropertyDefinition("GlobalWebDistributionEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OfflineAddressBookSchema.OabFlags
		}, null, ADObject.FlagGetterDelegate(OfflineAddressBookSchema.OabFlags, 2), ADObject.FlagSetterDelegate(OfflineAddressBookSchema.OabFlags, 2), null, null);

		// Token: 0x04002744 RID: 10052
		public static readonly ADPropertyDefinition WebDistributionEnabled = new ADPropertyDefinition("WebDistributionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OfflineAddressBookSchema.OabFlags,
			OfflineAddressBookSchema.VirtualDirectories
		}, null, new GetterDelegate(OfflineAddressBook.WebDistributionEnabledGetter), null, null, null);

		// Token: 0x04002745 RID: 10053
		public static readonly ADPropertyDefinition ConfiguredAttributes = new ADPropertyDefinition("ConfiguredAttributes", ExchangeObjectVersion.Exchange2010, typeof(OfflineAddressBookMapiProperty), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04002746 RID: 10054
		public static readonly ADPropertyDefinition AssociatedAddressBookPolicies = new ADPropertyDefinition("AssociatedAddressBookPolicies", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchOfflineAddressBookBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002747 RID: 10055
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04002748 RID: 10056
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<OfflineAddressBookConfigXML>(OfflineAddressBookSchema.ConfigurationXMLRaw);

		// Token: 0x04002749 RID: 10057
		public static readonly ADPropertyDefinition ManifestVersion = XMLSerializableBase.ConfigXmlProperty<OfflineAddressBookConfigXML, OfflineAddressBookManifestVersion>("ManifestVersion", ExchangeObjectVersion.Exchange2010, OfflineAddressBookSchema.ConfigurationXML, null, (OfflineAddressBookConfigXML configXML) => configXML.ManifestVersion, delegate(OfflineAddressBookConfigXML configXML, OfflineAddressBookManifestVersion value)
		{
			configXML.ManifestVersion = value;
		}, null, null);

		// Token: 0x0400274A RID: 10058
		public static readonly ADPropertyDefinition LastFailedTime = XMLSerializableBase.ConfigXmlProperty<OfflineAddressBookConfigXML, DateTime?>("LastFailedTime", ExchangeObjectVersion.Exchange2010, OfflineAddressBookSchema.ConfigurationXML, null, (OfflineAddressBookConfigXML configXML) => configXML.LastFailedTime, delegate(OfflineAddressBookConfigXML configXML, DateTime? value)
		{
			configXML.LastFailedTime = value;
		}, null, null);

		// Token: 0x0400274B RID: 10059
		public static readonly ADPropertyDefinition LastGeneratingData = XMLSerializableBase.ConfigXmlProperty<OfflineAddressBookConfigXML, OfflineAddressBookLastGeneratingData>("LastGeneratingData", ExchangeObjectVersion.Exchange2010, OfflineAddressBookSchema.ConfigurationXML, null, (OfflineAddressBookConfigXML configXML) => configXML.LastGeneratingData, delegate(OfflineAddressBookConfigXML configXML, OfflineAddressBookLastGeneratingData value)
		{
			configXML.LastGeneratingData = value;
		}, null, null);

		// Token: 0x0400274C RID: 10060
		public static readonly ADPropertyDefinition GeneratingMailbox = new ADPropertyDefinition("GeneratingMailbox", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchOABGeneratingMailboxLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400274D RID: 10061
		public static readonly ADPropertyDefinition ShadowMailboxDistributionEnabled = new ADPropertyDefinition("ShadowMailboxDistributionEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OfflineAddressBookSchema.OabFlags
		}, null, ADObject.FlagGetterDelegate(OfflineAddressBookSchema.OabFlags, 4), ADObject.FlagSetterDelegate(OfflineAddressBookSchema.OabFlags, 4), null, null);
	}
}
