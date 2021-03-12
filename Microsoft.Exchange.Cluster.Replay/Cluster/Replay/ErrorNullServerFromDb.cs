using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200048E RID: 1166
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorNullServerFromDb : TransientException
	{
		// Token: 0x06002C73 RID: 11379 RVA: 0x000BF645 File Offset: 0x000BD845
		public ErrorNullServerFromDb(string dbName) : base(ReplayStrings.ErrorNullServerFromDb(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000BF65A File Offset: 0x000BD85A
		public ErrorNullServerFromDb(string dbName, Exception innerException) : base(ReplayStrings.ErrorNullServerFromDb(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000BF670 File Offset: 0x000BD870
		protected ErrorNullServerFromDb(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000BF69A File Offset: 0x000BD89A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000BF6B5 File Offset: 0x000BD8B5
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014E2 RID: 5346
		private readonly string dbName;
	}
}
