using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000468 RID: 1128
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbOperationTimedoutException : AmDbOperationException
	{
		// Token: 0x06002B9F RID: 11167 RVA: 0x000BDBC5 File Offset: 0x000BBDC5
		public AmDbOperationTimedoutException(string dbName, string opr, TimeSpan timeout) : base(ReplayStrings.AmDbOperationTimedoutException(dbName, opr, timeout))
		{
			this.dbName = dbName;
			this.opr = opr;
			this.timeout = timeout;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000BDBEF File Offset: 0x000BBDEF
		public AmDbOperationTimedoutException(string dbName, string opr, TimeSpan timeout, Exception innerException) : base(ReplayStrings.AmDbOperationTimedoutException(dbName, opr, timeout), innerException)
		{
			this.dbName = dbName;
			this.opr = opr;
			this.timeout = timeout;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000BDC1C File Offset: 0x000BBE1C
		protected AmDbOperationTimedoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.opr = (string)info.GetValue("opr", typeof(string));
			this.timeout = (TimeSpan)info.GetValue("timeout", typeof(TimeSpan));
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000BDC94 File Offset: 0x000BBE94
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("opr", this.opr);
			info.AddValue("timeout", this.timeout);
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000BDCE1 File Offset: 0x000BBEE1
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000BDCE9 File Offset: 0x000BBEE9
		public string Opr
		{
			get
			{
				return this.opr;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x000BDCF1 File Offset: 0x000BBEF1
		public TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x040014A6 RID: 5286
		private readonly string dbName;

		// Token: 0x040014A7 RID: 5287
		private readonly string opr;

		// Token: 0x040014A8 RID: 5288
		private readonly TimeSpan timeout;
	}
}
