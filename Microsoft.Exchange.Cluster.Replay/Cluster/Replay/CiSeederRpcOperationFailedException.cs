using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000451 RID: 1105
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CiSeederRpcOperationFailedException : SeederServerException
	{
		// Token: 0x06002B2C RID: 11052 RVA: 0x000BD017 File Offset: 0x000BB217
		public CiSeederRpcOperationFailedException(string errMessage) : base(ReplayStrings.CiSeederRpcOperationFailedException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000BD031 File Offset: 0x000BB231
		public CiSeederRpcOperationFailedException(string errMessage, Exception innerException) : base(ReplayStrings.CiSeederRpcOperationFailedException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000BD04C File Offset: 0x000BB24C
		protected CiSeederRpcOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000BD076 File Offset: 0x000BB276
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000BD091 File Offset: 0x000BB291
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x0400148F RID: 5263
		private readonly string errMessage;
	}
}
