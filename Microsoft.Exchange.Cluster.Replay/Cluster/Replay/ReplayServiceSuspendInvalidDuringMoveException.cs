using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000410 RID: 1040
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendInvalidDuringMoveException : TaskServerException
	{
		// Token: 0x060029BF RID: 10687 RVA: 0x000BA2C3 File Offset: 0x000B84C3
		public ReplayServiceSuspendInvalidDuringMoveException(string dbCopy) : base(ReplayStrings.ReplayServiceSuspendInvalidDuringMoveException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000BA2DD File Offset: 0x000B84DD
		public ReplayServiceSuspendInvalidDuringMoveException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceSuspendInvalidDuringMoveException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000BA2F8 File Offset: 0x000B84F8
		protected ReplayServiceSuspendInvalidDuringMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000BA322 File Offset: 0x000B8522
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000BA33D File Offset: 0x000B853D
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001426 RID: 5158
		private readonly string dbCopy;
	}
}
