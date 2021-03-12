using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorADProvider : IAnchorADProvider
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000227C File Offset: 0x0000047C
		public AnchorADProvider(AnchorContext context, OrganizationId organizationId, string preferredDomainController = null)
		{
			AnchorUtil.ThrowOnNullArgument(organizationId, "organizationId");
			this.Context = context;
			this.OrganizationId = organizationId;
			this.preferredDomainController = preferredDomainController;
			this.RecipientSession = this.CreateRecipientSession();
			this.lazyConfigurationSession = new Lazy<ITopologyConfigurationSession>(new Func<ITopologyConfigurationSession>(AnchorADProvider.CreateTopologyConfigurationSession));
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022D2 File Offset: 0x000004D2
		public string TenantOrganizationName
		{
			get
			{
				if (OrganizationId.ForestWideOrgId == this.OrganizationId)
				{
					return "ForestWideOrganization";
				}
				return this.OrganizationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000231C File Offset: 0x0000051C
		public MicrosoftExchangeRecipient PrimaryExchangeRecipient
		{
			get
			{
				MicrosoftExchangeRecipient recipient = null;
				this.DoAdCallAndTranslateExceptions(delegate
				{
					recipient = this.ConfigurationSession.FindMicrosoftExchangeRecipient();
				}, true);
				return recipient;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002356 File Offset: 0x00000556
		public ITopologyConfigurationSession ConfigurationSession
		{
			get
			{
				return this.lazyConfigurationSession.Value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002363 File Offset: 0x00000563
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000236B File Offset: 0x0000056B
		private protected OrganizationId OrganizationId { protected get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002374 File Offset: 0x00000574
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000237C File Offset: 0x0000057C
		private AnchorContext Context { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002385 File Offset: 0x00000585
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000238D File Offset: 0x0000058D
		private IRecipientSession RecipientSession { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002396 File Offset: 0x00000596
		private string DebugInfo
		{
			get
			{
				return this.TenantOrganizationName;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000239E File Offset: 0x0000059E
		public static AnchorADProvider GetRootOrgProvider(AnchorContext context)
		{
			return new AnchorADProvider(context, OrganizationId.ForestWideOrgId, null);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000023D4 File Offset: 0x000005D4
		public ADRecipient GetADRecipientByObjectId(ADObjectId objectId)
		{
			AnchorUtil.ThrowOnNullArgument(objectId, "objectId");
			ADRecipient recipient = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				recipient = this.RecipientSession.Read(objectId);
			}, false);
			return recipient;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002425 File Offset: 0x00000625
		public IEnumerable<ADUser> GetOrganizationMailboxesByCapability(OrganizationCapability capability)
		{
			return OrganizationMailbox.GetOrganizationMailboxesByCapability(this.RecipientSession, capability);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002433 File Offset: 0x00000633
		public ADPagedReader<TEntry> FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties) where TEntry : MiniRecipient, new()
		{
			return this.RecipientSession.FindPagedMiniRecipient<TEntry>(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002449 File Offset: 0x00000649
		public ADPagedReader<ADRawEntry> FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			return this.RecipientSession.FindPagedADRawEntry(rootId, scope, filter, sortBy, pageSize, properties);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002498 File Offset: 0x00000698
		public void AddCapability(ADObjectId objectId, OrganizationCapability capability)
		{
			AnchorUtil.ThrowOnNullArgument(objectId, "objectId");
			ADUser user = this.GetAnchorADUser(objectId);
			this.DoAdCallAndTranslateExceptions(delegate
			{
				user.PersistedCapabilities.Add((Capability)capability);
				this.RecipientSession.Save(user);
			}, true);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000251C File Offset: 0x0000071C
		public void RemoveCapability(ADObjectId objectId, OrganizationCapability capability)
		{
			AnchorUtil.ThrowOnNullArgument(objectId, "objectId");
			ADUser user = this.GetAnchorADUser(objectId);
			this.DoAdCallAndTranslateExceptions(delegate
			{
				if (user.PersistedCapabilities.Remove((Capability)capability))
				{
					this.RecipientSession.Save(user);
				}
			}, true);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002568 File Offset: 0x00000768
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
				this.Context.Logger.Log(MigrationEventType.Error, ex, text, new object[0]);
				throw new AnchorDatabaseNotFoundTransientException(mdbGuid.ToString(), ex)
				{
					InternalError = text
				};
			}
			catch (StoragePermanentException exception)
			{
				this.Context.Logger.Log(MigrationEventType.Error, exception, "Server for mailbox with Guid {0} not found in ActiveManager using forceDiscovery set to {1}", new object[]
				{
					mdbGuid,
					forceRediscovery
				});
				throw;
			}
			catch (StorageTransientException exception2)
			{
				this.Context.Logger.Log(MigrationEventType.Error, exception2, "Server for mailbox with Guid {0} not found in ActiveManager using forceDiscovery set to {1}", new object[]
				{
					mdbGuid,
					forceRediscovery
				});
				throw;
			}
			catch (ServerForDatabaseNotFoundException ex2)
			{
				string text2 = string.Format("Server for mailbox with Guid {0} not found in ActiveManager using forceDiscovery set to {1}", mdbGuid, forceRediscovery);
				this.Context.Logger.Log(MigrationEventType.Error, ex2, text2, new object[0]);
				throw new AnchorServerNotFoundTransientException(mdbGuid.ToString(), ex2)
				{
					InternalError = text2
				};
			}
			return serverFqdn;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002700 File Offset: 0x00000900
		public string GetPreferredDomainController()
		{
			return this.RecipientSession.DomainController ?? this.RecipientSession.LastUsedDc;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002744 File Offset: 0x00000944
		public ADRecipient GetADRecipientByProxyAddress(string userEmail)
		{
			AnchorUtil.ThrowOnNullOrEmptyArgument(userEmail, "userEmail");
			ProxyAddress proxy = ProxyAddress.Parse(userEmail);
			ADRecipient recipient = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				recipient = this.RecipientSession.FindByProxyAddress<ADRecipient>(proxy);
			}, false);
			return recipient;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002795 File Offset: 0x00000995
		public string GetMailboxServerFqdn(ADUser user, bool forceRefresh)
		{
			return this.GetDatabaseServerFqdn(user.Database.ObjectGuid, forceRefresh);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027AC File Offset: 0x000009AC
		public void EnsureLocalMailbox(ADUser user, bool forceRefresh)
		{
			AnchorUtil.ThrowOnNullArgument(user, "user");
			string mailboxServerFqdn = this.GetMailboxServerFqdn(user, forceRefresh);
			if (!string.Equals(mailboxServerFqdn, LocalServer.GetServer().Fqdn, StringComparison.OrdinalIgnoreCase))
			{
				throw new AnchorMailboxNotFoundOnServerException(mailboxServerFqdn, LocalServer.GetServer().Fqdn, user.DistinguishedName);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027F8 File Offset: 0x000009F8
		internal ExchangePrincipal GetMailboxOwner(QueryFilter filter)
		{
			ADUser recipient = this.GetRecipient<ADUser>(this.RecipientSession, filter);
			return ExchangePrincipal.FromADUser(this.RecipientSession.SessionSettings, recipient, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002825 File Offset: 0x00000A25
		private static ITopologyConfigurationSession CreateTopologyConfigurationSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 409, "CreateTopologyConfigurationSession", "f:\\15.00.1497\\sources\\dev\\assistants\\src\\AnchorService\\Common\\AnchorADProvider.cs");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002848 File Offset: 0x00000A48
		private string GetMailboxServerFqdn(ExchangePrincipal mailboxOwner, bool forceRefresh)
		{
			AnchorUtil.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
			if (mailboxOwner.MailboxInfo.Location == null)
			{
				forceRefresh = true;
			}
			if (!forceRefresh)
			{
				return mailboxOwner.MailboxInfo.Location.ServerFqdn;
			}
			return this.GetDatabaseServerFqdn(mailboxOwner.MailboxInfo.GetDatabaseGuid(), true);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002898 File Offset: 0x00000A98
		private ADUser GetAnchorADUser(ADObjectId objectId)
		{
			AnchorUtil.ThrowOnNullArgument(objectId, "objectId");
			ADUser aduser = this.GetADRecipientByObjectId(objectId) as ADUser;
			if (aduser == null)
			{
				throw new AnchorMailboxNotFoundException();
			}
			return aduser;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028C8 File Offset: 0x00000AC8
		private IRecipientSession CreateRecipientSession()
		{
			if (OrganizationId.ForestWideOrgId == this.OrganizationId)
			{
				return DirectorySessionFactory.Default.CreateRootOrgRecipientSession(null, null, LcidMapper.DefaultLcid, false, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromRootOrgScopeSet(), 472, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\assistants\\src\\AnchorService\\Common\\AnchorADProvider.cs");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.preferredDomainController, null, LcidMapper.DefaultLcid, false, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 485, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\assistants\\src\\AnchorService\\Common\\AnchorADProvider.cs");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000295A File Offset: 0x00000B5A
		private T GetRecipient<T>(IRecipientSession session, QueryFilter filter) where T : class
		{
			return MigrationHelperBase.GetRecipient<T>(session, filter, (string msg) => new AnchorMailboxNotFoundException(), (string msg) => new MultipleAnchorMailboxesFoundException(), (string msg) => new AnchorMailboxNotFoundException(), this.DebugInfo);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000298D File Offset: 0x00000B8D
		private void DoAdCallAndTranslateExceptions(ADOperation call, bool expectObject)
		{
			MigrationHelperBase.DoAdCallAndTranslateExceptions(call, expectObject, this.DebugInfo);
		}

		// Token: 0x04000007 RID: 7
		internal static readonly bool IsDataCenter = Datacenter.IsRunningInExchangeDatacenter(false);

		// Token: 0x04000008 RID: 8
		private readonly string preferredDomainController;

		// Token: 0x04000009 RID: 9
		private readonly Lazy<ITopologyConfigurationSession> lazyConfigurationSession;
	}
}
