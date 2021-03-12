using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000523 RID: 1315
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DumpsterInvalidResubmitRequestException : DumpsterRedeliveryException
	{
		// Token: 0x06002FCB RID: 12235 RVA: 0x000C63E9 File Offset: 0x000C45E9
		public DumpsterInvalidResubmitRequestException(string dbName) : base(ReplayStrings.DumpsterInvalidResubmitRequestException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000C6403 File Offset: 0x000C4603
		public DumpsterInvalidResubmitRequestException(string dbName, Exception innerException) : base(ReplayStrings.DumpsterInvalidResubmitRequestException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000C641E File Offset: 0x000C461E
		protected DumpsterInvalidResubmitRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000C6448 File Offset: 0x000C4648
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000C6463 File Offset: 0x000C4663
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040015E6 RID: 5606
		private readonly string dbName;
	}
}
