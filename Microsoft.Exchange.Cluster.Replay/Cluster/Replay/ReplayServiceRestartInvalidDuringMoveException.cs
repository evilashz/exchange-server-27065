using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200040E RID: 1038
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceRestartInvalidDuringMoveException : TaskServerException
	{
		// Token: 0x060029B5 RID: 10677 RVA: 0x000BA1BF File Offset: 0x000B83BF
		public ReplayServiceRestartInvalidDuringMoveException(string dbCopy) : base(ReplayStrings.ReplayServiceRestartInvalidDuringMoveException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000BA1D9 File Offset: 0x000B83D9
		public ReplayServiceRestartInvalidDuringMoveException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceRestartInvalidDuringMoveException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000BA1F4 File Offset: 0x000B83F4
		protected ReplayServiceRestartInvalidDuringMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000BA21E File Offset: 0x000B841E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000BA239 File Offset: 0x000B8439
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001424 RID: 5156
		private readonly string dbCopy;
	}
}
