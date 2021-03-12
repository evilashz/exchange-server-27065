using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000278 RID: 632
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OnDemandMailboxLocation : IMailboxLocation
	{
		// Token: 0x06001A59 RID: 6745 RVA: 0x0007BCC8 File Offset: 0x00079EC8
		public OnDemandMailboxLocation(Func<IMailboxLocation> mailboxLocationFactory)
		{
			ArgumentValidator.ThrowIfNull("mailboxLocationFactory", mailboxLocationFactory);
			this.mailboxLocationFactory = mailboxLocationFactory;
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0007BCE2 File Offset: 0x00079EE2
		public string ServerFqdn
		{
			get
			{
				return this.GetDatabaseLocation().ServerFqdn;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0007BCEF File Offset: 0x00079EEF
		public Guid ServerGuid
		{
			get
			{
				return this.GetDatabaseLocation().ServerGuid;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0007BCFC File Offset: 0x00079EFC
		public string ServerLegacyDn
		{
			get
			{
				return this.GetDatabaseLocation().ServerLegacyDn;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0007BD09 File Offset: 0x00079F09
		public int ServerVersion
		{
			get
			{
				return this.GetDatabaseLocation().ServerVersion;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0007BD16 File Offset: 0x00079F16
		public ADObjectId ServerSite
		{
			get
			{
				return this.GetDatabaseLocation().ServerSite;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x0007BD23 File Offset: 0x00079F23
		public string DatabaseName
		{
			get
			{
				return this.GetDatabaseLocation().DatabaseName;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0007BD30 File Offset: 0x00079F30
		public string RpcClientAccessServerLegacyDn
		{
			get
			{
				return this.GetDatabaseLocation().RpcClientAccessServerLegacyDn;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0007BD3D File Offset: 0x00079F3D
		public string DatabaseLegacyDn
		{
			get
			{
				return this.GetDatabaseLocation().DatabaseLegacyDn;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0007BD4A File Offset: 0x00079F4A
		public Guid HomePublicFolderDatabaseGuid
		{
			get
			{
				return this.GetDatabaseLocation().HomePublicFolderDatabaseGuid;
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0007BD57 File Offset: 0x00079F57
		private IMailboxLocation GetDatabaseLocation()
		{
			if (this.databaseLocation == null && this.mailboxLocationFactory != null)
			{
				this.databaseLocation = this.mailboxLocationFactory();
				this.mailboxLocationFactory = null;
			}
			return this.databaseLocation;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0007BD87 File Offset: 0x00079F87
		public override string ToString()
		{
			return this.GetDatabaseLocation().ToString();
		}

		// Token: 0x040012A3 RID: 4771
		private Func<IMailboxLocation> mailboxLocationFactory;

		// Token: 0x040012A4 RID: 4772
		private IMailboxLocation databaseLocation;
	}
}
