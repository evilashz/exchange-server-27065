using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000458 RID: 1112
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServiceMonitorSystemShutdownException : LocalizedException
	{
		// Token: 0x06002B4F RID: 11087 RVA: 0x000BD38A File Offset: 0x000BB58A
		public AmServiceMonitorSystemShutdownException(string serviceName) : base(ReplayStrings.AmServiceMonitorSystemShutdownException(serviceName))
		{
			this.serviceName = serviceName;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000BD39F File Offset: 0x000BB59F
		public AmServiceMonitorSystemShutdownException(string serviceName, Exception innerException) : base(ReplayStrings.AmServiceMonitorSystemShutdownException(serviceName), innerException)
		{
			this.serviceName = serviceName;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000BD3B5 File Offset: 0x000BB5B5
		protected AmServiceMonitorSystemShutdownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000BD3DF File Offset: 0x000BB5DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000BD3FA File Offset: 0x000BB5FA
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x04001496 RID: 5270
		private readonly string serviceName;
	}
}
