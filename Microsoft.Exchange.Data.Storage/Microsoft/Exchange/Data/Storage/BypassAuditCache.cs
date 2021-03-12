using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F1D RID: 3869
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BypassAuditCache
	{
		// Token: 0x06008526 RID: 34086 RVA: 0x00246A83 File Offset: 0x00244C83
		private BypassAuditCache()
		{
		}

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x06008527 RID: 34087 RVA: 0x00246A96 File Offset: 0x00244C96
		public static BypassAuditCache Instance
		{
			get
			{
				return BypassAuditCache.instance;
			}
		}

		// Token: 0x06008528 RID: 34088 RVA: 0x00246AA0 File Offset: 0x00244CA0
		public bool IsUserBypassingAudit(OrganizationId organizationId, SecurityIdentifier logonSid)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			Util.ThrowOnNullArgument(logonSid, "logonSid");
			bool flag = this.GetOrganizationCache(organizationId).IsUserBypassingAudit(logonSid);
			if (flag)
			{
				return true;
			}
			if (!organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				flag = this.GetOrganizationCache(OrganizationId.ForestWideOrgId).IsUserBypassingAudit(logonSid);
			}
			return flag;
		}

		// Token: 0x06008529 RID: 34089 RVA: 0x00246AF8 File Offset: 0x00244CF8
		public void Reset()
		{
			lock (this.organizationCaches)
			{
				this.organizationCaches.Clear();
			}
		}

		// Token: 0x0600852A RID: 34090 RVA: 0x00246B40 File Offset: 0x00244D40
		private BypassAuditCache.OrganizationCache GetOrganizationCache(OrganizationId organizationId)
		{
			BypassAuditCache.OrganizationCache result;
			lock (this.organizationCaches)
			{
				if (!this.organizationCaches.ContainsKey(organizationId))
				{
					this.organizationCaches[organizationId] = new BypassAuditCache.OrganizationCache(organizationId);
				}
				result = this.organizationCaches[organizationId];
			}
			return result;
		}

		// Token: 0x04005937 RID: 22839
		private static readonly BypassAuditCache instance = new BypassAuditCache();

		// Token: 0x04005938 RID: 22840
		private readonly Dictionary<OrganizationId, BypassAuditCache.OrganizationCache> organizationCaches = new Dictionary<OrganizationId, BypassAuditCache.OrganizationCache>();

		// Token: 0x02000F1E RID: 3870
		private class OrganizationCache
		{
			// Token: 0x0600852C RID: 34092 RVA: 0x00246BBC File Offset: 0x00244DBC
			public OrganizationCache(OrganizationId organizationId)
			{
				Util.ThrowOnNullArgument(organizationId, "organizationId");
				this.adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(BypassAuditCache.OrganizationCache.servicesRootOrgId, organizationId, null, false);
				this.refreshTimer = new GuardedTimer(delegate(object state)
				{
					this.RefreshCache();
				});
				this.RefreshCache();
			}

			// Token: 0x0600852D RID: 34093 RVA: 0x00246C20 File Offset: 0x00244E20
			internal bool IsUserBypassingAudit(SecurityIdentifier identity)
			{
				Util.ThrowOnNullArgument(identity, "identity");
				bool result = false;
				HashSet<SecurityIdentifier> hashSet = Interlocked.Exchange<HashSet<SecurityIdentifier>>(ref this.bypassingUserSid, this.bypassingUserSid);
				if (hashSet != null && hashSet.Contains(identity))
				{
					result = true;
				}
				return result;
			}

			// Token: 0x0600852E RID: 34094 RVA: 0x00246C88 File Offset: 0x00244E88
			private void RefreshCache()
			{
				try
				{
					IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, this.adSettings, ConfigScopes.TenantSubTree, 208, "RefreshCache", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Auditing\\AuditCaches.cs");
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AuditBypassEnabled, true);
					ADRawEntry[] recipients = null;
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						recipients = recipientSession.Find(null, QueryScope.SubTree, filter, null, 0, BypassAuditCache.OrganizationCache.queryProperties);
					});
					if (adoperationResult.Succeeded)
					{
						HashSet<SecurityIdentifier> hashSet = null;
						if (recipients != null && recipients.Length > 0)
						{
							hashSet = new HashSet<SecurityIdentifier>();
							foreach (ADRawEntry adrawEntry in recipients)
							{
								SecurityIdentifier securityIdentifier = adrawEntry[IADSecurityPrincipalSchema.Sid] as SecurityIdentifier;
								if (null != securityIdentifier && !hashSet.Contains(securityIdentifier))
								{
									hashSet.Add(securityIdentifier);
								}
							}
						}
						Interlocked.Exchange<HashSet<SecurityIdentifier>>(ref this.bypassingUserSid, hashSet);
					}
					else
					{
						ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorReadingBypassAudit, this.adSettings.CurrentOrganizationId.ToString(), new object[]
						{
							this.adSettings.CurrentOrganizationId,
							adoperationResult.Exception
						});
					}
				}
				finally
				{
					this.refreshTimer.Change(this.RefreshInterval, new TimeSpan(0, 0, 0, 0, -1));
				}
			}

			// Token: 0x04005939 RID: 22841
			private readonly TimeSpan RefreshInterval = new TimeSpan(0, 30, 0);

			// Token: 0x0400593A RID: 22842
			private static readonly PropertyDefinition[] queryProperties = new PropertyDefinition[]
			{
				IADSecurityPrincipalSchema.Sid
			};

			// Token: 0x0400593B RID: 22843
			private static readonly ADObjectId servicesRootOrgId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();

			// Token: 0x0400593C RID: 22844
			private readonly ADSessionSettings adSettings;

			// Token: 0x0400593D RID: 22845
			private readonly GuardedTimer refreshTimer;

			// Token: 0x0400593E RID: 22846
			private HashSet<SecurityIdentifier> bypassingUserSid;
		}
	}
}
