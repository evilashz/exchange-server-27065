using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000412 RID: 1042
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSyncStateInvalidDuringMoveException : TaskServerException
	{
		// Token: 0x060029C9 RID: 10697 RVA: 0x000BA3C7 File Offset: 0x000B85C7
		public ReplayServiceSyncStateInvalidDuringMoveException(string dbCopy) : base(ReplayStrings.ReplayServiceSyncStateInvalidDuringMoveException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000BA3E1 File Offset: 0x000B85E1
		public ReplayServiceSyncStateInvalidDuringMoveException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceSyncStateInvalidDuringMoveException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000BA3FC File Offset: 0x000B85FC
		protected ReplayServiceSyncStateInvalidDuringMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000BA426 File Offset: 0x000B8626
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000BA441 File Offset: 0x000B8641
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001428 RID: 5160
		private readonly string dbCopy;
	}
}
