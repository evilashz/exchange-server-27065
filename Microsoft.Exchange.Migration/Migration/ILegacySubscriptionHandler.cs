using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000139 RID: 313
	internal interface ILegacySubscriptionHandler : IForceReportDisposeTrackable, IDisposeTrackable, IDisposable
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000FC1 RID: 4033
		bool SupportsDupeDetection { get; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000FC2 RID: 4034
		bool SupportsActiveIncrementalSync { get; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000FC3 RID: 4035
		bool SupportsAdvancedValidation { get; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000FC4 RID: 4036
		MigrationType SupportedMigrationType { get; }

		// Token: 0x06000FC5 RID: 4037
		bool CreateUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FC6 RID: 4038
		void DeleteUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FC7 RID: 4039
		void DisableSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FC8 RID: 4040
		void ResumeUnderlyingSubscriptions(MigrationUserStatus startedStatus, MigrationJobItem jobItem);

		// Token: 0x06000FC9 RID: 4041
		MigrationProcessorResult SyncToUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FCA RID: 4042
		void CancelUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FCB RID: 4043
		void StopUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FCC RID: 4044
		bool TestCreateUnderlyingSubscriptions(MigrationJobItem jobItem);

		// Token: 0x06000FCD RID: 4045
		void SyncSubscriptionSettings(MigrationJobItem jobItem);

		// Token: 0x06000FCE RID: 4046
		IEnumerable<MigrationJobItem> GetJobItemsForSubscriptionCheck(ExDateTime? cutoffTime, MigrationUserStatus status, int maxItemsToCheck);
	}
}
