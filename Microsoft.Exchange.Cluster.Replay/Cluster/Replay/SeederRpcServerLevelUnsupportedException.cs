using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003EF RID: 1007
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederRpcServerLevelUnsupportedException : SeederServerException
	{
		// Token: 0x0600291F RID: 10527 RVA: 0x000B9302 File Offset: 0x000B7502
		public SeederRpcServerLevelUnsupportedException(string serverName, string serverVersion, string supportedVersion) : base(ReplayStrings.SeederRpcServerLevelUnsupportedException(serverName, serverVersion, supportedVersion))
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000B932C File Offset: 0x000B752C
		public SeederRpcServerLevelUnsupportedException(string serverName, string serverVersion, string supportedVersion, Exception innerException) : base(ReplayStrings.SeederRpcServerLevelUnsupportedException(serverName, serverVersion, supportedVersion), innerException)
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000B9358 File Offset: 0x000B7558
		protected SeederRpcServerLevelUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
			this.supportedVersion = (string)info.GetValue("supportedVersion", typeof(string));
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000B93CD File Offset: 0x000B75CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("serverVersion", this.serverVersion);
			info.AddValue("supportedVersion", this.supportedVersion);
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x000B940A File Offset: 0x000B760A
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x000B9412 File Offset: 0x000B7612
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000B941A File Offset: 0x000B761A
		public string SupportedVersion
		{
			get
			{
				return this.supportedVersion;
			}
		}

		// Token: 0x0400140A RID: 5130
		private readonly string serverName;

		// Token: 0x0400140B RID: 5131
		private readonly string serverVersion;

		// Token: 0x0400140C RID: 5132
		private readonly string supportedVersion;
	}
}
