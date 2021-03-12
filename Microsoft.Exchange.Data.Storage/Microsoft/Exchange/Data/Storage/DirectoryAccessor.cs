using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002EF RID: 751
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DirectoryAccessor : IDirectoryAccessor, IADUserFinder
	{
		// Token: 0x06002147 RID: 8519 RVA: 0x00087C28 File Offset: 0x00085E28
		public IGenericADUser FindBySid(IRecipientSession recipientSession, SecurityIdentifier sid)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			IGenericADUser adUser = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				adUser = this.TranslateADRecipient(recipientSession.FindBySid(sid), false);
			}, "DirectoryAccessor::FindBySid");
			return adUser;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00087CB4 File Offset: 0x00085EB4
		public IGenericADUser FindByProxyAddress(IRecipientSession recipientSession, ProxyAddress proxyAddress)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			IGenericADUser adUser = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				adUser = this.TranslateADRecipient(recipientSession.FindByProxyAddress(proxyAddress), false);
			}, "DirectoryAccessor::FindByProxyAddress");
			return adUser;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00087D44 File Offset: 0x00085F44
		public IGenericADUser FindByExchangeGuid(IRecipientSession recipientSession, Guid mailboxGuid, bool includeSystemMailbox)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			IGenericADUser adUser = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				adUser = this.TranslateADRecipient(recipientSession.FindByExchangeGuidIncludingAlternate(mailboxGuid), includeSystemMailbox);
			}, "DirectoryAccessor::FindByExchangeGuid");
			return adUser;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00087DD4 File Offset: 0x00085FD4
		public IGenericADUser FindByObjectId(IRecipientSession recipientSession, ADObjectId directoryEntry)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			IGenericADUser adUser = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				adUser = this.TranslateADRecipient(recipientSession.Read(directoryEntry), false);
			}, "DirectoryAccessor::FindByObjectId");
			return adUser;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00087E60 File Offset: 0x00086060
		public IGenericADUser FindByLegacyExchangeDn(IRecipientSession recipientSession, string legacyExchangeDn)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			IGenericADUser adUser = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				adUser = this.TranslateADRecipient(recipientSession.FindByLegacyExchangeDN(legacyExchangeDn), false);
			}, "DirectoryAccessor::FindByLegacyExchangeDn");
			return adUser;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00087EE4 File Offset: 0x000860E4
		public IGenericADUser FindMiniRecipientByProxyAddress(IRecipientSession recipientSession, ProxyAddress proxyAddress, PropertyDefinition[] miniRecipientProperties, out StorageMiniRecipient miniRecipient)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			StorageMiniRecipient localMiniRecipient = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				localMiniRecipient = recipientSession.FindMiniRecipientByProxyAddress<StorageMiniRecipient>(proxyAddress, miniRecipientProperties);
			}, "DirectoryAccessor::FindMiniRecipientByProxyAddress");
			miniRecipient = localMiniRecipient;
			return this.TranslateMiniRecipient(miniRecipient);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00087F6C File Offset: 0x0008616C
		public SmtpAddress GetOrganizationFederatedMailboxIdentity(IConfigurationSession configurationSession)
		{
			ArgumentValidator.ThrowIfNull("configurationSession", configurationSession);
			SmtpAddress organizationFederatedMailboxIdentity = default(SmtpAddress);
			this.DoAdCallAndTranslateExceptions(delegate
			{
				organizationFederatedMailboxIdentity = configurationSession.FindSingletonConfigurationObject<TransportConfigContainer>().OrganizationFederatedMailbox;
			}, "DirectoryAccessor::GetOrganizationFederatedMailboxIdentity");
			return organizationFederatedMailboxIdentity;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00087FE0 File Offset: 0x000861E0
		public bool TryGetOrganizationContentConversionProperties(OrganizationId organizationId, out OrganizationContentConversionProperties organizationContentConversionProperties)
		{
			bool tryResult = false;
			OrganizationContentConversionProperties localOrganizationContentConversionProperties = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				tryResult = OrganizationContentConversionCache.TryGetOrganizationContentConversionProperties(organizationId, out localOrganizationContentConversionProperties);
			}, "DirectoryAccessor::TryGetOrganizationContentConversionProperties");
			organizationContentConversionProperties = localOrganizationContentConversionProperties;
			return tryResult;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00088068 File Offset: 0x00086268
		public OrganizationRelationship GetOrganizationRelationship(OrganizationId organizationId, string domain)
		{
			OrganizationRelationship relationship = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
				relationship = organizationIdCacheValue.GetOrganizationRelationship(domain);
			}, "DirectoryAccessor:GetOrganizationRelationship");
			return relationship;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000880D4 File Offset: 0x000862D4
		public int? GetPrimaryGroupId(IRecipientSession recipientSession, SecurityIdentifier userSid)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ADUser adUser = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				adUser = (recipientSession.FindBySid(userSid) as ADUser);
			}, "DirectoryAccessor::GetPrimaryGroupId");
			if (adUser == null)
			{
				return null;
			}
			return (int?)adUser[ADUserSchema.PrimaryGroupId];
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00088168 File Offset: 0x00086368
		public bool IsLicensingEnforcedInOrg(OrganizationId organizationId)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			bool isLicensingEnforcedInOrg = false;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				isLicensingEnforcedInOrg = CapabilityHelper.GetIsLicensingEnforcedInOrg(organizationId);
			}, "DirectoryAccessor::IsLicensingEnforcedInOrg");
			return isLicensingEnforcedInOrg;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000881E8 File Offset: 0x000863E8
		public bool IsTenantAccessBlocked(OrganizationId organizationId)
		{
			bool isTenantAccessBlocked = false;
			if (organizationId != null)
			{
				this.DoAdCallAndTranslateExceptions(delegate
				{
					OrganizationProperties organizationProperties;
					if (OrganizationPropertyCache.TryGetOrganizationProperties(organizationId, out organizationProperties))
					{
						isTenantAccessBlocked = organizationProperties.IsTenantAccessBlocked;
					}
				}, "DirectoryAccessor::IsTenantAccessBlocked");
			}
			return isTenantAccessBlocked;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00088250 File Offset: 0x00086450
		public Server GetLocalServer()
		{
			Server localServer = null;
			this.DoAdCallAndTranslateExceptions(delegate
			{
				localServer = LocalServer.GetServer();
			}, "DirectoryAccessor::GetLocalServer");
			return localServer;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00088288 File Offset: 0x00086488
		private IGenericADUser TranslateADRecipient(ADRecipient recipient, bool checkForSystemMailbox = false)
		{
			if (recipient != null)
			{
				if (checkForSystemMailbox && recipient.RecipientType == RecipientType.SystemMailbox)
				{
					ADSystemMailbox adsystemMailbox = recipient as ADSystemMailbox;
					if (adsystemMailbox != null)
					{
						return new ADSystemMailboxGenericWrapper(adsystemMailbox);
					}
				}
				else
				{
					ADUser aduser = recipient as ADUser;
					if (aduser != null)
					{
						return new ADUserGenericWrapper(aduser);
					}
					ADGroup adgroup = recipient as ADGroup;
					if (adgroup != null && adgroup.RecipientType == RecipientType.MailUniversalDistributionGroup && adgroup.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
					{
						return new ADGroupGenericWrapper(adgroup);
					}
				}
			}
			return null;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000882F4 File Offset: 0x000864F4
		private IGenericADUser TranslateMiniRecipient(StorageMiniRecipient recipient)
		{
			IGenericADUser result = null;
			if (recipient != null)
			{
				result = new MiniRecipientGenericWrapper(recipient);
			}
			return result;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00088310 File Offset: 0x00086510
		private void DoAdCallAndTranslateExceptions(Action call, string methodName)
		{
			try
			{
				call();
			}
			catch (DataValidationException innerException)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound, innerException);
			}
			catch (DataSourceOperationException ex)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "{0}. Failed due to directory exception {1}.", new object[]
				{
					methodName,
					ex
				});
			}
			catch (DataSourceTransientException ex2)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "{0}. Failed due to directory exception {1}.", new object[]
				{
					methodName,
					ex2
				});
			}
		}
	}
}
