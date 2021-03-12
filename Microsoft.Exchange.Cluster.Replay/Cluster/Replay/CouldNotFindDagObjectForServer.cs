using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200048C RID: 1164
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindDagObjectForServer : TransientException
	{
		// Token: 0x06002C68 RID: 11368 RVA: 0x000BF4FE File Offset: 0x000BD6FE
		public CouldNotFindDagObjectForServer(string serverName) : base(ReplayStrings.CouldNotFindDagObjectForServer(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000BF513 File Offset: 0x000BD713
		public CouldNotFindDagObjectForServer(string serverName, Exception innerException) : base(ReplayStrings.CouldNotFindDagObjectForServer(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000BF529 File Offset: 0x000BD729
		protected CouldNotFindDagObjectForServer(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000BF553 File Offset: 0x000BD753
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000BF56E File Offset: 0x000BD76E
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040014DF RID: 5343
		private readonly string serverName;
	}
}
