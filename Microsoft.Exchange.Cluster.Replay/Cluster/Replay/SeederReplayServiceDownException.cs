using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000439 RID: 1081
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederReplayServiceDownException : SeederServerException
	{
		// Token: 0x06002AAB RID: 10923 RVA: 0x000BC0C1 File Offset: 0x000BA2C1
		public SeederReplayServiceDownException(string serverName, string rpcErrorMessage) : base(ReplayStrings.SeederReplayServiceDownException(serverName, rpcErrorMessage))
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000BC0E3 File Offset: 0x000BA2E3
		public SeederReplayServiceDownException(string serverName, string rpcErrorMessage, Exception innerException) : base(ReplayStrings.SeederReplayServiceDownException(serverName, rpcErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000BC108 File Offset: 0x000BA308
		protected SeederReplayServiceDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.rpcErrorMessage = (string)info.GetValue("rpcErrorMessage", typeof(string));
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000BC15D File Offset: 0x000BA35D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("rpcErrorMessage", this.rpcErrorMessage);
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x000BC189 File Offset: 0x000BA389
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000BC191 File Offset: 0x000BA391
		public string RpcErrorMessage
		{
			get
			{
				return this.rpcErrorMessage;
			}
		}

		// Token: 0x0400146E RID: 5230
		private readonly string serverName;

		// Token: 0x0400146F RID: 5231
		private readonly string rpcErrorMessage;
	}
}
