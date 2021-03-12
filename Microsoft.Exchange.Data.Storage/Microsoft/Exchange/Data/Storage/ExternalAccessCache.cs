using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F1F RID: 3871
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExternalAccessCache
	{
		// Token: 0x06008531 RID: 34097 RVA: 0x00246E28 File Offset: 0x00245028
		private ExternalAccessCache()
		{
			this.refreshTimer = new GuardedTimer(delegate(object state)
			{
				this.ExpireEntriesInCache();
			});
			this.ExpireEntriesInCache();
		}

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x06008532 RID: 34098 RVA: 0x00246E78 File Offset: 0x00245078
		public static ExternalAccessCache Instance
		{
			get
			{
				return ExternalAccessCache.instance;
			}
		}

		// Token: 0x06008533 RID: 34099 RVA: 0x00246E80 File Offset: 0x00245080
		public bool IsExternalAccess(OrganizationId organizationId, SecurityIdentifier logonSid)
		{
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, organizationId.ToADSessionSettings(), 332, "IsExternalAccess", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Auditing\\AuditCaches.cs");
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			Util.ThrowOnNullArgument(logonSid, "logonSid");
			Guid guid = Guid.Empty;
			bool flag = false;
			lock (this.externalAccessCache)
			{
				ExternalAccessCache.OrganizationInfo organizationInfo;
				if (this.externalAccessCache.TryGetValue(logonSid, out organizationInfo))
				{
					organizationInfo.LastAccessed = DateTime.UtcNow;
					guid = organizationInfo.OrgIdGuid;
					flag = true;
				}
			}
			if (!flag)
			{
				ExternalAccessCache.OrganizationInfo organizationInfo2 = this.GetOrganizationInfo(logonSid);
				if (organizationInfo2 == null)
				{
					return true;
				}
				lock (this.externalAccessCache)
				{
					if (!this.externalAccessCache.ContainsKey(logonSid))
					{
						this.externalAccessCache.Add(logonSid, organizationInfo2);
					}
				}
				guid = organizationInfo2.OrgIdGuid;
			}
			bool result;
			if (organizationId.OrganizationalUnit == null)
			{
				result = !(guid == Guid.Empty);
			}
			else
			{
				result = !organizationId.OrganizationalUnit.ObjectGuid.Equals(guid);
			}
			return result;
		}

		// Token: 0x06008534 RID: 34100 RVA: 0x00246FEC File Offset: 0x002451EC
		private ExternalAccessCache.OrganizationInfo GetOrganizationInfo(SecurityIdentifier logonSid)
		{
			Util.ThrowOnNullArgument(logonSid, "logonSid");
			ADRecipient recipient = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				recipient = this.recipientSession.FindBySid(logonSid);
			});
			if (adoperationResult.Succeeded && recipient != null)
			{
				return new ExternalAccessCache.OrganizationInfo
				{
					OrgIdGuid = ((recipient.OrganizationId.OrganizationalUnit == null) ? Guid.Empty : recipient.OrganizationId.OrganizationalUnit.ObjectGuid),
					LastAccessed = DateTime.UtcNow
				};
			}
			ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorResolvingLogonUser, logonSid.ToString(), new object[]
			{
				logonSid,
				adoperationResult.Exception
			});
			return null;
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x002470F8 File Offset: 0x002452F8
		private void ExpireEntriesInCache()
		{
			bool flag = false;
			try
			{
				Dictionary<SecurityIdentifier, ExternalAccessCache.OrganizationInfo> obj;
				Monitor.Enter(obj = this.externalAccessCache, ref flag);
				DateTime timeNow = DateTime.UtcNow;
				IEnumerable<SecurityIdentifier> source = from record in this.externalAccessCache.Keys
				where COWAudit.LastAuditAccessRefreshInterval < timeNow - this.externalAccessCache[record].LastAccessed
				select record;
				SecurityIdentifier[] array = source.ToArray<SecurityIdentifier>();
				foreach (SecurityIdentifier key in array)
				{
					this.externalAccessCache.Remove(key);
				}
			}
			finally
			{
				if (flag)
				{
					Dictionary<SecurityIdentifier, ExternalAccessCache.OrganizationInfo> obj;
					Monitor.Exit(obj);
				}
			}
			this.refreshTimer.Change(this.ExpiryInterval, new TimeSpan(0, 0, 0, 0, -1));
		}

		// Token: 0x0400593F RID: 22847
		private readonly TimeSpan ExpiryInterval = new TimeSpan(6, 0, 0);

		// Token: 0x04005940 RID: 22848
		private static readonly ExternalAccessCache instance = new ExternalAccessCache();

		// Token: 0x04005941 RID: 22849
		private readonly Dictionary<SecurityIdentifier, ExternalAccessCache.OrganizationInfo> externalAccessCache = new Dictionary<SecurityIdentifier, ExternalAccessCache.OrganizationInfo>();

		// Token: 0x04005942 RID: 22850
		private readonly GuardedTimer refreshTimer;

		// Token: 0x04005943 RID: 22851
		private IRecipientSession recipientSession;

		// Token: 0x02000F20 RID: 3872
		private class OrganizationInfo
		{
			// Token: 0x04005944 RID: 22852
			internal DateTime LastAccessed;

			// Token: 0x04005945 RID: 22853
			internal Guid OrgIdGuid;
		}
	}
}
