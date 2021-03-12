using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000457 RID: 1111
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterEventNotifierTransientException : AmTransientException
	{
		// Token: 0x06002B4A RID: 11082 RVA: 0x000BD308 File Offset: 0x000BB508
		public AmClusterEventNotifierTransientException(int errCode) : base(ReplayStrings.AmClusterEventNotifierTransientException(errCode))
		{
			this.errCode = errCode;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000BD322 File Offset: 0x000BB522
		public AmClusterEventNotifierTransientException(int errCode, Exception innerException) : base(ReplayStrings.AmClusterEventNotifierTransientException(errCode), innerException)
		{
			this.errCode = errCode;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000BD33D File Offset: 0x000BB53D
		protected AmClusterEventNotifierTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errCode = (int)info.GetValue("errCode", typeof(int));
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000BD367 File Offset: 0x000BB567
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errCode", this.errCode);
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x000BD382 File Offset: 0x000BB582
		public int ErrCode
		{
			get
			{
				return this.errCode;
			}
		}

		// Token: 0x04001495 RID: 5269
		private readonly int errCode;
	}
}
