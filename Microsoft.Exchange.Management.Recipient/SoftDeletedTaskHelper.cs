using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200000E RID: 14
	internal static class SoftDeletedTaskHelper
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00004DD8 File Offset: 0x00002FD8
		internal static IRecipientSession GetSessionForSoftDeletedObjects(IRecipientSession dataProvider, ADObjectId rootId)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(dataProvider.DomainController, rootId ?? dataProvider.SearchRoot, dataProvider.Lcid, dataProvider.ReadOnly, dataProvider.ConsistencyMode, dataProvider.NetworkCredential, dataProvider.SessionSettings, 47, "GetSessionForSoftDeletedObjects", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\common\\SoftDeletedTaskHelper.cs");
			tenantOrRootOrgRecipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
			tenantOrRootOrgRecipientSession.EnforceDefaultScope = dataProvider.EnforceDefaultScope;
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = dataProvider.UseGlobalCatalog;
			tenantOrRootOrgRecipientSession.LinkResolutionServer = dataProvider.LinkResolutionServer;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004E5C File Offset: 0x0000305C
		internal static IRecipientSession GetSessionForSoftDeletedObjects(OrganizationId orgId, Fqdn domainController)
		{
			ADObjectId searchRoot = null;
			if (orgId != null && orgId.OrganizationalUnit != null)
			{
				searchRoot = new ADObjectId("OU=Soft Deleted Objects," + orgId.OrganizationalUnit.DistinguishedName);
			}
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId ?? OrganizationId.ForestWideOrgId, null, false);
			adsessionSettings.IncludeSoftDeletedObjects = true;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, searchRoot, CultureInfo.CurrentCulture.LCID, false, ConsistencyMode.IgnoreInvalid, null, adsessionSettings, 83, "GetSessionForSoftDeletedObjects", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\common\\SoftDeletedTaskHelper.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004EE9 File Offset: 0x000030E9
		public static IRecipientSession CreateTenantOrRootOrgRecipientSessionIncludeInactiveMailbox(IRecipientSession dataProvider, OrganizationId orgId)
		{
			return SoftDeletedTaskHelper.CreateIncludeInactiveMailboxTenantOrRootOrgRecipientSession(dataProvider, null);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004EF4 File Offset: 0x000030F4
		public static IRecipientSession CreateTenantOrRootOrgRecipientSessionInactiveMailboxOnly(IRecipientSession dataProvider, OrganizationId orgId)
		{
			ADObjectId rootId = dataProvider.SearchRoot;
			if (orgId != null && orgId.OrganizationalUnit != null)
			{
				rootId = new ADObjectId("OU=Soft Deleted Objects," + orgId.OrganizationalUnit.DistinguishedName);
			}
			return SoftDeletedTaskHelper.CreateIncludeInactiveMailboxTenantOrRootOrgRecipientSession(dataProvider, rootId);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004F3C File Offset: 0x0000313C
		internal static ADUser GetSoftDeletedADUser(OrganizationId organizationId, RecipientIdParameter identity, Task.ErrorLoggerDelegate errorLogger)
		{
			IRecipientSession sessionForSoftDeletedObjects = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(organizationId, null);
			IEnumerable<ADUser> enumerable = identity.GetObjects<ADUser>(organizationId.OrganizationalUnit, sessionForSoftDeletedObjects) ?? new List<ADUser>();
			ADUser result;
			using (IEnumerator<ADUser> enumerator = enumerable.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					errorLogger(new RecipientTaskException(Strings.ErrorMailboxNotFound(identity.ToString())), ExchangeErrorCategory.Client, null);
				}
				result = enumerator.Current;
				if (enumerator.MoveNext())
				{
					errorLogger(new RecipientTaskException(Strings.ErrorMailboxNotUnique(identity.ToString())), ExchangeErrorCategory.Client, null);
				}
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004FDC File Offset: 0x000031DC
		internal static void UpdateRecipientForSoftDelete(IRecipientSession session, ADUser recipient, bool includeInGarbageCollection)
		{
			SoftDeletedTaskHelper.UpdateRecipientForSoftDelete(session, recipient, includeInGarbageCollection, false);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004FE8 File Offset: 0x000031E8
		internal static void UpdateRecipientForSoftDelete(IRecipientSession session, ADUser recipient, bool includeInGarbageCollection, bool isInactive)
		{
			int num = 1;
			if (includeInGarbageCollection)
			{
				num |= 4;
			}
			if (isInactive)
			{
				num |= 8;
			}
			recipient.propertyBag.SetField(ADRecipientSchema.RecipientSoftDeletedStatus, num);
			recipient.propertyBag.SetField(ADRecipientSchema.WhenSoftDeleted, new DateTime?(DateTime.UtcNow));
			int num2 = (int)recipient.propertyBag[ADRecipientSchema.TransportSettingFlags];
			num2 |= 8;
			recipient.propertyBag.SetField(ADRecipientSchema.TransportSettingFlags, num2);
			if (!"Soft Deleted Objects".Equals(recipient.Id.Parent.Name))
			{
				ADObjectId childId = recipient.Id.Parent.GetChildId("OU", "Soft Deleted Objects");
				childId = childId.GetChildId(SoftDeletedTaskHelper.ReservedADNameStringRegex.Replace(recipient.Id.Name, string.Empty));
				string userPrincipalName = recipient.UserPrincipalName;
				session.SessionSettings.IncludeSoftDeletedObjects = true;
				if (session.Read(childId) != null)
				{
					childId = childId.Parent.GetChildId(MailboxTaskHelper.AppendRandomNameSuffix(childId.Name));
				}
				session.SessionSettings.IncludeSoftDeletedObjects = false;
				recipient.SetId(childId);
				recipient.UserPrincipalName = userPrincipalName;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005118 File Offset: 0x00003318
		internal static void UpdateMailboxForDisconnectInactiveMailbox(ADUser mailbox)
		{
			string text = mailbox[ADUserSchema.AdminDisplayName] as string;
			if (text == null || !text.Contains("MSOID:"))
			{
				mailbox[ADUserSchema.AdminDisplayName] = text + "MSOID:" + mailbox.ExternalDirectoryObjectId;
			}
			mailbox.ExternalDirectoryObjectId = string.Empty;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005170 File Offset: 0x00003370
		internal static string GetUniqueNameForRecovery(IRecipientSession session, string name, ADObjectId id)
		{
			string result = name;
			ADRecipient adrecipient = session.Read(id.Parent.Parent.GetChildId(name));
			if (adrecipient != null)
			{
				result = MailboxTaskHelper.AppendRandomNameSuffix(name);
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000051A2 File Offset: 0x000033A2
		internal static void UpdateExchangeGuidForMailEnabledUser(ADUser user)
		{
			if (user.ExchangeGuid != Guid.Empty && user.ExchangeGuid != SoftDeletedTaskHelper.PredefinedExchangeGuid)
			{
				user.PreviousExchangeGuid = user.ExchangeGuid;
				user.ExchangeGuid = SoftDeletedTaskHelper.PredefinedExchangeGuid;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000051E0 File Offset: 0x000033E0
		public static bool MSOSyncEnabled(IConfigurationSession session, OrganizationId organizationId)
		{
			ExchangeConfigurationUnit exchangeConfigUnit = RecipientTaskHelper.GetExchangeConfigUnit(session, organizationId);
			return exchangeConfigUnit != null && exchangeConfigUnit.MSOSyncEnabled;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005200 File Offset: 0x00003400
		public static bool MSODirSyncEnabled(IConfigurationSession session, OrganizationId organizationId)
		{
			ExchangeConfigurationUnit exchangeConfigUnit = RecipientTaskHelper.GetExchangeConfigUnit(session, organizationId);
			return exchangeConfigUnit != null && exchangeConfigUnit.MSOSyncEnabled && exchangeConfigUnit.IsDirSyncRunning;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000522C File Offset: 0x0000342C
		public static bool IsSoftDeleteSupportedRecipientTypeDetail(RecipientTypeDetails typeDetail)
		{
			if (typeDetail <= RecipientTypeDetails.RoomMailbox)
			{
				if (typeDetail != RecipientTypeDetails.UserMailbox && typeDetail != RecipientTypeDetails.SharedMailbox && typeDetail != RecipientTypeDetails.RoomMailbox)
				{
					return false;
				}
			}
			else if (typeDetail != RecipientTypeDetails.EquipmentMailbox && typeDetail != RecipientTypeDetails.MailUser && typeDetail != RecipientTypeDetails.User)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005270 File Offset: 0x00003470
		public static void UpdateShadowWhenSoftDeletedProperty(IRecipientSession session, IConfigurationSession configSession, OrganizationId organizationId, ADUser user)
		{
			if (!session.ServerSettings.WriteShadowProperties || user.propertyBag[ADRecipientSchema.WhenSoftDeleted.ShadowProperty] != null)
			{
				return;
			}
			ExchangeConfigurationUnit exchangeConfigUnit = RecipientTaskHelper.GetExchangeConfigUnit(configSession, organizationId);
			if (exchangeConfigUnit != null && exchangeConfigUnit.MSOSyncEnabled)
			{
				user.propertyBag.SetField(ADRecipientSchema.WhenSoftDeleted.ShadowProperty, user.WhenSoftDeleted);
				session.Save(user);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000052E0 File Offset: 0x000034E0
		private static IRecipientSession CreateIncludeInactiveMailboxTenantOrRootOrgRecipientSession(IRecipientSession dataProvider, ADObjectId rootId)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(dataProvider.DomainController, (rootId == null) ? dataProvider.SearchRoot : rootId, dataProvider.Lcid, dataProvider.ReadOnly, dataProvider.ConsistencyMode, dataProvider.NetworkCredential, dataProvider.SessionSettings, ConfigScopes.TenantSubTree, 342, "CreateIncludeInactiveMailboxTenantOrRootOrgRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\common\\SoftDeletedTaskHelper.cs");
			tenantOrRootOrgRecipientSession.SessionSettings.IncludeInactiveMailbox = true;
			tenantOrRootOrgRecipientSession.EnforceDefaultScope = dataProvider.EnforceDefaultScope;
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = dataProvider.UseGlobalCatalog;
			tenantOrRootOrgRecipientSession.LinkResolutionServer = dataProvider.LinkResolutionServer;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x04000011 RID: 17
		private const string ReservedStringPattern = "\\x0a(CNF|DEL):([0-9a-f]){8}-(([0-9a-f]){4}-){3}([0-9a-f]){12}$";

		// Token: 0x04000012 RID: 18
		private static readonly Regex ReservedADNameStringRegex = new Regex("\\x0a(CNF|DEL):([0-9a-f]){8}-(([0-9a-f]){4}-){3}([0-9a-f]){12}$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000013 RID: 19
		internal static readonly Guid PredefinedExchangeGuid = new Guid("{1B2EAA95-0D64-4469-9FB2-D8F9BE3E28CE}");
	}
}
