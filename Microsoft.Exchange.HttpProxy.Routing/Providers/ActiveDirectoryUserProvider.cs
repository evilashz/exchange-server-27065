using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.HttpProxy.Routing.Providers
{
	// Token: 0x0200000D RID: 13
	internal class ActiveDirectoryUserProvider : IUserProvider
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000022C9 File Offset: 0x000004C9
		public ActiveDirectoryUserProvider(bool useActiveDirectoryCache)
		{
			if (useActiveDirectoryCache)
			{
				this.directorySessionFactoryInstance = DirectorySessionFactory.GetInstance(DirectorySessionFactoryType.Cached);
				return;
			}
			this.directorySessionFactoryInstance = DirectorySessionFactory.GetInstance(DirectorySessionFactoryType.Default);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000234C File Offset: 0x0000054C
		public User FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, string tenantDomain, IRoutingDiagnostics diagnostics)
		{
			return this.Execute<User>(delegate
			{
				ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(tenantDomain);
				IRecipientSession tenantOrRootOrgRecipientSession = this.directorySessionFactoryInstance.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 64, "FindByExchangeGuidIncludingAlternate", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\Routing\\Providers\\ActiveDirectoryUserProvider.cs");
				ADRawEntry rawEntry = ActiveDirectoryUserProvider.FindByExchangeGuidIncludingAlternate(exchangeGuid, tenantOrRootOrgRecipientSession, diagnostics);
				return ActiveDirectoryUserProvider.CreateUserFromAdRawEntry(rawEntry);
			}, "FindByExchangeGuidIncludingAlternate failed");
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002408 File Offset: 0x00000608
		public User FindBySmtpAddress(SmtpAddress smtpAddress, IRoutingDiagnostics diagnostics)
		{
			return this.Execute<User>(delegate
			{
				SmtpProxyAddress proxyAddress = new SmtpProxyAddress(smtpAddress.ToString(), true);
				ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(smtpAddress.Domain);
				IRecipientSession tenantOrRootOrgRecipientSession = this.directorySessionFactoryInstance.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 88, "FindBySmtpAddress", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\Routing\\Providers\\ActiveDirectoryUserProvider.cs");
				ADRawEntry rawEntry = ActiveDirectoryUserProvider.FindByProxyAddress(proxyAddress, tenantOrRootOrgRecipientSession, diagnostics);
				return ActiveDirectoryUserProvider.CreateUserFromAdRawEntry(rawEntry);
			}, "FindBySmtpAddress failed");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024A8 File Offset: 0x000006A8
		public User FindByExternalDirectoryObjectId(Guid userGuid, Guid tenantGuid, IRoutingDiagnostics diagnostics)
		{
			return this.Execute<User>(delegate
			{
				ADSessionSettings sessionSettings = ActiveDirectoryUserProvider.FromExternalDirectoryOrganizationId(tenantGuid, diagnostics);
				IRecipientSession tenantOrRootOrgRecipientSession = this.directorySessionFactoryInstance.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 111, "FindByExternalDirectoryObjectId", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\Routing\\Providers\\ActiveDirectoryUserProvider.cs");
				ADRawEntry rawEntry = ActiveDirectoryUserProvider.FindAdUserByExternalDirectoryObjectId(userGuid, tenantOrRootOrgRecipientSession, diagnostics);
				return ActiveDirectoryUserProvider.CreateUserFromAdRawEntry(rawEntry);
			}, "FindByExternalDirectoryObjectId failed");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002564 File Offset: 0x00000764
		public User FindByLiveIdMemberName(SmtpAddress liveIdMemberName, string organizationContext, IRoutingDiagnostics diagnostics)
		{
			return this.Execute<User>(delegate
			{
				string text = organizationContext;
				if (string.IsNullOrEmpty(text))
				{
					text = liveIdMemberName.Domain;
				}
				ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(text);
				ITenantRecipientSession recipientSession = this.directorySessionFactoryInstance.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 140, "FindByLiveIdMemberName", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\Routing\\Providers\\ActiveDirectoryUserProvider.cs");
				ADRawEntry rawEntry = ActiveDirectoryUserProvider.FindByLiveIdMemberName(liveIdMemberName, recipientSession, diagnostics);
				return ActiveDirectoryUserProvider.CreateUserFromAdRawEntry(rawEntry);
			}, "FindByLiveIdMemberName failed");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025AA File Offset: 0x000007AA
		public string FindResourceForestFqdnByAcceptedDomainName(string acceptedDomain, IRoutingDiagnostics diagnostics)
		{
			return ADAccountPartitionLocator.GetResourceForestFqdnByAcceptedDomainName(acceptedDomain);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025B2 File Offset: 0x000007B2
		public string FindResourceForestFqdnByExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId, IRoutingDiagnostics diagnostics)
		{
			return ADAccountPartitionLocator.GetResourceForestFqdnByExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025BC File Offset: 0x000007BC
		private static User CreateUserFromAdRawEntry(ADRawEntry rawEntry)
		{
			if (rawEntry == null)
			{
				return null;
			}
			Guid? databaseGuid = null;
			string databaseResourceForest = null;
			Guid? archiveDatabaseGuid = null;
			string archiveDatabaseResourceForest = null;
			ADObjectId adobjectId = rawEntry[ADMailboxRecipientSchema.Database] as ADObjectId;
			if (adobjectId != null)
			{
				databaseGuid = new Guid?(adobjectId.ObjectGuid);
				databaseResourceForest = adobjectId.PartitionFQDN;
			}
			ADObjectId adobjectId2 = rawEntry[ADUserSchema.ArchiveDatabase] as ADObjectId;
			if (adobjectId2 != null)
			{
				archiveDatabaseGuid = new Guid?(adobjectId2.ObjectGuid);
				archiveDatabaseResourceForest = adobjectId2.PartitionFQDN;
			}
			return new User
			{
				ArchiveDatabaseGuid = archiveDatabaseGuid,
				ArchiveDatabaseResourceForest = archiveDatabaseResourceForest,
				ArchiveGuid = (rawEntry[ADUserSchema.ArchiveGuid] as Guid?),
				DatabaseGuid = databaseGuid,
				DatabaseResourceForest = databaseResourceForest,
				LastModifiedTime = (rawEntry[ADObjectSchema.WhenChangedUTC] as DateTime?)
			};
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000269C File Offset: 0x0000089C
		private static ADSessionSettings FromExternalDirectoryOrganizationId(Guid tenantGuid, IRoutingDiagnostics diagnostics)
		{
			DateTime utcNow = DateTime.UtcNow;
			ADSessionSettings result;
			try
			{
				result = ADSessionSettings.FromExternalDirectoryOrganizationId(tenantGuid);
			}
			finally
			{
				diagnostics.AddGlobalLocatorLatency(DateTime.UtcNow - utcNow);
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026DC File Offset: 0x000008DC
		private static ADRawEntry FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IRecipientSession recipientSession, IRoutingDiagnostics diagnostics)
		{
			DateTime utcNow = DateTime.UtcNow;
			ADRawEntry result;
			try
			{
				result = recipientSession.FindByExchangeGuidIncludingAlternate(exchangeGuid, ActiveDirectoryUserProvider.AdRawEntryProperties);
			}
			finally
			{
				diagnostics.AddAccountForestLatency(DateTime.UtcNow - utcNow);
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002724 File Offset: 0x00000924
		private static ADRawEntry FindByProxyAddress(ProxyAddress proxyAddress, IRecipientSession recipientSession, IRoutingDiagnostics diagnostics)
		{
			DateTime utcNow = DateTime.UtcNow;
			ADRawEntry result;
			try
			{
				result = recipientSession.FindByProxyAddress(proxyAddress, ActiveDirectoryUserProvider.AdRawEntryProperties);
			}
			finally
			{
				diagnostics.AddAccountForestLatency(DateTime.UtcNow - utcNow);
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000276C File Offset: 0x0000096C
		private static ADRawEntry FindByLiveIdMemberName(SmtpAddress liveIdMemberName, ITenantRecipientSession recipientSession, IRoutingDiagnostics diagnostics)
		{
			DateTime utcNow = DateTime.UtcNow;
			ADRawEntry result;
			try
			{
				result = recipientSession.FindByLiveIdMemberName(liveIdMemberName.ToString(), ActiveDirectoryUserProvider.AdRawEntryProperties);
			}
			finally
			{
				diagnostics.AddAccountForestLatency(DateTime.UtcNow - utcNow);
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027C0 File Offset: 0x000009C0
		private static ADRawEntry FindAdUserByExternalDirectoryObjectId(Guid userGuid, IRecipientSession recipientSession, IRoutingDiagnostics diagnostics)
		{
			DateTime utcNow = DateTime.UtcNow;
			ADRawEntry result;
			try
			{
				result = recipientSession.FindADUserByExternalDirectoryObjectId(userGuid.ToString());
			}
			finally
			{
				diagnostics.AddAccountForestLatency(DateTime.UtcNow - utcNow);
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000280C File Offset: 0x00000A0C
		private TResult Execute<TResult>(Func<TResult> func, string exceptionMessage)
		{
			TResult result;
			try
			{
				result = func();
			}
			catch (DataValidationException innerException)
			{
				throw new UserProviderException(exceptionMessage, innerException);
			}
			catch (ADTransientException innerException2)
			{
				throw new UserProviderException(exceptionMessage, innerException2);
			}
			catch (ADOperationException innerException3)
			{
				throw new UserProviderException(exceptionMessage, innerException3);
			}
			return result;
		}

		// Token: 0x04000007 RID: 7
		private static readonly PropertyDefinition[] AdRawEntryProperties = new PropertyDefinition[]
		{
			ADUserSchema.ArchiveDatabase,
			ADUserSchema.ArchiveGuid,
			ADMailboxRecipientSchema.Database
		};

		// Token: 0x04000008 RID: 8
		private readonly DirectorySessionFactory directorySessionFactoryInstance;
	}
}
