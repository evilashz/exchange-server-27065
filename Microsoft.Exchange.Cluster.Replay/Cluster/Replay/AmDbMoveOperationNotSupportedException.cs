using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000471 RID: 1137
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMoveOperationNotSupportedException : AmDbActionException
	{
		// Token: 0x06002BCE RID: 11214 RVA: 0x000BE121 File Offset: 0x000BC321
		public AmDbMoveOperationNotSupportedException(string dbName) : base(ReplayStrings.AmDbMoveOperationNotSupportedException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000BE13B File Offset: 0x000BC33B
		public AmDbMoveOperationNotSupportedException(string dbName, Exception innerException) : base(ReplayStrings.AmDbMoveOperationNotSupportedException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000BE156 File Offset: 0x000BC356
		protected AmDbMoveOperationNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000BE180 File Offset: 0x000BC380
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x000BE19B File Offset: 0x000BC39B
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014B1 RID: 5297
		private readonly string dbName;
	}
}
