using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000486 RID: 1158
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmBcsFailedToQueryCopiesException : AmBcsSelectionException
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000BF0A7 File Offset: 0x000BD2A7
		public AmBcsFailedToQueryCopiesException(string dbName, string queryError) : base(ReplayStrings.AmBcsFailedToQueryCopiesException(dbName, queryError))
		{
			this.dbName = dbName;
			this.queryError = queryError;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000BF0C9 File Offset: 0x000BD2C9
		public AmBcsFailedToQueryCopiesException(string dbName, string queryError, Exception innerException) : base(ReplayStrings.AmBcsFailedToQueryCopiesException(dbName, queryError), innerException)
		{
			this.dbName = dbName;
			this.queryError = queryError;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000BF0EC File Offset: 0x000BD2EC
		protected AmBcsFailedToQueryCopiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.queryError = (string)info.GetValue("queryError", typeof(string));
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000BF141 File Offset: 0x000BD341
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("queryError", this.queryError);
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000BF16D File Offset: 0x000BD36D
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000BF175 File Offset: 0x000BD375
		public string QueryError
		{
			get
			{
				return this.queryError;
			}
		}

		// Token: 0x040014D5 RID: 5333
		private readonly string dbName;

		// Token: 0x040014D6 RID: 5334
		private readonly string queryError;
	}
}
