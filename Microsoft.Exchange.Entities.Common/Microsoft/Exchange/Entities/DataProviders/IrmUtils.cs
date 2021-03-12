using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x02000010 RID: 16
	internal static class IrmUtils
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002E08 File Offset: 0x00001008
		internal static void DecodeIrmMessage(StoreSession storeSession, Item mailboxItem, bool acquireLicense)
		{
			RightsManagedMessageItem rightsManagedMessageItem = mailboxItem as RightsManagedMessageItem;
			if (rightsManagedMessageItem == null)
			{
				return;
			}
			if (!IrmUtils.DoesSessionSupportIrm(storeSession))
			{
				return;
			}
			if (!rightsManagedMessageItem.IsRestricted)
			{
				return;
			}
			if (!rightsManagedMessageItem.CanDecode)
			{
				return;
			}
			if (rightsManagedMessageItem.IsDecoded)
			{
				return;
			}
			OutboundConversionOptions outboundConversionOptions = IrmUtils.GetOutboundConversionOptions(storeSession.MailboxOwner.MailboxInfo.OrganizationId);
			rightsManagedMessageItem.TryDecode(outboundConversionOptions, acquireLicense);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E68 File Offset: 0x00001068
		internal static bool DoesSessionSupportIrm(StoreSession session)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession == null)
			{
				return false;
			}
			OrganizationId organizationId = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
			bool result;
			try
			{
				result = RmsClientManager.IRMConfig.IsClientAccessServerEnabledForTenant(organizationId);
			}
			catch (ExchangeConfigurationException innerException)
			{
				throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, innerException);
			}
			catch (RightsManagementException ex)
			{
				if (ex.IsPermanent)
				{
					throw new RightsManagementPermanentException(ServerStrings.RmExceptionGenericMessage, ex);
				}
				throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, ex);
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EF0 File Offset: 0x000010F0
		internal static AttachmentCollection GetAttachmentCollection(IItem xsoItem)
		{
			return IrmUtils.IsMessageRestrictedAndDecoded(xsoItem) ? ((RightsManagedMessageItem)xsoItem).ProtectedAttachmentCollection : xsoItem.AttachmentCollection;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F1C File Offset: 0x0000111C
		internal static Body GetBody(IItem xsoItem)
		{
			RightsManagedMessageItem rightsManagedMessageItem = xsoItem as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsDecoded)
			{
				return rightsManagedMessageItem.ProtectedBody;
			}
			return xsoItem.Body;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F48 File Offset: 0x00001148
		internal static string GetItemPreview(Item xsoItem)
		{
			Body body = IrmUtils.GetBody(xsoItem);
			if (body == null)
			{
				return null;
			}
			return body.PreviewText;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F68 File Offset: 0x00001168
		internal static OutboundConversionOptions GetOutboundConversionOptions(OrganizationId orgId)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 211, "GetOutboundConversionOptions", "f:\\15.00.1497\\sources\\dev\\Entities\\src\\Common\\DataProviders\\IrmUtils.cs");
			return new OutboundConversionOptions(IrmUtils.GetDefaultAcceptedDomainName(orgId))
			{
				ClearCategories = false,
				AllowPartialStnefConversion = true,
				DemoteBcc = true,
				UserADSession = tenantOrRootOrgRecipientSession
			};
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FBF File Offset: 0x000011BF
		internal static bool IsApplyingRmsTemplate(string complianceString, StoreSession session, out RmsTemplate template)
		{
			template = null;
			return !string.IsNullOrEmpty(complianceString) && "0" != complianceString && IrmUtils.TryLookupRmsTemplate(complianceString, session, out template);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FE3 File Offset: 0x000011E3
		internal static bool IsIrmEnabled(bool clientSupportsIrm, StoreSession mailboxSession)
		{
			return clientSupportsIrm && IrmUtils.DoesSessionSupportIrm(mailboxSession);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FF0 File Offset: 0x000011F0
		internal static bool IsMessageRestrictedAndDecoded(IItem item)
		{
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			return rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && rightsManagedMessageItem.IsDecoded;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000301C File Offset: 0x0000121C
		internal static bool IsProtectedVoicemailItem(RightsManagedMessageItem item)
		{
			string valueOrDefault = item.GetValueOrDefault<string>(StoreObjectSchema.ItemClass);
			return valueOrDefault.StartsWith("IPM.Note.RPMSG.Microsoft.Voicemail", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003044 File Offset: 0x00001244
		internal static bool IsProtectedVoicemailItem(ItemPart itemPart)
		{
			string valueOrDefault = itemPart.StorePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			return valueOrDefault.StartsWith("IPM.Note.RPMSG.Microsoft.Voicemail", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003073 File Offset: 0x00001273
		internal static void ThrowIfInternalLicensingDisabled(OrganizationId organizationId)
		{
			if (!RmsClientManager.IRMConfig.IsInternalLicensingEnabledForTenant(organizationId))
			{
				throw new RightsManagementPermanentException(Strings.RightsManagementInternalLicensingDisabled, null);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003090 File Offset: 0x00001290
		internal static MailboxSession ValidateAndGetMailboxSession(StoreSession storeSession)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				throw new RightsManagementPermanentException(Strings.RightsManagementMailboxOnlySupport, null);
			}
			return mailboxSession;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030B4 File Offset: 0x000012B4
		private static string GetDefaultAcceptedDomainName(OrganizationId organizationId)
		{
			string text;
			if (IrmUtils.DefaultAcceptedDomainTable.TryGetValue(organizationId, out text))
			{
				return text;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 375, "GetDefaultAcceptedDomainName", "f:\\15.00.1497\\sources\\dev\\Entities\\src\\Common\\DataProviders\\IrmUtils.cs");
			AcceptedDomain defaultAcceptedDomain = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain != null)
			{
				text = defaultAcceptedDomain.DomainName.ToString();
			}
			IrmUtils.DefaultAcceptedDomainTable.Add(organizationId, text);
			return text;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003144 File Offset: 0x00001344
		private static RmsTemplate LookupRmsTemplate(Guid templateId, MailboxSession session)
		{
			OrganizationId organizationId = session.MailboxOwner.MailboxInfo.OrganizationId;
			IrmUtils.ThrowIfInternalLicensingDisabled(organizationId);
			IEnumerable<RmsTemplate> source = RmsClientManager.AcquireRmsTemplates(organizationId, false);
			RmsTemplate result;
			try
			{
				result = source.SingleOrDefault((RmsTemplate template) => template.Id.Equals(templateId));
			}
			catch (InvalidOperationException)
			{
				throw new RightsManagementPermanentException(Strings.ErrorRightsManagementDuplicateTemplateId(templateId.ToString()), null);
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031CC File Offset: 0x000013CC
		private static bool TryLookupRmsTemplate(string complianceIdString, StoreSession session, out RmsTemplate template)
		{
			template = null;
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession == null)
			{
				return false;
			}
			Guid guid;
			if (!GuidHelper.TryParseGuid(complianceIdString, out guid) || Guid.Empty.Equals(guid))
			{
				return false;
			}
			Exception ex = null;
			try
			{
				template = IrmUtils.LookupRmsTemplate(guid, mailboxSession);
			}
			catch (RightsManagementPermanentException ex2)
			{
				ex = ex2;
			}
			catch (RightsManagementTransientException ex3)
			{
				ex = ex3;
			}
			catch (ExchangeConfigurationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.CommonTracer.TraceError<Exception>(0L, "Failed to lookup RMS template due to: {0}", ex);
			}
			return null != template;
		}

		// Token: 0x04000027 RID: 39
		internal const string NoRestrictionComplianceId = "0";

		// Token: 0x04000028 RID: 40
		private const string ProtectedVmItemClassPrefix = "IPM.Note.RPMSG.Microsoft.Voicemail";

		// Token: 0x04000029 RID: 41
		private static readonly MruDictionaryCache<OrganizationId, string> DefaultAcceptedDomainTable = new MruDictionaryCache<OrganizationId, string>(5, 50000, 5);
	}
}
