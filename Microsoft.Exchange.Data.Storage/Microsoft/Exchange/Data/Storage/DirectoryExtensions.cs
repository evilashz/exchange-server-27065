using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002F0 RID: 752
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DirectoryExtensions
	{
		// Token: 0x06002158 RID: 8536 RVA: 0x000883AC File Offset: 0x000865AC
		public static ADSessionSettings ToADSessionSettings(this OrganizationId organizationId)
		{
			if (organizationId == null || organizationId == OrganizationId.ForestWideOrgId)
			{
				return ADSessionSettings.FromRootOrgScopeSet();
			}
			if (DirectoryExtensions.ServicesRootOrgId == null)
			{
				DirectoryExtensions.ServicesRootOrgId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			}
			return ADSessionSettings.FromOrganizationIdWithoutRbacScopes(DirectoryExtensions.ServicesRootOrgId, organizationId, null, false);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000883F5 File Offset: 0x000865F5
		public static bool IsNullOrEmpty(this ADObjectId objectId)
		{
			return objectId == null || objectId.ObjectGuid == Guid.Empty;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0008840C File Offset: 0x0008660C
		public static Guid GetTenantGuid(this OrganizationId organizationId)
		{
			Guid result = Guid.Empty;
			if (organizationId != null && organizationId.OrganizationalUnit != null)
			{
				result = organizationId.OrganizationalUnit.ObjectGuid;
			}
			return result;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00088440 File Offset: 0x00086640
		public static T GetWithDirectoryExceptionTranslation<T>(Func<T> getter)
		{
			T result;
			try
			{
				result = getter();
			}
			catch (DataValidationException innerException)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound, innerException);
			}
			catch (DataSourceOperationException ex)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "{0}. Failed due to directory exception {1}.", new object[]
				{
					ex
				});
			}
			catch (DataSourceTransientException ex2)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "{0}. Failed due to directory exception {1}.", new object[]
				{
					ex2
				});
			}
			return result;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000884D0 File Offset: 0x000866D0
		public static bool IsArchiveMailbox(this IGenericADUser user, Guid mailboxGuid)
		{
			ArgumentValidator.ThrowIfEmpty("mailboxGuid", mailboxGuid);
			return user.ArchiveGuid == mailboxGuid;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00088500 File Offset: 0x00086700
		public static bool IsAggregatedMailbox(this IGenericADUser user, Guid mailboxGuid)
		{
			ArgumentValidator.ThrowIfEmpty("mailboxGuid", mailboxGuid);
			return user.AggregatedMailboxGuids != null && user.AggregatedMailboxGuids.Any((Guid aggregatedMailboxGuid) => aggregatedMailboxGuid == mailboxGuid);
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0008854C File Offset: 0x0008674C
		public static bool IsPrimaryMailboxRemote(this IGenericADUser user)
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.DataStorage.RepresentRemoteMailbox.Enabled && user.RecipientType == RecipientType.MailUser && user.MailboxDatabase.IsNullOrEmpty() && (user.RecipientTypeDetails & (RecipientTypeDetails)((ulong)int.MinValue)) == (RecipientTypeDetails)((ulong)int.MinValue) && user.ExternalEmailAddress != null;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000885AC File Offset: 0x000867AC
		public static MailboxLocationType? GetMailboxLocationType(this IGenericADUser user, Guid mailboxGuid)
		{
			MailboxLocationType? result = null;
			if (user.IsArchiveMailbox(mailboxGuid))
			{
				result = new MailboxLocationType?(MailboxLocationType.MainArchive);
			}
			else if (user.IsAggregatedMailbox(mailboxGuid))
			{
				result = new MailboxLocationType?(MailboxLocationType.Aggregated);
			}
			else if (user.MailboxGuid == mailboxGuid)
			{
				result = new MailboxLocationType?(MailboxLocationType.Primary);
			}
			else if (user.MailboxLocations != null)
			{
				foreach (IMailboxLocationInfo mailboxLocationInfo in user.MailboxLocations)
				{
					if (mailboxLocationInfo.MailboxGuid == mailboxGuid)
					{
						result = new MailboxLocationType?(mailboxLocationInfo.MailboxLocationType);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00088660 File Offset: 0x00086860
		public static bool IsArchiveMailboxRemote(this IGenericADUser user)
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.DataStorage.RepresentRemoteMailbox.Enabled && (user.IsPrimaryMailboxRemote() || user.ArchiveDomain != null);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000886A0 File Offset: 0x000868A0
		public static IRecipientSession CreateRecipientSession(this ADSessionSettings adSettings, string domainController = null)
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.IgnoreInvalid, null, adSettings, 217, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ActiveDirectory\\DirectoryExtensions.cs");
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000886CB File Offset: 0x000868CB
		public static IConfigurationSession CreateConfigurationSession(this ADSessionSettings adSettings)
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, adSettings, 232, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ActiveDirectory\\DirectoryExtensions.cs");
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000886E9 File Offset: 0x000868E9
		public static SmtpAddress GetFederatedSmtpAddress(this StorageMiniRecipient storageMiniRecipient)
		{
			return new MiniRecipientGenericWrapper(storageMiniRecipient).GetFederatedSmtpAddress(storageMiniRecipient.PrimarySmtpAddress);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000886FC File Offset: 0x000868FC
		public static SmtpAddress GetFederatedSmtpAddress(this IGenericADUser genericAdUser, SmtpAddress preferredSmtpAddress)
		{
			OrganizationId key = genericAdUser.OrganizationId ?? OrganizationId.ForestWideOrgId;
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(key);
			if (organizationIdCacheValue.FederatedDomains == null)
			{
				throw new UserWithoutFederatedProxyAddressException();
			}
			if (organizationIdCacheValue.DefaultFederatedDomain != null)
			{
				foreach (ProxyAddress proxyAddress in genericAdUser.EmailAddresses)
				{
					if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
					{
						SmtpAddress result = new SmtpAddress(proxyAddress.AddressString);
						if (StringComparer.OrdinalIgnoreCase.Equals(result.Domain, organizationIdCacheValue.DefaultFederatedDomain))
						{
							return result;
						}
					}
				}
			}
			List<string> source = new List<string>(organizationIdCacheValue.FederatedDomains);
			bool isValidAddress = preferredSmtpAddress.IsValidAddress;
			if (isValidAddress && !genericAdUser.EmailAddresses.Contains(new SmtpProxyAddress(preferredSmtpAddress.ToString(), false)))
			{
				throw new ArgumentException("preferredSmtpAddress");
			}
			if (isValidAddress)
			{
				if (source.Contains(preferredSmtpAddress.Domain, StringComparer.OrdinalIgnoreCase))
				{
					return preferredSmtpAddress;
				}
				if (genericAdUser.PrimarySmtpAddress.IsValidAddress && !StringComparer.OrdinalIgnoreCase.Equals(genericAdUser.PrimarySmtpAddress.Domain, preferredSmtpAddress.Domain) && source.Contains(genericAdUser.PrimarySmtpAddress.Domain, StringComparer.OrdinalIgnoreCase))
				{
					return genericAdUser.PrimarySmtpAddress;
				}
			}
			foreach (ProxyAddress proxyAddress2 in genericAdUser.EmailAddresses)
			{
				if (proxyAddress2.Prefix == ProxyAddressPrefix.Smtp)
				{
					SmtpAddress result2 = new SmtpAddress(proxyAddress2.AddressString);
					if (source.Contains(result2.Domain, StringComparer.OrdinalIgnoreCase))
					{
						return result2;
					}
				}
			}
			throw new UserWithoutFederatedProxyAddressException();
		}

		// Token: 0x040013B1 RID: 5041
		private static ADObjectId ServicesRootOrgId;
	}
}
