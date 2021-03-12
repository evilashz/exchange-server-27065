using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000487 RID: 1159
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmBcsGetCopyStatusRpcException : AmBcsSingleCopyValidationException
	{
		// Token: 0x06002C4C RID: 11340 RVA: 0x000BF17D File Offset: 0x000BD37D
		public AmBcsGetCopyStatusRpcException(string server, string database, string rpcError) : base(ReplayStrings.AmBcsGetCopyStatusRpcException(server, database, rpcError))
		{
			this.server = server;
			this.database = database;
			this.rpcError = rpcError;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000BF1A7 File Offset: 0x000BD3A7
		public AmBcsGetCopyStatusRpcException(string server, string database, string rpcError, Exception innerException) : base(ReplayStrings.AmBcsGetCopyStatusRpcException(server, database, rpcError), innerException)
		{
			this.server = server;
			this.database = database;
			this.rpcError = rpcError;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000BF1D4 File Offset: 0x000BD3D4
		protected AmBcsGetCopyStatusRpcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.database = (string)info.GetValue("database", typeof(string));
			this.rpcError = (string)info.GetValue("rpcError", typeof(string));
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000BF249 File Offset: 0x000BD449
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("database", this.database);
			info.AddValue("rpcError", this.rpcError);
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000BF286 File Offset: 0x000BD486
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000BF28E File Offset: 0x000BD48E
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x000BF296 File Offset: 0x000BD496
		public string RpcError
		{
			get
			{
				return this.rpcError;
			}
		}

		// Token: 0x040014D7 RID: 5335
		private readonly string server;

		// Token: 0x040014D8 RID: 5336
		private readonly string database;

		// Token: 0x040014D9 RID: 5337
		private readonly string rpcError;
	}
}
