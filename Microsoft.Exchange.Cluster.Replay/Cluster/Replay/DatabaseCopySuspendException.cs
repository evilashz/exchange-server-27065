using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004BF RID: 1215
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseCopySuspendException : LocalizedException
	{
		// Token: 0x06002D8F RID: 11663 RVA: 0x000C1A91 File Offset: 0x000BFC91
		public DatabaseCopySuspendException(string dbName, string server, string msg) : base(ReplayStrings.DatabaseCopySuspendException(dbName, server, msg))
		{
			this.dbName = dbName;
			this.server = server;
			this.msg = msg;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000C1AB6 File Offset: 0x000BFCB6
		public DatabaseCopySuspendException(string dbName, string server, string msg, Exception innerException) : base(ReplayStrings.DatabaseCopySuspendException(dbName, server, msg), innerException)
		{
			this.dbName = dbName;
			this.server = server;
			this.msg = msg;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000C1AE0 File Offset: 0x000BFCE0
		protected DatabaseCopySuspendException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000C1B55 File Offset: 0x000BFD55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("server", this.server);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x000C1B92 File Offset: 0x000BFD92
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000C1B9A File Offset: 0x000BFD9A
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000C1BA2 File Offset: 0x000BFDA2
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400153A RID: 5434
		private readonly string dbName;

		// Token: 0x0400153B RID: 5435
		private readonly string server;

		// Token: 0x0400153C RID: 5436
		private readonly string msg;
	}
}
