using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EE5 RID: 3813
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DbCopyAlreadyHostedOnServerException : LocalizedException
	{
		// Token: 0x0600A955 RID: 43349 RVA: 0x0028B70D File Offset: 0x0028990D
		public DbCopyAlreadyHostedOnServerException(string database, string hostServer) : base(Strings.DbCopyAlreadyHostedOnServerException(database, hostServer))
		{
			this.database = database;
			this.hostServer = hostServer;
		}

		// Token: 0x0600A956 RID: 43350 RVA: 0x0028B72A File Offset: 0x0028992A
		public DbCopyAlreadyHostedOnServerException(string database, string hostServer, Exception innerException) : base(Strings.DbCopyAlreadyHostedOnServerException(database, hostServer), innerException)
		{
			this.database = database;
			this.hostServer = hostServer;
		}

		// Token: 0x0600A957 RID: 43351 RVA: 0x0028B748 File Offset: 0x00289948
		protected DbCopyAlreadyHostedOnServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.hostServer = (string)info.GetValue("hostServer", typeof(string));
		}

		// Token: 0x0600A958 RID: 43352 RVA: 0x0028B79D File Offset: 0x0028999D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("hostServer", this.hostServer);
		}

		// Token: 0x170036DE RID: 14046
		// (get) Token: 0x0600A959 RID: 43353 RVA: 0x0028B7C9 File Offset: 0x002899C9
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x170036DF RID: 14047
		// (get) Token: 0x0600A95A RID: 43354 RVA: 0x0028B7D1 File Offset: 0x002899D1
		public string HostServer
		{
			get
			{
				return this.hostServer;
			}
		}

		// Token: 0x04006044 RID: 24644
		private readonly string database;

		// Token: 0x04006045 RID: 24645
		private readonly string hostServer;
	}
}
