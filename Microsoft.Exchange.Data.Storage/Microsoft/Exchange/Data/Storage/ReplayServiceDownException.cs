using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C5 RID: 197
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceDownException : TaskServerException
	{
		// Token: 0x06001258 RID: 4696 RVA: 0x00067880 File Offset: 0x00065A80
		public ReplayServiceDownException(string serverName, string rpcErrorMessage) : base(ServerStrings.ReplayServiceDown(serverName, rpcErrorMessage))
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000678A2 File Offset: 0x00065AA2
		public ReplayServiceDownException(string serverName, string rpcErrorMessage, Exception innerException) : base(ServerStrings.ReplayServiceDown(serverName, rpcErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000678C8 File Offset: 0x00065AC8
		protected ReplayServiceDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.rpcErrorMessage = (string)info.GetValue("rpcErrorMessage", typeof(string));
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0006791D File Offset: 0x00065B1D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("rpcErrorMessage", this.rpcErrorMessage);
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00067949 File Offset: 0x00065B49
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00067951 File Offset: 0x00065B51
		public string RpcErrorMessage
		{
			get
			{
				return this.rpcErrorMessage;
			}
		}

		// Token: 0x04000956 RID: 2390
		private readonly string serverName;

		// Token: 0x04000957 RID: 2391
		private readonly string rpcErrorMessage;
	}
}
