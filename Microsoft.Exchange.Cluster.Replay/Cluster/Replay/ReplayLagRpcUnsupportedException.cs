using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200041C RID: 1052
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayLagRpcUnsupportedException : TaskServerException
	{
		// Token: 0x060029FE RID: 10750 RVA: 0x000BA9BF File Offset: 0x000B8BBF
		public ReplayLagRpcUnsupportedException(string serverName, string serverVersion, string supportedVersion) : base(ReplayStrings.ReplayLagRpcUnsupportedException(serverName, serverVersion, supportedVersion))
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000BA9E9 File Offset: 0x000B8BE9
		public ReplayLagRpcUnsupportedException(string serverName, string serverVersion, string supportedVersion, Exception innerException) : base(ReplayStrings.ReplayLagRpcUnsupportedException(serverName, serverVersion, supportedVersion), innerException)
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000BAA18 File Offset: 0x000B8C18
		protected ReplayLagRpcUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
			this.supportedVersion = (string)info.GetValue("supportedVersion", typeof(string));
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000BAA8D File Offset: 0x000B8C8D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("serverVersion", this.serverVersion);
			info.AddValue("supportedVersion", this.supportedVersion);
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000BAACA File Offset: 0x000B8CCA
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x000BAAD2 File Offset: 0x000B8CD2
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000BAADA File Offset: 0x000B8CDA
		public string SupportedVersion
		{
			get
			{
				return this.supportedVersion;
			}
		}

		// Token: 0x04001435 RID: 5173
		private readonly string serverName;

		// Token: 0x04001436 RID: 5174
		private readonly string serverVersion;

		// Token: 0x04001437 RID: 5175
		private readonly string supportedVersion;
	}
}
