using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000441 RID: 1089
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederInstanceNotFoundException : SeederServerException
	{
		// Token: 0x06002AD4 RID: 10964 RVA: 0x000BC535 File Offset: 0x000BA735
		public SeederInstanceNotFoundException(string dbGuid) : base(ReplayStrings.SeederInstanceNotFoundException(dbGuid))
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000BC54F File Offset: 0x000BA74F
		public SeederInstanceNotFoundException(string dbGuid, Exception innerException) : base(ReplayStrings.SeederInstanceNotFoundException(dbGuid), innerException)
		{
			this.dbGuid = dbGuid;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000BC56A File Offset: 0x000BA76A
		protected SeederInstanceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbGuid = (string)info.GetValue("dbGuid", typeof(string));
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000BC594 File Offset: 0x000BA794
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbGuid", this.dbGuid);
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x000BC5AF File Offset: 0x000BA7AF
		public string DbGuid
		{
			get
			{
				return this.dbGuid;
			}
		}

		// Token: 0x04001477 RID: 5239
		private readonly string dbGuid;
	}
}
