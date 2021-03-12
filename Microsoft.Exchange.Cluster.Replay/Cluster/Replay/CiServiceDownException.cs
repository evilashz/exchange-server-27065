using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000452 RID: 1106
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CiServiceDownException : SeederServerException
	{
		// Token: 0x06002B31 RID: 11057 RVA: 0x000BD099 File Offset: 0x000BB299
		public CiServiceDownException(string serverName, string rpcErrorMessage) : base(ReplayStrings.CiServiceDownException(serverName, rpcErrorMessage))
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000BD0BB File Offset: 0x000BB2BB
		public CiServiceDownException(string serverName, string rpcErrorMessage, Exception innerException) : base(ReplayStrings.CiServiceDownException(serverName, rpcErrorMessage), innerException)
		{
			this.serverName = serverName;
			this.rpcErrorMessage = rpcErrorMessage;
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000BD0E0 File Offset: 0x000BB2E0
		protected CiServiceDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.rpcErrorMessage = (string)info.GetValue("rpcErrorMessage", typeof(string));
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000BD135 File Offset: 0x000BB335
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("rpcErrorMessage", this.rpcErrorMessage);
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000BD161 File Offset: 0x000BB361
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000BD169 File Offset: 0x000BB369
		public string RpcErrorMessage
		{
			get
			{
				return this.rpcErrorMessage;
			}
		}

		// Token: 0x04001490 RID: 5264
		private readonly string serverName;

		// Token: 0x04001491 RID: 5265
		private readonly string rpcErrorMessage;
	}
}
