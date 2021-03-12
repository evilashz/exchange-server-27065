using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000275 RID: 629
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxDatabaseLocation : IMailboxLocation
	{
		// Token: 0x06001A3F RID: 6719 RVA: 0x0007BA60 File Offset: 0x00079C60
		public MailboxDatabaseLocation(DatabaseLocationInfo locationInfo)
		{
			ArgumentValidator.ThrowIfNull("locationInfo", locationInfo);
			this.ServerFqdn = locationInfo.ServerFqdn;
			this.ServerGuid = locationInfo.ServerGuid;
			this.ServerLegacyDn = locationInfo.ServerLegacyDN;
			this.ServerVersion = locationInfo.ServerVersion;
			this.ServerSite = locationInfo.ServerSite;
			this.DatabaseName = locationInfo.DatabaseName;
			this.DatabaseLegacyDn = locationInfo.DatabaseLegacyDN;
			this.RpcClientAccessServerLegacyDn = locationInfo.RpcClientAccessServerLegacyDN;
			this.HomePublicFolderDatabaseGuid = locationInfo.HomePublicFolderDatabaseGuid;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0007BAEA File Offset: 0x00079CEA
		private MailboxDatabaseLocation()
		{
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0007BAF2 File Offset: 0x00079CF2
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x0007BB17 File Offset: 0x00079D17
		public string ServerFqdn
		{
			get
			{
				if (string.IsNullOrEmpty(this.serverFqdn))
				{
					throw new DatabaseLocationUnavailableException(ServerStrings.ExCurrentServerNotInSite(string.Empty));
				}
				return this.serverFqdn;
			}
			private set
			{
				this.serverFqdn = value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x0007BB20 File Offset: 0x00079D20
		// (set) Token: 0x06001A44 RID: 6724 RVA: 0x0007BB28 File Offset: 0x00079D28
		public Guid ServerGuid { get; private set; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x0007BB31 File Offset: 0x00079D31
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x0007BB39 File Offset: 0x00079D39
		public string ServerLegacyDn { get; private set; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0007BB42 File Offset: 0x00079D42
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x0007BB4A File Offset: 0x00079D4A
		public int ServerVersion { get; private set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0007BB53 File Offset: 0x00079D53
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x0007BB5B File Offset: 0x00079D5B
		public ADObjectId ServerSite { get; private set; }

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0007BB64 File Offset: 0x00079D64
		// (set) Token: 0x06001A4C RID: 6732 RVA: 0x0007BB6C File Offset: 0x00079D6C
		public string DatabaseName { get; private set; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x0007BB75 File Offset: 0x00079D75
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x0007BB7D File Offset: 0x00079D7D
		public string RpcClientAccessServerLegacyDn { get; private set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0007BB86 File Offset: 0x00079D86
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x0007BB8E File Offset: 0x00079D8E
		public string DatabaseLegacyDn { get; private set; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0007BB97 File Offset: 0x00079D97
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x0007BB9F File Offset: 0x00079D9F
		public Guid HomePublicFolderDatabaseGuid { get; private set; }

		// Token: 0x06001A53 RID: 6739 RVA: 0x0007BBA8 File Offset: 0x00079DA8
		public override string ToString()
		{
			return string.Format("ServerFqdn: {0}, ServerVersion: {1}, DatabaseName: {2}, HomePublicFolderDatabaseGuid: {3}", new object[]
			{
				this.ServerFqdn,
				this.ServerVersion,
				this.DatabaseName,
				this.HomePublicFolderDatabaseGuid
			});
		}

		// Token: 0x04001299 RID: 4761
		public static IMailboxLocation Unknown = new MailboxDatabaseLocation();

		// Token: 0x0400129A RID: 4762
		private string serverFqdn;
	}
}
