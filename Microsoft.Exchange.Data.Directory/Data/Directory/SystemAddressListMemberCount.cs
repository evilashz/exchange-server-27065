using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000654 RID: 1620
	internal static class SystemAddressListMemberCount
	{
		// Token: 0x06004BF7 RID: 19447 RVA: 0x00118629 File Offset: 0x00116829
		internal static int GetCount(IConfigurationSession configSession, OrganizationId organizationId, string systemAddressListName, bool useCache)
		{
			return SystemAddressListMemberCount.GetCount(configSession, organizationId, systemAddressListName, int.MaxValue, useCache);
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x0011863C File Offset: 0x0011683C
		internal static bool IsQuotaExceded(IConfigurationSession configSession, OrganizationId organizationId, string systemAddressListName, int countQuota)
		{
			int count = SystemAddressListMemberCount.GetCount(configSession, organizationId, systemAddressListName, countQuota, true);
			return count >= countQuota;
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0011865C File Offset: 0x0011685C
		private static int GetCount(IConfigurationSession configSession, OrganizationId organizationId, string systemAddressListName, int countQuota, bool useCache)
		{
			Organization organization = configSession.GetOrgContainer();
			if (organization == null || !object.Equals(organization.Identity, organizationId.ConfigurationUnit))
			{
				organization = configSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
				if (organization == null)
				{
					throw new TenantOrgContainerNotFoundException(organizationId.ToString());
				}
			}
			if (!organization.IsAddressListPagingEnabled)
			{
				return SystemAddressListMemberCount.GetBruteForceCountImmediate(configSession.DomainController, organizationId, systemAddressListName, countQuota);
			}
			if (useCache)
			{
				return SystemAddressListMemberCount.GetCountFromCache(configSession, organizationId, systemAddressListName, countQuota);
			}
			return SystemAddressListMemberCount.GetCountImmediate(configSession, organizationId, systemAddressListName);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x001186D0 File Offset: 0x001168D0
		private static int GetCountImmediate(IConfigurationSession session, OrganizationId orgId, string systemAddressListName)
		{
			AddressBookBase systemAddressList = SystemAddressListMemberCount.ReadSystemAddressListFromAD(session, orgId, systemAddressListName);
			SystemAddressListMemberCountCacheValue systemAddressListMemberCountCacheValue = new SystemAddressListMemberCountCacheValue(systemAddressList);
			return systemAddressListMemberCountCacheValue.GetMemberCountImmediate(session);
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x001186F4 File Offset: 0x001168F4
		private static int GetCountFromCache(IConfigurationSession session, OrganizationId orgId, string systemAddressListName, int quota)
		{
			int? num = null;
			SystemAddressListMemberCountCacheKey key = new SystemAddressListMemberCountCacheKey(orgId, systemAddressListName);
			SystemAddressListMemberCountCacheValue systemAddressListMemberCountCacheValue = null;
			try
			{
				if (!SystemAddressListMemberCount.cachedAddressListLock.TryEnterReadLock(SystemAddressListMemberCount.readerLockTimeout))
				{
					throw new TransientException(DirectoryStrings.ErrorTimeoutReadingSystemAddressListCache);
				}
				SystemAddressListMemberCount.memberCountCache.TryGetValue(key, out systemAddressListMemberCountCacheValue);
			}
			finally
			{
				try
				{
					SystemAddressListMemberCount.cachedAddressListLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (systemAddressListMemberCountCacheValue == null)
			{
				try
				{
					if (!SystemAddressListMemberCount.cachedAddressListLock.TryEnterUpgradeableReadLock(SystemAddressListMemberCount.readerLockTimeout))
					{
						throw new TransientException(DirectoryStrings.ErrorTimeoutReadingSystemAddressListCache);
					}
					if (!SystemAddressListMemberCount.memberCountCache.TryGetValue(key, out systemAddressListMemberCountCacheValue))
					{
						AddressBookBase systemAddressList = SystemAddressListMemberCount.ReadSystemAddressListFromAD(session, orgId, systemAddressListName);
						systemAddressListMemberCountCacheValue = new SystemAddressListMemberCountCacheValue(systemAddressList);
						num = new int?(systemAddressListMemberCountCacheValue.InitializeMemberCount(session, ExDateTime.UtcNow, quota));
						if (!SystemAddressListMemberCount.cachedAddressListLock.TryEnterWriteLock(SystemAddressListMemberCount.writerLockTimeout))
						{
							throw new TransientException(DirectoryStrings.ErrorTimeoutWritingSystemAddressListCache);
						}
						try
						{
							SystemAddressListMemberCount.memberCountCache.Add(key, systemAddressListMemberCountCacheValue);
						}
						finally
						{
							SystemAddressListMemberCount.cachedAddressListLock.ExitWriteLock();
						}
					}
				}
				finally
				{
					try
					{
						SystemAddressListMemberCount.cachedAddressListLock.ExitUpgradeableReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			if (num == null)
			{
				num = new int?(systemAddressListMemberCountCacheValue.GetMemberCount(session, ExDateTime.UtcNow, quota));
			}
			return num.Value;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x00118850 File Offset: 0x00116A50
		private static AddressBookBase ReadSystemAddressListFromAD(IConfigurationSession session, OrganizationId orgId, string systemAddressListName)
		{
			ADObjectId childId;
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				childId = session.SessionSettings.RootOrgId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId(systemAddressListName);
			}
			else
			{
				childId = orgId.ConfigurationUnit.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId(systemAddressListName);
			}
			return session.Read<AddressBookBase>(childId);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x001188AC File Offset: 0x00116AAC
		private static int GetBruteForceCountImmediate(string domainController, OrganizationId orgId, string systemAddressListName, int countQuota)
		{
			QueryFilter filter;
			if (!CannedSystemAddressLists.GetFilterByAddressList(systemAddressListName, out filter))
			{
				throw new ArgumentException("SystemAddressListName");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId, null, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 278, "GetBruteForceCountImmediate", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\SystemAddressListMemberCount.cs");
			ADPagedReader<MiniRecipient> adpagedReader = tenantOrRootOrgRecipientSession.FindPagedMiniRecipient<MiniRecipient>(null, QueryScope.OneLevel, filter, null, 0, new PropertyDefinition[]
			{
				ADObjectSchema.Id
			});
			int num = 0;
			foreach (MiniRecipient miniRecipient in adpagedReader)
			{
				num++;
				if (num >= countQuota)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x0400341F RID: 13343
		private static readonly TimeSpan readerLockTimeout = TimeSpan.FromSeconds(120.0);

		// Token: 0x04003420 RID: 13344
		private static readonly TimeSpan writerLockTimeout = TimeSpan.FromSeconds(300.0);

		// Token: 0x04003421 RID: 13345
		private static Dictionary<SystemAddressListMemberCountCacheKey, SystemAddressListMemberCountCacheValue> memberCountCache = new Dictionary<SystemAddressListMemberCountCacheKey, SystemAddressListMemberCountCacheValue>();

		// Token: 0x04003422 RID: 13346
		private static ReaderWriterLockSlim cachedAddressListLock = new ReaderWriterLockSlim();
	}
}
