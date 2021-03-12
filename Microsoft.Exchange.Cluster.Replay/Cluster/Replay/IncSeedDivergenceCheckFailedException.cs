using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000497 RID: 1175
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncSeedDivergenceCheckFailedException : LocalizedException
	{
		// Token: 0x06002CA3 RID: 11427 RVA: 0x000BFB89 File Offset: 0x000BDD89
		public IncSeedDivergenceCheckFailedException(string dbName, string sourceServer, string error) : base(ReplayStrings.IncSeedDivergenceCheckFailedException(dbName, sourceServer, error))
		{
			this.dbName = dbName;
			this.sourceServer = sourceServer;
			this.error = error;
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000BFBAE File Offset: 0x000BDDAE
		public IncSeedDivergenceCheckFailedException(string dbName, string sourceServer, string error, Exception innerException) : base(ReplayStrings.IncSeedDivergenceCheckFailedException(dbName, sourceServer, error), innerException)
		{
			this.dbName = dbName;
			this.sourceServer = sourceServer;
			this.error = error;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000BFBD8 File Offset: 0x000BDDD8
		protected IncSeedDivergenceCheckFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.sourceServer = (string)info.GetValue("sourceServer", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000BFC4D File Offset: 0x000BDE4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("sourceServer", this.sourceServer);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000BFC8A File Offset: 0x000BDE8A
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000BFC92 File Offset: 0x000BDE92
		public string SourceServer
		{
			get
			{
				return this.sourceServer;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000BFC9A File Offset: 0x000BDE9A
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040014EE RID: 5358
		private readonly string dbName;

		// Token: 0x040014EF RID: 5359
		private readonly string sourceServer;

		// Token: 0x040014F0 RID: 5360
		private readonly string error;
	}
}
