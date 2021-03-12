using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A20 RID: 2592
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationBatchSchema : ObjectSchema
	{
		// Token: 0x040035B3 RID: 13747
		public static readonly ProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(MigrationBatchId), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035B4 RID: 13748
		public static readonly ProviderPropertyDefinition ObjectState = UserConfigurationObjectSchema.ObjectState;

		// Token: 0x040035B5 RID: 13749
		public static readonly ProviderPropertyDefinition ExchangeVersion = UserConfigurationObjectSchema.ExchangeVersion;

		// Token: 0x040035B6 RID: 13750
		public static readonly ProviderPropertyDefinition TotalCount = new SimpleProviderPropertyDefinition("TotalCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035B7 RID: 13751
		public static readonly ProviderPropertyDefinition FinalizedCount = new SimpleProviderPropertyDefinition("FinalizedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035B8 RID: 13752
		public static readonly ProviderPropertyDefinition StoppedCount = new SimpleProviderPropertyDefinition("StoppedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035B9 RID: 13753
		public static readonly ProviderPropertyDefinition SyncedCount = new SimpleProviderPropertyDefinition("SyncedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035BA RID: 13754
		public static readonly ProviderPropertyDefinition PendingCount = new SimpleProviderPropertyDefinition("PendingCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035BB RID: 13755
		public static readonly ProviderPropertyDefinition SubmittedByUser = new SimpleProviderPropertyDefinition("SubmittedByUser", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035BC RID: 13756
		public static readonly ProviderPropertyDefinition OwnerId = new SimpleProviderPropertyDefinition("OwnerId", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035BD RID: 13757
		public static readonly ProviderPropertyDefinition OwnerExchangeObjectId = new SimpleProviderPropertyDefinition("OwnerExchangeObjectId", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.TaskPopulated, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035BE RID: 13758
		public static readonly ProviderPropertyDefinition DelegatedAdminOwner = new SimpleProviderPropertyDefinition("DelegatedAdminOwner", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035BF RID: 13759
		public static readonly ProviderPropertyDefinition CreationDateTime = new SimpleProviderPropertyDefinition("CreationDateTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.TaskPopulated, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C0 RID: 13760
		public static readonly ProviderPropertyDefinition CreationDateTimeUTC = new SimpleProviderPropertyDefinition("CreationDateTimeUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.TaskPopulated, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C1 RID: 13761
		public static readonly ProviderPropertyDefinition StartDateTime = new SimpleProviderPropertyDefinition("StartDateTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C2 RID: 13762
		public static readonly ProviderPropertyDefinition StartDateTimeUTC = new SimpleProviderPropertyDefinition("StartDateTimeUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C3 RID: 13763
		public static readonly ProviderPropertyDefinition FinalizedDateTime = new SimpleProviderPropertyDefinition("FinalizedDateTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C4 RID: 13764
		public static readonly ProviderPropertyDefinition FinalizedDateTimeUTC = new SimpleProviderPropertyDefinition("FinalizedDateTimeUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C5 RID: 13765
		public static readonly ProviderPropertyDefinition BatchStatus = new SimpleProviderPropertyDefinition("BatchStatus", ExchangeObjectVersion.Exchange2010, typeof(MigrationBatchStatus), PropertyDefinitionFlags.TaskPopulated, MigrationBatchStatus.Failed, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C6 RID: 13766
		public static readonly ProviderPropertyDefinition FailedCount = new SimpleProviderPropertyDefinition("MigrationErrorsCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C7 RID: 13767
		public static readonly ProviderPropertyDefinition FailedInitialSyncCount = new SimpleProviderPropertyDefinition("FailedInitialSyncCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C8 RID: 13768
		public static readonly ProviderPropertyDefinition FailedIncrementalSyncCount = new SimpleProviderPropertyDefinition("FailedIncrementalSyncCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035C9 RID: 13769
		public static readonly ProviderPropertyDefinition NumValidationErrors = new SimpleProviderPropertyDefinition("NumValidationErrors", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035CA RID: 13770
		public static readonly ProviderPropertyDefinition ValidationErrors = new SimpleProviderPropertyDefinition("ValidationErrors", ExchangeObjectVersion.Exchange2010, typeof(MigrationBatchError), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035CB RID: 13771
		public static readonly ProviderPropertyDefinition Message = new SimpleProviderPropertyDefinition("Message", ExchangeObjectVersion.Exchange2010, typeof(LocalizedString), PropertyDefinitionFlags.TaskPopulated, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035CC RID: 13772
		public static readonly ProviderPropertyDefinition NotificationEmails = new SimpleProviderPropertyDefinition("NotificationEmails", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035CD RID: 13773
		public static readonly ProviderPropertyDefinition ExcludedFolders = new SimpleProviderPropertyDefinition("ExcludedFolders", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035CE RID: 13774
		public static readonly ProviderPropertyDefinition MigrationType = new SimpleProviderPropertyDefinition("MigrationType", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035CF RID: 13775
		public static readonly ProviderPropertyDefinition BatchDirection = new SimpleProviderPropertyDefinition("BatchDirection", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 2, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D0 RID: 13776
		public static readonly ProviderPropertyDefinition SuppressErrors = new SimpleProviderPropertyDefinition("SuppressErrors", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D1 RID: 13777
		public static readonly ProviderPropertyDefinition ActiveCount = new SimpleProviderPropertyDefinition("ActiveCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D2 RID: 13778
		public static readonly ProviderPropertyDefinition ProvisionedCount = new SimpleProviderPropertyDefinition("ProvisionedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D3 RID: 13779
		public static readonly ProviderPropertyDefinition Locale = new SimpleProviderPropertyDefinition("Locale", ExchangeObjectVersion.Exchange2010, typeof(CultureInfo), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D4 RID: 13780
		public static readonly ProviderPropertyDefinition IsProvisioning = new SimpleProviderPropertyDefinition("IsProvisioning", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D5 RID: 13781
		public static readonly ProviderPropertyDefinition MigrationBatchFlags = new SimpleProviderPropertyDefinition("MigrationBatchFlags", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D6 RID: 13782
		public static readonly ProviderPropertyDefinition Reports = new SimpleProviderPropertyDefinition("Reports", ExchangeObjectVersion.Exchange2010, typeof(MigrationReportSet), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D7 RID: 13783
		public static readonly ProviderPropertyDefinition DiagnosticInfo = new SimpleProviderPropertyDefinition("DiagnosticInfo", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D8 RID: 13784
		public static readonly ProviderPropertyDefinition SupportedActions = new SimpleProviderPropertyDefinition("SupportedActions", ExchangeObjectVersion.Exchange2010, typeof(MigrationBatchSupportedActions), PropertyDefinitionFlags.TaskPopulated, MigrationBatchSupportedActions.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035D9 RID: 13785
		public static readonly ProviderPropertyDefinition InitialSyncDateTime = new SimpleProviderPropertyDefinition("InitialSyncDateTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035DA RID: 13786
		public static readonly ProviderPropertyDefinition InitialSyncDateTimeUTC = new SimpleProviderPropertyDefinition("InitialSyncDateTimeUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035DB RID: 13787
		public static readonly ProviderPropertyDefinition InitialSyncDuration = new SimpleProviderPropertyDefinition("InitialSyncDuration", ExchangeObjectVersion.Exchange2010, typeof(TimeSpan?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035DC RID: 13788
		public static readonly ProviderPropertyDefinition LastSyncedDateTimeUTC = new SimpleProviderPropertyDefinition("LastSyncedDateTimeUTC", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035DD RID: 13789
		public static readonly ProviderPropertyDefinition LastSyncedDateTime = new SimpleProviderPropertyDefinition("LastSyncedDateTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035DE RID: 13790
		public static readonly ProviderPropertyDefinition SourceEndpoint = new SimpleProviderPropertyDefinition("SourceEndpoint", ExchangeObjectVersion.Exchange2012, typeof(MigrationEndpoint), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035DF RID: 13791
		public static readonly ProviderPropertyDefinition TargetEndpoint = new SimpleProviderPropertyDefinition("TargetEndpoint", ExchangeObjectVersion.Exchange2012, typeof(MigrationEndpoint), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E0 RID: 13792
		public static readonly ProviderPropertyDefinition BadItemLimit = new SimpleProviderPropertyDefinition("BadItemLimit", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E1 RID: 13793
		public static readonly ProviderPropertyDefinition LargeItemLimit = new SimpleProviderPropertyDefinition("LargeItemLimit", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E2 RID: 13794
		public static readonly ProviderPropertyDefinition SourcePublicFolderDatabase = new SimpleProviderPropertyDefinition("SourcePublicFolderDatabase", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E3 RID: 13795
		public static readonly ProviderPropertyDefinition TargetDatabases = new SimpleProviderPropertyDefinition("TargetDatabases", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E4 RID: 13796
		public static readonly ProviderPropertyDefinition TargetArchiveDatabases = new SimpleProviderPropertyDefinition("TargetArchiveDatabases", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E5 RID: 13797
		public static readonly ProviderPropertyDefinition PrimaryOnly = new SimpleProviderPropertyDefinition("PrimaryOnly", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E6 RID: 13798
		public static readonly ProviderPropertyDefinition ArchiveOnly = new SimpleProviderPropertyDefinition("ArchiveOnly", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E7 RID: 13799
		public static readonly ProviderPropertyDefinition TargetDeliveryDomain = new SimpleProviderPropertyDefinition("TargetDeliveryDomain", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E8 RID: 13800
		public static readonly ProviderPropertyDefinition SkipSteps = new SimpleProviderPropertyDefinition("SkipSteps", ExchangeObjectVersion.Exchange2012, typeof(SkippableMigrationSteps), PropertyDefinitionFlags.None, SkippableMigrationSteps.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035E9 RID: 13801
		public static readonly ProviderPropertyDefinition CurrentRetryCount = new SimpleProviderPropertyDefinition("CurrentRetryCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035EA RID: 13802
		public static readonly ProviderPropertyDefinition AutoRetryCount = new SimpleProviderPropertyDefinition("AutoRetryCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035EB RID: 13803
		public static readonly ProviderPropertyDefinition StartAfter = new SimpleProviderPropertyDefinition("StartAfter", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035EC RID: 13804
		public static readonly ProviderPropertyDefinition StartAfterUTC = new SimpleProviderPropertyDefinition("StartAfterUTC", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035ED RID: 13805
		public static readonly ProviderPropertyDefinition CompleteAfter = new SimpleProviderPropertyDefinition("CompleteAfter", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035EE RID: 13806
		public static readonly ProviderPropertyDefinition CompleteAfterUTC = new SimpleProviderPropertyDefinition("CompleteAfterUTC", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035EF RID: 13807
		public static readonly ProviderPropertyDefinition AllowUnknownColumnsInCsv = new SimpleProviderPropertyDefinition("AllowUnknownColumnsInCsv", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035F0 RID: 13808
		public static readonly ProviderPropertyDefinition ReportInterval = new SimpleProviderPropertyDefinition("ReportInterval", ExchangeObjectVersion.Exchange2010, typeof(TimeSpan?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040035F1 RID: 13809
		public static readonly ProviderPropertyDefinition OriginalBatchId = new SimpleProviderPropertyDefinition("OriginalBatchId", ExchangeObjectVersion.Exchange2010, typeof(Guid?), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
