using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200035D RID: 861
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxDatabaseNotOnServerTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600268F RID: 9871 RVA: 0x00053545 File Offset: 0x00051745
		public MailboxDatabaseNotOnServerTransientException(string mdbName, Guid mdbId, string mdbServerFqdn, string currentServerFqdn) : base(MrsStrings.MdbNotOnServer(mdbName, mdbId, mdbServerFqdn, currentServerFqdn))
		{
			this.mdbName = mdbName;
			this.mdbId = mdbId;
			this.mdbServerFqdn = mdbServerFqdn;
			this.currentServerFqdn = currentServerFqdn;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00053574 File Offset: 0x00051774
		public MailboxDatabaseNotOnServerTransientException(string mdbName, Guid mdbId, string mdbServerFqdn, string currentServerFqdn, Exception innerException) : base(MrsStrings.MdbNotOnServer(mdbName, mdbId, mdbServerFqdn, currentServerFqdn), innerException)
		{
			this.mdbName = mdbName;
			this.mdbId = mdbId;
			this.mdbServerFqdn = mdbServerFqdn;
			this.currentServerFqdn = currentServerFqdn;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000535A8 File Offset: 0x000517A8
		protected MailboxDatabaseNotOnServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbName = (string)info.GetValue("mdbName", typeof(string));
			this.mdbId = (Guid)info.GetValue("mdbId", typeof(Guid));
			this.mdbServerFqdn = (string)info.GetValue("mdbServerFqdn", typeof(string));
			this.currentServerFqdn = (string)info.GetValue("currentServerFqdn", typeof(string));
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00053640 File Offset: 0x00051840
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbName", this.mdbName);
			info.AddValue("mdbId", this.mdbId);
			info.AddValue("mdbServerFqdn", this.mdbServerFqdn);
			info.AddValue("currentServerFqdn", this.currentServerFqdn);
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x0005369E File Offset: 0x0005189E
		public string MdbName
		{
			get
			{
				return this.mdbName;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000536A6 File Offset: 0x000518A6
		public Guid MdbId
		{
			get
			{
				return this.mdbId;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000536AE File Offset: 0x000518AE
		public string MdbServerFqdn
		{
			get
			{
				return this.mdbServerFqdn;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000536B6 File Offset: 0x000518B6
		public string CurrentServerFqdn
		{
			get
			{
				return this.currentServerFqdn;
			}
		}

		// Token: 0x04001064 RID: 4196
		private readonly string mdbName;

		// Token: 0x04001065 RID: 4197
		private readonly Guid mdbId;

		// Token: 0x04001066 RID: 4198
		private readonly string mdbServerFqdn;

		// Token: 0x04001067 RID: 4199
		private readonly string currentServerFqdn;
	}
}
