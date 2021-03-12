using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000434 RID: 1076
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllLastLogNotFoundException : TransientException
	{
		// Token: 0x06002A8D RID: 10893 RVA: 0x000BBCB2 File Offset: 0x000B9EB2
		public AcllLastLogNotFoundException(string dbCopy, long generation) : base(ReplayStrings.AcllLastLogNotFoundException(dbCopy, generation))
		{
			this.dbCopy = dbCopy;
			this.generation = generation;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000BBCCF File Offset: 0x000B9ECF
		public AcllLastLogNotFoundException(string dbCopy, long generation, Exception innerException) : base(ReplayStrings.AcllLastLogNotFoundException(dbCopy, generation), innerException)
		{
			this.dbCopy = dbCopy;
			this.generation = generation;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000BBCF0 File Offset: 0x000B9EF0
		protected AcllLastLogNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.generation = (long)info.GetValue("generation", typeof(long));
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000BBD45 File Offset: 0x000B9F45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("generation", this.generation);
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000BBD71 File Offset: 0x000B9F71
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x000BBD79 File Offset: 0x000B9F79
		public long Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x04001464 RID: 5220
		private readonly string dbCopy;

		// Token: 0x04001465 RID: 5221
		private readonly long generation;
	}
}
