using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B4 RID: 1204
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogCopierFailedNoLogsOnSourceException : TransientException
	{
		// Token: 0x06002D4F RID: 11599 RVA: 0x000C1271 File Offset: 0x000BF471
		public LogCopierFailedNoLogsOnSourceException(string srcServer) : base(ReplayStrings.LogCopierInitFailedBecauseNoLogsOnSource(srcServer))
		{
			this.srcServer = srcServer;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000C1286 File Offset: 0x000BF486
		public LogCopierFailedNoLogsOnSourceException(string srcServer, Exception innerException) : base(ReplayStrings.LogCopierInitFailedBecauseNoLogsOnSource(srcServer), innerException)
		{
			this.srcServer = srcServer;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000C129C File Offset: 0x000BF49C
		protected LogCopierFailedNoLogsOnSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.srcServer = (string)info.GetValue("srcServer", typeof(string));
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000C12C6 File Offset: 0x000BF4C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("srcServer", this.srcServer);
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000C12E1 File Offset: 0x000BF4E1
		public string SrcServer
		{
			get
			{
				return this.srcServer;
			}
		}

		// Token: 0x04001526 RID: 5414
		private readonly string srcServer;
	}
}
