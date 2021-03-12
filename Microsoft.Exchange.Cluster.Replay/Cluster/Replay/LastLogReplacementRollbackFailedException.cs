using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B0 RID: 1200
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementRollbackFailedException : LastLogReplacementException
	{
		// Token: 0x06002D3A RID: 11578 RVA: 0x000C1026 File Offset: 0x000BF226
		public LastLogReplacementRollbackFailedException(string dbCopy, string error) : base(ReplayStrings.LastLogReplacementRollbackFailedException(dbCopy, error))
		{
			this.dbCopy = dbCopy;
			this.error = error;
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000C1048 File Offset: 0x000BF248
		public LastLogReplacementRollbackFailedException(string dbCopy, string error, Exception innerException) : base(ReplayStrings.LastLogReplacementRollbackFailedException(dbCopy, error), innerException)
		{
			this.dbCopy = dbCopy;
			this.error = error;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000C106C File Offset: 0x000BF26C
		protected LastLogReplacementRollbackFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000C10C1 File Offset: 0x000BF2C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000C10ED File Offset: 0x000BF2ED
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000C10F5 File Offset: 0x000BF2F5
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001521 RID: 5409
		private readonly string dbCopy;

		// Token: 0x04001522 RID: 5410
		private readonly string error;
	}
}
