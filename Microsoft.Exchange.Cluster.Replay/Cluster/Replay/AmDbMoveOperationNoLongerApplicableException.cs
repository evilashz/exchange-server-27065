using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200046F RID: 1135
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMoveOperationNoLongerApplicableException : AmDbActionException
	{
		// Token: 0x06002BC1 RID: 11201 RVA: 0x000BDF2A File Offset: 0x000BC12A
		public AmDbMoveOperationNoLongerApplicableException(string dbName, string fromServer, string activeServer) : base(ReplayStrings.AmDbMoveOperationNoLongerApplicableException(dbName, fromServer, activeServer))
		{
			this.dbName = dbName;
			this.fromServer = fromServer;
			this.activeServer = activeServer;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000BDF54 File Offset: 0x000BC154
		public AmDbMoveOperationNoLongerApplicableException(string dbName, string fromServer, string activeServer, Exception innerException) : base(ReplayStrings.AmDbMoveOperationNoLongerApplicableException(dbName, fromServer, activeServer), innerException)
		{
			this.dbName = dbName;
			this.fromServer = fromServer;
			this.activeServer = activeServer;
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000BDF80 File Offset: 0x000BC180
		protected AmDbMoveOperationNoLongerApplicableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.fromServer = (string)info.GetValue("fromServer", typeof(string));
			this.activeServer = (string)info.GetValue("activeServer", typeof(string));
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000BDFF5 File Offset: 0x000BC1F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("fromServer", this.fromServer);
			info.AddValue("activeServer", this.activeServer);
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000BE032 File Offset: 0x000BC232
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000BE03A File Offset: 0x000BC23A
		public string FromServer
		{
			get
			{
				return this.fromServer;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000BE042 File Offset: 0x000BC242
		public string ActiveServer
		{
			get
			{
				return this.activeServer;
			}
		}

		// Token: 0x040014AC RID: 5292
		private readonly string dbName;

		// Token: 0x040014AD RID: 5293
		private readonly string fromServer;

		// Token: 0x040014AE RID: 5294
		private readonly string activeServer;
	}
}
