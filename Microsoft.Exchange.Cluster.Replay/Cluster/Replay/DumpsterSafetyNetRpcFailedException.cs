using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000520 RID: 1312
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DumpsterSafetyNetRpcFailedException : DumpsterRedeliveryException
	{
		// Token: 0x06002FB7 RID: 12215 RVA: 0x000C60BD File Offset: 0x000C42BD
		public DumpsterSafetyNetRpcFailedException(string hubServerName, string rpcStatus) : base(ReplayStrings.DumpsterSafetyNetRpcFailedException(hubServerName, rpcStatus))
		{
			this.hubServerName = hubServerName;
			this.rpcStatus = rpcStatus;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000C60DF File Offset: 0x000C42DF
		public DumpsterSafetyNetRpcFailedException(string hubServerName, string rpcStatus, Exception innerException) : base(ReplayStrings.DumpsterSafetyNetRpcFailedException(hubServerName, rpcStatus), innerException)
		{
			this.hubServerName = hubServerName;
			this.rpcStatus = rpcStatus;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000C6104 File Offset: 0x000C4304
		protected DumpsterSafetyNetRpcFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.hubServerName = (string)info.GetValue("hubServerName", typeof(string));
			this.rpcStatus = (string)info.GetValue("rpcStatus", typeof(string));
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000C6159 File Offset: 0x000C4359
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hubServerName", this.hubServerName);
			info.AddValue("rpcStatus", this.rpcStatus);
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000C6185 File Offset: 0x000C4385
		public string HubServerName
		{
			get
			{
				return this.hubServerName;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000C618D File Offset: 0x000C438D
		public string RpcStatus
		{
			get
			{
				return this.rpcStatus;
			}
		}

		// Token: 0x040015DE RID: 5598
		private readonly string hubServerName;

		// Token: 0x040015DF RID: 5599
		private readonly string rpcStatus;
	}
}
