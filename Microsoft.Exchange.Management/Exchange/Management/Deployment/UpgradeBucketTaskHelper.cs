using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000285 RID: 645
	internal sealed class UpgradeBucketTaskHelper
	{
		// Token: 0x060017A4 RID: 6052 RVA: 0x00063EE4 File Offset: 0x000620E4
		internal static int GetMailboxCount(ExchangeUpgradeBucket bucket)
		{
			int num = 0;
			List<ADObjectId> list = new List<ADObjectId>(bucket.Organizations.Count);
			foreach (ADObjectId adobjectId in bucket.Organizations)
			{
				int? mailboxCountFromCache = UpgradeBucketTaskHelper.MailboxCountCache.GetMailboxCountFromCache(adobjectId);
				if (mailboxCountFromCache != null)
				{
					num += mailboxCountFromCache.Value;
				}
				else
				{
					ADObjectIdResolutionHelper.ResolveDN(adobjectId);
				}
			}
			list.Sort((ADObjectId x, ADObjectId y) => x.PartitionGuid.CompareTo(y.PartitionGuid));
			ADObjectId adobjectId2 = null;
			ITenantConfigurationSession tenantConfigurationSession = null;
			foreach (ADObjectId adobjectId3 in list)
			{
				if (adobjectId2 == null || !adobjectId2.PartitionGuid.Equals(adobjectId3.PartitionGuid))
				{
					tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsObjectId(adobjectId3), 59, "GetMailboxCount", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\UpgradeBucketTaskHelper.cs");
				}
				ExchangeConfigurationUnit exchangeConfigurationUnit = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(adobjectId3);
				if (exchangeConfigurationUnit != null)
				{
					int mailboxCount = UpgradeBucketTaskHelper.MailboxCountCache.GetMailboxCount(exchangeConfigurationUnit.OrganizationId, tenantConfigurationSession);
					num += mailboxCount;
				}
				adobjectId2 = adobjectId3;
			}
			return num;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00064038 File Offset: 0x00062238
		internal static void ValidateSourceAndTargetVersions(string sourceVersion, string targetVersion, Task.ErrorLoggerDelegate errorLogger)
		{
			string[] array = sourceVersion.Split(new char[]
			{
				'.'
			});
			string[] array2 = targetVersion.Split(new char[]
			{
				'.'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != "*" && array2[i] != "*")
				{
					if (int.Parse(array[i]) < int.Parse(array2[i]))
					{
						return;
					}
					if (int.Parse(array[i]) > int.Parse(array2[i]))
					{
						errorLogger(new RecipientTaskException(Strings.ExchangeUpgradeBucketSourceVersionBiggerThanTarget(sourceVersion, targetVersion)), ExchangeErrorCategory.Client, null);
					}
				}
				else
				{
					errorLogger((array[i] == "*") ? new RecipientTaskException(Strings.ExchangeUpgradeBucketTargetIncludedInSource(sourceVersion, targetVersion)) : new RecipientTaskException(Strings.ExchangeUpgradeBucketSourceIncludedInTarget(sourceVersion, targetVersion)), ExchangeErrorCategory.Client, null);
				}
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00064118 File Offset: 0x00062318
		internal static void ValidateOrganizationAddition(ITopologyConfigurationSession configSession, OrganizationId organizationId, ExchangeUpgradeBucket exchangeUpgradeBucket, Task.ErrorLoggerDelegate errorLogger)
		{
			if (!exchangeUpgradeBucket.MaxMailboxes.IsUnlimited && !exchangeUpgradeBucket.Organizations.Contains(organizationId.ConfigurationUnit))
			{
				int mailboxCount = UpgradeBucketTaskHelper.MailboxCountCache.GetMailboxCount(organizationId, configSession);
				int mailboxCount2 = UpgradeBucketTaskHelper.GetMailboxCount(exchangeUpgradeBucket);
				int num = exchangeUpgradeBucket.MaxMailboxes.Value - mailboxCount2;
				if (mailboxCount > num)
				{
					errorLogger(new RecipientTaskException(Strings.ExchangeUpgradeBucketNotEnoughCapacity(exchangeUpgradeBucket.ToString(), num.ToString(), mailboxCount.ToString())), ExchangeErrorCategory.Client, organizationId);
				}
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0006419C File Offset: 0x0006239C
		internal static void ValidateOrganizationVersion(ExchangeConfigurationUnit configurationUnit, ExchangeUpgradeBucket exchangeUpgradeBucket, Task.ErrorLoggerDelegate errorLogger)
		{
			string text = configurationUnit.IsUpgradingOrganization ? exchangeUpgradeBucket.TargetVersion : exchangeUpgradeBucket.SourceVersion;
			if (!UpgradeBucketTaskHelper.ValidateExchangeObjectVersion(configurationUnit.AdminDisplayVersion, text))
			{
				errorLogger(new RecipientTaskException(Strings.ExchangeUpgradeBucketInvalidOrganizationVersion(configurationUnit.AdminDisplayVersion.ToString(), text)), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000641F0 File Offset: 0x000623F0
		private static bool ValidateExchangeObjectVersion(ExchangeObjectVersion version, string versionPattern)
		{
			string[] array = versionPattern.Split(new char[]
			{
				'.'
			});
			int[] array2 = new int[]
			{
				(int)version.ExchangeBuild.Major,
				(int)version.ExchangeBuild.Minor,
				(int)version.ExchangeBuild.Build,
				(int)version.ExchangeBuild.BuildRevision
			};
			int num = 0;
			while (num < array.Length && !(array[num] == "*"))
			{
				if (int.Parse(array[num]) != array2[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x04000A00 RID: 2560
		private static readonly UpgradeBucketTaskHelper.OrganizationMailboxCountCache MailboxCountCache = new UpgradeBucketTaskHelper.OrganizationMailboxCountCache();

		// Token: 0x02000286 RID: 646
		private sealed class OrganizationMailboxCountCache
		{
			// Token: 0x060017AC RID: 6060 RVA: 0x0006429C File Offset: 0x0006249C
			public int? GetMailboxCountFromCache(ADObjectId organizationId)
			{
				UpgradeBucketTaskHelper.OrganizationMailboxCountCache.OrganizationCountCacheEntry organizationCountCacheEntry;
				if (this.organizationCache.TryGetValue(organizationId, out organizationCountCacheEntry) && ExDateTime.Now - organizationCountCacheEntry.WhenRead < UpgradeBucketTaskHelper.OrganizationMailboxCountCache.CacheLifeTime)
				{
					return new int?(organizationCountCacheEntry.MailboxCount);
				}
				return null;
			}

			// Token: 0x060017AD RID: 6061 RVA: 0x000642EC File Offset: 0x000624EC
			public int GetMailboxCount(OrganizationId organizationId, IConfigurationSession configSession)
			{
				int? mailboxCountFromCache = this.GetMailboxCountFromCache(organizationId.ConfigurationUnit);
				if (mailboxCountFromCache == null)
				{
					mailboxCountFromCache = new int?(SystemAddressListMemberCount.GetCount(configSession, organizationId, "All Mailboxes(VLV)", false));
					this.organizationCache[organizationId.ConfigurationUnit] = new UpgradeBucketTaskHelper.OrganizationMailboxCountCache.OrganizationCountCacheEntry
					{
						WhenRead = ExDateTime.Now,
						MailboxCount = mailboxCountFromCache.Value
					};
				}
				return mailboxCountFromCache.Value;
			}

			// Token: 0x04000A02 RID: 2562
			private static readonly TimeSpan CacheLifeTime = new TimeSpan(6, 0, 0);

			// Token: 0x04000A03 RID: 2563
			private readonly Dictionary<ADObjectId, UpgradeBucketTaskHelper.OrganizationMailboxCountCache.OrganizationCountCacheEntry> organizationCache = new Dictionary<ADObjectId, UpgradeBucketTaskHelper.OrganizationMailboxCountCache.OrganizationCountCacheEntry>();

			// Token: 0x02000287 RID: 647
			private struct OrganizationCountCacheEntry
			{
				// Token: 0x04000A04 RID: 2564
				public ExDateTime WhenRead;

				// Token: 0x04000A05 RID: 2565
				public int MailboxCount;
			}
		}
	}
}
