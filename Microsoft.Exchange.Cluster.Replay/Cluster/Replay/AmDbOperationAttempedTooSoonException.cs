using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000469 RID: 1129
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbOperationAttempedTooSoonException : AmDbOperationException
	{
		// Token: 0x06002BA6 RID: 11174 RVA: 0x000BDCF9 File Offset: 0x000BBEF9
		public AmDbOperationAttempedTooSoonException(string dbName) : base(ReplayStrings.AmDbOperationAttempedTooSoonException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000BDD13 File Offset: 0x000BBF13
		public AmDbOperationAttempedTooSoonException(string dbName, Exception innerException) : base(ReplayStrings.AmDbOperationAttempedTooSoonException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000BDD2E File Offset: 0x000BBF2E
		protected AmDbOperationAttempedTooSoonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000BDD58 File Offset: 0x000BBF58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000BDD73 File Offset: 0x000BBF73
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014A9 RID: 5289
		private readonly string dbName;
	}
}
