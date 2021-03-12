using System;
using System.Linq;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000544 RID: 1348
	internal class OfflineAddressBookEndpoint : IEndpoint
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x000CAA42 File Offset: 0x000C8C42
		public Guid[] OfflineAddressBooks
		{
			get
			{
				return this.cachedOabs;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x000CAA4A File Offset: 0x000C8C4A
		public Guid[] OrganizationMailboxDatabases
		{
			get
			{
				return this.cachedOrgMailboxDatabases;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x000CAA52 File Offset: 0x000C8C52
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x000CAA55 File Offset: 0x000C8C55
		// (set) Token: 0x06002129 RID: 8489 RVA: 0x000CAA5D File Offset: 0x000C8C5D
		public Exception Exception { get; set; }

		// Token: 0x0600212A RID: 8490 RVA: 0x000CAA68 File Offset: 0x000C8C68
		public void Initialize()
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				Guid[] allOfflineAddressBookGuids = DirectoryAccessor.Instance.GetAllOfflineAddressBookGuids();
				this.cachedOabs = allOfflineAddressBookGuids;
				Guid[] allDatabaseGuidsForOrganizationMailboxes = DirectoryAccessor.Instance.GetAllDatabaseGuidsForOrganizationMailboxes();
				this.cachedOrgMailboxDatabases = allDatabaseGuidsForOrganizationMailboxes;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000CAAA0 File Offset: 0x000C8CA0
		public bool DetectChange()
		{
			bool flag = false;
			if (!LocalEndpointManager.IsDataCenter)
			{
				Guid[] allOfflineAddressBookGuids = DirectoryAccessor.Instance.GetAllOfflineAddressBookGuids();
				flag = this.IsChanged(allOfflineAddressBookGuids, this.cachedOabs);
				if (flag)
				{
					return true;
				}
				Guid[] allDatabaseGuidsForOrganizationMailboxes = DirectoryAccessor.Instance.GetAllDatabaseGuidsForOrganizationMailboxes();
				flag = this.IsChanged(allDatabaseGuidsForOrganizationMailboxes, this.cachedOrgMailboxDatabases);
				if (flag)
				{
					return true;
				}
			}
			return flag;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000CAAF3 File Offset: 0x000C8CF3
		private bool IsChanged(Guid[] cachedArray, Guid[] newArray)
		{
			return ((newArray == null || newArray.Length == 0) && cachedArray.Length > 0) || (newArray != null && (newArray.Length != cachedArray.Length || !newArray.SequenceEqual(cachedArray)));
		}

		// Token: 0x04001839 RID: 6201
		private Guid[] cachedOabs;

		// Token: 0x0400183A RID: 6202
		private Guid[] cachedOrgMailboxDatabases;
	}
}
