using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000467 RID: 1127
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionCancelledException : AmDbActionException
	{
		// Token: 0x06002B99 RID: 11161 RVA: 0x000BDAEF File Offset: 0x000BBCEF
		public AmDbActionCancelledException(string dbName, string opr) : base(ReplayStrings.AmDbActionCancelledException(dbName, opr))
		{
			this.dbName = dbName;
			this.opr = opr;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000BDB11 File Offset: 0x000BBD11
		public AmDbActionCancelledException(string dbName, string opr, Exception innerException) : base(ReplayStrings.AmDbActionCancelledException(dbName, opr), innerException)
		{
			this.dbName = dbName;
			this.opr = opr;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000BDB34 File Offset: 0x000BBD34
		protected AmDbActionCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.opr = (string)info.GetValue("opr", typeof(string));
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000BDB89 File Offset: 0x000BBD89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("opr", this.opr);
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x000BDBB5 File Offset: 0x000BBDB5
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x000BDBBD File Offset: 0x000BBDBD
		public string Opr
		{
			get
			{
				return this.opr;
			}
		}

		// Token: 0x040014A4 RID: 5284
		private readonly string dbName;

		// Token: 0x040014A5 RID: 5285
		private readonly string opr;
	}
}
