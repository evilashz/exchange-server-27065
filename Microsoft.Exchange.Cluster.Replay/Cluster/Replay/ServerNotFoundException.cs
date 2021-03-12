using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000494 RID: 1172
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerNotFoundException : TransientException
	{
		// Token: 0x06002C93 RID: 11411 RVA: 0x000BF9CA File Offset: 0x000BDBCA
		public ServerNotFoundException(string serverName) : base(ReplayStrings.ServerNotFoundException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000BF9DF File Offset: 0x000BDBDF
		public ServerNotFoundException(string serverName, Exception innerException) : base(ReplayStrings.ServerNotFoundException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000BF9F5 File Offset: 0x000BDBF5
		protected ServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000BFA1F File Offset: 0x000BDC1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000BFA3A File Offset: 0x000BDC3A
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040014EA RID: 5354
		private readonly string serverName;
	}
}
