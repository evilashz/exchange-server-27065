using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200052A RID: 1322
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToKillProcessForServiceException : TransientException
	{
		// Token: 0x06002FEF RID: 12271 RVA: 0x000C67C1 File Offset: 0x000C49C1
		public FailedToKillProcessForServiceException(string serviceName, string msg) : base(ReplayStrings.FailedToKillProcessForServiceException(serviceName, msg))
		{
			this.serviceName = serviceName;
			this.msg = msg;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000C67DE File Offset: 0x000C49DE
		public FailedToKillProcessForServiceException(string serviceName, string msg, Exception innerException) : base(ReplayStrings.FailedToKillProcessForServiceException(serviceName, msg), innerException)
		{
			this.serviceName = serviceName;
			this.msg = msg;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000C67FC File Offset: 0x000C49FC
		protected FailedToKillProcessForServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000C6851 File Offset: 0x000C4A51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000C687D File Offset: 0x000C4A7D
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x000C6885 File Offset: 0x000C4A85
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040015EE RID: 5614
		private readonly string serviceName;

		// Token: 0x040015EF RID: 5615
		private readonly string msg;
	}
}
