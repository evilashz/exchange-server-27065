using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F0 RID: 1008
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotBeginSeedingInstanceNotInStateException : TaskServerException
	{
		// Token: 0x06002926 RID: 10534 RVA: 0x000B9422 File Offset: 0x000B7622
		public CannotBeginSeedingInstanceNotInStateException(string dbName, string state) : base(ReplayStrings.CannotBeginSeedingInstanceNotInStateException(dbName, state))
		{
			this.dbName = dbName;
			this.state = state;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000B9444 File Offset: 0x000B7644
		public CannotBeginSeedingInstanceNotInStateException(string dbName, string state, Exception innerException) : base(ReplayStrings.CannotBeginSeedingInstanceNotInStateException(dbName, state), innerException)
		{
			this.dbName = dbName;
			this.state = state;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000B9468 File Offset: 0x000B7668
		protected CannotBeginSeedingInstanceNotInStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.state = (string)info.GetValue("state", typeof(string));
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000B94BD File Offset: 0x000B76BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("state", this.state);
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x000B94E9 File Offset: 0x000B76E9
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x000B94F1 File Offset: 0x000B76F1
		public string State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0400140D RID: 5133
		private readonly string dbName;

		// Token: 0x0400140E RID: 5134
		private readonly string state;
	}
}
