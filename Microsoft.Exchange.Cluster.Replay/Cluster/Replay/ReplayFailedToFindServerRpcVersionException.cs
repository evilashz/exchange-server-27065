using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000419 RID: 1049
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayFailedToFindServerRpcVersionException : TaskServerTransientException
	{
		// Token: 0x060029EF RID: 10735 RVA: 0x000BA839 File Offset: 0x000B8A39
		public ReplayFailedToFindServerRpcVersionException(string serverName) : base(ReplayStrings.ReplayFailedToFindServerRpcVersionException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000BA853 File Offset: 0x000B8A53
		public ReplayFailedToFindServerRpcVersionException(string serverName, Exception innerException) : base(ReplayStrings.ReplayFailedToFindServerRpcVersionException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000BA86E File Offset: 0x000B8A6E
		protected ReplayFailedToFindServerRpcVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000BA898 File Offset: 0x000B8A98
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x000BA8B3 File Offset: 0x000B8AB3
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04001432 RID: 5170
		private readonly string serverName;
	}
}
