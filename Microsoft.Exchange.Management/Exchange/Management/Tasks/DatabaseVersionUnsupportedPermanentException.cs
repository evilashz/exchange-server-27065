using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EBA RID: 3770
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseVersionUnsupportedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A87C RID: 43132 RVA: 0x0028A1CA File Offset: 0x002883CA
		public DatabaseVersionUnsupportedPermanentException(string dbName, string serverName, string serverVersion) : base(Strings.ErrorDatabaseVersionUnsupported(dbName, serverName, serverVersion))
		{
			this.dbName = dbName;
			this.serverName = serverName;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600A87D RID: 43133 RVA: 0x0028A1EF File Offset: 0x002883EF
		public DatabaseVersionUnsupportedPermanentException(string dbName, string serverName, string serverVersion, Exception innerException) : base(Strings.ErrorDatabaseVersionUnsupported(dbName, serverName, serverVersion), innerException)
		{
			this.dbName = dbName;
			this.serverName = serverName;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600A87E RID: 43134 RVA: 0x0028A218 File Offset: 0x00288418
		protected DatabaseVersionUnsupportedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
		}

		// Token: 0x0600A87F RID: 43135 RVA: 0x0028A28D File Offset: 0x0028848D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("serverName", this.serverName);
			info.AddValue("serverVersion", this.serverVersion);
		}

		// Token: 0x170036B1 RID: 14001
		// (get) Token: 0x0600A880 RID: 43136 RVA: 0x0028A2CA File Offset: 0x002884CA
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x170036B2 RID: 14002
		// (get) Token: 0x0600A881 RID: 43137 RVA: 0x0028A2D2 File Offset: 0x002884D2
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170036B3 RID: 14003
		// (get) Token: 0x0600A882 RID: 43138 RVA: 0x0028A2DA File Offset: 0x002884DA
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x04006017 RID: 24599
		private readonly string dbName;

		// Token: 0x04006018 RID: 24600
		private readonly string serverName;

		// Token: 0x04006019 RID: 24601
		private readonly string serverVersion;
	}
}
