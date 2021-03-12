using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D8 RID: 984
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagReplayServiceDownException : DagTaskServerException
	{
		// Token: 0x060028A2 RID: 10402 RVA: 0x000B8414 File Offset: 0x000B6614
		public DagReplayServiceDownException(string serverName, string rpcErrorMessage) : base(ReplayStrings.DagReplayServiceDownException(serverName, rpcErrorMessage))
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000B8436 File Offset: 0x000B6636
		public DagReplayServiceDownException(string serverName, string rpcErrorMessage, Exception innerException) : base(ReplayStrings.DagReplayServiceDownException(serverName, rpcErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000B845C File Offset: 0x000B665C
		protected DagReplayServiceDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.rpcErrorMessage = (string)info.GetValue("rpcErrorMessage", typeof(string));
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000B84B1 File Offset: 0x000B66B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("rpcErrorMessage", this.rpcErrorMessage);
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x000B84DD File Offset: 0x000B66DD
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000B84E5 File Offset: 0x000B66E5
		public string RpcErrorMessage
		{
			get
			{
				return this.rpcErrorMessage;
			}
		}

		// Token: 0x040013E9 RID: 5097
		private readonly string serverName;

		// Token: 0x040013EA RID: 5098
		private readonly string rpcErrorMessage;
	}
}
