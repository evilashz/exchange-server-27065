using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004BB RID: 1211
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseFailoverFailedException : LocalizedException
	{
		// Token: 0x06002D77 RID: 11639 RVA: 0x000C175E File Offset: 0x000BF95E
		public DatabaseFailoverFailedException(string dbName, string msg) : base(ReplayStrings.DatabaseFailoverFailedException(dbName, msg))
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000C177B File Offset: 0x000BF97B
		public DatabaseFailoverFailedException(string dbName, string msg, Exception innerException) : base(ReplayStrings.DatabaseFailoverFailedException(dbName, msg), innerException)
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000C179C File Offset: 0x000BF99C
		protected DatabaseFailoverFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000C17F1 File Offset: 0x000BF9F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000C181D File Offset: 0x000BFA1D
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000C1825 File Offset: 0x000BFA25
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001532 RID: 5426
		private readonly string dbName;

		// Token: 0x04001533 RID: 5427
		private readonly string msg;
	}
}
