using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200003B RID: 59
	internal class DirectoryHelper
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		internal static ITenantRecipientSession GetTenantRecipientSessionFromSmtpOrLiveId(string smtpOrLiveId)
		{
			if (string.IsNullOrEmpty(smtpOrLiveId))
			{
				throw new ArgumentNullException("smtpOrLiveId");
			}
			if (!SmtpAddress.IsValidSmtpAddress(smtpOrLiveId))
			{
				throw new ArgumentException(string.Format("{0} is not a valid SmtpAddress.", smtpOrLiveId));
			}
			string domain = SmtpAddress.Parse(smtpOrLiveId).Domain;
			if (string.IsNullOrEmpty(domain))
			{
				throw new ArgumentException(string.Format("Given SmtpAddress {0} does not contain a valid domain", smtpOrLiveId));
			}
			return DirectoryHelper.GetTenantRecipientSessionByDomainName(null, domain, false);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000C448 File Offset: 0x0000A648
		internal static ADRawEntry GetADRawEntry(string puid, string organizationContext, string smtpOrLiveId, PropertyDefinition[] properties)
		{
			ITenantRecipientSession tenantRecipientSession;
			return DirectoryHelper.GetADRawEntry(puid, organizationContext, smtpOrLiveId, properties, false, out tenantRecipientSession);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C461 File Offset: 0x0000A661
		internal static ADRawEntry GetADRawEntry(string puid, string organizationContext, string smtpOrLiveId, PropertyDefinition[] properties, out ITenantRecipientSession recipientSession)
		{
			recipientSession = null;
			return DirectoryHelper.GetADRawEntry(puid, organizationContext, smtpOrLiveId, properties, false, out recipientSession);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C474 File Offset: 0x0000A674
		internal static ADRawEntry GetADRawEntry(string puid, string organizationContext, string smtpOrLiveId, PropertyDefinition[] properties, bool requestForestWideDomainControllerAffinityByUserId, out ITenantRecipientSession tenantRecipient)
		{
			tenantRecipient = DirectoryHelper.GetTenantRecipientSession(puid, organizationContext, smtpOrLiveId, requestForestWideDomainControllerAffinityByUserId);
			ADRawEntry result;
			try
			{
				result = tenantRecipient.FindUniqueEntryByNetID(puid, organizationContext, properties);
			}
			catch (ADTransientException innerException)
			{
				throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(puid, smtpOrLiveId), innerException);
			}
			catch (DataValidationException innerException2)
			{
				throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(puid, smtpOrLiveId), innerException2);
			}
			catch (DataSourceOperationException innerException3)
			{
				throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(puid, smtpOrLiveId), innerException3);
			}
			return result;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		internal static List<string> GetTokenSids(ADRawEntry id, string puid, string organizationContext, string smtpOrLiveId, bool requestForestWideDomainControllerAffinityByUserId)
		{
			ITenantRecipientSession tenantRecipientSession = DirectoryHelper.GetTenantRecipientSession(puid, organizationContext, smtpOrLiveId, requestForestWideDomainControllerAffinityByUserId);
			List<string> tokenSids;
			try
			{
				tokenSids = tenantRecipientSession.GetTokenSids(id, AssignmentMethod.S4U);
			}
			catch (ADTransientException innerException)
			{
				throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(puid, smtpOrLiveId), innerException);
			}
			catch (DataValidationException innerException2)
			{
				throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(puid, smtpOrLiveId), innerException2);
			}
			catch (DataSourceOperationException innerException3)
			{
				throw new BackendRehydrationException(SecurityStrings.CannotLookupUserEx(puid, smtpOrLiveId), innerException3);
			}
			return tokenSids;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C570 File Offset: 0x0000A770
		internal static string GetDomainControllerWithForestWideAffinityByUserId(string userNetId, OrganizationId orgId)
		{
			ADRunspaceServerSettingsProvider instance = ADRunspaceServerSettingsProvider.GetInstance();
			try
			{
				bool flag;
				ADServerInfo gcFromToken = instance.GetGcFromToken(orgId.PartitionId.ForestFQDN, RunspaceServerSettings.GetTokenForUser(userNetId, orgId), out flag, true);
				if (gcFromToken != null)
				{
					return gcFromToken.Fqdn;
				}
			}
			catch (ADTransientException arg)
			{
				ExTraceGlobals.BackendRehydrationTracer.TraceDebug<string, ADTransientException>(0L, "[DirectoryHelper::GetDomainControllerWithForestWideAffinityByUserId] Caught ADTransientException when trying to get ADServerInfo by user NetID for {0}. Exception details: {1}.", userNetId, arg);
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		private static ITenantRecipientSession GetTenantRecipientSessionByDomainName(string puid, string domain, bool requestForestWideDomainControllerAffinityByUserId)
		{
			ADSessionSettings adsessionSettings;
			try
			{
				adsessionSettings = ADSessionSettings.FromTenantAcceptedDomain(domain);
			}
			catch (CannotResolveTenantNameException ex)
			{
				throw new BackendRehydrationException(SecurityStrings.CannotResolveOrganization(domain), new UnauthorizedAccessException(ex.Message));
			}
			string domainController = null;
			if (requestForestWideDomainControllerAffinityByUserId)
			{
				domainController = DirectoryHelper.GetDomainControllerWithForestWideAffinityByUserId(puid, adsessionSettings.CurrentOrganizationId);
			}
			return DirectorySessionFactory.Default.CreateTenantRecipientSession(domainController, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, adsessionSettings, 215, "GetTenantRecipientSessionByDomainName", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\BackendAuthenticator\\DirectoryHelper.cs");
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000C654 File Offset: 0x0000A854
		private static ITenantRecipientSession GetTenantRecipientSession(string puid, string organizationContext, string smtpOrLiveId, bool requestForestWideDomainControllerAffinityByUserId)
		{
			ITenantRecipientSession result;
			if (!string.IsNullOrEmpty(organizationContext))
			{
				result = DirectoryHelper.GetTenantRecipientSessionByDomainName(puid, organizationContext, requestForestWideDomainControllerAffinityByUserId);
			}
			else
			{
				result = DirectoryHelper.GetTenantRecipientSessionFromSmtpOrLiveId(smtpOrLiveId);
			}
			return result;
		}
	}
}
