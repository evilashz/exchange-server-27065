using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200001C RID: 28
	internal static class RightsManagement
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00009198 File Offset: 0x00007398
		public static RightsManagementProcessingResult ProcessRightsManagedMessage(StoreSession session, Item item)
		{
			RightsManagementProcessingResult result = RightsManagementProcessingResult.NotRightsManaged;
			MessageItem messageItem = item as MessageItem;
			if (messageItem != null)
			{
				if (!messageItem.IsRestricted)
				{
					return result;
				}
				if (!RightsManagement.config.IRMMessageProcessingEnabled)
				{
					return RightsManagementProcessingResult.Skipped;
				}
				OrganizationId organizationId = RightsManagement.GetOrganizationId(session);
				if (RmsClientManager.IRMConfig.IsSearchEnabledForTenant(organizationId))
				{
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 96, "ProcessRightsManagedMessage", "f:\\15.00.1497\\sources\\dev\\Search\\src\\MailboxOperators\\RightsManagement.cs");
					string acceptedDomainName = RightsManagement.GetAcceptedDomainName(organizationId);
					OutboundConversionOptions options = new OutboundConversionOptions(tenantOrRootOrgRecipientSession, acceptedDomainName);
					RightsManagedMessageItem rightsManagedMessageItem = messageItem as RightsManagedMessageItem;
					if (rightsManagedMessageItem != null)
					{
						if (!rightsManagedMessageItem.CanDecode)
						{
							return RightsManagementProcessingResult.Skipped;
						}
						try
						{
							if (!rightsManagedMessageItem.IsDecoded)
							{
								bool isTest = ExEnvironment.IsTest;
								rightsManagedMessageItem.Decode(options, isTest);
							}
							result = RightsManagementProcessingResult.Success;
						}
						catch (RightsManagementException)
						{
							result = RightsManagementProcessingResult.FailedPermanent;
						}
						catch (RightsManagementPermanentException)
						{
							result = RightsManagementProcessingResult.FailedPermanent;
						}
						catch (RightsManagementTransientException)
						{
							result = RightsManagementProcessingResult.FailedTransient;
						}
						catch (ObjectNotFoundException)
						{
							result = RightsManagementProcessingResult.FailedPermanent;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009290 File Offset: 0x00007490
		public static RightsManagementProcessingResult ProcessSMIMEMessage(StoreSession session, Item item, out Item smimeItem)
		{
			smimeItem = null;
			RightsManagementProcessingResult result = RightsManagementProcessingResult.NotRightsManaged;
			MessageItem messageItem = item as MessageItem;
			if (messageItem != null)
			{
				if (!ObjectClass.IsSmime(messageItem.ClassName))
				{
					return result;
				}
				if (ConvertUtils.IsMessageOpaqueSigned(messageItem))
				{
					return RightsManagementProcessingResult.Skipped;
				}
				OrganizationId organizationId = RightsManagement.GetOrganizationId(session);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 174, "ProcessSMIMEMessage", "f:\\15.00.1497\\sources\\dev\\Search\\src\\MailboxOperators\\RightsManagement.cs");
				string acceptedDomainName = RightsManagement.GetAcceptedDomainName(organizationId);
				InboundConversionOptions options = new InboundConversionOptions(tenantOrRootOrgRecipientSession, acceptedDomainName);
				if (ItemConversion.TryOpenSMimeContent(messageItem, options, out smimeItem))
				{
					result = RightsManagementProcessingResult.IsSMIME;
				}
				else
				{
					result = RightsManagementProcessingResult.FailedPermanent;
				}
			}
			return result;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009314 File Offset: 0x00007514
		private static OrganizationId GetOrganizationId(StoreSession session)
		{
			OrganizationId organizationId = null;
			if (session != null)
			{
				organizationId = session.OrganizationId;
			}
			if (organizationId == null)
			{
				throw new OrgIdNotFoundException();
			}
			return organizationId;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009340 File Offset: 0x00007540
		private static string GetAcceptedDomainName(OrganizationId orgId)
		{
			string text;
			lock (RightsManagement.lockObject)
			{
				if (!RightsManagement.defaultAcceptedDomainTable.TryGetValue(orgId, out text))
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 227, "GetAcceptedDomainName", "f:\\15.00.1497\\sources\\dev\\Search\\src\\MailboxOperators\\RightsManagement.cs");
					AcceptedDomain defaultAcceptedDomain = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain();
					if (defaultAcceptedDomain == null)
					{
						throw new OperationFailedException(Strings.AcceptedDomainRetrievalFailure);
					}
					text = defaultAcceptedDomain.DomainName.ToString();
					RightsManagement.defaultAcceptedDomainTable.InsertSliding(orgId, text, RightsManagement.cacheTimeout, new RemoveItemDelegate<OrganizationId, string>(RightsManagement.RemoveFromCacheCallback));
				}
			}
			return text;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000093EC File Offset: 0x000075EC
		private static void RemoveFromCacheCallback(OrganizationId key, string acceptedDomainName, RemoveReason reason)
		{
		}

		// Token: 0x0400014E RID: 334
		private const int MaxCacheSize = 1000;

		// Token: 0x0400014F RID: 335
		private static readonly TimeSpan cacheTimeout = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000150 RID: 336
		private static readonly object lockObject = new object();

		// Token: 0x04000151 RID: 337
		private static readonly SearchConfig config = SearchConfig.Instance;

		// Token: 0x04000152 RID: 338
		private static TimeoutCache<OrganizationId, string> defaultAcceptedDomainTable = new TimeoutCache<OrganizationId, string>(1, 1000, false);
	}
}
