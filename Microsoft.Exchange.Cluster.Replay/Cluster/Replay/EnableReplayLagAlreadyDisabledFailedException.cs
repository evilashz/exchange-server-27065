using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200041B RID: 1051
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EnableReplayLagAlreadyDisabledFailedException : TaskServerTransientException
	{
		// Token: 0x060029F9 RID: 10745 RVA: 0x000BA93D File Offset: 0x000B8B3D
		public EnableReplayLagAlreadyDisabledFailedException(string dbCopy) : base(ReplayStrings.EnableReplayLagAlreadyDisabledFailedException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000BA957 File Offset: 0x000B8B57
		public EnableReplayLagAlreadyDisabledFailedException(string dbCopy, Exception innerException) : base(ReplayStrings.EnableReplayLagAlreadyDisabledFailedException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000BA972 File Offset: 0x000B8B72
		protected EnableReplayLagAlreadyDisabledFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000BA99C File Offset: 0x000B8B9C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x000BA9B7 File Offset: 0x000B8BB7
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001434 RID: 5172
		private readonly string dbCopy;
	}
}
