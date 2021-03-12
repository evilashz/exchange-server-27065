using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003EE RID: 1006
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederRpcSafeDeleteUnsupportedException : SeederServerException
	{
		// Token: 0x06002918 RID: 10520 RVA: 0x000B91E2 File Offset: 0x000B73E2
		public SeederRpcSafeDeleteUnsupportedException(string serverName, string serverVersion, string supportedVersion) : base(ReplayStrings.SeederRpcSafeDeleteUnsupportedException(serverName, serverVersion, supportedVersion))
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000B920C File Offset: 0x000B740C
		public SeederRpcSafeDeleteUnsupportedException(string serverName, string serverVersion, string supportedVersion, Exception innerException) : base(ReplayStrings.SeederRpcSafeDeleteUnsupportedException(serverName, serverVersion, supportedVersion), innerException)
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000B9238 File Offset: 0x000B7438
		protected SeederRpcSafeDeleteUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
			this.supportedVersion = (string)info.GetValue("supportedVersion", typeof(string));
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000B92AD File Offset: 0x000B74AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("serverVersion", this.serverVersion);
			info.AddValue("supportedVersion", this.supportedVersion);
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x000B92EA File Offset: 0x000B74EA
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000B92F2 File Offset: 0x000B74F2
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000B92FA File Offset: 0x000B74FA
		public string SupportedVersion
		{
			get
			{
				return this.supportedVersion;
			}
		}

		// Token: 0x04001407 RID: 5127
		private readonly string serverName;

		// Token: 0x04001408 RID: 5128
		private readonly string serverVersion;

		// Token: 0x04001409 RID: 5129
		private readonly string supportedVersion;
	}
}
