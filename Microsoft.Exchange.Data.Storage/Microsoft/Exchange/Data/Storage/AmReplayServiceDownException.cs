using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000CE RID: 206
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmReplayServiceDownException : AmServerException
	{
		// Token: 0x06001282 RID: 4738 RVA: 0x00067C1E File Offset: 0x00065E1E
		public AmReplayServiceDownException(string serverName, string rpcErrorMessage) : base(ServerStrings.AmReplayServiceDownException(serverName, rpcErrorMessage))
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00067C40 File Offset: 0x00065E40
		public AmReplayServiceDownException(string serverName, string rpcErrorMessage, Exception innerException) : base(ServerStrings.AmReplayServiceDownException(serverName, rpcErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00067C64 File Offset: 0x00065E64
		protected AmReplayServiceDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.rpcErrorMessage = (string)info.GetValue("rpcErrorMessage", typeof(string));
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00067CB9 File Offset: 0x00065EB9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("rpcErrorMessage", this.rpcErrorMessage);
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00067CE5 File Offset: 0x00065EE5
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00067CED File Offset: 0x00065EED
		public string RpcErrorMessage
		{
			get
			{
				return this.rpcErrorMessage;
			}
		}

		// Token: 0x0400095C RID: 2396
		private readonly string serverName;

		// Token: 0x0400095D RID: 2397
		private readonly string rpcErrorMessage;
	}
}
