using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000529 RID: 1321
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToGetProcessForServiceException : TransientException
	{
		// Token: 0x06002FE9 RID: 12265 RVA: 0x000C66F5 File Offset: 0x000C48F5
		public FailedToGetProcessForServiceException(string serviceName, string msg) : base(ReplayStrings.FailedToGetProcessForServiceException(serviceName, msg))
		{
			this.serviceName = serviceName;
			this.msg = msg;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000C6712 File Offset: 0x000C4912
		public FailedToGetProcessForServiceException(string serviceName, string msg, Exception innerException) : base(ReplayStrings.FailedToGetProcessForServiceException(serviceName, msg), innerException)
		{
			this.serviceName = serviceName;
			this.msg = msg;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000C6730 File Offset: 0x000C4930
		protected FailedToGetProcessForServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000C6785 File Offset: 0x000C4985
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x000C67B1 File Offset: 0x000C49B1
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x000C67B9 File Offset: 0x000C49B9
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040015EC RID: 5612
		private readonly string serviceName;

		// Token: 0x040015ED RID: 5613
		private readonly string msg;
	}
}
