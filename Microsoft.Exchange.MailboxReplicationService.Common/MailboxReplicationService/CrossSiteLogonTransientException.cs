using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002AC RID: 684
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CrossSiteLogonTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002319 RID: 8985 RVA: 0x0004DEB1 File Offset: 0x0004C0B1
		public CrossSiteLogonTransientException(Guid mdbGuid, Guid serverGuid, string serverSite, string localSite) : base(MrsStrings.CrossSiteError(mdbGuid, serverGuid, serverSite, localSite))
		{
			this.mdbGuid = mdbGuid;
			this.serverGuid = serverGuid;
			this.serverSite = serverSite;
			this.localSite = localSite;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0004DEE0 File Offset: 0x0004C0E0
		public CrossSiteLogonTransientException(Guid mdbGuid, Guid serverGuid, string serverSite, string localSite, Exception innerException) : base(MrsStrings.CrossSiteError(mdbGuid, serverGuid, serverSite, localSite), innerException)
		{
			this.mdbGuid = mdbGuid;
			this.serverGuid = serverGuid;
			this.serverSite = serverSite;
			this.localSite = localSite;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x0004DF14 File Offset: 0x0004C114
		protected CrossSiteLogonTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbGuid = (Guid)info.GetValue("mdbGuid", typeof(Guid));
			this.serverGuid = (Guid)info.GetValue("serverGuid", typeof(Guid));
			this.serverSite = (string)info.GetValue("serverSite", typeof(string));
			this.localSite = (string)info.GetValue("localSite", typeof(string));
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x0004DFAC File Offset: 0x0004C1AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbGuid", this.mdbGuid);
			info.AddValue("serverGuid", this.serverGuid);
			info.AddValue("serverSite", this.serverSite);
			info.AddValue("localSite", this.localSite);
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0004E00F File Offset: 0x0004C20F
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x0004E017 File Offset: 0x0004C217
		public Guid ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0004E01F File Offset: 0x0004C21F
		public string ServerSite
		{
			get
			{
				return this.serverSite;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x0004E027 File Offset: 0x0004C227
		public string LocalSite
		{
			get
			{
				return this.localSite;
			}
		}

		// Token: 0x04000FB2 RID: 4018
		private readonly Guid mdbGuid;

		// Token: 0x04000FB3 RID: 4019
		private readonly Guid serverGuid;

		// Token: 0x04000FB4 RID: 4020
		private readonly string serverSite;

		// Token: 0x04000FB5 RID: 4021
		private readonly string localSite;
	}
}
