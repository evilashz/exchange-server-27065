using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200040D RID: 1037
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendRpcInvalidSeedingSourceException : TaskServerException
	{
		// Token: 0x060029B0 RID: 10672 RVA: 0x000BA13D File Offset: 0x000B833D
		public ReplayServiceSuspendRpcInvalidSeedingSourceException(string dbCopy) : base(ReplayStrings.ReplayServiceSuspendRpcInvalidSeedingSourceException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000BA157 File Offset: 0x000B8357
		public ReplayServiceSuspendRpcInvalidSeedingSourceException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceSuspendRpcInvalidSeedingSourceException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000BA172 File Offset: 0x000B8372
		protected ReplayServiceSuspendRpcInvalidSeedingSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000BA19C File Offset: 0x000B839C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x000BA1B7 File Offset: 0x000B83B7
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001423 RID: 5155
		private readonly string dbCopy;
	}
}
