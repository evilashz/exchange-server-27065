using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004BE RID: 1214
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseRemountFailedException : LocalizedException
	{
		// Token: 0x06002D89 RID: 11657 RVA: 0x000C19C5 File Offset: 0x000BFBC5
		public DatabaseRemountFailedException(string dbName, string msg) : base(ReplayStrings.DatabaseRemountFailedException(dbName, msg))
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000C19E2 File Offset: 0x000BFBE2
		public DatabaseRemountFailedException(string dbName, string msg, Exception innerException) : base(ReplayStrings.DatabaseRemountFailedException(dbName, msg), innerException)
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000C1A00 File Offset: 0x000BFC00
		protected DatabaseRemountFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000C1A55 File Offset: 0x000BFC55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000C1A81 File Offset: 0x000BFC81
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000C1A89 File Offset: 0x000BFC89
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001538 RID: 5432
		private readonly string dbName;

		// Token: 0x04001539 RID: 5433
		private readonly string msg;
	}
}
