using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004BD RID: 1213
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailureItemRecoveryException : LocalizedException
	{
		// Token: 0x06002D83 RID: 11651 RVA: 0x000C18F9 File Offset: 0x000BFAF9
		public FailureItemRecoveryException(string dbName, string msg) : base(ReplayStrings.FailureItemRecoveryFailed(dbName, msg))
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000C1916 File Offset: 0x000BFB16
		public FailureItemRecoveryException(string dbName, string msg, Exception innerException) : base(ReplayStrings.FailureItemRecoveryFailed(dbName, msg), innerException)
		{
			this.dbName = dbName;
			this.msg = msg;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000C1934 File Offset: 0x000BFB34
		protected FailureItemRecoveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000C1989 File Offset: 0x000BFB89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000C19B5 File Offset: 0x000BFBB5
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000C19BD File Offset: 0x000BFBBD
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001536 RID: 5430
		private readonly string dbName;

		// Token: 0x04001537 RID: 5431
		private readonly string msg;
	}
}
