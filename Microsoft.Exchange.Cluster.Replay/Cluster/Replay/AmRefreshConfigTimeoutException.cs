using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200047C RID: 1148
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmRefreshConfigTimeoutException : AmCommonTransientException
	{
		// Token: 0x06002C0B RID: 11275 RVA: 0x000BE89E File Offset: 0x000BCA9E
		public AmRefreshConfigTimeoutException(int timeoutSecs) : base(ReplayStrings.AmRefreshConfigTimeoutError(timeoutSecs))
		{
			this.timeoutSecs = timeoutSecs;
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000BE8B8 File Offset: 0x000BCAB8
		public AmRefreshConfigTimeoutException(int timeoutSecs, Exception innerException) : base(ReplayStrings.AmRefreshConfigTimeoutError(timeoutSecs), innerException)
		{
			this.timeoutSecs = timeoutSecs;
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000BE8D3 File Offset: 0x000BCAD3
		protected AmRefreshConfigTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.timeoutSecs = (int)info.GetValue("timeoutSecs", typeof(int));
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000BE8FD File Offset: 0x000BCAFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("timeoutSecs", this.timeoutSecs);
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000BE918 File Offset: 0x000BCB18
		public int TimeoutSecs
		{
			get
			{
				return this.timeoutSecs;
			}
		}

		// Token: 0x040014C2 RID: 5314
		private readonly int timeoutSecs;
	}
}
