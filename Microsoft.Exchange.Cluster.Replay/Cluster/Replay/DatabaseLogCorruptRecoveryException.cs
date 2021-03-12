using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004BC RID: 1212
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseLogCorruptRecoveryException : LocalizedException
	{
		// Token: 0x06002D7D RID: 11645 RVA: 0x000C182D File Offset: 0x000BFA2D
		public DatabaseLogCorruptRecoveryException(string dbName, string msg) : base(ReplayStrings.DatabaseLogCorruptRecoveryFailed(dbName, msg))
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000C184A File Offset: 0x000BFA4A
		public DatabaseLogCorruptRecoveryException(string dbName, string msg, Exception innerException) : base(ReplayStrings.DatabaseLogCorruptRecoveryFailed(dbName, msg), innerException)
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000C1868 File Offset: 0x000BFA68
		protected DatabaseLogCorruptRecoveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000C18BD File Offset: 0x000BFABD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000C18E9 File Offset: 0x000BFAE9
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000C18F1 File Offset: 0x000BFAF1
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001534 RID: 5428
		private readonly string dbName;

		// Token: 0x04001535 RID: 5429
		private readonly string msg;
	}
}
