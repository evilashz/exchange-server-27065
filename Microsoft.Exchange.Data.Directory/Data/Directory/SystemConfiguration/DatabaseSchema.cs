using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003CF RID: 975
	internal class DatabaseSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04001DBD RID: 7613
		public static readonly ADPropertyDefinition AllowFileRestore = new ADPropertyDefinition("AllowFileRestore", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchPatchMDB", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DBE RID: 7614
		public static readonly ADPropertyDefinition AdminDisplayVersion = new ADPropertyDefinition("AdminDisplayVersion", ExchangeObjectVersion.Exchange2003, typeof(ServerVersion), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DBF RID: 7615
		public static readonly ADPropertyDefinition BackgroundDatabaseMaintenance = new ADPropertyDefinition("BackgroundDatabaseMaintenance", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchESEParamBackgroundDatabaseMaintenance", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC0 RID: 7616
		public static readonly ADPropertyDefinition ReplayBackgroundDatabaseMaintenance = new ADPropertyDefinition("ReplayBackgroundDatabaseMaintenance", ExchangeObjectVersion.Exchange2010, typeof(bool?), "msExchESEParamReplayBackgroundDatabaseMaintenance", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC1 RID: 7617
		public static readonly ADPropertyDefinition BackgroundDatabaseMaintenanceSerialization = new ADPropertyDefinition("BackgroundDatabaseMaintenanceSerialization", ExchangeObjectVersion.Exchange2010, typeof(bool?), "msExchESEParamBackgroundDatabaseMaintenanceSerialization", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC2 RID: 7618
		public static readonly ADPropertyDefinition BackgroundDatabaseMaintenanceDelay = new ADPropertyDefinition("BackgroundDatabaseMaintenanceDelay", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamBackgroundDatabaseMaintenanceDelay", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC3 RID: 7619
		public static readonly ADPropertyDefinition ReplayBackgroundDatabaseMaintenanceDelay = new ADPropertyDefinition("ReplayBackgroundDatabaseMaintenanceDelay", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamReplayBackgroundDatabaseMaintenanceDelay", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC4 RID: 7620
		public static readonly ADPropertyDefinition MimimumBackgroundDatabaseMaintenanceInterval = new ADPropertyDefinition("MimimumBackgroundDatabaseMaintenanceInterval", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamBackgroundDatabaseMaintenanceIntervalMin", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC5 RID: 7621
		public static readonly ADPropertyDefinition MaximumBackgroundDatabaseMaintenanceInterval = new ADPropertyDefinition("MaximumBackgroundDatabaseMaintenanceInterval", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamBackgroundDatabaseMaintenanceIntervalMax", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC6 RID: 7622
		public static readonly ADPropertyDefinition DatabaseCreated = new ADPropertyDefinition("DatabaseCreated", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchDatabaseCreated", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.DoNotProvisionalClone, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC7 RID: 7623
		public static readonly ADPropertyDefinition DatabaseBL = new ADPropertyDefinition("DatabaseBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "homeMDBBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC8 RID: 7624
		public static readonly ADPropertyDefinition DelItemAfterBackupEnum = new ADPropertyDefinition("DelItemAfterBackupEnum", ExchangeObjectVersion.Exchange2003, typeof(DeletedItemRetention), "deletedItemFlags", ADPropertyDefinitionFlags.PersistDefaultValue, Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainForCustomPeriod, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(DeletedItemRetention))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DC9 RID: 7625
		public static readonly ADPropertyDefinition DeliveryMechanism = new ADPropertyDefinition("DeliveryMechanism", ExchangeObjectVersion.Exchange2003, typeof(DeliveryMechanisms), "deliveryMechanism", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, DeliveryMechanisms.MessageStore, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(DeliveryMechanisms))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DCA RID: 7626
		public static readonly ADPropertyDefinition Description = SharedPropertyDefinitions.Description;

		// Token: 0x04001DCB RID: 7627
		public static readonly ADPropertyDefinition EdbFilePath = SharedPropertyDefinitions.EdbFilePath;

		// Token: 0x04001DCC RID: 7628
		public static readonly ADPropertyDefinition EdbOfflineAtStartup = new ADPropertyDefinition("EdbOfflineAtStartup", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchEDBOffline", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DCD RID: 7629
		public static readonly ADPropertyDefinition ExchangeLegacyDN = SharedPropertyDefinitions.ExchangeLegacyDN;

		// Token: 0x04001DCE RID: 7630
		public static readonly ADPropertyDefinition DisplayName = SharedPropertyDefinitions.OptionalDisplayName;

		// Token: 0x04001DCF RID: 7631
		public static readonly ADPropertyDefinition FixedFont = new ADPropertyDefinition("FixedFont", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchConvertToFixedFont", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DD0 RID: 7632
		public static readonly ADPropertyDefinition DeletedItemRetention = new ADPropertyDefinition("DeletedItemRetention", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), "garbageCollPeriod", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(14.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DD1 RID: 7633
		public static readonly ADPropertyDefinition MaintenanceScheduleBitmaps = SharedPropertyDefinitions.MaintenanceScheduleBitmaps;

		// Token: 0x04001DD2 RID: 7634
		public static readonly ADPropertyDefinition MaintenanceScheduleMode = new ADPropertyDefinition("MaintenanceScheduleMode", ExchangeObjectVersion.Exchange2003, typeof(ScheduleMode), "activationStyle", ADPropertyDefinitionFlags.PersistDefaultValue, ScheduleMode.Never, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ScheduleMode))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DD3 RID: 7635
		public static readonly ADPropertyDefinition MaxCachedViews = new ADPropertyDefinition("MaxCachedViews", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchMaxCachedViews", ADPropertyDefinitionFlags.PersistDefaultValue, 11, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DD4 RID: 7636
		public static readonly ADPropertyDefinition QuotaNotificationMode = new ADPropertyDefinition("QuotaNotificationMode", ExchangeObjectVersion.Exchange2003, typeof(ScheduleMode), "quotaNotificationStyle", ADPropertyDefinitionFlags.PersistDefaultValue, ScheduleMode.Never, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ScheduleMode))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DD5 RID: 7637
		public static readonly ADPropertyDefinition QuotaNotificationScheduleBitmaps = SharedPropertyDefinitions.QuotaNotificationScheduleBitmaps;

		// Token: 0x04001DD6 RID: 7638
		public static readonly ADPropertyDefinition Recovery = new ADPropertyDefinition("Recovery", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchRestore", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DD7 RID: 7639
		public static readonly ADPropertyDefinition RestoreInProgress = new ADPropertyDefinition("RestoreInProgress", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchDatabaseBeingRestored", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.DoNotProvisionalClone, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DD8 RID: 7640
		public static readonly ADPropertyDefinition Server = SharedPropertyDefinitions.Server;

		// Token: 0x04001DD9 RID: 7641
		public static readonly ADPropertyDefinition MasterServerOrAvailabilityGroup = new ADPropertyDefinition("MasterServerOrAvailabilityGroup", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchMasterServerOrAvailabilityGroup", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DDA RID: 7642
		public static readonly ADPropertyDefinition SMimeSignatureEnabled = new ADPropertyDefinition("SMimeSignatureEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchDownGradeMultipartSigned", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DDB RID: 7643
		public static readonly ADPropertyDefinition IssueWarningQuota = new ADPropertyDefinition("IssueWarningQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBStorageQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DDC RID: 7644
		public static readonly ADPropertyDefinition EventHistoryRetentionPeriod = new ADPropertyDefinition("EventHistoryRetentionPeriod", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEventHistoryRetentionPeriod", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromSeconds(604800.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(1.0), EnhancedTimeSpan.FromSeconds(2592000.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DDD RID: 7645
		public static readonly ADPropertyDefinition DatabaseGroup = new ADPropertyDefinition("DatabaseGroup", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchDatabaseGroup", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DDE RID: 7646
		internal static readonly ADPropertyDefinition MailboxPublicFolderDatabase = SharedPropertyDefinitions.MailboxPublicFolderDatabase;

		// Token: 0x04001DDF RID: 7647
		public static readonly ADPropertyDefinition LogFilePrefix = new ADPropertyDefinition("LogFilePrefix", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchESEParamBaseName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE0 RID: 7648
		public static readonly ADPropertyDefinition CircularLoggingEnabledValue = new ADPropertyDefinition("CircularLoggingEnabledValue", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchESEParamCircularLog", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 1)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE1 RID: 7649
		public static readonly ADPropertyDefinition LogFolderPath = new ADPropertyDefinition("LogFolderPath", ExchangeObjectVersion.Exchange2010, typeof(NonRootLocalLongFullPath), "msExchESEParamLogFilePath", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint,
			new NoLeadingOrTrailingWhitespaceConstraint()
		}, null, null);

		// Token: 0x04001DE2 RID: 7650
		public static readonly ADPropertyDefinition SystemFolderPath = new ADPropertyDefinition("SystemFolderPath", ExchangeObjectVersion.Exchange2010, typeof(NonRootLocalLongFullPath), "msExchESEParamSystemPath", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint,
			new NoLeadingOrTrailingWhitespaceConstraint()
		}, null, null);

		// Token: 0x04001DE3 RID: 7651
		public static readonly ADPropertyDefinition TemporaryDataFolderPath = new ADPropertyDefinition("TemporaryDataFolderPath", ExchangeObjectVersion.Exchange2010, typeof(NonRootLocalLongFullPath), "msExchESEparamTempPath", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE4 RID: 7652
		public static readonly ADPropertyDefinition EventLogSourceID = new ADPropertyDefinition("EventLogSourceID", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchESEParamEventSource", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE5 RID: 7653
		public static readonly ADPropertyDefinition ZeroDatabasePagesValue = new ADPropertyDefinition("ZeroDatabasePagesValue", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchESEParamZeroDatabaseDuringBackup", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 1)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE6 RID: 7654
		public static readonly ADPropertyDefinition LogFileSize = new ADPropertyDefinition("LogFileSize", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchESEParamLogFileSize", ADPropertyDefinitionFlags.PersistDefaultValue, 1024, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE7 RID: 7655
		public static readonly ADPropertyDefinition LogBuffers = new ADPropertyDefinition("LogBuffers", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamLogBuffers", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE8 RID: 7656
		public static readonly ADPropertyDefinition MaximumOpenTables = new ADPropertyDefinition("MaximumOpenTables", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamMaxOpenTables", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DE9 RID: 7657
		public static readonly ADPropertyDefinition MaximumTemporaryTables = new ADPropertyDefinition("MaximumTemporaryTables", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamMaxTemporaryTables", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DEA RID: 7658
		public static readonly ADPropertyDefinition MaximumCursors = new ADPropertyDefinition("MaximumCursors", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamMaxCursors", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DEB RID: 7659
		public static readonly ADPropertyDefinition MaximumSessions = new ADPropertyDefinition("MaximumSessions", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamMaxSessions", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DEC RID: 7660
		public static readonly ADPropertyDefinition MaximumVersionStorePages = new ADPropertyDefinition("MaximumVersionStorePages", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamMaxVerPages", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DED RID: 7661
		public static readonly ADPropertyDefinition PreferredVersionStorePages = new ADPropertyDefinition("PreferredVersionStorePages", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamPreferredVerPages", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DEE RID: 7662
		public static readonly ADPropertyDefinition DatabaseExtensionSize = new ADPropertyDefinition("DatabaseExtensionSize", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamDbExtensionSize", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DEF RID: 7663
		public static readonly ADPropertyDefinition LogCheckpointDepth = new ADPropertyDefinition("LogCheckpointDepth", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamCheckpointDepthMax", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF0 RID: 7664
		public static readonly ADPropertyDefinition ReplayCheckpointDepth = new ADPropertyDefinition("ReplayCheckpointDepth", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamReplayCheckpointDepthMax", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF1 RID: 7665
		public static readonly ADPropertyDefinition CachePriority = new ADPropertyDefinition("CachePriority", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamCachePriority", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 100)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF2 RID: 7666
		public static readonly ADPropertyDefinition ReplayCachePriority = new ADPropertyDefinition("ReplayCachePriority", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamReplayCachePriority", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 100)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF3 RID: 7667
		public static readonly ADPropertyDefinition CachedClosedTables = new ADPropertyDefinition("CachedClosedTables", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEparamCachedClosedTables", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF4 RID: 7668
		public static readonly ADPropertyDefinition MaximumPreReadPages = new ADPropertyDefinition("MaximumPreReadPages", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamPreReadIOMax", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF5 RID: 7669
		public static readonly ADPropertyDefinition MaximumReplayPreReadPages = new ADPropertyDefinition("MaximumReplayPreReadPages", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchESEParamReplayPreReadIOMax", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF6 RID: 7670
		public static readonly ADPropertyDefinition DataMoveReplicationConstraintDefinition = new ADPropertyDefinition("DataMoveReplicationConstraint", ExchangeObjectVersion.Exchange2007, typeof(DataMoveReplicationConstraintParameter), "msExchDataMoveReplicationConstraint", ADPropertyDefinitionFlags.None, DataMoveReplicationConstraintParameter.SecondCopy, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001DF7 RID: 7671
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04001DF8 RID: 7672
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<DatabaseConfigXml>(DatabaseSchema.ConfigurationXMLRaw);

		// Token: 0x04001DF9 RID: 7673
		public static readonly ADPropertyDefinition MailboxProvisioningAttributes = new ADPropertyDefinition("MailboxProvisioningAttributes", ExchangeObjectVersion.Exchange2010, typeof(MailboxProvisioningAttributes), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.ConfigurationXMLRaw,
			ADObjectSchema.RawName,
			DatabaseSchema.Server,
			DatabaseSchema.MasterServerOrAvailabilityGroup
		}, null, new GetterDelegate(Database.MailboxProvisioningAttributesGetter), new SetterDelegate(Database.MailboxProvisioningAttributesSetter), null, null);

		// Token: 0x04001DFA RID: 7674
		public static readonly ADPropertyDefinition AdministrativeGroup = new ADPropertyDefinition("AdministrativeGroup", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(Database.AdministrativeGroupGetter), null, null, null);

		// Token: 0x04001DFB RID: 7675
		public static readonly ADPropertyDefinition MaintenanceSchedule = new ADPropertyDefinition("MaintenanceSchedule", ExchangeObjectVersion.Exchange2003, typeof(Schedule), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.MaintenanceScheduleBitmaps,
			DatabaseSchema.MaintenanceScheduleMode
		}, null, new GetterDelegate(Database.MaintenanceScheduleGetter), new SetterDelegate(Database.MaintenanceScheduleSetter), null, null);

		// Token: 0x04001DFC RID: 7676
		public static readonly ADPropertyDefinition MountAtStartup = new ADPropertyDefinition("MountAtStartup", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.EdbOfflineAtStartup
		}, new CustomFilterBuilderDelegate(Database.MountAtStartupFilterBuilder), (IPropertyBag propertyBag) => !(bool)propertyBag[DatabaseSchema.EdbOfflineAtStartup], delegate(object value, IPropertyBag propertyBag)
		{
			propertyBag[DatabaseSchema.EdbOfflineAtStartup] = !(bool)value;
		}, null, null);

		// Token: 0x04001DFD RID: 7677
		public static readonly ADPropertyDefinition CircularLoggingEnabled = new ADPropertyDefinition("CircularLoggingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.CircularLoggingEnabledValue
		}, null, (IPropertyBag propertyBag) => 0 != (int)propertyBag[DatabaseSchema.CircularLoggingEnabledValue], delegate(object value, IPropertyBag propertyBag)
		{
			propertyBag[DatabaseSchema.CircularLoggingEnabledValue] = (((bool)value) ? 1 : 0);
		}, null, null);

		// Token: 0x04001DFE RID: 7678
		public static readonly ADPropertyDefinition Organization = new ADPropertyDefinition("Organization", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.Id
		}, null, new GetterDelegate(Database.OrganizationGetter), null, null, null);

		// Token: 0x04001DFF RID: 7679
		public static readonly ADPropertyDefinition QuotaNotificationSchedule = new ADPropertyDefinition("QuotaNotificationSchedule", ExchangeObjectVersion.Exchange2003, typeof(Schedule), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.QuotaNotificationScheduleBitmaps,
			DatabaseSchema.QuotaNotificationMode
		}, null, new GetterDelegate(Database.QuotaNotificationScheduleGetter), new SetterDelegate(Database.QuotaNotificationScheduleSetter), null, null);

		// Token: 0x04001E00 RID: 7680
		public static readonly ADPropertyDefinition RetainDeletedItemsUntilBackup = new ADPropertyDefinition("RetainDeletedItemsUntilBackup", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.DelItemAfterBackupEnum
		}, new CustomFilterBuilderDelegate(Database.RetainDeletedItemsUntilBackupFilterBuilder), new GetterDelegate(Database.RetainDeletedItemsUntilBackupGetter), new SetterDelegate(Database.RetainDeletedItemsUntilBackupSetter), null, null);

		// Token: 0x04001E01 RID: 7681
		public static readonly ADPropertyDefinition MasterServerOrAvailabilityGroupName = new ADPropertyDefinition("MasterServerOrAvailabilityGroupName", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.MasterServerOrAvailabilityGroup
		}, null, new GetterDelegate(Database.MasterServerOrAvailabilityGroupNameGetter), null, null, null);

		// Token: 0x04001E02 RID: 7682
		public static readonly ADPropertyDefinition ServerName = new ADPropertyDefinition("ServerName", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.Server
		}, null, new GetterDelegate(Database.ServerNameGetter), null, null, null);

		// Token: 0x04001E03 RID: 7683
		public new static readonly ADPropertyDefinition Name = new ADPropertyDefinition("Name", ExchangeObjectVersion.Exchange2003, typeof(string), "name", ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new ADObjectNameStringLengthConstraint(1, 64),
			new ContainingNonWhitespaceConstraint(),
			new ADObjectNameCharacterConstraint(new char[]
			{
				'\\',
				'/',
				'=',
				';',
				'\0',
				'\n'
			})
		}, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.RawName
		}, new CustomFilterBuilderDelegate(ADObject.DummyCustomFilterBuilderDelegate), new GetterDelegate(Database.DatabaseNameGetter), new SetterDelegate(Database.DatabaseNameSetter), null, null);

		// Token: 0x04001E04 RID: 7684
		public static readonly ADPropertyDefinition IsExchange2009OrLater = new ADPropertyDefinition("IsExchange2009OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ExchangeVersion
		}, null, new GetterDelegate(Database.IsExchange2009OrLaterGetter), null, null, null);

		// Token: 0x04001E05 RID: 7685
		public static readonly ADPropertyDefinition RpcClientAccessServerExchangeLegacyDN = new ADPropertyDefinition("RpcClientAccessServerExchangeLegacyDN", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			DatabaseSchema.ExchangeLegacyDN
		}, null, new GetterDelegate(Database.RpcClientAccessServerExchangeLegacyDNGetter), new SetterDelegate(Database.RpcClientAccessServerExchangeLegacyDNSetter), null, null);

		// Token: 0x04001E06 RID: 7686
		public static readonly ADPropertyDefinition AutoDagFlags = new ADPropertyDefinition("AutoDagFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchAutoDAGParamDatabaseFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001E07 RID: 7687
		public static readonly ADPropertyDefinition AutoDatabaseMountDialType = new ADPropertyDefinition("AutoDatabaseMountDialType", ExchangeObjectVersion.Exchange2010, typeof(AutoDatabaseMountDial), "msExchDataLossForAutoDatabaseMount", ADPropertyDefinitionFlags.PersistDefaultValue, AutoDatabaseMountDial.GoodAvailability, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001E08 RID: 7688
		public static readonly ADPropertyDefinition AutoDagExcludeFromMonitoring = new ADPropertyDefinition("AutoDagExcludeFromMonitoring", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			DatabaseSchema.AutoDagFlags
		}, null, ADObject.FlagGetterDelegate(DatabaseSchema.AutoDagFlags, 1), ADObject.FlagSetterDelegate(DatabaseSchema.AutoDagFlags, 1), null, null);

		// Token: 0x04001E09 RID: 7689
		public static readonly ADPropertyDefinition InvalidDatabaseCopiesAllowed = new ADPropertyDefinition("InvalidDatabaseCopiesAllowed", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
