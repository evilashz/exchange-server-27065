using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000411 RID: 1041
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceResumeInvalidDuringMoveException : TaskServerException
	{
		// Token: 0x060029C4 RID: 10692 RVA: 0x000BA345 File Offset: 0x000B8545
		public ReplayServiceResumeInvalidDuringMoveException(string dbCopy) : base(ReplayStrings.ReplayServiceResumeInvalidDuringMoveException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000BA35F File Offset: 0x000B855F
		public ReplayServiceResumeInvalidDuringMoveException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceResumeInvalidDuringMoveException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000BA37A File Offset: 0x000B857A
		protected ReplayServiceResumeInvalidDuringMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000BA3A4 File Offset: 0x000B85A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000BA3BF File Offset: 0x000B85BF
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001427 RID: 5159
		private readonly string dbCopy;
	}
}
