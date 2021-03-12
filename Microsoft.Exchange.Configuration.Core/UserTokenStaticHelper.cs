using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200000F RID: 15
	internal static class UserTokenStaticHelper
	{
		// Token: 0x0600005D RID: 93 RVA: 0x0000384C File Offset: 0x00001A4C
		internal static ADRawEntry GetADRawEntry(UserToken token)
		{
			return UserTokenStaticHelper.GetADRawEntry(token.PartitionId, token.Organization, token.UserSid);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003868 File Offset: 0x00001A68
		internal static ADRawEntry GetADRawEntry(PartitionId partitionId, OrganizationId organizationId, SecurityIdentifier sid)
		{
			ADRawEntry adrawEntry = null;
			if (UserTokenStaticHelper.adRawEntryCache.TryGetValue(sid, out adrawEntry))
			{
				return adrawEntry;
			}
			IRecipientSession recipientSession;
			if (partitionId != null)
			{
				recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 92, "GetADRawEntry", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\Core\\Common\\UserTokenStaticHelper.cs");
			}
			else if (organizationId != null && !OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 99, "GetADRawEntry", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\Core\\Common\\UserTokenStaticHelper.cs");
			}
			else
			{
				recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 106, "GetADRawEntry", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\Core\\Common\\UserTokenStaticHelper.cs");
			}
			recipientSession.UseGlobalCatalog = true;
			adrawEntry = recipientSession.FindADRawEntryBySid(sid, UserTokenStaticHelper.properties);
			UserTokenStaticHelper.adRawEntryCache.InsertAbsolute(sid, adrawEntry, UserTokenStaticHelper.cacheTimeout, null);
			return adrawEntry;
		}

		// Token: 0x0400003D RID: 61
		private static PropertyDefinition[] userProperties = new PropertyDefinition[]
		{
			ADObjectSchema.RawName,
			ADObjectSchema.Name,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.DisplayName,
			ADObjectSchema.Id,
			ADObjectSchema.ExchangeVersion,
			IADSecurityPrincipalSchema.Sid,
			ADRecipientSchema.MasterAccountSid,
			ADRecipientSchema.ProtocolSettings,
			ADRecipientSchema.RemotePowerShellEnabled,
			ADUserSchema.UserPrincipalName,
			ADRecipientSchema.WindowsLiveID,
			ADObjectSchema.OrganizationalUnitRoot
		};

		// Token: 0x0400003E RID: 62
		private static PropertyDefinition[] properties = UserTokenStaticHelper.userProperties.Union(ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>().AllProperties).ToArray<PropertyDefinition>();

		// Token: 0x0400003F RID: 63
		private static TimeoutCache<SecurityIdentifier, ADRawEntry> adRawEntryCache = new TimeoutCache<SecurityIdentifier, ADRawEntry>(20, 1000, false);

		// Token: 0x04000040 RID: 64
		private static TimeSpan cacheTimeout = TimeSpan.FromMinutes(5.0);
	}
}
