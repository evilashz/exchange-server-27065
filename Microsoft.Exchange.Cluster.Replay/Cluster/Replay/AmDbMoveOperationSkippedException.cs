using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000472 RID: 1138
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMoveOperationSkippedException : AmDbActionException
	{
		// Token: 0x06002BD3 RID: 11219 RVA: 0x000BE1A3 File Offset: 0x000BC3A3
		public AmDbMoveOperationSkippedException(string dbName, string reason) : base(ReplayStrings.AmDbMoveOperationSkippedException(dbName, reason))
		{
			this.dbName = dbName;
			this.reason = reason;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000BE1C5 File Offset: 0x000BC3C5
		public AmDbMoveOperationSkippedException(string dbName, string reason, Exception innerException) : base(ReplayStrings.AmDbMoveOperationSkippedException(dbName, reason), innerException)
		{
			this.dbName = dbName;
			this.reason = reason;
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000BE1E8 File Offset: 0x000BC3E8
		protected AmDbMoveOperationSkippedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000BE23D File Offset: 0x000BC43D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000BE269 File Offset: 0x000BC469
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x000BE271 File Offset: 0x000BC471
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040014B2 RID: 5298
		private readonly string dbName;

		// Token: 0x040014B3 RID: 5299
		private readonly string reason;
	}
}
