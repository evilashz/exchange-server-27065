using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200019A RID: 410
	internal class SharedConfiguration
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x00053DA8 File Offset: 0x00051FA8
		private SharedConfiguration(OrganizationId tinyTenantId, ExchangeConfigurationUnit sharedConfigurationCU)
		{
			this.tinyTenantId = tinyTenantId;
			this.sharedConfigurationCU = sharedConfigurationCU;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00053DBE File Offset: 0x00051FBE
		public static string SCTNamePrefix
		{
			get
			{
				return SharedConfiguration.sctNamePrefix;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00053DC5 File Offset: 0x00051FC5
		public static int SctNameMaxLength
		{
			get
			{
				return SharedConfiguration.sctNameMaxLength;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00053DCC File Offset: 0x00051FCC
		public OrganizationId TinyTenantId
		{
			get
			{
				return this.tinyTenantId;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00053DD4 File Offset: 0x00051FD4
		public ExchangeConfigurationUnit TenantCU
		{
			get
			{
				this.LoadTinyTenantCUIfNecessary();
				return this.tinyTenantCU;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00053DE2 File Offset: 0x00051FE2
		public OrganizationId SharedConfigId
		{
			get
			{
				return this.sharedConfigurationCU.OrganizationId;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x00053DEF File Offset: 0x00051FEF
		public ExchangeConfigurationUnit SharedConfigurationCU
		{
			get
			{
				return this.sharedConfigurationCU;
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00053DF8 File Offset: 0x00051FF8
		public static ExchangeConfigurationUnit[] LoadSharedConfigurationsSorted(OrganizationId orgId)
		{
			if (orgId.Equals(OrganizationId.ForestWideOrgId))
			{
				return null;
			}
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsPartitionId(orgId.PartitionId), 151, "LoadSharedConfigurationsSorted", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
			ExchangeConfigurationUnit[] array = tenantConfigurationSession.FindSharedConfigurationByOrganizationId(orgId);
			if (array.Length > 1)
			{
				Array.Sort<ExchangeConfigurationUnit>(array, new Comparison<ExchangeConfigurationUnit>(SharedConfiguration.CompareBySharedConfigurationInfo));
			}
			return array;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00053E5C File Offset: 0x0005205C
		public static SharedConfiguration GetSharedConfiguration(OrganizationId orgId)
		{
			ExchangeConfigurationUnit[] array = SharedConfiguration.LoadSharedConfigurationsSorted(orgId);
			if (array == null)
			{
				return null;
			}
			ExchangeConfigurationUnit exchangeConfigurationUnit = null;
			if (array.Length == 1)
			{
				exchangeConfigurationUnit = array[0];
			}
			else if (array.Length > 1)
			{
				ExchangeConfigurationUnit exchangeConfigurationUnit2 = null;
				int i = 0;
				while (i < array.Length)
				{
					exchangeConfigurationUnit = array[i];
					int num = ServerVersion.Compare(exchangeConfigurationUnit.SharedConfigurationInfo.CurrentVersion, ServerVersion.InstalledVersion);
					if (num == 0)
					{
						break;
					}
					if (num > 0)
					{
						if (exchangeConfigurationUnit.SharedConfigurationInfo.CurrentVersion.Major > ServerVersion.InstalledVersion.Major && exchangeConfigurationUnit2 != null)
						{
							exchangeConfigurationUnit = exchangeConfigurationUnit2;
							break;
						}
						break;
					}
					else
					{
						exchangeConfigurationUnit2 = array[i];
						i++;
					}
				}
			}
			if (exchangeConfigurationUnit != null)
			{
				return new SharedConfiguration(orgId, exchangeConfigurationUnit);
			}
			return null;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00053EF0 File Offset: 0x000520F0
		internal static int CompareBySharedConfigurationInfo(ExchangeConfigurationUnit x, ExchangeConfigurationUnit y)
		{
			int result;
			if (x == null)
			{
				if (y == null)
				{
					result = 0;
				}
				else
				{
					result = -1;
				}
			}
			else if (y == null)
			{
				result = 1;
			}
			else
			{
				result = ServerVersion.Compare(x.SharedConfigurationInfo.CurrentVersion, y.SharedConfigurationInfo.CurrentVersion);
			}
			return result;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00053F30 File Offset: 0x00052130
		public static bool IsSharedConfiguration(OrganizationId orgId)
		{
			if (OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				return false;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(orgId.PartitionId), 279, "IsSharedConfiguration", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
			ExchangeConfigurationUnit[] array = tenantOrTopologyConfigurationSession.Find<ExchangeConfigurationUnit>(orgId.ConfigurationUnit, QueryScope.Base, new ExistsFilter(OrganizationSchema.SharedConfigurationInfo), null, 1);
			return array != null && array.Length == 1;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00054010 File Offset: 0x00052210
		public static bool IsDehydratedConfiguration(OrganizationId orgId)
		{
			return Globals.IsDatacenter && !(orgId == null) && !OrganizationId.ForestWideOrgId.Equals(orgId) && !DatacenterRegistry.IsForefrontForOffice() && ProvisioningCache.Instance.TryAddAndGetOrganizationData<bool>(CannedProvisioningCacheKeys.IsDehydratedConfigurationCacheKey, orgId, delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(orgId.PartitionId), 310, "IsDehydratedConfiguration", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
				ExchangeConfigurationUnit[] array = tenantOrTopologyConfigurationSession.Find<ExchangeConfigurationUnit>(orgId.ConfigurationUnit, QueryScope.Base, new ExistsFilter(OrganizationSchema.SupportedSharedConfigurations), null, 1);
				return array != null && array.Length == 1 && array[0].IsDehydrated;
			});
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0005407B File Offset: 0x0005227B
		public static bool IsDehydratedConfiguration(IConfigurationSession adConfigSession)
		{
			return SharedConfiguration.IsDehydratedConfiguration(adConfigSession.SessionSettings.CurrentOrganizationId);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00054090 File Offset: 0x00052290
		public static IConfigurationSession CreateScopedToSharedConfigADSession(OrganizationId orgId)
		{
			ADSessionSettings adsessionSettings = null;
			if (SharedConfiguration.IsDehydratedConfiguration(orgId) || SharedConfiguration.IsSharedConfiguration(orgId))
			{
				SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(orgId);
				if (sharedConfiguration != null)
				{
					adsessionSettings = sharedConfiguration.GetSharedConfigurationSessionSettings();
				}
			}
			if (adsessionSettings == null)
			{
				adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId, null, false);
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, adsessionSettings, 366, "CreateScopedToSharedConfigADSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000540EC File Offset: 0x000522EC
		public static IList<RetentionPolicy> GetDefaultRetentionPolicy(IConfigurationSession scopedSession, ADRawEntry user, SortBy sortBy, int resultSize)
		{
			bool isArbitrationMailbox = false;
			if (user[OrgPersonPresentationObjectSchema.RecipientTypeDetails] != null && (RecipientTypeDetails)user[OrgPersonPresentationObjectSchema.RecipientTypeDetails] == RecipientTypeDetails.ArbitrationMailbox)
			{
				isArbitrationMailbox = true;
			}
			return SharedConfiguration.GetDefaultRetentionPolicy(scopedSession, isArbitrationMailbox, sortBy, resultSize);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0005412C File Offset: 0x0005232C
		public static IList<RetentionPolicy> GetDefaultRetentionPolicy(IConfigurationSession scopedSession, bool isArbitrationMailbox, SortBy sortBy, int resultSize)
		{
			QueryFilter filter;
			if (isArbitrationMailbox)
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, RetentionPolicySchema.IsDefaultArbitrationMailbox, true);
			}
			else
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, RetentionPolicySchema.IsDefault, true);
			}
			return scopedSession.Find<RetentionPolicy>(null, QueryScope.SubTree, filter, sortBy, resultSize);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00054170 File Offset: 0x00052370
		public static bool ExecutingUserHasRetentionPolicy(ADRawEntry executingUser, OrganizationId orgId)
		{
			if (executingUser[ADUserSchema.RetentionPolicy] != null)
			{
				return true;
			}
			if (executingUser[ADObjectSchema.OrganizationId] != null && !OrganizationId.ForestWideOrgId.Equals(executingUser[ADObjectSchema.OrganizationId]))
			{
				IConfigurationSession scopedSession = SharedConfiguration.CreateScopedToSharedConfigADSession(orgId);
				IList<RetentionPolicy> defaultRetentionPolicy = SharedConfiguration.GetDefaultRetentionPolicy(scopedSession, executingUser, null, 1);
				if (defaultRetentionPolicy != null && defaultRetentionPolicy.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0005429C File Offset: 0x0005249C
		public static SharedTenantConfigurationState GetSharedConfigurationState(OrganizationId orgId)
		{
			if (OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				return SharedTenantConfigurationState.UnSupported;
			}
			return ProvisioningCache.Instance.TryAddAndGetOrganizationData<SharedTenantConfigurationState>(CannedProvisioningCacheKeys.SharedConfigurationStateCacheKey, orgId, delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(orgId.PartitionId), 461, "GetSharedConfigurationState", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
				ExchangeConfigurationUnit[] array = tenantOrTopologyConfigurationSession.Find<ExchangeConfigurationUnit>(orgId.ConfigurationUnit, QueryScope.Base, QueryFilter.OrTogether(new QueryFilter[]
				{
					new ExistsFilter(OrganizationSchema.SupportedSharedConfigurations),
					new ExistsFilter(OrganizationSchema.SharedConfigurationInfo)
				}), null, 1);
				if (array == null || array.Length != 1)
				{
					return SharedTenantConfigurationState.NotShared;
				}
				if (null != array[0].SharedConfigurationInfo)
				{
					return SharedTenantConfigurationState.Shared;
				}
				SharedTenantConfigurationState sharedTenantConfigurationState = SharedTenantConfigurationState.UnSupported;
				if (array[0].IsStaticConfigurationShared)
				{
					sharedTenantConfigurationState |= SharedTenantConfigurationState.Static;
				}
				if (array[0].IsDehydrated)
				{
					sharedTenantConfigurationState |= SharedTenantConfigurationState.Dehydrated;
				}
				return sharedTenantConfigurationState;
			});
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000542EC File Offset: 0x000524EC
		public ADObjectId[] GetSharedRoleGroupIds(ADObjectId[] origRoleGroupIds)
		{
			if (origRoleGroupIds == null)
			{
				throw new ArgumentNullException("origRoleGroupIds");
			}
			this.LoadTinyTenantCUIfNecessary();
			return this.GetGroupMapping(origRoleGroupIds, this.tinyTenantCU, this.sharedConfigurationCU);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00054315 File Offset: 0x00052515
		public ADObjectId[] GetTinyTenantGroupIds(ADObjectId[] sharedGroupIds)
		{
			if (sharedGroupIds == null)
			{
				throw new ArgumentNullException("sharedGroupIds");
			}
			this.LoadTinyTenantCUIfNecessary();
			return this.GetGroupMapping(sharedGroupIds, this.sharedConfigurationCU, this.tinyTenantCU);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00054340 File Offset: 0x00052540
		private ADObjectId[] GetGroupMapping(ADObjectId[] sourceGroupIds, ExchangeConfigurationUnit sourceConfigUnit, ExchangeConfigurationUnit targetConfigUnit)
		{
			if (sourceGroupIds == null)
			{
				throw new ArgumentNullException("sourceGroupIds");
			}
			if (sourceConfigUnit == null)
			{
				throw new ArgumentNullException("sourceConfigUnit");
			}
			if (targetConfigUnit == null)
			{
				throw new ArgumentNullException("targetConfigUnit");
			}
			List<ADObjectId> list = new List<ADObjectId>(sourceGroupIds.Length);
			foreach (ADObjectId adobjectId in sourceGroupIds)
			{
				Guid wkGuid;
				if (sourceConfigUnit.TryGetWellKnownGuidById(adobjectId, out wkGuid))
				{
					ADObjectId item;
					if (targetConfigUnit.TryGetIdByWellKnownGuid(wkGuid, out item))
					{
						list.Add(item);
					}
					else
					{
						list.Add(adobjectId);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000543C8 File Offset: 0x000525C8
		private void LoadTinyTenantCUIfNecessary()
		{
			if (this.tinyTenantCU == null)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(this.tinyTenantId.PartitionId), 587, "LoadTinyTenantCUIfNecessary", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
				this.tinyTenantCU = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(this.tinyTenantId.ConfigurationUnit);
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00054420 File Offset: 0x00052620
		public ADObjectId GetSharedRoleAssignmentPolicy()
		{
			if (this.sharedRoleAssignmentPolicy == null)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, this.GetSharedConfigurationSessionSettings(), 602, "GetSharedRoleAssignmentPolicy", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
				RoleAssignmentPolicy[] array = tenantOrTopologyConfigurationSession.Find<RoleAssignmentPolicy>(null, QueryScope.SubTree, null, null, 1);
				this.sharedRoleAssignmentPolicy = array[0].Id;
			}
			return this.sharedRoleAssignmentPolicy;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00054478 File Offset: 0x00052678
		public ADSessionSettings GetSharedConfigurationSessionSettings()
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), this.sharedConfigurationCU.OrganizationId, null, false);
			adsessionSettings.IsSharedConfigChecked = true;
			adsessionSettings.IsRedirectedToSharedConfig = true;
			return adsessionSettings;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000544AC File Offset: 0x000526AC
		internal RbacContainer GetRbacContainer()
		{
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, this.GetSharedConfigurationSessionSettings(), 648, "GetRbacContainer", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
			return tenantConfigurationSession.Read<RbacContainer>(this.sharedConfigurationCU.Id.GetDescendantId(new ADObjectId("CN=RBAC")));
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000544FC File Offset: 0x000526FC
		internal static ExchangeConfigurationUnit FindOneSharedConfiguration(SharedConfigurationInfo sci, PartitionId partitionId)
		{
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 657, "FindOneSharedConfiguration", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SharedConfiguration.cs");
			ExchangeConfigurationUnit[] array = tenantConfigurationSession.FindSharedConfiguration(sci, true);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			Random random = new Random();
			return array[random.Next(array.Length)];
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00054558 File Offset: 0x00052758
		internal static OrganizationId FindOneSharedConfigurationId(SharedConfigurationInfo sci, PartitionId partitionId)
		{
			ExchangeConfigurationUnit exchangeConfigurationUnit = SharedConfiguration.FindOneSharedConfiguration(sci, partitionId);
			if (exchangeConfigurationUnit == null)
			{
				return null;
			}
			return exchangeConfigurationUnit.OrganizationId;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00054578 File Offset: 0x00052778
		internal static bool DoesSctExistForVersion(ServerVersion version, string programId, string offerId, PartitionId partitionId)
		{
			SharedConfigurationInfo sci = new SharedConfigurationInfo(version, programId, offerId);
			return SharedConfiguration.FindOneSharedConfigurationId(sci, partitionId) != null;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0005459C File Offset: 0x0005279C
		internal static OrganizationId FindMostRecentSharedConfigurationInPartition(OrganizationId sourceOrganizationId, PartitionId targetAccountPartitionId, out Exception ex)
		{
			ex = null;
			OrganizationId organizationId = null;
			ExchangeConfigurationUnit[] array = SharedConfiguration.LoadSharedConfigurationsSorted(sourceOrganizationId);
			if (array != null && array.Length > 0)
			{
				int num = array.Length;
				SharedConfigurationInfo sharedConfigurationInfo = array[num - 1].SharedConfigurationInfo;
				organizationId = SharedConfiguration.FindOneSharedConfigurationId(sharedConfigurationInfo, targetAccountPartitionId);
				if (organizationId == null)
				{
					ex = new InvalidOperationException(DirectoryStrings.ErrorTargetPartitionSctMissing(sourceOrganizationId.ConfigurationUnit.DistinguishedName, targetAccountPartitionId.ForestFQDN, sharedConfigurationInfo.ToString()));
				}
			}
			return organizationId;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00054608 File Offset: 0x00052808
		internal static string CreateSharedConfigurationName(string programId, string offerId)
		{
			SharedConfigurationInfo sharedConfigurationInfo = SharedConfigurationInfo.FromInstalledVersion(programId, offerId);
			string text = string.Format("{0}{1}{2}_{3}", new object[]
			{
				SharedConfiguration.SCTNamePrefix,
				"-",
				sharedConfigurationInfo.ToString().ToLower().Replace("_hydrated", null),
				Guid.NewGuid().ToString().Substring(0, 5)
			});
			if (text.Length > SharedConfiguration.SctNameMaxLength)
			{
				text = text.Substring(0, SharedConfiguration.SctNameMaxLength);
			}
			return text;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00054694 File Offset: 0x00052894
		internal static SmtpDomain CreateSharedConfigurationDomainName(string sctName)
		{
			string text = sctName;
			string text2 = string.Format(".{0}", SharedConfiguration.SCTNamePrefix);
			int num = SharedConfiguration.SctNameMaxLength - text2.Length;
			if (text.Length > num)
			{
				text = text.Substring(0, num);
			}
			text = string.Format("{0}{1}", text, text2).ToLower();
			return new SmtpDomain(text);
		}

		// Token: 0x040009E9 RID: 2537
		private static string sctNamePrefix = "sct";

		// Token: 0x040009EA RID: 2538
		private static int sctNameMaxLength = 64;

		// Token: 0x040009EB RID: 2539
		private OrganizationId tinyTenantId;

		// Token: 0x040009EC RID: 2540
		private ExchangeConfigurationUnit sharedConfigurationCU;

		// Token: 0x040009ED RID: 2541
		private ExchangeConfigurationUnit tinyTenantCU;

		// Token: 0x040009EE RID: 2542
		private ADObjectId sharedRoleAssignmentPolicy;
	}
}
