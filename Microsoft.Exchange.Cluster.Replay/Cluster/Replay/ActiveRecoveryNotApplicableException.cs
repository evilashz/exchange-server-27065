using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004BA RID: 1210
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ActiveRecoveryNotApplicableException : LocalizedException
	{
		// Token: 0x06002D72 RID: 11634 RVA: 0x000C16E6 File Offset: 0x000BF8E6
		public ActiveRecoveryNotApplicableException(string dbName) : base(ReplayStrings.ActiveRecoveryNotApplicableException(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000C16FB File Offset: 0x000BF8FB
		public ActiveRecoveryNotApplicableException(string dbName, Exception innerException) : base(ReplayStrings.ActiveRecoveryNotApplicableException(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000C1711 File Offset: 0x000BF911
		protected ActiveRecoveryNotApplicableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000C173B File Offset: 0x000BF93B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000C1756 File Offset: 0x000BF956
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x04001531 RID: 5425
		private readonly string dbName;
	}
}
