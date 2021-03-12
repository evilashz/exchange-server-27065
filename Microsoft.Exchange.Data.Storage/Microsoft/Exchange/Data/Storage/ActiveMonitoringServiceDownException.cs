using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000126 RID: 294
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveMonitoringServiceDownException : ActiveMonitoringServerException
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x0006A80A File Offset: 0x00068A0A
		public ActiveMonitoringServiceDownException(string serverName, string rpcErrorMessage) : base(ServerStrings.ActiveMonitoringServiceDown(serverName, rpcErrorMessage))
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0006A82C File Offset: 0x00068A2C
		public ActiveMonitoringServiceDownException(string serverName, string rpcErrorMessage, Exception innerException) : base(ServerStrings.ActiveMonitoringServiceDown(serverName, rpcErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0006A850 File Offset: 0x00068A50
		protected ActiveMonitoringServiceDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.rpcErrorMessage = (string)info.GetValue("rpcErrorMessage", typeof(string));
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0006A8A5 File Offset: 0x00068AA5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("rpcErrorMessage", this.rpcErrorMessage);
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0006A8D1 File Offset: 0x00068AD1
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0006A8D9 File Offset: 0x00068AD9
		public string RpcErrorMessage
		{
			get
			{
				return this.rpcErrorMessage;
			}
		}

		// Token: 0x040009B1 RID: 2481
		private readonly string serverName;

		// Token: 0x040009B2 RID: 2482
		private readonly string rpcErrorMessage;
	}
}
