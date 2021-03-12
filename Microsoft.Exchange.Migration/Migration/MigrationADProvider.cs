using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200004B RID: 75
	internal sealed class MigrationADProvider : IMigrationADProvider
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000C097 File Offset: 0x0000A297
		public MigrationADProvider(IRecipientSession recipientSession)
		{
			this.recipientSession = recipientSession;
			if (recipientSession != null)
			{
				this.OrganizationId = recipientSession.SessionSettings.CurrentOrganizationId;
				return;
			}
			this.OrganizationId = OrganizationId.ForestWideOrgId;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C0C6 File Offset: 0x0000A2C6
		public MigrationADProvider(OrganizationId organizationId)
		{
			this.OrganizationId = organizationId;
			this.recipientSession = this.CreateRecipientSession();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C0E1 File Offset: 0x0000A2E1
		public MigrationADProvider(TenantPartitionHint partitionHint) : this(MigrationHelperBase.CreateRecipientSession(partitionHint))
		{
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000C0EF File Offset: 0x0000A2EF
		public string TenantOrganizationName
		{
			get
			{
				if (this.OrganizationId == OrganizationId.ForestWideOrgId)
				{
					return "ForestWideOrganization";
				}
				return this.OrganizationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000C119 File Offset: 0x0000A319
		public bool IsLicensingEnforced
		{
			get
			{
				return !this.IsEnterpriseOrFirstOrg && this.ExchangeConfigurationUnit.IsLicensingEnforced;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000C130 File Offset: 0x0000A330
		public bool IsSmtpAddressCheckWithAcceptedDomain
		{
			get
			{
				return !this.IsEnterpriseOrFirstOrg && this.ExchangeConfigurationUnit.SMTPAddressCheckWithAcceptedDomain;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000C168 File Offset: 0x0000A368
		public bool IsMigrationEnabled
		{
			get
			{
				if (!MigrationServiceFactory.Instance.IsMultiTenantEnabled())
				{
					return true;
				}
				TransportConfigContainer configContainer = null;
				this.DoAdCallAndTranslateExceptions(delegate
				{
					configContainer = this.GetConfigurationSession().FindSingletonConfigurationObject<TransportConfigContainer>();
				}, true);
				if (configContainer == null)
				{
					MigrationLogger.Log(MigrationEventType.Warning, "Expect to have a transport config container, but not found.  migration requires it", new object[0]);
					return false;
				}
				PerTenantTransportSettings perTenantTransportSettings = new PerTenantTransportSettings(configContainer);
				return perTenantTransportSettings.MigrationEnabled;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
		public bool IsDirSyncEnabled
		{
			get
			{
				return !this.IsEnterpriseOrFirstOrg && this.ExchangeConfigurationUnit.IsDirSyncRunning;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000C1EE File Offset: 0x0000A3EE
		public bool IsMSOSyncEnabled
		{
			get
			{
				return !this.IsEnterpriseOrFirstOrg && this.ExchangeConfigurationUnit.MSOSyncEnabled;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000C228 File Offset: 0x0000A428
		public MicrosoftExchangeRecipient PrimaryExchangeRecipient
		{
			get
			{
				MicrosoftExchangeRecipient recipient = null;
				this.DoAdCallAndTranslateExceptions(delegate
				{
					recipient = this.GetConfigurationSession().FindMicrosoftExchangeRecipient();
				}, true);
				return recipient;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000C262 File Offset: 0x0000A462
		public IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession != null)
				{
					return this.recipientSession;
				}
				return this.CreateRecipientSession();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000C279 File Offset: 0x0000A479
		private bool IsEnterpriseOrFirstOrg
		{
			get
			{
				return !MigrationServiceFactory.Instance.IsMultiTenantEnabled() || this.OrganizationId == OrganizationId.ForestWideOrgId;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		private ExchangeConfigurationUnit ExchangeConfigurationUnit
		{
			get
			{
				if (this.IsEnterpriseOrFirstOrg)
				{
					return null;
				}
				if (this.exchangeConfigurationUnit == null)
				{
					ITenantConfigurationSession tenantSession = this.GetConfigurationSession() as ITenantConfigurationSession;
					if (tenantSession != null)
					{
						this.DoAdCallAndTranslateExceptions(delegate
						{
							this.exchangeConfigurationUnit = tenantSession.GetExchangeConfigurationUnitByName(this.TenantOrganizationName);
						}, true);
					}
				}
				return this.exchangeConfigurationUnit;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000C329 File Offset: 0x0000A529
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000C331 File Offset: 0x0000A531
		private OrganizationId OrganizationId { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000C33A File Offset: 0x0000A53A
		private string DebugInfo
		{
			get
			{
				return this.TenantOrganizationName;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C342 File Offset: 0x0000A542
		public static MigrationADProvider GetRootOrgADProvider()
		{
			return new MigrationADProvider(OrganizationId.ForestWideOrgId);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C374 File Offset: 0x0000A574
		public ADRecipient GetADRecipientByObjectId(ADObjectId objectId)
		{
			MigrationUtil.ThrowOnNullArgument(objectId, "objectId");
			ADRecipient recipient = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				recipient = this.RecipientSession.Read(objectId);
			}, false);
			return recipient;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public ADRecipient GetADRecipientByExchangeObjectId(Guid exchangeObjectGuid)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(exchangeObjectGuid, "exchangeObjectGuid");
			ADRecipient recipient = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				recipient = this.RecipientSession.FindByExchangeObjectId(exchangeObjectGuid);
			}, false);
			return recipient;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C43D File Offset: 0x0000A63D
		public ADRecipient GetADRecipientByProxyAddress(string userEmail)
		{
			return this.GetADRecipientByProxyAddress<ADRecipient>(userEmail);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C468 File Offset: 0x0000A668
		public MailboxData GetMailboxDataFromLegacyDN(string userLegDN, bool forceRefresh, string userEmailAddressForDebug)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(userLegDN, "userLegDN");
			MailboxData result;
			LocalizedException ex;
			if (this.TryGetMailboxDataFromAdUser(() => this.GetADRecipientByLegDN(userLegDN, false) as ADUser, userLegDN, forceRefresh, out result, out ex))
			{
				return result;
			}
			if (ex != null)
			{
				throw ex;
			}
			throw new MigrationRecipientNotFoundException(userLegDN);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		public MailboxData GetMailboxDataFromSmtpAddress(string userEmail, bool forceRefresh, bool throwOnNotFound = true)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(userEmail, "userEmail");
			MailboxData result;
			LocalizedException ex;
			if (this.TryGetMailboxDataFromAdUser(() => this.GetADRecipientByProxyAddress<ADUser>(userEmail), userEmail, forceRefresh, out result, out ex))
			{
				return result;
			}
			if (!throwOnNotFound)
			{
				return null;
			}
			if (ex != null)
			{
				throw ex;
			}
			throw new MigrationRecipientNotFoundException(userEmail);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		public MailboxData GetPublicFolderMailboxDataFromName(string name, bool forceRefresh, bool throwOnNotFound = true)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			Func<ADUser> findUser = delegate()
			{
				QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
				{
					Filters.GetRecipientTypeDetailsFilterOptimization(RecipientTypeDetails.PublicFolderMailbox),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, name)
				});
				return this.GetRecipient<ADUser>(this.RecipientSession, filter);
			};
			MailboxData result;
			LocalizedException ex;
			if (this.TryGetMailboxDataFromAdUser(findUser, name, forceRefresh, out result, out ex))
			{
				return result;
			}
			if (!throwOnNotFound)
			{
				return null;
			}
			if (ex != null)
			{
				throw ex;
			}
			throw new MigrationRecipientNotFoundException(name);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000C61C File Offset: 0x0000A81C
		public string GetPublicFolderHierarchyMailboxName()
		{
			TenantPublicFolderConfigurationCache.Instance.RemoveValue(this.OrganizationId);
			TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(this.OrganizationId);
			PublicFolderInformation hierarchyMailboxInformation = value.GetHierarchyMailboxInformation();
			if (hierarchyMailboxInformation == null || hierarchyMailboxInformation.HierarchyMailboxGuid == Guid.Empty)
			{
				return null;
			}
			PublicFolderRecipient localMailboxRecipient = value.GetLocalMailboxRecipient(hierarchyMailboxInformation.HierarchyMailboxGuid);
			if (localMailboxRecipient == null)
			{
				return null;
			}
			return localMailboxRecipient.MailboxName;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000C680 File Offset: 0x0000A880
		public string GetDatabaseServerFqdn(Guid mdbGuid, bool forceRediscovery)
		{
			GetServerForDatabaseFlags gsfdFlags = forceRediscovery ? GetServerForDatabaseFlags.ReadThrough : GetServerForDatabaseFlags.None;
			string serverFqdn;
			try
			{
				DatabaseLocationInfo serverForDatabase = ActiveManager.GetCachingActiveManagerInstance().GetServerForDatabase(mdbGuid, gsfdFlags);
				if (serverForDatabase == null)
				{
					throw new MigrationMailboxDatabaseInfoNotAvailableException(mdbGuid.ToString());
				}
				serverFqdn = serverForDatabase.ServerFqdn;
			}
			catch (ObjectNotFoundException ex)
			{
				string text = string.Format("Server for mailbox with Guid {0} not found in ActiveManager using forceDiscovery set to {1}", mdbGuid, forceRediscovery);
				MigrationLogger.Log(MigrationEventType.Error, ex, text, new object[0]);
				throw new AnchorDatabaseNotFoundTransientException(mdbGuid.ToString(), ex)
				{
					InternalError = text
				};
			}
			catch (StoragePermanentException exception)
			{
				MigrationLogger.Log(MigrationEventType.Error, exception, "Server for mailbox with Guid {0} not found in ActiveManager using forceDiscovery set to {1}", new object[]
				{
					mdbGuid,
					forceRediscovery
				});
				throw;
			}
			catch (StorageTransientException exception2)
			{
				MigrationLogger.Log(MigrationEventType.Error, exception2, "Server for mailbox with Guid {0} not found in ActiveManager using forceDiscovery set to {1}", new object[]
				{
					mdbGuid,
					forceRediscovery
				});
				throw;
			}
			catch (ServerForDatabaseNotFoundException ex2)
			{
				string text2 = string.Format("Server for mailbox with Guid {0} not found in ActiveManager using forceDiscovery set to {1}", mdbGuid, forceRediscovery);
				MigrationLogger.Log(MigrationEventType.Error, ex2, text2, new object[0]);
				throw new AnchorServerNotFoundTransientException(mdbGuid.ToString(), ex2)
				{
					InternalError = text2
				};
			}
			return serverFqdn;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		public string GetPreferredDomainController()
		{
			return this.RecipientSession.DomainController ?? this.RecipientSession.LastUsedDc;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000C808 File Offset: 0x0000AA08
		public ExchangePrincipal GetExchangePrincipalFromMbxGuid(Guid mailboxGuid)
		{
			return ExchangePrincipal.FromMailboxGuid(this.RecipientSession.SessionSettings, mailboxGuid, RemotingOptions.AllowCrossSite, null);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000C994 File Offset: 0x0000AB94
		public void UpdateMigrationUpgradeConstraint(UpgradeConstraint constraint)
		{
			MigrationUtil.ThrowOnNullArgument(constraint, "constraint");
			this.DoAdCallAndTranslateExceptions(delegate
			{
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(false, ConsistencyMode.PartiallyConsistent, this.RecipientSession.SessionSettings, 610, "UpdateMigrationUpgradeConstraint", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationADProvider.cs");
				ExchangeConfigurationUnit exchangeConfigurationUnitByName = tenantConfigurationSession.GetExchangeConfigurationUnitByName(this.TenantOrganizationName);
				ExAssert.RetailAssert(exchangeConfigurationUnitByName != null, "Organization must always exist.");
				if (exchangeConfigurationUnitByName.UpgradeConstraints == null || exchangeConfigurationUnitByName.UpgradeConstraints.UpgradeConstraints == null)
				{
					exchangeConfigurationUnitByName.UpgradeConstraints = new UpgradeConstraintArray(new UpgradeConstraint[]
					{
						constraint
					});
				}
				else
				{
					IComparable<UpgradeConstraint> comparableConstraint = constraint;
					if (exchangeConfigurationUnitByName.UpgradeConstraints.UpgradeConstraints.Any((UpgradeConstraint existingConstraint) => comparableConstraint.CompareTo(existingConstraint) == 0))
					{
						return;
					}
					IEnumerable<UpgradeConstraint> first = from c in exchangeConfigurationUnitByName.UpgradeConstraints.UpgradeConstraints
					where !c.Name.Equals(constraint.Name)
					select c;
					exchangeConfigurationUnitByName.UpgradeConstraints = new UpgradeConstraintArray(first.Union(new UpgradeConstraint[]
					{
						constraint
					}).ToArray<UpgradeConstraint>());
				}
				MigrationLogger.Log(MigrationEventType.Verbose, "Saving organization constraints", new object[0]);
				tenantConfigurationSession.Save(exchangeConfigurationUnitByName);
			}, false);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		public void RemovePublicFolderMigrationLock()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId), 651, "RemovePublicFolderMigrationLock", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationADProvider.cs");
			Organization orgContainer = tenantOrTopologyConfigurationSession.GetOrgContainer();
			orgContainer.DefaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox.Clone();
			orgContainer.DefaultPublicFolderMailbox.SetHierarchyMailbox(orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid, PublicFolderInformation.HierarchyType.MailboxGuid);
			tenantOrTopologyConfigurationSession.Save(orgContainer);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public bool CheckPublicFoldersLockedForMigration()
		{
			Organization orgContainer = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId), 672, "CheckPublicFoldersLockedForMigration", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationADProvider.cs").GetOrgContainer();
			return orgContainer.DefaultPublicFolderMailbox.Type == PublicFolderInformation.HierarchyType.InTransitMailboxGuid;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		internal static Uri GetEcpUrl(IExchangePrincipal owner)
		{
			try
			{
				Uri frontEndEcpUrl = FrontEndLocator.GetFrontEndEcpUrl(owner);
				if (frontEndEcpUrl != null)
				{
					return frontEndEcpUrl;
				}
			}
			catch (ServerNotFoundException)
			{
			}
			return BackEndLocator.GetBackEndEcpUrl(owner.MailboxInfo);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		internal static OrganizationId GetOrganization(string tenantName)
		{
			return ADSessionSettings.FromTenantCUName(tenantName).CurrentOrganizationId;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CB04 File Offset: 0x0000AD04
		internal static OrganizationId GetOrganization(ADObjectId organizationADObjectId)
		{
			if (!MigrationServiceFactory.Instance.IsMultiTenantEnabled() || ADObjectId.IsNullOrEmpty(organizationADObjectId))
			{
				return OrganizationId.ForestWideOrgId;
			}
			OrganizationId organizationId;
			if (MigrationADProvider.CachedOrganizations.TryGetValue(organizationADObjectId, out organizationId))
			{
				return organizationId;
			}
			ADOrganizationalUnit[] organizations = null;
			ITenantConfigurationSession tenantSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsObjectId(organizationADObjectId), 733, "GetOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationADProvider.cs");
			MigrationHelperBase.DoAdCallAndTranslateExceptions(delegate
			{
				organizations = tenantSession.Find<ADOrganizationalUnit>(organizationADObjectId, QueryScope.Base, null, null, 1);
			}, true, organizationADObjectId.ToString());
			if (organizations == null || organizations.Length <= 0)
			{
				throw new MigrationObjectNotFoundInADException(organizationADObjectId.ToString(), tenantSession.Source);
			}
			organizationId = organizations[0].OrganizationId;
			MigrationADProvider.CachedOrganizations[organizationADObjectId] = organizationId;
			return organizationId;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		internal ExchangePrincipal GetLocalSystemMailboxOwner(Guid mdbGuid)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MigrationADProvider.GetSystemMailboxName(mdbGuid));
			ADSystemMailbox recipient = this.GetRecipient<ADSystemMailbox>(this.RecipientSession, filter);
			return ExchangePrincipal.FromADSystemMailbox(this.RecipientSession.SessionSettings, recipient, LocalServer.GetServer());
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000CC44 File Offset: 0x0000AE44
		internal ExchangePrincipal GetLocalMigrationMailboxOwner(string migrationMailboxLegacyDn)
		{
			MigrationUtil.ThrowOnNullArgument(migrationMailboxLegacyDn, "migrationMailboxLegacyDn");
			ADUser aduser = this.GetADRecipientByLegDN(migrationMailboxLegacyDn, true) as ADUser;
			if (aduser == null)
			{
				throw new MigrationObjectNotFoundInADException(migrationMailboxLegacyDn, this.RecipientSession.Source);
			}
			return ExchangePrincipal.FromADUser(this.RecipientSession.SessionSettings, aduser, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		internal MailboxData GetMailboxDataForManagementMailbox()
		{
			MailboxData result;
			LocalizedException ex;
			if (this.TryGetMailboxDataFromAdUser(() => this.GetRecipient<ADUser>(this.RecipientSession, MigrationHelperBase.ManagementMailboxFilter), "Management mailbox", true, out result, out ex))
			{
				return result;
			}
			if (ex != null)
			{
				throw ex;
			}
			throw new MigrationRecipientNotFoundException("Management mailbox");
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		internal void EnsureLocalMailbox(IExchangePrincipal mailboxOwner, bool forceRefresh)
		{
			MigrationUtil.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
			if (mailboxOwner.MailboxInfo.Location == null)
			{
				forceRefresh = true;
			}
			string text;
			if (!forceRefresh)
			{
				text = mailboxOwner.MailboxInfo.Location.ServerFqdn;
			}
			else
			{
				text = this.GetDatabaseServerFqdn(mailboxOwner.MailboxInfo.GetDatabaseGuid(), true);
			}
			if (!string.Equals(text, MigrationServiceFactory.Instance.GetLocalServerFqdn(), StringComparison.OrdinalIgnoreCase))
			{
				throw new MigrationMailboxNotFoundOnServerException(text, MigrationServiceFactory.Instance.GetLocalServerFqdn(), mailboxOwner.LegacyDn);
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000CD5B File Offset: 0x0000AF5B
		private static string GetSystemMailboxName(Guid mdbGuid)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			return "SystemMailbox{" + mdbGuid.ToString() + "}";
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000CD84 File Offset: 0x0000AF84
		private IRecipientSession CreateRecipientSession()
		{
			if (OrganizationId.ForestWideOrgId == this.OrganizationId)
			{
				return DirectorySessionFactory.Default.CreateRootOrgRecipientSession(null, null, LcidMapper.DefaultLcid, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromRootOrgScopeSet(), 894, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationADProvider.cs");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, LcidMapper.DefaultLcid, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 907, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationADProvider.cs");
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		private IConfigurationSession GetConfigurationSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, this.RecipientSession.SessionSettings, 923, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Migration\\DataAccessLayer\\MigrationADProvider.cs");
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000CE23 File Offset: 0x0000B023
		private void DoAdCallAndTranslateExceptions(ADOperation call, bool expectObject)
		{
			MigrationHelperBase.DoAdCallAndTranslateExceptions(call, expectObject, this.DebugInfo);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000CE58 File Offset: 0x0000B058
		private TResult GetADRecipientByProxyAddress<TResult>(string userEmail) where TResult : ADObject, new()
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(userEmail, "userEmail");
			ProxyAddress proxy = ProxyAddress.Parse(userEmail);
			TResult recipient = default(TResult);
			this.DoAdCallAndTranslateExceptions(delegate
			{
				recipient = this.RecipientSession.FindByProxyAddress<TResult>(proxy);
			}, false);
			return recipient;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000CEC3 File Offset: 0x0000B0C3
		private T GetRecipient<T>(IRecipientSession session, QueryFilter filter) where T : class
		{
			return MigrationHelperBase.GetRecipient<T>(session, filter, (string msg) => new MigrationMailboxNotFoundException(), (string msg) => new MultipleMigrationMailboxesFoundException(), (string msg) => new MigrationMailboxNotFoundException(), this.DebugInfo);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000CF1C File Offset: 0x0000B11C
		private ADRecipient GetADRecipientByLegDN(string legDN, bool expectObject = false)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(legDN, "legDN");
			ADRecipient recipient = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				recipient = this.RecipientSession.FindByLegacyExchangeDN(legDN);
			}, expectObject);
			return recipient;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000CF70 File Offset: 0x0000B170
		private bool TryGetMailboxDataFromAdUser(Func<ADUser> findUser, string userIdentifier, bool refreshMailboxData, out MailboxData result, out LocalizedException error)
		{
			result = null;
			try
			{
				ADUser aduser = findUser();
				if (aduser == null)
				{
					error = new MigrationRecipientNotFoundException(userIdentifier);
					return false;
				}
				switch (aduser.RecipientType)
				{
				case RecipientType.UserMailbox:
				{
					Guid objectGuid = aduser.Database.ObjectGuid;
					string databaseServerFqdn = this.GetDatabaseServerFqdn(objectGuid, refreshMailboxData);
					result = new MailboxData(aduser.ExchangeGuid, objectGuid, new Fqdn(databaseServerFqdn), aduser.LegacyExchangeDN, aduser.Id, aduser.ExchangeObjectId);
					break;
				}
				case RecipientType.MailUser:
					if (aduser.ExchangeGuid == Guid.Empty)
					{
						error = new MissingExchangeGuidException(userIdentifier);
						return false;
					}
					result = new MailboxData(aduser.ExchangeGuid, aduser.LegacyExchangeDN, aduser.Id, aduser.ExchangeObjectId);
					break;
				default:
					MigrationLogger.Log(MigrationEventType.Verbose, "Attempted to get a MailboxData for an unknown recipient type: {0}:{1}", new object[]
					{
						aduser.Guid,
						aduser.RecipientType
					});
					break;
				}
				error = null;
			}
			catch (LocalizedException ex)
			{
				error = ex;
				MigrationLogger.Log(MigrationEventType.Information, ex, "Could not find a valid user for '{0}'.", new object[]
				{
					userIdentifier
				});
			}
			if (result != null)
			{
				result.Update(userIdentifier, this.OrganizationId);
			}
			return result != null;
		}

		// Token: 0x040000F4 RID: 244
		private static readonly MruDictionaryCache<ADObjectId, OrganizationId> CachedOrganizations = new MruDictionaryCache<ADObjectId, OrganizationId>(8, 1000, 480);

		// Token: 0x040000F5 RID: 245
		private readonly IRecipientSession recipientSession;

		// Token: 0x040000F6 RID: 246
		private ExchangeConfigurationUnit exchangeConfigurationUnit;
	}
}
