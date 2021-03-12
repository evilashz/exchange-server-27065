using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200048D RID: 1165
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindDagObjectLookupErrorForServer : TransientException
	{
		// Token: 0x06002C6D RID: 11373 RVA: 0x000BF576 File Offset: 0x000BD776
		public CouldNotFindDagObjectLookupErrorForServer(string serverName, string error) : base(ReplayStrings.CouldNotFindDagObjectLookupErrorForServer(serverName, error))
		{
			this.serverName = serverName;
			this.error = error;
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000BF593 File Offset: 0x000BD793
		public CouldNotFindDagObjectLookupErrorForServer(string serverName, string error, Exception innerException) : base(ReplayStrings.CouldNotFindDagObjectLookupErrorForServer(serverName, error), innerException)
		{
			this.serverName = serverName;
			this.error = error;
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000BF5B4 File Offset: 0x000BD7B4
		protected CouldNotFindDagObjectLookupErrorForServer(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000BF609 File Offset: 0x000BD809
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000BF635 File Offset: 0x000BD835
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000BF63D File Offset: 0x000BD83D
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040014E0 RID: 5344
		private readonly string serverName;

		// Token: 0x040014E1 RID: 5345
		private readonly string error;
	}
}
