using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000211 RID: 529
	internal class RequestJobSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000B24 RID: 2852
		public static readonly SimpleProviderPropertyDefinition UserId = new SimpleProviderPropertyDefinition("UserId", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B25 RID: 2853
		public static readonly SimpleProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B26 RID: 2854
		public static readonly SimpleProviderPropertyDefinition Alias = new SimpleProviderPropertyDefinition("Alias", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B27 RID: 2855
		public static readonly SimpleProviderPropertyDefinition SourceAlias = new SimpleProviderPropertyDefinition("SourceAlias", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B28 RID: 2856
		public static readonly SimpleProviderPropertyDefinition TargetAlias = new SimpleProviderPropertyDefinition("TargetAlias", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B29 RID: 2857
		public static readonly SimpleProviderPropertyDefinition SourceDatabase = new SimpleProviderPropertyDefinition("SourceDatabase", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B2A RID: 2858
		public static readonly SimpleProviderPropertyDefinition SourceVersion = new SimpleProviderPropertyDefinition("SourceVersion", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B2B RID: 2859
		public static readonly SimpleProviderPropertyDefinition SourceServer = new SimpleProviderPropertyDefinition("SourceServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B2C RID: 2860
		public static readonly SimpleProviderPropertyDefinition TargetDatabase = new SimpleProviderPropertyDefinition("TargetDatabase", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B2D RID: 2861
		public static readonly SimpleProviderPropertyDefinition TargetVersion = new SimpleProviderPropertyDefinition("TargetVersion", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B2E RID: 2862
		public static readonly SimpleProviderPropertyDefinition TargetServer = new SimpleProviderPropertyDefinition("TargetServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B2F RID: 2863
		public static readonly SimpleProviderPropertyDefinition TargetContainerGuid = new SimpleProviderPropertyDefinition("TargetContainerGuid", ExchangeObjectVersion.Exchange2012, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B30 RID: 2864
		public static readonly SimpleProviderPropertyDefinition TargetUnifiedMailboxId = new SimpleProviderPropertyDefinition("TargetUnifiedMailboxId", ExchangeObjectVersion.Exchange2012, typeof(CrossTenantObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B31 RID: 2865
		public static readonly SimpleProviderPropertyDefinition RequestQueue = new SimpleProviderPropertyDefinition("RequestQueue", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B32 RID: 2866
		public static readonly SimpleProviderPropertyDefinition SourceArchiveDatabase = new SimpleProviderPropertyDefinition("SourceArchiveDatabase", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B33 RID: 2867
		public static readonly SimpleProviderPropertyDefinition SourceArchiveVersion = new SimpleProviderPropertyDefinition("SourceArchiveVersion", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B34 RID: 2868
		public static readonly SimpleProviderPropertyDefinition SourceArchiveServer = new SimpleProviderPropertyDefinition("SourceArchiveServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B35 RID: 2869
		public static readonly SimpleProviderPropertyDefinition TargetArchiveDatabase = new SimpleProviderPropertyDefinition("TargetArchiveDatabase", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B36 RID: 2870
		public static readonly SimpleProviderPropertyDefinition TargetArchiveVersion = new SimpleProviderPropertyDefinition("TargetArchiveVersion", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B37 RID: 2871
		public static readonly SimpleProviderPropertyDefinition TargetArchiveServer = new SimpleProviderPropertyDefinition("TargetArchiveServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B38 RID: 2872
		public static readonly SimpleProviderPropertyDefinition ArchiveDomain = new SimpleProviderPropertyDefinition("ArchiveDomain", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B39 RID: 2873
		public static readonly SimpleProviderPropertyDefinition ExchangeGuid = new SimpleProviderPropertyDefinition("ExchangeGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B3A RID: 2874
		public static readonly SimpleProviderPropertyDefinition SourceExchangeGuid = new SimpleProviderPropertyDefinition("SourceExchangeGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B3B RID: 2875
		public static readonly SimpleProviderPropertyDefinition TargetExchangeGuid = new SimpleProviderPropertyDefinition("TargetExchangeGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B3C RID: 2876
		public static readonly SimpleProviderPropertyDefinition ArchiveGuid = new SimpleProviderPropertyDefinition("ArchiveGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B3D RID: 2877
		public static readonly SimpleProviderPropertyDefinition SourceIsArchive = new SimpleProviderPropertyDefinition("SourceIsArchive", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B3E RID: 2878
		public static readonly SimpleProviderPropertyDefinition TargetIsArchive = new SimpleProviderPropertyDefinition("TargetIsArchive", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B3F RID: 2879
		public static readonly SimpleProviderPropertyDefinition SourceRootFolder = new SimpleProviderPropertyDefinition("SourceRootFolder", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B40 RID: 2880
		public static readonly SimpleProviderPropertyDefinition TargetRootFolder = new SimpleProviderPropertyDefinition("TargetRootFolder", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B41 RID: 2881
		public static readonly SimpleProviderPropertyDefinition IncludeFolders = new SimpleProviderPropertyDefinition("IncludeFolders", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B42 RID: 2882
		public static readonly SimpleProviderPropertyDefinition ExcludeFolders = new SimpleProviderPropertyDefinition("ExcludeFolders", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B43 RID: 2883
		public static readonly SimpleProviderPropertyDefinition ExcludeDumpster = new SimpleProviderPropertyDefinition("ExcludeDumpster", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B44 RID: 2884
		public static readonly SimpleProviderPropertyDefinition RequestGuid = new SimpleProviderPropertyDefinition("RequestGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B45 RID: 2885
		public static readonly SimpleProviderPropertyDefinition Status = new SimpleProviderPropertyDefinition("Status", ExchangeObjectVersion.Exchange2010, typeof(RequestStatus), PropertyDefinitionFlags.None, RequestStatus.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(RequestStatus))
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B46 RID: 2886
		public static readonly SimpleProviderPropertyDefinition Flags = new SimpleProviderPropertyDefinition("Flags", ExchangeObjectVersion.Exchange2010, typeof(RequestFlags), PropertyDefinitionFlags.None, RequestFlags.None, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateMailboxMoveFlags))
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B47 RID: 2887
		public static readonly SimpleProviderPropertyDefinition RemoteHostName = new SimpleProviderPropertyDefinition("RemoteHostName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B48 RID: 2888
		public static readonly SimpleProviderPropertyDefinition RemoteHostPort = new SimpleProviderPropertyDefinition("RemoteHostPort", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B49 RID: 2889
		public static readonly SimpleProviderPropertyDefinition SmtpServerName = new SimpleProviderPropertyDefinition("SmtpServerName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B4A RID: 2890
		public static readonly SimpleProviderPropertyDefinition SmtpServerPort = new SimpleProviderPropertyDefinition("SmtpServerPort", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B4B RID: 2891
		public static readonly SimpleProviderPropertyDefinition SecurityMechanism = new SimpleProviderPropertyDefinition("SecurityMechanism", ExchangeObjectVersion.Exchange2010, typeof(IMAPSecurityMechanism), PropertyDefinitionFlags.None, IMAPSecurityMechanism.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B4C RID: 2892
		public static readonly SimpleProviderPropertyDefinition SyncProtocol = new SimpleProviderPropertyDefinition("SyncProtocol", ExchangeObjectVersion.Exchange2010, typeof(SyncProtocol), PropertyDefinitionFlags.None, Microsoft.Exchange.MailboxReplicationService.SyncProtocol.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B4D RID: 2893
		public static readonly SimpleProviderPropertyDefinition EmailAddress = new SimpleProviderPropertyDefinition("EmailAddress", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B4E RID: 2894
		public static readonly SimpleProviderPropertyDefinition IncrementalSyncInterval = new SimpleProviderPropertyDefinition("IncrementalSyncInterval", ExchangeObjectVersion.Exchange2010, typeof(TimeSpan), PropertyDefinitionFlags.None, TimeSpan.Zero, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B4F RID: 2895
		public static readonly SimpleProviderPropertyDefinition BatchName = new SimpleProviderPropertyDefinition("BatchName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 255)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B50 RID: 2896
		public static readonly SimpleProviderPropertyDefinition RequestJobState = new SimpleProviderPropertyDefinition("RequestJobState", ExchangeObjectVersion.Exchange2010, typeof(JobProcessingState), PropertyDefinitionFlags.None, JobProcessingState.NotReady, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B51 RID: 2897
		public static readonly SimpleProviderPropertyDefinition RemoteDatabaseName = new SimpleProviderPropertyDefinition("RemoteDatabaseName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B52 RID: 2898
		public static readonly SimpleProviderPropertyDefinition RemoteDatabaseGuid = new SimpleProviderPropertyDefinition("RemoteDatabaseGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B53 RID: 2899
		public static readonly SimpleProviderPropertyDefinition RemoteArchiveDatabaseName = new SimpleProviderPropertyDefinition("RemoteArchiveDatabaseName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B54 RID: 2900
		public static readonly SimpleProviderPropertyDefinition RemoteArchiveDatabaseGuid = new SimpleProviderPropertyDefinition("RemoteArchiveDatabaseGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B55 RID: 2901
		public static readonly SimpleProviderPropertyDefinition BadItemLimit = new SimpleProviderPropertyDefinition("BadItemLimit", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B56 RID: 2902
		public static readonly SimpleProviderPropertyDefinition BadItemsEncountered = new SimpleProviderPropertyDefinition("BadItemsEncountered", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B57 RID: 2903
		public static readonly SimpleProviderPropertyDefinition LargeItemLimit = new SimpleProviderPropertyDefinition("LargeItemLimit", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B58 RID: 2904
		public static readonly SimpleProviderPropertyDefinition LargeItemsEncountered = new SimpleProviderPropertyDefinition("LargeItemsEncountered", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B59 RID: 2905
		public static readonly SimpleProviderPropertyDefinition MissingItemsEncountered = new SimpleProviderPropertyDefinition("MissingItemsEncountered", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B5A RID: 2906
		public static readonly SimpleProviderPropertyDefinition AllowLargeItems = new SimpleProviderPropertyDefinition("AllowLargeItems", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B5B RID: 2907
		public static readonly SimpleProviderPropertyDefinition MRSServerName = new SimpleProviderPropertyDefinition("MRSServerName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B5C RID: 2908
		public static readonly SimpleProviderPropertyDefinition TotalMailboxItemCount = new SimpleProviderPropertyDefinition("TotalMailboxItemCount", ExchangeObjectVersion.Exchange2010, typeof(ulong), PropertyDefinitionFlags.PersistDefaultValue, 0UL, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B5D RID: 2909
		public static readonly SimpleProviderPropertyDefinition TotalArchiveItemCount = new SimpleProviderPropertyDefinition("TotalArchiveItemCount", ExchangeObjectVersion.Exchange2010, typeof(ulong?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B5E RID: 2910
		public static readonly SimpleProviderPropertyDefinition ItemsTransferred = new SimpleProviderPropertyDefinition("ItemsTransferred", ExchangeObjectVersion.Exchange2010, typeof(ulong?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B5F RID: 2911
		public static readonly SimpleProviderPropertyDefinition PercentComplete = new SimpleProviderPropertyDefinition("PercentComplete", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B60 RID: 2912
		public static readonly SimpleProviderPropertyDefinition FailureCode = new SimpleProviderPropertyDefinition("FailureCode", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B61 RID: 2913
		public static readonly SimpleProviderPropertyDefinition FailureType = new SimpleProviderPropertyDefinition("FailureType", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B62 RID: 2914
		public static readonly SimpleProviderPropertyDefinition FailureSide = new SimpleProviderPropertyDefinition("FailureSide", ExchangeObjectVersion.Exchange2010, typeof(ExceptionSide?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B63 RID: 2915
		public static readonly SimpleProviderPropertyDefinition Message = new SimpleProviderPropertyDefinition("Message", ExchangeObjectVersion.Exchange2010, typeof(LocalizedString), PropertyDefinitionFlags.None, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B64 RID: 2916
		public static readonly SimpleProviderPropertyDefinition RemoteCredentialUsername = new SimpleProviderPropertyDefinition("RemoteCredentialUsername", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B65 RID: 2917
		public static readonly SimpleProviderPropertyDefinition RemoteOrgName = new SimpleProviderPropertyDefinition("RemoteOrgName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B66 RID: 2918
		public static readonly SimpleProviderPropertyDefinition AllowedToFinishMove = new SimpleProviderPropertyDefinition("AllowedToFinishJob", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B67 RID: 2919
		public static readonly SimpleProviderPropertyDefinition PreserveMailboxSignature = new SimpleProviderPropertyDefinition("PreserveMailboxSignature", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B68 RID: 2920
		public static readonly SimpleProviderPropertyDefinition CancelRequest = new SimpleProviderPropertyDefinition("CancelRequest", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B69 RID: 2921
		public static readonly SimpleProviderPropertyDefinition DomainControllerToUpdate = new SimpleProviderPropertyDefinition("DomainControllerToUpdate", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.PersistDefaultValue, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B6A RID: 2922
		public static readonly SimpleProviderPropertyDefinition RemoteDomainControllerToUpdate = new SimpleProviderPropertyDefinition("RemoteDomainControllerToUpdate", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.PersistDefaultValue, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B6B RID: 2923
		public static readonly SimpleProviderPropertyDefinition SyncStage = new SimpleProviderPropertyDefinition("SyncStage", ExchangeObjectVersion.Exchange2010, typeof(SyncStage), PropertyDefinitionFlags.None, Microsoft.Exchange.MailboxReplicationService.SyncStage.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B6C RID: 2924
		public static readonly SimpleProviderPropertyDefinition SourceDCName = new SimpleProviderPropertyDefinition("SourceDCName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B6D RID: 2925
		public static readonly SimpleProviderPropertyDefinition TargetDCName = new SimpleProviderPropertyDefinition("TargetDCName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B6E RID: 2926
		public static readonly SimpleProviderPropertyDefinition TotalMailboxSize = new SimpleProviderPropertyDefinition("TotalMailboxSize", ExchangeObjectVersion.Exchange2010, typeof(ulong), PropertyDefinitionFlags.PersistDefaultValue, 0UL, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B6F RID: 2927
		public static readonly SimpleProviderPropertyDefinition TotalArchiveSize = new SimpleProviderPropertyDefinition("TotalArchiveSize", ExchangeObjectVersion.Exchange2010, typeof(ulong?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B70 RID: 2928
		public static readonly SimpleProviderPropertyDefinition BytesTransferred = new SimpleProviderPropertyDefinition("BytesTransferred", ExchangeObjectVersion.Exchange2010, typeof(ulong?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B71 RID: 2929
		public static readonly SimpleProviderPropertyDefinition RetryCount = new SimpleProviderPropertyDefinition("RetryCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B72 RID: 2930
		public static readonly SimpleProviderPropertyDefinition TotalRetryCount = new SimpleProviderPropertyDefinition("TotalRetryCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B73 RID: 2931
		public static readonly SimpleProviderPropertyDefinition UserOrgName = new SimpleProviderPropertyDefinition("UserOrgName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B74 RID: 2932
		public static readonly SimpleProviderPropertyDefinition TargetDeliveryDomain = new SimpleProviderPropertyDefinition("TargetDeliveryDomain", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B75 RID: 2933
		public static readonly SimpleProviderPropertyDefinition IgnoreRuleLimitErrors = new SimpleProviderPropertyDefinition("IgnoreRuleLimitErrors", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B76 RID: 2934
		public static readonly SimpleProviderPropertyDefinition JobType = new SimpleProviderPropertyDefinition("JobType", ExchangeObjectVersion.Exchange2010, typeof(MRSJobType), PropertyDefinitionFlags.None, MRSJobType.RequestJobE14R3, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B77 RID: 2935
		public static readonly SimpleProviderPropertyDefinition RequestType = new SimpleProviderPropertyDefinition("RequestType", ExchangeObjectVersion.Exchange2010, typeof(MRSRequestType), PropertyDefinitionFlags.PersistDefaultValue, MRSRequestType.Move, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B78 RID: 2936
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("RequestName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B79 RID: 2937
		public static readonly SimpleProviderPropertyDefinition FilePath = new SimpleProviderPropertyDefinition("FilePath", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B7A RID: 2938
		public static readonly SimpleProviderPropertyDefinition MailboxRestoreFlags = new SimpleProviderPropertyDefinition("MailboxRestoreFlags", ExchangeObjectVersion.Exchange2010, typeof(MailboxRestoreType?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B7B RID: 2939
		public static readonly SimpleProviderPropertyDefinition SourceUserId = new SimpleProviderPropertyDefinition("SourceUser", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B7C RID: 2940
		public static readonly SimpleProviderPropertyDefinition TargetUserId = new SimpleProviderPropertyDefinition("TargetUser", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B7D RID: 2941
		public static readonly SimpleProviderPropertyDefinition RemoteUserName = new SimpleProviderPropertyDefinition("RemoteUserName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B7E RID: 2942
		public static readonly SimpleProviderPropertyDefinition RemoteMailboxLegacyDN = new SimpleProviderPropertyDefinition("RemoteMailboxLegacyDN", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B7F RID: 2943
		public static readonly SimpleProviderPropertyDefinition RemoteMailboxServerLegacyDN = new SimpleProviderPropertyDefinition("RemoteMailboxServerLegacyDN", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B80 RID: 2944
		public static readonly SimpleProviderPropertyDefinition RemoteUserLegacyDN = new SimpleProviderPropertyDefinition("RemoteUserLegacyDN", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B81 RID: 2945
		public static readonly SimpleProviderPropertyDefinition OutlookAnywhereHostName = new SimpleProviderPropertyDefinition("OutlookAnywhereHostName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B82 RID: 2946
		public static readonly SimpleProviderPropertyDefinition AuthMethod = new SimpleProviderPropertyDefinition("AuthMethod", ExchangeObjectVersion.Exchange2010, typeof(AuthenticationMethod?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B83 RID: 2947
		public static readonly SimpleProviderPropertyDefinition IsAdministrativeCredential = new SimpleProviderPropertyDefinition("IsAdministrativeCredential", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B84 RID: 2948
		public static readonly SimpleProviderPropertyDefinition ConflictResolutionOption = new SimpleProviderPropertyDefinition("ConflictResolutionOption", ExchangeObjectVersion.Exchange2010, typeof(ConflictResolutionOption?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B85 RID: 2949
		public static readonly SimpleProviderPropertyDefinition AssociatedMessagesCopyOption = new SimpleProviderPropertyDefinition("AssociatedMessagesCopyOption", ExchangeObjectVersion.Exchange2010, typeof(FAICopyOption?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B86 RID: 2950
		public static readonly SimpleProviderPropertyDefinition OrganizationalUnitRoot = new SimpleProviderPropertyDefinition("OrganizationalUnitRoot", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B87 RID: 2951
		public static readonly SimpleProviderPropertyDefinition ConfigurationUnit = new SimpleProviderPropertyDefinition("ConfigurationUnit", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B88 RID: 2952
		public static readonly SimpleProviderPropertyDefinition OrganizationId = new SimpleProviderPropertyDefinition("OrganizationId", ExchangeObjectVersion.Exchange2010, typeof(OrganizationId), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimpleProviderPropertyDefinition[]
		{
			RequestJobSchema.OrganizationalUnitRoot,
			RequestJobSchema.ConfigurationUnit
		}, null, new GetterDelegate(RequestJobBase.OrganizationIdGetter), new SetterDelegate(RequestJobBase.OrganizationIdSetter));

		// Token: 0x04000B89 RID: 2953
		public static readonly SimpleProviderPropertyDefinition ContentFilter = new SimpleProviderPropertyDefinition("ContentFilter", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B8A RID: 2954
		public static readonly SimpleProviderPropertyDefinition ContentFilterLCID = new SimpleProviderPropertyDefinition("ContentFilterLCID", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, CultureInfo.InvariantCulture.LCID, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B8B RID: 2955
		public static readonly SimpleProviderPropertyDefinition Priority = new SimpleProviderPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2010, typeof(RequestPriority), PropertyDefinitionFlags.PersistDefaultValue, RequestPriority.Normal, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B8C RID: 2956
		public static readonly SimpleProviderPropertyDefinition WorkloadType = new SimpleProviderPropertyDefinition("WorkloadType", ExchangeObjectVersion.Exchange2010, typeof(RequestWorkloadType), PropertyDefinitionFlags.PersistDefaultValue, RequestWorkloadType.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B8D RID: 2957
		public static readonly SimpleProviderPropertyDefinition JobInternalFlags = new SimpleProviderPropertyDefinition("JobInternalFlags", ExchangeObjectVersion.Exchange2010, typeof(RequestJobInternalFlags), PropertyDefinitionFlags.PersistDefaultValue, RequestJobInternalFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B8E RID: 2958
		public static readonly SimpleProviderPropertyDefinition CompletedRequestAgeLimit = new SimpleProviderPropertyDefinition("CompletedRequestAgeLimit", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<EnhancedTimeSpan>), PropertyDefinitionFlags.None, Unlimited<EnhancedTimeSpan>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x04000B8F RID: 2959
		public static readonly SimpleProviderPropertyDefinition RequestCreator = new SimpleProviderPropertyDefinition("RequestCreator", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B90 RID: 2960
		public static readonly SimpleProviderPropertyDefinition RehomeRequest = new SimpleProviderPropertyDefinition("RehomeRequest", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B91 RID: 2961
		public static readonly SimpleProviderPropertyDefinition PoisonCount = new SimpleProviderPropertyDefinition("PoisonCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B92 RID: 2962
		public static readonly SimpleProviderPropertyDefinition ContentCodePage = new SimpleProviderPropertyDefinition("ContentCodePage", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B93 RID: 2963
		public static readonly SimpleProviderPropertyDefinition RecipientTypeDetails = new SimpleProviderPropertyDefinition("RecipientTypeDetails", ExchangeObjectVersion.Exchange2010, typeof(long), PropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B94 RID: 2964
		public static readonly SimpleProviderPropertyDefinition LastPickupTime = new SimpleProviderPropertyDefinition("LastPickupTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B95 RID: 2965
		public static readonly SimpleProviderPropertyDefinition RestartingAfterSignatureChange = new SimpleProviderPropertyDefinition("RestartingAfterSignatureChange", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B96 RID: 2966
		public static readonly SimpleProviderPropertyDefinition IsIntegData = new SimpleProviderPropertyDefinition("IsIntegData", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B97 RID: 2967
		public static readonly SimpleProviderPropertyDefinition UserPuid = new SimpleProviderPropertyDefinition("UserPuid", ExchangeObjectVersion.Exchange2010, typeof(long?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000B98 RID: 2968
		public static readonly SimpleProviderPropertyDefinition OlcDGroup = new SimpleProviderPropertyDefinition("OlcDGroup", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
