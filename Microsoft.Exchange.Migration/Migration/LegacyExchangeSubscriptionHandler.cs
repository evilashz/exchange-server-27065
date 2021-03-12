using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000155 RID: 341
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LegacyExchangeSubscriptionHandler : LegacyMrsSubscriptionHandlerBase
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x00047372 File Offset: 0x00045572
		internal LegacyExchangeSubscriptionHandler(IMigrationDataProvider dataProvider, MigrationJob job) : base(dataProvider, job, MRSSubscriptionArbiter.Instance)
		{
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00047381 File Offset: 0x00045581
		public override MigrationType SupportedMigrationType
		{
			get
			{
				return MigrationType.ExchangeOutlookAnywhere;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00047384 File Offset: 0x00045584
		public override bool SupportsAdvancedValidation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00047387 File Offset: 0x00045587
		protected override MigrationUserStatus PostTestStatus
		{
			get
			{
				return MigrationUserStatus.Provisioning;
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004738B File Offset: 0x0004558B
		public override bool CreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			return base.InternalCreate(jobItem, false);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00047395 File Offset: 0x00045595
		public override bool TestCreateUnderlyingSubscriptions(MigrationJobItem jobItem)
		{
			return base.InternalCreate(jobItem, true);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0004739F File Offset: 0x0004559F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LegacyExchangeSubscriptionHandler>(this);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000473A8 File Offset: 0x000455A8
		protected override bool DiscoverAndSetSubscriptionSettings(MigrationJobItem jobItem)
		{
			ExchangeOutlookAnywhereEndpoint exchangeOutlookAnywhereEndpoint = jobItem.MigrationJob.SourceEndpoint as ExchangeOutlookAnywhereEndpoint;
			MigrationUtil.AssertOrThrow(exchangeOutlookAnywhereEndpoint != null, "An SEM job should have an ExchangeOutlookAnywhereEndpoint as its source.", new object[0]);
			if (exchangeOutlookAnywhereEndpoint.UseAutoDiscover)
			{
				IMigrationAutodiscoverClient autodiscoverClient = MigrationServiceFactory.Instance.GetAutodiscoverClient();
				AutodiscoverClientResponse userSettings = autodiscoverClient.GetUserSettings(exchangeOutlookAnywhereEndpoint, jobItem.RemoteIdentifier ?? jobItem.Identifier);
				if (userSettings.Status != AutodiscoverClientStatus.NoError)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "job item {0} couldn't get auto-discover settings {1}", new object[]
					{
						this,
						userSettings.ErrorMessage
					});
					LocalizedException localizedError;
					if (userSettings.Status == AutodiscoverClientStatus.ConfigurationError)
					{
						localizedError = new AutoDiscoverFailedConfigurationErrorException(userSettings.ErrorMessage);
					}
					else
					{
						localizedError = new AutoDiscoverFailedInternalErrorException(userSettings.ErrorMessage);
					}
					jobItem.SetSubscriptionFailed(base.DataProvider, MigrationUserStatus.Failed, localizedError);
					return false;
				}
				jobItem.SetSubscriptionSettings(base.DataProvider, ExchangeJobItemSubscriptionSettings.CreateFromAutodiscoverResponse(userSettings));
			}
			else
			{
				jobItem.SetSubscriptionSettings(base.DataProvider, null);
			}
			return true;
		}
	}
}
