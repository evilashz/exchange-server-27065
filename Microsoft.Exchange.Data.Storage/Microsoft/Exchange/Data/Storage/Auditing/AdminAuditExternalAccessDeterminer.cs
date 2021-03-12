using System;
using System.Security.Principal;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F1C RID: 3868
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AdminAuditExternalAccessDeterminer
	{
		// Token: 0x06008521 RID: 34081 RVA: 0x002468F4 File Offset: 0x00244AF4
		public static bool IsExternalAccess(string userId, OrganizationId userOrganization, OrganizationId currentOrganization)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return true;
			}
			if (AuditFeatureManager.IsExternalAccessCheckOnDedicatedEnabled())
			{
				NTAccount ntaccount = new NTAccount(AdminAuditExternalAccessDeterminer.TransformUserID(userId));
				SecurityIdentifier securityIdentifier;
				try
				{
					securityIdentifier = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
				}
				catch (IdentityNotMappedException)
				{
					ntaccount = new NTAccount(userId);
					try
					{
						securityIdentifier = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
					}
					catch (IdentityNotMappedException)
					{
						return true;
					}
				}
				bool flag;
				return AdminAuditExternalAccessDeterminer.externalAccessLRUCache.Get(securityIdentifier.ToString(), out flag);
			}
			bool flag2 = userOrganization == null || currentOrganization == null;
			return !flag2 && !userOrganization.Equals(currentOrganization);
		}

		// Token: 0x06008522 RID: 34082 RVA: 0x002469B4 File Offset: 0x00244BB4
		private static bool UserExistsInAD(string sid)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 103, "UserExistsInAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Auditing\\AdminAuditExternalAccessDeterminer.cs");
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				ADMailboxRecipientSchema.Sid
			};
			ADRawEntry adrawEntry = tenantOrRootOrgRecipientSession.FindUserBySid(new SecurityIdentifier(sid), properties);
			return adrawEntry != null;
		}

		// Token: 0x06008523 RID: 34083 RVA: 0x00246A0C File Offset: 0x00244C0C
		private static string TransformUserID(string userId)
		{
			int num = userId.IndexOf('/');
			if (num > 0)
			{
				return userId.Substring(0, num) + "\\" + userId.Substring(userId.LastIndexOf('/') + 1);
			}
			return userId;
		}

		// Token: 0x04005934 RID: 22836
		private const int ExternalAccessCacheCapacity = 10000;

		// Token: 0x04005935 RID: 22837
		private static LRUCache<string, bool> externalAccessLRUCache = new LRUCache<string, bool>(10000, (string key) => !AdminAuditExternalAccessDeterminer.UserExistsInAD(key));
	}
}
